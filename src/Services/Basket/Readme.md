# Basket API

## REST Endpoints

| **Method** | **Request URI**      | **Use Cases**             |
| ---------- | -------------------- | ------------------------- |
| GET        | `/basket/{userName}` | Get basket w/ username    |
| POST       | `/basket/{username}` | Store basket w/ username  |
| DELETE     | `/basket/{userName}` | Delete basket w/ username |
| POST       | `/basket/checkout`   | Checkout basket           |

## Redis Caching

#### Proxy Pattern

Use Redis to implement Cache-aside Pattern / Cache invalidation

#### Decorator Pattern

We develop Cached Respository and Decorate using `Scrutor` package
