using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;

public interface IMetaExpressionVariable : 
    IMetaExpressionAtomic
{
    MetaExpressionHeadSpecsVariable VariableHeadSpecs { get; }
        
    void SetRhsExpressionValue(double number);
}