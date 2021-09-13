using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Numbers
{
    public interface ISymbolicNumber : 
        ISymbolicExpressionAtomicIndependent
    {
        ISymbolicHeadSpecsNumber NumberHeadSpecs { get; }

        string NumberText { get; }

        bool IsZero { get; }

        bool IsNearZero { get; }

        bool IsPositive { get; }

        bool IsNegative { get; }

        bool IsNotNearPositive { get; }

        bool IsNotNearNegative { get; }
    }
}