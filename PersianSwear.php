<?php
class PersianSwear
{
	private $words;

	public function __construct()
	{
		if (file_exists("data.json")) {
			$data = file_get_contents('data.json');
			$data_encoded = json_decode($data, true);
			$this->words = array_flip($data_encoded['word']);
		} else {
			die("Dataset file not found!");
		}
	}

	public function is_empty()
	{
		return empty($this->words);
	}

	public function filter_words($text, $symbol = "*")
	{
		if ($this->is_empty()) {
			return $text;
		}

		$pattern = '/\b(' . implode('|', array_keys($this->words)) . ')\b/i';
		return preg_replace($pattern, str_repeat($symbol, strlen('$0')), $text);
	}

	public function add_word($text)
	{
		$this->words[ltrim(rtrim($text))] = true;
	}

	public function remove_word($text)
	{
		unset($this->words[ltrim(rtrim($text))]);
	}

	public function is_bad($text)
	{
		return empty($this->filter_words($text, ''));
	}

	public function has_swear($text)
	{
		return $this->is_bad($text);
	}

	public function tostring()
	{
		return implode(" ", array_keys($this->words));
	}
}
