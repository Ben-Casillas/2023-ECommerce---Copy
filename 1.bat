CLS

docker-compose down

docker rmi itemserviceapi:1
docker rmi ecommercebasketserviceapi:1
docker rmi ecommerceorderserviceapi:1
docker rmi ecommerceapigatewayocelot:1

docker-compose up --build -d
