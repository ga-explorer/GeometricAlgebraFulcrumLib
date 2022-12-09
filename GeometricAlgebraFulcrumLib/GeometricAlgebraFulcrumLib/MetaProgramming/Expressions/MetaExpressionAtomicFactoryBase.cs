using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions
{
    public abstract class MetaExpressionAtomicFactoryBase
    {
        public MetaContext Context { get; }

        public IScalarAlgebraProcessor<IMetaExpression> ScalarProcessor
            => Context.MetaExpressionProcessor;


        protected MetaExpressionAtomicFactoryBase([NotNull] MetaContext context)
        {
            Context = context;
        }
    }
}
