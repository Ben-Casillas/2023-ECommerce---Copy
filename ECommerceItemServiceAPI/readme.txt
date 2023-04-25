docker network create e-commerce
docker run -d -p 4747:27017 --name ItemsServiceDBMongo --net e-commerce mongo:latest