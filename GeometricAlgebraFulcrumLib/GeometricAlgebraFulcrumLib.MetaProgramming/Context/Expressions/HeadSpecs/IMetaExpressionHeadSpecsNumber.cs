using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public interface IMetaExpressionHeadSpecsNumber :
    IMetaExpressionHeadSpecsAtomic
{
    IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor { get; }

    double NumberFloat64Value { get; }

    string NumberText { get; }
}