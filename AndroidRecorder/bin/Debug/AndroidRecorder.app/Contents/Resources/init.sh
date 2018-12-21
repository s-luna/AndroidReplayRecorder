#!/bin/sh

currentdir=$(pwd)

bash ${currentdir}/../../../main.sh &
echo $!

