namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

public interface IMetaExpressionHeadSpecsVariable :
    IMetaExpressionHeadSpecsAtomic
{
    string VariableName { get; }
}