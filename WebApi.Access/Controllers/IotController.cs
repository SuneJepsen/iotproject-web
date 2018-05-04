using System;
using System.Collections.Generic;
using System.Web.Http;
using DataLayer.Domain;
using DataLayer.Facade;
using Library.Job;
using WebApi.Access.Domain;
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
        [HttpGet]
        public DeviceData GetInferredData(Guid? guid)
        {
            return _inferredDataService.GetInferredDataForCurrentDay(guid);
        }
        [HttpGet]
        public IEnumerable<DeviceData> GetCopyData(Guid? doorGuid, Guid? floorGuid)
        {
            return _inferredDataService.GetCopyDataForCurrentDay(doorGuid, floorGuid);
        }
        [HttpGet]
        public string Move()
        {
            new Scheduler().Run(); ;
            return "move";
        }
    }
}