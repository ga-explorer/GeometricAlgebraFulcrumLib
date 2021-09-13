using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Composite;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public interface ISymbolicHeadSpecsFunction :
        ISymbolicHeadSpecsComposite
    {
        string FunctionName { get; }

        int Precedence { get; }

        bool IsLeftAssociative { get; }

        bool IsRightAssociative { get; }

        bool IsAssociative { get; }

        bool IsNonAssociative { get; }

        SymbolicFunctionAssociationKind AssociationKind { get; }

        ISymbolicFunction CreateFunction();

        ISymbolicFunction CreateFunction(ISymbolicExpression argument1);

        ISymbolicFunction CreateFunction(ISymbolicExpression argument1, ISymbolicExpression argument2);

        ISymbolicFunction CreateFunction(ISymbolicExpression argument1, ISymbolicExpression argument2, ISymbolicExpression argument3);

        ISymbolicFunction CreateFunction(params ISymbolicExpression[] argumentsList);

        ISymbolicFunction CreateFunction(IEnumerable<ISymbolicExpression> argumentsList);
    }
}