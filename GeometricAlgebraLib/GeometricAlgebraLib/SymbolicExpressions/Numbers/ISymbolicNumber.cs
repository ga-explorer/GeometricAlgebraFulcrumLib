using GeometricAlgebraLib.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraLib.SymbolicExpressions.Numbers
{
    public interface ISymbolicNumber : 
        ISymbolicExpressionAtomicIndependent
    {
        ISymbolicHeadSpecsNumber NumberHeadSpecs { get; }

        string NumberText { get; }

        bool IsZero { get; }

        bool IsNearZero { get; }
    }
}