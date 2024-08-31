using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiUsers.Domain.Dtos
{
    public class ResponseDto<T>
    {
        public string? Response { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
        public int PageCount { get; set; }
    }
}
