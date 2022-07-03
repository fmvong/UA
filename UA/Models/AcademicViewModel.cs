using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UA.Models
{
    
    public class AcademicViewModel
    {
        #region Constructor
        public AcademicViewModel() { }
        public AcademicViewModel(List<CarreraC> carreras)
        {
            foreach (var carrera in carreras)
            {
                this.Carreras[0].ID = carrera.ID;
                this.Carreras[0].Carrera = carrera.Carrera;
            }
        }
        #endregion

        public List<CarreraC> Carreras { get; set; }

        public List<MateriaC> Materias { get; set; }

        #region Methods
        public CarreraC ToEntity()
        {
            return new CarreraC
            {
                ID = this.Carreras[0].ID,
                Carrera = this.Carreras[0].Carrera,
            };
        }
        #endregion

}


}