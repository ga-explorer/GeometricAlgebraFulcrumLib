//
// System.Collections.Generic.SortedDictionary
//
// Author:
//    Raja R Harinath <rharinath@novell.com>
//
// Authors of previous (superseded) version:
//    Kazuki Oikawa (kazuki@panicode.com)
//    Atsushi Enomoto (atsushi@ximian.com)
//

//
// Copyright (C) 2007, Novell, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace DataStructuresLib.ODS
{
    [Serializable]
    [DebuggerDisplay("Count={Count}")]
    public class SortedDictionary<TKey, TValue> : ISortedDictionary<TKey, TValue>, IDictionary, ISerializable
    {
        private class Node : RbTree.Node
        {
            public TKey Key;
            public TValue Value;

            public Node(TKey key)
            {
                Key = key;
            }

            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public override void SwapValue(RbTree.Node other)
            {
                var o = (Node)other;
                var k = Key; Key = o.Key; o.Key = k;
                var v = Value; Value = o.Value; o.Value = v;
            }

            public KeyValuePair<TKey, TValue> AsKv()
            {
                return new KeyValuePair<TKey, TValue>(Key, Value);
            }

            public DictionaryEntry AsDe()
            {
                return new DictionaryEntry(Key, Value);
            }
        }

        [Serializable]
        private class NodeHelper : RbTree.INodeHelper<TKey>
        {
            public IComparer<TKey> Cmp;

            public int Compare(TKey key, RbTree.Node node)
            {
                return Cmp.Compare(key, ((Node)node).Key);
            }

            public RbTree.Node CreateNode(TKey key)
            {
                return new Node(key);
            }

            private NodeHelper(IComparer<TKey> cmp)
            {
                Cmp = cmp;
            }

            private static NodeHelper _default = new NodeHelper(Comparer<TKey>.Default);
            public static NodeHelper GetHelper(IComparer<TKey> cmp)
            {
                if (cmp == null || cmp == Comparer<TKey>.Default)
                    return _default;
                return new NodeHelper(cmp);
            }
        }

        private RbTree _tree;
        private NodeHelper _hlp;

        #region Constructor
        public SortedDictionary()
            : this((IComparer<TKey>)null)
        {
        }

        public SortedDictionary(IComparer<TKey> comparer)
        {
            _hlp = NodeHelper.GetHelper(comparer);
            _tree = new RbTree(_hlp);
        }

        public SortedDictionary(IDictionary<TKey, TValue> dic)
            : this(dic, null)
        {
        }

        public SortedDictionary(IDictionary<TKey, TValue> dic, IComparer<TKey> comparer)
            : this(comparer)
        {
            if (dic == null)
                throw new ArgumentNullException();
            foreach (var entry in dic)
                Add(entry.Key, entry.Value);
        }

        protected SortedDictionary(SerializationInfo info, StreamingContext context)
        {
            _hlp = (NodeHelper)info.GetValue("Helper", typeof(NodeHelper));
            _tree = new RbTree(_hlp);

            var data = (KeyValuePair<TKey, TValue>[])info.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
            foreach (var entry in data)
                Add(entry.Key, entry.Value);
        }

        #endregion

        #region PublicProperty

        public IComparer<TKey> Comparer => _hlp.Cmp;

        public int Count => _tree.Count;

        public TValue this[TKey key]
        {
            get
            {
                var n = (Node)_tree.Lookup(key);
                if (n == null)
                    throw new KeyNotFoundException();
                return n.Value;
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                var n = (Node)_tree.Intern(key, null);
                n.Value = value;
            }
        }

        public KeyCollection Keys => new KeyCollection(this);

        public ValueCollection Values => new ValueCollection(this);

        #endregion

        #region PublicMethod

        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            RbTree.Node n = new Node(key, value);
            if (_tree.Intern(key, n) != n)
                throw new ArgumentException("key already present in dictionary", "key");
        }

        public void Clear()
        {
            _tree.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return _tree.Lookup(key) != null;
        }

        public bool ContainsValue(TValue value)
        {
            IEqualityComparer<TValue> vcmp = EqualityComparer<TValue>.Default;
            foreach (Node n in _tree)
                if (vcmp.Equals(value, n.Value))
                    return true;
            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (Count == 0)
                return;
            if (array == null)
                throw new ArgumentNullException();
            if (arrayIndex < 0 || array.Length <= arrayIndex)
                throw new ArgumentOutOfRangeException();
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException();

            foreach (Node n in _tree)
                array[arrayIndex++] = n.AsKv();
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public bool Remove(TKey key)
        {
            return _tree.Remove(key) != null;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var n = (Node)_tree.Lookup(key);
            value = n == null ? default(TValue) : n.Value;
            return n != null;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            var data = new KeyValuePair<TKey, TValue>[Count];
            CopyTo(data, 0);
            info.AddValue("KeyValuePairs", data);
            info.AddValue("Helper", _hlp);
        }

        #endregion

        #region PrivateMethod

        private TKey ToKey(object key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (!(key is TKey))
                throw new ArgumentException(string.Format("Key \"{0}\" cannot be converted to the key type {1}.", key, typeof(TKey)));
            return (TKey)key;
        }

        private TValue ToValue(object value)
        {
            if (!(value is TValue) && (value != null || typeof(TValue).IsValueType))
                throw new ArgumentException(string.Format("Value \"{0}\" cannot be converted to the value type {1}.", value, typeof(TValue)));
            return (TValue)value;
        }
        #endregion

        #region IDictionary<TKey,TValue> Member

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => new KeyCollection(this);

        ICollection<TValue> IDictionary<TKey, TValue>.Values => new ValueCollection(this);

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Member

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            return TryGetValue(item.Key, out value) &&
                EqualityComparer<TValue>.Default.Equals(item.Value, value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            return TryGetValue(item.Key, out value) &&
                EqualityComparer<TValue>.Default.Equals(item.Value, value) &&
                Remove(item.Key);
        }

        #endregion

        #region IDictionary Member

        void IDictionary.Add(object key, object value)
        {
            Add(ToKey(key), ToValue(value));
        }

        bool IDictionary.Contains(object key)
        {
            return ContainsKey(ToKey(key));
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new Enumerator(this);
        }

        bool IDictionary.IsFixedSize => false;

        bool IDictionary.IsReadOnly => false;

        ICollection IDictionary.Keys => new KeyCollection(this);

        void IDictionary.Remove(object key)
        {
            Remove(ToKey(key));
        }

        ICollection IDictionary.Values => new ValueCollection(this);

        object IDictionary.this[object key]
        {
            get => this[ToKey(key)];
            set => this[ToKey(key)] = ToValue(value);
        }

        #endregion

        #region ICollection Member

        void ICollection.CopyTo(Array array, int index)
        {
            if (Count == 0)
                return;
            if (array == null)
                throw new ArgumentNullException();
            if (index < 0 || array.Length <= index)
                throw new ArgumentOutOfRangeException();
            if (array.Length - index < Count)
                throw new ArgumentException();

            foreach (Node n in _tree)
                array.SetValue(n.AsDe(), index++);
        }

        bool ICollection.IsSynchronized => false;

        // TODO:Is this correct? If this is wrong,please fix.
        object ICollection.SyncRoot => this;

        #endregion

        #region IEnumerable Member

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region IEnumerable<TKey> Member

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        [Serializable]
        [DebuggerDisplay("Count={Count}")]
        public sealed class ValueCollection : ICollection<TValue>,
            IEnumerable<TValue>, ICollection, IEnumerable
        {
            private SortedDictionary<TKey, TValue> _dic;

            public ValueCollection(SortedDictionary<TKey, TValue> dic)
            {
                _dic = dic;
            }

            void ICollection<TValue>.Add(TValue item)
            {
                throw new NotSupportedException();
            }

            void ICollection<TValue>.Clear()
            {
                throw new NotSupportedException();
            }

            bool ICollection<TValue>.Contains(TValue item)
            {
                return _dic.ContainsValue(item);
            }

            public void CopyTo(TValue[] array, int arrayIndex)
            {
                if (Count == 0)
                    return;
                if (array == null)
                    throw new ArgumentNullException();
                if (arrayIndex < 0 || array.Length <= arrayIndex)
                    throw new ArgumentOutOfRangeException();
                if (array.Length - arrayIndex < Count)
                    throw new ArgumentException();
                foreach (Node n in _dic._tree)
                    array[arrayIndex++] = n.Value;
            }

            public int Count => _dic.Count;

            bool ICollection<TValue>.IsReadOnly => true;

            bool ICollection<TValue>.Remove(TValue item)
            {
                throw new NotSupportedException();
            }

            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            {
                return GetEnumerator();
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(_dic);
            }

            void ICollection.CopyTo(Array array, int index)
            {
                if (Count == 0)
                    return;
                if (array == null)
                    throw new ArgumentNullException();
                if (index < 0 || array.Length <= index)
                    throw new ArgumentOutOfRangeException();
                if (array.Length - index < Count)
                    throw new ArgumentException();
                foreach (Node n in _dic._tree)
                    array.SetValue(n.Value, index++);
            }

            bool ICollection.IsSynchronized => false;

            object ICollection.SyncRoot => _dic;

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(_dic);
            }

            public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
            {
                private RbTree.NodeEnumerator _host;

                private TValue _current;

                internal Enumerator(SortedDictionary<TKey, TValue> dic)
                    : this()
                {
                    _host = dic._tree.GetEnumerator();
                }

                public TValue Current => _current;

                public bool MoveNext()
                {
                    if (!_host.MoveNext())
                        return false;
                    _current = ((Node)_host.Current).Value;
                    return true;
                }

                public void Dispose()
                {
                    _host.Dispose();
                }

                object IEnumerator.Current
                {
                    get
                    {
                        _host.check_current();
                        return _current;
                    }
                }

                void IEnumerator.Reset()
                {
                    _host.Reset();
                }
            }
        }

        [Serializable]
        [DebuggerDisplay("Count={Count}")]
        public sealed class KeyCollection : ICollection<TKey>,
            IEnumerable<TKey>, ICollection, IEnumerable
        {
            private SortedDictionary<TKey, TValue> _dic;

            public KeyCollection(SortedDictionary<TKey, TValue> dic)
            {
                _dic = dic;
            }

            void ICollection<TKey>.Add(TKey item)
            {
                throw new NotSupportedException();
            }

            void ICollection<TKey>.Clear()
            {
                throw new NotSupportedException();
            }

            bool ICollection<TKey>.Contains(TKey item)
            {
                return _dic.ContainsKey(item);
            }

            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void CopyTo(TKey[] array, int arrayIndex)
            {
                if (Count == 0)
                    return;
                if (array == null)
                    throw new ArgumentNullException();
                if (arrayIndex < 0 || array.Length <= arrayIndex)
                    throw new ArgumentOutOfRangeException();
                if (array.Length - arrayIndex < Count)
                    throw new ArgumentException();
                foreach (Node n in _dic._tree)
                    array[arrayIndex++] = n.Key;
            }

            public int Count => _dic.Count;

            bool ICollection<TKey>.IsReadOnly => true;

            bool ICollection<TKey>.Remove(TKey item)
            {
                throw new NotSupportedException();
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(_dic);
            }

            void ICollection.CopyTo(Array array, int index)
            {
                if (Count == 0)
                    return;
                if (array == null)
                    throw new ArgumentNullException();
                if (index < 0 || array.Length <= index)
                    throw new ArgumentOutOfRangeException();
                if (array.Length - index < Count)
                    throw new ArgumentException();
                foreach (Node n in _dic._tree)
                    array.SetValue(n.Key, index++);
            }

            bool ICollection.IsSynchronized => false;

            object ICollection.SyncRoot => _dic;

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(_dic);
            }

            public struct Enumerator : IEnumerator<TKey>, IEnumerator, IDisposable
            {
                private RbTree.NodeEnumerator _host;

                private TKey _current;

                internal Enumerator(SortedDictionary<TKey, TValue> dic)
                    : this()
                {
                    _host = dic._tree.GetEnumerator();
                }

                public TKey Current => _current;

                public bool MoveNext()
                {
                    if (!_host.MoveNext())
                        return false;
                    _current = ((Node)_host.Current).Key;
                    return true;
                }

                public void Dispose()
                {
                    _host.Dispose();
                }

                object IEnumerator.Current
                {
                    get
                    {
                        _host.check_current();
                        return _current;
                    }
                }

                void IEnumerator.Reset()
                {
                    _host.Reset();
                }
            }
        }

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IDictionaryEnumerator, IEnumerator
        {
            private RbTree.NodeEnumerator _host;

            private KeyValuePair<TKey, TValue> _current;

            internal Enumerator(SortedDictionary<TKey, TValue> dic)
                : this()
            {
                _host = dic._tree.GetEnumerator();
            }

            public KeyValuePair<TKey, TValue> Current => _current;

            public bool MoveNext()
            {
                if (!_host.MoveNext())
                    return false;
                _current = ((Node)_host.Current).AsKv();
                return true;
            }

            public void Dispose()
            {
                _host.Dispose();
            }

            private Node CurrentNode
            {
                get
                {
                    _host.check_current();
                    return (Node)_host.Current;
                }
            }

            DictionaryEntry IDictionaryEnumerator.Entry => CurrentNode.AsDe();

            object IDictionaryEnumerator.Key => CurrentNode.Key;

            object IDictionaryEnumerator.Value => CurrentNode.Value;

            object IEnumerator.Current => CurrentNode.AsDe();

            void IEnumerator.Reset()
            {
                _host.Reset();
            }
        }

        KeyValuePair<TKey, TValue>? ISortedDictionary<TKey, TValue>.First()
        {
            throw new NotImplementedException();
        }

        KeyValuePair<TKey, TValue>? ISortedDictionary<TKey, TValue>.Last()
        {
            throw new NotImplementedException();
        }

        KeyValuePair<TKey, TValue>? ISortedDictionary<TKey, TValue>.Lower(TKey key)
        {
            throw new NotImplementedException();
        }

        KeyValuePair<TKey, TValue>? ISortedDictionary<TKey, TValue>.Higher(TKey key)
        {
            RbTree.Node highParent = null;
            var current = _tree.Root;
            while (current != null)
            {
                var cmp = _hlp.Compare(key, current);
                if (cmp < 0)
                {
                    highParent = current;
                    current = current.Left;
                }
                else if (cmp > 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            // we finished walk on a node with key equal to ours
            if (current != null && current.Right != null)
            {
                return ((Node)current.Right.FirstNode()).AsKv();
            }
            if (highParent == null)
                return null;
            else
                return ((Node)highParent).AsKv();
        }

    }
}
