using Moq;
using MovieRatingCorrectness.Core.DomainServices;
using Xunit;

namespace MovieRatingCorrectness.UnitTests.ServiceTests
{
    public class MovieRatingServiceTest
    {
        private Mock<IMovieRatingRepository> repoMock;

        public MovieRatingServiceTest()
        {
            repoMock = new Mock<IMovieRatingRepository>();
            repoMock.SetupAllProperties();
            
        }
    }
}