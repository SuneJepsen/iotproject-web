using System;
using System.Collections.Generic;
using System.Web.Http;
using DataLayer.Domain;
using DataLayer.Facade;
using WebApi.Access.ServiceLayer;

namespace WebApi.Access.Controllers
{

    // https://webapiaccess20180420013135.azurewebsites.net/api/iot
    public class IotController : ApiController
    {
        private InferredDataService _inferredDataService;
        public IotController()
        {
            _inferredDataService = new InferredDataService(new FacadeData());
        }
        // GET api/<controller>
        public IEnumerable<Measurement> Get()
        {
            return _inferredDataService.GetDataForCurrentDay();
        }
    }
}