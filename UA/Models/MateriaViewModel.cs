using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace UA.Models
{
    public class MateriaViewModel
    {
        public int Id { get; set; }

        public string Materia { get; set; }

        public string IDcarrera { get; set; }
    }
}