using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MVM.Infrastructure.Repositories
{
    public interface IGenericRepository<T, A, C> where A : class where T : class where C : DbContext
    {
        List<A> GetAll();
        List<A> GetAll(string includeProperties);
        List<A> FindBy(Expression<Func<T, bool>> predicate);
        List<A> FindBy(Expression<Func<T, bool>> predicate, string includeProperties);
        A Add(A contract);
        void AddRange(List<A> contracts);
        void Delete(Expression<Func<T, bool>> predicate);
        void DeleteRange(Expression<Func<T, bool>> predicate);
        void Edit(Expression<Func<T, bool>> predicate, A contract, Func<T, A, T> selector);
        void UpdateRange(List<A> contracts);
        void Save();
    }
}
