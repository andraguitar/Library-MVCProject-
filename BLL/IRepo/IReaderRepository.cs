using BLL.Entities;

namespace BLL.IRepo
{
    public interface IReaderRepository : IRepository<ReaderEntity> 
    {
        ReaderEntity GetReader(string name);
    }
}
