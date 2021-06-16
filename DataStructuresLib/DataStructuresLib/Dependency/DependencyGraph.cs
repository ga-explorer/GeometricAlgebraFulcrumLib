using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Dependency
{
    /// <summary>
    /// This data structure can be used to store many-to-many relations between a set of items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class DependencyGraph<TKey, TItem>
    {
        private readonly Dictionary<TKey, DependencyInfo<TItem>> _dependencies =
            new Dictionary<TKey, DependencyInfo<TItem>>();


        /// <summary>
        /// Returns all dependency information for all items in this graph
        /// </summary>
        public IEnumerable<DependencyInfo<TItem>> Items 
            => _dependencies.Values;

        /// <summary>
        /// Returns all dependency information for items having no user items in this graph
        /// </summary>
        public IEnumerable<DependencyInfo<TItem>> ItemsWithNoUser
        {
            get { return _dependencies.Values.Where(m => m.UserCount == 0); }
        }

        /// <summary>
        /// Returns all dependency information for items having at least one user item in this graph
        /// </summary>
        public IEnumerable<DependencyInfo<TItem>> ItemsWithUser
        {
            get { return _dependencies.Values.Where(m => m.UserCount > 0); }
        }

        /// <summary>
        /// Returns all dependency information for items having no used items in this graph
        /// </summary>
        public IEnumerable<DependencyInfo<TItem>> ItemsWithNoUsed
        {
            get { return _dependencies.Values.Where(m => m.UsedCount == 0); }
        }

        /// <summary>
        /// Returns all dependency information for items having at least one used item in this graph
        /// </summary>
        public IEnumerable<DependencyInfo<TItem>> ItemsWithUsed
        {
            get { return _dependencies.Values.Where(m => m.UsedCount > 0); }
        }

        /// <summary>
        /// Returns all dependency information for items having no used items and no user 
        /// items in this graph
        /// </summary>
        public IEnumerable<DependencyInfo<TItem>> ItemsWithNoRelations
        {
            get { return _dependencies.Values.Where(m => m.UserCount == 0 && m.UsedCount == 0); }
        }

        /// <summary>
        /// Returns all dependency information for items having at least one used item or one user 
        /// item in this graph
        /// </summary>
        public IEnumerable<DependencyInfo<TItem>> ItemsWithAnyRelation
        {
            get { return _dependencies.Values.Where(m => m.UserCount > 0 || m.UsedCount > 0); }
        }


        /// <summary>
        /// Gets the key of a given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract TKey ItemToKey(TItem item);

        /// <summary>
        /// Populate the graph with dependency information
        /// </summary>
        public abstract void Populate();


        /// <summary>
        /// Gets the dependency information of an item with the given key if exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DependencyInfo<TItem> this[TKey key] => _dependencies[key];

        /// <summary>
        /// Remove all dependency information in this graph
        /// </summary>
        public void Clear()
        {
            _dependencies.Clear();
        }

        /// <summary>
        /// True if the graph contains an item with the given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return _dependencies.ContainsKey(key);
        }

        /// <summary>
        /// True if the graph contains the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ContainsItem(TItem item)
        {
            return _dependencies.ContainsKey(ItemToKey(item));
        }

        /// <summary>
        /// Try get the dependency information of an item with the given key if exists in the graph
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dep"></param>
        /// <returns></returns>
        public bool TryGetDependency(TKey key, out DependencyInfo<TItem> dep)
        {
            return _dependencies.TryGetValue(key, out dep);
        }

        /// <summary>
        /// Try get the dependency information of the given item if exists in the graph
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dep"></param>
        /// <returns></returns>
        public bool TryGetDependencyOfItem(TKey key, out DependencyInfo<TItem> dep)
        {
            return _dependencies.TryGetValue(key, out dep);
        }

        /// <summary>
        /// Find all dependencies whose base item satisfy the given predicate
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<DependencyInfo<TItem>> GetDependencies(Predicate<TItem> filter)
        {
            return _dependencies.Values.Where(item => filter(item.BaseItem));
        }

        /// <summary>
        /// Find all dependencies satisfy the given predicate
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<DependencyInfo<TItem>> GetDependencies(Predicate<DependencyInfo<TItem>> filter)
        {
            return _dependencies.Values.Where(item => filter(item));
        }
    
        /// <summary>
        /// If the given item already has dependency information in the graph it's returned, else 
        /// a dependency information object for the item is created in the graph and returned
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public DependencyInfo<TItem> ItemToDependency(TItem item)
        {
            var key = ItemToKey(item);

            if (_dependencies.TryGetValue(key, out var dep) == false)
            {
                dep = new DependencyInfo<TItem>(item);

                _dependencies.Add(key, dep);
            }

            return dep;
        }

        /// <summary>
        /// Get a list of all items that are direct or indirect users of the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IEnumerable<DependencyInfo<TItem>> GetAllUserItems(TItem item)
        {
            var itemsList = new Dictionary<TKey, DependencyInfo<TItem>>();

            var queue = new Queue<TItem>();

            var itemDep = _dependencies[ItemToKey(item)];

            foreach (var userItem in itemDep.UserItems)
                queue.Enqueue(userItem);

            while (queue.Count > 0)
            {
                var curItem = queue.Dequeue();
                var curKey = ItemToKey(curItem);

                if (itemsList.ContainsKey(curKey))
                    continue;

                var curDep = _dependencies[curKey];

                itemsList.Add(curKey, curDep);

                foreach (var userItem in curDep.UserItems)
                    queue.Enqueue(userItem);
            }

            return itemsList.Values;
        }

        /// <summary>
        /// Get a list of all items that are directly or indirectly used by the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IEnumerable<DependencyInfo<TItem>> GetAllUsedItems(TItem item)
        {
            var itemsList = new Dictionary<TKey, DependencyInfo<TItem>>();

            var queue = new Queue<TItem>();

            var itemDep = _dependencies[ItemToKey(item)];

            foreach (var usedItem in itemDep.UsedItems)
                queue.Enqueue(usedItem);

            while (queue.Count > 0)
            {
                var curItem = queue.Dequeue();
                var curKey = ItemToKey(curItem);

                if (itemsList.ContainsKey(curKey))
                    continue;

                var curDep = _dependencies[curKey];

                itemsList.Add(curKey, curDep);

                foreach (var usedItem in curDep.UsedItems)
                    queue.Enqueue(usedItem);
            }

            return itemsList.Values;
        }

        /// <summary>
        /// Get a list of all items that are directly or indirectly related to (i.e. used by or users of) 
        /// the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IEnumerable<DependencyInfo<TItem>> GetAllRelatedItems(TItem item)
        {
            var itemsList = new Dictionary<TKey, DependencyInfo<TItem>>();

            var queue = new Queue<TItem>();

            var itemDep = _dependencies[ItemToKey(item)];

            foreach (var relatedItem in itemDep.RelatedItems)
                queue.Enqueue(relatedItem);

            while (queue.Count > 0)
            {
                var curItem = queue.Dequeue();
                var curKey = ItemToKey(curItem);

                if (itemsList.ContainsKey(curKey))
                    continue;

                var curDep = _dependencies[curKey];

                itemsList.Add(curKey, curDep);

                foreach (var relatedItem in curDep.RelatedItems)
                    queue.Enqueue(relatedItem);
            }

            return itemsList.Values;
        }


        protected void AddDependency(TItem userItem, TItem usedItem)
        {
            ItemToDependency(userItem).AddUsedItem(usedItem);

            ItemToDependency(usedItem).AddUserItem(userItem);
        }

        protected void AddDependencies(TItem userItem, IEnumerable<TItem> usedItems)
        {
            var userDep = ItemToDependency(userItem);

            foreach (var usedItem in usedItems)
            {
                userDep.AddUsedItem(usedItem);

                ItemToDependency(usedItem).AddUserItem(userItem);
            }
        }

        protected void AddDependencies(IEnumerable<TItem> userItems, TItem usedItem)
        {
            var usedDep = ItemToDependency(usedItem);

            foreach (var userItem in userItems)
            {
                ItemToDependency(userItem).AddUsedItem(usedItem);

                usedDep.AddUserItem(userItem);
            }
        }
    }
}
