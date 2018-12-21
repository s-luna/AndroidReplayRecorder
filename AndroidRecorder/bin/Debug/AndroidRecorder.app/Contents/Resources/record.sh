#!/bin/sh

# sleep 5
# curl 'http://local.com:3000/users'

timelimit=10
name=$(date +%Y%m%d_%H-%M-%S)

source ~/.bash_profile

adb shell screenrecord --time-limit ${1} /sdcard/${2} &
echo $!
sleep ${1}
# adb pull /sdcard/${2}
# adb shell rm /sdcard/${2}
