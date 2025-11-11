using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class UpdateProfileRequestDTO
    {
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public string? ProfileImageURL { get; set; }
        public string? CoverImageURL { get; set; }
    }
}