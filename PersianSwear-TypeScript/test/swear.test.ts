import { test, expect } from "@jest/globals";

import { PersianSwear } from '../src';

test("test check bad word", () => {
    expect(PersianSwear.isBad("آشغال")).toBe(true);
});
test("test check not bad word", () => {
    expect(PersianSwear.isBad("سلام")).toBe(false);
});

test("test check text no has bad word", () => {
    expect(PersianSwear.hasSwear("سلام عزیزم")).toBe(false);
});
test("test check text has bad word", () => {
    expect(PersianSwear.hasSwear("سلام کصافت")).toBe(true);
});
test("test check text has bad word and replace with symbol", () => {
    expect(PersianSwear.filterWords("سلام کصافت خوبی", "*")).toBe("سلام * خوبی");
});
