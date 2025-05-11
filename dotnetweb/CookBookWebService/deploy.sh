docker build -t suriyakalivardhan/cookbookwebservice:v2 .
docker run -d -p 5000:80 --name cookbookwebapi suriyakalivardhan/cookbookwebservice:v2

curl http://localhost:5000/api/Employees | jq
docker push suriyakalivardhan/cookbookwebservice:v2


#az configure -d subscription=ea4faa5b-5e44-4236-91f6-5483d5b17d14 group=suriyak-wus2poc location=westus2
