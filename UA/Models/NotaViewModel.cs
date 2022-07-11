using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UA.Models
{
    public class NotaViewModel
    {
        [Required(ErrorMessage = "Debe cargar fecha de evaluación")]
        public string Fecha { get; set; }
        [Required(ErrorMessage = "Debe cargar la nota")]
        public int Nota { get; set; }

        [Key]
        public Guid Id { get; set; }

    }

}