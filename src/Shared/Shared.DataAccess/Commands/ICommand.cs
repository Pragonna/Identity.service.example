using MediatR;
using Shared.DataAccess.Common;

namespace Shared.DataAccess.Commands;

public interface ICommand<TResponse> : IRequest<Result<TResponse, Error>>
    where TResponse : class, new()
{
}