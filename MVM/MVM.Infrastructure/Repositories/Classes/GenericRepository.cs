using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MVM.Infrastructure.Repositories
{
    public class GenericRepository<T, A, C> : IGenericRepository<T, A, C> where A : class where T : class where C : DbContext
    {
        #region Properties

        private readonly C Entities;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors 

        public GenericRepository(IMapper mapper, C _entities)
        {
            Entities = _entities;
            _mapper = mapper;
        }

        #endregion

        #region Public overridable Methods

        public virtual List<A> GetAll(string includeProperties)
        {
            IQueryable<T> query = Entities.Set<T>();

            foreach (string includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return _mapper.Map<List<T>, List<A>>(query.ToList());
        }

        public virtual List<A> GetAll()
        {
            IQueryable<T> query = Entities.Set<T>();
            return _mapper.Map<List<T>, List<A>>(query.ToList());
        }

        public List<A> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = Entities.Set<T>().Where(predicate);
            return _mapper.Map<List<T>, List<A>>(query.ToList());
        }

        public List<A> FindBy(Expression<Func<T, bool>> predicate, string includeProperties)
        {

            IQueryable<T> query = Entities.Set<T>().Where(predicate);

            foreach (string includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return _mapper.Map<List<T>, List<A>>(query.ToList());
        }

        public virtual A Add(A contract)
        {
            T entity = _mapper.Map<A, T>(contract);
            T newEntity = Entities.Set<T>().Add(entity).Entity;
            Save();
            return _mapper.Map<T, A>(newEntity);
        }

        public virtual void AddRange(List<A> contracts)
        {
            List<T> entities = _mapper.Map<List<A>, List<T>>(contracts);
            Entities.Set<T>().AddRange(entities);
            Save();
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            T entity = Entities.Set<T>().FirstOrDefault(predicate);
            if (!(entity is null))
            {
                Entities.Set<T>().Attach(entity);
                Entities.Set<T>().Remove(entity);
                Save();
            }
        }

        public virtual void DeleteRange(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> entities = Entities.Set<T>().Where(predicate);
            Entities.Set<T>().RemoveRange(entities);
            Save();
        }

        public virtual void Edit(Expression<Func<T, bool>> predicate, A contract, Func<T, A, T> selector)
        {
            T entity = Entities.Set<T>().Where(predicate).FirstOrDefault();
            if (!(entity is null))
            {
                entity = selector(entity, contract);
                Entities.Entry(entity).State = EntityState.Modified;
                Entities.Set<T>().Update(entity);
                Save();
            }
        }

        public virtual void UpdateRange(List<A> contracts)
        {
            List<T> entities = _mapper.Map<List<A>, List<T>>(contracts);
            Entities.Set<T>().UpdateRange(entities);
            Save();
        }

        public virtual void Save()
        {
            Entities.SaveChanges();
        }

        #endregion
    }
}
