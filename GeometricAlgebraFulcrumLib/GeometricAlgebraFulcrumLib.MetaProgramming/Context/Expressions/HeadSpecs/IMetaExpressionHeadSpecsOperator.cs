using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public interface IMetaExpressionHeadSpecsOperator :
    IMetaExpressionHeadSpecsFunction
{
    string SymbolText { get; }

    MetaExpressionOperatorPosition Position { get; }
}