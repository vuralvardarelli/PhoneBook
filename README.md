# PhoneBook

## Installation
1. Download repository from here.
2. Please go to solution folder and type to cmd "docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d". This will create orchestrated container and start all needed services.
3. Go to "localhost:9997" for the API gateway. At "localhost:9998" Repository Service will be running. At "localhost:9999" Report Service will be running. 

## Tech Stack
* .NET 5
* Microservices Architecture
* Docker (TBD)
* Redis
* Elasticsearch & Serilog
* RabbitMQ
* Postgres & EF Core w/ Code-First Approach
* CQRS Pattern
* FluentValidation
* AutoMapper
* Swagger

## Downsides
* NUnit & Moq (TBD)
