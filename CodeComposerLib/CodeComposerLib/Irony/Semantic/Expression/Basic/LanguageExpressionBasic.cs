using System.Collections.Generic;
using CodeComposerLib.Irony.Semantic.Operator;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.Basic;

/// <summary>
/// This abstract class is the base for all basic expressions (three-address style for composing expressions)
/// </summary>
public abstract class LanguageExpressionBasic : ILanguageExpressionBasic
{
    public ILanguageType ExpressionType { get; }

    public IronyAst RootAst => ExpressionType.RootAst;

    /// <summary>
    /// The operator of the expression
    /// </summary>
    public ILanguageOperator Operator { get; }


    protected LanguageExpressionBasic(ILanguageType exprType, ILanguageOperator langOperator)
    {
        ExpressionType = exprType;

        Operator = langOperator;
    }


    public abstract IEnumerable<ILanguageExpressionAtomic> RhsOperands { get; }

    public abstract LanguageExpressionBasic Duplicate();

    public abstract bool IsSimpleExpression { get; }


    public override string ToString()
    {
        return RootAst.Describe(this);
    }
}