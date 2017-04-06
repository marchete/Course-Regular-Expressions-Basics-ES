# Groups & Capturing Groups

Groups use the `(` `)` symbols (like alternations, but the `|` symbol is not needed).
They are useful for creating blocks of patterns, so you can apply repetitions or other modifiers to them as a whole.
In the pattern `([a-x]{3}[0-9])+` The `+` metacharacter is applied to the whole group.

Also another main use of groups is for processing parts of the match, like extracting data or replacing it.

### `(` `)` Unnamed Groups
With `pattern1(pattern2)pattern3` you'll capture the results of pattern2 for later use, but not the part that `pattern1` or `pattern3` matched.
This is useful when you want only a part of the search. Imagine that you are reading some text files, created as forms. They can have data like that:
```
Name:"John" Surname:"Doe" Email:"john@example.com"
```
As you imagine, if you need to process that file you don't need the Name:" part, you just need the data.
So you can use a pattern like that: `Name:"([\w]+?)"` to capture just the useful data, but using the `Name:"` as a reference for searching the data.

>**Note:** If you apply a repetition to a group, only the last match of the repetition is stored. `([\w])+?` will only give you the last matched characted, whereas `([\w]+?)` group have the repetition inside, so it will give you all matched characters.

### `(?:` `)` Non capturing Groups
Use `(?:` `)` for non capturing groups. That is, if you need to use groups as a block but you won't process it later, then make it non-capturing

### Named Groups
Use `(?<groupname>` `)` to capture a group with name `groupname`. This is useful for later processing, when input data can have different ordering.

```
Name:"John" Surname:"Doe" Email:"john@example.com"
```
The following regex pattern: `Name:"(?<Name>[\w]+?)".*?Surname:"(?<Surname>[\w]+?)".*?Email:"(?<Email>\b[\w.%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}\b)"`
Will match each data and will create three Name Groups: Group 'Name' with data `John`, Group 'Surname' with data `Doe` and Group 'Email' with data `john@example.com

Each language and regex engine define how to access all matched groups.

>**Note:** There are many others types of grouping, for lookahead, lookbehind, atomic groups, conditionals, recursion,...
All them are out of scope of this course.

Click on **`NEXT LESSON`** to go to the final lesson.