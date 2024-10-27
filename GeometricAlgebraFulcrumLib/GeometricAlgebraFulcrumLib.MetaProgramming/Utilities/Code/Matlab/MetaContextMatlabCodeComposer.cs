using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code.Matlab;

// ReSharper disable once InconsistentNaming
public sealed class MetaContextMatlabCodeComposer :
    MetaContextCodeComposerBase
{
    public override string Name { get; }

    public override string Description { get; }


    public MetaContextMatlabCodeComposer(GaFuLLanguageServerBase targetLanguage) : base(targetLanguage)
    {
    }


    protected override bool InitializeTemplates()
    {
        throw new NotImplementedException();
    }

    protected override void InitializeSubComponents()
    {
        throw new NotImplementedException();
    }

    protected override bool VerifyReadyToGenerate()
    {
        throw new NotImplementedException();
    }

    protected override void ComposeTextFiles()
    {
        throw new NotImplementedException();
    }

    protected override void FinalizeSubComponents()
    {
        throw new NotImplementedException();
    }

    public override GaFuLCodeLibraryComposerBase CreateEmptyComposer()
    {
        throw new NotImplementedException();
    }

    protected override void DefineContextParameters(MetaContext context)
    {
        throw new NotImplementedException();
    }

    protected override void DefineContextComputations(MetaContext context)
    {
        throw new NotImplementedException();
    }

    protected override void DefineContextExternalNames(MetaContext context)
    {
        throw new NotImplementedException();
    }
}