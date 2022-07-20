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
            MateriaIdCarrerasViewModel model = new MateriaIdCarrerasViewModel { ID = " ", Materia = " ", IDcarrera = " ", Semestre = 1, CarrerasID = new List<string>() };
            List<CarreraC> carreras = db.Carreras.ToList();
            foreach(CarreraC carrera in carreras)
            {
                model.CarrerasID.Add(carrera.ID.Trim());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult CargaMateria(MateriaIdCarrerasViewModel model)
        {
            ValidarCaracteres50materia(model);
            ValidarSemestre(model);
            ValidarCaracteres10materia(model);
            ValidarCarreraExiste(model);
            ValidarMateria(model);

            if (model.CarrerasID == null)
            {
                List<CarreraC> carreras = db.Carreras.ToList();
                List<string> a = new List<string>();
                model.CarrerasID = a;
                foreach (CarreraC carrera in carreras)
                {
                    model.CarrerasID.Add(carrera.ID.Trim());
                }
            }

            if (ModelState.IsValid)
            {
                db.Materias.Add(new MateriaC { ID = model.ID, Materia = model.Materia, IDcarrera = model.IDcarrera, IDcorrelativa1 = model.IDcorrelativa1, IDcorrelativa2 = model.IDcorrelativa2, Semestre = model.Semestre });
                db.SaveChanges();

                return RedirectToAction("ExitoRegMateria", new { nombre = model.Materia });
            }

            return View(model);
        }

        private void ValidarSemestre(MateriaIdCarrerasViewModel model)
        {
            if (model.Semestre != 1 && model.Semestre != 2)
            {
                ModelState.AddModelError(nameof(model.Semestre), "El semestre debe ser 1 o 2");
            }
        }

        private void ValidarCaracteres50materia(MateriaIdCarrerasViewModel model)
        {
            if (model.Materia != null && model.Materia.Length > 50)
            {
                ModelState.AddModelError(nameof(model.Materia), "La materia puede tener hasta 50 caracteres.");
            }
        }

        private void ValidarCaracteres10materia(MateriaIdCarrerasViewModel model)
        {
            if (model.ID != null && model.ID.Length > 10)
            {
                ModelState.AddModelError(nameof(model.ID), "El id tiene hasta 10 caracteres.");
            }
        }

        private void ValidarCarreraExiste(MateriaIdCarrerasViewModel model)
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

        private void ValidarMateria(MateriaIdCarrerasViewModel model)
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

        private void ValidarCarreraNoExiste(CarreraViewModel model)
        {
            bool carreraExiste = false;
            List<CarreraC> carreras = db.Carreras.ToList();
            foreach (CarreraC carrera in carreras)
            {
                if (model.ID == carrera.ID.Trim())
                {
                    carreraExiste = true;
                }
            }
            if (carreraExiste == true)
            {
                ModelState.AddModelError(nameof(model.ID), "La carrera ya existe");
            }
        }

        private void ValidarCaracteres10(CarreraViewModel model)
        {
            if (model.ID!= null && model.ID.Length > 10)
            {
                ModelState.AddModelError(nameof(model.ID), "El id tiene hasta 10 caracteres.");
            }
        }

        private void ValidarCaracteres50(CarreraViewModel model)
        {
            if (model.Carrera != null && model.Carrera.Length > 50)
            {
                ModelState.AddModelError(nameof(model.Carrera), "La Carrera tiene hasta 50 caracteres.");
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
            ValidarCaracteres50(model);
            ValidarCaracteres10(model);
            ValidarCarreraNoExiste(model);
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