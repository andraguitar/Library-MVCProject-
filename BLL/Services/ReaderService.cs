using System.Collections.Generic;
using BLL.Entities;
using BLL.Interfaces;
using BLL.IRepo;

namespace BLL.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _repository;

        public ReaderService(IReaderRepository repository)
        {
            _repository = repository;
        }

        public int CheckReader(string name)
        {
            var id = _repository.GetReader(name).Id;
            return id;
        }

        public void Create(ReaderEntity model)
        {
            var reader = new ReaderEntity
            {
                Fname = model.Fname,
                Lname = model.Lname,
                Adress = model.Adress,
                Email = model.Email,
                Telephone = model.Telephone
            };
            _repository.Create(reader);

        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public ReaderEntity Get(int id)
        {
            ReaderEntity readerDb = _repository.Get(id);
            return readerDb;
        }

        public ReaderEntity GetReader(string name)
        {
            ReaderEntity readerDb = _repository.GetReader(name);
            return readerDb;
        }

        public List<ReaderEntity> GetMany()
        {
            var listReader = _repository.GetMany();
            var model = new List<ReaderEntity>();
            foreach (var reader in listReader)
            {
                model.Add(reader);
            }
            return model;
        }

        public void Update(ReaderEntity model)
        {
              _repository.Update(model);
          
        }
    }
}