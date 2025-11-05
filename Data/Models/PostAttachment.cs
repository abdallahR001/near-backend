using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data.Models
{
    public class PostAttachment
    {
        public int Id { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public string URL { get; set; }
        public string MimeType { get; set; }
        public PostType Type { get; set; }
    }

    public enum PostType {
        Image,
        Video
    }
}