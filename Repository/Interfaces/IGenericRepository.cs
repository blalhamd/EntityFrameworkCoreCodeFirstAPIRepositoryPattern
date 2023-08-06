
using System.Linq.Expressions;

namespace MiddelLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        Task<IQueryable<T>> GetAll(string[] includes=null);

        Task<T> GetById(object id);

        Task<IQueryable<T>> FindBy(Expression<Func<T, bool>> match, string[] includes);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Update(T entity);
        
        void delete(T entity);
        
        void Save();


    }
}
