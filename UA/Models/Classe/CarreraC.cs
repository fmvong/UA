using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UA.Models
{
    [Table("Carreras")]
    public class CarreraC
    {
        public string ID { get; set; }

        public string Carrera { get; set; }

    }


}