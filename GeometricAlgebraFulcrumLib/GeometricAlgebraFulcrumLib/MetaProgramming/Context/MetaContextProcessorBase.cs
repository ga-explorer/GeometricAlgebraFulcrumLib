using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context
{
    internal abstract class MetaContextProcessorBase
    {
        public MetaContext Context { get; }


        protected MetaContextProcessorBase([NotNull] MetaContext context)
        {
            Context = context;
        }


        protected abstract void BeginProcessing();
    }
}
