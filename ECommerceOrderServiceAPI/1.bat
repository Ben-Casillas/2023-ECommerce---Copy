CLS

docker stop ECommerceOrderServiceAPI
docker rm ECommerceOrderServiceAPI
docker rmi ecommerceorderserviceapi:1
docker build -t ecommerceorderserviceapi:1 .
docker run -d -p 8083:80 --name ECommerceOrderServiceAPI --net e-commerce ecommerceorderserviceapi:1