using System.Text.RegularExpressions;

namespace RegexCourse
{
    public class Exercise1
    {
        /**
         *  This function should match any vowel, both lowercase and uppercase
         **/
        public static Match MatchVowels(string text)
        {
            string Pattern = @""; //Set the regex pattern to match any vowel, both lowercase and uppercase
            Regex regex = new Regex(Pattern);
            return regex.Match(text);
        }
    }
}