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
        public InscripcionViewModel() {}
        public InscripcionViewModel(Inscripcion inscripcion)
        {
            this.IdAlumno = inscripcion.IdAlumno;
            this.IDMateria = inscripcion.IDMateria;
            this.Fecha = inscripcion.Fecha;
            this.Nota = inscripcion.Nota;
        }
        #endregion

        #region Properties

        #endregion
        [Required(ErrorMessage = "Debe cargar su Id")]
        public int IdAlumno { get; set; }
        [Required(ErrorMessage = "Debe cargar Id de la Materia")]
        public string IDMateria { get; set; }

        public string Fecha { get; set; }

        public int Nota { get; set; }

        #region Methods

        #endregion
        public Inscripcion AgregarId()
        {
            return new Inscripcion
            {
                IdAlumno = this.IdAlumno,
                IDMateria = this.IDMateria,
                //Fecha = " ",
                //Nota = null,
                Id = Guid.NewGuid(),
            };
        }
    }


}