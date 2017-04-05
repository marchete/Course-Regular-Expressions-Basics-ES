# Characters And Metacharacters

### Literal Characters: Letters, digits and unicode
All letters, digits and unicode characters on a regex pattern are literal, so the regex engine will search exactly that pattern without any other processing.

So if you search `at`, your pattern will match on these strings: "**at**","c**at**", "b**at**", "f**at**","you need to be **at** home **at** 10".

>**Remember:** Regex patterns are case sensitive, although there are ways to set it to case insensitive (it depends on the language used).

### Reserved Character List (Metacharacters)

Almost any other character (both ASCII and Unicode) will be treated as literals too.

**But** there is a list of reserved, special, characters.
These are the most important (we will see all those on more detail on next lessons):

| Chars | Description |
| ------ | ------ |
| `.` | Any single character. |
| <code>&#124;</code> | Or expression (one or another). |
| `[` `]` | Character set delimitators. |
| `(` `)` | Grouping delimitators. |
| `{` `}` | Repetitions delimitators. |
| `*` | Zero or more repetitions of the previous character. |
| `+` | One or more repetitions. |
| `?` | Zero or one repetition. Also it's used for lazy matches. |
| `^` | Start of the string. You can use it to force a pattern to match only at start. Also it's used as negative inside character sets. |
| `$` | End of the string. |
| `\`  | Escape character of all reserved characters, so \\? will search the literal ?. It's also used for other special search patterns (see below).  |

They are called metacharacters.
>**Anchors:** `^` and `$` are also called anchors because they match zero characters.

>**Note:** In other searching engines, the `*` is the wildcard, it matches everything. But in regex the wildcard is created with the union of two metacharacters: `.*`, that means "any single character zero or more times". Similarly, on other searches `?` is equivalent to `.` in regex (any single character, once).


### Other metacharacters
The symbol `\` can precede any metacharacter to escape it, but it can be combined with some letters creating new metacharacters:

| Pattern | Description |
| ------ | ------ |
| `\w` | Any letter (a to z, both lowercase and uppercase), digit (0 to 9), or the underscore char `_`. |
| `\W` | Just the opposite to the previous metacharacter. |
| `\d` | Any digit, 0 to 9. |
| `\D` | Just the opposite to the previous metacharacter. |
| `\b` | Word boundaries. It's an anchor used to find the start or the end of a word (any number or \w characters). `\bat` will match " **at**tack " and "**at**lantis" but not "bat" because the word doesn't start with `at`. |
| `\B` | Just the opposite to the previous metacharacter. |
| `\t` | Tabulation. |
| `\r` | Return carriage. |
| `\n` | New line. |
| `\s` | Means either: `space` or `\t` or `\r` or `\n`. That is, any kind of separation. |
| `\S` | Just the opposite to the previous metacharacter. |

>**Note:** There are many other patterns with `\`, but these are the most important.