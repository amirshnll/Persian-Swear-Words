# Persian-Swear-Words
Persian (Farsi) Swear Words + .json Datasets

* Author: Amir Shokri
* Author Email: amirsh.nll@gmail.com
* Last Update: 11 October, 2021
* Data format: JSON Data
* Functions Availabe :
	* PHP
	* Python
	* Javascript
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

* PHP
* Python
* Javascript
* C#

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

print(persianswear.is_bad(ignoreOT=False ,'خر')) # True

print(persianswear.is_bad(ignoreOT=False ,'امروز')) # False

print(persianswear.is_bad(ignoreOT=False ,'چرت و پرت')) # False

persianswear.add_word('چرت و پرت')
print(persianswear.is_bad(ignoreOT=False ,'چرت و پرت')) # True

print(persianswear.has_swear(ignoreOT=False ,'تو دوست من هستی')) # False

print(persianswear.has_swear(ignoreOT=False ,'تو هیز هستی')) # True

print(persianswear.filter_words(ignoreOT=False ,'تو دوست من هستی')) # تو دوست من هستی

print(persianswear.filter_words(ignoreOT=False ,'تو هیز هستی')) # تو * هستی

print(persianswear.filter_words(ignoreOT=False ,'تو هیز هستی', '&')) # تو & هستی


print(persianswear.is_bad(ignoreOT=True , 'خ.ر')) # True

print(persianswear.is_bad(ignoreOT=True , 'ام.روز')) # False

print(persianswear.has_swear(ignoreOT=True ,'تو دو.ست من هستی')) # False

print(persianswear.has_swear(ignoreOT=False ,'تو اسک.ل هستی')) # True

print(persianswear.filter_words(ignoreOT=True ,'تو دو.ست من هستی')) # تو دو.ست من هستی

print(persianswear.filter_words(ignoreOT=False ,'تو هی.ز هستی')) # تو * هستی

print(persianswear.filter_words(ignoreOT=False ,'تو هی.ز هس.تی')) # تو * هس.تی

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

<p dir="rtl">از این دیتاست و کلمات این دیتاست می توانید برای فیلتر کردن کامنت ها و پیام های اینستاگرامتون هم استفاده کنید:</p>

https://youtu.be/8YNCmrB5Y0M


<br />

### Related Link
* https://jadi.net/2020/11/mondays-99-08/
* https://awesomeopensource.com/projects/persian
* https://twitter.com/SamadiPour/status/1362702419252178945?s=20
* https://packagist.org/packages/amirshnll/persian-swear-words
* https://www.npmjs.com/package/persian-swear-words
* https://github.com/mmdbalkhi/Sansorchi
* https://matnbaz.net/github/amirshnll/Persian-Swear-Words

<br />

### Donate 
Donate (BTC Address) : 188GPtyoyz2LXXxbyWcCrDgUM7ejSHHgZd

Donate (Ethereum Address) : 0x2a77dAB50D76A2aE0A3A7572Ee2Fc34d5F94b3dB

<br />

### Other datasets collected by me
* [Persian-Swear-Words](https://github.com/amirshnll/Persian-Swear-Words/)
* [persian-stop-word](https://github.com/amirshnll/persian-stop-word/)
* [persian-sms-spam-word](https://github.com/amirshnll/persian-sms-spam-word/)
* [tsetmc-dataset](https://github.com/amirshnll/tsetmc-dataset/)
* [persianwordjson](https://github.com/amirshnll/persianwordjson/)
* [English-Persian-Word-Database](https://github.com/amirshnll/English-Persian-Word-Database/)
* [Covid-patient-datasets](https://github.com/amirshnll/Covid-patient-datasets/)
* [ibto-datasets](https://github.com/amirshnll/ibto-datasets)
* [persian-slang](https://github.com/amirshnll/persian-slang)
* [persian-news-dataset](https://github.com/amirshnll/persian-news-dataset)
* [persian-sms-dataset](https://github.com/amirshnll/persian-sms-dataset)
