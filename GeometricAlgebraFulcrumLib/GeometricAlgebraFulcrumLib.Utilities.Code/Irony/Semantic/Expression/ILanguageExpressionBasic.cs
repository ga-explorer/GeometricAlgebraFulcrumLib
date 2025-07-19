using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Operator;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression;

public interface ILanguageExpressionBasic : ILanguageExpression
{
    ILanguageOperator Operator { get; }

    IEnumerable<ILanguageExpressionAtomic> RhsOperands { get; }
}