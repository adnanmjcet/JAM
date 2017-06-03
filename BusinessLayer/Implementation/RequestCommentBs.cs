using BusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.CommonModels;
using DataAccessLayer.DataModel;
using DataAccessLayer.GenericPattern.Interface;
using DataAccessLayer.GenericPattern.Implementation;

namespace BusinessLayer.Implementation
{
    public class RequestCommentBs : IRequestComment
    {
        private readonly IGenericPattern<RequestComment> _RequestComment;
        private readonly RequestCommentModel _RequestCommentModel;
        private readonly IGenericPattern<RequestSubmit> _RequestSubmit;

        public RequestCommentBs()
        {
            _RequestComment = new GenericPattern<RequestComment>();
            _RequestSubmit = new GenericPattern<RequestSubmit>();

            _RequestCommentModel = new RequestCommentModel();
        }
        public RequestCommentModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public RequestCommentModel GetDetails(RequestCommentModel model)
        {
            throw new NotImplementedException();
        }

        public List<RequestCommentModel> PanelCommentList(int id)
        {
            List<RequestCommentModel> requestCommentModel = new List<RequestCommentModel>();
            var requestComments = _RequestComment.GetAll().Where(x => x.RequestSubmitId == id).ToList();

            requestCommentModel = (from item in requestComments
                                   select new RequestCommentModel
                                   {
                                       Id = item.Id,
                                       UserId = Convert.ToInt32(item.UserId),
                                       UserName = item.User.Name,
                                       UserTypeName = item.UserType.Name,
                                       Comment = item.Comment,
                                       CreatedDate = Convert.ToDateTime(item.CreatedDate),
                                       UserTypeId = Convert.ToInt32(item.UserTypeId),
                                   }).OrderByDescending(x => x.Id).ToList();
            return requestCommentModel;
        }


        public List<RequestCommentModel> BoardCommentList(int id)
        {
            List<RequestCommentModel> requestCommentModel = new List<RequestCommentModel>();
            var requestComments = _RequestComment.GetAll().Where(x => x.RequestSubmitId == id && (x.UserTypeId == 2 || x.UserTypeId == 3 || x.UserTypeId == 4 || x.UserTypeId == 5 || x.UserTypeId == 10 || x.UserTypeId == 11 || x.UserTypeId == 13 || x.UserTypeId == 14 || x.UserTypeId == 15)).ToList();

            requestCommentModel = (from item in requestComments
                                   select new RequestCommentModel
                                   {
                                       Id = item.Id,
                                       UserId = Convert.ToInt32(item.UserId),
                                       UserName = item.User.Name,
                                       UserTypeName = item.UserType.Name,
                                       Comment = item.Comment,
                                       CreatedDate = Convert.ToDateTime(item.CreatedDate)
                                   }).OrderByDescending(x => x.Id).ToList();
            return requestCommentModel;
        }

        public int Save(RequestCommentModel model)
        {
            RequestComment _requestComment = new RequestComment(model);
            if (model.Id != null && model.Id != 0)
            {
                _RequestComment.Update(_requestComment);

            }
            else
            {
                //  _requestApprove.CreatedDate = System.DateTime.Now;

                _requestComment = _RequestComment.Insert(_requestComment);
            }

            return _requestComment.Id;
        }


        public int AmeerComment(RequestSubmitModel model)
        {
            RequestSubmit _requestComment = new RequestSubmit(model);
            if (model.Id != null && model.Id != 0)
            {
                _RequestSubmit.Update(_requestComment);

            }
            else
            {
                //  _requestApprove.CreatedDate = System.DateTime.Now;

                _requestComment = _RequestSubmit.Insert(_requestComment);
            }

            return _requestComment.Id;
        }

    }
}
