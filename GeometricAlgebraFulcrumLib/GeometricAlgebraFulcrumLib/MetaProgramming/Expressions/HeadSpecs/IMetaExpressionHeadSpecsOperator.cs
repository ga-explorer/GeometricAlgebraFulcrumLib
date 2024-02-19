using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

public interface IMetaExpressionHeadSpecsOperator :
    IMetaExpressionHeadSpecsFunction
{
    string SymbolText { get; }

    MetaExpressionOperatorPosition Position { get; }
}