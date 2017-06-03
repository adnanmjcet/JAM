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
    public class ZoneBs : IZone
    {
        private readonly IGenericPattern<Zone> _tbl_Zone;

        public ZoneBs()
        {
            _tbl_Zone = new GenericPattern<Zone>();

        }

        public List<ZoneModel> ZoneList()
        {
            List<ZoneModel> _ZoneList = new List<ZoneModel>();
            var ZoneData = _tbl_Zone.GetAll().ToList();
            _ZoneList = (from item in ZoneData
                         select new ZoneModel
                         {
                                Id = item.Id,
                                Name = item.Name,
                                                    
                         }).OrderByDescending(x => x.Id).ToList();
            return _ZoneList;
        }

        public ZoneModel GetById(int id)
        {
            ZoneModel _Zone = new ZoneModel();
            var item = _tbl_Zone.GetById(id);
            item = item ?? new Zone();
            _Zone = new ZoneModel
            {
                Id = item.Id,
                Name = item.Name,
            };
            return _Zone;
        }

        public ZoneModel GetDetails(ZoneModel model)
        {
            model = model ?? new ZoneModel();
            if (model.Id != 0)
            {
                model.ZoneModelList = ZoneList();
            }
            model.ZoneModelList = ZoneList();

            return model;
        }

        public int Save(ZoneModel model)
        {
            Zone _tblZone = new Zone(model);
            if (model.Id != null && model.Id != 0)
            {
                _tbl_Zone.Update(_tblZone);

            }
            else
            {
                _tblZone = _tbl_Zone.Insert(_tblZone);
            }

            return _tblZone.Id;
        }
    }
}
