using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UA.Models
{
    public class CarreraViewModel
    {

        [Required(ErrorMessage = "Debe cargar un Id válido")]
        public string ID { get; set; }

        [Required(ErrorMessage = "Debe cargar el nombre de la carrera")]
        public string Carrera { get; set; }

    }


}