#!/bin/bash

az account list

# chnage active Azure subscription
az account set --subscription <SUBSCRIPTION_ID>

RG_NAME=rg-apim-sample
AZ_REGION=westeurope
# create a new Azure Resource Group
az group create -n $RG_NAME -l $AZ_REGION --tags app='Azure API Management Sample'

APIM_NAME=apim-sample-2020

# check availability of the name for the desired Azure API Management instance
az apim check-name -n $APIM_NAME

# create a new Azure API Management instance
az apim create -n $APIM_NAME -g $RG_NAME -l $AZ_REGION --publisher-name "Thinktecture AG" --publisher-email office@thinktecture.com --sku-name Developer --sku-capacity 1 --tags app='Azure API Management Sample'