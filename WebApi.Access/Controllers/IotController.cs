using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer.Crypto;
using DataLayer.Domain;
using DataLayer.Repository;
using DataLayer.Repository.Abstract;
using DataLayer.Repository.Concrete;
using WebApi.Access.ServiceLayer;

namespace WebApi.Access.Controllers
{
    public class IotController : ApiController
    {
        private InferredDataService _inferredDataService;
        public IotController()
        {
            _inferredDataService = new InferredDataService(new FirebaseDb(string.Format(FirebaseConnectionString.InferredData, DateTime.Now.ToString("dd-MM-yyyy"))),new RijndaelManaged());
        }
        // GET api/<controller>
        public IEnumerable<Measurement> Get()
        {
            return _inferredDataService.GetDataForCurrentDay();
        }
    }
}