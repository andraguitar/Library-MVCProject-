using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL.Entities;
using BLL.IRepo;
using Dapper;


namespace DALwhithDapper.DapperRepository
{
    public class UserDapRepository : IUserRepository
    {
        private readonly IDbConnection _db;

        public UserDapRepository(IDbConnection db)
        {
            _db = db;
        }
        
        public bool CheckUser(string name, string password)
        {
            var user = _db.Query<int>("SELECT COUNT(*) FROM Users WHERE Name = @name AND Password = @password", new {name,password }).FirstOrDefault();
            if (user != 0)
            {
                return true;
            }

            return false;
        }

        public bool CheckUserName(string name)
        {
            var user = _db.Query<int>("SELECT COUNT(*) FROM Users WHERE Name = @name", new { name }).FirstOrDefault();
            if (user != 0)
            {
                return true;
            }

            return false;
        }

        public UserEntity Get(int id)
        {
            var user = _db.Query<UserEntity>("SELECT Id, Name, Password, Email, Age, RoleId as 'Role' FROM Users WHERE Id = @id", new { id }).FirstOrDefault();

            return user;
        }

        public List<UserEntity> GetMany()
        {
            var users = _db.Query<UserEntity>("SELECT Id, Name, Password, Email, Age, RoleId as 'Role' FROM Users").ToList();

            return users;
        }

        public void Create(UserEntity model)
        {
            var sqlQuery = "INSERT INTO Users (Email, Password, Name, Age, RoleId) VALUES(@Email, @Password, @Name, @Age, @Role)";       
            _db.Execute(sqlQuery,  model);
        }

        public void Update(UserEntity model)
        {
            var sqlQuery = "UPDATE Users SET Name=@Name, Email = @Email, Password = @Password, Age = @Age, RoleId = @Role WHERE Id = @Id";
            _db.Execute(sqlQuery, model);
        }

        public int CountOfNames(UserEntity model)
        {
            var count = _db.Query<int>("SELECT COUNT(*) FROM Users WHERE Name = @name AND Id !=@Id ", new { model.Name, model.Id }).FirstOrDefault();
            return count;
        }

        public void Delete(int id)
        {
            var sqlQuery = "DELETE FROM Users WHERE Id = @id";
            _db.Execute(sqlQuery, new { id });
        }
    }
}
