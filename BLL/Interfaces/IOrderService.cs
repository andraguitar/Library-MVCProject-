using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        void Delete(int id);
        OrderEntity Get(int id);
        List<OrderEntity> GetMany();
        void Update(OrderEntity model);
        void Create(OrderEntity model);
        List<string> GetBooksWhithReaderId(int id);
        List<string> CountOfBooksOnWeek();
        bool IsBookExist(int id);
    }
}