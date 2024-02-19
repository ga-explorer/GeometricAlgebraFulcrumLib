namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context;

internal abstract class MetaContextProcessorBase
{
    public MetaContext Context { get; }


    protected MetaContextProcessorBase(MetaContext context)
    {
        Context = context;
    }


    protected abstract void BeginProcessing();
}