## How To Use Library


### Create Filter 
First of All You Need To Create Instance of **FilterPersianWords**

`var filter  = new FilterPersianWords();`

if you have any optional json file path you can pass it down to constructor.

### Use Functions

 - Is a **single word** bad?

> `var isBadWord = filter.IsBadWord("yourWord");`
- Is a **multi line string** bad?
>`var isBadSentence = filter.IsBadSentence("your long sentence");`
- **Get all bad words** inside of string 
>`var badList = filter.GetBadWords("your long sentence");`
- **Remove All Bad words** From String
>`var clearedString = filter.RemoveBadWords("your bad sentence");`
>>This Method Will not change any data from string except the bad words.


## Exceptions

 - None Of the parameters should leaved as null or empty otherwise methods will throw 
 - -[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception?view=net-6.0)


## Platforms
**This Small Library Will Only Support On .Net + 6**