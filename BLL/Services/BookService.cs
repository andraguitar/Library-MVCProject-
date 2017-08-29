using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using BLL.Interfaces;
using BLL.IRepo;

namespace BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }
        
        public void Create(BookEntity model)
        {
            _repository.Create(model);
        }

        public BookEntity GetBook(string name)
        {
            var book =_repository.GetBook(name);
            return book;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public BookEntity Get(int id)
        {
            BookEntity bookDb = _repository.Get(id);
            return bookDb;
        }

        public List<BookEntity> GetMany()
        {
            var listBook = _repository.GetMany();
            return listBook.ToList();
        }

        public void Update(BookEntity model)
        {
           _repository.Update(model);             
        }
    }
}