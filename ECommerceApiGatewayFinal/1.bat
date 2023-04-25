CLS

docker stop ECommerceAPIGatewayOcelot
docker rm ECommerceAPIGatewayOcelot
docker rmi ecommerceapigatewayocelot:1
docker build --no-cache -t ecommerceapigatewayocelot:1 .
docker run -d -p 5041:80 --name ECommerceAPIGatewayOcelot --net e-commerce ecommerceapigatewayocelot:1