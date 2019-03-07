#!/bin/sh

adbPath=${1}
recordTime=${2}
fileName=${3}
${adbPath} shell screenrecord --time-limit ${recordTime} /sdcard/${fileName} &
echo $!
sleep recordTime