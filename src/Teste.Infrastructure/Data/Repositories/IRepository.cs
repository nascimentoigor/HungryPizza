using System.Linq;
using System.Threading.Tasks;
using Teste.Domain.Entities;

namespace Teste.Infrastructure.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Delete(TEntity entity);
        Task<bool> Exists(int id);
        Task<TEntity> Insert(TEntity entity);
        IQueryable<TEntity> AsQueryable();
        Task<TEntity> SelectById(int id);
        Task Update(TEntity entity);
        Task Commit();
    }
}