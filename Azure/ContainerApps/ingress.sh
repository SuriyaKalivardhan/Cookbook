LOG_ANALYTICS_WORKSPACE_CLIENT_ID=$(az monitor log-analytics workspace show --query customerId -n suriyakacaloganalytics -o tsv)
LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET=$(az monitor log-analytics workspace get-shared-keys --query primarySharedKey -n suriyakacaloganalytics -o tsv)
echo $LOG_ANALYTICS_WORKSPACE_CLIENT_ID
echo $LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET
az containerapp env create --name ingressacaenv --logs-workspace-id $LOG_ANALYTICS_WORKSPACE_CLIENT_ID --logs-workspace-key $LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET

az containerapp create --name ingressacaapp1 --environment ingressacaenv --image docker.io/suriyakalivardhan/cookbookwebservice:v2 --ingress external --target-port 80