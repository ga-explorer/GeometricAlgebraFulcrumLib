using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.Expression;

namespace GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExpressionTree
{
    public sealed class CasExpressionTreeNode
    {
        public MathematicaExpression RhsExpression { get; }

        public MathematicaExpression RhsReducedExpression;

        public string LhsVariableName = "";

        public List<CasExpressionTreeNode> NodeChildren;


        public string TextExpression => RhsExpression.ExpressionText;

        public int ChildCount => NodeChildren.Count;

        public bool IsLeaf => (NodeChildren == null) || (NodeChildren.Count == 0);


        public CasExpressionTreeNode(MathematicaExpression nodeExpr)
        {
            RhsExpression = nodeExpr;
            RhsReducedExpression = nodeExpr;
        }

        public CasExpressionTreeNode(string lhsVarName, MathematicaExpression nodeExpr)
        {
            RhsExpression = nodeExpr;
            RhsReducedExpression = nodeExpr;
            LhsVariableName = lhsVarName;
        }


        public void AddChild(CasExpressionTreeNode childNode)
        {
            if (NodeChildren == null)
                NodeChildren = new List<CasExpressionTreeNode>();

            NodeChildren.Add(childNode);
        }


        public override string ToString()
        {
            return RhsExpression.ExpressionText;
        }
    }
}
