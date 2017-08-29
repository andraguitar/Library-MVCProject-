using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL.Entities;
using BLL.IRepo;
using Dapper;

namespace DALwhithDapper.DapperRepository
{
    public class BookDapRepository :  IBookRepository
    {
        private readonly IDbConnection _db;

        public BookDapRepository(IDbConnection db)
        {
            _db = db;
        }

        public BookEntity GetBook(string name)
        {
            var book = _db.Query<BookEntity>("SELECT * FROM Books WHERE Name = @name", new { name }).FirstOrDefault();

            return book;
        }

        public void Create(BookEntity model)
        {
            var sqlQuery = "INSERT INTO Books (Name, Author,Cost) VALUES(@Name, @Author, @Cost)";
            _db.Execute(sqlQuery, model);
        }

        public void Delete(int id)
        {
            var sqlQuery = "DELETE FROM Books WHERE Id = @id";
            _db.Execute(sqlQuery, new { id });
        }
        
        public List<BookEntity> GetMany()
        {
            var books = _db.Query<BookEntity>("SELECT * FROM Books").ToList();

            return books;
        }
       
        public void Update(BookEntity model)
        {
            var sqlQuery = "UPDATE Books SET Name = @Name, Author = @Author, Cost = @Cost WHERE Id = @Id";
            _db.Execute(sqlQuery, model);
        }
        
        public BookEntity Get(int id)
        {
            var book = _db.Query<BookEntity>("SELECT * FROM Books WHERE Id = @id", new { id }).FirstOrDefault();

            return book;
        }
    }
}
