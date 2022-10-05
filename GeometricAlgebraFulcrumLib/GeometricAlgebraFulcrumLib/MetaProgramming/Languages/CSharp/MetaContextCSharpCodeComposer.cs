using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Languages.CSharp
{
    // ReSharper disable once InconsistentNaming
    public sealed class MetaContextCSharpCodeComposer :
        MetaContextCodeComposerBase
    {
        public override string Name { get; }

        public override string Description { get; }


        public MetaContextCSharpCodeComposer(GaFuLLanguageServerBase targetLanguage) : base(targetLanguage)
        {
        }


        protected override bool InitializeTemplates()
        {
            throw new System.NotImplementedException();
        }

        protected override void InitializeSubComponents()
        {
            throw new System.NotImplementedException();
        }

        protected override bool VerifyReadyToGenerate()
        {
            throw new System.NotImplementedException();
        }

        protected override void ComposeTextFiles()
        {
            throw new System.NotImplementedException();
        }

        protected override void FinalizeSubComponents()
        {
            throw new System.NotImplementedException();
        }

        public override GaFuLCodeLibraryComposerBase CreateEmptyComposer()
        {
            throw new System.NotImplementedException();
        }

        protected override void DefineContextParameters(MetaContext context)
        {
            throw new System.NotImplementedException();
        }

        protected override void DefineContextComputations(MetaContext context)
        {
            throw new System.NotImplementedException();
        }

        protected override void DefineContextExternalNames(MetaContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}