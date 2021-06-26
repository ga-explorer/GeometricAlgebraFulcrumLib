using CodeComposerLib.Languages;
using GeometricAlgebraLib.CodeComposer.LanguageServers;
using TextComposerLib.Logs.Progress;
using TextComposerLib.Text.Parametric;

namespace GeometricAlgebraLib.CodeComposer.Composers
{
    /// <summary>
    /// This abstract class can be used to implement a sub-process of code generation using the main
    /// code library generator components
    /// </summary>
    public abstract class GaClcCodePartComposerBase : 
        IProgressReportSource
    {
        public GaCodeLibraryComposerBase LibraryComposer { get; }

        public ParametricTextComposerCollection Templates 
            => LibraryComposer.Templates;

        public virtual string ProgressSourceId 
            => GetType().FullName;

        public ProgressComposer Progress 
            => null;

        public GaClcLanguageServer GaClcLanguage 
            => LibraryComposer.GaClcLanguage;

        public LanguageCodeGenerator CodeGenerator 
            => LibraryComposer.GaClcLanguage.CodeGenerator;

        public LanguageSyntaxFactory SyntaxFactory 
            => LibraryComposer.GaClcLanguage.SyntaxFactory;


        protected GaClcCodePartComposerBase(GaCodeLibraryComposerBase codeLibraryComposer)
        {
            LibraryComposer = codeLibraryComposer;
        }
    }
}
