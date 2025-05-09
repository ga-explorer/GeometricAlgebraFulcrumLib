using GeometricAlgebraFulcrumLib.Core.Utilities.Text.TextExpressions.Ast;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text.TextExpressions;

public abstract class TextExpressionVisitor : ITextExpressionVisitor
{
    public abstract void Visit(TeLiteralNumber textExpr);

    public abstract void Visit(TeLiteralString textExpr);

    public abstract void Visit(TeIdentifier textExpr);

    public abstract void Visit(TeList textExpr);

    public abstract void Visit(TeDictionary textExpr);

    public virtual void Visit(ISimpleTextExpression textExpr)
    {
        if (ReferenceEquals(textExpr, null)) return;

        var textExprNumber = textExpr as TeLiteralNumber;

        if (ReferenceEquals(textExprNumber, null) == false)
        {
            Visit(textExprNumber);
            return;
        }

        var textExprString = textExpr as TeLiteralString;

        if (ReferenceEquals(textExprString, null) == false)
        {
            Visit(textExprString);
            return;
        }

        var textExprIdentifier = textExpr as TeIdentifier;

        if (ReferenceEquals(textExprIdentifier, null) == false)
            Visit(textExprIdentifier);
    }

    public virtual void Visit(ICompositeTextExpression textExpr)
    {
        if (ReferenceEquals(textExpr, null)) return;

        var textExprList = textExpr as TeList;

        if (ReferenceEquals(textExprList, null) == false)
        {
            Visit(textExprList);
            return;
        }

        var textExprDict = textExpr as TeDictionary;

        if (ReferenceEquals(textExprDict, null) == false)
            Visit(textExprDict);
    }

    public virtual void Visit(ITextExpression textExpr)
    {
        if (ReferenceEquals(textExpr, null)) return;

        var textExprNumber = textExpr as TeLiteralNumber;

        if (ReferenceEquals(textExprNumber, null) == false)
        {
            Visit(textExprNumber);
            return;
        }

        var textExprString = textExpr as TeLiteralString;

        if (ReferenceEquals(textExprString, null) == false)
        {
            Visit(textExprString);
            return;
        }

        var textExprIdentifier = textExpr as TeIdentifier;

        if (ReferenceEquals(textExprIdentifier, null) == false)
        {
            Visit(textExprIdentifier);
            return;
        }

        var textExprList = textExpr as TeList;

        if (ReferenceEquals(textExprList, null) == false)
        {
            Visit(textExprList);
            return;
        }

        var textExprDict = textExpr as TeDictionary;

        if (ReferenceEquals(textExprDict, null) == false)
            Visit(textExprDict);
    }
}

public abstract class TextExpressionVisitor<T> : ITextExpressionVisitor<T>
{
    public abstract T Visit(TeLiteralNumber textExpr);

    public abstract T Visit(TeLiteralString textExpr);

    public abstract T Visit(TeIdentifier textExpr);

    public abstract T Visit(TeList textExpr);

    public abstract T Visit(TeDictionary textExpr);

    public virtual T Visit(ISimpleTextExpression textExpr)
    {
        if (ReferenceEquals(textExpr, null)) return default;

        var textExprNumber = textExpr as TeLiteralNumber;

        if (ReferenceEquals(textExprNumber, null) == false)
            return Visit(textExprNumber);

        var textExprString = textExpr as TeLiteralString;

        if (ReferenceEquals(textExprString, null) == false)
            return Visit(textExprString);

        var textExprIdentifier = textExpr as TeIdentifier;

        if (ReferenceEquals(textExprIdentifier, null) == false)
            return Visit(textExprIdentifier);

        return default;
    }

    public virtual T Visit(ICompositeTextExpression textExpr)
    {
        if (ReferenceEquals(textExpr, null)) return default;

        var textExprList = textExpr as TeList;

        if (ReferenceEquals(textExprList, null) == false)
            return Visit(textExprList);

        var textExprDict = textExpr as TeDictionary;

        if (ReferenceEquals(textExprDict, null) == false)
            return Visit(textExprDict);

        return default;
    }

    public virtual T Visit(ITextExpression textExpr)
    {
        if (ReferenceEquals(textExpr, null)) return default;

        var textExprNumber = textExpr as TeLiteralNumber;

        if (ReferenceEquals(textExprNumber, null) == false)
            return Visit(textExprNumber);

        var textExprString = textExpr as TeLiteralString;

        if (ReferenceEquals(textExprString, null) == false)
            return Visit(textExprString);

        var textExprIdentifier = textExpr as TeIdentifier;

        if (ReferenceEquals(textExprIdentifier, null) == false)
            return Visit(textExprIdentifier);

        var textExprList = textExpr as TeList;

        if (ReferenceEquals(textExprList, null) == false)
            return Visit(textExprList);

        var textExprDict = textExpr as TeDictionary;

        if (ReferenceEquals(textExprDict, null) == false)
            return Visit(textExprDict);

        return default;
    }
}