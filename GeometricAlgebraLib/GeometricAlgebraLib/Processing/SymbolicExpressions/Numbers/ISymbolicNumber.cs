using GeometricAlgebraLib.Processing.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Numbers
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