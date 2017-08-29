using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        bool CheckUser(string name, string password);
        void Create(UserEntity model);
        bool CheckUserName(UserEntity model);
    }
}
