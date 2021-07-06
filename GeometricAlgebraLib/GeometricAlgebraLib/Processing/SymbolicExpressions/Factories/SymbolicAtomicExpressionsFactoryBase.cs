using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Factories
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
