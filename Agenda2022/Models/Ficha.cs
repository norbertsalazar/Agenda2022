using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Agenda2022.Models
{
    public class Ficha
    {
        [Key]
        public int FichaId { get; set; }

        [Required(ErrorMessage ="El campo {0}, es obligatorio")]
        [StringLength(50, MinimumLength =4, 
            ErrorMessage ="El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Programa { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(10, MinimumLength = 7,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Index("IndexCodigo", IsUnique = true)]
        public string Codigo   { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        //propiedad navigacional para la relacion con Aprendiz
        public virtual IEnumerable<Aprendiz> Aprendizs { get; set; }
        public virtual IEnumerable<FichaInstructor> FichaInstructors { get; set; }

    }
}