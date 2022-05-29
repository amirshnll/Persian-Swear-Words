"""
swearWords Class
Author : Sorosh Safari @coci
created date : 7 October, 2021
updated date : 11 October, 2021
"""
import json
from string import punctuation
class PersianSwear(object):
	def __init__(self):
		self.data = json.load(open('data.json'))

	#Rmove punctuation characters from text
	# return string
	def ignoreSY(self, text):
		for i in punctuation:
				if i in text:
					text=text.replace(i,'')
		return text
	# return string
	def filter_words(self, text , symbol="*" , ignoreOT=False):
		if(self.is_empty()):
			return text

		text = text.split()
		for i in range(len(text)):
			if text[i] in self.data['word'] or (ignoreOT and self.ignoreSY(text[i]) in self.data['word']):
				text[i] = symbol

		return " ".join(text)

	# return boolean
	def is_empty(self):
		if(len(self.data['word'])<1):
			return True;
		return False;

	# return nothing
	def add_word(self, text):
		self.data['word'].append(text)

	# return nothing
	def remove_word(self, text):
		self.data['word'].remove(text)	

	# return boolean
	def is_bad(self, text , ignoreOT=False):
		if ignoreOT:
			text=self.ignoreSY(text)
		text=text.replace("\u200c","")
		return text in self.data['word']

	# return boolean
	def has_swear(self, text , ignoreOT=False):
		if ignoreOT:
			text=self.ignoreSY(text)
		text=text.replace("\u200c","")
		if(self.is_empty()):
			return text

		text = text.split()
		for i in range(len(text)):
			if text[i] in self.data['word']:
				return True

		return False

	# return string
	def tostring(self):
		return ' - '.join(self.data['word'])
