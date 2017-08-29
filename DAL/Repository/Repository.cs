using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using BLL.IRepo;
using DAL.Models;


namespace DAL.Repository
{
    public class Repository<T, TDto> : IRepository<T> where T : class
        where TDto : class
    {
        protected readonly Library1Entities9 Db;
        protected readonly DbSet<TDto> DbSet;

        public Repository(Library1Entities9 context)
        {
            Db = context;
            DbSet = context.Set<TDto>();
        }

        public void Create(T model)
        {
            Mapper.Initialize(c => c.CreateMap<T, TDto>());
            var modelDto = Mapper.Map<T, TDto>(model);
            DbSet.Add(modelDto);
            Db.SaveChanges();
        }
        
        public void Delete(int id)
        {
            var models = DbSet.Find(id);
            if (models != null) DbSet.Remove(models);
            Db.SaveChanges();
        }

        public T Get(int id)
        {
            var modelDb = Db.Set<TDto>().Find(id);
            Mapper.Initialize(cfg => cfg.CreateMap<TDto, T>());
            var modelDto = Mapper.Map<TDto, T>(modelDb);

            return modelDto;
        }

        public List<int> GetBooks(int id)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> GetMany()
        {
            var list = DbSet.ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<TDto, T>());
            var listDto = Mapper.Map<List<TDto>, List<T>>(list);

            return listDto;
        }

        public virtual void Update(T model)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<T, TDto>());
            var modelDto = Mapper.Map<T, TDto>(model);
            DbSet.AddOrUpdate(modelDto);
            Db.SaveChanges();
        }
    }
}