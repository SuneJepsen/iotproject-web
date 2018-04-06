using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Domain;

namespace Library.Job.Task.Abstract
{
    public interface ITask
    {
        void Work();
        List<Measurement> Done();
    }
}
