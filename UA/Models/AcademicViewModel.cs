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
        public AcademicViewModel(List<CarreraViewModel> carreras)
        {
            foreach (var carrera in carreras)
            {
                this.Carreras[0].ID = carrera.ID;
                this.Carreras[0].Carrera = carrera.Carrera;
            }
        }
        #endregion

        public List<CarreraViewModel> Carreras { get; set; }

        public List<MateriaC> Materias { get; set; }

        #region Methods
        public CarreraViewModel ToEntity()
        {
            return new CarreraViewModel
            {
                ID = this.Carreras[0].ID,
                Carrera = this.Carreras[0].Carrera,
            };
        }
        #endregion

}


}