using rTsd.Models;
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
    ///     https://blog.verslu.is/stackoverflow-answers/alternate-row-color-listview/
    /// </summary>
    internal class AlternateCollectionViewDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Data template for even indeces.
        /// </summary>
        public DataTemplate EvenTemplate { get; set; }

        /// <summary>
        /// Data template for uneven indeces.
        /// </summary>
        public DataTemplate UnevenTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // Ensure given container is a collection view.
            if (!(container is CollectionView collectionView)) return EvenTemplate;

            // Calculate the row index.
            var index = ((List<Post>)collectionView.ItemsSource).IndexOf(item as Post);

            // If mod 2 (save division by 2) equals 0, get the even, elsewise the uneven template.
            return  index % 2 == 0 ? EvenTemplate : UnevenTemplate;
        }
    }
}
