namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX.Expressions;

public sealed class WclKaTeXLeafExpression : IWclKaTeXExpression
{
    public string TexCode { get; }

    public bool IsLeafExpression 
        => true;

    public bool IsFunctionExpression 
        => false;

    public int ChildExpressionsCount 
        => 0;

    public IEnumerable<IWclKaTeXExpression> ChildExpressions
        => Enumerable.Empty<IWclKaTeXExpression>();


    internal WclKaTeXLeafExpression(string texCode)
    {
        TexCode = texCode;
    }


    public override string ToString()
    {
        return TexCode;
    }
}