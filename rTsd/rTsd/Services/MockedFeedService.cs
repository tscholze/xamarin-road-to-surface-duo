using rTsd.Models;
using System.Collections.Generic;

namespace rTsd.Services
{
    /// <summary>
    /// A <see cref="IElementService{T}"/> that mocks a Feed reader with 
    /// static and testable values.
    /// </summary>
    public class MockedFeedService : IElementService<Post>
    {
        #region Private constants

        /// <summary>
        /// Defines the max count of mocked entries.
        /// </summary>
        private const int MAX_MOCKED_ENTRIES_COUNT = 5;

        #endregion

        #region IElementService implementation

        public List<Post> GetAll(bool forceReload = false)
        {
            var posts = new List<Post>();

            // Mock non forced reload with empty list.
            if (forceReload == false)
            {
                return posts;
            }

            for (int i = 0; i < MAX_MOCKED_ENTRIES_COUNT; i++)
            {
                posts.Add(new Post
                {
                    Id = $"{i}",
                    Title = $"Title #{i}",
                    Content = "Lorem ipsum nupssie das ist ein toller Abstract Text. Bitte liest ihn alle!"
                });
            }

            return posts;
        }

        #endregion
    }
}
