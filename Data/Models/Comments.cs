using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data.Models
{
    public class Comments
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Comment { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}