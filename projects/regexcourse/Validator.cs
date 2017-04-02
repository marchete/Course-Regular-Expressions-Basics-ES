using Answer;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace CodinGame
{
    [TestClass]
    public class Validator
    {
	    public void VerifyMatches(Match ValidatorMatch,Match UserMatch)
		{
           Assert.AreEqual( UserMatch.Success, ValidatorMatch.Success);
   	       if (ValidatorMatch.Success)
			{
			 //TODO: Check indexes and such
			}
		}
        [TestMethod]
        public void VerifyExercise1() {
		    List<string> INPUTS = new List<string>{"ABCDEFGHIJKLMNOPQRSTUVWXYXZ+abcdefeghijklmnopqrstuvwxyz","El veloz murcielago. Abria el paso hacia el siguiente arbol.","DDDDRTTTTTTPQRWT"};
            string Pattern=@"[aeiouAEIOU]";
            Regex regex = new Regex(Pattern);
            foreach (string text in INPUTS){
			  VerifyMatches(regex.Match(text),Exercise1.MatchVowels(text));
			  }
        }
    }
}