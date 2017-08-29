using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL.Entities;
using BLL.IRepo;
using Dapper;



namespace DALwhithDapper.DapperRepository
{
    public class ReaderDapRepository : IReaderRepository
    {
        private readonly IDbConnection _db;

        public ReaderDapRepository(IDbConnection db)
        {
            _db = db;
        }
        
        public List<ReaderEntity> GetMany()
        {
            var readers = _db.Query<ReaderEntity>("SELECT * FROM Readers").ToList();

            return readers;
        }
        
        public ReaderEntity Get(int id)
        {
            var reader = _db.Query<ReaderEntity>("SELECT * FROM Readers WHERE Id = @id", new { id }).FirstOrDefault();

            return reader;
        }

        public void Create(ReaderEntity model)
        {
            var sqlQuery = "INSERT INTO Readers (Fname, Lname, Adress, Email, Telephone) VALUES(@Fname, @LName, @Adress, @Email, @Telephone)";
            _db.Execute(sqlQuery, model);
        }

        public void Update(ReaderEntity model)
        {
            var sqlQuery = "UPDATE Readers SET Fname = @Fname, Lname = @Lname, Adress = @Adress, Email = @Email, Telephone = @Telephone WHERE Id = @Id";
            _db.Execute(sqlQuery, model);
        }

        public void Delete(int id)
        {
            var sqlQuery = "DELETE FROM Readers WHERE Id = @id";
            _db.Execute(sqlQuery, new { id });
        }

        public ReaderEntity GetReader(string name)
        {
            var reader = _db.Query<ReaderEntity>("SELECT * FROM Readers WHERE Fname = @name", new { name }).FirstOrDefault();

            return reader;
        }
    }
}
