using BLL.Entities;

namespace BLL.IRepo
{
    public interface IBookRepository : IRepository<BookEntity> 
    {
        BookEntity GetBook(string name);
    }
}
