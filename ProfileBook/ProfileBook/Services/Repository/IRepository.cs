using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    public interface IRepository<T> where T : IEntity, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllWithCommand(string sql);
        Task<T> FindWithCommand(string sql);
        Task Add(T element);
        Task Remove(T element);
        Task Update(T element);
        Task AddOrUpdata(T element);
    }
}
