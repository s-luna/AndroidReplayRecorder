#!/bin/sh
curl 'http://local.com:3000/users'
source ~/.bash_profile

adb pull /sdcard/${1}
