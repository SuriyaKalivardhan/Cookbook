#az configure -d subscription=921496dc-987f-410f-bd57-426eb2611356 group=suriyak-wf location=westus2

az extension add --name containerapp --upgrade
az provider register --namespace Microsoft.App
az provider register --namespace Microsoft.OperationalInsights

az configure -d subscription=ea4faa5b-5e44-4236-91f6-5483d5b17d14 group=suriyak-aca-wus2 location=westus2
az group create -n suriyak-aca-wus2 --tags owner=suriyak

az monitor log-analytics workspace create --workspace-name suriyakacaloganalytics
LOG_ANALYTICS_WORKSPACE_CLIENT_ID=$(az monitor log-analytics workspace show --query customerId -n suriyakacaloganalytics -o tsv)
LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET=$(az monitor log-analytics workspace get-shared-keys --query primarySharedKey -n suriyakacaloganalytics -o tsv)

echo $LOG_ANALYTICS_WORKSPACE_CLIENT_ID 
echo $LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET