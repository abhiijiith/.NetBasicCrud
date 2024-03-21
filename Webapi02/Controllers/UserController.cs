using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;



namespace Webapi02.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            try
            {
                //String connectionString = "Data Source=DESKTOP-JCV8357\\SQLEXPRESS;Initial Catalog=Mystore;Integrated Security=True;";
                String connectionString = "Data Source=DESKTOP-JCV8357\\SQLEXPRESS;Initial Catalog=Mystore;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                { 
                    connection.Open();
                    String sql = "Select * From clients";
                    using(SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())  
                        {
                            while (reader.Read())
                            {
                                Client client = new Client();

                                client.id = reader.GetInt32(0).ToString();
                                client.name = reader.GetString(1);
                                client.email = reader.GetString(2);
                                client.phone = reader.GetString(3);
                                client.address = reader.GetString(4);
                                client.createdAt = reader.GetDateTime(5).ToString();

                                clients.Add(client);
                            }

                            return (clients);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost]
        public void PutClients(Client client)
        {
            if (client.name.Length == 0 || client.email.Length == 0 || client.phone.Length == 0 || client.address.Length == 0)
            {
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Mystore;Integrated Security=True;";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "insert into clients (name,email,phone,address) VALUES (@name,@email,@phone,@address)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", client.name);
                        command.Parameters.AddWithValue("@email", client.email);
                        command.Parameters.AddWithValue("@phone", client.phone);
                        command.Parameters.AddWithValue("@address",client.address);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        [HttpGet]
        public Client GetClientsById([FromQuery] string name, [FromQuery] string email)
        {
            Client clientInfo = new Client();
            try
            {
                //String connectionString = "Data Source=DESKTOP-JCV8357\\SQLEXPRESS;Initial Catalog=Mystore;Integrated Security=True;";
                String connectionString = "Data Source=DESKTOP-JCV8357\\SQLEXPRESS;Initial Catalog=Mystore;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * From clients where name =@name and email=@email";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@email", email);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.createdAt = reader.GetDateTime(5).ToString();

                            }
                            return (clientInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
    public class Client
    {
        public String id {  get; set; }
        public String name { get; set; }
        public String email {  get; set; }
        public String phone { get; set; }
        public String address { get; set; }
        public String createdAt { get; set; }
    }


    public class Model
    {
        public String name { get; set; }
        public String email { get; set; }
    }
}

