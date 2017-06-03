using BusinessLayer.Implementation;
using CommonLayer.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamiatAhleHadees.Areas.Admin.Controllers
{
    [Authorize(Roles = "Ameer")]
    public class AdminCommentController : Controller
    {
        private RequestCommentModel _RequestCommentModel;
        private readonly RequestCommentBs _RequestCommentBs;

        public AdminCommentController()
        {
            _RequestCommentModel = new RequestCommentModel();
            _RequestCommentBs = new RequestCommentBs();
        }
        // GET: Admin/AdminComment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AmmerComment(RequestSubmitModel model)
        {
            UserRegistrationBs obj = new UserRegistrationBs();
            var UserId = obj.UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Id;
            var UserTypeId = obj.UserRegistrationList().Where(x => x.UserName == User.Identity.Name).FirstOrDefault().UserTypeId;

            var RequestDetails = new RequestSubmitBs().RequestSubmitList().Where(x => x.Id == model.Id).FirstOrDefault();
            if (model != null)
            {
                model.UserId = Convert.ToInt32(UserId);
                model.Id = Convert.ToInt32(model.Id);
                model.RequestTypeId = RequestDetails.RequestTypeId;
                model.ShortDesc = RequestDetails.ShortDesc;
                model.IsApproved = RequestDetails.IsApproved;
                model.Comment = model.Comment;
                model.UserTypeId = Convert.ToInt32(UserTypeId);
                model.CreatedDate = Convert.ToDateTime(System.DateTime.Now);
                _RequestCommentBs.AmeerComment(model);
            }
            return Json("_RequestComentModel", JsonRequestBehavior.AllowGet);
        }

    }
}