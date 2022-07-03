using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UA.Models
{
    
    public class InscripcionViewModel
    {
        #region Constructor
        public InscripcionViewModel() { }
        public InscripcionViewModel(Inscripcion inscripcion)
        {
            this.IdAlumno = inscripcion.IdAlumno;
            this.IDMateria = inscripcion.IDMateria;
        }
        #endregion

        #region Properties

            [Required(ErrorMessage = "Debe cargar su Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe cargar su Apellido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Debe cargar su Carrera")]
        public string IDcarrera { get; set; }

        #endregion
        public int IdAlumno { get; set; }

        public string IDMateria { get; set; }

        #region Methods
        public Alumno ToEntity(int ultimoAlu)
        {
            return new Alumno
            {
                Nombre = this.Nombre,
                Apellido = this.Apellido,
                IDcarrera = this.IDcarrera,
                ID = ultimoAlu+1,
            };
        }
        #endregion
        public Inscripcion AgregarId(int ultimaIns)
        {
            return new Inscripcion
            {
                IdAlumno = this.IdAlumno,
                IDMateria = this.IDMateria,
                Fecha = "",
                Nota = "",
                Id = ultimaIns + 1,
            };
        }
    }


}