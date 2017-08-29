using BLL.Entities;
using BLL.Interfaces;
using BLL.IRepo;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _repository;

        public AccountService(IUserRepository repository)
        {
            _repository = repository;
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
    

    public bool CheckUser(string name, string password)
        {
            var t = _repository.CheckUser(name, password);
            return t;
        }

        public bool CheckUserName(UserEntity model)
        {
            var name = model.Name;
            var t = _repository.CheckUserName(name);
            return t;
        }
    }
}