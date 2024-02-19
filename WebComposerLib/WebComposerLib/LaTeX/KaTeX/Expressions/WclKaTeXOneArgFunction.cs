namespace WebComposerLib.LaTeX.KaTeX.Expressions;

public sealed class WclKaTeXOneArgFunction : IWclKaTeXExpression
{
    public string TexCodeTemplate { get; }

    public IWclKaTeXExpression Argument { get; set; }

    public string TexCode
        => TexCodeTemplate.Replace(
            "texArg1", 
            Argument.TexCode
        ); 

    public bool IsLeafExpression 
        => false;

    public bool IsFunctionExpression 
        => true;

    public int ChildExpressionsCount 
        => 1;

    public IEnumerable<IWclKaTeXExpression> ChildExpressions
    {
        get { yield return Argument; }
    }


    internal WclKaTeXOneArgFunction(string texCodeTemplate)
    {
        TexCodeTemplate = texCodeTemplate;
    }


    public override string ToString()
    {
        return TexCode;
    }
}