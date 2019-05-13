#!/bin/sh

# docker run -v C:\Dev\Git\CodeGames\2019\cg-truffle\app:/usr/src/app --rm -it --network 2019_cg-net  2019_truffle:latest

echo --------------- TRUFFLE -----------------
truffle compile
truffle migrate
