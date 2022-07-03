using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UA.Models
{
    [Table("Historial")]
    public class Historial
    {
        [ForeignKey("Alumno")]
        public int IdAlumno { get; set; }
        [ForeignKey("Materia")]
        public string IDMateria { get; set; }

        public string Fecha { get; set; }

        public string Nota { get; set; }

        /*[Key]
        public Guid Id { get; set; }*/

    }


}