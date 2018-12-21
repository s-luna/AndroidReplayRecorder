#!/bin/sh

source ~/.bash_profile

# echo "Start Recording..."
echo $!
time=10

# name=$(date +%Y%m%d_%H-%M-%S)

currentdir=$(pwd)


while true
do
    name=$(date +%Y%m%d_%H-%M-%S)
    # echo ${name}
    # bash ./record ${name} ${time} &
    bash ${currentdir}/../../../record ${name} ${time} &
    sleep ${time}
done



