using Flockbuster.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Runtime.CompilerServices;

namespace Flockbuster.Domain
{
    public class SQLConn
    {
        private readonly string connectionString;
        public SQLConn(IConfiguration configuration) { connectionString = configuration.GetConnectionString("Default"); }

        public List<Movies> GetMovies()
        {
            List<Movies> listMovie = new List<Movies>();
            using (SqlConnection con = new(connectionString)) 
            { 
                con.Open();
                SqlCommand cmd = new("GetAllMovies", con) { CommandType = CommandType.StoredProcedure };
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { 
                    listMovie.Add(new Movies
                    {
                        Id = reader.GetInt32("ID"),
                        Title = reader.GetString("Title"),
                        RequiredAge = reader.GetInt32("Age Rating"),
                        TTW = reader.GetInt64("TTW"),
                        RelaseDate = reader.GetString("Release Date"),
                        Price = reader.GetInt32("Price")
                    });
                }
            }
            return listMovie;
        }
        public List<Users> GetUsers()
        {
            List<Users> listUser = new List<Users>();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new("GetAllMovies", con) { CommandType = CommandType.StoredProcedure };
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listUser.Add(new Users
                    {
                        Id = reader.GetInt32("ID"),
                        Name = reader.GetString("Name"),
                        Age = reader.GetInt32("Age"),
                        Email = reader.GetString("E-Mail"),
                        Password = reader.GetString("Password"),
                        Balance = reader.GetInt32("Balance"),
                        IsAdmin = reader.GetBoolean("Admin")
                    });
                }
            }
            return listUser;
        }
        public List<BorrowedMovie> GetBorrowedMovies()
        {
            List<BorrowedMovie> listBorrowedMovies = new List<BorrowedMovie>();
            using (SqlConnection con = new(connectionString))
            { 
                con.Open();
                SqlCommand cmd = new("GetAllBorrowedMovies", con) { CommandType= CommandType.StoredProcedure };
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBorrowedMovies.Add(new BorrowedMovie
                    { 
                        Id = reader.GetInt32("ID"),
                        MovieID = reader.GetInt32("Movie ID"),
                        UserID = reader.GetInt32("User ID"),
                        IsBorrowed = reader.GetBoolean("IsBorrowed"),
                        WasBorrowed = reader.GetBoolean("WasBorrowed")
                    });
                }
            }
            return listBorrowedMovies;
        }
        public List<Genre> GetGenres() 
        { 
            List<Genre> listGenres = new List<Genre>();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new("GetAllGenres", con) { CommandType = CommandType.StoredProcedure };
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listGenres.Add(new Genre
                    {
                        Id = reader.GetInt32("ID"),
                        GenreName = reader.GetString("Genre")
                    });
                }
            }
            return listGenres;
        }
        public Users GetUserByMail(string mail)
        {
            Users user = new Users();
            using (SqlConnection con = new(connectionString))
            { 
                con.Open();
                SqlCommand cmd = new SqlCommand("GetUserByMail", con) { CommandType= CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@Mail", mail);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { 
                    user.Id = reader.GetInt32("ID");
                }
            }
            return user;
        }
    }
}