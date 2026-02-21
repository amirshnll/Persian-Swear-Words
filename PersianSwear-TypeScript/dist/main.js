"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const data_json_1 = __importDefault(require("./data/data.json"));
const PersianSwear = {
    dataSet: new Set(data_json_1.default.word),
    isBad: function (text) {
        return this.dataSet.has(text);
    },
    filterWords: function (text, symbol) {
        const splitedWord = text.split(" ");
        return splitedWord
            .map((word) => (this.isBad(word) ? symbol : word))
            .join(" ");
    },
    hasSwear: function (text) {
        const splitedWord = text.split(" ");
        return splitedWord.some((word) => this.dataSet.has(word));
    },
};
exports.default = PersianSwear;
