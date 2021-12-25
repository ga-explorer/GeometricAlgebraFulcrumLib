using System.Collections.Generic;

namespace DataStructuresLib.ODS
{
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
                this.Key = key;
            }

            internal RbuIntNode(uint key, T value)
            {
                this.Key = key;
                this.Value = value;
            }

            public override void SwapValue(RbTree.Node other)
            {
                var node = (RbuIntNode)other;
                var tempKey = Key;
                this.Key = node.Key;
                node.Key = tempKey;
                var tempValue = this.Value;
                this.Value = node.Value;
                node.Value = tempValue;
            }

            public override bool Equals(object obj)
            {
                var node = obj as RbuIntNode;
                return (node != null) && (node.Key == Key) && (object.Equals(node.Value, Value));
            }

            public override int GetHashCode()
            {
                return Key.GetHashCode() ^ (Value == null ? int.MaxValue : Value.GetHashCode());
            }
        }
    }
}
