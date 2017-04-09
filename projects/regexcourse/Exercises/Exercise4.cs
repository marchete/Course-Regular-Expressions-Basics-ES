namespace RegexCourse{
    public static class Exercise4{
		//Write a regex pattern to match simplified XML tags.
        //Simplified XML tags will be defined as <text> or </text>
		//text has size >=1, and can contain these characters:
		//  -Any letter
		//  -Any digit
		//  -These symbols: = \s " - _ :
		
		//Your regex pattern should not match the characters between XML tags.
		//I.e: In the XML <text>ZZZZZ</text>, the text ZZZZZ shouldn't be matched.
		//Tags starting with <? should NOT match, as ? is not in the set of allowed symbols
		//Note: In literal C# strings, the " symbol must be escaped as double ""
		// Also, in C# you need to escape / with \/
		
        public static string Pattern_Exercise4=@"";
    }
}
