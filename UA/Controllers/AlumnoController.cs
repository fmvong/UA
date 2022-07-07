using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using UA.Models;

namespace UA.Controllers
{
    public class AlumnoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult CancelarIns(Guid id)
        {
            Inscripcion inscripcion = db.Inscripcion.First(i => i.Id == id);
            db.Inscripcion.Remove(inscripcion);
            db.SaveChanges();
            return RedirectToAction("MateriasAlu", new { id = inscripcion.IdAlumno });
        }

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
                //var db = new ApplicationDbContext();
                //model.Id = Guid.NewGuid();
                db.Inscripcion.Add(model.AgregarId());
                db.SaveChanges();
                return RedirectToAction("MateriasAlu", new { id = model.IdAlumno });
            }
            //ViewBag.Message = ("No cargo lista");
            return View(model);
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View(new LogInViewModel());


            /*List<Inscripcion> listaId = new List<Inscripcion>();
            Inscripcion a = new Inscripcion();
            a.IdAlumno = 8;
            listaId.Add(a);

            int id = listaId[0].IdAlumno;
            List<Inscripcion> materias = new List<Inscripcion>();
            List<Inscripcion> materiasAlu = new List<Inscripcion>();
            materias = db.Inscripcion.ToList();
            foreach (Inscripcion materia in materias)
            {
                if (materia.IdAlumno == id)
                {
                    materiasAlu.Add(materia);
                }
            }

            return View(materiasAlu);*/
        }

        [HttpPost]
        public ActionResult LogIn(LogInViewModel logIn)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("MateriasAlu", new { id = logIn.Id });

                /*int id = list[0].IdAlumno;
                List<Inscripcion> materias = new List<Inscripcion>();
                List<Inscripcion> materiasAlu = new List<Inscripcion>();
                materias = db.Inscripcion.ToList();
                foreach (Inscripcion materia in materias)
                {
                    if (materia.IdAlumno == id)
                    {
                        materiasAlu.Add(materia);
                    }
                }

                return View(materiasAlu);*/
                //
            }
            return View(logIn);
        }

        [HttpGet]
        public ActionResult MateriasAlu(int id)
        {
            List<Inscripcion> listaId = new List<Inscripcion>();
            Inscripcion a = new Inscripcion();
            a.IdAlumno = id;
            listaId.Add(a);

            List<Inscripcion> materias = new List<Inscripcion>();
            materias = db.Inscripcion.Where(i => i.IdAlumno == id).ToList();
            return View(materias);
        }


        [HttpPost]
        public ActionResult MateriasAlu(List<Inscripcion> listaId)
        {
            int id = listaId[0].IdAlumno;
            List<Inscripcion> materias = new List<Inscripcion>();
            List<Inscripcion> materiasAlu = new List<Inscripcion>();
            materias = db.Inscripcion.ToList();
            foreach(Inscripcion materia in materias)
            {
                if(materia.IdAlumno == id)
                {
                    materiasAlu.Add(materia);
                }
            }

            return View(materiasAlu);
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