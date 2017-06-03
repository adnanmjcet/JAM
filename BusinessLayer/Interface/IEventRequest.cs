using CommonLayer.CommonModels;
using DataAccessLayer.DataModel;
using DataAccessLayer.GenericPattern.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
   public interface IEventRequest
    {
       
        List<EventRequestModel> GetAllEvents();

        List<EventRequestDetailModel> GetAllEventDetails();

        int Save(EventRequestModel model);

        EventRequestModel GetById(int id);

        EventRequestDetailModel GetEventDetailsId(int id);

        int SaveEventDetails(EventRequestDetailModel model);

        List<EventMasterModel> GetAllEventMasterList();



    }
}
