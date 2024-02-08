using System;
using System.Linq;
using NUnit.Framework;

namespace PersianSwear.Tests;

public class FilterPersianWordsTests
{
    private readonly FilterPersianWords _sut;
    private readonly Helper _helper;

    public FilterPersianWordsTests()
    {
        _sut = new FilterPersianWords();
        _helper = new Helper();
    }

    [Test]
    public void IsSensitivePhrase_WhenCalledWithNullPhrase_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _sut.IsSensitivePhrase(null!));
    }

    [Test]
    public void IsSensitivePhrase_WhenCalledWithEmptyString_ReturnsFalse()
    {
        Assert.IsFalse(_sut.IsSensitivePhrase(string.Empty));
    }

    [Test]
    public void IsSensitivePhrase_WhenCalledWithInSensitivePhrase_ReturnFalse()
    {
        var inSensitivePhrase = _helper.GenerateInSensitivePhrase();
        var result = _sut.IsSensitivePhrase(inSensitivePhrase);
        Assert.IsFalse(result);
    }

    [Test]
    public void IsSensitivePhrase_WhenCalledWithSensitivePhrase_ReturnsTrue()
    {
        var sensitivePhrase = _helper.GenerateSensitivePhrase();
        var result = _sut.IsSensitivePhrase(sensitivePhrase);
        Assert.IsTrue(result);
    }


    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void IsSensitiveSentence_WhenCalledWithNullOrEmpty_ThrowsArgumentNullException(string phrase)
    {
        Assert.Throws<ArgumentNullException>(() => _sut.IsSensitiveSentence(phrase));
    }

    [Test]
    public void IsSensitiveSentence_WhenCalledWithInSensitiveSentence_ReturnsFalse()
    {
        var inSensitiveSentence = _helper.GenerateInSensitiveSentence();
        var result = _sut.IsSensitiveSentence(inSensitiveSentence);
        Assert.IsFalse(result);
    }

    [Test]
    public void IsSensitiveSentence_WhenCalledWithSensitiveSentenceAndExpectedCountAboveActual_ReturnsFalse()
    {
        var inSensitiveSentence = _helper.GenerateSensitivePhrase();
        var result = _sut.IsSensitiveSentence(inSensitiveSentence, 100);
        Assert.IsFalse(result);
    }

    [Test]
    public void IsSensitiveSentence_WhenCalledWithSensitiveSentence_ReturnsTrue()
    {
        var inSensitiveSentence = _helper.GenerateSensitiveSentence();
        var result = _sut.IsSensitiveSentence(inSensitiveSentence);
        Assert.IsTrue(result);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void RemoveBadWords_WhenCalledWithNullOrEmptySentence_ThrowsArgumentNullException(string phrase)
    {
        Assert.Throws<ArgumentNullException>(() => _sut.RemoveSensitivePhrases(phrase));
    }

    [Test]
    public void RemoveSensitivePhrases_WhenCalledWithInSensitiveSentence_ReturnsExactSameSentenceAsPassed()
    {
        var inSensitiveSentence = _helper.GenerateInSensitiveSentence();
        var result = _sut.RemoveSensitivePhrases(inSensitiveSentence);
		Assert.AreEqual(inSensitiveSentence.RemoveLinesInSentence().RemoveSeperatiorsInSentence(), result);
	}

    [Test]
    public void RemoveSensitivePhrases_WhenCalledWithSensitiveSentence_ReturnsFixedSentence()
    {
        var sensitiveSentence = _helper.GenerateSensitiveSentence();
        var result = _sut.RemoveSensitivePhrases(sensitiveSentence);
        Assert.AreNotEqual(sensitiveSentence, result);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void GetSensitivePhrases_WhenCalledWithNullOrEmptySentence_ThrowsArgumentNullException(string phrase)
    {
        Assert.Throws<ArgumentNullException>(() => _sut.RemoveSensitivePhrases(phrase));
    }

    [Test]
    public void GetSensitivePhrases_WhenCalledWithInSensitiveSentence_ReturnsEmptyList()
    {
        var inSensitiveSentence = _helper.GenerateInSensitiveSentence();
        var result = _sut.GetSensitivePhrases(inSensitiveSentence);
        Assert.IsEmpty(result);
    }

    [Test]
    public void GetSensitivePhrases_WhenCalledWithSensitiveSentence_ReturnsListOfSensitivePhrases()
    {
		var sensitiveSentence = _helper.GenerateSensitiveSentence();
		var result = _sut.GetSensitivePhrases(sensitiveSentence);
		var sensitivePhrases = result as string[] ?? result.ToArray();
		Assert.IsNotEmpty(sensitivePhrases);
		foreach (var sensitivePhrase in sensitivePhrases)
		{
			Assert.IsTrue(_helper.DoesPhraseExistInFile(sensitivePhrase));
		}
	}

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void GetSensitivePhrasesWithMatches_WhenCalledWithNullOrEmptySentence_ThrowsArgumentNullException(
        string sentence)
    {
        Assert.Throws<ArgumentNullException>(() => _sut.GetSensitivePhrasesWithMatches(sentence));
    }

    [Test]
    public void GetSensitivePhrasesWithMatches_WhenCalledWithInSensitiveSentence_ReturnsEmptyList()
    {
        var inSensitiveSentence = _helper.GenerateInSensitivePhrase();
        var result = _sut.GetSensitivePhrasesWithMatches(inSensitiveSentence);
        Assert.IsEmpty(result);
    }

    [Test]
    public void GetSensitivePhrasesWithMatches_WhenCalledWithSensitiveSentence_ReturnsListOfSensitivePhrases()
    {
        var sensitiveSentence = _helper.GenerateSensitivePhrase();
        var result = _sut.GetSensitivePhrasesWithMatches(sensitiveSentence);
        Assert.IsNotEmpty(result);
        foreach (var phrase in result)
        {
            Assert.Greater(phrase.Value.Count, 0);

            foreach (var sensitivePhrase in phrase.Value)
            {
                Assert.IsTrue(_helper.DoesPhraseExistInFile(sensitivePhrase));
            }
        }
    }
}