#!/bin/sh

# echo "hoge"
source ~/.bash_profile
# echo $!

fileName=${2}
recordTime=${1}
# mkdirはどっか同じやつ参照するようにする
# adb shell mkdir /sdcard/AndroidRecorder
adb shell screenrecord --time-limit ${recordTime} /sdcard/${fileName} &
echo $!
sleep recordTime
# sleep ${recordTime}