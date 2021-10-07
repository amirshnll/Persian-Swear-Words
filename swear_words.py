import json

class SwearWords(object):
    def __init__(self):
        self.data = json.load(open('data.json'))

    def filter_words(self,text,symbol="*"):
        text = text.split()
        print(text)
        for i in range(len(text)):
            if text[i] in self.data['word']:
                text[i] = symbol * len(text[i])
        return " ".join(text)