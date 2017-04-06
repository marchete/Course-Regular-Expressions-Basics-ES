using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.IO;
using System;
using System.Net;

namespace RegexCourse
{
    [TestClass]
    public class Validator
    {
		public static string ReportPath = "/project/target/";
		public static string ReportTemplate = Path.Combine(ReportPath,"report.html");
		public static string RowReport = @"<tr><td>%name%</td><td><span class=""glyphicon glyphicon-%ok1% text-%ok2%"" aria-hidden=""true""></span></td><td>%match1%</td><td>%match2%</td></tr>";
		
		
		public struct RegexUseCase
        {  
          public string Name;  
          public string Value;  
		  public RegexUseCase(string n, string v){Name = n; Value = v;}
        }


        //Matching using Named Groups
        public string CreateHTMLMatches(Regex regex, RegexUseCase regexTest, List<string> GroupMatches)
        {
            string[] pen_colors = new string[] { "yellow","green","pink","blue","red","grey"};
            string HTMLMatch = "";
            int[] char_captured = new int[regexTest.Value.Length]; //Each group will have an index, starting from 0
            //Set array to -1, because index starts at 0
            for (int i = 0; i < regexTest.Value.Length; ++i)
             {
                char_captured[i] = -1; 
             }
            var matches = regex.Matches(regexTest.Value);
            foreach (Match m in matches)
                if (m.Success)
                {
                    for (int g = 0; g < GroupMatches.Count; ++g )
                    {
                        if (m.Groups[g].Success)
                        for (int i = 0; i < m.Groups[g].Length; ++i)
                        {
                            char_captured[m.Groups[g].Index + i] = g;
                        }
                    }
                }

            if (char_captured[0] >= 0) HTMLMatch += "<span class='" + pen_colors[char_captured[0]] + "-highlight'>";
            HTMLMatch += WebUtility.HtmlEncode("" + regexTest.Value[0]);
            for (int i = 1; i < regexTest.Value.Length; ++i)
            {
                if (char_captured[i - 1] >= 0 && char_captured[i] != char_captured[i - 1]) HTMLMatch += "</span>";
                if (char_captured[i - 1] != char_captured[i] && char_captured[i] >= 0) HTMLMatch += "<span class='" + pen_colors[char_captured[i]] + "-highlight'>";
                HTMLMatch += WebUtility.HtmlEncode("" + regexTest.Value[i]);
            }
            if (char_captured[regexTest.Value.Length - 1] >= 0) HTMLMatch += "</span>";
            return HTMLMatch;
        }

        //Simple Matching
        public string CreateHTMLMatches(Regex regex, RegexUseCase regexTest,string MarkerColor)
		{
			string HTMLMatch = "";
            bool[] char_captured = new bool[regexTest.Value.Length];
            var matches = regex.Matches(regexTest.Value);
            foreach (Match m in matches)
            if (m.Success)
            {
                for (int i = 0; i < m.Length;++i )
                {
                    char_captured[m.Index + i] = true;
                }
            }

            if (char_captured[0]) HTMLMatch += "<span class='"+MarkerColor+"-highlight'>";
            HTMLMatch += WebUtility.HtmlEncode("" + regexTest.Value[0]);
            for (int i = 1; i < regexTest.Value.Length;++i )
            {
                if ( char_captured[i - 1] && !char_captured[i]) HTMLMatch += "</span>";
                if (!char_captured[i - 1] && char_captured[i]) HTMLMatch += "<span class='" + MarkerColor + "-highlight'>";
                HTMLMatch += WebUtility.HtmlEncode("" + regexTest.Value[i]);
            }
            if (char_captured[regexTest.Value.Length - 1]) HTMLMatch += "</span>";
			return HTMLMatch;
		}
		
        public bool VerifyMatches(string ReportName,string Title_Report,string RefPattern, string UserPattern, List<RegexUseCase> regexcases,string hints)
        {
            return VerifyMatches(ReportName, Title_Report, RefPattern, UserPattern, regexcases, hints, new List<string>());
        }

        public bool VerifyMatches(string ReportName,string Title_Report,string RefPattern, string UserPattern, List<RegexUseCase> regexcases,string hints,List<string> GroupMatches)
        {
			bool UnitTestOK = true;			
			string path = Path.Combine(ReportPath,ReportName);
            int countCorrect = 0;
			int percentage = 0;
			

            Regex Refregex = new Regex(RefPattern);
			Regex Userregex = new Regex(RefPattern);
            bool UserInvalidPattern = false;
            try
            {
                Userregex = new Regex(UserPattern);
            }
            catch (ArgumentException ex) 
            {
                UserInvalidPattern = true;
                Console.WriteLine("CG> message -c err INCORRECT REGEX: '"+ex.Message+"'");
            }
			string contents = File.ReadAllText(ReportTemplate);

            contents = contents.Replace("%REPORT_NAME%", WebUtility.HtmlEncode(Title_Report)); //Set Title
            contents = contents.Replace("%hints%", WebUtility.HtmlEncode(hints)); 
			
			string rowreport = "";
			foreach (var regexTest in regexcases)
			{
			  string User_char_captured ;
              string Ref_char_captured;
                
                if (GroupMatches.Count > 0)
                     Ref_char_captured = CreateHTMLMatches(Refregex, regexTest, GroupMatches);
                else Ref_char_captured = CreateHTMLMatches(Refregex, regexTest, "green");

                if (UserInvalidPattern)
                    User_char_captured = "Error, Invalid Regex Pattern";
                else
                {
                    if (GroupMatches.Count > 0)
                         User_char_captured = CreateHTMLMatches(Userregex, regexTest, GroupMatches);
                    else User_char_captured = CreateHTMLMatches(Userregex, regexTest, "yellow");
                }
                bool isCorrect = (GroupMatches.Count > 0 ? Ref_char_captured == User_char_captured : (Ref_char_captured == User_char_captured.Replace("class='yellow-highlight", "class='green-highlight")));
              if (isCorrect) ++countCorrect;
			  UnitTestOK = UnitTestOK && isCorrect;
              rowreport += RowReport.Replace("%name%", WebUtility.HtmlEncode(regexTest.Name)).Replace("%ok1%", (isCorrect ? "ok" : "remove")).Replace("%ok2%", (isCorrect ? "success" : "danger"))
			                        .Replace("%match1%",User_char_captured).Replace("%match2%",Ref_char_captured)+"\r\n";
			}
            percentage = 100 * countCorrect / regexcases.Count;
            if (countCorrect == regexcases.Count)
                contents = contents.Replace("%globalresult%", "success");  //100% Score Green Button
            else if (percentage < 16)
                contents = contents.Replace("%globalresult%", "danger");   //Low Score <16%, send a Red Button
            else contents = contents.Replace("%globalresult%", "warning"); //Something in between, yellow button
            contents = contents.Replace("%percent%", "" + percentage); //Set percentage
			contents = contents.Replace("%report_body%",rowreport);
			File.WriteAllText (path, contents);
            Console.WriteLine("CG> message Report is:" + path + " Size:" + new System.IO.FileInfo(path).Length);
            Console.WriteLine("CG> message -channel \"exercise results\" Solved: " + countCorrect + "/" + regexcases.Count + " (" + percentage + "%)");
            Console.WriteLine("CG> open --static-dir "+ReportPath+" /" + ReportName);
			return UnitTestOK;
        }


        [TestMethod]
        public void VerifyExercise1()
        {
            string hints = "Vowels are: a,e,i,o,u and A,E,I,O,U. Create a simple character set that contains only these characters.";
            string RefPattern = @"[aeiouAEIOU]";
			string UserPattern = Exercise1.Pattern_MatchVowels;
			List<RegexUseCase> regexcases = new List<RegexUseCase>();
			regexcases.Add( new RegexUseCase("Simple a","a"));
			regexcases.Add( new RegexUseCase("Simple e","e"));
			regexcases.Add( new RegexUseCase("Simple i","i"));
			regexcases.Add( new RegexUseCase("Simple o","o"));
			regexcases.Add( new RegexUseCase("Simple u","u"));
			regexcases.Add( new RegexUseCase("A with z","ZzzzzzzAzzzzzzZ"));
			regexcases.Add( new RegexUseCase("E with numbers","23124415235E5"));
			regexcases.Add( new RegexUseCase("I with numbers","452345I31234"));
			regexcases.Add( new RegexUseCase("O with zeros","0000O00O0OO0OO0"));
			regexcases.Add( new RegexUseCase("U with symbols","$)(=U:;_+-*/"));
			regexcases.Add( new RegexUseCase("Numbers","01235467689"));
			regexcases.Add( new RegexUseCase("Consonants","bCdFgHjKlMnPqRsTvWxYz"));
			regexcases.Add( new RegexUseCase("Alphabet","AbCdEfGhIjKlMnOpQrStUvWxYz"));
			regexcases.Add( new RegexUseCase("Lorem Ipsum","Lorem Ipsum dolor sit amet, consectetur adipiscing elit."));
			Assert.IsTrue(VerifyMatches("exercise1.html","Exercise 1 - Match vowels",RefPattern,UserPattern,regexcases,hints));
        }


        [TestMethod]
        public void VerifyExercise2()
        {
            string hints = @"Years have 4 digits: First will be always 2, second will be either 0 or 1, and the last two can be 0-9. Remember to add \b at start and end to define the word boundaries";
            string RefPattern = @"\b2[0-1][0-9][0-9]\b";
            string UserPattern = Exercise2.Pattern_Exercise2;
            List<RegexUseCase> regexcases = new List<RegexUseCase>();
            regexcases.Add(new RegexUseCase("Good years", "2000 2001 2020 2035 2076 2089 2110 2139 2149 2171 2189 2199"));
            regexcases.Add(new RegexUseCase("Bad years", "1999 20000 02000 2200 2209 2390 1885 20999"));
            regexcases.Add(new RegexUseCase("Bad years 2", "n2000 f2001 20r20 203E5 a2076 92089 k2110 21309+ a2149 21d71 n2189 2199x"));
            regexcases.Add(new RegexUseCase("Numbers", "01235467689"));
            regexcases.Add(new RegexUseCase("IT Crowd 😁", "118 999 881 999 119 725...3"));
            regexcases.Add(new RegexUseCase("Lorem Ipsum", "Lorem Ipsum dolor sit amet 2122,1983 consectetur adipiscing elit."));
            Assert.IsTrue(VerifyMatches("exercise2.html", "Exercise 2 - Searching years", RefPattern, UserPattern, regexcases, hints));
        }


        [TestMethod]
        public void VerifyExercise3()
        {
            string hints = @"The exercise asked for consonants, that can be matched with [a-zA-Z-[aeiouAEIOU]]. After that you need to create a character set for lowercase vowels. Finally you must create a character set with [ns]. Joining the three character sets will create the solution.";
            string RefPattern = @"[a-zA-Z-[aeiouAEIOU]][aeiou][ns]";
            string UserPattern = Exercise3.Pattern_Exercise3;
            List<RegexUseCase> regexcases = new List<RegexUseCase>();
            regexcases.Add(new RegexUseCase("Simple set", "This is an example for capturing regex matches"));
            regexcases.Add(new RegexUseCase("Matches", "Mask mask can send. Test then senseless foxes troubles"));
            regexcases.Add(new RegexUseCase("Matches II", "Ban ben Cis cos Dun das Fen fin Gos gun Has hen Jin jos Kun kas Lun les Mis man Nun nas Pos pun Qes qus Run res Sun sas Ten tan Von van Was wen Xin xan Zen zas"));
            regexcases.Add(new RegexUseCase("No Matches", "MAsk MaSk sEnd TEST"));
            regexcases.Add(new RegexUseCase("No Matches II", "BAn bEn CIs coS DuN dAS FeN fIN GOs gUn HaS heN JiN jOS KUn kAs LuN lEs MIs mAn NUN nAS POS pUn QEs qUs RUn rES SuN sAS TEn taN VoN vAN WAS wEN XIn xAN ZeN zaS"));
            regexcases.Add(new RegexUseCase("Lorem Ipsum", "Lorem Ipsum dolor sit amet , consectetur adipiscing elit."));
            Assert.IsTrue(VerifyMatches("exercise3.html", "Exercise 3 - Complex sets", RefPattern, UserPattern, regexcases, hints));
        }

        [TestMethod]
        public void VerifyExercise4()
        {
            string hints = @"As <text> and </text> are very similar use ? to make the / optional: <\/?text>. Then text part is defined as a character set with a lazy repetition of 1 or more: [a-zA-Z0-9=\s""""\-_:]";
            string RefPattern = @"<\/?[a-zA-Z0-9=\s""\-_:]+?>";
            string UserPattern = Exercise4.Pattern_Exercise4;
            List<RegexUseCase> regexcases = new List<RegexUseCase>();
            regexcases.Add(new RegexUseCase("Opening Tag", "<tag>"));
            regexcases.Add(new RegexUseCase("Closing Tag","</tag>"));
            regexcases.Add(new RegexUseCase("No Tag","</tag"));
            regexcases.Add(new RegexUseCase("Two Tags", "   <tag></tag>   "));
            regexcases.Add(new RegexUseCase("Two Tags and Text", "<check-check>Text unmatched</check-check>"));

            string xml1 = 
@"<?xml version=""1.0""?>
<catalog>
   <book id=""Book1"">
      <author>Doe, John</author>
      <title>Regular Expressions 101</title>
      <genre>Computer</genre>
      <price>88.22</price>
      <publish_date>2017-01-01</publish_date>
      <description>Regex in all its sexyness.</description>
   </book>
   <book id=""Book2"">
      <author>Knight, Michael</author>
      <title>Knight Rider</title>
      <genre>Fantasy</genre>
      <price>19.82</price>
      <publish_date>1982-08-08</publish_date>
      <description>KITT, help me!</description>
   </book>
</catalog>";

string xml2 =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
 <cookbook>
     <recipe xml:id=""Pommodoro_Mezzo"">
         <title>Pommodoro Mezzo e Mezzo</title>
         <ingredient quantity=""2""
                     unit=""piece"">Tomatoes</ingredient>
         <ingredient quantity=""2""
                     unit=""gr"">Salt </ingredient>
         <time quantity=""1"" unit=""minute""> </time>
         <method>
             <step>1. Pick a knife.</step>
             <step>2. Cut the tomato exactly on half.</step>
             <step>3. Add salt.</step>
             <step>4. Serve.</step>
             <step>5. Eat.</step>
         </method>
     </recipe>
 </cookbook>";
            regexcases.Add(new RegexUseCase("XML File 1", xml2));
            regexcases.Add(new RegexUseCase("XML File 2", xml1));
            Assert.IsTrue(VerifyMatches("exercise4.html", "Exercise 4 - XML Tags", RefPattern, UserPattern, regexcases, hints));
        }


        [TestMethod]
        public void VerifyExercise5()
        {
            string hints = @"<filename> part is a character set that can be repeated 1 or more times, followed by a literal dot.<Extension> part can be created as an alternation of each possible extension, just take care that each character on them must be both lowercase and uppercase [pP][nN][gG] for example.";
            string RefPattern = @"[a-zA-Z0-9\.\-+_=\(\)]+\.([jJ][pP][eE]?[gG]|[pP][nN][gG]|[bB][mM][pP]|[gG][iI][fF])";
            string UserPattern = Exercise5.Pattern_Exercise5;
            List<RegexUseCase> regexcases = new List<RegexUseCase>();
            regexcases.Add(new RegexUseCase("Simple image", "image.jpg"));
            regexcases.Add(new RegexUseCase("Several images", "image1.jpg image2.jpeg image3.png image4.bmp image5.gif"));
            regexcases.Add(new RegexUseCase("Double extension", "image.jpg.gif"));
            regexcases.Add(new RegexUseCase("Invalid images", "image,jpg myphotos.gpeg imagejpg .gif"));
            regexcases.Add(new RegexUseCase("Images with symbols", "image1.dot.jpg image2...+..+...gif image3.d-_-b.___.bmp image(=copy=).gif"));
            regexcases.Add(new RegexUseCase("Mixed Uppercase", "image1.DOt.Jpg iMAge2...+..+...GIF IMAGE3.d-_-b.___.Bmp IMAGE(=copy=).gIF"));
            regexcases.Add(new RegexUseCase("Image with path", @"C:\Users\Moss\Documents\Images\My_image.copy.JPG"));
            Assert.IsTrue(VerifyMatches("exercise5.html", "Exercise 5 - Image files", RefPattern, UserPattern, regexcases, hints));
        }

        [TestMethod]
        public void VerifyExercise6()
        {
            string hints = @"
//That's a hard one:
 public static string DrivePattern = @""(?<Drive>\b[a-zA-Z])"";
 public static string DirPattern = @""[a-zA-Z0-9\-+_=\(\)]+"";
 public static string DirsPattern = @""(?<Path>(?:"" + DirPattern + @""\\)*)"";
 public static string TextPattern = @""[a-zA-Z0-9\.\-+_=\(\)]+"";
 public static string FilePattern = @""(?<Name>"" + TextPattern + @""\.(?:[jJ][pP][eE]?[gG]|[pP][nN][gG]|[bB][mM][pP]|[gG][iI][fF])\b)"";
 public static string Pattern_Exercise6= DrivePattern + @"":\\"" + DirsPattern + FilePattern;
";
            string DrivePattern = @"(?<Drive>\b[a-zA-Z])";

            string DirPattern = @"[a-zA-Z0-9\-+_=\(\)]+";
            string DirsPattern = @"(?<Path>(?:" + DirPattern + @"\\)*)";

            string TextPattern = @"[a-zA-Z0-9\.\-+_=\(\)]+";
            string FilePattern = @"(?<Name>" + TextPattern + @"\.(?:[jJ][pP][eE]?[gG]|[pP][nN][gG]|[bB][mM][pP]|[gG][iI][fF])\b)";

            string RefPattern = DrivePattern+@":\\"+DirsPattern+FilePattern;

            string UserPattern = Exercise6.Pattern_Exercise6;

            List<string> GroupMatches = new List<string>() { "Drive","Path","Name"};

            List<RegexUseCase> regexcases = new List<RegexUseCase>();
            regexcases.Add(new RegexUseCase("Simple image", @"D:\image.jpg"));
            regexcases.Add(new RegexUseCase("Several images", @"E:\image1.jpg F:\image2.Jpeg Z:\image3.PnG x:\image3.d-_-b.___.bmp V:\image(=copy=).gif"));
            regexcases.Add(new RegexUseCase("Double extension", @"E:\image1.Exe.jpg F:\image2.XLS.Jpeg Z:\image3.TXT.PnG x:\image3.d-_-b.___.txt.bmp V:\image(=copy=).gif.jpg"));
            regexcases.Add(new RegexUseCase("Invalid images", @"D:\image,jpg c:\myphotos.gpeg imagejpg .gif d:\ f:\d\d\d.txt f:\a\b\.jpg dd:\nodoubledriveletter.jpg"));
            regexcases.Add(new RegexUseCase("Long path", @"C:\Users\Moss\Documents\Images\My_image.copy.JPG"));
            regexcases.Add(new RegexUseCase("Invalid path 1", @"C:\\Moss\Documents\Images\My_image.copy.JPG"));
            regexcases.Add(new RegexUseCase("Invalid path 2", @"C:My_image.copy.JPG"));
            regexcases.Add(new RegexUseCase("Invalid path 3", @"C:\..\dir\My_image.copy.JPG C:\?\$\My_image.copy.JPG"));
            regexcases.Add(new RegexUseCase("Multiple files", @"C:\Users\Moss\Documents\Images\My_image.copy.JPG C:\01\02\03\04\05+06\Picture.copy.JpG X:\++\--\__\==\==+-+-.jpg"));
            Assert.IsTrue(VerifyMatches("exercise6.html", "Exercise 6 - Image files with Path", RefPattern, UserPattern, regexcases, hints, GroupMatches));
        }

        [TestMethod]
        public void VerifyExample1()
        {
            string hints = "On this button you'll get hints for solving each exercise.";
            string RefPattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}\b";
            string UserPattern = Example1.Pattern_Email;
            List<RegexUseCase> regexcases = new List<RegexUseCase>();
            regexcases.Add(new RegexUseCase("Valid Email 1", "email@example.com"));
            regexcases.Add(new RegexUseCase("Valid Email 2", "name.surname@evil-big-corp.co.uk"));
            regexcases.Add(new RegexUseCase("Valid Email in Sentence", "Hi, this is an email: checkthisout@awsomeness-day.com"));
            regexcases.Add(new RegexUseCase("Valid Email Weird", "This a weird valid email: this__.__%++a.a@a.-.-.-.-.com.-.-.-.-.what.-.es believe it or not."));
            regexcases.Add(new RegexUseCase("Lorem Ipsum with email", "Lorem Ipsum dolor sit amet, socrates@old.-.classics.gr consectetur adipiscing elit."));
            regexcases.Add(new RegexUseCase("Invalid Email 1", "email@example_invalid.com"));
            regexcases.Add(new RegexUseCase("Invalid Email 2", "email@@example.com"));
            regexcases.Add(new RegexUseCase("Invalid Email 3", "email@example.comunications"));
            regexcases.Add(new RegexUseCase("Invalid Email 4", "This@is not an email address at all"));
            regexcases.Add(new RegexUseCase("Invalid Email 5", "452345I31234"));
            regexcases.Add(new RegexUseCase("Lorem Ipsum", "Lorem Ipsum dolor sit amet, consectetur adipiscing elit."));
            Assert.IsTrue(VerifyMatches("example1.html", "Example 1 - Email pattern", RefPattern, UserPattern, regexcases,hints));
        }

        [TestMethod]
        public void VerifyExample2()
        {
            string hints = "On this button you'll get hints for solving each exercise.";
            string RefPattern = @"ain";
            string UserPattern = Example2.Pattern_Example2;
            List<RegexUseCase> regexcases = new List<RegexUseCase>();
            regexcases.Add(new RegexUseCase("Pain Gain", "No pain, no gain"));
            regexcases.Add(new RegexUseCase("Many ain's", "Pain gain main saint maintenance"));
            regexcases.Add(new RegexUseCase("Uppercased", "Won't match: NO PAIN, NO GAIN"));
            regexcases.Add(new RegexUseCase("Random Sentence 1", "Again he is talking about Genetic Algorithm in CSB!"));
            regexcases.Add(new RegexUseCase("Random Sentence 2", "Classic Denzel"));
            regexcases.Add(new RegexUseCase("Lorem Ipsum", "Lorem Ipsum dolor sit amet, consectetur adipiscing elit."));
            Assert.IsTrue(VerifyMatches("example2.html", "Example 2 - ain pattern", RefPattern, UserPattern, regexcases, hints));
        }


    }
}
