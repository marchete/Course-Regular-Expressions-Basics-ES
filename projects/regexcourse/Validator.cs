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
            List<string> INPUTS = new List<string> { "ABCDEFGHIJKLMNOPQRSTUVWXYXZ+abcdefeghijklmnopqrstuvwxyz", "El veloz murcielago. Abria el paso hacia el siguiente arbol.", "DDDDRTTTTTTPQRWT" };
            string Pattern = @"[aeiouAEIOU]";
            Regex regex = new Regex(Pattern);
            foreach (string text in INPUTS)
            {
                VerifyMatches(regex.Match(text), Exercise1.MatchVowels(text));
            }
        }
    }
}
