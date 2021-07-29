using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using TextComposerLib.Files;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public abstract class GaSymbolicContextCodeComposerBase :
        GaCodeLibraryComposerBase
    {
        public TextFileComposer FileComposer 
            => ActiveFileComposer;

        public LinearTextComposer TextComposer 
            => FileComposer.TextComposer;

        public SymbolicContext Context { get; private set; }

        public GaSymbolicContextCodeComposer ContextCodeComposer { get; private set; }


        protected GaSymbolicContextCodeComposerBase(GaLanguageServer targetLanguage)
            : base(targetLanguage)
        {
        }


        protected virtual void SetContextOptions(SymbolicContextOptions options)
        {
        }

        protected abstract void DefineContextParameters(SymbolicContext context);

        protected abstract void DefineContextComputations(SymbolicContext context);

        protected abstract void DefineContextExternalNames(SymbolicContext context);

        protected virtual void SetContextCodeComposerOptions(GaSymbolicContextCodeComposerOptions options)
        {
        }


        protected string GenerateCode()
        {
            Context = new SymbolicContext(DefaultContextOptions);

            SetContextOptions(Context.ContextOptions);

            DefineContextParameters(Context);

            DefineContextComputations(Context);

            Context.OptimizeContext();

            DefineContextExternalNames(Context);

            ContextCodeComposer = 
                new GaSymbolicContextCodeComposer(
                    GaLanguage, 
                    Context, 
                    DefaultContextCodeComposerOptions
                );

            SetContextCodeComposerOptions(ContextCodeComposer.ComposerOptions);

            return ContextCodeComposer.Generate();
        }
    }
}