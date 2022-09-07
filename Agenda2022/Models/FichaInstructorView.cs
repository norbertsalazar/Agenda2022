using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agenda2022.Models
{
    public class FichaInstructorView
    {
        public int FichaId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int InstructorId { get; set; }
    }
}