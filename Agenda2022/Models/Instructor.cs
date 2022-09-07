using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Agenda2022.Models
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(15, MinimumLength = 6,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Index("IndexDocumento", IsUnique = true)]
        [RegularExpression(@"[0-9]*" , ErrorMessage ="Solo se permiten valores numericos")]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(40, MinimumLength = 3,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [RegularExpression(@"[A-Za-z ]*", ErrorMessage = "Solo se permiten letras en este campo")]

        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(40, MinimumLength = 3,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Apellidos { get; set; }

        public string FullName { get { return (this.Nombres + " " + this.Apellidos);  } }

        [Range(3000000000, 3999999999, ErrorMessage ="Ingrese un numero de celular valido")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        //Relacion con ficha Instructor 
        public virtual IEnumerable<FichaInstructor> FichaInstructors { get; set; }
        

    }
}