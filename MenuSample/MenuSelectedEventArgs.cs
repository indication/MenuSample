using System;

namespace MenuSample
{
    /// <summary>
    /// Menu event
    /// </summary>
    public class MenuEventArgs : EventArgs
    {
        /// <summary>
        /// Target item on the event
        /// </summary>
        public MenuListViewItem Item { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">Event argument</param>
        public MenuEventArgs(MenuListViewItem item)
        {
            Item = item;
        }
    }
}
