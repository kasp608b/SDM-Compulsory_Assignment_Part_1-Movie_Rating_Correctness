using System.Collections.Generic;
using Entities;

namespace Core
{
    public interface IMovieRatingRepository
    {
        public MovieRating[] Ratings { get; }

    }
}