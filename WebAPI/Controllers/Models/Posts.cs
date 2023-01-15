using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.Models
{
    public class Posts
    {
        public int ID { get; set; } 
        public string Content { get; set; }
        public int UserID { get; set; }
        public string? Image { get; set; }

    }
}
