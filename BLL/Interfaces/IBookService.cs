using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IBookService
    {
        void Delete(int id);
        BookEntity Get(int id);
        List<BookEntity> GetMany();
        void Update(BookEntity model);
        void Create(BookEntity model);
        BookEntity GetBook(string name);
    }
}
