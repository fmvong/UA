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
        public ActionResult BajaCarrera(int id)
        {
            //Inscripcion inscripcion = db.Inscripcion.First(i => i.IdAlumno == id);
            List<Inscripcion> inscripciones = db.Inscripcion.Where(i => i.IdAlumno == id).ToList();
            foreach(Inscripcion inscripcion in inscripciones)
            {
                db.Inscripcion.Remove(inscripcion);
            }
            Alumno alumno = db.Alumnos.First(i => i.ID == id);
            db.Alumnos.Remove(alumno);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult MateriasRendidas(int id)
        {
            List<Inscripcion> materias = new List<Inscripcion>();
            materias = db.Inscripcion.Where(i => i.IdAlumno == id && i.Nota > 0).ToList();
            List<ReporteViewModel> reporte = new List<ReporteViewModel>();
            foreach (Inscripcion materia in materias)
            {
                ReporteViewModel reporteModel = new ReporteViewModel(materia);

                reporteModel.Materia = db.Materias.First(i => i.ID == materia.IDMateria).Materia;
                reporte.Add(reporteModel);
            }
            return View(reporte);
        }

        [HttpGet]
        public ActionResult MateriasAprobadas(int id)
        {
            List<Inscripcion> materias = new List<Inscripcion>();
            materias = db.Inscripcion.Where(i => i.IdAlumno == id && i.Nota > 3).ToList();
            List<ReporteViewModel> reporte = new List<ReporteViewModel>();
            foreach (Inscripcion materia in materias)
            {
                ReporteViewModel reporteModel = new ReporteViewModel(materia);

                reporteModel.Materia = db.Materias.First(i => i.ID == materia.IDMateria).Materia;
                reporte.Add(reporteModel);
            }
            return View(reporte);
        }



        [HttpGet]
        public ActionResult CancelarIns(Guid id)
        {
            Inscripcion inscripcion = db.Inscripcion.First(i => i.Id == id);
            db.Inscripcion.Remove(inscripcion);
            db.SaveChanges();
            return RedirectToAction("MateriasAlu", new { id = inscripcion.IdAlumno });
        }

        [HttpGet]
        public ActionResult NuevaInscripcion(int id)
        {
            Inscripcion inscripcion = db.Inscripcion.First(i => i.IdAlumno == id);
            InscripcionViewModel model = new InscripcionViewModel { IdAlumno = id };
            return View(model);
        }

        [HttpPost]
        public ActionResult NuevaInscripcion(InscripcionViewModel model)
        {
            ValidarYaInscripto(model);
            ValidarMateriaExiste(model);
            Validar7materias(model);
            ValidarCorrelativas(model);
            if (ModelState.IsValid)
            {
                db.Inscripcion.Add(model.AgregarId());
                db.SaveChanges();
                return RedirectToAction("MateriasAlu", new { id = model.IdAlumno });
            }
            return View(model);
        }

        private void ValidarYaInscripto(InscripcionViewModel model)
        {
            bool materiaNoExiste = false;
            //filtro inscripciones del alumno
            List<Inscripcion> inscripciones = db.Inscripcion.Where(d => d.IdAlumno == model.IdAlumno).ToList();

            foreach (Inscripcion inscripcion in inscripciones)
            {
                if (model.IDMateria == inscripcion.IDMateria.Trim() && (inscripcion.Nota > 3 || inscripcion.Nota == 0))
                {
                    materiaNoExiste = true;
                }
            }
            if (materiaNoExiste == true)
            {
                ModelState.AddModelError(nameof(model.IDMateria), "La inscripciona esta Materia ya existe");
            }
        }

        private void ValidarMateriaExiste(InscripcionViewModel model)
        {
            bool materiaExiste = false;
            List<MateriaC> materias = db.Materias.ToList();
            foreach (MateriaC materia in materias)
            {
                if (model.IDMateria == materia.ID.Trim())
                {
                    materiaExiste = true;
                }
            }
            if (materiaExiste == false)
            {
                ModelState.AddModelError(nameof(model.IDMateria), "La Materia no existe");
            }
        }

        private void Validar7materias(InscripcionViewModel model)
        {
            int materia7 = 0;
            //filtro inscripciones del alumno
            List<Inscripcion> inscripciones = db.Inscripcion.Where(d => d.IdAlumno == model.IdAlumno).ToList();
            int semestre = db.Materias.First(i => i.ID == model.IDMateria).Semestre;

            foreach (Inscripcion inscripcion in inscripciones)
            {
                int semestreIns = db.Materias.First(i => i.ID == inscripcion.IDMateria).Semestre;
                if (inscripcion.Nota == 0 && semestre == semestreIns)
                {
                    materia7++;
                }
            }
            if (materia7 >= 7)
            {
                ModelState.AddModelError(nameof(model.IDMateria), "El número máximo de inscripciones es siete.");
            }
        }

        private bool ValidarCorrelativa1(InscripcionViewModel model)
        {
            bool correlativaAprovada1 = false;
            MateriaC materia = db.Materias.First(i => i.ID == model.IDMateria);

            if (materia.IDcorrelativa1 != null)
            {
                MateriaC correlativa1 = db.Materias.First(i => i.ID == materia.IDcorrelativa1);
                //MateriaC correlativa2 = db.Materias.First(i => i.ID == materia.IDcorrelativa2);

                //filtro inscripciones del alumno
                List<Inscripcion> inscripciones = db.Inscripcion.Where(d => d.IdAlumno == model.IdAlumno).ToList();
                
                    foreach (Inscripcion inscripcion in inscripciones)
                    {
                        if (inscripcion.IDMateria == correlativa1.ID && inscripcion.Nota > 3)
                        {
                            correlativaAprovada1 = true;
                        }
                    }
                    return correlativaAprovada1;
                    /*if (correlativaAprovada1 == false)
                    {
                        ModelState.AddModelError(nameof(model.IDMateria), $"Falta aprobar {correlativa1.Materia} y {correlativa2.Materia}");
                    }*/
            }
            return correlativaAprovada1;
        }

        private bool ValidarCorrelativa2(InscripcionViewModel model)
        {
            bool correlativaAprovada2 = false;
            MateriaC materia = db.Materias.First(i => i.ID == model.IDMateria);
            if (materia.IDcorrelativa2 != null)
            {
                //MateriaC correlativa1 = db.Materias.First(i => i.ID == materia.IDcorrelativa1);
                MateriaC correlativa2 = db.Materias.First(i => i.ID == materia.IDcorrelativa2);

                //filtro inscripciones del alumno
                List<Inscripcion> inscripciones = db.Inscripcion.Where(d => d.IdAlumno == model.IdAlumno).ToList();

                foreach (Inscripcion inscripcion in inscripciones)
                {
                    if (inscripcion.IDMateria == correlativa2.ID && inscripcion.Nota > 3)
                    {
                        correlativaAprovada2 = true;
                    }
                }

                return correlativaAprovada2;
                /*if (correlativaAprovada2 == false)
                {
                    ModelState.AddModelError(nameof(model.IDMateria), $"Falta aprobar {correlativa1.Materia} y {correlativa2.Materia}");
                }*/
            }
            return correlativaAprovada2;
        }

        public void ValidarCorrelativas(InscripcionViewModel model)
        {
            MateriaC materia = db.Materias.First(i => i.ID == model.IDMateria);
            /*if (materia.IDcorrelativa2 != null && materia.IDcorrelativa1 != null)
            {*/
            if (materia.IDcorrelativa1 != null && materia.IDcorrelativa2 != null && ValidarCorrelativa1(model) == false && ValidarCorrelativa2(model) == false)
            {
                MateriaC correlativa2 = db.Materias.First(i => i.ID == materia.IDcorrelativa2);
                MateriaC correlativa1 = db.Materias.First(i => i.ID == materia.IDcorrelativa1);
                ModelState.AddModelError(nameof(model.IDMateria), $"Falta aprobar {correlativa1.Materia} y {correlativa2.Materia}");
            }
            else if (materia.IDcorrelativa1 != null && ValidarCorrelativa1(model) == false)
            {
                MateriaC correlativa1 = db.Materias.First(i => i.ID == materia.IDcorrelativa1);
                ModelState.AddModelError(nameof(model.IDMateria), $"Falta aprobar {correlativa1.Materia}");
            }
            else if (materia.IDcorrelativa2 != null && ValidarCorrelativa2(model) == false)
            {
                MateriaC correlativa2 = db.Materias.First(i => i.ID == materia.IDcorrelativa2);
                ModelState.AddModelError(nameof(model.IDMateria), $"Falta aprobar {correlativa2.Materia}");
            }
            //}
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View(new LogInViewModel());
        }

        [HttpPost]
        public ActionResult LogIn(LogInViewModel logIn)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("MateriasAlu", new { id = logIn.Id });
            }
            return View(logIn);
        }

        [HttpGet]
        public ActionResult MateriasAlu(int id)
        {
            List<Inscripcion> materias = new List<Inscripcion>();
            materias = db.Inscripcion.Where(i => i.IdAlumno == id).ToList();
            List<ReporteViewModel> reporte = new List<ReporteViewModel>();
            foreach(Inscripcion materia in materias)
            {
                ReporteViewModel reporteModel = new ReporteViewModel(materia);

                reporteModel.Materia = db.Materias.First(i => i.ID == materia.IDMateria).Materia;
                reporteModel.Semestre = db.Materias.First(i => i.ID == materia.IDMateria).Semestre;

                reporte.Add(reporteModel);
            }


            return View(reporte);
        }

        /*[HttpPost]
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
        }*/

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

        private void ValidarRegistro(AlumnoViewModel model)
        {
            if (model.IDcarrera!="A1"&& model.IDcarrera != "D2" && model.IDcarrera != "G3" && model.IDcarrera != "I4" && model.IDcarrera != "P5")
            {
                ModelState.AddModelError(nameof(model.IDcarrera), "La carrera no es valida");
            }
        }
    }
}