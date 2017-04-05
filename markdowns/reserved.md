# Reserved Characters

### Literal Characters: Letters, digits and unicode
All letters and digits on a regex pattern are literal characters, so the regex engine will search exactly that pattern without any other processing.

So if you search `at`, your pattern will match on strings like `at`,`cat`, `bat`, `fat`,`you need to be at home at 10`.

*Remember that regex patterns are case sensitive, although there are ways to set it to case insensitive (it depends on the language used).*

### Reserved Character List

Almost any other character (both ASCII and Unicode) will be treated as literals too.

**But** there is a list of reserved, special, characters.
These are the most important (we will see all those on more detail on next lessons):
-`.`: Any single character.
-`|`: Or expression (one or another).
-`[` `]`: Character set delimitators.
-`(` `)`: Grouping delimitators.
-`{` `}`: Repetitions delimitators.
-`*`: Zero or more repetitions of the previous character.
-`+`: One or more repetitions.
-`?`: Zero or one repetition. Also it's used for lazy matches.
-`^`: Start of the string. You can use it to force a pattern to match only at start. Also it's used as negative inside character sets.
-`$`: End of the string.
-`\`: Escape character of all reserved characters, so `\?` will search the literal `?`. Also it's used for other special search patterns (see below).

### Other Special blocks
Talk about \b \B and others