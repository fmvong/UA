using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UA.Models
{
    
    public class AlumnoViewModel
    {
        #region Constructor
        public AlumnoViewModel() { }
        public AlumnoViewModel(Alumno alumno)
        {
            this.Nombre = alumno.Nombre;
            this.Apellido = alumno.Apellido;
            this.IDcarrera = alumno.IDcarrera;
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

    }


}