using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;

public interface IMetaExpressionNumber :
    IMetaExpressionAtomicIndependent
{
    IMetaExpressionHeadSpecsNumber NumberHeadSpecs { get; }

    string NumberText { get; }

    bool IsZero { get; }

    bool IsNearZero { get; }

    bool IsFiniteNumber { get; }

    bool IsPositive { get; }

    bool IsNegative { get; }

    bool IsNotNearPositive { get; }

    bool IsNotNearNegative { get; }

    void SetStateFrom(IMetaExpressionNumber number);
}