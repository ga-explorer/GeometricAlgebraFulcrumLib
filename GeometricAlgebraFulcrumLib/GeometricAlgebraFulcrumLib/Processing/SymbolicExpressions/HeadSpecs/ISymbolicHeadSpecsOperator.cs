using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Composite;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs
{
    public interface ISymbolicHeadSpecsOperator :
        ISymbolicHeadSpecsFunction
    {
        string SymbolText { get; }

        SymbolicOperatorPosition Position { get; }
    }
}