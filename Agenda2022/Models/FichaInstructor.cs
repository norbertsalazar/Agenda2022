using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agenda2022.Models
{
    public class FichaInstructor
    {
        [Key]
        public int FichaInstructorId { get; set; }
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        public int FichaId { get; set; }
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        public int InstructorId { get; set; }
        public virtual Ficha ficha { get; set; }
        public virtual Instructor instructor { get; set; }

    }
}