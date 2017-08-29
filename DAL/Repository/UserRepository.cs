using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Entities;
using BLL.IRepo;
using DAL.Models;


namespace DAL.Repository
{
    public class UserRepository : Repository<UserEntity, Users>, IUserRepository
    {
        public UserRepository(Library1Entities9 context) : base(context)
        {
        }

        public override  List<UserEntity> GetMany() 
        {
            var list = DbSet.ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Users, UserEntity>());
            var listDto = Mapper.Map<List<Users>, List<UserEntity>>(list);
            
            return listDto;
        }

        public override void Update(UserEntity model)
        {
            var id = model.Id;
            var user = DbSet.FirstOrDefault(x => x.Id == id);
            if (user == null) return;
            user.Email = model.Email;
            user.Age = model.Age;
            user.Name = model.Name;
            user.Password = model.Password;
            Db.SaveChanges();
        }

        public bool CheckUser(string name, string password)
        {
            var user = DbSet.FirstOrDefault(x => x.Name == name && x.Password == password);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckUserName(string name)
        {
            var user = DbSet.FirstOrDefault(x => x.Name == name);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public int CountOfNames(UserEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
