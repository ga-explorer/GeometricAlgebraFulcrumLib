using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

public abstract class MetaExpressionAtomicFactoryBase
{
    public MetaContext Context { get; }

    public IScalarProcessor<IMetaExpression> ScalarProcessor
        => Context.MetaExpressionProcessor;


    protected MetaExpressionAtomicFactoryBase(MetaContext context)
    {
        Context = context;
    }
}