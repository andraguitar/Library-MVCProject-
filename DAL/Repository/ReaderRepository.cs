using System;
using System.Linq;
using BLL.Entities;
using BLL.IRepo;
using DAL.Models;


namespace DAL.Repository
{
    public class ReaderRepository: Repository<ReaderEntity, Readers>, IReaderRepository
        
    {
       public ReaderRepository(Library1Entities9 context) : base(context)
        {
        }
        public int CheckReader(string name)
        {
            Readers reader = DbSet.FirstOrDefault(x => x.Fname == name);
            if (reader != null)
            {
                return reader.Id;
            }
            return -1;
        }

        public ReaderEntity GetReader(string name)
        {
            throw new NotImplementedException();
        }
    }
}
