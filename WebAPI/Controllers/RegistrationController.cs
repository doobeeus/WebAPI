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
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Registration")]

        public Response Registration(Registration registration)
        {
            Response response = new Response();
            // string connection
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SNCon").ToString());
            Dal dal = new Dal();
            response = dal.Registration(registration, connection);
            return response;
        }

        [HttpPost]
        [Route("Login")]
        public Response Login(Registration registration)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SNCon").ToString());
            Dal dal = new Dal();
            response = dal.Login(registration, connection);
            return response;
        }
    }
}
