using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages.CSharp
{
    // ReSharper disable once InconsistentNaming
    public sealed class GaFuLCSharpSymbolicContextComposer :
        GaFuLSymbolicContextCodeComposerBase
    {
        public override string Name { get; }

        public override string Description { get; }


        public GaFuLCSharpSymbolicContextComposer(GaFuLLanguageServerBase targetLanguage) : base(targetLanguage)
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

        protected override void DefineContextParameters(SymbolicContext context)
        {
            throw new System.NotImplementedException();
        }

        protected override void DefineContextComputations(SymbolicContext context)
        {
            throw new System.NotImplementedException();
        }

        protected override void DefineContextExternalNames(SymbolicContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}