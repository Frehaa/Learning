@echo off

cl /LD add2.c
cl -Zi -EHsc /DEBUG usedll.c