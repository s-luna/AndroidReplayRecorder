#!/bin/sh


source ~/.bash_profile

timelimit=180
name=$(date +%Y%m%d_%H-%M-%S)

adb shell screenrecord --time-limit ${timelimit} /sdcard/${name}.mp4 &
pid=$!
sleep 178
kill -9 ${pid}
sleep 1
adb pull /sdcard/${name}.mp4

