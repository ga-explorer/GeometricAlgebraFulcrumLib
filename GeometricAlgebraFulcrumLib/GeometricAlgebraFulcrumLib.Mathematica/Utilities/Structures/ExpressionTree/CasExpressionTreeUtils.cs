using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using Wolfram.NETLink;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.Expression;

namespace GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExpressionTree;

public static class CasExpressionTreeUtils
{
    public static CasExpressionTree ToExpressionTree(this MathematicaExpression expr, IntegerSequenceGenerator seqGen, SortedDictionary<string, CasExpressionTreeNode> subExprDict, string lhsVarName)
    {
        var srcStk = new Stack<Expr>();
        var dstStk = new Stack<CasExpressionTreeNode>();

        var rootExpr = expr.Expression;
        var rootNode = new CasExpressionTreeNode(lhsVarName, expr);
        var tree = new CasExpressionTree(rootNode, subExprDict);

        var rootExprText = rootExpr.ToString();
        if (subExprDict.ContainsKey(rootExprText) == false)
            subExprDict.Add(rootExprText, rootNode);

        srcStk.Push(rootExpr);
        dstStk.Push(rootNode);

        while (srcStk.Count > 0)
        {
            var curExpr = srcStk.Pop();
            var curNode = dstStk.Pop();

            if (curExpr.Args.Length > 0)
            {

                var newArgs = new object[curExpr.Args.Length];

                var i = 0;
                foreach (var childExpr in curExpr.Args)
                {
                    var childExprText = childExpr.ToString();

                    if (subExprDict.TryGetValue(childExprText, out var childNode) == false)
                    {
                        var childArgName = seqGen.GetNewStringId();

                        var childCasExpr =
                            MathematicaExpression.Create(expr.CasInterface, childExpr);

                        childNode = new CasExpressionTreeNode(childArgName, childCasExpr);

                        subExprDict.Add(childExprText, childNode);
                    }

                    curNode.AddChild(childNode);

                    srcStk.Push(childExpr);
                    dstStk.Push(childNode);

                    if (childExpr.AtomQ())
                        newArgs[i] = childExpr;
                    else
                        newArgs[i] = new Expr(ExpressionType.Symbol, childNode.LhsVariableName);

                    i++;
                }

                curNode.RhsReducedExpression =
                    MathematicaExpression.Create(
                        expr.CasInterface,
                        new Expr(curExpr.Head, newArgs)
                    );
            }
            else
            {
                curNode.RhsReducedExpression = curNode.RhsExpression;
            }
        }

        return tree;
    }


}