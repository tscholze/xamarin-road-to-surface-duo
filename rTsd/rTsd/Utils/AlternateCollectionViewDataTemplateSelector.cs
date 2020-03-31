using rTsd.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace rTsd.Utils
{
    /// <summary>
    /// Data Template selector that returns a even or an uneven index-based
    /// data template.
    /// 
    /// Necessary Enhancements:
    ///     - Find better way to get to the index
    ///     - Make it generic to avoid the Post cast.
    /// 
    /// Based on:
    ///     - https://blog.verslu.is/stackoverflow-answers/alternate-row-color-listview/
    ///     - https://stackoverflow.com/questions/51422465/alternating-item-backgroundcolor-in-xamarin-forms-listview
    /// </summary>
    public class AlternateCollectionViewDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Data template for even indeces.
        /// </summary>
        public DataTemplate EvenTemplate { get; set; }

        /// <summary>
        /// Data template for uneven indeces.
        /// </summary>
        public DataTemplate UnevenTemplate { get; set; }

        /// <summary>
        /// Determines which template selector will be used for given item.
        /// </summary>
        /// <param name="item">Item (cell)</param>
        /// <param name="container">Container (CollectionView)</param>
        /// <returns>TemplateSelector for given item.</returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // Ensure given container is a collection view.
            if (!(container is CollectionView collectionView)) return EvenTemplate;

            // Type check what object is the underlying type of the list.
            int index;
            // Check for post collection view.
            if (item is Post post)
            {
                index = ((List<Post>)collectionView.ItemsSource).IndexOf(post);
            }
            // Check for video collection views.
            else if(item is Video video)
            {
                index = ((List<Video>)collectionView.ItemsSource).IndexOf(video);
            }
            // No other types are supported, yet.
            else
            {
                throw new NotImplementedException();
            }

            // If mod 2 (save division by 2) equals 0, get the even, elsewise the uneven template.
            return  index % 2 == 0 ? EvenTemplate : UnevenTemplate;
        }
    }
}
