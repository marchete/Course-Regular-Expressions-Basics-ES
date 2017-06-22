# Repetitions

Repetitions simplify using the same pattern several consecutive times. They also allow for flexible length searches, so you can match 'aaZ' and 'aaaaaaaaaaaaaaaaZ' with the same pattern.

### `{` `}` Ranges

You can use these syntaxes for defined ranges:

| Pattern | Description |
| ------ | ------ |
| `{n}` | Repeat the previous symbol exactly `n` times |
| `{n,}` | Repeat the previous symbol `n` or more times |
| `{min,max}` | Repeat the previous symbol between `min` and `max` times, both included |

So `a{6}` is the same as `aaaaaa`, and `[a-z]{1,3}` will match any text that has between 1 and 3 consecutive letters.
>**Note:** In repetitions, each symbol match is independent. If `[a-z]{1,3}` first matches with 'a', on the next letter it can match with anything in the [a-z] range, not only 'a'.

### Other Ranges

You can use these syntaxes for other types of ranges:

| Pattern | Description |
| ------ | ------ |
| `*` | Repeat the previous symbol `0` or more times  |
| `+` | Repeat the previous symbol `1` or more times |
| `?` | Repeat the previous symbol `0` or `1` times |

>**Note:** `*` is the same as `{0,}`, `+` is the same as `{1,}` and `?` is the same as `{0,1}`

A common use for `?` is to allow both singular and plural words: `cats?` will match either `cat` or `cats`. 

Repetitions are greedy on searches, they try to get the largest match possible. Sometimes that's undesired. You can force a lazy search by adding `?` after `*` or `+`.
The `?` instructs the regex engine to make a lazy search, which gives the smallest match possible.

Greedy Search: `a.*a` will find ![Greedy Search](/images/greedy.png)
Lazy Search: `a.*?a` will find ![Lazy Search](/images/greedy.png)

@[Exercise 4 - Simplified XML Tags ]({"stubs": ["Exercises/Exercise4.cs"],"command": "RegexCourse.Validator.VerifyExercise4"})

>**Note:** Regex is not recommended for parsing XML or HTML. See: http://stackoverflow.com/questions/1732348/regex-match-open-tags-except-xhtml-self-contained-tags 
But for simple things it can find what you need.

In the next lesson you'll learn about Alternations.
