docker build -t suriyakalivardhan/cookbookwebservice:v0 .
docker run -d -p 5000:80 --name cookbookwebapi suriyakalivardhan/cookbookwebservice:v0

curl http://localhost:5000/api/Employees | jq
docker push suriyakalivardhan/cookbookwebservice:v0