# Introduction
A regular expression (or the acronym "regex") is a pattern that allows complex text searches. 

Imagine you need to search email addresses on a given text. An email address has a complex structure: it has an account part, the @ sign and the domain name part, and each part has it's own restrictions. This is a hard task to do if you try to program it as usual, with loops, simple text searches and conditionals.

Instead of that, let's try to search email addresses with a regular expression. 
In the example below, there is a code with an email regex pattern. To test it, press **`RUN MY CODE`**

### Example - Searching E-mail addresses
@[Regex pattern to search E-mail addresses]({"stubs": ["Exercises/Example1.cs"],"command": "RegexCourse.Validator.VerifyExample1"})

If you click on the `View Validators` button you'll see what it's matching on each validator, highlighted with a color.
Use the `<span class="glyphicon glyphicon-thumbs-up"></span> Hints` button if you got stuck on some Exercise.

As you see, a regex pattern is a very powerful tool. It's used on a lot of different fields and programs.

On this course you'll learn the basics of regex, enough to understand a regex pattern, and creating yours.

### Always use literal strings!
I will use C# as the reference language for the course. Please note that string definition varies on other languages. It's important that you take into account how your language treats some characters inside strings. For example C# don't take the `\` character as literal on normal strings. This is problematic for regex, because then you'll need to escape `\`. 

For this reason I use string literals on C# (see https://msdn.microsoft.com/en-us/library/aa691090(v=vs.71).aspx ), they are defined with a `@` before the quote: 

```csharp
 string literal = @"This is a verbatim string literal, \ doesn't need to be escaped";
 string  normal =  "This is a normal string, \\ needs to be escaped";
```
If you use another language you must ensure that your regex pattern is literal. Otherwise you'll get unexpected results, or you'll need to escape non literal characters.

Click on **"NEXT LESSON"** to learn about reserved characters in Regular Expressions.