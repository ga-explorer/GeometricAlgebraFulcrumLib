using CodeComposerLib.Languages;
using TextComposerLib.Files;
using TextComposerLib.Loggers.Progress;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;

namespace CodeComposerLib
{
    public interface ICclCodeComposer : 
        IProgressReportSource
    {
        string Name { get; }

        string Description { get; }

        CclLanguageServerBase Language { get; }

        CclLanguageCodeGeneratorBase CodeComposer { get; }

        CclLanguageSyntaxFactory SyntaxFactory { get; }

        TextFileComposer ActiveFileComposer { get; }

        ParametricTextComposerCollection Templates { get; }

        LinearTextComposer ActiveFileTextComposer { get; }
    }
}