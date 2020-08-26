#!/bin/bash

RG_NAME=rg-apim-sample
APIM_NAME=apim-sample-2020 

# import customers API
az apim api import -n $APIM_NAME -g $RG_NAME --specification-format OpenApi --specification-url https://as-apim-2020-customers.azurewebsites.net/swagger/v1/swagger.json --path customers --api-id customers --display-name "Customers API" --api-type http --service-url https://as-apim-2020-customers.azurewebsites.net/

# import products API
az apim api import -n $APIM_NAME -g $RG_NAME --specification-format OpenApi --specification-url https://as-apim-2020-products.azurewebsites.net/swagger/v1/swagger.json --path products --api-id products --display-name "Products API"--api-type http --service-url https://as-apim-2020-products.azurewebsites.net/