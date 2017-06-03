﻿using BusinessLayer.Interface;
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
    public class NewMadarsaOperationsRequestBs : INewMadarsaOperationsRequest
    {

        private readonly IGenericPattern<RequestSubmit> _RequestSubmit;

        private readonly RequestSubmitModel _RequestSubmitModel;


        private readonly IGenericPattern<NewMadarsaOperationsRequest> tbl_NewMadarsaOperationsRequestModel;

        public NewMadarsaOperationsRequestBs()
        {
            _RequestSubmit = new GenericPattern<RequestSubmit>();
            _RequestSubmitModel = new RequestSubmitModel();
            tbl_NewMadarsaOperationsRequestModel = new GenericPattern<NewMadarsaOperationsRequest>();
        }

        public NewMadarsaOperationsRequestModel GetById(int id)
        {
            NewMadarsaOperationsRequestModel varList = new NewMadarsaOperationsRequestModel();
            var item = tbl_NewMadarsaOperationsRequestModel.GetById(id);
            item = item ?? new NewMadarsaOperationsRequest();
            varList = new NewMadarsaOperationsRequestModel
            {
                Id = item.Id,
                ShortDescription = item.ShortDescription,
                Location = item.Location,
                Area = item.Area,
                ExpectedStudents = item.ExpectedStudents,
                Girls = item.Girls,
                Boys = item.Boys,
                Teachers = item.Teachers,
                IsResidential = item.IsResidential,
                MonthlyConst = item.MonthlyConst,
                CostPerStudent = item.CostPerStudent,
                RevenueSource = item.RevenueSource,
                TotalLandArea = item.TotalLandArea,
                ConstructedArea = item.ConstructedArea,
                IsRented = item.IsRented,
                ChargingStudent = item.ChargingStudent,
                IfChargingHowmuch = item.IfChargingHowmuch,
                Doc1 = item.Doc1,
                Doc2 = item.Doc2,
                Doc3 = item.Doc3,
                Pic1 = item.Pic1,
                Pic2 = item.Pic2,
                Pic3 = item.Pic3,
                Status = item.Status,
                CreatedDate = item.CreatedDate,
                // CreatedBy = item.CreatedBy,

                UserId = item.UserId,
                UserName = (item.User != null) ? item.User.Name : string.Empty,

                MadarsaId = item.MadarsaId,
                MadarsaName = (item.Madarsa != null) ? item.Madarsa.Name : string.Empty,

                RequestTypeId = item.RequestTypeId,
                RequestTypeName = (item.RequestType != null) ? item.RequestType.Name : string.Empty,

                RequestSubmitId = item.RequestSubmitId,
                //  RequestSubmitId
            };
            varList.RequestCommentList = new RequestCommentBs().BoardCommentList(id);
            varList.ApprovedList = new RequestApproveRejectBs().ApproveRejectDisplay(id);

            return varList;
        }

        public List<NewMadarsaOperationsRequestModel> NewMadarsaOperationRequestList()
        {
            List<NewMadarsaOperationsRequestModel> _varList = new List<NewMadarsaOperationsRequestModel>();
            var varData = tbl_NewMadarsaOperationsRequestModel.GetAll().ToList();
            _varList = (from item in varData
                        select new NewMadarsaOperationsRequestModel
                        {
                            Id = item.Id,
                            ShortDescription = item.ShortDescription,
                            Location = item.Location,
                            Area = item.Area,
                            ExpectedStudents = item.ExpectedStudents,
                            Girls = item.Girls,
                            Boys = item.Boys,
                            Teachers = item.Teachers,
                            IsResidential = item.IsResidential,
                            MonthlyConst = item.MonthlyConst,
                            CostPerStudent = item.CostPerStudent,
                            RevenueSource = item.RevenueSource,
                            TotalLandArea = item.TotalLandArea,
                            ConstructedArea = item.ConstructedArea,
                            IsRented = item.IsRented,
                            ChargingStudent = item.ChargingStudent,
                            IfChargingHowmuch = item.IfChargingHowmuch,

                            Doc1 = item.Doc1,
                            Doc2 = item.Doc2,
                            Doc3 = item.Doc3,
                            Pic1 = item.Pic1,
                            Pic2 = item.Pic2,
                            Pic3 = item.Pic3,
                            Status = item.Status,
                            CreatedDate = item.CreatedDate,
                            // CreatedBy = item.CreatedBy,

                            UserId = item.UserId,
                            UserName = (item.User != null) ? item.User.Name : string.Empty,

                            MadarsaId = item.MadarsaId,
                            MadarsaName = (item.Madarsa != null) ? item.Madarsa.Name : string.Empty,

                            RequestTypeId = item.RequestTypeId,
                            RequestTypeName = (item.RequestType != null) ? item.RequestType.Name : string.Empty,

                            RequestSubmitId = item.RequestSubmitId,
                            //  RequestSubmitId


                        }).OrderByDescending(x => x.Id).ToList();
            return _varList;
        }
        public NewMadarsaOperationsRequestModel GetByRequestSubmitId(int id, int UserTypeId, int UserId)
        {
            NewMadarsaOperationsRequestModel varList = new NewMadarsaOperationsRequestModel();
            var item = tbl_NewMadarsaOperationsRequestModel.GetAll().Where(x => x.RequestSubmitId == id).FirstOrDefault();
            item = item ?? new NewMadarsaOperationsRequest();
            varList = new NewMadarsaOperationsRequestModel
            {
                Id = item.Id,
                ShortDescription = item.ShortDescription,
                Location = item.Location,
                Area = item.Area,
                ExpectedStudents = item.ExpectedStudents,
                Girls = item.Girls,
                Boys = item.Boys,
                Teachers = item.Teachers,
                IsResidential = item.IsResidential,
                MonthlyConst = item.MonthlyConst,
                CostPerStudent = item.CostPerStudent,
                RevenueSource = item.RevenueSource,
                TotalLandArea = item.TotalLandArea,
                ConstructedArea = item.ConstructedArea,
                IsRented = item.IsRented,
                ChargingStudent = item.ChargingStudent,
                IfChargingHowmuch = item.IfChargingHowmuch,

                Doc1 = item.Doc1,
                Doc2 = item.Doc2,
                Doc3 = item.Doc3,
                Pic1 = item.Pic1,
                Pic2 = item.Pic2,
                Pic3 = item.Pic3,
                Status = item.Status,
                CreatedDate = item.CreatedDate,
                // CreatedBy = item.UserId,

                UserId = item.UserId,
                UserName = (item.User != null) ? item.User.Name : string.Empty,

                MadarsaId = item.MadarsaId,
                MadarsaName = (item.Madarsa != null) ? item.Madarsa.Name : string.Empty,

                RequestTypeId = item.RequestTypeId,
                RequestTypeName = (item.RequestType != null) ? item.RequestType.Name : string.Empty,

                RequestSubmitId = item.RequestSubmitId,
                //  RequestSubmitId
            };
            RequestCommentBs obj = new RequestCommentBs();
            var BoardComments = obj.BoardCommentList(id).ToList();
            varList.BoardCommentList = BoardComments;



            var PannelComments = obj.PanelCommentList(id).ToList();
            varList.PanelCommentList = PannelComments;
            if (UserTypeId == 6 || UserTypeId == 15)
            {
                varList.PanelCommentList = varList.PanelCommentList.Where(x => x.UserTypeId == 6 || x.UserTypeId == 15).ToList();
            }
            else if (UserTypeId == 7 || UserTypeId == 13)
            {
                varList.PanelCommentList = varList.PanelCommentList.Where(x => x.UserTypeId == 7 || x.UserTypeId == 13).ToList();

            }
            else if (UserTypeId == 8 || UserTypeId == 11)
            {
                varList.PanelCommentList = varList.PanelCommentList.Where(x => x.UserTypeId == 8 || x.UserTypeId == 11).ToList();

            }
            else if (UserTypeId == 12 || UserTypeId == 14)
            {
                varList.PanelCommentList = varList.PanelCommentList.Where(x => x.UserTypeId == 12 || x.UserTypeId == 14).ToList();

            }

            RequestApproveRejectBs obj1 = new RequestApproveRejectBs();
            varList.ApprovedList = obj1.ApproveRejectDisplay(id).ToList();
            varList.AlreadyExistsInMemberOpinion = varList.ApprovedList == null ? false : varList.ApprovedList.Where(m => m.UserTypeId == UserTypeId && m.RequestSubmitId == item.RequestSubmitId && m.UserId == UserId).Any();
            if (varList.AlreadyExistsInMemberOpinion)
            {
                varList.MemberOpinionId = varList.ApprovedList.Where(m => m.UserTypeId == UserTypeId && m.RequestSubmitId == item.RequestSubmitId).FirstOrDefault().Id;
                varList.IsAgreed = varList.ApprovedList.Where(m => m.UserTypeId == UserTypeId && m.RequestSubmitId == item.RequestSubmitId && m.UserId == UserId).FirstOrDefault().IsApproved;
                varList.LikeStatus = varList.ApprovedList.Where(m => m.UserTypeId == UserTypeId && m.RequestSubmitId == item.RequestSubmitId && m.UserId == UserId).FirstOrDefault().LikeStatus;
            }
            PanelInvolveBs panelobject = new PanelInvolveBs();

            varList.PannelMemberInvolved = panelobject.InvolveList(id).ToList();
            varList.PaannelMemberLikeDisLike = obj1.ApproveRejectDisplay(id).ToList();
            if (UserTypeId == 6 || UserTypeId == 15)
            {
                varList.PaannelMemberLikeDisLike = varList.PaannelMemberLikeDisLike.Where((x => x.UserTypeId == 6 || x.UserTypeId == 15 && x.LikeStatus != null)).ToList();
            }
            else if (UserTypeId == 7 || UserTypeId == 13)
            {
                varList.PaannelMemberLikeDisLike = varList.PaannelMemberLikeDisLike.Where((x => x.UserTypeId == 7 || x.UserTypeId == 13 && x.LikeStatus != null)).ToList();

            }
            else if (UserTypeId == 8 || UserTypeId == 11)
            {
                varList.PaannelMemberLikeDisLike = varList.PaannelMemberLikeDisLike.Where((x => x.UserTypeId == 8 || x.UserTypeId == 11 && x.LikeStatus != null)).ToList();

            }
            else if (UserTypeId == 12 || UserTypeId == 14)
            {
                varList.PaannelMemberLikeDisLike = varList.PaannelMemberLikeDisLike.Where((x => x.UserTypeId == 12 || x.UserTypeId == 14 && x.LikeStatus != null)).ToList();

            }
            return varList;
        }

        public NewMadarsaOperationsRequestModel GetByRequestSubmitIdBoard(int id, int userid)
        {
            NewMadarsaOperationsRequestModel varList = new NewMadarsaOperationsRequestModel();
            var item = tbl_NewMadarsaOperationsRequestModel.GetAll().Where(x => x.RequestSubmitId == id).FirstOrDefault();
            item = item ?? new NewMadarsaOperationsRequest();
            varList = new NewMadarsaOperationsRequestModel
            {
                Id = item.Id,
                ShortDescription = item.ShortDescription,
                Location = item.Location,
                Area = item.Area,
                ExpectedStudents = item.ExpectedStudents,
                Girls = item.Girls,
                Boys = item.Boys,
                Teachers = item.Teachers,
                IsResidential = item.IsResidential,
                MonthlyConst = item.MonthlyConst,
                CostPerStudent = item.CostPerStudent,
                RevenueSource = item.RevenueSource,
                TotalLandArea = item.TotalLandArea,
                ConstructedArea = item.ConstructedArea,
                IsRented = item.IsRented,
                ChargingStudent = item.ChargingStudent,
                IfChargingHowmuch = item.IfChargingHowmuch,

                Doc1 = item.Doc1,
                Doc2 = item.Doc2,
                Doc3 = item.Doc3,
                Pic1 = item.Pic1,
                Pic2 = item.Pic2,
                Pic3 = item.Pic3,
                Status = item.Status,
                CreatedDate = item.CreatedDate,
                // CreatedBy = item.UserId,

                UserId = item.UserId,
                UserName = (item.User != null) ? item.User.Name : string.Empty,

                MadarsaId = item.MadarsaId,
                MadarsaName = (item.Madarsa != null) ? item.Madarsa.Name : string.Empty,

                RequestTypeId = item.RequestTypeId,
                RequestTypeName = (item.RequestType != null) ? item.RequestType.Name : string.Empty,

                RequestSubmitId = item.RequestSubmitId,
                //  RequestSubmitId
            };
            RequestCommentBs obj = new RequestCommentBs();
            var BoardComments = obj.BoardCommentList(id).ToList();
            varList.BoardCommentList = BoardComments;

            var PannelComments = obj.PanelCommentList(id).ToList();
            varList.PanelCommentList = PannelComments;

            RequestApproveRejectBs obj1 = new RequestApproveRejectBs();
            varList.ApprovedList = obj1.ApproveRejectDisplay(id).ToList();
            varList.AlreadyExistsInMemberOpinion = varList.ApprovedList == null ? false : varList.ApprovedList.Where(m => m.UserTypeId == userid && m.RequestSubmitId == item.RequestSubmitId).Any();
            if (varList.AlreadyExistsInMemberOpinion)
            {
                varList.MemberOpinionId = varList.ApprovedList.Where(m => m.UserTypeId == userid && m.RequestSubmitId == item.RequestSubmitId).FirstOrDefault().Id;
                varList.IsAgreed = varList.ApprovedList.Where(m => m.UserTypeId == userid && m.RequestSubmitId == item.RequestSubmitId).FirstOrDefault().IsApproved;
                varList.LikeStatus = varList.ApprovedList.Where(m => m.UserTypeId == userid && m.RequestSubmitId == item.RequestSubmitId).FirstOrDefault().LikeStatus;
            }
            PanelInvolveBs panelobject = new PanelInvolveBs();

            varList.PannelMemberInvolved = panelobject.InvolveList(id).ToList();
            return varList;
        }

        public NewMadarsaOperationsRequestModel GetByRequestSubmitIdAmeer(int id)
        {
            NewMadarsaOperationsRequestModel varList = new NewMadarsaOperationsRequestModel();
            var item = tbl_NewMadarsaOperationsRequestModel.GetAll().Where(x => x.RequestSubmitId == id).FirstOrDefault();
            item = item ?? new NewMadarsaOperationsRequest();
            varList = new NewMadarsaOperationsRequestModel
            {
                Id = item.Id,
                ShortDescription = item.ShortDescription,
                Location = item.Location,
                Area = item.Area,
                ExpectedStudents = item.ExpectedStudents,
                Girls = item.Girls,
                Boys = item.Boys,
                Teachers = item.Teachers,
                IsResidential = item.IsResidential,
                MonthlyConst = item.MonthlyConst,
                CostPerStudent = item.CostPerStudent,
                RevenueSource = item.RevenueSource,
                TotalLandArea = item.TotalLandArea,
                ConstructedArea = item.ConstructedArea,
                IsRented = item.IsRented,
                ChargingStudent = item.ChargingStudent,
                IfChargingHowmuch = item.IfChargingHowmuch,

                Doc1 = item.Doc1,
                Doc2 = item.Doc2,
                Doc3 = item.Doc3,
                Pic1 = item.Pic1,
                Pic2 = item.Pic2,
                Pic3 = item.Pic3,
                Status = item.Status,
                CreatedDate = item.CreatedDate,
                // CreatedBy = item.UserId,

                UserId = item.UserId,
                UserName = (item.User != null) ? item.User.Name : string.Empty,

                MadarsaId = item.MadarsaId,
                MadarsaName = (item.Madarsa != null) ? item.Madarsa.Name : string.Empty,

                RequestTypeId = item.RequestTypeId,
                RequestTypeName = (item.RequestType != null) ? item.RequestType.Name : string.Empty,

                RequestSubmitId = item.RequestSubmitId,
                //  RequestSubmitId
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

        public NewMadarsaOperationsRequestModel GetByRequestSubmitId(int id)
        {
            NewMadarsaOperationsRequestModel varList = new NewMadarsaOperationsRequestModel();
            var item = tbl_NewMadarsaOperationsRequestModel.GetAll().Where(x => x.RequestSubmitId == id).FirstOrDefault();
            item = item ?? new NewMadarsaOperationsRequest();
            varList = new NewMadarsaOperationsRequestModel
            {
                Id = item.Id,
                ShortDescription = item.ShortDescription,
                Location = item.Location,
                Area = item.Area,
                ExpectedStudents = item.ExpectedStudents,
                Girls = item.Girls,
                Boys = item.Boys,
                Teachers = item.Teachers,
                IsResidential = item.IsResidential,
                MonthlyConst = item.MonthlyConst,
                CostPerStudent = item.CostPerStudent,
                RevenueSource = item.RevenueSource,
                TotalLandArea = item.TotalLandArea,
                ConstructedArea = item.ConstructedArea,
                IsRented = item.IsRented,
                ChargingStudent = item.ChargingStudent,
                IfChargingHowmuch = item.IfChargingHowmuch,

                Doc1 = item.Doc1,
                Doc2 = item.Doc2,
                Doc3 = item.Doc3,
                Pic1 = item.Pic1,
                Pic2 = item.Pic2,
                Pic3 = item.Pic3,
                Status = item.Status,
                CreatedDate = item.CreatedDate,
                //CreatedBy = item.UserId,

                UserId = item.UserId,
                UserName = (item.User != null) ? item.User.Name : string.Empty,

                MadarsaId = item.MadarsaId,
                MadarsaName = (item.Madarsa != null) ? item.Madarsa.Name : string.Empty,

                RequestTypeId = item.RequestTypeId,
                RequestTypeName = (item.RequestType != null) ? item.RequestType.Name : string.Empty,

                RequestSubmitId = item.RequestSubmitId,

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
            return varList;
        }

        public NewMadarsaOperationsRequestModel GetDetails(NewMadarsaOperationsRequestModel model)
        {
            model = model ?? new NewMadarsaOperationsRequestModel();
            if (model.Id != 0)
            {
                model.NewMadarsaOperationRequestModelList = NewMadarsaOperationRequestList();
                model.UserModelList = UserList();
                model.MadarsaModelList = MadarsaList();
                model.RequestSubmitModelList = RequestSubmitList();
                model.RequestTypeModelList = RequestTypeList();
            }
            model.NewMadarsaOperationRequestModelList = NewMadarsaOperationRequestList();

            return model;
        }

        public List<MadarsaModel> MadarsaList()
        {
            GenericPattern<Madarsa> _tblList = new GenericPattern<Madarsa>();
            List<MadarsaModel> _ModelList = new List<MadarsaModel>();
            var varData = _tblList.GetAll().ToList();
            _ModelList = (from item in varData
                          select new MadarsaModel
                          {
                              Id = item.Id,
                              Name = item.Name,

                          }).OrderByDescending(x => x.Id).ToList();
            return _ModelList;
        }

        public List<RequestSubmitModel> RequestSubmitList()
        {
            GenericPattern<DataAccessLayer.DataModel.RequestSubmit> _tblList = new GenericPattern<DataAccessLayer.DataModel.RequestSubmit>();
            List<RequestSubmitModel> _ModelList = new List<RequestSubmitModel>();
            var varData = _tblList.GetAll().ToList();
            _ModelList = (from item in varData
                          select new RequestSubmitModel
                          {
                              //Id = item.Id,
                              // Name = item.Name,

                          }).OrderByDescending(x => x.Id).ToList();
            return _ModelList;
        }

        public List<RequestTypeModel> RequestTypeList()
        {
            GenericPattern<RequestType> _tblList = new GenericPattern<RequestType>();
            List<RequestTypeModel> _ModelList = new List<RequestTypeModel>();
            var varData = _tblList.GetAll().ToList();
            _ModelList = (from item in varData
                          select new RequestTypeModel
                          {
                              Id = item.Id,
                              Name = item.Name,

                          }).OrderByDescending(x => x.Id).ToList();
            return _ModelList;
        }

        public int Save(NewMadarsaOperationsRequestModel model)
        {

            NewMadarsaOperationsRequest _tblList = new NewMadarsaOperationsRequest(model);

            _RequestSubmitModel.ShortDesc = model.ShortDescription;
            _RequestSubmitModel.UserId = model.UserId;
            _RequestSubmitModel.CreatedDate = Convert.ToDateTime(System.DateTime.Now);
            _RequestSubmitModel.RequestTypeId = model.RequestTypeId;
            RequestSubmit _requestSubmit = new RequestSubmit(_RequestSubmitModel);

            if (_tblList.Id != null && _tblList.Id != 0)
            {
                _tblList.CreatedDate = Convert.ToDateTime(System.DateTime.Now);
                tbl_NewMadarsaOperationsRequestModel.Update(_tblList);
            }
            else
            {
                _requestSubmit = _RequestSubmit.Insert(_requestSubmit);
                _tblList.RequestSubmitId = _requestSubmit.Id;
                _tblList.CreatedDate = Convert.ToDateTime(System.DateTime.Now);
                _tblList = tbl_NewMadarsaOperationsRequestModel.Insert(_tblList);
            }

            return _tblList.Id;
        }
        public List<UserModel> UserList()
        {
            GenericPattern<User> _tblList = new GenericPattern<User>();
            List<UserModel> _ModelList = new List<UserModel>();
            var varData = _tblList.GetAll().ToList();
            _ModelList = (from item in varData
                          select new UserModel
                          {
                              Id = item.Id,
                              Name = item.Name,

                          }).OrderByDescending(x => x.Id).ToList();
            return _ModelList;
        }
    }
}

