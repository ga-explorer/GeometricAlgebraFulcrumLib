using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;

public interface IMetaExpressionFunction :
    IMetaExpressionComposite
{
    IMetaExpressionHeadSpecsFunction FunctionHeadSpecs { get; }

    IMetaExpressionHeadSpecsOperator OperatorHeadSpecs { get; }

    bool IsLeftAssociative { get; }

    bool IsRightAssociative { get; }

    bool IsAssociative { get; }

    bool IsNonAssociative { get; }

    MetaExpressionFunctionAssociationKind AssociationKind { get; }
}