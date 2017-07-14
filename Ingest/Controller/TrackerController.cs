using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ingest.Controller
{
    class TrackerController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "Welcome to Sheepishly 1.0.0 - The Combleat Sheep Tracking Suite";
        }

        [HttpGet]
        [Route("sheep/{sheepId}/lastlocation")]
        public async Task<object> LastLocation(Guid sheepId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("sheep/{sheepId}/lastseen")]
        public async Task<DateTime> LastSeen(Guid sheedId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("locations")]
        public async Task<bool> Log(Object location)
        {
            throw new NotImplementedException();
        }




    }
}
