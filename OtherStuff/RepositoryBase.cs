using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace DotNet.Cqrs
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> 
        where TEntity : IEntity
    {
        private readonly string _connectionString;

        protected RepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task Save(TEntity entity)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var insert = GetSqlStatement();

                await db.ExecuteAsync(insert, GetSqlParameters(entity));
            }
        }

        public async Task Update(TEntity entity)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var update = GetUpdateSqlStatement();

                await db.ExecuteAsync(update, GetUpdateSqlParameters(entity));
            }
        }
        protected abstract string GetSqlStatement();

        protected abstract object GetSqlParameters(TEntity entity);

        protected abstract string GetUpdateSqlStatement();
        protected abstract object GetUpdateSqlParameters(TEntity entity);
    }
}