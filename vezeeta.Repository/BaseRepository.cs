using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Dtos;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repository;

namespace vezeeta.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task <T> GetByIdAsync(int id)
        {
            return  await _context.Set<T>().FindAsync(id);
        }

        public TResult GetById<TResult>(Expression<Func<T, bool>> criteria, Expression<Func<T, TResult>> selector)
        {
            var result = _context.Set<T>().Where(criteria).Select(selector).ToList().SingleOrDefault();
            return result;
        }



        public async Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<T, bool>> criteria, Expression<Func<T, TResult>> selector)
        {
            var result = await _context.Set<T>().Where(criteria).Select(selector).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<TResult>> GetAll<TResult>(int page, int pagesize , Expression<Func<T, TResult>> selector) 
        {
            var result = await _context.Set<T>().Select(selector)
                        .Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<T, bool>> criteria, int page, int pagesize, Expression<Func<T, TResult>> selector)
        {
            var result = await _context.Set<T>().Where(criteria).Select(selector)
                        .Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<TResult>> GetAllSearch<TResult>(int page, int pagesize, Expression<Func<T, TResult>> selector, Expression<Func<TResult, bool>> criteria)
        {
            var result = await _context.Set<T>().Select(selector).Where(criteria)
                        .Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<TResult>> GetAllSearch<TResult>(Expression<Func<T, bool>> criteria1, int page, int pagesize, Expression<Func<T, TResult>> selector, Expression<Func<TResult, bool>> criteria2)
        {
            var result = await _context.Set<T>().Where(criteria1).Select(selector).Where(criteria2)
                        .Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();
            return result;
        }



        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return  await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
        }



        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
           await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }


        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }

        public bool Any(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Any(criteria);
        }

     
    }

}





