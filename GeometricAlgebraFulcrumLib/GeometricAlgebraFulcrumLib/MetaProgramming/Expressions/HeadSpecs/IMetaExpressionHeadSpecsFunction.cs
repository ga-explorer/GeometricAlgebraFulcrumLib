using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

public interface IMetaExpressionHeadSpecsFunction :
    IMetaExpressionHeadSpecsComposite
{
    string FunctionName { get; }

    int Precedence { get; }

    bool IsLeftAssociative { get; }

    bool IsRightAssociative { get; }

    bool IsAssociative { get; }

    bool IsNonAssociative { get; }

    MetaExpressionFunctionAssociationKind AssociationKind { get; }

    IMetaExpressionFunction CreateFunction();

    IMetaExpressionFunction CreateFunction(IMetaExpression argument1);

    IMetaExpressionFunction CreateFunction(IMetaExpression argument1, IMetaExpression argument2);

    IMetaExpressionFunction CreateFunction(IMetaExpression argument1, IMetaExpression argument2, IMetaExpression argument3);

    IMetaExpressionFunction CreateFunction(params IMetaExpression[] argumentsList);

    IMetaExpressionFunction CreateFunction(IEnumerable<IMetaExpression> argumentsList);
}