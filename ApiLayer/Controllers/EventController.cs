using BusinessLayer.Implementation;
using CommonLayer.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiLayer.Controllers
{
    public class EventsController : ApiController
    {

        private readonly EventRequestBs _EventRequestBs;
        APIResponseModel apiResponse;

        public EventsController()
        {
            _EventRequestBs = new EventRequestBs();
            apiResponse = new APIResponseModel();
        }

        [HttpGet]
        public IHttpActionResult GetAllEvents()
        {
            var res = _EventRequestBs.GetAllEvents().ToList();


            if (res != null)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Data = res;
                apiResponse.Message = "List of Events Found";
            }
            else
            {
                apiResponse.IsSuccess = false;
                // apiResponse.Data = res;
                apiResponse.Message = "No Events Found";
            }

            return Ok(apiResponse);
        }


        [HttpGet]
        public IHttpActionResult GetEventDetails(int EventId)
        {
            var res = _EventRequestBs.GetAllEventDetails().Where(x => x.EventRequestId == EventId);
            if (res != null)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Data = res;
                apiResponse.Message = "List of Events Found";
            }
            else
            {
                apiResponse.IsSuccess = false;
                // apiResponse.Data = res;
                apiResponse.Message = "No Events Found";
            }

            return Ok(apiResponse);

        }

    }
}
