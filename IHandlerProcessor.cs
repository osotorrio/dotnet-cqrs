using System;
using System.Threading.Tasks;
using DotNet.Cqrs.Commands;
using DotNet.Cqrs.Queries;

namespace DotNet.Cqrs
{
    // TODO: Probably it is best to split this on 2 (SRP). Or maybe just have a processor only for queries.
    public interface IHandlerProcessor
    {
        Task ProcessAsync(ICommand command);

        Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query);
    }

    public class HandlerProcessor : IHandlerProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public HandlerProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ProcessAsync(ICommand command)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            dynamic handler = _serviceProvider.GetService(handlerType);

            await handler.ExecuteAsync((dynamic)command);
        }

        public async Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = _serviceProvider.GetService(handlerType);

            return await handler.ExecuteAsync((dynamic)query);
        }
    }
}
