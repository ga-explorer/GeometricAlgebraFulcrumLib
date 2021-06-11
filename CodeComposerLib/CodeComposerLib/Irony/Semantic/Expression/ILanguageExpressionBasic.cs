using System.Collections.Generic;
using CodeComposerLib.Irony.Semantic.Operator;

namespace CodeComposerLib.Irony.Semantic.Expression
{
    public interface ILanguageExpressionBasic : ILanguageExpression
    {
        ILanguageOperator Operator { get; }

        IEnumerable<ILanguageExpressionAtomic> RhsOperands { get; }
    }
}
