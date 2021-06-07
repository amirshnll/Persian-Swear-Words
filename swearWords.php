<?php

	/**
	 * swearWords Class
	 * Author : Amir Shokri
	 * E-mail : amirsh.nll@gmail.com
	 * created date : 29 May, 2020
	 * updated date : 8 June, 2021
	 */

	class swearWords
	{
		private $data;
		private $data_encoded;
		private $words;

		function __construct()
		{
			$this->data 			= file_get_contents('data.json');
			$this->data_encoded 		= json_decode($this->data, true);
			$this->words 			= $this->data_encoded['word'];
		}

		function filterwords($text, $symbol="*"){
			$filterCount = sizeof((array) $this->words);
			for ($i = 0; $i < $filterCount; $i++) {
				$text = preg_replace('['.$this->words[$i].']', str_repeat($symbol, strlen('$0')), $text);
			}
			return $text;
		}

	}

?>
