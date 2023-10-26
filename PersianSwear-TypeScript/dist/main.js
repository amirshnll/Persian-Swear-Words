"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.PersianSwear = void 0;
const data_json_1 = __importDefault(require("./data/data.json"));
exports.PersianSwear = {
    dataSet: new Set(data_json_1.default.word),
    isBad: function (text) {
        return this.dataSet.has(text);
    },
    filterWords: function (text, symbol) {
        const pattern = new RegExp(`\\b(${this.dataSet.join('|')})\\b`, 'gi');
        return text.replace(pattern, symbol);
    },
    hasSwear: function (text) {
        const pattern = new RegExp(`\\b(${this.dataSet.join('|')})\\b`, 'gi');
        return pattern.test(text);
    },
};
