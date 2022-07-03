using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UA.Models;
using System.Linq;

namespace UA.Controllers
{
    public class AlumnoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //codigo incompleto para traer las materias en las que un alumno esta inscripto.

        [HttpGet]
        public ActionResult LogIn()
        {
            return View(new Alumno());
        }

        [HttpPost]
        public ActionResult LogIn(Alumno alumno)
        {
            var historial = new List<Historial>();
            historial.Add(new Historial());
            historial[0].IdAlumno = alumno.ID; 

            return RedirectToAction("MateriasAlu", new { historial });
        }
        [HttpGet]
        public ActionResult MateriasAlu()
        { 

            return View();
        }


        [HttpPost]
        public ActionResult MateriasAlu(List<Historial> historial)
        {

            if (ModelState.IsValid)
            {
                //var db = new ApplicationDbContext();

                /*using (db)
                {

                    var modelMaterias = db.Materias.SqlQuery("Select * from Materias").ToList();

                }*/


                //db.SaveChanges();
                var modelMaterias = db.Historial.SqlQuery("Select * from Historial").ToList();
                return View(modelMaterias);
            }

            return View();
        }

        [HttpGet]
        public ActionResult CrearAlu()
        {
            
            ViewBag.Message = "Estas registrado?";

            return View(new AlumnoViewModel());
        }

        [HttpPost]
        public ActionResult CrearAlu(AlumnoViewModel model)
        {
            //ViewBag.Message = "Estas registrado? Seguro?";

            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                //db.Alumnos.Add(new Alumno { IdAlumno = Guid.NewGuid(), Nombre = model.Nombre, Apellido = model.Apellido, IDcarrera = model.IDcarrera });
                int ultimoID = db.Alumnos.ToList().Last().ID;
                db.Alumnos.Add(model.ToEntity(ultimoID));
                db.SaveChanges();
                
                return RedirectToAction("CreacionAluExitosa", new { id = ultimoID });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreacionAluExitosa(int id)
        {
            String IdNuevo = (id + 1).ToString();

            ViewBag.Message = (IdNuevo);

            return View();
        }

        //No se usa
        [HttpGet]
        public ActionResult UltimoAlumnoReg()
        {
            var db = new ApplicationDbContext();
            int a = db.Alumnos.Last().ID;

            return View(new AlumnoViewModel());
        }

        //Ejemplo de Editar

        /*
        public ActionResult Edit(Guid id)
        {
            Alumno alumno = db.Alumnos.Find(id);
            AlumnoViewModel model = new AlumnoViewModel(alumno);

            return View(alumno);
        }
        */

        //Ejemplo de validacion. No funciona para nuevas Carreras

        public ActionResult ShowAlu()
        {

            List<Alumno> alumnos = db.Alumnos.ToList();

            return View(alumnos);
        }

        private void ValidarRegistro(AlumnoViewModel model)
        {
            if (model.IDcarrera!="A1"&& model.IDcarrera != "D2" && model.IDcarrera != "G3" && model.IDcarrera != "I4" && model.IDcarrera != "P5")
            {
                ModelState.AddModelError(nameof(model.IDcarrera), "La carrera no es valida");
            }
        }

    }
}