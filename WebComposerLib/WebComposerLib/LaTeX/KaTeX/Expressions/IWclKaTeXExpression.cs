namespace WebComposerLib.LaTeX.KaTeX.Expressions;

public interface IWclKaTeXExpression
{
    string TexCode { get; }

    bool IsLeafExpression { get; }

    bool IsFunctionExpression { get; }

    int ChildExpressionsCount { get; }

    IEnumerable<IWclKaTeXExpression> ChildExpressions { get; }
}