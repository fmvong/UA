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

        public ActionResult ExitoRegMateria()
        {
            return View();
        }
        public ActionResult CargaMateria()
        {
            return View(new MateriaViewModel());
        }

        [HttpPost]
        public ActionResult CargaMateria(MateriaViewModel model)
        {
            ValidarCarrera(model);
            ValidarMateria(model);
            if (ModelState.IsValid)
            {
                db.Materias.Add(new MateriaC { ID = model.ID, Materia = model.Materia, IDcarrera = model.IDcarrera, IDcorrelativa1 = model.IDcorrelativa1, IDcorrelativa2 = model.IDcorrelativa2, Semestre = model.Semestre });
                db.SaveChanges();

                return RedirectToAction("ExitoRegMateria", new { nombre = model.Materia });
            }
            return View(model);
        }

        private void ValidarCarrera(MateriaViewModel model)
        {
            bool carreraExiste = false;
            List<CarreraC> carreras = db.Carreras.ToList();
            foreach (CarreraC carrera in carreras)
            {
                if (model.IDcarrera == carrera.ID.Trim())
                {
                    carreraExiste = true;
                }
            }
            if (carreraExiste != true)
            {
                ModelState.AddModelError(nameof(model.IDcarrera), "La carrera no existe");
            }
        }

        private void ValidarMateria(MateriaViewModel model)
        {
            bool materiaNoExiste = false;
            List<MateriaC> materias = db.Materias.ToList();
            foreach (MateriaC materia in materias)
            {
                if (model.ID == materia.ID.Trim())
                {
                    materiaNoExiste = true;
                }
            }
            if (materiaNoExiste == true)
            {
                ModelState.AddModelError(nameof(model.ID), "La materia ya existe");
            }
        }

        public ActionResult index()
        {
            ViewBag.Message = "Bienvenido Admin";

            return View(new CarreraViewModel());
        }

        [HttpPost]
        public ActionResult index(CarreraViewModel model)
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
        public ActionResult ExitoRegistroCarrera(string nomCarrera)
        {
            ViewBag.Message = (nomCarrera);

            return View();
        }


        /*// GET: Admin
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
                *//*
                db.Carreras.Add(model.ToEntity());
                foreach (var carrera in model.Carreras)
                {
                    db.Carreras.Add(model.ToEntity());
                }*//*
                //int ultimoID = db.Alumnos.ToList().Last().ID;
                    //db.Alumnos.Add(model.ToEntity(ultimoID));
                db.SaveChanges();
                

                return RedirectToAction("CreacionAluExitosa", new { id = 1 });
            }
            return View(model);
        }*/
    }
}