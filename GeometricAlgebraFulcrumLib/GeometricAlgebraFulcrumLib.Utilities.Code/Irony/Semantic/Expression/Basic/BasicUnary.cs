using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Operator;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.Basic;

/// <summary>
/// This class represents a basic unary expression
/// </summary>
public sealed class BasicUnary : LanguageExpressionBasic
{
    /// <summary>
    /// The operand of the basic unary expression
    /// </summary>
    public ILanguageExpressionAtomic Operand { get; private set; }


    private BasicUnary(ILanguageType exprType, ILanguageOperator langOp, ILanguageExpressionAtomic operand)
        : base(exprType, langOp)
    {
        Operand = operand;
    }


    public void ChangeOperand(ILanguageExpressionAtomic operand)
    {
        if (operand.ExpressionType.IsSameType(Operand.ExpressionType))
            Operand = operand;
        else
            throw new InvalidOperationException();
    }

    public override LanguageExpressionBasic Duplicate()
    {
        return new BasicUnary(ExpressionType, Operator, Operand);
    }

    public override IEnumerable<ILanguageExpressionAtomic> RhsOperands
    {
        get { yield return Operand; }
    }

    public override bool IsSimpleExpression => Operand.IsSimpleExpression;


    public static BasicUnary Create(ILanguageType exprType, ILanguageOperator langOp, ILanguageExpressionAtomic operand)
    {
        return new BasicUnary(exprType, langOp, operand);
    }

    public static BasicUnary CreatePrimitive(ILanguageType exprType, OperatorPrimitive langOp, ILanguageExpressionAtomic operand)
    {
        return new BasicUnary(exprType, langOp, operand);
    }

    public static BasicUnary CreatePrimitive(ILanguageType exprType, string opName, ILanguageExpressionAtomic operand)
    {
        ILanguageOperator langOp = exprType.RootAst.OperatorPrimitiveDictionary[opName];

        return new BasicUnary(exprType, langOp, operand);
    }

    public static BasicUnary CreateTypeCast(ILanguageType exprType, ILanguageOperator langOp, ILanguageExpressionAtomic operand)
    {
        return new BasicUnary(exprType, langOp, operand);
    }
}