using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Category or any other generic model
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null); //retrieve all category where we are displaying all of them
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false); // retrive if only one category. Retrieve only one category and pass or display on screen.
        //out result will be a boolean

        void Add(T entity); // Create category
        void Remove(T entity); // Remove category
        void RemoveRange(IEnumerable<T> entitiy); // Remove multiple category

    }
}
