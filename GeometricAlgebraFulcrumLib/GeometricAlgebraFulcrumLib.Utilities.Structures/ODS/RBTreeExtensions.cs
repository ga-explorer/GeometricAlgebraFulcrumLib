namespace GeometricAlgebraFulcrumLib.Utilities.Structures.ODS;

internal static class RbTreeExtensions
{
    public static RbTree.Node FirstNode(this RbTree tree)
    {
        return FirstNode(tree.Root);
    }

    public static RbTree.Node FirstNode(this RbTree.Node node)
    {
        if (node == null)
            return null;
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    public static RbTree.Node LastNode(this RbTree.Node node)
    {
        if (node == null)
            return null;
        while (node.Right != null)
            node = node.Right;
        return node;
    }

    public static RbTree.Node LastNode(this RbTree tree)
    {
        return LastNode(tree.Root);
    }

    public static void CopySorted(this RbTree tree, RbTree.Node[] array, int offset)
    {
        InOrderAdd(tree.Root, array, offset);
    }

    public static RbTree.Node[] ToSortedArray(this RbTree tree)
    {
        var array = new RbTree.Node[tree.Count];
        CopySorted(tree, array, 0);
        return array;
    }

    private static int InOrderAdd(RbTree.Node node, RbTree.Node[] array, int index)
    {
        if (node == null)
            return index;
        index = InOrderAdd(node.Left, array, index);
        array[index++] = node;
        return InOrderAdd(node.Right, array, index);
    }
}