using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using TextComposerLib.Files;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public abstract class GaFuLSymbolicContextCodeComposerBase :
        GaFuLCodeLibraryComposerBase
    {
        public TextFileComposer FileComposer 
            => ActiveFileComposer;

        public LinearTextComposer TextComposer 
            => FileComposer.TextComposer;

        public SymbolicContext Context { get; private set; }

        public GaFuLSymbolicContextCodeComposer ContextCodeComposer { get; private set; }


        protected GaFuLSymbolicContextCodeComposerBase(GaFuLLanguageServerBase targetLanguage)
            : base(targetLanguage)
        {
        }


        protected virtual void SetContextOptions(SymbolicContextOptions options)
        {
        }

        protected abstract void DefineContextParameters(SymbolicContext context);

        protected abstract void DefineContextComputations(SymbolicContext context);

        protected abstract void DefineContextExternalNames(SymbolicContext context);

        protected virtual void SetContextCodeComposerOptions(GaFuLSymbolicContextCodeComposerOptions options)
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
                new GaFuLSymbolicContextCodeComposer(
                    GaLanguage, 
                    Context, 
                    DefaultContextCodeComposerOptions
                );

            SetContextCodeComposerOptions(ContextCodeComposer.ComposerOptions);

            return ContextCodeComposer.Generate();
        }
    }
}