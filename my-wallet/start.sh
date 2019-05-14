#!/bin/bash
set -e
cd /usr/src/app
echo --- Npm Install... ---
npm install
echo --- Executing angular... ---
ng serve --host 0.0.0.0 --disableHostCheck
