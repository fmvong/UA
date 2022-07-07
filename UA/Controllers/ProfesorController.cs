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
            //List<Inscripcion> listaId = new List<Inscripcion>();
            //Inscripcion inscripcion = new Inscripcion();
            Inscripcion inscripcion = db.Inscripcion.First(i => i.Id == id);
            inscripcion.Id = id;
            //listaId.Add(a);

            //List<Inscripcion> materias = new List<Inscripcion>();
            //materias = db.Inscripcion.Where(i => i.Id == id).ToList();
            return View(inscripcion);

            //return View(new NotaViewModel());
        }
        [HttpPost]
        public ActionResult AsignarNota(NotaViewModel materiaid)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("VerExamenes", new { id = materiaid.Id });
            }
            return View(materiaid);
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
            List<Inscripcion> listaId = new List<Inscripcion>();
            Inscripcion a = new Inscripcion();
            a.IDMateria = id;
            listaId.Add(a);

            List<Inscripcion> materias = new List<Inscripcion>();
            materias = db.Inscripcion.Where(i => i.IDMateria == id).ToList();
            return View(materias);
        }

        [HttpPost]
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
        }

        
    }
}