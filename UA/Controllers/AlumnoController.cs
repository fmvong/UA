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
        // GET: Alumno
        /*public ActionResult Alumno()
        {
            ViewBag.Message = "Estas registrado?";

            return View();
        }*/

        [HttpGet]
        public ActionResult Materias()
        {

            List<MateriaViewModel> materias = db.Materia.ToList();

            List<MateriaViewModel> model = new List<MateriaViewModel>();

            foreach (Materia materia in materias)
            {
                model.Add(new MateriaViewModel { Id = materias.Id, Materia = materias.Materia, IDcarrera = materia.IDcarrera });
            }
 
            return View(new AlumnoViewModel());
        }

        [HttpGet]
        public ActionResult Alumno()
        {
            
            ViewBag.Message = "Estas registrado?";

            return View(new AlumnoViewModel());
        }

        [HttpPost]
        public ActionResult Alumno(AlumnoViewModel model)
        {
            ViewBag.Message = "Estas registrado?";

            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                db.Alumnos.Add(new AlumnoViewModel { IdAlumno = Guid.NewGuid(), Nombre = model.Nombre, Apellido = model.Apellido, IDcarrera = model.IDcarrera });
                db.SaveChanges();

                return RedirectToAction("Alumno");
            }
            return View(model);
        }

        //Ejemplo de validacion. No funciona para nuevas Carreras
        private void ValidarRegistro(AlumnoViewModel model)
        {
            if (model.IDcarrera!="A1"&& model.IDcarrera != "D2" && model.IDcarrera != "G3" && model.IDcarrera != "I4" && model.IDcarrera != "P5")
            {
                ModelState.AddModelError(nameof(model.IDcarrera), "La carrera no es valida");
            }
        }
    }
}