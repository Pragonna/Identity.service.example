using System.Net;
using EventBus.EventBus.Base.Abstraction;
using Shared.DataAccess.Common;
using Shared.DataAccess.Repositories;

namespace Shared.DataAccess.Commands;

public abstract class CommandHandler<TRequest, TResponse>(
    IUnitOfWork unitOfWork,
    IEventBus eventBus = null) : ICommandHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : class, new()
{

    public async Task<Result<TResponse, Error>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var response = await ExecuteAsync((TRequest)request, cancellationToken);
        if (!response.IsSuccess)
        {
            throw new ArgumentException(HttpStatusCode.BadRequest.ToString(), nameof(request));
        }

        await unitOfWork.CommitAsync();

        if (response.HasEvent)
            await DispatchEventAsync(response, cancellationToken);

        return response;
    }
    public abstract Task<Result<TResponse, Error>> ExecuteAsync(TRequest request, CancellationToken cancellationToken);
    private async Task DispatchEventAsync(Result<TResponse, Error> result, CancellationToken cancellationToken)
    {
        if (eventBus is null) throw new ArgumentNullException(nameof(eventBus));

        foreach (var @event in result.Events)
        {
            await eventBus.Publish(@event);
        }
    }

}