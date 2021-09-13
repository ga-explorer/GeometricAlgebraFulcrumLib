namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public interface ISymbolicHeadSpecsNumber :
        ISymbolicHeadSpecsAtomic
    {
        double NumberFloat64Value { get; }

        string NumberText { get; }
    }
}