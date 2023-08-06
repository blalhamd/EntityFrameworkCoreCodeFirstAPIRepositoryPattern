

using Microsoft.EntityFrameworkCore;
using MiddelLayer.APPDBCONTEXT;
using MiddelLayer.Interfaces;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MiddelLayer.EntitiesRepositories
{
    public class GenericRepositorie<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepositorie(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IQueryable<T>> GetAll(string[] includes=null)
        {
            IQueryable<T> query =  _context.Set<T>();
           
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
          
            return query;
        }

        public async Task<T> GetById(object id)
        {
             return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IQueryable<T>> FindBy(Expression<Func<T, bool>> match, string[] includes=null)
        {
            IQueryable<T> query =  _context.Set<T>().Where(match);

            foreach (var item in includes)
            {
                query=query.Include(item);
            }

            return query;
        }


        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);

        }


        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

     
        public void delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

      
    }
}

/*
 generate Class employee with fields id as pk and name and another class department has fields id and name and employee's id as fk and apply repository pattern With GenereicRepository will implement IGenericRepository and generete actions in controller for each class
 */

/*
 but when testing on postman and execute Get Action appear title and description but collection of employees appear null and too when execute get emplyee department appear null,how to overcome on this problem!
 */
