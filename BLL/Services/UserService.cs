using System.Collections.Generic;
using BLL.Entities;
using BLL.Interfaces;
using BLL.IRepo;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public UserEntity Get(int id)
        {
            UserEntity userDb = _repository.Get(id);

            return userDb;
        }

        public int CountOfNames(UserEntity model)
        {
            return _repository.CountOfNames(model);
        }

        public List<UserEntity> GetMany()
        {
            var listUser = _repository.GetMany();
            var model = new List<UserEntity>();
            foreach (var user in listUser)
            {
                model.Add(user);
            }

            return model;
        }

        public void Update(UserEntity model)
        {
            _repository.Update(model);
        }

        public bool CheckUserName(UserEntity model)
        {
            var name = model.Name;
            var result = _repository.CheckUserName(name);

            return result;
        }

        public void Create(UserEntity model)
        {
            var user = new UserEntity()
            {
                Name = model.Name,
                Password = model.Password,
                Age = model.Age,
                Role = model.Role,
                Email = model.Email,
                Id = model.Id
            };
            _repository.Create(user);
        }
    }
}

