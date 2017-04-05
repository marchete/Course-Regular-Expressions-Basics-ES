namespace RegexCourse{
    public static class Exercise4{
		//Write a regex pattern to match simplified XML tags
        //Simplified XML tags will be defined as <text> or </text>
		//text has size >=1, and can contain these characters:
		//  -Any letter
		//  -Any digit
		//  -These symbols: = \s " - _ :
		
		//Your regex pattern should not match the characters between XML tags.
		//I.e: On <text>ZZZZZ</text> XML, the text ZZZZZ shouldn't match.
		//Note: On literal C# strings, " symbol must be escaped as double ""
        public static string Pattern_Exercise4=@"";
    }
}