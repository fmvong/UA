using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UA.Models;

namespace UA.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult index()
        {
            ViewBag.Message = "Bienvenido Admin";

            return View(new CarreraC());
        }

        [HttpPost]
        public ActionResult index(CarreraC model)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                db.Carreras.Add(new CarreraC { ID = model.ID, Carrera = model.Carrera });
                db.SaveChanges();

                return RedirectToAction("ExitoRegistroCarrera", new { nombre = model.Carrera });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ExitoRegistroCarrera(String nomCarrera)
        {
            ViewBag.Message = (nomCarrera);

            return View();
        }


        // GET: Admin
        public ActionResult index1()
        {
            ViewBag.Message = "Bienvenido Admin";
             
            return View(new AcademicViewModel());
        }

        [HttpPost]
        public ActionResult index1(AcademicViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();

                String a = model.Carreras[0].ID;
                String b = model.Carreras[0].Carrera;

                //db.Carreras.Add(new CarreraViewModel { ID = a, Carrera = b });
                /*
                db.Carreras.Add(model.ToEntity());
                foreach (var carrera in model.Carreras)
                {
                    db.Carreras.Add(model.ToEntity());
                }*/
                //int ultimoID = db.Alumnos.ToList().Last().ID;
                    //db.Alumnos.Add(model.ToEntity(ultimoID));
                db.SaveChanges();
                

                return RedirectToAction("CreacionAluExitosa", new { id = 1 });
            }
            return View(model);
        }
    }
}