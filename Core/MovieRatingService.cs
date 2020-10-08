using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Core
{
    public class MovieRatingService : IMovieRatingService
    {
        private IMovieRatingRepository _movieRatingRepository;

        public MovieRatingService(IMovieRatingRepository movieRatingRepository)
        {
            _movieRatingRepository = movieRatingRepository;
        }

        //Extra method
        public int NumberOfMoviesWithGrade(int grade)
        {
            if (grade < 1 || grade > 5)
            {
                throw new ArgumentException("Grade must be 1 - 5");
            }

            HashSet<int> movies = new HashSet<int>();
            foreach (MovieRating rating in _movieRatingRepository.Ratings)
            {
                if (rating.Grade == grade)
                {
                    movies.Add(rating.Movie);
                }
            }
            return movies.Count;
        }

        //1. On input N, what are the number of reviews from reviewer N?
        public int GetNumberOfReviewsFromReviewer(int reviewer)
        {
            return _movieRatingRepository.Ratings
                .Where(r => r.Reviewer == reviewer)
                .Count();
        }

        //2. On input N, what is the average rate that reviewer N had given?
        public double GetAverageRateFromReviewer(int reviewer)
        {
            if (_movieRatingRepository.Ratings.Length == 0)
            {
                throw new ArgumentException("List is empty");

            }

            List<MovieRating> ratingsfromReviewer = _movieRatingRepository.Ratings.Where(rating => rating.Reviewer == reviewer).ToList();

            double sumRating = 0;

            foreach (MovieRating rating in ratingsfromReviewer)
            {

                sumRating += rating.Grade;


            }




            double AverageRateFromReviewer = sumRating / ratingsfromReviewer.Count;
            AverageRateFromReviewer = Math.Round(AverageRateFromReviewer, 2);

            return AverageRateFromReviewer;

        }

        //3. On input N and R, how many times has reviewer N given rate R?
        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            return _movieRatingRepository
                .Ratings
                .Where(rating => rating.Reviewer == reviewer)
                .Count(rating => rating.Grade == rate);
        }

        //4. On input N, how many have reviewed movie N?
        public int GetNumberOfReviews(int movie)
        {
            return _movieRatingRepository
                .Ratings
                .Count(rating => rating.Movie == movie);
        }

        //5. On input N, what is the average rate the movie N had received?
        public double GetAverageRateOfMovie(int movie)
        {
            try
            {
                return _movieRatingRepository.Ratings
                    .Where(rating => rating.Movie == movie)
                    .Average(rating => rating.Grade);
            }
            catch (InvalidOperationException e)
            {
                return 0;
            }

        }

        //6. On input N and R, how many times had movie N received rate R?
        public int GetNumberOfRates(int movie, int rate)
        {
            return _movieRatingRepository
                .Ratings
                .Where(rating => rating.Movie == movie)
                .Count(rating => rating.Grade == rate);
        }

        //7. What is the id(s) of the movie(s) with the highest number of top rates (5)?
        public List<int> GetMoviesWithHighestNumberOfTopRates()
        {
            var movie5 = _movieRatingRepository.Ratings
                .Where(r => r.Grade == 5)
                .GroupBy(r => r.Movie)
                .Select(group => new {
                    Movie = group.Key,
                    MovieGrade5 = group.Count()
                });

            int max5 = movie5.Max(grp => grp.MovieGrade5);

            return movie5
                .Where(grp => grp.MovieGrade5 == max5)
                .Select(grp => grp.Movie)
                .ToList();
        }

        //8. What reviewer(s) had done most reviews?
        public List<int> GetMostProductiveReviewers()
        {
            var reviewers = _movieRatingRepository.Ratings
                .GroupBy(r => r.Reviewer)
                .Select(group => new {
                    Review = group.Key,
                    NumberOfReviews = group.Count()
                });

            int max = reviewers.Max(grp => grp.NumberOfReviews);

            return reviewers
                .Where(grp => grp.NumberOfReviews == max)
                .Select(grp => grp.Review)
                .ToList();
        }

        //9. On input N, what is top N of movies? The score of a movie is its average rate.
        public List<int> GetTopRatedMovies(int amount)
        {
            return _movieRatingRepository
                .Ratings
                .GroupBy(r => r.Movie)
                .Select(grp => new
                {
                    Movie = grp.Key,
                    GradeAvg = grp.Average(x => x.Grade)
                })
                .OrderByDescending(grp => grp.GradeAvg)
                .Select(grp => grp.Movie)
                .Take(amount)
                .ToList();
        }

        /*10. On input N, what are the movies that reviewer N has reviewed? The list should
        be sorted decreasing by rate first, and date secondly.*/
        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            return _movieRatingRepository
                .Ratings
                .Where(rating => rating.Reviewer == reviewer)
                .OrderByDescending(rating => rating.Grade)
                .ThenByDescending(rating => rating.Date)
                .Select(rating => rating.Movie)
                .ToList();
        }

        /*11. On input N, who are the reviewers that have reviewed movie N? The list should
        be sorted decreasing by rate first, and date secondly.*/
        public List<int> GetReviewersByMovie(int movie)
        {
            return _movieRatingRepository
                .Ratings
                .Where(rating => rating.Movie == movie)
                .OrderByDescending(rating => rating.Grade)
                .ThenByDescending(rating => rating.Date)
                .Select(rating => rating.Reviewer)
                .ToList();
        }


    }
}
