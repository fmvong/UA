using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UA.Models;

namespace UA.Controllers
{
    public class ProfesorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profesor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VerExamenes()
        {
            return View(new List<Inscripcion>());
        }

        [HttpPost]
        public ActionResult VerExamenes(List<Inscripcion> model)
        {
            if (ModelState.IsValid)
            {
                //var db = new ApplicationDbContext();
                //var inscripciones = db.Inscripcion.ToList();
                ViewBag.Message = ("La lista se cargo de db");
                //int a = db.Alumnos.Count();
                return View(db.Inscripcion.ToList());
                //return RedirectToAction("ExitoRegistroMateria", new { nombre = model.Materia });
            }
            ViewBag.Message = ("No cargo lista");
            return View(model);
        }

        
    }
}