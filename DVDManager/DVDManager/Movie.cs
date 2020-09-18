using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDManager {

    /// <summary>
    /// A Class that contain the detail of a specific Movie.
    /// </summary>
    class Movie {

        /// <summary>
        /// The title of this Movie
        /// </summary>
        public string title { get; }
        /// <summary>
        /// The Star of this Movie
        /// </summary>
        public string starring { get; }
        /// <summary>
        /// The Director of this Movie
        /// </summary>
        public string director { get; }
        /// <summary>
        /// The Release Date of this Movie
        /// </summary>
        public string releaseDate { get; }
        /// <summary>
        /// The Genre of this Movie
        /// </summary>
        public Genre genre { get; }
        /// <summary>
        /// The Classification of this Movie
        /// </summary>
        public Classification classification { get; }
        /// <summary>
        /// The duration of this Movie
        /// </summary>
        public string duration { get; }

        /// <summary>
        /// The available number of copy of this Movie
        /// </summary>
        public int numberOfAvailableCopy { get; set; }
        /// <summary>
        /// The number this movie has been rented
        /// </summary>
        public int timesRented { get; set; }

        /// <summary>
        /// Constructor for this Movie Class.
        /// </summary>
        /// <param name="title">The title of this Movie</param>
        /// <param name="genre">The genre of this Movie</param>
        /// <param name="director">The director of this Movie</param>
        /// <param name="starring">The star of this Movie</param>
        /// <param name="releaseDate">The release date of this Movie</param>
        /// <param name="classification">The classification of this Movie</param>
        /// <param name="duration">The duration of this Movie</param>
        /// <param name="numberOfCopy">The available number of copy of this Movie</param>
        public Movie(string title, Genre genre, string director, string starring, string releaseDate, Classification classification, string duration, int numberOfCopy) {
            this.title = title;
            this.genre = genre;
            this.director = director;
            this.starring = starring;
            this.releaseDate = releaseDate;
            this.classification = classification;
            this.duration = duration;
            this.numberOfAvailableCopy = numberOfCopy;
            this.timesRented = 0;
        }


    }
}
