using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Dependency
{
    /// <summary>
    /// This class holds information about the dependencies of a single item
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public sealed class DependencyInfo<TItem>
    {
        private readonly List<TItem> _userItems = new List<TItem>();

        private readonly List<TItem> _usedItems = new List<TItem>();


        /// <summary>
        /// The base item of this dependency information structure
        /// </summary>
        public TItem BaseItem { get; private set; }

        /// <summary>
        /// An integer tag to be used for indexing of items inside the graph or any other purpose
        /// </summary>
        public int BaseItemIndex { get; set; }

        /// <summary>
        /// A list of all items that directly use the base item
        /// </summary>
        public IEnumerable<TItem> UserItems { get { return _userItems; } }

        /// <summary>
        /// A list of all items directly used by the base item
        /// </summary>
        public IEnumerable<TItem> UsedItems { get { return _usedItems; } }

        /// <summary>
        /// A list of all items directly used by the base item or are direct users of the base item
        /// </summary>
        public IEnumerable<TItem> RelatedItems { get { return _usedItems.Concat(_userItems); } }

        /// <summary>
        /// The number of users of the base items
        /// </summary>
        public int UserCount { get { return _userItems.Count; } }

        /// <summary>
        /// The number of items used by the base item
        /// </summary>
        public int UsedCount { get { return _usedItems.Count; } }

        /// <summary>
        /// The number of items used by the base item or users of the base item
        /// </summary>
        public int RelatedCount { get { return _usedItems.Count + _userItems.Count; } }


        internal DependencyInfo(TItem baseItem)
        {
            BaseItem = baseItem;
        }


        internal void AddUserItem(TItem item)
        {
            _userItems.Add(item);
        }

        internal void AddUsedItem(TItem item)
        {
            _usedItems.Add(item);
        }
    }
}
