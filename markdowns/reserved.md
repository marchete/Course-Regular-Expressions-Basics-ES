# Characters And Metacharacters

### Literal Characters: Letters, digits and unicode
All letters, digits and most unicode characters in a regex pattern are literal, so the regex engine will search for *exactly* that pattern, without any other processing.

So if you search for `at`, your pattern will match these strings: "c**at**", "b**at**", "You were l**at**e, you need to be **at** home **at** 10".

>**Remember:** Regex patterns are case sensitive, although there are ways to specify a case insensitive search (depending on the language used).

### Reserved Character List (Metacharacters)

Almost any other characters (both ASCII and Unicode) will be treated as literals too.

**But** there is a list of reserved, special characters called *metacharacters*.
These are the most important (we will see all of them in more detail in the subsequent lessons):

| Chars | Description |
| ------ | ------ |
| `.` | Any single character. |
| `(` `)` | Grouping delimitators. |
| `[` `]` | Character set delimiters. OR at character level |
| <code>&#124;</code> | OR expression for patterns (one or another). |
| `{` `}` | Repetitions delimiters. |
| `*` | Zero or more repetitions of the previous character. |
| `+` | One or more repetitions. |
| `?` | Zero or one repetition. Also it's used for lazy matches. |
| `^` | Start of the string. You can use it to force a pattern to match only at the start. Also it's used as a NOT inside of character sets. |
| `$` | End of the string. |
| `/` | Separator. In many regex engines, regex patterns must be enclosed in `/`s |
| `-` | Range definition. Used to define a range of consecutive characters, like A-Z |
| `\` | Escape character for all reserved characters, so \\? will search for a literal ?. It's also used for other special search patterns (see below).  |

>**Anchors:** `^` and `$` are also called anchors. Anchors match zero characters.

>**Note:** In other search engines, the `*` is a wildcard that matches everything. However in regex, the wildcard is created with the union of two metacharacters: `.*`, which means "any single character zero or more times". Similarly, in other search engines `?` is equivalent to `.` in regex (any single character, once).


### Other metacharacters

| Pattern | Description |
| ------ | ------ |
| `\w` | Any letter (a to z, both lowercase and uppercase), digit (0 to 9), or the underscore char `_`. |
| `\W` | Just the opposite of the previous metacharacter. |
| `\d` | Any digit, 0 to 9. |
| `\D` | Just the opposite of the previous metacharacter. |
| `\b` | Word boundaries. It's an anchor used to find the start or the end of a word (defined as any number of consecutive \w characters). `\bat` will match " **at**tack " and "**at**lantis" but not "bat" because the word doesn't start with `at`. |
| `\B` | Just the opposite of the previous metacharacter. |
| `\t` | Tabulation. |
| `\r` | Carriage return. |
| `\n` | New line. |
| `\s` | Means either: `space` or `\t` or `\r` or `\n`. That is, any kind of white space. |
| `\S` | Just the opposite of the previous metacharacter. |

>**Note:** There are many other patterns using `\`, but these are the most important.

### Backslash

Using backslash, outside of the previously defined metacharacters, will escape the following character. The regex engine will then consider this character as a simple literal. According to the table above, `*` is a quantifier, but if you need to use the character `*` in a string, `\*` means "the character `*`".

Furthermore, the `\` can escape itself if you need to match a backslash character. You can use `\\` to match backslash.

### Search flags and modifiers
Most regex engines have some flags to change the search behaviour.
Check your language to know how to apply them.

| Modifier | Description |
| --- | ------ |
| i | Case insensitive search |
| g | Global search. Some regex engines stop on the first match, the `g` flag forces the search to return all possible matches. |
| s | Single line, `.` will match `\n` so the whole text is considered as a single line.  |
| m | Multiline, `^` and `$` will match on each line, and not only at start and end of string.  |

In the next lesson, we will learn about Character Sets.
