docker stop ECommerceBasketServiceAPI
docker rm ECommerceBasketServiceAPI
docker rmi ecommercebasketserviceapi:1
docker build -t ecommercebasketserviceapi:1 .
docker run -d -p 8082:8080 --name BasketServiceAPI --net e-commerce ecommercebasketserviceapi:1
