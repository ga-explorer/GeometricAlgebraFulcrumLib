using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

public interface IMetaExpressionVariable :
    IMetaExpressionAtomic
{
    MetaExpressionHeadSpecsVariable VariableHeadSpecs { get; }

    void SetRhsExpressionValue(double number);
}