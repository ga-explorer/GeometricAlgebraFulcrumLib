namespace DataStructuresLib.ODS
{
    partial class YFastTrie<T>
    {
        internal class Node
        {
            internal static readonly RbuIntNodeHelper Helper = new RbuIntNodeHelper();
            internal RbTree Tree;

            public Node(uint key, T value)
            {
                Tree = new RbTree(Helper);
                Tree.Intern(key, new RbuIntNode(key, value));
            }
        }
    }
}
