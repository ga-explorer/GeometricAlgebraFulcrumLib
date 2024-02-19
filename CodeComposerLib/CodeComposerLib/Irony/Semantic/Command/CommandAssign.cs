using System;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Scope;

namespace CodeComposerLib.Irony.Semantic.Command;

/// <summary>
/// This class represents an assignment command
/// </summary>
public class CommandAssign : LanguageCommand
{
    /// <summary>
    /// The LHS of the assignment command
    /// </summary>
    public LanguageValueAccess LhsValueAccess { get; private set; }

    /// <summary>
    /// The RHS of the assignment command
    /// </summary>
    public ILanguageExpression RhsExpression { get; private set; }


    public CommandAssign(LanguageScope parentScope, LanguageValueAccess lhsValue, ILanguageExpression rhsExpr)
        : base(parentScope)
    {
        LhsValueAccess = lhsValue;
        RhsExpression = rhsExpr;
    }


    public void SetLhsValueAccess(LanguageValueAccess valueAccess)
    {
        if (LhsValueAccess.ExpressionType.IsSameType(valueAccess.ExpressionType))
            LhsValueAccess = valueAccess;
        else
            throw new InvalidOperationException();
    }

    public void SetRhsExpression(ILanguageExpression rhsExpr)
    {
        if (RhsExpression.ExpressionType.IsSameType(rhsExpr.ExpressionType))
            RhsExpression = rhsExpr;
        else
            throw new InvalidOperationException();
    }

    public void SetCommandSides(LanguageValueAccess lhsValueAccess, ILanguageExpression rhsExpr)
    {
        if (lhsValueAccess.ExpressionType.IsSameType(rhsExpr.ExpressionType))
        {
            LhsValueAccess = lhsValueAccess;
            RhsExpression = rhsExpr;
        }
        else
            throw new InvalidOperationException();
    }
}