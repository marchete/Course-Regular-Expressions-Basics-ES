# Character Sets

### Simple character set
In previous lessons we learned that a regex made from literal characters, like `ain`, will search exactly those 3 letters in that exact order.
It's essentially a search for **a & i & n**. But what if I need an | (OR) instead of an & (AND)?

That's why you have the `[` `]`. When you create a pattern like `[ain]`, you'll search for a single character that must be either `a` OR `i` OR `n`.

In this first exercise, you'll need to create a pattern to match vowels, and then run the code.
@[Exercise 1 - Create a pattern to match vowels ]({"stubs": ["Exercises/Exercise1.cs"],"command": "RegexCourse.Validator.VerifyExercise1"})

### Range character set
A simple character set can be bothersome to declare when you need to match the whole alphabet, or all digits.
For that reason, you can use `-` in Regular Expressions to declare ranges of consecutive characters.
Using the pattern `[a-z]`, you'll match any character from a to z (a,b,c,d,e....x,y or z), `[2-5]` will match any number from 2 to 5.
You can also combine several ranges inside the character set. `[B-Ga-v]` is valid.
As stated before, regex patterns are case sensitive, `[a-z]` and `[A-Z]` match differently.

The `^` metacharacter is a special case. When used inside of `[` `]`, that character creates a negative match. `[^2-5]` will match with any character **except** 2,3,4 and 5. But take care because that doesn't mean it just matches with 0,1,6,7,8 or 9. It matches with **any** other character, even letters and symbols.

Some regex engines (check your language first) support character set substractions and intersections.
- Substractions are usually defined as `[range-[subrange_to_remove]]`, like `[0-9-[2-7]]` indicating a set that matches only 0,1,8 or 9.
- Intersections are defined as `[range1&&range2]`. The character must belong to both ranges to be matched in the search.

>**Note:** Remember `\w` from the previous lesson? It's shorthand for `[a-zA-Z0-9_]`

@[Exercise 2 - Searching years from 2000 to 2199 ]({"stubs": ["Exercises/Exercise2.cs"],"command": "RegexCourse.Validator.VerifyExercise2"})
Regex is especially bad when searching numeric ranges. There is no easy way to create a regex pattern for arbitrary ranges (i.e. numbers from 733 to 8586)

@[Exercise 3 - Complex pattern sets ]({"stubs": ["Exercises/Exercise3.cs"],"command": "RegexCourse.Validator.VerifyExercise3"})

Continue to the next lesson to learn about Repetitions in Regular Expressions.
