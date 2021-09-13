using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public abstract class SymbolicAtomicExpressionFactoryBase
    {
        public SymbolicContext Context { get; }

        public IScalarAlgebraProcessor<ISymbolicExpression> ScalarProcessor
            => Context.SymbolicScalarProcessor;


        protected SymbolicAtomicExpressionFactoryBase([NotNull] SymbolicContext context)
        {
            Context = context;
        }
    }
}
