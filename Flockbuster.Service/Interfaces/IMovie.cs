using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flockbuster.Domain.Models;

namespace Flockbuster.Service.Interfaces
{
    public interface IMovie
    {
        List<Movies> GetMovies();
        List<BorrowedMovie> GetBorrowedMovies();
        List<Genre> GetGenres();
        Genre GetGenreById(int id);
        Movies GetMoviesById(int id);
        BorrowedMovie GetBorrowedMovieById(int id);
        List<Genre> GetMovieGenre(int id);
        Movies CreateMovie(string title, int ageRating, int hour, int minutes, DateTime releaseDate, int price, int genreId);
        void DeleteMovie(int id);
        Movies UpdateMovie(int id, string title, int ageRating, int hour, int minutes, DateTime releaseDate, int price);
        void UpdateMoviePicture(int id, string filePath);
        int GetMovieByTitle(string title);
    }
}
