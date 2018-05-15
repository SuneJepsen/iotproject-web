using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using DataLayer.Domain;
using DataLayer.Facade;
using DataLayer.Helper.HandshakeHelper;
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
  
            //_inferredDataService = new InferredDataService(new FacadeData(new HandShakeHelperSaveToFile(@"..\DataLayer\Settings\handshake.json")));
            _inferredDataService = new InferredDataService(new FacadeData(new HandShakeHelperSaveToFB()));
        }

        /// <summary>
        /// The measurement with the passed guid will not be included in the return result
        /// If the whole list should be returned, then pass an empty guid as: /api/iot/GetInferredData?guid=
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public DeviceData GetInferredData(Guid? guid)
        {

            return _inferredDataService.GetInferredDataForCurrentDay(guid);
        }
        /// <summary>
        /// The measurement with the passed guid will not be included in the return result
        /// Ex: /api/iot/GetCopyData?doorGuid=6b3d3b0c-3644-4b22-b40e-fd84b7794c21&floorGuid=d160eb6f-a71a-4eca-bf99-6dc65b557b3f
        /// Ex. If the whole list should be returned, then pass an empty guid as: /api/iot/GetCopyData?doorGuid=&floorGuid=
        /// </summary>
        /// <param name="doorGuid"></param>
        /// <param name="floorGuid"></param>
        /// <returns></returns>
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