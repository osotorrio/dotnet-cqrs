using System.Threading.Tasks;

namespace DotNet.Cqrs.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
