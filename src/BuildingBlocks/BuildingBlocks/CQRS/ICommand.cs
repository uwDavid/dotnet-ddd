using MediatR;

namespace BuildingBlocks.CQRS;

// ICommand that returns a Response
public interface ICommand<out TResponse> : IRequest<TResponse>
{

}

// ICommand that doesn't return a Response
public interface ICommand : ICommand<Unit>
{

}