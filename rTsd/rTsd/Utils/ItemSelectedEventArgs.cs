using System;

namespace rTsd.Utils
{
    /// <summary>
    /// This event contains all required members to identify
    /// the selected item.
    /// </summary>
    public class ItemSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Item ID (GUID).
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Corresponding viewmodel for ItemPage of the
        /// selected item.
        /// </summary>
        public object ItemViewModel { get; set; }
    }
}