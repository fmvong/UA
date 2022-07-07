using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UA.Models
{
    
    public class LogInViewModel
    {
        #region Constructor
        public LogInViewModel() { }
        
        #endregion

        #region Properties

        [Required(ErrorMessage = "Debe cargar su Id")]
        public int Id { get; set; }
    
        #endregion

        #region Methods
        
        #endregion

    }


}