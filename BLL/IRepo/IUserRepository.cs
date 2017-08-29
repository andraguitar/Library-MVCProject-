using BLL.Entities;

namespace BLL.IRepo
{
    public interface IUserRepository : IRepository<UserEntity> 
    {
        bool CheckUser(string name, string password);
        bool CheckUserName(string name);
        int CountOfNames(UserEntity model);
    }
}
