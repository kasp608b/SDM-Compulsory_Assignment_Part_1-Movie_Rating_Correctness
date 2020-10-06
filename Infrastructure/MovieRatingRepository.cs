using System;
using System.Collections.Generic;
using System.IO;
using Core;
using Entities;
using Newtonsoft.Json;

namespace Infrastructure
{
    public class MovieRatingRepository : IMovieRatingRepository
    {
        public List<MovieRating> Ratings { get; private set; }
        

        public MovieRatingRepository(string fileName)
        {
            Ratings = ReadAllMovieRatings(fileName);
        }


        public List<MovieRating> ReadAllMovieRatings(string fileName)
        {
            List<MovieRating> ratingsList = new List<MovieRating>();
            using (StreamReader sr = new StreamReader(fileName))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        MovieRating m = GetOneMovieRating(reader);
                        ratingsList.Add(m);
                    }
                }
            }

            return ratingsList;
        }

        private MovieRating GetOneMovieRating(JsonReader reader)
        {
            MovieRating rating = new MovieRating();

            reader.Read();
            rating.Reviewer = (int) reader.ReadAsInt32();

            reader.Read();
            rating.Movie = (int) reader.ReadAsInt32();

            reader.Read();
            rating.Grade = (int) reader.ReadAsInt32();

            reader.Read();
            rating.Date = (DateTime) reader.ReadAsDateTime();

            return rating;
        }

    }
}