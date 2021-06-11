using System.Collections.Generic;

namespace CodeComposerLib.KaTeX.Expressions
{
    public interface IKaTeXExpression
    {
        string TexCode { get; }

        bool IsLeafExpression { get; }

        bool IsFunctionExpression { get; }

        int ChildExpressionsCount { get; }

        IEnumerable<IKaTeXExpression> ChildExpressions { get; }
    }
}
