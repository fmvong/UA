using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UA.Models
{
    public class MateriaIdCarrerasViewModel
    {
        [Key]
        [Required(ErrorMessage = "Debe cargar un Id valido")]
        public string ID { get; set; }
        [Required(ErrorMessage = "Debe cargar el nombre de la materia")]
        public string Materia { get; set; }
        [Required(ErrorMessage = "Debe cargar el Id de la carrera")]
        public string IDcarrera { get; set; }

        public string IDcorrelativa1 { get; set; }

        public string IDcorrelativa2 { get; set; }
        [Required(ErrorMessage = "Debe cargar el semestre")]
        public int Semestre { get; set; }

        public List<string> CarrerasID {get; set; } 
    }
}