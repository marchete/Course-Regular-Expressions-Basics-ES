using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace RegexCourse
{
    [TestClass]
    public class Validator
    {
        public void VerifyMatches(Match ValidatorMatch, Match UserMatch)
        {
            Assert.AreEqual(UserMatch.Success, ValidatorMatch.Success);
            if (ValidatorMatch.Success)
            {
                //TODO: Check indexes and such
            }
        }

        [TestMethod]
        public void VerifyExercise1()
        {
            List<string> INPUTS = new List<string> { 
            "a","e","i","o","u","zzzzzzzAzzzzzzz","123124415235E546745674567","452345I31234","0000O00O0OO0OO0","$)(=U:;_>","1235467689","DDDDRTTTTTTPQRWT", "ABCDEFGHIJKLMNOPQRSTUVWXYXZ+abcdefeghijklmnopqrstuvwxyz", "El veloz murcielago. Abria el paso hacia el siguiente arbol."};
            string RefPattern = @"[aeiouAEIOU]";
			string UserPattern = Exercise1.Pattern_MatchVowels;
            Regex Refregex = new Regex(RefPattern);
			Regex Userregex = new Regex(UserPattern);
            foreach (string text in INPUTS)
            {
                VerifyMatches(Refregex.Match(text), Userregex.Match(text));
            }
        }
    }
}
