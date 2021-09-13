using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context
{
    internal abstract class SymbolicContextProcessorBase
    {
        public SymbolicContext Context { get; }


        protected SymbolicContextProcessorBase([NotNull] SymbolicContext context)
        {
            Context = context;
        }


        protected abstract void BeginProcessing();
    }
}
