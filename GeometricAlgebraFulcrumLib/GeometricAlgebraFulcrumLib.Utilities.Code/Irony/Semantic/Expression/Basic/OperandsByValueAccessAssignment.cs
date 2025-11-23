using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.ValueAccess;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.Basic;

public sealed class OperandsByValueAccessAssignment : IIronyAstObject
{
    /// <summary>
    /// The LHS of the operand assignment
    /// </summary>
    public LanguageValueAccess LhsValueAccess { get; }

    /// <summary>
    /// The RHS of the operand assignment
    /// </summary>
    public ILanguageExpressionAtomic RhsExpression { get; private set; }

    public IronyAst RootAst => LhsValueAccess.RootAst;


    public OperandsByValueAccessAssignment(LanguageValueAccess lhsValue, ILanguageExpressionAtomic rhsExpr)
    {
        LhsValueAccess = lhsValue;
        RhsExpression = rhsExpr;
    }


    public void ChangeRhsExpression(ILanguageExpressionAtomic rhsExpr)
    {
        if (rhsExpr.ExpressionType.IsSameType(RhsExpression.ExpressionType))
            RhsExpression = rhsExpr;
        else
            throw new InvalidOperationException();
    }

    public override string ToString()
    {
        return RootAst.Describe(this);
    }
}