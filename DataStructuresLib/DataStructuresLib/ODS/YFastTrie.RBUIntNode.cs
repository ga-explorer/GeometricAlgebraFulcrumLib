using System.Collections.Generic;

namespace DataStructuresLib.ODS;

partial class YFastTrie<T>
{
    internal class RbuIntNode : RbTree.Node
    {
        internal uint Key;
        internal T Value;

        internal RbuIntNode(KeyValuePair<uint, T> pair)
            : this(pair.Key, pair.Value)
        {}

        internal RbuIntNode(uint key)
        {
            Key = key;
        }

        internal RbuIntNode(uint key, T value)
        {
            Key = key;
            Value = value;
        }

        public override void SwapValue(RbTree.Node other)
        {
            var node = (RbuIntNode)other;
            var tempKey = Key;
            Key = node.Key;
            node.Key = tempKey;
            var tempValue = Value;
            Value = node.Value;
            node.Value = tempValue;
        }

        public override bool Equals(object obj)
        {
            var node = obj as RbuIntNode;
            return (node != null) && (node.Key == Key) && (Equals(node.Value, Value));
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode() ^ (Value == null ? int.MaxValue : Value.GetHashCode());
        }
    }
}