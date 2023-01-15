using System.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Social.Models
{
    public class Dal 
    {
        public Response Registration(Registration registration, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand register = new SqlCommand("INSERT into Users(Name, Email, Password) VALUES(@Name, @Email, @Password);", connection);
            register.Parameters.AddWithValue("@Name", registration.Name);
            register.Parameters.AddWithValue("@Email", registration.Email);
            register.Parameters.AddWithValue("@Password", registration.Password);

            SqlCommand checkIfExists = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email OR Name = @Name;", connection);
            checkIfExists.Parameters.AddWithValue("@Email", registration.Email);
            checkIfExists.Parameters.AddWithValue("@Name", registration.Name);
            connection.Open();
            int UserExist = (int)checkIfExists.ExecuteScalar();


            if (UserExist > 0)
            {
                response.StatusCode = 100;
                response.StatusMessage = "User or Email already is registered";
                return response;
            }
            else
            {
                int i = register.ExecuteNonQuery();

                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Registration Successful";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Registration Failed";
                }
                return response;
            }
        }

        public Response Login(Registration registration, SqlConnection connection)
        {
            SqlDataAdapter login = new SqlDataAdapter("SELECT * FROM Users WHERE Email = @Email AND Password = @Password  ;", connection);
            login.SelectCommand.Parameters.AddWithValue("@Email", registration.Email);
            login.SelectCommand.Parameters.AddWithValue("@Password", registration.Password);
            DataTable dt = new DataTable();
            login.Fill(dt);
            Response response = new Response();
            connection.Open();
            if (dt.Rows.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Login Successful";
                Registration reg = new Registration();
                // These are so we can access id, name, and email in the react frontend
                foreach (DataRow row in dt.Rows)
                {
                    reg.ID = Convert.ToInt32(row["ID"]);
                    reg.Name = Convert.ToString(row["Name"]);
                    reg.Email = Convert.ToString(row["Email"]);
                }
                response.Registration = reg;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Login Failed";
            }
            return response;
        }
    }
}
