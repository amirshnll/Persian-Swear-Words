package main

import (
	"bufio"
	"os"
	"regexp"
	"strings"
)

type PersianSwear struct {
	swearWords []string
}

func NewPersianSwear() *PersianSwear {
	ps := &PersianSwear{}
	file, err := os.Open("data.txt")
	if err != nil {
		panic(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := scanner.Text()
		splitedWords := strings.Split(line, ", ")
		ps.swearWords = append(ps.swearWords, splitedWords...)
	}

	if err := scanner.Err(); err != nil {
		panic(err)
	}
	return ps
}

func (ps *PersianSwear) clearWord(word string) string {
	pattern := regexp.MustCompile(`[\p{P}\p{Mn}\p{S}\p{N}]`)
	clearedWord := pattern.ReplaceAllString(word, "")
	clearedWord = strings.TrimSpace(clearedWord)
	clearedWord = strings.ReplaceAll(clearedWord, "\u200c", " ")
	clearedWord = strings.ReplaceAll(clearedWord, "ي", "ی")
	clearedWord = strings.ReplaceAll(clearedWord, "ك", "ک")
	clearedWord = strings.ReplaceAll(clearedWord, "ـ", "")
	return clearedWord
}

func (ps *PersianSwear) AddWord(word string) {
	ps.swearWords = append(ps.swearWords, strings.TrimSpace(word))
}

func (ps *PersianSwear) AddWords(words ...string) {
	ps.swearWords = append(ps.swearWords, words...)
}

func (ps *PersianSwear) RemoveWord(word string) {
	trimmedWord := strings.TrimSpace(word)
	for i, w := range ps.swearWords {
		if w == trimmedWord {
			ps.swearWords = append(ps.swearWords[:i], ps.swearWords[i+1:]...)
			break
		}
	}
}

func (ps *PersianSwear) RemoveWords(words ...string) {
	for _, word := range words {
		ps.RemoveWord(word)
	}
}

func (ps *PersianSwear) IsBad(word string) bool {
	clearedWord := ps.clearWord(word)
	for _, w := range ps.swearWords {
		if w == clearedWord {
			return true
		}
	}
	return false
}

func (ps *PersianSwear) HasSwear(text string) bool {
	words := strings.Split(text, " ")
	for _, word := range words {
		if ps.IsBad(word) {
			return true
		}
	}
	return false
}

func (ps *PersianSwear) FilterWords(text, symbol string) string {
	words := strings.Split(text, " ")
	for i, word := range words {
		if ps.IsBad(word) {
			words[i] = symbol
		}
	}
	return strings.Join(words, " ")
}

func (ps *PersianSwear) FilterWordsWithDefaultSymbol(text string) string {
	return ps.FilterWords(text, "*")
}
