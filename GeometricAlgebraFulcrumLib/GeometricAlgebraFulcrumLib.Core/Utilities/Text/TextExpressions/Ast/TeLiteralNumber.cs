namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text.TextExpressions.Ast;

public sealed class TeLiteralNumber : ISimpleTextExpression
{
    public string Text { get; set; }

    public string TextLiteral => Text.ValueToQuotedLiteral();

    public bool IsSimple => true;

    public bool IsComposite => false;

    public bool IsLiteral => true;

    public bool IsLiteralNumber => true;

    public bool IsLiteralString => false;

    public bool IsIdentifier => false;

    public bool IsSingleIdentifier => false;

    public bool IsQualifiedIdentifier => false;

    public bool IsList => false;

    public bool IsNamedList => false;

    public bool IsNamelessList => false;

    public bool IsDictionary => false;

    public bool IsNamedDictionary => false;

    public bool IsNamelessDictionary => false;


    public TeLiteralNumber(int number)
    {
        Text = number.ToString();
    }

    public TeLiteralNumber(long number)
    {
        Text = number.ToString();
    }

    public TeLiteralNumber(float number)
    {
        Text = number.ToString("G");
    }

    public TeLiteralNumber(double number)
    {
        Text = number.ToString("G");
    }

    public TeLiteralNumber(string number)
    {
        Text = number;
    }


    public ITextExpression CreateCopy()
    {
        return new TeLiteralNumber(Text);
    }

    public override string ToString()
    {
        return Text;
    }
}