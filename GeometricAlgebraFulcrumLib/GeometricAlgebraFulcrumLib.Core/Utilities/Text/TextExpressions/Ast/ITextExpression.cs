namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text.TextExpressions.Ast;

public interface ITextExpression
{
    string Text { get; }

    string TextLiteral { get; }


    bool IsSimple { get; }

    bool IsComposite { get; }

    bool IsLiteral { get; }

    bool IsLiteralNumber { get; }

    bool IsLiteralString { get; }

    bool IsIdentifier { get; }

    bool IsSingleIdentifier { get; }

    bool IsQualifiedIdentifier { get; }

    bool IsList { get; }

    bool IsNamedList { get; }

    bool IsNamelessList { get; }

    bool IsDictionary { get; }

    bool IsNamedDictionary { get; }

    bool IsNamelessDictionary { get; }


    ITextExpression CreateCopy();
}