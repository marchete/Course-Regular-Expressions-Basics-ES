# Groups & Capturing Groups

Groups use the `(` `)` symbols (like alternations, but the `|` symbol is not needed).
They are useful for creating blocks of patterns, so you can apply repetitions or other modifiers to them as a whole.
In the pattern `([a-x]{3}[0-9])+`, the `+` metacharacter is applied to the whole group.

Also, another main use of groups is for processing parts of a match, like extracting data or replacing it.

### `(` `)` Unnamed Groups
With `pattern1(pattern2)pattern3` you'll capture the results of pattern2 for later use, but not the parts matched by `pattern1` or `pattern3`.
This is useful when you want to extract only a portion of the search. Imagine that you are reading some text files that are formatted as forms. They could have data like this:
```
Name:"John" Surname:"Doe" Email:"john@example.com"
```
As you may imagine, if you need to process that file you don't need the Name:" part. You just need the data.
So you can use a pattern like this: `Name:"([\w]+?)"` to capture just the useful data, using the `Name:"` as a reference for locating the data within the text.

>**Note:** If you apply a repetition to a group, only the last match of the repetition is stored. `([\w])+?` will only give you the last matched character, whereas the `([\w]+?)` group has the repetition inside, so it will give you all matched characters.

### `(?:` `)` Non capturing Groups
Use `(?:` `)` for non capturing groups. That is, if you need to use a group as a block but you won't process it later, then make it non-capturing.

### Named Groups
Use `(?<groupname>` `)` to capture a group with name `groupname`. This is useful for later processing, when input data may be presented in a different order than desired.

```
Name:"John" Surname:"Doe" Email:"john@example.com"
```
The following regex pattern:
```regex
Name:"(?<Name>[\w]+?)".*?Surname:"(?<Surname>[\w]+?)".*?Email:"(?<Email>\b[\w.%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}\b)"
```
Will match each piece of data and will create three Name Groups: Group 'Name' with data `John`, Group 'Surname' with data `Doe` and Group 'Email' with data `john@example.com`.
Each language and regex engine define how to access matched groups. Check your language documentation to learn how to iterate and process matched groups.

@[Exercise 6 - Image Files with Path ]({"stubs": ["Exercises/Exercise6.cs"],"command": "RegexCourse.Validator.VerifyExercise6"})


>**Note:** There are many other types of grouping, for lookahead, lookbehind, atomic groups, conditionals, recursion, ...
All them are outside the scope of this course.
