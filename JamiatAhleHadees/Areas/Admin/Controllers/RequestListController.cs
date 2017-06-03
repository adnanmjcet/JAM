using BusinessLayer.Implementation;
using CommonLayer.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace JamiatAhleHadees.Areas.Admin.Controllers
{
    [Authorize(Roles = "Ameer")]
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
            var RequestList = _RequestSubmitBs.RequestSubmitList().Where(X => X.IsApproved == null).ToList().ToPagedList(page ?? 1, 10);
            return View(RequestList);
        }
        
        public ActionResult ApprovedRequestList(int? page)
        {
            var RequestList = _RequestSubmitBs.AmeerApprovedList().Where(X => X.IsApproved == true).ToList().ToPagedList(page ?? 1, 10);
            return View(RequestList);
        }

        public ActionResult RejectedRequestList(int? page)
        {
            var RequestList = _RequestSubmitBs.AmeerRejectedList().Where(X => X.IsApproved == false).ToList().ToPagedList(page ?? 1, 10);
            return View(RequestList);
        }
        public ActionResult GetDetailsbyId(int Id, int RequestTypeId)
        {
            var ReturnResult = "";
            if (RequestTypeId == 1)
            {

                ReturnResult = "GetMasjidConstructionDetails";
            }
            else if (RequestTypeId == 6)
            {

                ReturnResult = "MadarsaLandRequestDetails";
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


            return RedirectToAction(ReturnResult, new { id = Id });
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
            var res = new MasjidConstructionRequestBs().GetByRequestSubmitIdAmeer(Id);
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();

            var isapproved = new RequestSubmitBs().RequestSubmitList().Where(x => x.Id == Id).FirstOrDefault();


            res.PanelCommentList = res.PanelCommentList.Where(x => x.UserTypeId == 6 || x.UserTypeId == 7 || x.UserTypeId == 8 || x.UserTypeId == 12).ToList();
            if (isapproved != null)
            {
                if (isapproved.IsApproved == true)
                {
                    res.IsAmeerApproved = true;
                }
                else if (isapproved.IsApproved == false)
                {
                    res.IsAmeerApproved = false;

                }

            }

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
            var res = new MadarsaLandRequestBs().GetByRequestSubmitIdAmeer(Id);
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();

            var isapproved = new RequestSubmitBs().RequestSubmitList().Where(x => x.Id == Id).FirstOrDefault();
            res.PanelCommentList = res.PanelCommentList.Where(x => x.UserTypeId == 6 || x.UserTypeId == 7 || x.UserTypeId == 8 || x.UserTypeId == 12).ToList();
            if (isapproved != null)
            {
                if (isapproved.IsApproved == true)
                {
                    res.IsAmeerApproved = true;
                }
                else if (isapproved.IsApproved == false)
                {
                    res.IsAmeerApproved = false;

                }

            }

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
            var res = new MasjidExtensionRequestBs().GetByRequestSubmitIdAmeer(Id);
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();

            var isapproved = new RequestSubmitBs().RequestSubmitList().Where(x => x.Id == Id).FirstOrDefault();


            res.PanelCommentList = res.PanelCommentList.Where(x => x.UserTypeId == 6 || x.UserTypeId == 7 || x.UserTypeId == 8 || x.UserTypeId == 12).ToList();
            if (isapproved != null)
            {
                if (isapproved.IsApproved == true)
                {
                    res.IsAmeerApproved = true;
                }
                else if (isapproved.IsApproved == false)
                {
                    res.IsAmeerApproved = false;

                }

            }

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
            var res = new MasjidLandRequestBs().GetByRequestSubmitIdAmeer(Id);
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();

            var isapproved = new RequestSubmitBs().RequestSubmitList().Where(x => x.Id == Id).FirstOrDefault();


            res.PanelCommentList = res.PanelCommentList.Where(x => x.UserTypeId == 6 || x.UserTypeId == 7 || x.UserTypeId == 8 || x.UserTypeId == 12).ToList();
            if (isapproved != null)
            {
                if (isapproved.IsApproved == true)
                {
                    res.IsAmeerApproved = true;
                }
                else if (isapproved.IsApproved == false)
                {
                    res.IsAmeerApproved = false;

                }

            }

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
            var res = new MasjidRenovationRequestBs().GetByRequestSubmitIdAmeer(Id);
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();

            var isapproved = new RequestSubmitBs().RequestSubmitList().Where(x => x.Id == Id).FirstOrDefault();


            res.PanelCommentList = res.PanelCommentList.Where(x => x.UserTypeId == 6 || x.UserTypeId == 7 || x.UserTypeId == 8 || x.UserTypeId == 12).ToList();
            if (isapproved != null)
            {
                if (isapproved.IsApproved == true)
                {
                    res.IsAmeerApproved = true;
                }
                else if (isapproved.IsApproved == false)
                {
                    res.IsAmeerApproved = false;

                }

            }

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
            var res = new NewMadarsaOperationsRequestBs().GetByRequestSubmitIdAmeer(Id);
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();

            var isapproved = new RequestSubmitBs().RequestSubmitList().Where(x => x.Id == Id).FirstOrDefault();


            res.PanelCommentList = res.PanelCommentList.Where(x => x.UserTypeId == 6 || x.UserTypeId == 7 || x.UserTypeId == 8 || x.UserTypeId == 12).ToList();
            if (isapproved != null)
            {
                if (isapproved.IsApproved == true)
                {
                    res.IsAmeerApproved = true;
                }
                else if (isapproved.IsApproved == false)
                {
                    res.IsAmeerApproved = false;

                }

            }

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
            var res = new MadarsaExtensionRequestBs().GetByRequestSubmitIdAmeer(Id);
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();

            var isapproved = new RequestSubmitBs().RequestSubmitList().Where(x => x.Id == Id).FirstOrDefault();


            res.PanelCommentList = res.PanelCommentList.Where(x => x.UserTypeId == 6 || x.UserTypeId == 7 || x.UserTypeId == 8 || x.UserTypeId == 12).ToList();
            if (isapproved != null)
            {
                if (isapproved.IsApproved == true)
                {
                    res.IsAmeerApproved = true;
                }
                else if (isapproved.IsApproved == false)
                {
                    res.IsAmeerApproved = false;

                }

            }

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
            var res = new ExistingMadarsaOperationsRequestBs().GetByRequestSubmitIdAmeer(Id);
            res.ApprovedList = res.ApprovedList.Where(x => x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5).ToList();

            var isapproved = new RequestSubmitBs().RequestSubmitList().Where(x => x.Id == Id).FirstOrDefault();


            res.PanelCommentList = res.PanelCommentList.Where(x => x.UserTypeId == 6 || x.UserTypeId == 7 || x.UserTypeId == 8 || x.UserTypeId == 12).ToList();
            if (isapproved != null)
            {
                if (isapproved.IsApproved == true)
                {
                    res.IsAmeerApproved = true;
                }
                else if (isapproved.IsApproved == false)
                {
                    res.IsAmeerApproved = false;

                }

            }

            return View(res);
        }
    }
}