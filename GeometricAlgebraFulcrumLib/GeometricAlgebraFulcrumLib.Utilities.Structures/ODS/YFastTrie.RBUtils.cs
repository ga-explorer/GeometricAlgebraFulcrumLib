namespace GeometricAlgebraFulcrumLib.Utilities.Structures.ODS;

partial class YFastTrie<T>
{
    internal class RbUtils
    {
        internal static RbuIntNode HigherNode(RbTree tree, uint key)
        {
            RbuIntNode highParent = null;
            var current = (RbuIntNode)tree.Root;
            while (current != null)
            {
                if (key < current.Key)
                {
                    highParent = current;
                    current = (RbuIntNode)current.Left;
                }
                else if (key > current.Key)
                {
                    current = (RbuIntNode)current.Right;
                }
                else
                {
                    break;
                }
            }
            // we finished walk on a node with key equal to ours
            if (current != null && current.Right != null)
            {
                return (RbuIntNode)current.Right.FirstNode();
            }
            return highParent;
        }

        internal static RbuIntNode LowerNode(RbTree tree, uint key)
        {
            RbuIntNode lowParent = null;
            var current = (RbuIntNode)tree.Root;
            while (current != null)
            {
                if (key < current.Key)
                {
                    current = (RbuIntNode)current.Left;
                }
                else if (key > current.Key)
                {
                    lowParent = current;
                    current = (RbuIntNode)current.Right;
                }
                else
                {
                    break;
                }
            }
            // we finished walk on a node with key equal to ours
            if (current != null && current.Left != null)
            {
                return (RbuIntNode)current.Left.LastNode();
            }
            return lowParent;
        }

        internal static RbTree FromSortedList(RbTree.Node[] list, int start, int stop)
        {
            var tree = new RbTree(new RbuIntNodeHelper());
            var length = stop - start + 1;
            if (start == stop)
                return tree;
            var maxDepth = BitHacks.Power2Msb(BitHacks.RoundToPower((uint)(length + 1))) - 1;
            tree.Root = list[start + (length >> 1)];
            tree.Root.IsBlack = true;
            tree.Root.Size = (uint)length;
            RbInsertChildren(tree.Root, true, 1, maxDepth, list, start, start + (length >> 1) - 1);
            RbInsertChildren(tree.Root, false, 1, maxDepth, list, start + (length >> 1) + 1, stop);
            return tree;
        }

        private static void RbInsertChildren(RbTree.Node node, bool left, int depth, int totalDepth, RbTree.Node[] list, int start, int stop)
        {
            if (start > stop)
            {
                if (left)
                    node.Left = null;
                else
                    node.Right = null;
                return;
            }
            var middle = start + ((stop - start) >> 1);
            var current = list[middle];
            current.Size = (uint)(stop - start) + 1;
            current.IsBlack = (((totalDepth - depth) & 1) == 1);
            if (left)
                node.Left = current;
            else
                node.Right = current;
            RbInsertChildren(current, true, depth + 1, totalDepth, list, start, middle - 1);
            RbInsertChildren(current, false, depth + 1, totalDepth, list, middle + 1, stop);
        }
    }
}