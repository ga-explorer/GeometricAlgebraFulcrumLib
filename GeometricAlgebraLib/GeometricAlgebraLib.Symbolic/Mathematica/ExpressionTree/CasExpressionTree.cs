using System.Collections.Generic;

namespace GeometricAlgebraLib.Symbolic.Mathematica.ExpressionTree
{
    public sealed class CasExpressionTree
    {
        public CasExpressionTreeNode RootNode { get; }

        public SortedDictionary<string, CasExpressionTreeNode> SubexpressionsDictionary { get; private set; }


        public CasExpressionTree(CasExpressionTreeNode rootNode, SortedDictionary<string, CasExpressionTreeNode> subExprDict)
        {
            RootNode = rootNode;
            SubexpressionsDictionary = subExprDict;
        }


        public override string ToString()
        {
            return RootNode.TextExpression;
        }
    }
}
