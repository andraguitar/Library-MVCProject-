using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IReaderService
    {
        void Delete(int id);
        ReaderEntity Get(int id);
        ReaderEntity GetReader(string name);
        List<ReaderEntity> GetMany();
        void Update(ReaderEntity model);
        void Create(ReaderEntity model);
    }
}
