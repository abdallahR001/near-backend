using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserCategory> UserCategories { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }
    }
}