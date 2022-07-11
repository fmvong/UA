using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UA.Models
{
    
    public class ReporteViewModel
    {
        #region Constructor
        public ReporteViewModel() {}
        public ReporteViewModel(Inscripcion inscripcion)
        {
            this.IdAlumno = inscripcion.IdAlumno;
            this.IDMateria = inscripcion.IDMateria;
            this.Fecha = inscripcion.Fecha;
            this.Nota = inscripcion.Nota;
            this.Id = inscripcion.Id;
        }
        #endregion

        #region Properties
        
        [Required(ErrorMessage = "Debe cargar su Id")]
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Debe cargar Id de la Materia")]
        public string IDMateria { get; set; }
        public string Materia { get; set; }
        public string Fecha { get; set; }
        public int Nota { get; set; }
        public Guid Id { get; set; }
        #endregion

        #region Methods

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
        #endregion
    }


}