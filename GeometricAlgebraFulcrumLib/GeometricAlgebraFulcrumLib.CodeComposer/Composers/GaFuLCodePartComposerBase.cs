using CodeComposerLib.Languages;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using TextComposerLib.Loggers.Progress;
using TextComposerLib.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    /// <summary>
    /// This abstract class can be used to implement a sub-process of code generation using the main
    /// code library generator components
    /// </summary>
    public abstract class GaFuLCodePartComposerBase : 
        IProgressReportSource
    {
        public IGaFuLCodeComposer CodeComposer { get; }

        public ParametricTextComposerCollection Templates 
            => CodeComposer.Templates;

        public virtual string ProgressSourceId 
            => GetType().FullName;

        public ProgressComposer Progress 
            => null;

        public GaFuLLanguageServerBase GaLanguage 
            => CodeComposer.GaLanguage;

        public CclLanguageCodeGeneratorBase CodeGenerator 
            => CodeComposer.GaLanguage.CodeGenerator;

        public CclLanguageSyntaxFactory SyntaxFactory 
            => CodeComposer.GaLanguage.SyntaxFactory;


        protected GaFuLCodePartComposerBase(IGaFuLCodeComposer codeLibraryComposer)
        {
            CodeComposer = codeLibraryComposer;
        }
    }
}
