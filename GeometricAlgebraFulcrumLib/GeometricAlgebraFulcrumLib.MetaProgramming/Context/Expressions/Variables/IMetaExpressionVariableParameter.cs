namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

public interface IMetaExpressionVariableParameter :
    IMetaExpressionVariable,
    IMetaExpressionAtomicIndependent
{
    void SetStateFrom(IMetaExpressionVariableParameter variable);
}