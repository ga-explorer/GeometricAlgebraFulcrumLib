using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

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