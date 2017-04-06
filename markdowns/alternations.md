# Alternations `(|)`

With `[` `]` you have the OR operator at character level, now with alternation you have the OR operator at pattern level:

`(pattern1|pattern2|pattern3)` will match either `pattern1` OR `pattern2` OR `pattern3`.

Those subpatterns can be as complex as needed, and there is no limit on the number of patterns separated by the `|` metacharacter.

Examples:

- Example 1: `(yes|no)` Will search exactly `yes` OR `no`.
- Example 2: `([Yy]es|[Nn]o){2,4}` Will search `Yes` OR `yes` OR `No` OR `no` repeated between two and four times.
- Example 3: `\b([0-9]+|[a-z]+)\b` Will search a word made entirely of digits OR a word made entirely of lowercase letters. But it won't match mixes of letters and digits.
- Example 4: `(([Yy]es|[Nn]o){2,4}|\b([0-9]+|[a-z]+)\b)` Combination of the two previous patterns. So it matches either the same as "Example 1" OR "Example 2".

@[Exercise 5 - Search Image Files ]({"stubs": ["Exercises/Exercise5.cs"],"command": "RegexCourse.Validator.VerifyExercise5"})

Continue to next lesson to learn about Groups and Capturing Groups.