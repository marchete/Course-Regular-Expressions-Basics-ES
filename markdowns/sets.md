# Character Sets

### Simple character set
On previous lessons we learned that a regex made from literal characters like `ain` will search exactly those 3 letters in that exact order.
It's like to search a & i & n. But what if I need an | (OR) instead of & (AND)?

That's why you have the `[` `]`. When you create a pattern like `[ain]` you'll search for a single character that must be either `a` OR `i` OR `n`

Now let's check how regex matches are different when using the character sets
@[Example 2 - Searching `ain` pattern]({"stubs": ["Exercises/Example2.cs"],"command": "RegexCourse.Validator.VerifyExample2"})

>**Note:** On this first exercise you'll need to create the pattern, and then run the code
@[Exercise 1 - Searching 'ain' character set]({"stubs": ["Exercises/Exercise1.cs"],"command": "RegexCourse.Validator.VerifyExercise1"})


### Range character set
[A-Za-z]

>**Note:** Remember `\w` from the previous lesson? It's shorthand for `[a-zA-Z0-9_]`

@[Exercise 2 - Create a pattern to match vowels ]({"stubs": ["Exercises/Exercise2.cs"],"command": "RegexCourse.Validator.VerifyExercise2"})

@[Exercise 3 - Searching years 2000 to 2199 ]({"stubs": ["Exercises/Exercise3.cs"],"command": "RegexCourse.Validator.VerifyExercise3"})