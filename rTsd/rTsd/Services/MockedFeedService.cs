using rTsd.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<List<Post>> GetAllAsync(bool forceReload = false)
        {
            var task = Task.Run(() => {
                // TODO: Check if linked list or array list fits better.
                // TODO: May want to use linq.
                var posts = new List<Post>();

                // Mock non forced reload with empty list.
                if(forceReload == false)
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
            });

            return await task.ConfigureAwait(false);
        }

        public Post GetById(string id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
