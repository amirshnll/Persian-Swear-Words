# Persian-Swear-Words
Persian (Farsi) Swear Words + .json Datasets

* Author: Amir Shokri
* Author Email: amirsh.nll@gmail.com
* Last Update: 11 October, 2021
* Data format: JSON Data
* Functions Availabe :
	* Java
    * PHP
	* Python
	* Javascript
	* Swift
* Contribute: Fork and Push Requests :)
* DOI : 10.34740/kaggle/dsv/2094967

**Note:** This is a to-be-complete list of Persian Swears you can use in your production to filter unwanted content. Wordlist is available in JSON format.

<div dir="rtl">

**یادداشت ها:**

برخی از کلمات در ظاهر کلمات بد به حساب نمیان ولی برای کاربردهای خاص ممکنه نیاز به فیلتر شدن داشته باشن که هر کس با توجه به نیاز  باید شخصی سازی انجام بده و از این دیتاست استفاده کنه

در صورت علاقه، به تکمیل شدن این دیتاست کمک کنید

از این دیتاست در فیلتر کردن متن ها در پروژه های خود استفاده کنید و متون پاک و سالمی را داشته باشید

از ارسال PR های کوچک خودداری کنید و مشارکت  مفیدتری داشته باشید.

به جز مشارکت در دیتاست می توانید به زبان برنامه نویسی مورد نظر خودتان class یا function با کمک این دیتاست بنویسید و به پروژه اضافه کنید. در حال حاضر توابع مربوط به زبان های زیر موجود است:

</div>

* Java
* PHP
* Python
* Javascript
* C#
* Swift

<br />

### Installation
#### composer
```
composer require amirshnll/persian-swear-words
```
#### npm
```
npm i persian-swear-words
```

### Usage

#### Java [ 🔗 Class ](PersianSwear.java)
```
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

#### PHP [ 🔗 Class ](PersianSwear.php)
```
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
#### Python [ 🔗 Class ](PersianSwear.py)
```
persianswear = PersianSwear()

print(persianswear.is_bad(,'خر',ignoreOT=False )) # True

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
#### Javascript [  🔗 Function ](PersianSwear.js)
```
console.log(PersianSwear.is_bad('خر')); // true
console.log(PersianSwear.is_bad('امروز')); // false

console.log(PersianSwear.is_bad('چرت و پرت')); // false
PersianSwear.add_word('چرت و پرت');
console.log(PersianSwear.is_bad('چرت و پرت')); // true

console.log(PersianSwear.is_bad('گاو')); // true
PersianSwear.remove_word('گاو');
console.log(PersianSwear.is_bad('گاو')); // false

console.log(PersianSwear.has_swear('تو دوست من هستی')); // false
console.log(PersianSwear.has_swear('تو هیز هستی')); // true

console.log(PersianSwear.filter_words('تو دوست من هستی')); // تو دوست من هستی 
console.log(PersianSwear.filter_words('تو هیز هستی')); // تو * هستی 
console.log(PersianSwear.filter_words('تو هیز هستی', '&')); // تو & هستی 
```

<br />

#### C# [  🔗 Helper ](PersianSwear-CSharp)

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

<br /><br />

#### Swift [  🔗 Classes and Protocol ](PersianSwear.swift)

<div dir='rtl'>
کلاس اصلی <code>PersianSwear</code> هست، که متدها داخلش پیاده‌سازی شده:

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

یه پروتکل هم با اسم <code>PersianSwearDataLoader</code> داریم که کارش لود کردن کلمات هست:

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

### Related Link
* https://jadi.net/2020/11/mondays-99-08/
* https://awesomeopensource.com/projects/persian
* https://twitter.com/SamadiPour/status/1362702419252178945?s=20
* https://packagist.org/packages/amirshnll/persian-swear-words
* https://www.npmjs.com/package/persian-swear-words
* https://github.com/mmdbalkhi/Sansorchi
* https://matnbaz.net/github/amirshnll/Persian-Swear-Words
