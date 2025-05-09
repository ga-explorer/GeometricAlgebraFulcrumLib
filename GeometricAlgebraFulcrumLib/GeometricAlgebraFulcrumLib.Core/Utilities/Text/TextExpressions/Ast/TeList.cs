namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text.TextExpressions.Ast;

public sealed class TeList : List<ITextExpression>, ICompositeTextExpression
{
    public TeIdentifier Name { get; set; }

    public bool IsNamed => IsNamedList;

    public bool IsNameless => IsNamelessList;

    public IEnumerable<ITextExpression> ChildExpressions => this;

    public string Text => IsNamed
        ? this.Concatenate(", ", "[", "]")
        : Name + this.Concatenate(", ", "[", "]");

    public string TextLiteral => Text.ValueToQuotedLiteral();

    public bool IsSimple => false;

    public bool IsComposite => true;

    public bool IsLiteral => false;

    public bool IsLiteralNumber => false;

    public bool IsLiteralString => false;

    public bool IsIdentifier => false;

    public bool IsSingleIdentifier => false;

    public bool IsQualifiedIdentifier => false;

    public bool IsList => true;

    public bool IsNamedList => ReferenceEquals(Name, null) == false && Name.HasName;

    public bool IsNamelessList => IsNamedList == false;

    public bool IsDictionary => false;

    public bool IsNamedDictionary => false;

    public bool IsNamelessDictionary => false;


    public ITextExpression CreateCopy()
    {
        var newList = new TeList { Name = Name };

        newList.AddRange(this.Select(t => t.CreateCopy()));

        return newList;
    }

    public override string ToString()
    {
        return Text;
    }
}