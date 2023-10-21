/**
 * Represents an interface for working with potentially offensive language, such as swear words or profanity.
 * This interface provides methods to check for, filter, and detect swear words in text.
 *
 * @interface ISwear
 */
interface ISwear {
    /**
     * Check if the provided text contains swear words.
     *
     * @param {string} text - The text to be checked for swear words.
     * @returns {boolean} `true` if swear words are found, otherwise `false`.
     */
    isBad: (text: string) => boolean;
    /**
     * Replace characters in swear words within the provided text with a specified symbol.
     *
     * @param {string} text - The text in which swear words will be replaced.
     * @param {string} symbol - The symbol used to replace characters in swear words.
     * @returns {string} The filtered text with swear words replaced by the specified symbol.
     */
    filterWords: (text: string, symbol: string) => string;
    /**
     * Check if the provided text contains swear words.
     *
     * @param {string} text - The text to be checked for swear words.
     * @returns {boolean} `true` if swear words are found, otherwise `false`.
     */
    hasSwear: (text: string) => boolean;
    dataSet: Set<string>;
}
export declare const PersianSwear: ISwear;
export {};
