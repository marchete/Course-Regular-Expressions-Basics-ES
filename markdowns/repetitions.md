# Repetitions

Repetitions simplifies using the same pattern several consecutive times. It also allows flexible length searches, so you can match 'aaZ' and 'aaaaaaaaaaaaaaaaZ' with the same pattern.

### `{` `}` Ranges

You can use these sintaxis for defined ranges:

| Pattern | Description |
| ------ | ------ |
| `{n}` | Repeat the previous symbol exactly `n` times |
| `{n,}` | Repeat the previous symbol `n` times or more |
| `{min,max}` | Repeat the previous symbol between `min` and `max` times, both included |

So `a{6}` it's the same as `aaaaaa`, and `[a-z]{1,3}` will match any text that has between 1 and 3 consecutive letters.
>**Note:** On repetitions each symbol match is independent. On `[a-z]{1,3}` pattern matching `a` first won't force that the second character must be an `a` too, it can be any letter from the character set.

### Other Ranges

You can use these sintaxis for other types of range:

| Pattern | Description |
| ------ | ------ |
| `*` | Repeat the previous symbol `0` times or more |
| `+` | Repeat the previous symbol `1` times or more |
| `?` | Repeat the previous symbol `0` or `1` times |

>**Note:** `*` it's the same as `{0,}`, `+` it's the same as `{1,}` and `?` it's the same as `{0,1}`

A common use for `?` is to allow both singular and plural words: `cats?` will match either `cat` or `cats`. 

Repetitions are greedy on searches, it tries to get the largest match possible. Sometimes that's undesired, you can force a lazy search by adding `?` after `*` or `+`.
That instructs the regex engine to make a lazy search, the smallest match possible.

@[Exercise 4 - Simplified XML Tags ]({"stubs": ["Exercises/Exercise4.cs"],"command": "RegexCourse.Validator.VerifyExercise4"})

Click on **`NEXT LESSON`** to learn about Alternations.