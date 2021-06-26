using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.SymbolicExpressions.Context;

namespace GeometricAlgebraLib.SymbolicExpressions.Factories
{
    public abstract class SymbolicAtomicExpressionsFactoryBase
    {
        public SymbolicContext Context { get; }

        public IGaScalarProcessor<ISymbolicExpression> ScalarProcessor
            => Context.SymbolicExpressionProcessor;


        protected SymbolicAtomicExpressionsFactoryBase([NotNull] SymbolicContext context)
        {
            Context = context;
        }
    }
}
