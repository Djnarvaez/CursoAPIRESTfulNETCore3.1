using ProductWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWEB.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAllAsync(string url);
        Task<T> GetAsync(string url, int id);
        Task<ModelStateError> CreateAsync(string url, T entity);
        Task<ModelStateError> UpdateAsync(string url, T entity);
        Task<ModelStateError> DeleteAsync(string url, int id);
    }
}
