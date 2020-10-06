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
            var result = service.GetNumberOfReviewsFromReviewer(1);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        [Timeout(4000)]
        public void GetAverageRateFromReviewer()
        {
            MovieRatingService service = new MovieRatingService(repo);
            var result = service.GetAverageRateFromReviewer(1);
            Assert.IsNotNull(result);
        }
    }
}
