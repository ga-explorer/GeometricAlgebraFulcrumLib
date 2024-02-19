namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

public interface IMetaExpressionHeadSpecsNumber :
    IMetaExpressionHeadSpecsAtomic
{
    double NumberFloat64Value { get; }

    string NumberText { get; }
}