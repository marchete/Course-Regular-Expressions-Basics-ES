namespace RegexCourse{
    public static class Exercise6{
		//Match all images with full paths, and create three named groups for capturing the following:
		//Named Group 'Drive': <Drive> Letter only
		//Named Group 'Path':  <directories>
		//Named Group 'Name':  <filename>.<extension>
		
		//Files are in NTFS, and are composed as follows:
		//<Drive>:\<directories><filename>.<extension>
		//<Drive> is a letter from a to z
        //<directories> is an optional section, formed by zero or more <directory>\ blocks
		// <directory> is formed as one or more of the following characters:
		//    -Any alphanumeric character
		//    -Any of these symbols: +-_=()
		// <filename> is formed as one or more of the following characters:
		//    -Any alphanumeric character
		//    -Any of these symbols: .+-_=()
		//Valid <extension> for images are:
		// jpg,jpeg,png,bmp,gif
		
		
		public static string Pattern_Exercise6=@"";
		/*
   		    //Another option, as combination of subpatterns
		    public static string DrivePattern = @""; //(?<Drive>...

            public static string DirPattern = @""; //To match text of each directory
            public static string DirsPattern = @"" + DirPattern + @""; //(?<Path>...

            public static string TextPattern = @""; //To match <filename>, similar to Exercise5
            public static string FilePattern = @"" + TextPattern + @""; //(?<Name>...

            public static string Pattern_Exercise6 = DrivePattern+@":\\"+DirsPattern+FilePattern;
		*/
    }
}