using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Results
{
    public class UserUpdateResult
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public string? ProfileImageURL { get; set; }
        public string? CoverImageURL { get; set; }
    }
}