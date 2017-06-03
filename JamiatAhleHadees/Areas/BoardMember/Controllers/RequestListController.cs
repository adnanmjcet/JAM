using BusinessLayer.Implementation;
using CommonLayer.CommonModels;
using JamiatAhleHadees.Areas.User.Controllers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamiatAhleHadees.Areas.BoardMember.Controllers
{
    [Authorize(Roles = "BoardMember,Ameee")]
    public class RequestListController : Controller
    {

        private RequestSubmitModel _RequestSubmitModel;
        private readonly RequestSubmitBs _RequestSubmitBs;
        private readonly MasjidConstructionRequestBs _MasjidConstructionRequestBs;

        public RequestListController()
        {
            _RequestSubmitModel = new RequestSubmitModel();
            _RequestSubmitBs = new RequestSubmitBs();
            _MasjidConstructionRequestBs = new MasjidConstructionRequestBs();
        }
        // GET: BoardMember/RequestList
        public ActionResult Index(int? page)
        {
            var RequestList = _RequestSubmitBs.RequestSubmitList().Where(m => m.IsApproved == null).ToList().ToPagedList(page ?? 1, 10);
            return View(RequestList);
        }
        //return Json(new { data = RequestList }, JsonRequestBehavior.AllowGet);

        public ActionResult GetDetailsbyId(int Id, int RequestTypeId)
        {
            var ReturnResult = "";
            if (RequestTypeId == 1)
            {

                ReturnResult = "GetMasjidConstructionDetails";
            }

            else if (RequestTypeId == 2)
            {

                ReturnResult = "GetMasjidExtensionDetails";
            }


            else if (RequestTypeId == 3)
            {

                ReturnResult = "GetMasjidLandRequestDetails";
            }

            else if (RequestTypeId == 4)
            {

                ReturnResult = "GetMasjidRenovationRequestDetails";
            }

            else if (RequestTypeId == 5)
            {

                ReturnResult = "ExistingMadarasaOeprationDetails";
            }

            else if (RequestTypeId == 6)
            {

                ReturnResult = "MadarsaLandRequestDetails";
            }
            else if (RequestTypeId == 7)
            {

                ReturnResult = "NewMadarsaOperationRequestDetails";
            }
            else if (RequestTypeId == 8)
            {

                ReturnResult = "MadarsaExtensionRequestDetails";
            }
            else if (RequestTypeId==9)
            {
                ReturnResult = "GetEventRequestDetails";
            }



            return RedirectToAction(ReturnResult, new { id = Id });
        }


        public ActionResult GetEventRequestDetails(int Id)
        {
            var userId = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().UserTypeId;

            var res = new EventRequestBs().GetByRequestSubmitIdBoard(Id, Convert.ToInt32(userId));
            EventRequestBs eve = new EventRequestBs();
            var EventRequestDetails = eve.GetAllEventDetails().Where(x => x.EventRequestId == res.Id).ToList();
            res.EventRequestDetailList = EventRequestDetails;
            return View(res);
        }
        public ActionResult GetMasjidConstructionDetails(int Id)
        {
            if (TempData["AlreadyExistsInPanel"] != null)
            {
                TempData["message"] = TempData["AlreadyExistsInPanel"];
            }

            else
            {
                if (TempData["agree"] != null)
                {
                    ViewBag.AlertMessage = TempData["agree"];
                }
            }
            var userId = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().UserTypeId;
            var res = new MasjidConstructionRequestBs().GetByRequestSubmitIdBoard(Id, Convert.ToInt32(userId));
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();
            return View(res);
        }


        public ActionResult GetMasjidExtensionDetails(int Id)
        {
            if (TempData["AlreadyExistsInPanel"] != null)
            {
                TempData["message"] = TempData["AlreadyExistsInPanel"];
            }

            else
            {
                if (TempData["agree"] != null)
                {
                    ViewBag.AlertMessage = TempData["agree"];
                }
            }
            var userId = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().UserTypeId;
            var res = new MasjidExtensionRequestBs().GetByRequestSubmitIdBoard(Id, Convert.ToInt32(userId));
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();
            return View(res);
        }


        public ActionResult GetMasjidLandRequestDetails(int Id)
        {
            if (TempData["AlreadyExistsInPanel"] != null)
            {
                TempData["message"] = TempData["AlreadyExistsInPanel"];
            }

            else
            {
                if (TempData["agree"] != null)
                {
                    ViewBag.AlertMessage = TempData["agree"];
                }
            }
            var userId = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().UserTypeId;
            var res = new MasjidLandRequestBs().GetByRequestSubmitIdBoard(Id, Convert.ToInt32(userId));
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();
            return View(res);
        }


        public ActionResult GetMasjidRenovationRequestDetails(int Id)
        {
            if (TempData["AlreadyExistsInPanel"] != null)
            {
                TempData["message"] = TempData["AlreadyExistsInPanel"];
            }

            else
            {
                if (TempData["agree"] != null)
                {
                    ViewBag.AlertMessage = TempData["agree"];
                }
            }
            var userId = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().UserTypeId;
            var res = new MasjidRenovationRequestBs().GetByRequestSubmitIdBoard(Id, Convert.ToInt32(userId));
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();
            return View(res);
        }


        public ActionResult MadarsaLandRequestDetails(int Id)
        {
            if (TempData["AlreadyExistsInPanel"] != null)
            {
                TempData["message"] = TempData["AlreadyExistsInPanel"];
            }

            else
            {
                if (TempData["agree"] != null)
                {
                    ViewBag.AlertMessage = TempData["agree"];
                }
            }
            var userId = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().UserTypeId;
            var res = new MadarsaLandRequestBs().GetByRequestSubmitIdBoard(Id, Convert.ToInt32(userId));
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();
            return View(res);
        }
        public ActionResult NewMadarsaOperationRequestDetails(int Id)
        {
            if (TempData["AlreadyExistsInPanel"] != null)
            {
                TempData["message"] = TempData["AlreadyExistsInPanel"];
            }

            else
            {
                if (TempData["agree"] != null)
                {
                    ViewBag.AlertMessage = TempData["agree"];
                }
            }
            var userId = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().UserTypeId;
            var res = new NewMadarsaOperationsRequestBs().GetByRequestSubmitIdBoard(Id, Convert.ToInt32(userId));
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();
            return View(res);
        }

        public ActionResult MadarsaExtensionRequestDetails(int Id)
        {
            if (TempData["AlreadyExistsInPanel"] != null)
            {
                TempData["message"] = TempData["AlreadyExistsInPanel"];
            }

            else
            {
                if (TempData["agree"] != null)
                {
                    ViewBag.AlertMessage = TempData["agree"];
                }
            }
            var userId = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().UserTypeId;
            var res = new MadarsaExtensionRequestBs().GetByRequestSubmitIdBoard(Id, Convert.ToInt32(userId));
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();
            return View(res);
        }

        public ActionResult ExistingMadarasaOeprationDetails(int Id)
        {
            if (TempData["AlreadyExistsInPanel"] != null)
            {
                TempData["message"] = TempData["AlreadyExistsInPanel"];
            }

            else
            {
                if (TempData["agree"] != null)
                {
                    ViewBag.AlertMessage = TempData["agree"];
                }
            }
            var userId = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().UserTypeId;
            var res = new ExistingMadarsaOperationsRequestBs().GetByRequestSubmitIdBoard(Id, Convert.ToInt32(userId));
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();
            return View(res);
        }
    }
}