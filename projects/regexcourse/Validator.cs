using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.IO;
using System;

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
            HTMLMatch += regexTest.Value[0];
            for (int i = 1; i < regexTest.Value.Length;++i )
            {
                if ( char_captured[i - 1] && !char_captured[i]) HTMLMatch += "</span>";
                if (!char_captured[i - 1] && char_captured[i]) HTMLMatch += "<span class='" + MarkerColor + "-highlight'>";
                HTMLMatch += regexTest.Value[i];
            }
            if (char_captured[regexTest.Value.Length - 1]) HTMLMatch += "</span>";
			return HTMLMatch;
		}
		
        public bool VerifyMatches(string ReportName,string Title_Report,string RefPattern, string UserPattern, List<RegexUseCase> regexcases,string hints)
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

			contents = contents.Replace("%REPORT_NAME%",Title_Report); //Set Title
            contents = contents.Replace("%hints%", hints); 
			
			string rowreport = "";
			foreach (var regexTest in regexcases)
			{
			  string User_char_captured ;
              string Ref_char_captured = CreateHTMLMatches(Refregex, regexTest, "green");
              if (UserInvalidPattern)
                   User_char_captured = "Error, Invalid Regex Pattern";
              else User_char_captured = CreateHTMLMatches(Userregex, regexTest, "yellow");
              bool isCorrect = (Ref_char_captured == User_char_captured.Replace("class='yellow-highlight","class='green-highlight"));
              if (isCorrect) ++countCorrect;
			  UnitTestOK = UnitTestOK && isCorrect;
			  rowreport += RowReport.Replace("%name%",regexTest.Name).Replace("%ok1%",(isCorrect?"ok":"remove")).Replace("%ok2%",(isCorrect?"success":"danger"))
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
            string hints = "The best option is to use the pattern [ain]";
            string RefPattern = @"[ain]";
            string UserPattern = Exercise1.Pattern_Exercise1;
            List<RegexUseCase> regexcases = new List<RegexUseCase>();
            regexcases.Add(new RegexUseCase("Pain Gain", "No pain, no gain"));
            regexcases.Add(new RegexUseCase("Many ain's", "Pain gain main saint maintenance"));
            regexcases.Add(new RegexUseCase("Uppercased", "Won't match: NO PAIN, NO GAIN"));
            regexcases.Add(new RegexUseCase("Random Sentence 1", "Again he is talking about Genetic Algorithm in CSB!"));
            regexcases.Add(new RegexUseCase("Random Sentence 2", "Classic Denzel"));
            regexcases.Add(new RegexUseCase("Lorem Ipsum", "Lorem Ipsum dolor sit amet, consectetur adipiscing elit."));
            Assert.IsTrue(VerifyMatches("exercise1.html", "Exercise 1 - [ain] pattern", RefPattern, UserPattern, regexcases, hints));
        }

        [TestMethod]
        public void VerifyExercise2()
        {
            string hints = "Vowels are: a,e,i,o,u and A,E,I,O,U. Create a simple character set that contains only these characters.";
            string RefPattern = @"[aeiouAEIOU]";
			string UserPattern = Exercise2.Pattern_MatchVowels;
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
			Assert.IsTrue(VerifyMatches("exercise2.html","Exercise 2 - Match vowels",RefPattern,UserPattern,regexcases,hints));
        }


        [TestMethod]
        public void VerifyExercise3()
        {
            string hints = @"Years have 4 digits: First will be always 2, second will be either 0 or 1, and the last two can be 0-9. Remember to add \b at start and end to define the word boundaries";
            string RefPattern = @"\b2[0-1][0-9][0-9]\b";
            string UserPattern = Exercise3.Pattern_Exercise3;
            List<RegexUseCase> regexcases = new List<RegexUseCase>();
            regexcases.Add(new RegexUseCase("Good years", "2000 2001 2020 2035 2076 2089 2110 2139 2149 2171 2189 2199"));
            regexcases.Add(new RegexUseCase("Bad years", "1999 20000 02000 2200 2209 2390 1885 20999"));
            regexcases.Add(new RegexUseCase("Bad years 2", "n2000 f2001 20r20 203E5 a2076 92089 k2110 21309+ a2149 21d71 n2189 2199x"));
            regexcases.Add(new RegexUseCase("Numbers", "01235467689"));
            regexcases.Add(new RegexUseCase("Lorem Ipsum", "Lorem Ipsum dolor sit amet 2122,1983 consectetur adipiscing elit."));
            Assert.IsTrue(VerifyMatches("exercise3.html", "Exercise 3 - Searching years", RefPattern, UserPattern, regexcases, hints));
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
