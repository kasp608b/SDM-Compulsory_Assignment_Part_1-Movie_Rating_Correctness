using System;
using System.Collections.Generic;
using Core;
using Entities;
using Moq;
using Xunit;

namespace UnitTests
{
    public class MovieRatingServiceTest
    {
        private Mock<IMovieRatingRepository> repoMock;
        private List<Rating> ratings;

        public MovieRatingServiceTest()
        {
            ratings = new List<Rating>();
            repoMock = new Mock<IMovieRatingRepository>();
            repoMock.SetupAllProperties();
            repoMock.Setup(x => x.ReadAll()).Returns(() => ratings);
        }

        [Fact]
        public void CreateMovieRatingServiceTest()
        {
            IMovieRatingRepository repo = repoMock.Object;
            MovieRatingService movieRatingService = new MovieRatingService(repo);

            Assert.NotNull(movieRatingService);
        }

        [Fact]
        public void GetAllEmptyMovieRatingServiceTest()
        {
            IMovieRatingRepository repo = repoMock.Object;
            MovieRatingService movieRatingService = new MovieRatingService(repo);

            Assert.Empty(ratings);
        }

        [Fact]
        public void GetAllOneMovieRatingServiceTest()
        {
            IMovieRatingRepository repo = repoMock.Object;
            MovieRatingService movieRatingService = new MovieRatingService(repo);

            ratings = new List<Rating>
            {
                new Rating(1,1,1, DateTime.Now)
            };

            Assert.Equal(ratings[0], repo.ReadAll()[0]);
        }
    }
}
