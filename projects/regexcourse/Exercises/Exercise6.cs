namespace RegexCourse{
    public static class Exercise6{
		//Match all images with full paths.
		//Files are in NTFS, and are composed as follows:
		//<Drive>:\<directories><filename>.<extension>
		//<Drive> is a letter from a to z
        //<directories> is an optional section, formed by zero or more <directory>\
		// <directory> is formed as one or more of the following characters:
		//    -Any alphanumeric character
		//    -Any of these symbols: .+-_=()
		// <filename> is formed as one or more of the following characters:
		//    -Any alphanumeric character
		//    -Any of these symbols: .+-_=()
		//Valid <extension> for images are:
		// jpg,jpeg,png,bmp,gif
		
		//NTFS filesystem is case insensitive.
        public static string Pattern_Exercise6=@"";
    }
}