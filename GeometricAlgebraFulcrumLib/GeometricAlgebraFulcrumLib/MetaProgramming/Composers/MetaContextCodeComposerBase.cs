using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using TextComposerLib.Files;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Composers;

public abstract class MetaContextCodeComposerBase :
    GaFuLCodeLibraryComposerBase
{
    public TextFileComposer FileComposer 
        => ActiveFileComposer;

    public LinearTextComposer TextComposer 
        => FileComposer.TextComposer;

    public MetaContext Context { get; private set; }

    public GaFuLMetaContextCodeComposer ContextCodeComposer { get; private set; }


    protected MetaContextCodeComposerBase(GaFuLLanguageServerBase targetLanguage)
        : base(targetLanguage)
    {
    }


    protected virtual void SetContextOptions(MetaContextOptions options)
    {
    }

    protected abstract void DefineContextParameters(MetaContext context);

    protected abstract void DefineContextComputations(MetaContext context);

    protected abstract void DefineContextExternalNames(MetaContext context);

    protected virtual void SetContextCodeComposerOptions(GaFuLMetaContextCodeComposerOptions options)
    {
    }


    protected string GenerateCode()
    {
        Context = new MetaContext(DefaultContextOptions);

        SetContextOptions(Context.ContextOptions);

        DefineContextParameters(Context);

        DefineContextComputations(Context);

        Context.OptimizeContext();

        DefineContextExternalNames(Context);

        ContextCodeComposer = 
            new GaFuLMetaContextCodeComposer(
                GeoLanguage, 
                Context, 
                DefaultContextCodeComposerOptions
            );

        SetContextCodeComposerOptions(ContextCodeComposer.ComposerOptions);

        return ContextCodeComposer.Generate();
    }
}