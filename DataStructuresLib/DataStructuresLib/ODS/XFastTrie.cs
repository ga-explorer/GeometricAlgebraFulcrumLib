using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.ODS
{
    public partial class XFastTrie<T> : SortedDictionaryBase<T>
    {
        internal int Width;
        private int _count;
        private int _version;
        private readonly IDictionary<uint, XFastNode>[] _table;
        internal LeafNode LeafList;

        public XFastTrie()
            : this(32) { }

        public XFastTrie(int width)
            :this(() => new HashTable<XFastNode>(), width) { }

        private XFastTrie(Func<IDictionary<uint, XFastNode>> factory, int bitWidth)
        {
            this.Width = bitWidth;
            _table = new IDictionary<uint, XFastNode>[bitWidth];
            for (var i = 0; i < bitWidth; i++)
                _table[i] = factory();
        }

        public static XFastTrie<T> FromDictionary<TDict>(int width) where TDict : IDictionary<uint, XFastNode>, new()
        {
            return new XFastTrie<T>(() => new TDict(), width);
        }

        private XFastNode Bottom(uint key)
        {
            var l = 0;
            var h = Width;
            XFastNode tempNode;
            XFastNode correctNode = null;
            do
            {
                var j = (l + h) / 2;
                var ancestor = key >> (Width - 1 - j) >> 1;
                if (_table[j].TryGetValue(ancestor, out tempNode))
                {
                    l = j + 1;
                    correctNode = tempNode;
                }
                else
                {
                    h = j;
                }
            }
            while (l < h);
            return correctNode;
        }

        private void InsertLeafAfter(XFastNode marker, LeafNode newLeaf)
        {
            if (marker == null)
            {
                if (LeafList == null)
                {
                    LeafList = newLeaf;
                    newLeaf.Left = newLeaf;
                    newLeaf.Right = newLeaf;
                }
                else
                {
                    XFastNode rightNode = LeafList;
                    LeafList.Left.Right = newLeaf;
                    newLeaf.Left = LeafList.Left;
                    newLeaf.Right = LeafList;
                    LeafList.Left = newLeaf;
                    LeafList = newLeaf;
                }
            }
            else
            {
                var rightNode = marker.Right;
                marker.Right = newLeaf;
                newLeaf.Left = marker;
                newLeaf.Right = rightNode;
                rightNode.Left = newLeaf;
            }
        }

        public override KeyValuePair<uint, T>? First()
        {
            if (LeafList == null)
                return null;
            return new KeyValuePair<uint,T>(LeafList.Key, LeafList.Value);
        }

        public override KeyValuePair<uint, T>? Last()
        {
            if (LeafList == null)
                return null;
            if (LeafList.Left == LeafList)
                return new KeyValuePair<uint, T>(LeafList.Key, LeafList.Value);
            var leftLeaf = (LeafNode)LeafList.Left;
            return new KeyValuePair<uint, T>(leftLeaf.Key, leftLeaf.Value);
        }

        private LeafNode LowerNodeFromBottom(XFastNode bottom, uint key)
        {
            if (bottom == null)
                return null;
            var leaf = bottom.Right as LeafNode;
            if (leaf != null && leaf.Key < key)
                return leaf;
            leaf = bottom.Left as LeafNode;
            if (leaf != null && leaf.Key < key)
                return leaf;
            leaf = bottom.Left.Left as LeafNode;
            if (leaf != null && leaf.Key < key)
                return leaf;
            return null;
        }

        internal LeafNode LowerNode(uint key)
        {
            var ancestor = Bottom(key);
            return LowerNodeFromBottom(ancestor, key);
        }

        internal LeafNode HigherNode(uint key)
        {
            var ancestor = Bottom(key);
            if (ancestor == null)
                return null;
            var leaf = ancestor.Left as LeafNode;
            if (leaf != null && leaf.Key > key)
                return leaf;
            leaf = ancestor.Right as LeafNode;
            if (leaf != null && leaf.Key > key)
                return leaf;
            leaf = ancestor.Right.Right as LeafNode;
            if (leaf != null && leaf.Key > key)
                return leaf;
            return null;
        }

        public override KeyValuePair<uint, T>? Lower(uint key)
        {
            var lower = LowerNode(key);
            if (lower == null)
                return null;
            return new KeyValuePair<uint,T>(lower.Key, lower.Value);
        }

        public override KeyValuePair<uint, T>? Higher(uint key)
        {
            var lower = HigherNode(key);
            if (lower == null)
                return null;
            return new KeyValuePair<uint, T>(lower.Key, lower.Value);
        }

        private void AddChecked(uint key, T value, bool overwrite)
        {
            // Insert node in linked list
            var bottom = Bottom(key);
            var predecessor = LowerNodeFromBottom(bottom, key);
            // check for overwrite
            LeafNode predRight;
            if (predecessor != null)
                predRight = (LeafNode)predecessor.Right;
            else
                predRight = LeafList;
            if (predRight != null && predRight.Key == key)
            {
                if (!overwrite)
                    throw new ArgumentException();
                else
                {
                    predRight.Value = value;
                    return;
                }
            }
            _count++;
            _version++;
            // merrily continue
            var endNode = new LeafNode() { Key = key, Value = value };
            InsertLeafAfter(predecessor, endNode);
            // Fix the jump path
            if (bottom == null)
            {
                bottom = new XFastNode();
                _table[0].Add(0, bottom);
                bottom.Left = endNode;
                bottom.Right = endNode;
            }

            XFastNode oldNode = null;
            XFastNode current;
            for (var i = 0; i < Width; i++)
            {
                var id = key >> (Width - 1 - i) >> 1;
                if (_table[i].TryGetValue(id, out current))
                {
                    // fix the jump path
                    var leaf = current.Left as LeafNode;
                    if (leaf != null && leaf.Key > key)
                    {
                        current.Left = endNode;
                    }
                    else
                    {
                        leaf = current.Right as LeafNode;
                        if(leaf != null && leaf.Key < key)
                            current.Right = endNode;
                    }
                }
                else
                {
                    // insert new node
                    current = new XFastNode() { Left = endNode, Right = endNode };
                    _table[i].Add(id, current);
                    // fix link between old and new node
                    if ((id & 1) > 0)
                        oldNode.Right = current;
                    else
                        oldNode.Left = current;
                }
                oldNode = current;
            }
        }

        public override void Add(uint key, T value)
        {
            AddChecked(key, value, false);
        }

        private void RemoveLeaf(LeafNode leaf)
        {
            var right = (LeafNode)leaf.Right;
            if (right == leaf)
            {
                LeafList = null;
            }
            else
            {
                leaf.Left.Right = right;
                right.Left = leaf.Left;
                if (leaf == LeafList)
                    LeafList = right;
            }
        }

        public override bool Remove(uint key)
        {
            var bottom = Bottom(key);
            if (bottom == null)
                return false;
            // get the leaf node in endNode
            var endNode = bottom.Left as LeafNode;
            if(endNode == null || endNode.Key != key)
            {
                endNode = bottom.Right as LeafNode;
                if(endNode == null || endNode.Key != key )
                    return false;
            }
            // get pointers to node elft and right from endNode
            var leftLeaf = endNode.Left;
            var rightLeaf = endNode.Right;
            // remove bottom node from the table and leaf node from the list
            //table[width - 1].Remove(key >> 1);
            RemoveLeaf(endNode);
            // iterate levels
            var single = true;
            for(var i = Width - 1; i >= 0; i--)
            {
                XFastNode current;
                var id = key >> (Width - 1 - i) >> 1;
                var isFromRight = ((key >> (Width - 1 - i)) & 1) == 1;
                _table[i].TryGetValue(id, out current);
                // remove the node
                if (single)
                {
                    if (isFromRight && (!(current.Left is LeafNode) || (i == (Width - 1) && ((LeafNode)current.Left).Key != key)))
                    {
                        current.Right = leftLeaf;
                        single = false;
                    }
                    else if (!isFromRight && (!(current.Right is LeafNode) || (i == (Width - 1) && ((LeafNode)current.Right).Key != key)))
                    {
                        current.Left = rightLeaf;
                        single = false;
                    }
                    else
                    {
                        _table[i].Remove(id);
                    }
                }
                // fix jump pointers
                else
                {
                    if (current.Left == endNode)
                        current.Left = rightLeaf;
                    else if (current.Right == endNode)
                        current.Right = leftLeaf;
                }
            }
            _count--;
            _version++;
            return true;
        }

        public override bool TryGetValue(uint key, out T value)
        {
            XFastNode node;
            if (_table[Width - 1].TryGetValue(key >> 1, out node))
            {
                if ((key & 1) == 1)
                {
                    var right = (LeafNode)node.Right;
                    if (right != null && right.Key == key)
                    {
                        value = right.Value;
                        return true;
                    }
                }
                else
                {
                    var left = (LeafNode)node.Left;
                    if (left != null && left.Key == key)
                    {
                        value = left.Value;
                        return true;
                    }
                }
            }
            value = default(T);
            return false;
        }

        public override T this[uint key]
        {
            get
            {
                T temp;
                if (TryGetValue(key, out temp))
                    return temp;
                else
                    throw new KeyNotFoundException();
            }
            set => AddChecked(key, value, true);
        }

        public override IEnumerator<KeyValuePair<uint, T>> GetEnumerator()
        {
            LeafNode start = LeafList, current = LeafList;
            if (current == null)
                yield break;

            do
            {
                yield return new KeyValuePair<uint, T>(current.Key, current.Value);
                current = (LeafNode)current.Right;
            }
            while (current != LeafList);
        }

        public override int Count => _count;

        public override void Clear()
        {
            _count = 0;
            _version = 0;
            LeafList = null;
            for (var i = 0; i < Width; i++)
                _table[i] = new HashTable<XFastNode>();
        }

        public override bool ContainsKey(uint key)
        {
            XFastNode node;
            if (_table[Width - 1].TryGetValue(key >> 1, out node))
            {
                if ((key & 1) == 1)
                    return node.Right != null;
                else
                    return node.Left != null;
            }
            return false;
        }

#if DEBUG
        public void Verify()
        {
            var levels = new HashSet<uint>[Width];
            for (var i = 0; i < Width; i++)
                levels[i] = new HashSet<uint>();
            XFastNode temp;
            var nodes = this.Select(pair => pair.Key).ToArray();
            foreach (var node in nodes)
            {
                for (var i = 0; i < Width; i++)
                {
                    // check levels
                    var id = node >> (Width - 1 - i) >> 1;
                    if(!_table[i].TryGetValue(id, out temp))
                        throw new Exception();
                    if(temp.Left is LeafNode && !nodes.Contains(((LeafNode)temp.Left).Key))
                        throw new Exception();
                    if (temp.Right is LeafNode && !nodes.Contains(((LeafNode)temp.Right).Key))
                        throw new Exception();
                    if (i == Width - 1 && (!(temp.Left is LeafNode) || !(temp.Right is LeafNode)))
                        throw new Exception();
                    levels[i].Add(id);
                }
            }
            for(var i =0; i < Width; i++)
            {
                if (_table[i].Count != levels[i].Count)
                    throw new Exception();
            }
        }
#endif
    }
}
