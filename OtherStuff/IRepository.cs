using System.Threading.Tasks;

namespace DotNet.Cqrs
{
    public interface IRepository<in TEntity> where TEntity : IEntity
    {
        Task Save(TEntity entity);

        Task Update(TEntity entity);
    }
}