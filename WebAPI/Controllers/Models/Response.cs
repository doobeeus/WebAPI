using Social.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<Registration>? listRegistration { get; set; }
        public Registration? Registration { get; set; }

        public Posts? Posts { get; set; }
        public List<Posts>? listPosts { get; set; }
    }
}
