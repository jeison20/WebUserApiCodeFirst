using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiUsers.Domain.Dtos
{
    public class CreateUserDto
    {
        
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"[a-zA-Z ]{2,80}", ErrorMessage = "Characters are not allowed.")]
        public string FirstName { get; set; }

       
        [MaxLength(50)]
        [RegularExpression(@"[a-zA-Z ]{2,80}", ErrorMessage = "Characters are not allowed.")]
        public string SecondName { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"[a-zA-Z ]{2,80}", ErrorMessage = "Characters are not allowed.")]
        public string FirstLastName { get; set; }

        
        [MaxLength(50)]
        [RegularExpression(@"[a-zA-Z ]{2,80}", ErrorMessage = "Characters are not allowed.")]
        public string SecondLastName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BornDate { get; set; }

        [Required]
        [Range(1,999999999)]
        public long Sueldo { get; set; }

    }
}
