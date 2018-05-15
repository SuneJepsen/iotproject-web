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
        /// Ex: /api/iot/GetCopyData?endDate=2018-05-15 21:58:21.200 - please look at the database for end dates
        /// Ex. If the whole list should be returned, then pass an empty endDate as: /api/iot/GetCopyData?endDate=
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DeviceData> GetCopyData(DateTime? endDate)
        {
            return _inferredDataService.GetCopyDataForCurrentDay(endDate);
        }
        [HttpGet]
        public string Move()
        {
            new Scheduler().Run(); ;
            return "move";
        }
    }
}