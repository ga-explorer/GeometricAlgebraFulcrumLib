using GeometricAlgebraLib.Processing.SymbolicExpressions.Composite;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.HeadSpecs
{
    public interface ISymbolicHeadSpecsOperator :
        ISymbolicHeadSpecsFunction
    {
        string SymbolText { get; }

        SymbolicOperatorPosition Position { get; }
    }
}