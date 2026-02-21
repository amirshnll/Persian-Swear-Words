"""
Unit tests for PersianSwear (PersianSwear.py).
Covers filter_words (single-word and multi-word), has_swear, is_bad, add_word, remove_word, is_empty, tostring.
"""
import os
import sys
import unittest

sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
os.chdir(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

from PersianSwear import PersianSwear


class TestFilterWords(unittest.TestCase):
    """Tests for filter_words (single-word and multi-word replacement)."""

    def setUp(self):
        self.p = PersianSwear()

    def test_multi_word_phrase_replacement_issue29(self):
        """Issue #29: multi-word phrase in dataset is replaced as one unit."""
        self.p.swear_words = set()
        self.p.phrase_list = [["test", "world"]]
        self.assertEqual(
            self.p.filter_words("this is a test world"),
            "this is a *",
        )

    def test_multi_word_phrase_custom_symbol(self):
        """Multi-word phrase is replaced with custom symbol."""
        self.p.swear_words = set()
        self.p.phrase_list = [["foo", "bar", "baz"]]
        self.assertEqual(
            self.p.filter_words("one foo bar baz two", symbol="***"),
            "one *** two",
        )

    def test_single_word_replacement(self):
        """Single swear word is replaced."""
        self.p.swear_words = {"bad"}
        self.p.phrase_list = []
        self.assertEqual(
            self.p.filter_words("this is bad"),
            "this is *",
        )

    def test_single_word_with_real_data(self):
        """Single word from data.json is filtered (e.g. کثافت)."""
        result = self.p.filter_words("سلام کثافت خوبی")
        self.assertEqual(result, "سلام * خوبی")

    def test_multi_word_phrase_with_real_data(self):
        """Multi-word phrase from data.json is filtered as one (e.g. سگ پدر)."""
        result = self.p.filter_words("سلام سگ پدر خوبی")
        self.assertEqual(result, "سلام * خوبی")

    def test_phrase_then_single_word(self):
        """Longer phrase is matched before single word when overlapping."""
        self.p.swear_words = set()
        self.p.phrase_list = [["a", "b", "c"], ["a", "b"]]
        self.assertEqual(
            self.p.filter_words("x a b c y"),
            "x * y",
        )
        self.p.phrase_list = [["a", "b"], ["a", "b", "c"]]
        self.assertEqual(
            self.p.filter_words("x a b c y"),
            "x * c y",
        )

    def test_ignoreOT_single_word(self):
        """ignoreOT: single word with punctuation is matched."""
        self.p.swear_words = {"bad"}
        self.p.phrase_list = []
        self.assertEqual(
            self.p.filter_words("this is b.a.d", ignoreOT=True),
            "this is *",
        )

    def test_ignoreOT_phrase(self):
        """ignoreOT: phrase with punctuation in words is matched."""
        self.p.swear_words = set()
        self.p.phrase_list = [["test", "world"]]
        self.assertEqual(
            self.p.filter_words("this is a test. world", ignoreOT=True),
            "this is a *",
        )

    def test_empty_dataset_returns_unchanged(self):
        """When no words/phrases, text is returned unchanged."""
        self.p.swear_words = set()
        self.p.phrase_list = []
        self.assertEqual(
            self.p.filter_words("anything here"),
            "anything here",
        )

    def test_no_swear_returns_unchanged(self):
        """Text with no swear words is unchanged."""
        self.p.swear_words = {"other"}
        self.p.phrase_list = []
        self.assertEqual(
            self.p.filter_words("hello world"),
            "hello world",
        )


class TestHasSwear(unittest.TestCase):
    """Tests for has_swear (single-word and multi-word)."""

    def setUp(self):
        self.p = PersianSwear()

    def test_has_swear_single_word(self):
        self.p.swear_words = {"bad"}
        self.p.phrase_list = []
        self.assertTrue(self.p.has_swear("this is bad"))
        self.assertFalse(self.p.has_swear("this is good"))

    def test_has_swear_multi_word_phrase(self):
        self.p.swear_words = set()
        self.p.phrase_list = [["test", "world"]]
        self.assertTrue(self.p.has_swear("this is a test world"))
        self.assertFalse(self.p.has_swear("this is a test"))

    def test_has_swear_real_phrase(self):
        """Real data: سگ پدر is a phrase."""
        self.assertTrue(self.p.has_swear("سلام سگ پدر خوبی"))
        self.assertFalse(self.p.has_swear("سلام دوست خوبی"))

    def test_has_swear_empty_dataset(self):
        self.p.swear_words = set()
        self.p.phrase_list = []
        self.assertFalse(self.p.has_swear("anything"))


class TestIsBad(unittest.TestCase):
    """Tests for is_bad (exact single-word match)."""

    def setUp(self):
        self.p = PersianSwear()

    def test_is_bad_exact_match(self):
        self.p.swear_words = {"bad"}
        self.p.phrase_list = []
        self.assertTrue(self.p.is_bad("bad"))
        self.assertFalse(self.p.is_bad("bad word"))
        self.assertFalse(self.p.is_bad("not bad"))


class TestAddRemoveWord(unittest.TestCase):
    """Tests for add_word and remove_word (single and phrase)."""

    def setUp(self):
        self.p = PersianSwear()

    def test_add_remove_single_word(self):
        self.p.swear_words = set()
        self.p.phrase_list = []
        self.p.add_word("x")
        self.assertIn("x", self.p.swear_words)
        self.assertTrue(self.p.has_swear("x"))
        self.p.remove_word("x")
        self.assertNotIn("x", self.p.swear_words)
        self.assertFalse(self.p.has_swear("x"))

    def test_add_remove_phrase(self):
        self.p.swear_words = set()
        self.p.phrase_list = []
        self.p.add_word("test world")
        self.assertTrue(self.p.has_swear("hello test world"))
        self.assertEqual(self.p.filter_words("hello test world"), "hello *")
        self.p.remove_word("test world")
        self.assertFalse(self.p.has_swear("hello test world"))
        self.assertEqual(self.p.filter_words("hello test world"), "hello test world")


class TestIsEmpty(unittest.TestCase):
    """Tests for is_empty."""

    def setUp(self):
        self.p = PersianSwear()

    def test_is_empty_after_clear(self):
        self.p.swear_words = set()
        self.p.phrase_list = []
        self.assertTrue(self.p.is_empty())

    def test_not_empty_with_words(self):
        self.p.swear_words = {"a"}
        self.p.phrase_list = []
        self.assertFalse(self.p.is_empty())
        self.p.swear_words = set()
        self.p.phrase_list = [["a", "b"]]
        self.assertFalse(self.p.is_empty())


class TestTostring(unittest.TestCase):
    """Tests for tostring."""

    def setUp(self):
        self.p = PersianSwear()

    def test_tostring_joins_single_words(self):
        self.p.swear_words = {"a", "b"}
        self.p.phrase_list = []
        s = self.p.tostring()
        self.assertIn("a", s)
        self.assertIn("b", s)
        self.assertIn(" - ", s)


if __name__ == "__main__":
    unittest.main()
