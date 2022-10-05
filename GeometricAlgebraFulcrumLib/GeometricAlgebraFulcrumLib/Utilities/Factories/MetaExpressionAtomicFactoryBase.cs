using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
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
