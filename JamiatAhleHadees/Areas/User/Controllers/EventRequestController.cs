using BusinessLayer.Implementation;
using CommonLayer.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamiatAhleHadees.Areas.User.Controllers
{
    public class EventRequestController : Controller
    {
        private readonly EventRequestBs _EventRequestBs;

        private readonly EventRequestModel _EventRequestModel;

        public EventRequestController()
        {
            _EventRequestBs = new EventRequestBs();
            _EventRequestModel = new EventRequestModel();
        }

        // GET: User/EventRequest
        public ActionResult Index()
        {
            _EventRequestModel.EventRequestModelList = _EventRequestBs.GetAllEvents().ToList();
            return View(_EventRequestModel);
        }

        public ActionResult CreateEvent(int? id)
        {
            EventRequestModel model = new EventRequestModel();
            if (id != null)
            {
                var Varial = _EventRequestBs.GetById(Convert.ToInt32(id));

                return View(Varial);

            }
            else
            {
                ViewBag.EventTypeName = new SelectList(_EventRequestBs.GetAllEventMasterList(), "Id", "Name");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateEvent(EventRequestModel model)
        {
            long i = 0;

            if (model != null)
            {
                //if (File != null)
                //{
                //    model.Poster = new byte[File.ContentLength];
                //    File.InputStream.Read(model.Poster, 0, File.ContentLength);
                //}
                model.UserId = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Id;
                i = _EventRequestBs.Save(model);
            }


            if (i > 0)
            {
                TempData["msg"] = "Save Successfully";
            }
            else
            {
                TempData["msg"] = "Error while saving data";
            }

            return RedirectToAction("Index", "EventRequest", new { area = "User" });

        }

        public ActionResult GetEventDetails(int id)
        {
            var EventsList = _EventRequestBs.GetAllEventDetails().Where(x => x.EventRequestId == id);
            TempData["EventId"] = id;
            return View(EventsList);
        }


        public ActionResult AddSpeakers(int? id)
        {
            if (id != null)
            {
                var res = _EventRequestBs.GetEventDetailsId(Convert.ToInt32(id));
                return View(res);
            }
            EventRequestDetailModel model = new EventRequestDetailModel();
            if (TempData["EventId"] != null)
                model.EventRequestId = Convert.ToInt32(TempData["EventId"]);

            return View(model);
        }


        [HttpPost]
        public ActionResult AddSpeakers(FormCollection form)
        {

            List<EventRequestDetailModel> model = new List<EventRequestDetailModel>();

            var keys = form.AllKeys.Where(x => x.StartsWith("Topic")).ToList();

            var obj = new EventRequestDetailModel();
            foreach (var item in keys)
            {
                var currentKeyNum = item.Replace("Topic", "");
                obj.Id = Convert.ToInt32(form["Id"]);
                obj.Topic =  form["Topic" + currentKeyNum];
                obj.SpeakerName = form["SpeakerName" + currentKeyNum];
                obj.Date =Convert.ToDateTime(form["Date" + currentKeyNum]);
                obj.Time = form["Time" + currentKeyNum];
                obj.EventRequestId = Convert.ToInt32(form["EventRequestId"]);

                

                 _EventRequestBs.SaveEventDetails(obj);
            }
           EventRequestDetailModel res = new EventRequestDetailModel();
            return View(res);

            }
        

        }
}