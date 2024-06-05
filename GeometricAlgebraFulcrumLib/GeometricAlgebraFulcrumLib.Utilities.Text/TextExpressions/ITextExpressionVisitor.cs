using GeometricAlgebraFulcrumLib.Utilities.Text.TextExpressions.Ast;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.TextExpressions;

public interface ITextExpressionVisitor
{
    void Visit(TeLiteralNumber textExpr);

    void Visit(TeLiteralString textExpr);

    void Visit(TeIdentifier textExpr);

    void Visit(TeList textExpr);

    void Visit(TeDictionary textExpr);

    void Visit(ISimpleTextExpression textExpr);

    void Visit(ICompositeTextExpression textExpr);

    void Visit(ITextExpression textExpr);
}

public interface ITextExpressionVisitor<out T>
{
    T Visit(TeLiteralNumber textExpr);

    T Visit(TeLiteralString textExpr);

    T Visit(TeIdentifier textExpr);

    T Visit(TeList textExpr);

    T Visit(TeDictionary textExpr);

    T Visit(ISimpleTextExpression textExpr);

    T Visit(ICompositeTextExpression textExpr);

    T Visit(ITextExpression textExpr);
}