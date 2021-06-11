using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.SyntaxTree.Expressions;

namespace CodeComposerLib.SyntaxTree
{
    public static class SyntaxTreeUtils
    {

        public static IEnumerable<SteExpression> CreateCopy(this IEnumerable<SteExpression> exprList)
        {
            if (ReferenceEquals(exprList, null)) return null;

            return exprList.Select(
                a => ReferenceEquals(a, null) ? null : a.CreateCopy()
                );
        }

        public static bool Equals(this SteExpression symbolicExpr1, SteExpression symbolicExpr2)
        {
            if (ReferenceEquals(symbolicExpr1, null) || ReferenceEquals(symbolicExpr2, null))
                return false;

            if (ReferenceEquals(symbolicExpr1, symbolicExpr2))
                return true;

            if (symbolicExpr1.HeadText != symbolicExpr2.HeadText)
                return false;

            if (symbolicExpr1.ArgumentsCount == 0 && symbolicExpr2.ArgumentsCount == 0)
                return true;

            if (symbolicExpr1.ArgumentsCount != symbolicExpr2.ArgumentsCount)
                return false;

            return symbolicExpr1.Arguments.Zip(symbolicExpr2.Arguments, (t, s) => t.Equals(s)).All(b => b);
        }

    }
}
