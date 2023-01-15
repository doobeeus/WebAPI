using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social.Models;
using System.Data.SqlClient;
namespace Social.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PostsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AddPost")]

        public Response AddPost(Posts post)
        {
            Response response = new Response();
            // string connection, located in appsettings.json
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SNCon").ToString());
            Dal dal = new Dal();
            response = dal.AddPost(post, connection);
            return response;
        }

        [HttpPost]
        [Route("DeletePost")]
        public Response DeletePost(Posts post)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SNCon").ToString());
            Dal dal = new Dal();
            response = dal.DeletePost(post, connection);
            return response;
        }
    }
}
