using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UA.Models
{
    public class HistorialAluViewModel
    {
        public string IDMateria { get; set; }

        public string IDfecha { get; set; }

        public string nota { get; set; }
    }
}