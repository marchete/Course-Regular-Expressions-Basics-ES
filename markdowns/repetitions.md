# Repetitions

Repetitions simplifies using the same pattern several consecutive times. It also allows flexible length searches, so you can match 'aaZ' and 'aaaaaaaaaaaaaaaaZ' with the same pattern.

### `{` `}` Ranges

You can use these sintaxis for defined ranges:

| Pattern | Description |
| ------ | ------ |
| `{n}` | Repeat the previous symbol exactly `n` times |
| `{n,}` | Repeat the previous symbol `n` times or more |
| `{min,max}` | Repeat the previous symbol between `min` and `max` times, both included |

So `a{6}` is the same as `aaaaaa`, and `[a-z]{1,3}` will match any text that has between 1 and 3 consecutive letters.
>**Note:** On repetitions each symbol match is independent. If `[a-z]{1,3}` first matches with 'a', on the next letter it can match with anything in [a-z] range, not only 'a'.

### Other Ranges

You can use these sintaxis for other types of range:

| Pattern | Description |
| ------ | ------ |
| `*` | Repeat the previous symbol `0` times or more |
| `+` | Repeat the previous symbol `1` times or more |
| `?` | Repeat the previous symbol `0` or `1` times |

>**Note:** `*` is the same as `{0,}`, `+` is the same as `{1,}` and `?` is the same as `{0,1}`

A common use for `?` is to allow both singular and plural words: `cats?` will match either `cat` or `cats`. 

Repetitions are greedy on searches, it tries to get the largest match possible. Sometimes that's undesired, you can force a lazy search by adding `?` after `*` or `+`.
That instructs the regex engine to make a lazy search, the smallest match possible.

@[Exercise 4 - Simplified XML Tags ]({"stubs": ["Exercises/Exercise4.cs"],"command": "RegexCourse.Validator.VerifyExercise4"})

>**Note:** Regex is not recommended for parsing XML or HTML. See: http://stackoverflow.com/questions/1732348/regex-match-open-tags-except-xhtml-self-contained-tags 
But for simple things it can find what you need.

On the next lesson you'll learn about Alternations.