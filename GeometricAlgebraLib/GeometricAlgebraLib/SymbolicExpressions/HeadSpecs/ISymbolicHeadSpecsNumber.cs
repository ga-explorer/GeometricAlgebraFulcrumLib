namespace GeometricAlgebraLib.SymbolicExpressions.HeadSpecs
{
    public interface ISymbolicHeadSpecsNumber :
        ISymbolicHeadSpecsAtomic
    {
        double NumberValue { get; }

        string NumberText { get; }
    }
}