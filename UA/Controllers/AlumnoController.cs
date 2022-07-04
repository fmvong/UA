using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UA.Models;

namespace UA.Controllers
{
    public class AlumnoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult NuevaInscripcion()
        {
            return View(new InscripcionViewModel());
        }

        [HttpPost]
        public ActionResult NuevaInscripcion(InscripcionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                int ultimoID = db.Inscripcion.ToList().Last().Id;
                db.Inscripcion.Add(model.AgregarId(ultimoID));
                db.SaveChanges();

                return RedirectToAction("MateriasAlu");     //, new { id = model.IdAlumno }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View(new Alumno());
        }

        [HttpPost]
        public ActionResult LogIn(int idAlumno)
        {
            /*var historial = new List<Inscripcion>();
            var A = new Inscripcion();
            A.IdAlumno = alumno.ID;
            historial.Add(A);
            historial[0].IdAlumno = alumno.ID;*/ 

            return RedirectToAction("MateriasAlu", new { id = idAlumno});
            //return View(historial);
        }

        [HttpGet]
        public ActionResult MateriasAlu()
        {
            /*List<Inscripcion> materias = db.Inscripcion.ToList();
            List<InscripcionViewModel> model = new List<InscripcionViewModel>(); 
            foreach(Inscripcion inscripcion in materias)
            {
                model.Add(new InscripcionViewModel { IdAlumno = inscripcion.IdAlumno, IDMateria = inscripcion.IDMateria });
            }*/

            List<Inscripcion> materias = db.Inscripcion.ToList();
            return View(materias);
        }


        [HttpPost]
        public ActionResult MateriasAlu(int idAlumno)
        {
            List<Inscripcion> materias = new List<Inscripcion>();
            materias = db.Inscripcion.ToList();
            return View(materias);
          
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