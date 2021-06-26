using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraLib.SymbolicExpressions.Context
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
