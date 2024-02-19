namespace DataStructuresLib.ODS;

partial class YFastTrie<T>
{
    internal class RbuIntNodeHelper : RbTree.INodeHelper<uint>
    {
        public int Compare(uint key, RbTree.Node node)
        {
            return key.CompareTo(((RbuIntNode)node).Key);
        }

        public RbTree.Node CreateNode(uint key)
        {
            return new RbuIntNode(key);
        }
    }
}