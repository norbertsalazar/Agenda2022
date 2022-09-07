using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agenda2022.Models
{
    public class FichaDetailsView
    {
        public int FichaId { get; set; }
        public string Programa { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<FichaInstructor> FichaInstructors { get; set; }

    }
}