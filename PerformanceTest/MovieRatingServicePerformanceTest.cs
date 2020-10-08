using Core;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PerformanceTest
{
    [TestClass]
    public class MovieRatingServicePerformanceTest
    {
        private static MovieRatingRepository repo;

        [ClassInitialize]
        public static void SetUpTest(TestContext tc)
        {
            repo = new MovieRatingRepository(@"C:\Users\kacpe\Desktop\Datamatiker\Visualstudio C# projects\SDM-Compulsory_Assignment_Part_1-Movie_Rating_Correctness\RatingsArray\ratings.json");
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberofReviewsFromReviewer()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetNumberOfReviewsFromReviewer(100000);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        [Timeout(4000)]
        public void GetAverageRateFromReviewer()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetAverageRateFromReviewer(100000);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfRatesByReviewer()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetNumberOfRatesByReviewer(100000, 5);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfReviews()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetNumberOfReviews(100000);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetAverageRateOfMovie()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetAverageRateOfMovie(100000);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfRates()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetNumberOfRates(100000, 5);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetMoviesWithHighestNumberOfTopRates();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetMostProductiveReviewers()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetMostProductiveReviewers();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetTopRatedMovies()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetTopRatedMovies(100000);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetTopMoviesByReviewer()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetTopMoviesByReviewer(100000);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetReviewersByMovie()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetReviewersByMovie(10000);
            Assert.IsNotNull(result);
        }
    }
}
