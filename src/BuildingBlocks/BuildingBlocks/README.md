# Building Blocks Classlib

We create a classlib to house all library dependencies.
Benefits:

- One place to manage all library dependencies
- Microservices only need to reference this one classlib

## Library Dependencies

- MediatR for CQRS
- Carter for API Endpoints
- Marten for PostgreSQL interaction
- Mapster for Object Maping
- FluentValidation for Input Validation

## CQRS Abstraction

We also create an abstraction for Command and Query to help implement CQRS.

MediatR Pattern
`IRequestHandler<Request, Response>`
`IRequest<Response>`

Command Abstraction
IRequestHandler: `ICommandHandler<Request, Response>`
IRequest: `ICommand<Response>` : `IRequest<Response>`

See `/CQRS` folder
