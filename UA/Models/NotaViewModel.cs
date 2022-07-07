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
        public string Fecha { get; set; }

        public int Nota { get; set; }

        [Key]
        public Guid Id { get; set; }

    }

}