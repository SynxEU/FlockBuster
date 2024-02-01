using Azure;
using Flockbuster.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Flockbuster.Domain
{
    public class SQLConn
    {
        private readonly string connectionString;
        public SQLConn(IConfiguration configuration) => connectionString = configuration.GetConnectionString("Default");

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
                        TTW = reader.GetInt32("TTW"),
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
            using (SqlConnection con = new(connectionString))
            { 
                con.Open();
                SqlCommand cmd = new SqlCommand("GetUserByMail", con) { CommandType= CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@Mail", mail);
                cmd.ExecuteNonQuery();

                Users user = new Users();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user = (new Users
                    {
                        Id = reader.GetInt32("ID"),
                        Name = reader.GetString("Name"),
                        Age = reader.GetInt32("Age"),
                        Email = reader.GetString("E-Mail"),
                        Balance = reader.GetInt32("Balance"),
                        IsAdmin = reader.GetBoolean("Admin")
                    });
                }
                return user;
            }
        }
        public Genre GetGenreById(int id)
        {
            Genre genre = new Genre();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetGenreById", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@GenreId", id);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    genre.GenreName = reader.GetString("Genre");
                }
            }
            return genre;
        }
        public Movies GetMoviesById(int id)
        {
            Movies movie = new Movies();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetMovieById", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@MovieId", id);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    movie.Id = reader.GetInt32("ID");
                    movie.Title = reader.GetString("Title");
                    movie.RequiredAge = reader.GetInt32("Age Rating");
                    movie.TTW = reader.GetInt32("TTW");
                    movie.RelaseDate = reader.GetString("Release Date");
                    movie.Price = reader.GetInt32("Price");
                }
            }
            return movie;
        }
        public Users GetUsersById(int id)
        {
            Users user = new Users();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetUserById", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@UserId", id);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.Id = reader.GetInt32("ID");
                    user.Name = reader.GetString("Name");
                    user.Age = reader.GetInt32("Age");
                    user.Email = reader.GetString("E-Mail");
                    user.Balance = reader.GetInt32("Balance");
                    user.IsAdmin = reader.GetBoolean("Admin");
                }
            }
            return user;
        }
        public BorrowedMovie GetBorrowedMovieById(int id)
        {
            BorrowedMovie borrowedMovie = new BorrowedMovie();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetBorrowedMoviesById", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@UserId", id);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    borrowedMovie.Id = reader.GetInt32("ID");
                    borrowedMovie.UserID = reader.GetInt32("User ID");
                    borrowedMovie.MovieID = reader.GetInt32("Movie ID");
                    borrowedMovie.IsBorrowed = reader.GetBoolean("IsBorrowed");
                    borrowedMovie.WasBorrowed = reader.GetBoolean("WasBorrowed");
                }
            }
            return borrowedMovie;
        }
        public List<Genre> GetMovieGenre(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Genre> listGenres = new List<Genre>();
                conn.Open();
                SqlCommand com = new SqlCommand("GetMovieGenre", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Movie_ID", id);
                com.ExecuteNonQuery();

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    listGenres.Add(new Genre
                    {
                        GenreName = reader.GetString("Genre"),
                    });
                }
                return listGenres;
            }
        }
        public Users GetUserDetailsLogin(string mail, string password)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                Users user = new Users();
                con.Open();
                SqlCommand cmd = new("GetLogin", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@Mail", mail);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.Id = reader.GetInt32("ID");
                    user.Name = reader.GetString("Name");
                }
                return user;
            }
        }
        public Users CreateUser(string name, int age, string mail, string password)
        {
            Users user = new Users();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateUser", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Mail", mail);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.ExecuteNonQuery();
                user.Name = name;
                user.Age = age;
                user.Email = mail;
                user.Password = password;
            }
            return user;
        }
        public Movies CreateMovie(string title, int ageRating, int hour, int minutes, DateTime releaseDate, int price, int genreId) 
        { 
            int seconds = ((hour * 60) + minutes) * 60;

            Movies movie = new Movies();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateMovie", con) { CommandType = CommandType.StoredProcedure };
                SqlCommand cmd2 = new SqlCommand("CreateGenreConnection", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@AgeRating", ageRating);
                cmd.Parameters.AddWithValue("@TTW", seconds);
                cmd.Parameters.AddWithValue("@ReleaseDate", releaseDate.ToShortDateString());
                cmd.Parameters.AddWithValue("@Price", price);
                cmd2.Parameters.AddWithValue("@Genre_ID", genreId);
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                movie.Title = title;
                movie.RequiredAge = ageRating;
                movie.TTW = seconds;
                movie.RelaseDate = releaseDate.ToShortDateString();
                movie.Price = price;
            }
            return movie;
        }
        public Users UpdateUser(int id, string name, int age, string mail) 
        {
            Users user = new Users();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateUser", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@UserID", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Mail", mail);
                cmd.ExecuteNonQuery();
                user.Name = name;
                user.Age = age;
                user.Email = mail;
            }
            return user;
        }
        public Users UpdateUserPassword(int id, string password) 
        {
            Users user = new Users();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateUserPassword", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@UserID", id);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.ExecuteNonQuery();
                user.Password = password;
            }
            return user;
        }
        public Movies UpdateMovie(int id, string title, int ageRating, int hour, int minutes, DateTime releaseDate, int price)
        {
            int seconds = ((hour * 60) + minutes) * 60;

            Movies movie = new Movies();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateMovie", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@MovieID", id);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@AgeRating", ageRating);
                cmd.Parameters.AddWithValue("@TTW", seconds);
                cmd.Parameters.AddWithValue("@ReleaseDate", releaseDate.ToShortDateString());
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.ExecuteNonQuery();
                movie.Title = title;
                movie.RequiredAge = ageRating;
                movie.TTW = seconds;
                movie.RelaseDate = releaseDate.ToShortDateString();
                movie.Price = price;
            }
            return movie;
        }
        public Users Admin(int id, bool admin)
        {
            Users users = new Users();
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Admins", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@ID", id);
                if (admin) { cmd.Parameters.AddWithValue("@Admin", 1); }
                else { cmd.Parameters.AddWithValue("@Admin", 0); }
            }
            return users;
        }
        public void DeleteMovie(int id)
        {
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteMovie", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@MovieID", id);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteUser(int id)
        {
            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteUser", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@UserID", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}