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
            // parameters to prevent SQL Injection
            register.Parameters.AddWithValue("@Name", registration.Name);
            register.Parameters.AddWithValue("@Email", registration.Email);
            register.Parameters.AddWithValue("@Password", registration.Password);

            // if count > 0, user exists with email aor name
            SqlCommand checkIfExists = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email OR Name = @Name;", connection);
            checkIfExists.Parameters.AddWithValue("@Email", registration.Email);
            checkIfExists.Parameters.AddWithValue("@Name", registration.Name);
            connection.Open();
            int UserExist = (int)checkIfExists.ExecuteScalar();

            // if exists, return error
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
            // Data Adapter is used to fill a dataset
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
                // These are so we can access id, name, and email in the react frontend by including it in json
                foreach (DataRow row in dt.Rows)
                {
                    // response.Registration.Name, etc
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
        public Response AddPost(Posts post, SqlConnection connection)
        {
            Response response = new Response();

            SqlCommand addpostcomm = new SqlCommand("INSERT into Posts(Content, UserID, Image) VALUES(@Content, @UserID, @Image);", connection);
            // parameters to prevent SQL Injection
            addpostcomm.Parameters.AddWithValue("@Content", post.Content);
            addpostcomm.Parameters.AddWithValue("@UserID", post.UserID);
            addpostcomm.Parameters.AddWithValue("@Image", post.Image);

            connection.Open();

            int i = addpostcomm.ExecuteNonQuery();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Post created successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Post creation failed";
            }
            return response;
            }
        public Response DeletePost(Posts post, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand deletepostcomm = new SqlCommand("DELETE from POSTS where UserID = @UserID and Content = @Content and Image = @Image;", connection);
            deletepostcomm.Parameters.AddWithValue("@Content", post.Content);
            deletepostcomm.Parameters.AddWithValue("@UserID", post.UserID);
            deletepostcomm.Parameters.AddWithValue("@Image", post.Image);

            connection.Open();
            int checkExists = (int)deletepostcomm.ExecuteScalar();

            if (checkExists > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Post deleted successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Post deletion failed";
            }



            return response;

        }
    }
}
