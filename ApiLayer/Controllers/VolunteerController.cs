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
    public class VolunteerController : ApiController
    {
        private readonly VolunteerModel _VolunteerModel;
        private readonly VolunteerBs _VolunteerBs;
        private readonly MasjidBs _MasjidBs;
        APIResponseModel apiResponse;

        public VolunteerController()
        {
            apiResponse = new APIResponseModel();
            _VolunteerModel = new VolunteerModel();
            _VolunteerBs = new VolunteerBs();
            _MasjidBs = new MasjidBs();
        }
        [HttpPost]
        public IHttpActionResult AddVolunteer(VolunteerModel model)
        {
            int res = 0;
            if (model != null)
            {
                res = _VolunteerBs.Save(model);
            }
            if (res > 0)
            {

                apiResponse.IsSuccess = true;
                apiResponse.Message = "Volunter Added Sucessfully";
                apiResponse.Data = res;
            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Failed";
                apiResponse.Data = res;
            }

            return Ok(apiResponse);

        }

        [HttpGet]
        public IEnumerable<ZoneModel> ZoneList()
        {

            var res = _MasjidBs.ZoneList().ToList();

            return res;

        }
    }
}
