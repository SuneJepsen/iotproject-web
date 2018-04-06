using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Domain;

namespace DataLayer.Repository.Abstract
{
    public interface IRepository
    {
        List<Measurement> GetAll();
        void Save(List<Measurement>measurements);

        void DeleteAll();
    }
}
