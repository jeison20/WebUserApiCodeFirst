using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiUsers.Domain.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [Required]
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        [Required]
        public DateTime BornDate { get; set; }
        [Required]
        [Range(1, 999999999)]
        public long Sueldo { get; set; }
    }
}
