using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UA.Models
{
    public class MateriaLogInViewModel
    {   
        [Key]
        [Required(ErrorMessage = "Debe cargar el Id de la materia")]
        public string ID { get; set; }

        
    }
}