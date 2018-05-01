using System.Collections.Generic;
using DataLayer.Domain;
using DataLayer.Facade;

namespace WebApi.Access.ServiceLayer
{
    public class InferredDataService
    {
        private IFacade _facade;
        public InferredDataService(IFacade facade)
        {
            _facade = facade;
        }
        public List<Measurement> GetDataForCurrentDay()
        {
            return _facade.GetAllInferredData(); ;
        }




    }
}