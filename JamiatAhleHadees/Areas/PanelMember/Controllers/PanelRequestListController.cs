using BusinessLayer.Implementation;
using CommonLayer.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace JamiatAhleHadees.Areas.PanelMember.Controllers
{
    [Authorize(Roles = "Ameer,PanelMember")]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class PanelRequestListController : Controller
    {
        private RequestApproveModel _RequestApproveModel;
        private RequestSubmitModel _RequestSubmitModel;
        private readonly RequestSubmitBs _RequestSubmitBs;
        private readonly MasjidConstructionRequestBs _MasjidConstructionRequestBs;
        private readonly RequestApproveRejectBs _RequestApproveBs;
        private readonly UserRegistrationBs _UserRegistrationBs;
        public PanelRequestListController()
        {
            _RequestApproveModel = new RequestApproveModel();
            _RequestSubmitModel = new RequestSubmitModel();
            _RequestSubmitBs = new RequestSubmitBs();
            _MasjidConstructionRequestBs = new MasjidConstructionRequestBs();
            _RequestApproveBs = new RequestApproveRejectBs();
            _UserRegistrationBs = new UserRegistrationBs();
        }
        // GET: BoardMember/RequestList
        public ActionResult Index(int? page)
        {
            var RequestList = _RequestSubmitBs.RequestSubmitList().Where(m => m.IsApproved == null).ToList().ToPagedList(page ?? 1, 10);
            return View(RequestList);
        }
        public ActionResult ApprovedRequestList()
        {
            var RequestList = _RequestSubmitBs.AmeerRejectedList().Where(m => m.IsApproved == true).ToList();
            return View(RequestList);
        }

        public ActionResult RejectedRequestList()
        {
            var RequestList = _RequestSubmitBs.AmeerRejectedList().Where(m => m.IsApproved == false).ToList();
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
            var userDetails = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var useerTypeId = Convert.ToInt32(userDetails.UserTypeId);

            List<int> _listUserHeads = new List<int> { 11, 13, 14, 15 };

            bool IsPanelHeadUser = _listUserHeads.Contains(useerTypeId);
            var userId = Convert.ToInt32(userDetails.Id);
            var headUserTypeId = Convert.ToInt32(userDetails.MainUserType);
            var res = new MasjidConstructionRequestBs().GetByRequestId(Id, useerTypeId, userId);
            int IsPanelHead;
            bool checkForPanel = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Any();
            if (checkForPanel)
            {
                IsPanelHead = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelHead = 0;
            }

            int IsPanelMember;

            if ((new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Count != 0))
            {
                IsPanelMember = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelMember = 0;
            }
            //Check is user involved in any of the panels
            List<int> _listUser = _UserRegistrationBs.GetUserTypesByMainUserType(headUserTypeId).Select(m => (Int32)m.UserTypeId).ToList();

            //bool IsPanelInvolved = new PanelInvolveBs().IsPanelInvoled(_listUser);
            int IsPanelLiked = new RequestApproveRejectBs().IsPannelMemberLiked(_listUser);


            // List<int> PanelHeadUsers = new List<int> {6,7,8, 11,12, 13, 14, 15 };



            res.IsPanelInvolved = checkForPanel;
            res.IsPanelHeadUser = IsPanelHeadUser;
            res.IsPanelHead = IsPanelHead;
            res.IsPanelMember = IsPanelMember;

            return View(res);
        }

        public ActionResult GetMasjidExtensionDetails(int Id)
        {
            var userDetails = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var useerTypeId = Convert.ToInt32(userDetails.UserTypeId);

            List<int> _listUserHeads = new List<int> { 11, 13, 14, 15 };

            bool IsPanelHeadUser = _listUserHeads.Contains(useerTypeId);
            var userId = Convert.ToInt32(userDetails.Id);
            var headUserTypeId = Convert.ToInt32(userDetails.MainUserType);
            var res = new MasjidExtensionRequestBs().GetByRequestId(Id, useerTypeId, userId);
            int IsPanelHead;
            bool checkForPanel = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Any();
            if (checkForPanel)
            {
                IsPanelHead = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelHead = 0;
            }

            int IsPanelMember;

            if ((new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Count != 0))
            {
                IsPanelMember = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelMember = 0;
            }
            //Check is user involved in any of the panels
            List<int> _listUser = _UserRegistrationBs.GetUserTypesByMainUserType(headUserTypeId).Select(m => (Int32)m.UserTypeId).ToList();

            //bool IsPanelInvolved = new PanelInvolveBs().IsPanelInvoled(_listUser);
            int IsPanelLiked = new RequestApproveRejectBs().IsPannelMemberLiked(_listUser);


            // List<int> PanelHeadUsers = new List<int> {6,7,8, 11,12, 13, 14, 15 };



            res.IsPanelInvolved = checkForPanel;
            res.IsPanelHeadUser = IsPanelHeadUser;
            res.IsPanelHead = IsPanelHead;
            res.IsPanelMember = IsPanelMember;

            return View(res);
        }

        public ActionResult GetMasjidLandRequestDetails(int Id)
        {
            var userDetails = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var useerTypeId = Convert.ToInt32(userDetails.UserTypeId);

            List<int> _listUserHeads = new List<int> { 11, 13, 14, 15 };

            bool IsPanelHeadUser = _listUserHeads.Contains(useerTypeId);
            var userId = Convert.ToInt32(userDetails.Id);
            var headUserTypeId = Convert.ToInt32(userDetails.MainUserType);
            var res = new MasjidLandRequestBs().GetByRequestId(Id, useerTypeId, userId);
            int IsPanelHead;
            bool checkForPanel = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Any();
            if (checkForPanel)
            {
                IsPanelHead = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelHead = 0;
            }

            int IsPanelMember;

            if ((new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Count != 0))
            {
                IsPanelMember = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelMember = 0;
            }
            //Check is user involved in any of the panels
            List<int> _listUser = _UserRegistrationBs.GetUserTypesByMainUserType(headUserTypeId).Select(m => (Int32)m.UserTypeId).ToList();

            //bool IsPanelInvolved = new PanelInvolveBs().IsPanelInvoled(_listUser);
            int IsPanelLiked = new RequestApproveRejectBs().IsPannelMemberLiked(_listUser);


            // List<int> PanelHeadUsers = new List<int> {6,7,8, 11,12, 13, 14, 15 };



            res.IsPanelInvolved = checkForPanel;
            res.IsPanelHeadUser = IsPanelHeadUser;
            res.IsPanelHead = IsPanelHead;
            res.IsPanelMember = IsPanelMember;

            return View(res);
        }

        public ActionResult GetMasjidRenovationRequestDetails(int Id)
        {
            var userDetails = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var useerTypeId = Convert.ToInt32(userDetails.UserTypeId);

            List<int> _listUserHeads = new List<int> { 11, 13, 14, 15 };

            bool IsPanelHeadUser = _listUserHeads.Contains(useerTypeId);
            var userId = Convert.ToInt32(userDetails.Id);
            var headUserTypeId = Convert.ToInt32(userDetails.MainUserType);
            var res = new MasjidRenovationRequestBs().GetByRequestId(Id, useerTypeId, userId);
            int IsPanelHead;
            bool checkForPanel = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Any();
            if (checkForPanel)
            {
                IsPanelHead = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelHead = 0;
            }

            int IsPanelMember;

            if ((new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Count != 0))
            {
                IsPanelMember = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelMember = 0;
            }
            //Check is user involved in any of the panels
            List<int> _listUser = _UserRegistrationBs.GetUserTypesByMainUserType(headUserTypeId).Select(m => (Int32)m.UserTypeId).ToList();

            //bool IsPanelInvolved = new PanelInvolveBs().IsPanelInvoled(_listUser);
            int IsPanelLiked = new RequestApproveRejectBs().IsPannelMemberLiked(_listUser);


            // List<int> PanelHeadUsers = new List<int> {6,7,8, 11,12, 13, 14, 15 };



            res.IsPanelInvolved = checkForPanel;
            res.IsPanelHeadUser = IsPanelHeadUser;
            res.IsPanelHead = IsPanelHead;
            res.IsPanelMember = IsPanelMember;

            return View(res);
        }

        public ActionResult MadarsaLandRequestDetails(int Id)
        {
            var userDetails = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var useerTypeId = Convert.ToInt32(userDetails.UserTypeId);

            List<int> _listUserHeads = new List<int> { 11, 13, 14, 15 };

            bool IsPanelHeadUser = _listUserHeads.Contains(useerTypeId);
            var userId = Convert.ToInt32(userDetails.Id);
            var headUserTypeId = Convert.ToInt32(userDetails.MainUserType);
            var res = new MadarsaLandRequestBs().GetByRequestSubmitId(Id, useerTypeId, userId);
            int IsPanelHead;
            bool checkForPanel = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Any();
            if (checkForPanel)
            {
                IsPanelHead = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelHead = 0;
            }

            int IsPanelMember;

            if ((new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Count != 0))
            {
                IsPanelMember = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelMember = 0;
            }
            //Check is user involved in any of the panels
            List<int> _listUser = _UserRegistrationBs.GetUserTypesByMainUserType(headUserTypeId).Select(m => (Int32)m.UserTypeId).ToList();

            //bool IsPanelInvolved = new PanelInvolveBs().IsPanelInvoled(_listUser);
            int IsPanelLiked = new RequestApproveRejectBs().IsPannelMemberLiked(_listUser);


            // List<int> PanelHeadUsers = new List<int> {6,7,8, 11,12, 13, 14, 15 };



            res.IsPanelInvolved = checkForPanel;
            res.IsPanelHeadUser = IsPanelHeadUser;
            res.IsPanelHead = IsPanelHead;
            res.IsPanelMember = IsPanelMember;

            return View(res);
        }

        public ActionResult NewMadarsaOperationRequestDetails(int Id)
        {
            var userDetails = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var useerTypeId = Convert.ToInt32(userDetails.UserTypeId);

            List<int> _listUserHeads = new List<int> { 11, 13, 14, 15 };

            bool IsPanelHeadUser = _listUserHeads.Contains(useerTypeId);
            var userId = Convert.ToInt32(userDetails.Id);
            var headUserTypeId = Convert.ToInt32(userDetails.MainUserType);
            var res = new NewMadarsaOperationsRequestBs().GetByRequestSubmitId(Id, useerTypeId, userId);
            int IsPanelHead;
            bool checkForPanel = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Any();
            if (checkForPanel)
            {
                IsPanelHead = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelHead = 0;
            }

            int IsPanelMember;

            if ((new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Count != 0))
            {
                IsPanelMember = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelMember = 0;
            }
            //Check is user involved in any of the panels
            List<int> _listUser = _UserRegistrationBs.GetUserTypesByMainUserType(headUserTypeId).Select(m => (Int32)m.UserTypeId).ToList();

            //bool IsPanelInvolved = new PanelInvolveBs().IsPanelInvoled(_listUser);
            int IsPanelLiked = new RequestApproveRejectBs().IsPannelMemberLiked(_listUser);


            // List<int> PanelHeadUsers = new List<int> {6,7,8, 11,12, 13, 14, 15 };



            res.IsPanelInvolved = checkForPanel;
            res.IsPanelHeadUser = IsPanelHeadUser;
            res.IsPanelHead = IsPanelHead;
            res.IsPanelMember = IsPanelMember;

            return View(res);
        }

        public ActionResult MadarsaExtensionRequestDetails(int Id)
        {
            var userDetails = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var useerTypeId = Convert.ToInt32(userDetails.UserTypeId);

            List<int> _listUserHeads = new List<int> { 11, 13, 14, 15 };

            bool IsPanelHeadUser = _listUserHeads.Contains(useerTypeId);
            var userId = Convert.ToInt32(userDetails.Id);
            var headUserTypeId = Convert.ToInt32(userDetails.MainUserType);
            var res = new MadarsaExtensionRequestBs().GetByRequestSubmitId(Id, useerTypeId, userId);
            int IsPanelHead;
            bool checkForPanel = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Any();
            if (checkForPanel)
            {
                IsPanelHead = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelHead = 0;
            }

            int IsPanelMember;

            if ((new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Count != 0))
            {
                IsPanelMember = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelMember = 0;
            }
            //Check is user involved in any of the panels
            List<int> _listUser = _UserRegistrationBs.GetUserTypesByMainUserType(headUserTypeId).Select(m => (Int32)m.UserTypeId).ToList();

            //bool IsPanelInvolved = new PanelInvolveBs().IsPanelInvoled(_listUser);
            int IsPanelLiked = new RequestApproveRejectBs().IsPannelMemberLiked(_listUser);


            // List<int> PanelHeadUsers = new List<int> {6,7,8, 11,12, 13, 14, 15 };



            res.IsPanelInvolved = checkForPanel;
            res.IsPanelHeadUser = IsPanelHeadUser;
            res.IsPanelHead = IsPanelHead;
            res.IsPanelMember = IsPanelMember;

            return View(res);
        }

        public ActionResult ExistingMadarasaOeprationDetails(int Id)
        {
            var userDetails = new UserRegistrationBs().UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var useerTypeId = Convert.ToInt32(userDetails.UserTypeId);

            List<int> _listUserHeads = new List<int> { 11, 13, 14, 15 };

            bool IsPanelHeadUser = _listUserHeads.Contains(useerTypeId);
            var userId = Convert.ToInt32(userDetails.Id);
            var headUserTypeId = Convert.ToInt32(userDetails.MainUserType);
            var res = new ExistingMadarsaOperationsRequestBs().GetByRequestSubmitId(Id, useerTypeId, userId);
            int IsPanelHead;
            bool checkForPanel = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Any();
            if (checkForPanel)
            {
                IsPanelHead = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelHead = 0;
            }

            int IsPanelMember;

            if ((new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).ToList().Count != 0))
            {
                IsPanelMember = new PanelInvolveBs().PanelMemberInvolveList(Id, useerTypeId).FirstOrDefault().UserTypeId;
            }
            else
            {
                IsPanelMember = 0;
            }
            //Check is user involved in any of the panels
            List<int> _listUser = _UserRegistrationBs.GetUserTypesByMainUserType(headUserTypeId).Select(m => (Int32)m.UserTypeId).ToList();

            //bool IsPanelInvolved = new PanelInvolveBs().IsPanelInvoled(_listUser);
            int IsPanelLiked = new RequestApproveRejectBs().IsPannelMemberLiked(_listUser);


            // List<int> PanelHeadUsers = new List<int> {6,7,8, 11,12, 13, 14, 15 };



            res.IsPanelInvolved = checkForPanel;
            res.IsPanelHeadUser = IsPanelHeadUser;
            res.IsPanelHead = IsPanelHead;
            res.IsPanelMember = IsPanelMember;

            return View(res);
        }

    }
}