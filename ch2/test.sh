#!/usr/bin/env bash
# Script to test and validate code functionality
echo "Running code from book:"
mono ch2_sqliFuzzer.exe ""
echo "Running optimized code:"
# Doesn't work w/ docker image of badstore currently
# mono ch2_getFuzzer.exe "http://10.5.0.5/cgi-bin/badstore.cgi?searchquery=test&action=search"
mono ch2_getFuzzer.exe "http://192.168.177.128/cgi-bin/badstore.cgi?searchquery=test&action=search"
# mono ch2_dev.exe
mono ch2_dev.exe 
