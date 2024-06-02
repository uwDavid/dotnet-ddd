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
