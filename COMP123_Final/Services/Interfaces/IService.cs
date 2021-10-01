using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP123_Final.Models;

namespace COMP123_Final.Services
{
    public interface IService<T>
    {
        void Save(T obj);
        List<T> Load();
        void Update(T obj);
        void Delete(T obj);
    }
}
