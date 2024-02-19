using System.Collections.Generic;
using Irony.Parsing;

namespace CodeComposerLib.Irony.Compiler;

public static class CompilerUtils
{
    /// <summary>
    /// Compare two parse tree nodes and return true only if the contents of the whole trees match
    /// </summary>
    /// <param name="node1"></param>
    /// <param name="node2"></param>
    /// <returns></returns>
    public static bool IsSameParseTreeNode(this ParseTreeNode node1, ParseTreeNode node2)
    {
        var stack1 = new Stack<ParseTreeNode>();
        var stack2 = new Stack<ParseTreeNode>();

        stack1.Push(node1);
        stack2.Push(node2);

        while (stack1.Count > 0)
        {
            node1 = stack1.Pop();
            node2 = stack2.Pop();

            if (ReferenceEquals(node1, null) || ReferenceEquals(node2, null))
                return false;

            if (ReferenceEquals(node1, node2))
                return true;

            if (node1.ChildNodes.Count != node2.ChildNodes.Count)
                return false;

            if (node1.FindTokenAndGetText() != node2.FindTokenAndGetText())
                return false;

            for (var i = 0; i < node1.ChildNodes.Count; i++)
            {
                stack1.Push(node1.ChildNodes[i]);
                stack2.Push(node2.ChildNodes[i]);
            }
        }

        return true;
    }

}