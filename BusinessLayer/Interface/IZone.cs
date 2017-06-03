using CommonLayer.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IZone
    {

        List<ZoneModel> ZoneList();


        ZoneModel GetDetails(ZoneModel model);

        int Save(ZoneModel model);

        ZoneModel GetById(int id);
    }
}
