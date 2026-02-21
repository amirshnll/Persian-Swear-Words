import json
from string import punctuation


class PersianSwear:
    def __init__(self):
        with open("data.json") as file:
            self.data = json.load(file)
        all_words = self.data["word"]
        self.swear_words = set(w for w in all_words if " " not in w)
        phrase_words_list = [w.split() for w in all_words if " " in w]
        self.phrase_list = sorted(phrase_words_list, key=len, reverse=True)

    def ignoreSY(self, text):
        translator = str.maketrans("", "", punctuation)
        return text.translate(translator)

    def filter_words(self, text, symbol="*", ignoreOT=False):
        if not self.swear_words and not self.phrase_list:
            return text

        words = text.split()
        filtered_words = []
        i = 0
        while i < len(words):
            matched_phrase = False
            for phrase_words in self.phrase_list:
                n = len(phrase_words)
                if i + n > len(words):
                    continue
                if ignoreOT:
                    slice_normalized = [self.ignoreSY(words[i + j]) for j in range(n)]
                    phrase_normalized = [self.ignoreSY(w) for w in phrase_words]
                    if slice_normalized == phrase_normalized:
                        filtered_words.append(symbol)
                        i += n
                        matched_phrase = True
                        break
                else:
                    if words[i : i + n] == phrase_words:
                        filtered_words.append(symbol)
                        i += n
                        matched_phrase = True
                        break
            if not matched_phrase:
                word = words[i]
                if word in self.swear_words or (
                    ignoreOT and self.ignoreSY(word) in self.swear_words
                ):
                    filtered_words.append(symbol)
                else:
                    filtered_words.append(word)
                i += 1
        return " ".join(filtered_words)

    def is_empty(self):
        return not self.swear_words and not self.phrase_list

    def add_word(self, word):
        self.data["word"].append(word)
        if " " in word:
            phrase_words = word.split()
            self.phrase_list.append(phrase_words)
            self.phrase_list.sort(key=len, reverse=True)
        else:
            self.swear_words.add(word)

    def remove_word(self, word):
        if word in self.data["word"]:
            self.data["word"].remove(word)
        if " " in word:
            phrase_words = word.split()
            if phrase_words in self.phrase_list:
                self.phrase_list.remove(phrase_words)
        elif word in self.swear_words:
            self.swear_words.remove(word)

    def is_bad(self, text, ignoreOT=False):
        if ignoreOT:
            text = self.ignoreSY(text)
        text = text.replace("\u200c", "")
        return text in self.swear_words

    def has_swear(self, text, ignoreOT=False):
        text = text.replace("\u200c", "")
        if not self.swear_words and not self.phrase_list:
            return False
        if ignoreOT:
            text = self.ignoreSY(text)
        words = text.split()
        i = 0
        while i < len(words):
            for phrase_words in self.phrase_list:
                n = len(phrase_words)
                if i + n > len(words):
                    continue
                if ignoreOT:
                    slice_n = [self.ignoreSY(words[i + j]) for j in range(n)]
                    phrase_n = [self.ignoreSY(w) for w in phrase_words]
                    if slice_n == phrase_n:
                        return True
                else:
                    if words[i : i + n] == phrase_words:
                        return True
            if words[i] in self.swear_words or (
                ignoreOT and self.ignoreSY(words[i]) in self.swear_words
            ):
                return True
            i += 1
        return False

    def tostring(self):
        return " - ".join(self.swear_words)
