using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Image { get; set; }
        public string? CoverImage { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string? Location { get; set; }
        public uint FollowersCount { get; set; }
        public uint FollowingCount { get; set; }
        public uint PostsCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Post>? Posts { get; set; }
        public ICollection<Followers> Followers { get; set; } = new List<Followers>();

        public ICollection<Followers> Following { get; set; } = new List<Followers>();

        public ICollection<Likes> Likes { get; set; }
        public ICollection<Comments> Comments { get; set; }
}

    public enum Gender{
        Male,
        Female,
    }
}