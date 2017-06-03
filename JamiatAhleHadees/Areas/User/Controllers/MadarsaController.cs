using BusinessLayer.Interface;
using BusinessLogic.Implementation;
using CommonLayer.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamiatAhleHadees.Areas.User.Controllers
{
    [Authorize(Roles = "Ameer,User")]
    public class MadarsaController : Controller
    {
        private MadarsaModel _MadarsaModel;
        private readonly IMadarsa _MadarsaBs;

        public MadarsaController()
        {
            _MadarsaModel = new MadarsaModel();
            _MadarsaBs = new MadarsaBs();
        }
        public ActionResult Index()
        {
            var varial = _MadarsaBs.MadarsaList();
            return View(varial);
        }
        public ActionResult Create(int? id)
        {
            if (id != null)
            {

                _MadarsaModel = _MadarsaBs.GetById(Convert.ToInt32(id));
                _MadarsaModel.UserLists = _MadarsaBs.UserList().Where(x=>x.RoleId==4).ToList();
                _MadarsaModel.ZoneModelList = _MadarsaBs.ZoneList().ToList();
                return View(_MadarsaModel);

            }
            else
            {
                _MadarsaModel.UserLists = _MadarsaBs.UserList().Where(x => x.RoleId == 4).ToList();
                _MadarsaModel.ZoneModelList = _MadarsaBs.ZoneList().ToList();
                _MadarsaModel.MadarsaLists = _MadarsaBs.MadarsaList().ToList();
                return View(_MadarsaModel);

            }
        }
        [HttpPost]
        public ActionResult Create(MadarsaModel model)
        {
            int i = 0;
        
            if (model != null)
            {
             i=  _MadarsaBs.Save(model);
            }
            if (i>0)
            {
                TempData["msg"] = "Madarsa Added Successfully";
                return RedirectToAction("Index", "Home", new { area = "User" });
            }
            else
            {
                _MadarsaModel.UserLists = _MadarsaBs.UserList().Where(x => x.RoleId == 4).ToList();
                _MadarsaModel.ZoneModelList = _MadarsaBs.ZoneList().ToList();
                _MadarsaModel.MadarsaLists = _MadarsaBs.MadarsaList().ToList();
                TempData["msg"] = "Madarsa Not Registered";
                return RedirectToAction("Create", _MadarsaModel);
            }
            
        }
    }
}