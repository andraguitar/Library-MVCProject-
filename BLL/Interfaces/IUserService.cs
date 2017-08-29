using System.Collections.Generic;
using BLL.Entities;


namespace BLL.Interfaces
{
    public interface IUserService
    {
        void Delete(int id);
        UserEntity Get(int id);
        List<UserEntity> GetMany();
        void Update(UserEntity model);
        void Create(UserEntity model);
        bool CheckUserName(UserEntity model);
        int CountOfNames(UserEntity model);
    }
}
