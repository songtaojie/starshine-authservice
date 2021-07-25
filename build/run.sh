#!/bin/bash

curpwd=$(pwd)
git pull
cd /home/songtaojie/git/hx-identity-server/
git pull
cd $curpwd

source ./.env

#�������
function build(){
    echo -e -n "\033[33m please confirm whether to start processing,y=>yes? \033[0m"
    read confirm
    if [ $confirm == "y" ]
    then
        docker-compose up --build -d --force-recreate $DOCKER_REGISTRY_IMAGE
        echo -e "\033[32m build finished, good luck \033[0m"
    else
    echo -e "\033[31m you canceled operation \033[0m" 
    fi
}

#��������
function up(){
    echo -e -n "\033[33m please confirm whether to start processing,y=>yes? \033[0m"
    read confirm
    if [ $confirm == "y" ]
    then
        docker-compose up  -d  $DOCKER_REGISTRY_IMAGE
        echo -e "\033[32m up finished, good luck \033[0m"
    else
    echo -e "\033[31m you canceled operation \033[0m" 
    fi
}

#��������
function push(){
    pushnamespace=songtaojie_centos
    echo -e -n "\033[33m please confirm whether to push,y=>yes? \033[0m"
    read INPUT_STRING
    if [ $INPUT_STRING != y ];
    then
        exit 0
    fi

    docker tag $DOCKER_REGISTRY/$DOCKER_REGISTRY_NAMESPACE/$DOCKER_REGISTRY_IMAGE:latest $DOCKER_REGISTRY/$DOCKER_REGISTRY_NAMESPACE/$DOCKER_REGISTRY_IMAGE:latest
    docker push $DOCKER_REGISTRY/$DOCKER_REGISTRY_NAMESPACE/$DOCKER_REGISTRY_IMAGE:latest
    echo -e "\033[32m push finished, good luck \033[0m"
}

echo "1 Build Services"
echo "5 Up Services"
echo -e "10 Push Images to songtaojie_centos- \e[33mWarning\e[0m:this operation will replace the docker images on Ali Docker Images of songtaojie_centos."
while :
do
  read INPUT_STRING
  case $INPUT_STRING in
	1)
	    build
		break 
		;;
	5)
	    up
		break 
		;;
    10)
		push
    break
    ;;
	*)
		echo "Invalid input."
		;;
  esac
done
