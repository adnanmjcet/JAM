using CommonLayer.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IMasjidExtensionRequest
    {
        List<MasjidModel> MasjidList();


        //List<RequestSubmitModel> RequestSubmitList();

        List<RequestTypeModel> RequestTypeList();

        List<UserModel> UserList();

        List<MasjidExtensionRequestModel> MasjidExtensionRequestList();

        MasjidExtensionRequestModel GetDetails(MasjidExtensionRequestModel model);

        int Save(MasjidExtensionRequestModel model);

        MasjidExtensionRequestModel GetById(int id);
        MasjidExtensionRequestModel GetByRequestSubmitIdBoard(int id, int userid);
        MasjidExtensionRequestModel GetByRequestId(int id, int UserTypeId, int UserId);


        //void Delete(MasjidExtensionRequestModel entity);
    }
}
