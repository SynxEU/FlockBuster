using Flockbuster.Domain;
using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flockbuster.Service.Methods
{
    public class Movie : IMovie
    {
        SQLConn _connection;

        public Movie(IConfiguration configuration) => _connection = new SQLConn(configuration);

        public List<Movies> GetMovies() => _connection.GetMovies();
        public List<BorrowedMovie> GetBorrowedMovies() => _connection.GetBorrowedMovies();
        public List<Genre> GetGenres() => _connection.GetGenres();
        public Genre GetGenreById(int id) => _connection.GetGenreById(id);
        public Movies GetMoviesById(int id) => _connection.GetMoviesById(id);
        public BorrowedMovie GetBorrowedMovieById(int id) => _connection.GetBorrowedMovieById(id);
        public List<Genre> GetMovieGenre(int id) => _connection.GetMovieGenre(id);
        public Movies CreateMovie(string title, int ageRating, int hour, int minutes, DateTime releaseDate, int price, int genreId) => _connection.CreateMovie(title, ageRating, hour, minutes, releaseDate, price, genreId);
        public void DeleteMovie(int id) => _connection?.DeleteMovie(id);
        public Movies UpdateMovie(int id, string title, int ageRating, int hour, int minutes, DateTime releaseDate, int price) => _connection.UpdateMovie(id, title, ageRating, hour, minutes, releaseDate, price);
        public void UpdateMoviePicture(int id, string filePath) => _connection?.UpdateMoviePicture(id, filePath);
        public int GetMovieByTitle(string title) => _connection.GetMovieByTitle(title);
    }
}
