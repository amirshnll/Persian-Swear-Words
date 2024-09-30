# Persian-Swear-Words

Persian (Farsi) Swear Words + ‍`.json` Datasets

- Functions Availabe:
	- [Java](#java)
    - [Go](#go)
	- [PHP](#php)
	- [Python](#python)
	- [JavaScript](#javascript)
	- [TypeScript](#typescript)
	- [C#](#csharp)
	- [Swift](#swift)
- Contribute: Fork and Push Requests :)
- DOI : `10.34740/kaggle/dsv/2094967`
- [`CONTRIBUTING.md`](CONTRIBUTING.md): Guidelines for contributing to the repository.
- [`CODE_OF_CONDUCT.md`](CODE_OF_CONDUCT.md): The code of conduct for participants in the repository.


**Note:** This is a to-be-complete list of Persian Swears you can use in your production to filter unwanted content. Wordlist is available in JSON format.

<div dir="rtl">

**یادداشت‌ها:**

این دیتاست شامل کلماتی است که ممکن است در برخی موارد نیاز به فیلترینگ داشته باشند. کاربران برای استفاده‌های خاص باید دیتاست را متناسب با نیازهای خود شخصی‌سازی کنند. تشویق می‌شود علاقه‌مندان در تکمیل این دیتاست مشارکت کنند و برای پروژه‌های خود از آن استفاده نمایند تا متون پاک و مناسبی داشته باشند. به جای ارسال PRهای کوچک، مشارکت‌های ارزشمندتری انجام دهید. همچنین، امکان اضافه کردن class یا function به زبان‌های برنامه‌نویسی مختلف با استفاده از این دیتاست وجود دارد.

در حال حاضر توابع مربوط به زبان های زیر موجود است:

</div>

- [Java](#java)
- [Go](#go)
- [PHP](#php)
- [Python](#python)
- [JavaScript](#javascript)
- [TypeScript](#typescript)
- [C#](#csharp)
- [Swift](#swift)

<br />

موارد بیشتر: 
- [`CONTRIBUTING.md`](CONTRIBUTING.md): دستورالعمل‌هایی برای مشارکت در مخزن این پروژه
- [`CODE_OF_CONDUCT.md`](CODE_OF_CONDUCT.md): کد اخلاقی برای مشارکت‌کنندگان در مخزن این پروژه


### Installation | دستورالعمل‌های نصب

#### نصب با composer

```
composer require amirshnll/persian-swear-words
```

#### نصب با npm

```
npm i persian-swear-words
```

### Usage | روش‌های استفاده

#### java

Code link: [ 🔗 Class ](PersianSwear.java)

```java
var persianSwear = new PersianSwear();

// add word(s) to DataSet
persianSwear.addWord("word");
persianSwear.addWords(new String[]{"word1", "word2"});

// remove word(s) from DataSet
persianSwear.removeWord("word");
persianSwear.removeWords(new String[]{"word1", "word2"});

// check single word
persianSwear.isBad("الا.غ "); // true
persianSwear.isBad("امروز"); // false

// check existing bad word in text
persianSwear.hasSwear("تو هیز هستی");     // true
persianSwear.hasSwear("تو دوست من هستی"); // false

// replace bad words in text
persianSwear.filterWords("تو هیز هستی");      // تو * هستی
persianSwear.filterWords("تو هیز هستی", "&"); // تو & هستی
```
#### go

Code link: [ 🔗 Function ](PersianSwear.go)

```go
persianswear := NewPersianSwear()

// add word(s) to DataSet
persianswear.AddWord("word")
persianswear.AddWords([]string{"word1", "word2"})

// remove word(s) from DataSet
persianswear.RemoveWord("word")
persianswear.RemoveWords([]string{"word1", "word2"})

// check single word
persianswear.IsBad("الا.غ ") // true
persianswear.IsBad("امروز") // false

// check existing bad word in text
persianswear.HasSwear("تو هیز هستی")     // true
persianswear.HasSwear("تو دوست من هستی") // false

// replace bad words in text
persianswear.FilterWords("تو هیز هستی")      // تو * هستی
persianswear.FilterWords("تو هیز هستی", "&") // تو & هستی
```

#### php

Code link: [ 🔗 Class ](PersianSwear.php)

```php
require('PersianSwear.php');
$persianswear = new PersianSwear();

// is bad
if($persianswear->is_bad('خر'))
	echo 'is bad';
else
	echo 'not bad';

// not bad
if($persianswear->is_bad('امروز'))
	echo 'is bad';
else
	echo 'not bad';

// not bad
if($persianswear->is_bad('چرت و پرت'))
	echo 'is bad';
else
	echo 'not bad';

$persianswear->add_word('چرت و پرت');
// is bad

if($persianswear->is_bad('چرت و پرت'))
	echo 'is bad';
else
	echo 'not bad';

// is bad
if($persianswear->is_bad('گاو'))
	echo 'is bad';
else
	echo 'not bad';

$persianswear->remove_word('گاو');

// not bad
if($persianswear->is_bad('گاو'))
	echo 'is bad';
else
	echo 'not bad';

// not bad
if($persianswear->has_swear('تو دوست من هستی'))
	echo 'is bad';
else
	echo 'not bad';

// is bad
if($persianswear->has_swear('تو هیز هستی'))
	echo 'is bad';
else
	echo 'not bad';

echo $persianswear->filter_words('تو دوست من هستی'); // تو دوست من هستی
echo $persianswear->filter_words('تو هیز هستی'); // تو * هستی
echo $persianswear->filter_words('تو هیز هستی', "&"); // تو & هستی

echo $persianswear->tostring(); // show all swear words
```

#### python

Code link: [ 🔗 Class ](PersianSwear.py)

```python
persianswear = PersianSwear()

print(persianswear.is_bad('خر',ignoreOT=False )) # True

print(persianswear.is_bad('امروز',ignoreOT=False )) # False

print(persianswear.is_bad('چرت و پرت',ignoreOT=False )) # False

persianswear.add_word('چرت و پرت')
print(persianswear.is_bad('چرت و پرت' , ignoreOT=False )) # True

print(persianswear.has_swear('تو دوست من هستی' , ignoreOT=False )) # False

print(persianswear.has_swear('تو هیز هستی' , ignoreOT=False )) # True

print(persianswear.filter_words('تو دوست من هستی' , ignoreOT=False )) # تو دوست من هستی

print(persianswear.filter_words('تو هیز هستی' , ignoreOT=False )) # تو * هستی

print(persianswear.filter_words('تو هیز هستی', '&' , ignoreOT=False )) # تو & هستی


print(persianswear.is_bad('خ.ر' , ignoreOT=True )) # True

print(persianswear.is_bad( 'ام.روز' , ignoreOT=True )) # False

print(persianswear.has_swear('تو دو.ست من هستی' , ignoreOT=True )) # False

print(persianswear.has_swear('تو اسک.ل هستی' , ignoreOT=True )) # True

print(persianswear.filter_words('تو دو.ست من هستی',ignoreOT=True )) # تو دو.ست من هستی

print(persianswear.filter_words('تو هی.ز هستی',ignoreOT=True )) # تو * هستی

print(persianswear.filter_words('تو هی.ز هس.تی' , ignoreOT=True )) # تو * هس.تی

print(persianswear.tostring()) # show all swear words
```

#### javaScript

Code link: [ 🔗 Function ](PersianSwear.js)

```javaScript
import PersianSwear from './PersianSwear.js';

const text1 = 'تو دوست من هستی';
console.log(PersianSwear.hasSwear(text1)); // Output: false

const text2 = 'تو هیز هستی';
console.log(PersianSwear.hasSwear(text2)); // Output: true

const text3 = 'تو هیز هستی';
const filteredText = PersianSwear.filterWords(text3);
console.log(filteredText); // Output: تو * هستی

const word = 'خر';
console.log(PersianSwear.isBad(word)); // Output: true
```

<br />

#### typeScript

Code link: [ 🔗 Function ](/PersianSwear-TypeScript/dist/)

```typeScript
import { test, expect } from "@jest/globals";

import { PersianSwear } from "../src";

test("test check bad word", () => {
  expect(PersianSwear.isBad("آشغال")).toBe(true);
});
test("test check not bad word", () => {
  expect(PersianSwear.isBad("سلام")).toBe(false);
});

test("test check text no has bad word", () => {
  expect(PersianSwear.hasSwear("سلام عزیزم")).toBe(false);
});
test("test check text has bad word", () => {
  expect(PersianSwear.hasSwear("سلام کصافت")).toBe(true);
});
test("test check text has bad word and replace with symbol", () => {
  expect(PersianSwear.filterWords("سلام کصافت خوبی", "*")).toBe("سلام * خوبی");
});
```

<br />

#### csharp

Code link: [ 🔗 Helper ](PersianSwear-CSharp)

### Create Filter

First of All You Need To Create Instance of **FilterPersianWords**

`var filter  = new FilterPersianWords();`

if you have any optional json file path you can pass it down to constructor.

### Use Functions

- Is a **single word** bad?
`var isBadWord = filter.IsBadWord("yourWord");`

- Is a **multi line string** bad?
`var isBadSentence = filter.IsBadSentence("your long sentence");`

- **Get all bad words** inside of string
> `var badList = filter.GetBadWords("your long sentence");`

- **Remove All Bad words** From String
`var clearedString = filter.RemoveBadWords("your bad sentence");`

This Method Will not change any data from string except the bad words.

<br /><br />

#### wwift

Code link: [ 🔗 Classes and Protocol ](PersianSwear.swift)

<div dir='rtl'>
کلاس اصلی <code>PersianSwear</code> هست، که متدها داخل آن پیاده‌سازی شده است:

<br>

```swift
// add word(s) to DataSet
PersianSwear.shared.addWord("bad-word")
PersianSwear.shared.addWords(["bad-word-1", "bad-word-2"])

// remove word(s) from DataSet
PersianSwear.shared.removeWord("bad-word")
PersianSwear.shared.removeWords(["bad-word-1", "bad-word-2"])

// check single word
let isBadWord = PersianSwear.shared.isBadWord("single word")

// check existing bad word in text
let hasBadWord = PersianSwear.shared.hasBadWord("long text")

// existing bad word in text
let badWords = PersianSwear.shared.badWords(in: "long text")

// replace bad words in text
let newText = PersianSwear.shared.replaceBadWords(in: "long text", with: "****")
```

<br>

یک پروتکل با اسم <code>PersianSwearDataLoader</code> که کارایی آن لود کردن کلمات هست وجود دارد:

<br>

```swift
protocol PersianSwearDataLoader {
	func loadWords(
		_ completion: @escaping (Result<PersianSwear.Words, Error>) -> Void
	)
}
```

<br>

برای نمونه، تایپ لود کننده کلمات از روی گیت‌هاب پیاده‌سازی شده. نمونه استفاده هم بصورت زیر هست:

<br>

```swift
let loader = GithubPersianSwearDataLoader()
PersianSwear.shared.loadWords(using: loader) { result in
	switch result {
	case .failure(let error):
		print("Error:", error.localizedDescription)
	case .success(let words):
		print("Words:", words.count)
	}
}
```

</div>

<br /><br />

### Related Link |‌ لینک‌های مرتبط با پروژه

- https://jadi.net/2020/11/mondays-99-08/
- https://awesomeopensource.com/projects/persian
- https://twitter.com/SamadiPour/status/1362702419252178945?s=20
- https://packagist.org/packages/amirshnll/persian-swear-words
- https://www.npmjs.com/package/persian-swear-words
- https://github.com/mmdbalkhi/Sansorchi
- https://matnbaz.net/github/amirshnll/Persian-Swear-Words
