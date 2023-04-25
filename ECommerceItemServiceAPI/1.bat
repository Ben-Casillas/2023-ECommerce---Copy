docker stop ItemServiceAPI
docker rm ItemServiceAPI
docker rmi itemserviceapi:1
docker build -t itemserviceapi:1 .
docker run -d -p 5051:8080 --name ItemServiceAPI --net e-commerce itemserviceapi:1