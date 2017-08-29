using AutoMapper;
using BLL.MapFolder;

namespace WebApplication3.Mapping
{
    public class MyMapper : IGenericMapper
    {
        public TD Map<T, TD>(T value)
        {
            return Mapper.Map<T, TD>(value);
        }
    }
}