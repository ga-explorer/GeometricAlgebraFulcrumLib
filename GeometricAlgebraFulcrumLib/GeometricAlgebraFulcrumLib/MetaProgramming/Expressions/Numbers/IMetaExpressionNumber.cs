using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers
{
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
    }
}