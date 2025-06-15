using MediatR;
using Shared.DataAccess.Common;

namespace Shared.DataAccess.Commands;

public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse, Error>>
    where TRequest : ICommand<TResponse>
    where TResponse : class, new()
{
}