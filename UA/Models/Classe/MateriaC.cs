using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UA.Models
{
    [Table("Materias")]
    public class MateriaC
    {
        public string ID { get; set; }

        public string Materia { get; set; }

        public string IDcarrera { get; set; }

        public string Correlativa1 { get; set; }

        public string Correlativa2 { get; set; }

        public int Semestre { get; set; }
    }
}