using BusinessLayer.Implementation;
using CommonLayer.CommonModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiLayer.Controllers
{
    public class CourseSessionController : ApiController
    {
        private readonly CourseSessionBs _courseSessionBs;
        private readonly CourseSessionModel _courseSessionModel;
        private readonly CourseBs _courseBs;
        APIResponseModel apiResponse;

        public CourseSessionController()
        {
            _courseBs = new CourseBs();
            _courseSessionBs = new CourseSessionBs();
            _courseSessionModel = new CourseSessionModel();
            apiResponse = new APIResponseModel();
        }
        [HttpGet]
        public IHttpActionResult GetAllCourses()
        {
            var res = _courseBs.CourseList().ToList();
            if (res != null)
            {
                apiResponse.Data = res;
                apiResponse.IsSuccess = true;
                apiResponse.Message = "Events List Found";
            }
            else
            {

                apiResponse.IsSuccess = false;
                apiResponse.Message = "Not Events List Found";

            }

            return Ok(apiResponse);
        }
        [HttpGet]
        public IHttpActionResult GetCourseSession(Int64 topicID)
        {
            CourseSessionModel model = new CourseSessionModel();
            var sessionData = _courseSessionBs.GetById(topicID);
            if (sessionData == null)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "No Session Found";
            }
            model.Topic = sessionData.Topic;
            model.VideoLink = sessionData.VideoLink;
            model.AudioLink = sessionData.AudioLink;
            model.Document1 = sessionData.Document1 != null ? ConfigurationManager.AppSettings["BaseUrl"] + "/Documents" + "/" + sessionData.Document1 : string.Empty;
            model.Document2 = sessionData.Document2 != null ? ConfigurationManager.AppSettings["BaseUrl"] + "/Documents" + "/" + sessionData.Document2 : string.Empty;
            model.CourseID = sessionData.CourseID;
            model.Id = sessionData.Id;


            apiResponse.IsSuccess = true;
            apiResponse.Data = model;
            return Ok(apiResponse);
        }

        [HttpGet]
        public IHttpActionResult SendCourseSessionForNewUser(string deviceID)
        {
            _courseSessionBs.SendCourseSessionForUser(deviceID);
            apiResponse.IsSuccess = true;
            return Ok(apiResponse);
        }


        [HttpGet]
        public IHttpActionResult SendAllCourseSessions()
        {
            var res = _courseSessionBs.SendCourseSessionForNewUser().ToList();
            if (res != null)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Data = res;
                apiResponse.Message = "List of Sessions Found";
            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Data = res;
                apiResponse.Message = "No Records Found";
            }
            return Ok(apiResponse);
        }
    }
}
