using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Agenda2022.Models
{
    public class Aprendiz
    {
        [Key]
        public int AprendizId {get; set;}

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(15, MinimumLength = 6,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Index("IndexDocumento", IsUnique = true)]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(40, MinimumLength = 3,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(40, MinimumLength = 3,
            ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        //Relacion con la clase Ficha
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        public int FichaId { get; set; }
        public virtual Ficha ficha { get; set; }
    }
}