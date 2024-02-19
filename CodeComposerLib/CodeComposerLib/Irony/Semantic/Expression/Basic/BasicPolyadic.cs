using System.Collections.Generic;
using CodeComposerLib.Irony.Semantic.Operator;
using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.Basic;

/// <summary>
/// This class represents a basic polyadic expression (for example a function call or a construction of a structure)
/// </summary>
public sealed class BasicPolyadic : LanguageExpressionBasic
{
    /// <summary>
    /// The operands of the basic polyadic expression
    /// </summary>
    private PolyadicOperands _operands;

    /// <summary>
    /// The operands of the basic polyadic expression
    /// </summary>
    public PolyadicOperands Operands
    {
        get
        {
            return _operands;
        }
        set
        {
            if (ReferenceEquals(_operands, null) && ReferenceEquals(value, null) == false)
                _operands = value;
        }
    }


    private BasicPolyadic(ILanguageType exprType, ILanguageOperator langOp)
        : base(exprType, langOp)
    {
    }

    private BasicPolyadic(ILanguageType exprType, ILanguageOperator langOp, PolyadicOperands operands)
        : base(exprType, langOp)
    {
        _operands = operands;
    }


    public override LanguageExpressionBasic Duplicate()
    {
        return new BasicPolyadic(ExpressionType, Operator, Operands.Duplicate());
    }

    public override IEnumerable<ILanguageExpressionAtomic> RhsOperands => Operands.RhsOperands;

    public override bool IsSimpleExpression => Operands.IsAllSimpleOperands;


    public static BasicPolyadic Create(ILanguageType exprType, ILanguageOperator langOp)
    {
        return new BasicPolyadic(exprType, langOp);
    }

    public static BasicPolyadic Create(ILanguageType exprType, ILanguageOperator langOp, PolyadicOperands operands)
    {
        return new BasicPolyadic(exprType, langOp, operands);
    }

    public static BasicPolyadic CreateProcedureCall(ILanguageType exprType, SymbolProcedure langOp)
    {
        return new BasicPolyadic(exprType, langOp);
    }
}