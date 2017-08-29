using BLL.Entities;
using BLL.IRepo;
using DAL.Models;


namespace DAL.Repository
{
    public class BookRepository :  Repository<BookEntity, Books>, IBookRepository
    {
        public BookRepository(Library1Entities9 context) : base(context)
        {
        }

        public BookEntity GetBook(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
