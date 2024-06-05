# Order Service

This service will implement DDD using entities, value objects, and aggregates.
Additionally, it will take advantage of Domain Events and Integration Events.

## Domain

Primary domian models are `Order` and `OrderItem`
`Order` will be the aggregate root that contains all order info.

| Method | Request URI                   | Use Cases                |
| ------ | ----------------------------- | ------------------------ |
| GET    | /orders                       | Get orders               |
| GET    | /orders/{orderName}           | Gte orders by OrderName  |
| GET    | /orders/customer/{customerId} | Get orders by customer   |
| POST   | /orders                       | Create new order         |
| PUT    | /orders                       | Update an existing order |
| DELETE | /order/{id}                   | Delete order with Id     |

## Infrastructure Layer

Maps Domain Objects to EF Core Entities
Migrates tables into SQL Database
Raise & Dispatch Domain Events

## Adding Migrations

Run below command in `/Order` folder to save migration folder to `Ordering.Infrastructure` layer.

VS Studio:

`Add-Migration InitialCreate -OutputDir Data/Migrations -Project Ordering.Infrastructure -StartupProject Ordering.API`

VS Code:

`dotnet ef migrations add InitialCreate -o Data/Migrations -p Ordering.Infrastructure -s Ordering.API`

## Update Database

In `/Order` folder to run update database command
`dotnet ef database update -p Ordering.Infrastructure -s Ordering.API`
