using System.Collections.Generic;
using BLL.Entities;

namespace BLL.IRepo
{
    public interface IOrderRepository : IRepository<OrderEntity>
    {
        List<string> GetBooks(int id);
        List<string> CountOfBooksOnWeek();
        bool IsBookExist(int id);
    }

    public interface IRepository<T> where T : class
    {
        void Delete(int id);
        T Get(int id);
        List<T> GetMany();
        void Create(T model);
        void Update(T model);
    }
}
