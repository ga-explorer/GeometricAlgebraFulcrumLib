namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public interface IMetaExpressionHeadSpecsVariable :
    IMetaExpressionHeadSpecsAtomic
{
    string VariableName { get; }
}