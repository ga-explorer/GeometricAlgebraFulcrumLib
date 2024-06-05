using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.TextExpressions.Ast;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.TextExpressions;

public sealed class TextExpressionComposer : TextExpressionVisitor
{
    public LinearTextComposer TextComposer { get; }


    private TextExpressionComposer()
    {
        TextComposer = new LinearTextComposer();
    }


    public override void Visit(TeLiteralNumber textExpr)
    {
        if (ReferenceEquals(textExpr, null)) return;

        TextComposer.Append(textExpr.ToString());
    }

    public override void Visit(TeLiteralString textExpr)
    {
        if (ReferenceEquals(textExpr, null)) return;

        TextComposer.Append(textExpr.ToString());
    }

    public override void Visit(TeIdentifier textExpr)
    {
        if (ReferenceEquals(textExpr, null)) return;

        TextComposer.Append(textExpr.ToString());
    }

    public override void Visit(TeList textExpr)
    {
        if (ReferenceEquals(textExpr, null)) return;

        if (textExpr.IsNamed) 
            TextComposer.Append(textExpr.Name.ToString());

        TextComposer.Append("[");

        var flag = false;
        foreach (var subExpr in textExpr)
        {
            if (flag) TextComposer.Append(", ");
            else flag = true;

            Visit(subExpr);
        }

        TextComposer.Append("]");
    }

    public override void Visit(TeDictionary textExpr)
    {
        if (ReferenceEquals(textExpr, null)) return;

        if (textExpr.IsNamed)
            TextComposer.Append(textExpr.Name.ToString()).AppendLine(" {").IncreaseIndentation();
        else
            TextComposer.AppendLine("{").IncreaseIndentation();

        var flag = false;
        foreach (var pair in textExpr)
        {
            if (flag) TextComposer.AppendLine(", ");
            else flag = true;

            TextComposer.Append(pair.Key).Append(" : ");

            Visit(pair.Value);
        }

        TextComposer.DecreaseIndentation().AppendAtNewLine("}");
    }

    public static string Generate(ITextExpression textExpr)
    {
        var composer = new TextExpressionComposer();

        composer.Visit(textExpr);

        return composer.TextComposer.ToString();
    }
}