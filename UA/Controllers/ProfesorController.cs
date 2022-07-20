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

        public ActionResult AsignarNota(Guid id)
        {
            DateTime Now = DateTime.Now;
            NotaViewModel examen = new NotaViewModel { Id = id, Fecha = Now.ToString("MM/dd/yyyy") , Nota = 6 };
            return View(examen);
        }

        [HttpPost]
        public ActionResult AsignarNota(NotaViewModel examen) 
        {
            if (ModelState.IsValid)
            {
                Inscripcion inscripcion = db.Inscripcion.First(i => i.Id == examen.Id);
                inscripcion.Fecha = examen.Fecha;
                inscripcion.Nota = examen.Nota;
                db.SaveChanges();

                return RedirectToAction("VerExamenes", new { id = inscripcion.IDMateria.Trim() });
            }
            return View(examen);
        }

        // GET: Profesor
        public ActionResult Index()
        {
            return View(new MateriaLogInViewModel());
        }
        [HttpPost]
        public ActionResult Index(MateriaLogInViewModel materiaid)
        {
            //Validar marteria si existe
            List<MateriaC> materias = db.Materias.Where(d=>d.ID.Trim() == materiaid.ID).ToList();
            if(materias.Count != 1)
            {
                ModelState.AddModelError(nameof(materiaid.ID), "La materia no existe");
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("VerExamenes", new { id = materiaid.ID });
            }
            return View(materiaid);
        }

        [HttpGet]
        public ActionResult VerExamenes(string id)
        {
            List<Inscripcion> materias = new List<Inscripcion>();
            materias = db.Inscripcion.Where(i => i.IDMateria == id).ToList();
            if (materias.Count == 0)
            {
                List<ReporteViewModel> reporteVacio = new List<ReporteViewModel>();
                ReporteViewModel a = new ReporteViewModel();
                a.IdAlumno = 1;
                a.IDMateria = id;
                a.Id = new Guid();
                a.Semestre = 0;
                a.Fecha = " ";
                a.Nota = 4;
                a.Materia = "Ejemplo";
                reporteVacio.Add(a);
                return View(reporteVacio);
            }
            List<ReporteViewModel> reporte = new List<ReporteViewModel>();
            foreach (Inscripcion materia in materias)
            {
                ReporteViewModel reporteModel = new ReporteViewModel(materia);

                reporteModel.Materia = db.Materias.First(i => i.ID == materia.IDMateria).Materia;

                Alumno alumno = db.Alumnos.FirstOrDefault(i => i.ID == materia.IdAlumno);
                if (alumno == null)
                    continue;
                reporteModel.Nombre = alumno.Nombre;
                reporteModel.Apellido = alumno.Apellido;

                //reporteModel.Nombre = db.Alumnos.FirstOrDefault(i => i.ID == materia.IdAlumno).Nombre;
                //reporteModel.Apellido = db.Alumnos.FirstOrDefault(i => i.ID == materia.IdAlumno).Apellido;

                reporte.Add(reporteModel);
            }
            return View(reporte);
        }

        /*[HttpPost]
        public ActionResult VerExamenes(List<MateriaC> materiasId)
        {
            string id = materiasId[0].ID;
            List<Inscripcion> inscripciones = new List<Inscripcion>();
            List<Inscripcion> materiasfiltro = new List<Inscripcion>();
            inscripciones = db.Inscripcion.ToList();
            foreach (Inscripcion materia in inscripciones)
            {
                if (materia.IDMateria == id)
                {
                    materiasfiltro.Add(materia);
                }
            }

            return View(materiasfiltro);
        }*/

        
    }
}