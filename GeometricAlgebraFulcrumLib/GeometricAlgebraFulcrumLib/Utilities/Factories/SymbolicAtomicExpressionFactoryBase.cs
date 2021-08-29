using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public abstract class SymbolicAtomicExpressionFactoryBase
    {
        public SymbolicContext Context { get; }

        public IScalarProcessor<ISymbolicExpression> ScalarProcessor
            => Context.SymbolicExpressionProcessor;


        protected SymbolicAtomicExpressionFactoryBase([NotNull] SymbolicContext context)
        {
            Context = context;
        }
    }
}
