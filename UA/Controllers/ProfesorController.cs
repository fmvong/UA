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
            //Inscripcion inscripcion = db.Inscripcion.First(i => i.Id == id);
            NotaViewModel examen = new NotaViewModel { Id = id, Fecha = "07/06/2022", Nota = 6 };
            return View(examen);

            //return View(new NotaViewModel());
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
            List<ReporteViewModel> reporte = new List<ReporteViewModel>();
            foreach (Inscripcion materia in materias)
            {
                ReporteViewModel reporteModel = new ReporteViewModel(materia);

                reporteModel.Materia = db.Materias.First(i => i.ID == materia.IDMateria).Materia;
                reporteModel.Nombre = db.Alumnos.First(i => i.ID == materia.IdAlumno).Nombre;
                reporteModel.Apellido = db.Alumnos.First(i => i.ID == materia.IdAlumno).Apellido;

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