using BusinessLayer.Interface;
using CommonLayer.CommonModels;
using DataAccessLayer.DataModel;
using DataAccessLayer.GenericPattern.Implementation;
using DataAccessLayer.GenericPattern.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class MasjidExtensionRequestBs : IMasjidExtensionRequest
    {

        private readonly IGenericPattern<MasjidExtensionRequest> _tbl_MER;
        private MasjidExtensionRequestModel _MasjidExtensionRequestModel;


        private readonly IGenericPattern<RequestSubmit> _RequestSubmit;

        private readonly RequestSubmitModel _RequestSubmitModel;


        public MasjidExtensionRequestBs()
        {
            _tbl_MER = new GenericPattern<MasjidExtensionRequest>();
            _MasjidExtensionRequestModel = new MasjidExtensionRequestModel();

            _RequestSubmit = new GenericPattern<RequestSubmit>();
            _RequestSubmitModel = new RequestSubmitModel();
        }

        public List<MasjidExtensionRequestModel> MasjidExtensionRequestList()
        {
            List<MasjidExtensionRequestModel> _MERList = new List<MasjidExtensionRequestModel>();
            var MERData = _tbl_MER.GetAll().ToList();
            _MERList = (from item in MERData
                        select new MasjidExtensionRequestModel
                        {

                            Id = item.Id,
                            ShortDescription = item.ShortDescription,
                            UserId = item.UserId,
                            UserName = (item.User != null) ? item.User.Name : string.Empty,
                            Location = item.Location,
                            Area = item.Area,
                            MasjidId = item.MasjidId,
                            MasjidName = (item.Masjid != null) ? item.Masjid.Name : string.Empty,
                            ConstructionCost = item.ConstructionCost,
                            ExistingFloors = item.ExistingFloors,
                            AmountNeeded = item.AmountNeeded,
                            Engineer = item.Engineer,
                            Elevation = item.Elevation,
                            Paln = item.Paln,
                            Doc1 = item.Doc1,
                            Doc2 = item.Doc2,
                            Doc3 = item.Doc3,
                            Pic1 = item.Pic1,
                            Pic2 = item.Pic2,
                            Pic3 = item.Pic3,

                            RequestSubmitId = item.RequestSubmitId,
                            //RequestSubmitName= (item.RequestSubmit != null) ? item.RequestSubmit.Name : string.Empty,
                            Status = item.Status,
                            CreatedDate = item.CreatedDate,
                            RequestTypeId = item.RequestTypeId,
                            RequestTypeName = (item.RequestType != null) ? item.RequestType.Name : string.Empty,
                        }).OrderByDescending(x => x.Id).ToList();
            return _MERList;
        }

        public List<MasjidModel> MasjidList()
        {
            GenericPattern<Masjid> _tbl_Masjid = new GenericPattern<Masjid>();
            List<MasjidModel> _MasjidList = new List<MasjidModel>();
            var MasjidData = _tbl_Masjid.GetAll().ToList();
            _MasjidList = (from item in MasjidData
                           select new MasjidModel
                           {

                               Id = item.Id,
                               Name = item.Name,
                           }).OrderByDescending(x => x.Id).ToList();
            return _MasjidList;
        }

        public List<UserModel> UserList()
        {
            GenericPattern<User> _tbl_User = new GenericPattern<User>();
            List<UserModel> _UserList = new List<UserModel>();
            var UserData = _tbl_User.GetAll().ToList();
            _UserList = (from item in UserData
                         select new UserModel
                         {
                             Id = item.Id,
                             Name = item.Name,

                         }).OrderByDescending(x => x.Id).ToList();
            return _UserList;
        }

        public List<RequestTypeModel> RequestTypeList()
        {
            GenericPattern<RequestType> _tbl_RequestType = new GenericPattern<RequestType>();
            List<RequestTypeModel> _RequestTypeModelList = new List<RequestTypeModel>();
            var RequestTypeData = _tbl_RequestType.GetAll().ToList();
            _RequestTypeModelList = (from item in RequestTypeData
                                     select new RequestTypeModel
                                     {
                                         Id = item.Id,
                                         Name = item.Name,

                                     }).OrderByDescending(x => x.Id).ToList();
            return _RequestTypeModelList;
        }

        public List<RequestSubmitModel> RequestSubmitList()
        {
            GenericPattern<RequestSubmit> _tbl_RequestSubmit = new GenericPattern<RequestSubmit>();
            List<RequestSubmitModel> _RequestSubmitModelList = new List<RequestSubmitModel>();
            var RequestSubmitData = _tbl_RequestSubmit.GetAll().ToList();
            _RequestSubmitModelList = (from item in RequestSubmitData
                                       select new RequestSubmitModel
                                       {
                                           //Id = item.Id,
                                           //Name = item.Name,

                                       }).OrderByDescending(x => x.Id).ToList();
            return _RequestSubmitModelList;
        }

        public MasjidExtensionRequestModel GetDetails(MasjidExtensionRequestModel model)
        {
            model = model ?? new MasjidExtensionRequestModel();
            if (model.Id != 0)
            {
                model.MasjidLists = MasjidList();
                model.RequestTypeLists = RequestTypeList();
                model.UserLists = UserList();
                model.MasjidExtensionRequestLists = MasjidExtensionRequestList();
            }
            model.MasjidExtensionRequestLists = MasjidExtensionRequestList();

            return model;

        }
        public int Save(MasjidExtensionRequestModel model)
        {
            MasjidExtensionRequest _tbl_mer = new MasjidExtensionRequest(model);
            _RequestSubmitModel.ShortDesc = model.ShortDescription;
            _RequestSubmitModel.UserId = model.UserId;
            _RequestSubmitModel.RequestTypeId = model.RequestTypeId;
            _RequestSubmitModel.CreatedDate = Convert.ToDateTime(System.DateTime.Now);
            RequestSubmit _requestSubmit = new RequestSubmit(_RequestSubmitModel);

            if (_tbl_mer.Id != null && _tbl_mer.Id != 0)
            {
                _tbl_mer.CreatedDate = Convert.ToDateTime(System.DateTime.Now);
                //_tblList.CreatedBy = 1;
                _tbl_MER.Update(_tbl_mer);
            }
            else
            {
                _requestSubmit = _RequestSubmit.Insert(_requestSubmit);
                _tbl_mer.RequestSubmitId = _requestSubmit.Id;
                _tbl_mer.CreatedDate = Convert.ToDateTime(System.DateTime.Now);
                //_tblList.CreatedBy = 1;
                _tbl_mer = _tbl_MER.Insert(_tbl_mer);
            }


            return _tbl_mer.Id;
        }
        public MasjidExtensionRequestModel GetById(int id)
        {
            MasjidExtensionRequestModel _MasjidExtensionRequestModel = new MasjidExtensionRequestModel();
            var MERbyId = _tbl_MER.GetById(id);
            MERbyId = MERbyId ?? new MasjidExtensionRequest();
            _MasjidExtensionRequestModel = new MasjidExtensionRequestModel
            {
                Id = MERbyId.Id,
                ShortDescription = MERbyId.ShortDescription,
                UserId = MERbyId.UserId,
                UserName = (MERbyId.User != null) ? MERbyId.User.UserName : string.Empty,
                Location = MERbyId.Location,
                Area = MERbyId.Area,
                MasjidId = MERbyId.MasjidId,
                MasjidName = (MERbyId.Masjid != null) ? MERbyId.Masjid.Name : string.Empty,
                ConstructionCost = MERbyId.ConstructionCost,
                ExistingFloors = MERbyId.ExistingFloors,
                AmountNeeded = MERbyId.AmountNeeded,
                Engineer = MERbyId.Engineer,
                Elevation = MERbyId.Elevation,
                Paln = MERbyId.Paln,
                Doc1 = MERbyId.Doc1,
                Doc2 = MERbyId.Doc2,
                Doc3 = MERbyId.Doc3,
                Pic1 = MERbyId.Pic1,
                Pic2 = MERbyId.Pic2,
                Pic3 = MERbyId.Pic3,
                RequestSubmitId = MERbyId.RequestSubmitId,
                //RequestSubmitName= (item.RequestSubmit != null) ? item.RequestSubmit.Name : string.Empty,
                Status = MERbyId.Status,
                CreatedDate = MERbyId.CreatedDate,
                RequestTypeId = MERbyId.RequestTypeId,
                RequestTypeName = (MERbyId.RequestType != null) ? MERbyId.RequestType.Name : string.Empty,
            };
            _MasjidExtensionRequestModel.RequestCommentList = new RequestCommentBs().BoardCommentList(id);
            _MasjidExtensionRequestModel.ApprovedList = new RequestApproveRejectBs().ApproveRejectDisplay(id);
            return _MasjidExtensionRequestModel;
        }

        public MasjidExtensionRequestModel GetByRequestId(int id, int UserTypeId, int userId)
        {
            MasjidExtensionRequestModel _MasjidExtensionRequestModel = new MasjidExtensionRequestModel();
            var MERbyId = _tbl_MER.GetAll().Where(m => m.RequestSubmitId == id).FirstOrDefault();
            MERbyId = MERbyId ?? new MasjidExtensionRequest();
            _MasjidExtensionRequestModel = new MasjidExtensionRequestModel
            {
                Id = MERbyId.Id,
                ShortDescription = MERbyId.ShortDescription,
                UserId = MERbyId.UserId,
                UserName = (MERbyId.User != null) ? MERbyId.User.UserName : string.Empty,
                Location = MERbyId.Location,
                Area = MERbyId.Area,
                MasjidId = MERbyId.MasjidId,
                MasjidName = (MERbyId.Masjid != null) ? MERbyId.Masjid.Name : string.Empty,
                ExistingFloors = MERbyId.ExistingFloors,
                AmountNeeded = MERbyId.AmountNeeded,
                Engineer = MERbyId.Engineer,
                Elevation = MERbyId.Elevation,
                Paln = MERbyId.Paln,
                Doc1 = MERbyId.Doc1,
                Doc2 = MERbyId.Doc2,
                Doc3 = MERbyId.Doc3,
                Pic1 = MERbyId.Pic1,
                Pic2 = MERbyId.Pic2,
                Pic3 = MERbyId.Pic3,
                RequestSubmitId = MERbyId.RequestSubmitId,
                //RequestSubmitName= (item.RequestSubmit != null) ? item.RequestSubmit.Name : string.Empty,
                Status = MERbyId.Status,
                CreatedDate = MERbyId.CreatedDate,
                RequestTypeId = MERbyId.RequestTypeId,
                RequestTypeName = (MERbyId.RequestType != null) ? MERbyId.RequestType.Name : string.Empty,
            };
            RequestCommentBs obj = new RequestCommentBs();
            var BoardComments = obj.BoardCommentList(id).ToList();
            _MasjidExtensionRequestModel.BoardCommentList = BoardComments;

            var PannelComments = obj.PanelCommentList(id).ToList();
            _MasjidExtensionRequestModel.PanelCommentList = PannelComments;
            if (UserTypeId == 6 || UserTypeId == 15)
            {
                _MasjidExtensionRequestModel.PanelCommentList = _MasjidExtensionRequestModel.PanelCommentList.Where(x => x.UserTypeId == 6 || x.UserTypeId == 15).ToList();
            }
            else if (UserTypeId == 7 || UserTypeId == 13)
            {
                _MasjidExtensionRequestModel.PanelCommentList = _MasjidExtensionRequestModel.PanelCommentList.Where(x => x.UserTypeId == 7 || x.UserTypeId == 13).ToList();

            }
            else if (UserTypeId == 8 || UserTypeId == 11)
            {
                _MasjidExtensionRequestModel.PanelCommentList = _MasjidExtensionRequestModel.PanelCommentList.Where(x => x.UserTypeId == 8 || x.UserTypeId == 11).ToList();

            }
            else if (UserTypeId == 12 || UserTypeId == 14)
            {
                _MasjidExtensionRequestModel.PanelCommentList = _MasjidExtensionRequestModel.PanelCommentList.Where(x => x.UserTypeId == 12 || x.UserTypeId == 14).ToList();

            }

            RequestApproveRejectBs obj1 = new RequestApproveRejectBs();
            _MasjidExtensionRequestModel.ApprovedList = obj1.ApproveRejectDisplay(id).ToList();
            _MasjidExtensionRequestModel.AlreadyExistsInMemberOpinion = _MasjidExtensionRequestModel.ApprovedList == null ? false : _MasjidExtensionRequestModel.ApprovedList.Where(m => m.UserTypeId == UserTypeId && m.RequestSubmitId == MERbyId.RequestSubmitId && m.UserId == userId).Any();
            if (_MasjidExtensionRequestModel.AlreadyExistsInMemberOpinion)
            {
                _MasjidExtensionRequestModel.MemberOpinionId = _MasjidExtensionRequestModel.ApprovedList.Where(m => m.UserTypeId == UserTypeId && m.RequestSubmitId == MERbyId.RequestSubmitId).FirstOrDefault().Id;
                _MasjidExtensionRequestModel.IsAgreed = _MasjidExtensionRequestModel.ApprovedList.Where(m => m.UserTypeId == UserTypeId && m.RequestSubmitId == MERbyId.RequestSubmitId && m.UserId == userId).FirstOrDefault().IsApproved;
                _MasjidExtensionRequestModel.LikeStatus = _MasjidExtensionRequestModel.ApprovedList.Where(m => m.UserTypeId == UserTypeId && m.RequestSubmitId == MERbyId.RequestSubmitId && m.UserId == userId).FirstOrDefault().LikeStatus;
            }
            PanelInvolveBs panelobject = new PanelInvolveBs();

            _MasjidExtensionRequestModel.PannelMemberInvolved = panelobject.InvolveList(id).ToList();
            _MasjidExtensionRequestModel.PaannelMemberLikeDisLike = obj1.ApproveRejectDisplay(id).ToList();
            if (UserTypeId == 6 || UserTypeId == 15)
            {
                _MasjidExtensionRequestModel.PaannelMemberLikeDisLike = _MasjidExtensionRequestModel.PaannelMemberLikeDisLike.Where((x => x.UserTypeId == 6 || x.UserTypeId == 15 && x.LikeStatus != null)).ToList();
            }
            else if (UserTypeId == 7 || UserTypeId == 13)
            {
                _MasjidExtensionRequestModel.PaannelMemberLikeDisLike = _MasjidExtensionRequestModel.PaannelMemberLikeDisLike.Where((x => x.UserTypeId == 7 || x.UserTypeId == 13 && x.LikeStatus != null)).ToList();

            }
            else if (UserTypeId == 8 || UserTypeId == 11)
            {
                _MasjidExtensionRequestModel.PaannelMemberLikeDisLike = _MasjidExtensionRequestModel.PaannelMemberLikeDisLike.Where((x => x.UserTypeId == 8 || x.UserTypeId == 11 && x.LikeStatus != null)).ToList();

            }
            else if (UserTypeId == 12 || UserTypeId == 14)
            {
                _MasjidExtensionRequestModel.PaannelMemberLikeDisLike = _MasjidExtensionRequestModel.PaannelMemberLikeDisLike.Where((x => x.UserTypeId == 12 || x.UserTypeId == 14 && x.LikeStatus != null)).ToList();

            }
            return _MasjidExtensionRequestModel;
        }

        public MasjidExtensionRequestModel GetByRequestSubmitIdBoard(int id, int userid)
        {
            MasjidExtensionRequestModel varList = new MasjidExtensionRequestModel();
            var MERbyId = _tbl_MER.GetAll().Where(x => x.RequestSubmitId == id).FirstOrDefault();
            MERbyId = MERbyId ?? new MasjidExtensionRequest();
            varList = new MasjidExtensionRequestModel
            {
                Id = MERbyId.Id,
                ShortDescription = MERbyId.ShortDescription,
                UserId = MERbyId.UserId,
                UserName = (MERbyId.User != null) ? MERbyId.User.UserName : string.Empty,
                Location = MERbyId.Location,
                Area = MERbyId.Area,
                MasjidId = MERbyId.MasjidId,
                MasjidName = (MERbyId.Masjid != null) ? MERbyId.Masjid.Name : string.Empty,
                ConstructionCost = MERbyId.ConstructionCost,
                ExistingFloors = MERbyId.ExistingFloors,
                AmountNeeded = MERbyId.AmountNeeded,
                Engineer = MERbyId.Engineer,
                Elevation = MERbyId.Elevation,
                Paln = MERbyId.Paln,
                Doc1 = MERbyId.Doc1,
                Doc2 = MERbyId.Doc2,
                Doc3 = MERbyId.Doc3,
                Pic1 = MERbyId.Pic1,
                Pic2 = MERbyId.Pic2,
                Pic3 = MERbyId.Pic3,
                RequestSubmitId = MERbyId.RequestSubmitId,
                //RequestSubmitName= (item.RequestSubmit != null) ? item.RequestSubmit.Name : string.Empty,
                Status = MERbyId.Status,
                CreatedDate = MERbyId.CreatedDate,
                RequestTypeId = MERbyId.RequestTypeId,
                RequestTypeName = (MERbyId.RequestType != null) ? MERbyId.RequestType.Name : string.Empty,
            };
            RequestCommentBs obj = new RequestCommentBs();
            var BoardComments = obj.BoardCommentList(id).ToList();
            varList.BoardCommentList = BoardComments;

            var PannelComments = obj.PanelCommentList(id).ToList();
            varList.PanelCommentList = PannelComments;

            RequestApproveRejectBs obj1 = new RequestApproveRejectBs();
            varList.ApprovedList = obj1.ApproveRejectDisplay(id).ToList();
            varList.AlreadyExistsInMemberOpinion = varList.ApprovedList == null ? false : varList.ApprovedList.Where(m => m.UserTypeId == userid && m.RequestSubmitId == MERbyId.RequestSubmitId).Any();
            if (varList.AlreadyExistsInMemberOpinion)
            {
                varList.MemberOpinionId = varList.ApprovedList.Where(m => m.UserTypeId == userid && m.RequestSubmitId == MERbyId.RequestSubmitId).FirstOrDefault().Id;
                varList.IsAgreed = varList.ApprovedList.Where(m => m.UserTypeId == userid && m.RequestSubmitId == MERbyId.RequestSubmitId).FirstOrDefault().IsApproved;
                varList.LikeStatus = varList.ApprovedList.Where(m => m.UserTypeId == userid && m.RequestSubmitId == MERbyId.RequestSubmitId).FirstOrDefault().LikeStatus;
            }
            PanelInvolveBs panelobject = new PanelInvolveBs();

            varList.PannelMemberInvolved = panelobject.InvolveList(id).ToList();
            return varList;
        }

        public MasjidExtensionRequestModel GetMasjidExtensionById(int id)
        {
            MasjidExtensionRequestModel _MasjidExtensionRequestModel = new MasjidExtensionRequestModel();
            var MERbyId = _tbl_MER.GetAll().Where(x => x.RequestSubmitId == id).FirstOrDefault();
            MERbyId = MERbyId ?? new MasjidExtensionRequest();
            _MasjidExtensionRequestModel = new MasjidExtensionRequestModel
            {
                Id = MERbyId.Id,
                ShortDescription = MERbyId.ShortDescription,
                UserId = MERbyId.UserId,
                UserName = (MERbyId.User != null) ? MERbyId.User.UserName : string.Empty,
                Location = MERbyId.Location,
                Area = MERbyId.Area,
                MasjidId = MERbyId.MasjidId,
                MasjidName = (MERbyId.Masjid != null) ? MERbyId.Masjid.Name : string.Empty,
                ConstructionCost = MERbyId.ConstructionCost,
                ExistingFloors = MERbyId.ExistingFloors,
                AmountNeeded = MERbyId.AmountNeeded,
                Engineer = MERbyId.Engineer,
                Elevation = MERbyId.Elevation,
                Paln = MERbyId.Paln,
                Doc1 = MERbyId.Doc1,
                Doc2 = MERbyId.Doc2,
                Doc3 = MERbyId.Doc3,
                Pic1 = MERbyId.Pic1,
                Pic2 = MERbyId.Pic2,
                Pic3 = MERbyId.Pic3,
                RequestSubmitId = MERbyId.RequestSubmitId,
                //RequestSubmitName= (item.RequestSubmit != null) ? item.RequestSubmit.Name : string.Empty,
                Status = MERbyId.Status,
                CreatedDate = MERbyId.CreatedDate,
                RequestTypeId = MERbyId.RequestTypeId,
                RequestTypeName = (MERbyId.RequestType != null) ? MERbyId.RequestType.Name : string.Empty,
            };
            RequestCommentBs obj = new RequestCommentBs();
            var BoardComments = obj.BoardCommentList(id).ToList();
            _MasjidExtensionRequestModel.BoardCommentList = BoardComments;

            var PannelComments = obj.PanelCommentList(id).ToList();
            _MasjidExtensionRequestModel.PanelCommentList = PannelComments;

            RequestApproveRejectBs obj1 = new RequestApproveRejectBs();
            _MasjidExtensionRequestModel.ApprovedList = obj1.ApproveRejectDisplay(id).ToList();

            PanelInvolveBs panelobject = new PanelInvolveBs();
            _MasjidExtensionRequestModel.PannelMemberInvolved = panelobject.InvolveList(id).ToList();

            return _MasjidExtensionRequestModel;
        }

        public MasjidExtensionRequestModel GetByRequestSubmitIdAmeer(int id)
        {
            MasjidExtensionRequestModel varList = new MasjidExtensionRequestModel();
            var MERbyId = _tbl_MER.GetAll().Where(x => x.RequestSubmitId == id).FirstOrDefault();
            MERbyId = MERbyId ?? new MasjidExtensionRequest();
            varList = new MasjidExtensionRequestModel
            {
                Id = MERbyId.Id,
                ShortDescription = MERbyId.ShortDescription,
                UserId = MERbyId.UserId,
                UserName = (MERbyId.User != null) ? MERbyId.User.UserName : string.Empty,
                Location = MERbyId.Location,
                Area = MERbyId.Area,
                MasjidId = MERbyId.MasjidId,
                MasjidName = (MERbyId.Masjid != null) ? MERbyId.Masjid.Name : string.Empty,
                ConstructionCost = MERbyId.ConstructionCost,
                ExistingFloors = MERbyId.ExistingFloors,
                AmountNeeded = MERbyId.AmountNeeded,
                Engineer = MERbyId.Engineer,
                Elevation = MERbyId.Elevation,
                Paln = MERbyId.Paln,
                Doc1 = MERbyId.Doc1,
                Doc2 = MERbyId.Doc2,
                Doc3 = MERbyId.Doc3,
                Pic1 = MERbyId.Pic1,
                Pic2 = MERbyId.Pic2,
                Pic3 = MERbyId.Pic3,
                RequestSubmitId = MERbyId.RequestSubmitId,
                //RequestSubmitName= (item.RequestSubmit != null) ? item.RequestSubmit.Name : string.Empty,
                Status = MERbyId.Status,
                CreatedDate = MERbyId.CreatedDate,
                RequestTypeId = MERbyId.RequestTypeId,
                RequestTypeName = (MERbyId.RequestType != null) ? MERbyId.RequestType.Name : string.Empty,
            };
            RequestCommentBs obj = new RequestCommentBs();
            var BoardComments = obj.BoardCommentList(id).ToList();
            varList.BoardCommentList = BoardComments;

            var PannelComments = obj.PanelCommentList(id).ToList();
            varList.PanelCommentList = PannelComments;

            RequestApproveRejectBs obj1 = new RequestApproveRejectBs();
            varList.ApprovedList = obj1.ApproveRejectDisplay(id).ToList();

            PanelInvolveBs panelobject = new PanelInvolveBs();

            varList.PannelMemberInvolved = panelobject.InvolveList(id).ToList();

            varList.PaannelMemberLikeDisLike = obj1.ApproveRejectDisplay(id).ToList();
            return varList;
        }

    }
}
