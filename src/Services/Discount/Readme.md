# Discount gRPC

| Method (gRPC)  | Request URI           | Use Cases                     |
| -------------- | --------------------- | ----------------------------- |
| GetDiscount    | GetDiscountRequest    | Get discount with productname |
| CreateDiscount | CreateDiscountRequest | Create discount               |
| UpdateDiscount | UpdateDiscountRequest | Update discount               |
| DeleteDiscount | DeleteDiscountRequest | Delete discount               |

## Application Layer

**Data Access Layer**
Performs database operations
We will use Entity Framework Core as ORM for database operations.

**Business Logic Layer**
Implement business logic.
Process data gathered from Data Access Layer, we do not use Data Access Layer direction.

**Presentation Layer**
Show data to user.
Transmit data from Business Layer to user.

This is the gRPC proto definitions.
