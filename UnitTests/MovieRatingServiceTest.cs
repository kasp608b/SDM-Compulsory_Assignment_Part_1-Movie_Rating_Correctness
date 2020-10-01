using System;
using System.Collections.Generic;
using System.IO;
using Core;
using Entities;
using Moq;
using Xunit;

namespace UnitTests
{
    public class MovieRatingServiceTest
    {
        private Mock<IMovieRatingRepository> repoMock;
        private List<MovieRating> ratings;

        public MovieRatingServiceTest()
        {
            repoMock = new Mock<IMovieRatingRepository>();
            repoMock.Setup(x => x.ReadAll()).Returns(() => ratings);
        }

        //Extra test.

        [Theory]
        [InlineData(1, 0)]
        [InlineData(3, 1)]
        [InlineData(5, 2)]
        public void NumberOfMoviesWithGrade(int grade, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 3, DateTime.Now),
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 4, DateTime.Now),

                new MovieRating(3, 5, 5, DateTime.Now),
                new MovieRating(3, 2, 5, DateTime.Now),
                new MovieRating(4, 2, 5, DateTime.Now)
            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act
            int result = mrs.NumberOfMoviesWithGrade(grade);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Once);
        }

       
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(6)]
        public void NumberOfMoviesWithGradeInvalidExpectArgumentException(int grade)
        {
            // arrange
            Mock<IMovieRatingRepository> repoMock = new Mock<IMovieRatingRepository>();
            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                int result = mrs.NumberOfMoviesWithGrade(grade);
            });

            // assert
            Assert.Equal("Grade must be 1 - 5", ex.Message);
        }


        //  1. On input N, what are the number of reviews from reviewer N? 
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void GetNumberOfReviewsFromReviewer(int reviewer, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 4, DateTime.Now),
                new MovieRating(3, 2, 3, DateTime.Now),
                new MovieRating(4, 1, 4, DateTime.Now)
            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act

            int result = mrs.GetNumberOfReviewsFromReviewer(reviewer);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Once);
        }


        // 2. exception
        [Theory]
        [InlineData(1)]
        public void GetAverageRateFromReviewerInvalidExpectArgumentException(int reviewer)
        {
            // arrange
            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            ratings = new List<MovieRating>
            {

            };
            
            // act
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                double result = mrs.GetAverageRateFromReviewer(reviewer);
            });

            // assert
            Assert.Equal("List is empty", ex.Message);
        }

        // 2. On input N, what is the average rate that reviewer N had given?
        [Theory]
        [InlineData(2, 3.67)]
        [InlineData(3, 4.5)]
        [InlineData(4, 1.5)]
        public void GetAverageRateFromReviewer(int reviewer, double expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(2, 2, 4, DateTime.Now),
                new MovieRating(2, 3, 4, DateTime.Now),
                new MovieRating(3, 1, 5, DateTime.Now),
                new MovieRating(3, 2, 5, DateTime.Now),
                new MovieRating(3, 3, 5, DateTime.Now),
                new MovieRating(3, 2, 3, DateTime.Now),
                new MovieRating(4, 1, 2, DateTime.Now),
                new MovieRating(4, 2, 2, DateTime.Now),
                new MovieRating(4, 3, 1, DateTime.Now),
                new MovieRating(4, 4, 1, DateTime.Now),
            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act

            double result = mrs.GetAverageRateFromReviewer(reviewer);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Exactly(2));
        }

        // 3. On input N and R, how many times has reviewer N given rate R?
        [Theory]
        [InlineData(2, 3, 1)]
        [InlineData(2, 4, 2)]
        [InlineData(3, 5, 3)]
        [InlineData(4, 3, 0)]
        public void GetNumberOfRatesByReviewer(int reviewer, int rate, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(2, 2, 4, DateTime.Now),
                new MovieRating(2, 3, 4, DateTime.Now),
                new MovieRating(3, 1, 5, DateTime.Now),
                new MovieRating(3, 2, 5, DateTime.Now),
                new MovieRating(3, 3, 5, DateTime.Now),
                new MovieRating(3, 2, 3, DateTime.Now),
                new MovieRating(4, 1, 2, DateTime.Now),
                new MovieRating(4, 2, 2, DateTime.Now),
                new MovieRating(4, 3, 1, DateTime.Now),
                new MovieRating(4, 4, 1, DateTime.Now),
            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act

            int result = mrs.GetNumberOfRatesByReviewer(reviewer, rate);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Once);
        }

        // 4. On input N, how many have reviewed movie N?
        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 1)]
        [InlineData(3, 0)]
        public void GetNumberOfReviews(int movie, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(2, 2, 4, DateTime.Now),
                new MovieRating(3, 1, 5, DateTime.Now),
                new MovieRating(4, 1, 2, DateTime.Now),
            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act

            int result = mrs.GetNumberOfReviews(movie);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Once);
        }

        //5. On input N, what is the average rate the movie N had received?
        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 0)]
        [InlineData(3, 5)]
        public void GetAverageRateOfMovie(int movie, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
               new MovieRating(1,1,3, DateTime.Now),
               new MovieRating(2,1,3, DateTime.Now),
               new MovieRating(3,1,3, DateTime.Now),
               new MovieRating(1,3,5, DateTime.Now),

            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act

            double result = mrs.GetAverageRateOfMovie(movie);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Once);
        }

        // 6. On input N and R, how many times had movie N received rate R?
        [Theory]
        [InlineData(1, 1, 0)]
        [InlineData(2, 4, 1)]
        [InlineData(1, 3, 2)]
        public void GetNumberOfRates(int movie, int rate, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(2, 2, 4, DateTime.Now),
                new MovieRating(3, 1, 5, DateTime.Now),
                new MovieRating(4, 1, 3, DateTime.Now),
            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act

            int result = mrs.GetNumberOfRates(movie, rate);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Once);
        }


        // 7. What is the id(s) of the movie(s) with the highest number of top rates (5)? 
        [Fact]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 5, DateTime.Now),
                new MovieRating(1, 2, 5, DateTime.Now),

                new MovieRating(2, 1, 4, DateTime.Now),
                new MovieRating(2, 2, 5, DateTime.Now),

                new MovieRating(2, 3, 5, DateTime.Now),
                new MovieRating(3, 3, 5, DateTime.Now),
            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            List<int> expected = new List<int>() { 2, 3 };

            // act
            var result = mrs.GetMoviesWithHighestNumberOfTopRates();

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Once);

        }

        // 8. What reviewer(s) had done most reviews?
        [Fact]
        public void GetMostProductiveReviewers()
        {
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 5, DateTime.Now),
                new MovieRating(1, 2, 5, DateTime.Now),

                new MovieRating(2, 1, 4, DateTime.Now),
                new MovieRating(2, 2, 5, DateTime.Now),

                new MovieRating(4, 3, 5, DateTime.Now),
                new MovieRating(3, 3, 5, DateTime.Now),
            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            List<int> expected = new List<int>() { 1, 2 };

            // act
            var result = mrs.GetMostProductiveReviewers();

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Once);

        }

        // 9. On input N, what is top N of movies? The score of a movie is its average rate.
        [Theory]
        [InlineData(1, new int[]{3})]
        [InlineData(2, new int[]{3 , 1})]
        [InlineData(3, new int[]{3 , 1, 2})]
        public void GetTopRatedMovies(int amount, int[] expected)
        {
            List<int> expectedList = new List<int>(expected);

            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 3, DateTime.Now),
                new MovieRating(2, 1, 5, DateTime.Now),
                new MovieRating(1, 2, 2, DateTime.Now),
                new MovieRating(2, 2, 5, DateTime.Now),
                new MovieRating(1, 3, 4, DateTime.Now),
            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act

            List<int> result = mrs.GetTopRatedMovies(amount);

            // assert
            Assert.Equal(expectedList, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Once);
        }

        /*10. On input N, what are the movies that reviewer N has reviewed? The list should
        be sorted decreasing by rate first, and date secondly*/
        [Theory]
        [InlineData(1, new int[] { 3, 1, 2 })]
        [InlineData(2, new int[] { 1, 2})]
        public void GetTopMoviesByReviewer(int reviewer, int[] expected)
        {
            List<int> expectedList = new List<int>(expected);

            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 3, DateTime.Now),
                new MovieRating(2, 1, 5, DateTime.Now),
                new MovieRating(1, 2, 2, DateTime.Now),
                new MovieRating(2, 2, 5, DateTime.Now.AddDays(-5)),
                new MovieRating(1, 3, 4, DateTime.Now),
            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act

            List<int> result = mrs.GetTopMoviesByReviewer(reviewer);

            // assert
            Assert.Equal(expectedList, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Once);
        }

        /*11. On input N, who are the reviewers that have reviewed movie N? The list should
        be sorted decreasing by rate first, and date secondly.*/
        [Theory]
        [InlineData(1, new int[] { 2, 1})]
        [InlineData(2, new int[] { 1, 2})]
        [InlineData(3, new int[] {1})]
        public void GetReviewersByMovie(int movie, int[] expected)
        {
            List<int> expectedList = new List<int>(expected);

            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 3, DateTime.Now),
                new MovieRating(2, 1, 5, DateTime.Now),
                new MovieRating(1, 2, 5, DateTime.Now),
                new MovieRating(2, 2, 5, DateTime.Now.AddDays(-5)),
                new MovieRating(1, 3, 4, DateTime.Now),
            };

            MovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act

            List<int> result = mrs.GetReviewersByMovie(movie);

            // assert
            Assert.Equal(expectedList, result);
            repoMock.Verify(repo => repo.ReadAll(), Times.Once);
        }
    }





}

