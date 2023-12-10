using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {

        Task<T> GetByIdAsync(int id);
        TResult GetById<TResult>(Expression<Func<T, bool>> criteria, Expression<Func<T, TResult>> selector);

        Task<IEnumerable<TResult>> GetAll<TResult>(int page, int pagesize, Expression<Func<T, TResult>> selector) ;
        Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<T, bool>> criteria, int page, int pagesize, Expression<Func<T, TResult>> selector);
        Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<T, bool>> criteria, Expression<Func<T, TResult>> selector);

        Task<IEnumerable<TResult>> GetAllSearch<TResult>(int page, int pagesize, Expression<Func<T, TResult>> selector, Expression<Func<TResult, bool>> criteria);
        Task<IEnumerable<TResult>> GetAllSearch<TResult>(Expression<Func<T, bool>> criteria1, int page, int pagesize, Expression<Func<T, TResult>> selector, Expression<Func<TResult, bool>> criteria2);


        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
      
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        T Update(T entity);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);

        bool Any(Expression<Func<T, bool>> criteria);

    }
}
