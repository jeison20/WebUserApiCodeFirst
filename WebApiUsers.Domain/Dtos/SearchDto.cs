using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiUsers.Domain.Dtos
{
    public class SearchDto
    {

        [MaxLength(50)]
        [RegularExpression(@"[a-zA-Z ]{2,80}", ErrorMessage = "Characters are not allowed.")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [RegularExpression(@"[a-zA-Z ]{2,80}", ErrorMessage = "Characters are not allowed.")]
        public string FirstLastName { get; set; }       
        public int PageNumber { get; set; }
        
        public int PageSize { get; set; }

        public int PageCount { get; set; }
    }
}
