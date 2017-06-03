using BusinessLayer.Implementation;
using BusinessLayer.Interface;
using CommonLayer.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamiatAhleHadees.Areas.User.Controllers
{
    [Authorize(Roles = "Ameer,User")]
    public class ZoneController : Controller
    {

        private ZoneModel _ZoneModel;
        private readonly IZone _ZoneBs;

        public ZoneController()
        {
            _ZoneModel = new ZoneModel();
            _ZoneBs = new ZoneBs();
        }
        // GET: User/Zone
        public ActionResult Index()
        {
            var varial = _ZoneBs.ZoneList();
            return View(varial);
        }

        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                _ZoneModel = _ZoneBs.GetById(Convert.ToInt32(id));
                _ZoneModel.ZoneModelList = _ZoneBs.ZoneList().ToList();
                return View(_ZoneModel);

            }
            else
            {
                _ZoneModel.ZoneModelList = _ZoneBs.ZoneList().ToList();
                return View(_ZoneModel);

            }

        }
        [HttpPost]
        public ActionResult Create(ZoneModel model)
        {
            int i = 0;

            if (model != null)
            {
                i = _ZoneBs.Save(model);
            }
            if (i > 0)
            {
                TempData["msg"] = "Zone Added Successfully";
                return RedirectToAction("Index", "Home", new { area = "User" });
            }
            else
            {
                _ZoneModel.ZoneModelList = _ZoneBs.ZoneList().ToList();
                TempData["msg"] = "Zone Not Registered";
                return RedirectToAction("Create", _ZoneModel);
            }

        }
    }
}