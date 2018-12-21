#!/bin/sh


curl 'http://local.com:3000/users'

currentdir=$(pwd)

bash ${currentdir}/../../../main.sh &
echo $!

