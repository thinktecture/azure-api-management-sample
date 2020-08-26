#!/bin/bash

# create an Azure App Service Plan
AZ_REGION=westeurope
RG_NAME=rg-apim-sample
APP_SERVICE_PLAN_NAME=asp-apim-sample-2020

az appservice plan create -n $APP_SERVICE_PLAN_NAME -g $RG_NAME -l $AZ_REGION --sku B1 --tags app='Azure API Management Sample'

APP_SERVICE_CUSTOMERS_NAME=as-apim-2020-customers
APP_SERVICE_PRODUCTS_NAME=as-apim-2020-products

# Create an Azure App Service for the CustomerAPI
az webapp create -n $APP_SERVICE_CUSTOMERS_NAME -p $APP_SERVICE_PLAN_NAME -g $RG_NAME
# Create an Azure App Service for the ProductsAPI
az webapp create -n $APP_SERVICE_PRODUCTS_NAME -p $APP_SERVICE_PLAN_NAME -g $RG_NAME

# enable run from package by setting the corresponding app setting on Azure App Services
az webapp config appsettings set -name $APP_SERVICE_CUSTOMERS_NAME -g $RG_NAME --settings WEBSITE_RUN_FROM_PACKAGE="1" 
az webapp config appsettings set -name $APP_SERVICE_PRODUCTS_NAME -g $RG_NAME --settings WEBSITE_RUN_FROM_PACKAGE="1"

# download zips from GitHub
wget https://github.com/thinktecture/azure-api-management-sample/releases/download/v0.0.1/CustomersAPI.zip
wget https://github.com/thinktecture/azure-api-management-sample/releases/download/v0.0.1/ProductsAPI.zip

# configure zip release on GitHub
az webapp deployment source config-zip -n $APP_SERVICE_CUSTOMERS_NAME -g $RG_NAME --src CustomresAPI.zip
az webapp deployment source config-zip -n $APP_SERVICE_PRODUCTS_NAME -g $RG_NAME --src ProductsAPI.zip
