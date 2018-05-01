using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Domain;

namespace DataLayer.Repository.Abstract
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        void Save(List<T>measurements);

        void DeleteAll();
    }
}
