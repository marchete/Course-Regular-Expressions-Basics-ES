namespace RegexCourse{
    public static class Example1{
		//Email validator pattern
        public static string Pattern_Email=@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}\b";
		//Please note that there are other regex patterns more accurate to RFC 822, but more complex too
    }
}