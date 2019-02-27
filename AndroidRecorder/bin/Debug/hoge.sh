#!/bin/sh

source ~/.bash_profile

adb shell screenrecord --time-limit 10 /sdcard/AndroidRecorder/hoge.mp4 &
pid=$!

sleep 3
kill ${pid}