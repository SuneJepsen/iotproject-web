using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using DataLayer.Domain;
using DataLayer.Repository;
using DataLayer.Repository.Abstract;
using DataLayer.Repository.Concrete;
using Newtonsoft.Json.Linq;

namespace WebApi.Access.Controllers
{
    public class ValuesController : ApiController
    {
        private IRepository<Measurement> _repo;

        public ValuesController()
        {
            _repo = new FirebaseDb<Measurement>(FirebaseConnectionString.InferredData);
        }

        // GET api/values
        public IEnumerable<Measurement> Get()
        {
            return _repo.GetAll();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

       
    }
}
