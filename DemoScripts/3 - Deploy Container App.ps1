# Set up our parameters
$RESOURCE_GROUP = "rg-container-apps-demo-01"
$LOCATION = "westeurope"
$LOG_ANALYTICS_WORKSPACE = "log-container-apps-demo-01"
$CONTAINERAPPS_ENVIRONMENT = "aca-container-apps-demo-01"

$LOG_ANALYTICS_WORKSPACE_CLIENT_ID = (az monitor log-analytics workspace show --query customerId -g $RESOURCE_GROUP -n $LOG_ANALYTICS_WORKSPACE --out tsv)
$LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET = (az monitor log-analytics workspace get-shared-keys --query primarySharedKey -g $RESOURCE_GROUP -n $LOG_ANALYTICS_WORKSPACE --out tsv)

# Deploy our container app environment
az containerapp env create `
  --name $CONTAINERAPPS_ENVIRONMENT `
  --resource-group $RESOURCE_GROUP `
  --logs-workspace-id $LOG_ANALYTICS_WORKSPACE_CLIENT_ID `
  --logs-workspace-key $LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET `
  --location $LOCATION

# Set ACR details
$REGISTRY_LOGIN_SERVER = "acrcontainerappsdemo01.azurecr.io"
$REGISTRY_USERNAME = "64336747-7d78-4fa9-aa92-da989f8f4e1e"
$REGISTRY_PASSWORD = ".wA7Q~plO.b0QK6_PRnZuqtXFvb4QgdRMol4F"

# Deploy our API for app integration
$CONTAINER_APP_NAME = "integration-api-01"
$CONTAINER_IMAGE_NAME = "integrationapi:latest"

az containerapp create `
  --name $CONTAINER_APP_NAME `
  --resource-group $RESOURCE_GROUP `
  --image "$REGISTRY_LOGIN_SERVER/$CONTAINER_IMAGE_NAME" `
  --environment $CONTAINERAPPS_ENVIRONMENT `
  --registry-login-server $REGISTRY_LOGIN_SERVER `
  --registry-username $REGISTRY_USERNAME `
  --registry-password $REGISTRY_PASSWORD `
  --target-port 80 `
  --ingress 'external' `
  --query configuration.ingress.fqdn

# Deploy our console app to ingest data
$CONTAINER_APP_NAME = "ingest-batch-app-01"
$CONTAINER_IMAGE_NAME = "ingestapidata:latest"

az containerapp create `
  --name $CONTAINER_APP_NAME `
  --resource-group $RESOURCE_GROUP `
  --image "$REGISTRY_LOGIN_SERVER/$CONTAINER_IMAGE_NAME" `
  --environment $CONTAINERAPPS_ENVIRONMENT `
  --registry-login-server $REGISTRY_LOGIN_SERVER `
  --registry-username $REGISTRY_USERNAME `
  --registry-password $REGISTRY_PASSWORD