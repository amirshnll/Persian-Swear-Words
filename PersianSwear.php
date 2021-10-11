<?php

/**
* PersianSwear Class
* Author : Amir Shokri @amirshnll
* created date : 29 May, 2020
* updated date : 11 October, 2021
*/

class PersianSwear {
	private $data;
	private $data_encoded;
	private $words;

	public function __construct() {
		if(file_exists("data.json")) {
			$this->data 		= file_get_contents('data.json');
			$this->data_encoded = json_decode($this->data, true);
			$this->words 		= $this->data_encoded['word'];
		} else
			die("dataset file not found!");
	}

	// return boolean
	public function is_empty() {
		if(count($this->words) < 1)
			return true;
		return false;
	}

	// return string
	public function filter_words($text, $symbol = "*") {
		if($this->is_empty())
			return $text;
		
		$filterCount = sizeof((array) $this->words);

		for ($i = 0; $i < $filterCount; $i++) {
			$text = preg_replace('['.$this->words[$i].']', str_repeat($symbol, strlen('$0')), $text);
		}
		return $text;
	}

	// return nothing
	public function add_word($text) {
		$this->words[count($this->words)] = ltrim(rtrim($text));
	}

	// return nothing
	public function remove_word($text) {
		$text = ltrim(rtrim($text));
		for ($i=0; $i <= count($this->words) - 1; $i++)
			if($this->words[$i] == $text)
				$this->words[$i] = null;
	}

	// return boolean
	public function is_bad($text) {
		if(empty(ltrim(rtrim($this->filter_words($text,"")))))
			return true;
		return false;
	}

	// return boolean
	public function has_swear($text) {
		$text_splited = str_split(ltrim(rtrim($text, " ")));
		foreach ($text_splited as $key => $val) {
			if($this->is_bad($val))
				return true;
		}
		return false;
	}

	// return string
	public function tostring() {
		return implode(" ",(array) $this->data);
	}

}