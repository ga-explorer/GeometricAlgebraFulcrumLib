using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Factories
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
