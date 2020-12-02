using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medics.Models.ViewModels
{
    public class MedicViewModel
    {
        public int IdMedic { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public int? Matricula { get; set; }
    }
}