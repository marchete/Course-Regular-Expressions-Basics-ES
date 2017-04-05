# Character Sets

### Simple character set
On previous lessons we learned that a regex made from literal characters like `ain` will search exactly those 3 letters in that exact order.
It's like to search a & i & n. But what if I need an | (OR) instead of & (AND)?

That's why you have the `[` `]`. When you create a pattern like `[ain]` you'll search for a single character that must be either `a` OR `i` OR `n`

Now let's check how regex matches are different when using the character sets:
@[Example 2 - Searching `ain` pattern]({"layout": "aside","stubs": ["Exercises/Example2.cs"],"command": "RegexCourse.Validator.VerifyExample2"})

On this first exercise you'll need to create the pattern, and then run the code. Modify the string in the **"Exercise1.cs"** tab, and add your regex pattern.
@[Exercise 1 - Searching a character set]({"layout": "aside","stubs": ["Exercises/Exercise1.cs"],"command": "RegexCourse.Validator.VerifyExercise1"})
As you see, a character set with `[` `]` matches any single character declared inside.

### Range character set
A simple character set can be bothersome to declare when you need to match the whole alphabet, or digits.

For that reason on Regular Expressions you can use `-` to declare ranges of consecutive characters.

Using the pattern `[a-z]` you'll match any character from a to z (a,b,c,d,e....x,y or z), `[2-5]` will match any number from 2 to 5.

You can also combine several ranges inside the character set, `[B-Ga-v]` is valid.

As stated before, regex patterns are case sensitive, `[a-z]` and `[A-Z]` matches differently.

There is a special case with the `^` metacharacter, that create a negative match. `[^2-5]` will match with any character except 2,3,4 and 5. But take care because that doesn't mean it just matches with 0,1,6,7,8 or 9, it matches with any other character, even letters and symbols

Some regex engines (check your language first) support character set substractions and intersections.
- Substractions are usually defined as `[range-[subrange_to_remove]]`, like `[0-9-[2-7]]` to have a set that matches only 0,1,8 or 9.
- Intersections are defined as `[range1&&range2]`. The character must belong to both ranges to be used on the search.

>**Note:** Remember `\w` from the previous lesson? It's shorthand for `[a-zA-Z0-9_]`

@[Exercise 2 - Create a pattern to match vowels ]({"layout": "aside","stubs": ["Exercises/Exercise2.cs"],"command": "RegexCourse.Validator.VerifyExercise2"})

@[Exercise 3 - Searching years from 2000 to 2199 ]({"layout": "aside","stubs": ["Exercises/Exercise3.cs"],"command": "RegexCourse.Validator.VerifyExercise3"})
Regex is especially bad when searching numeric ranges. There is no easy way to create a regex pattern for arbitrary ranges (i.e. numbers from 733 to 8586)