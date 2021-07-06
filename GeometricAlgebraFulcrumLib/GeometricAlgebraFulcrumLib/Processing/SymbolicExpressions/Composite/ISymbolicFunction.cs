using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Composite
{
    public interface ISymbolicFunction : 
        ISymbolicExpressionComposite
    {
        ISymbolicHeadSpecsFunction FunctionHeadSpecs { get; }

        ISymbolicHeadSpecsOperator OperatorHeadSpecs { get; }

        bool IsLeftAssociative { get; }

        bool IsRightAssociative { get; }

        bool IsAssociative { get; }

        bool IsNonAssociative { get; }

        SymbolicFunctionAssociationKind AssociationKind { get; }
    }
}