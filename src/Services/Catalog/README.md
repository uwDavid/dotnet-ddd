# Catalog API

## Entity Domain

Product has many Categories

## Application Use Cases

- List `Products` and `Categories`.
- Get `Product` with `ProductId`
- Get `Product` with `Category`
- CRUD operation on `Product`

## API Endpoints

| **Method** | **Request URI**      | **Use Cases**            |
| ---------- | -------------------- | ------------------------ |
| GET        | `/products`          | List all products        |
| GET        | `/products/{id}`     | Fetch a specific product |
| GET        | `/products/category` | Get products by category |
| POST       | `/products`          | Create a new product     |
| PUT        | `/products/{id}`     | Update a product         |
| DELETE     | `/products/{id}`     | Delete a product         |

## Data Store

2 options:

1. MongoDB - No-SQL database
2. PostgreSQL DB JSON Columns - PostgreSQL using Marten library

## Application Architecture

Implement using a **Vertical Slice Architecture**.
Organize folder structure on a feature-by-feature basis.

#### CQRS Pattern

Command Query Responsibility Segregation divides operations into `commands` (write) and `queries` (read).

#### Mediator Pattern

Facilitates object interaction through a 'mediator'.
Thus reduce direct dependencies and simplify communications.

## Library Dependencies

- MediatR for CQRS
- Carter for API Endpoints
- Marten for PostgreSQL interaction
- Mapster for Object Maping
- FluentValidation for Input Validation
