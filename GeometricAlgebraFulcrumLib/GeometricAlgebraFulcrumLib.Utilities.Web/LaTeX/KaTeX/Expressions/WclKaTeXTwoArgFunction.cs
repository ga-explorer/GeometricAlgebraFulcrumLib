namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX.Expressions;

public sealed class WclKaTeXTwoArgFunction : IWclKaTeXExpression
{
    public string TexCodeTemplate { get; }

    public IWclKaTeXExpression Argument1 { get; set; }

    public IWclKaTeXExpression Argument2 { get; set; }

    public string TexCode
        => TexCodeTemplate.Replace(
            "texArg1", 
            Argument1?.TexCode ?? string.Empty
        ).Replace(
            "texArg2",
            Argument2?.TexCode ?? string.Empty
        ); 

    public bool IsLeafExpression 
        => false;

    public bool IsFunctionExpression 
        => true;

    public int ChildExpressionsCount 
        => 2;

    public IEnumerable<IWclKaTeXExpression> ChildExpressions
    {
        get
        {
            yield return Argument1;
            yield return Argument2;
        }
    }


    internal WclKaTeXTwoArgFunction(string texCodeTemplate)
    {
        TexCodeTemplate = texCodeTemplate;
    }


    public override string ToString()
    {
        return TexCode;
    }
}