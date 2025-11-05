using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedAt { get; set; }
        public uint LikesCount { get; set; }
        public uint CommentsCount { get; set; }
        public List<PostAttachment>? PostAttachments { get; set; }
    }
}