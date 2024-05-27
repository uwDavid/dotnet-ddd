# .NET Microservices using DDD

This project demonstrates some best practices building microservices in .NET 8 using `DDD` and `Vertical Slice` architecture.

## Note on Docker

Visual Studio has built-in integration with Docker. However, it has some internal configurations to make it work.
To run `docker build` without Visual Studio support:

1. Run `docker build` from `/src` folder, where the `.sln` file is saved
2. Use `-f` option to specify where the Dockerfile is located

For example, to build the Catalog API:

1. Current working directory is `/src`
2. Run `docker build -t catalog-api -f Service/Catalog/Catalog.API/Dockerfile .`

## Postman

Postman test routes are set up in `EShopMicroservices.postman_collection.json`
Import this into Postman and modify ports as necessary.
