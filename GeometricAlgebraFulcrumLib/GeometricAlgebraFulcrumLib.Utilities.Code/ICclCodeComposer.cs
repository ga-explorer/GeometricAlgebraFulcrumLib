using GeometricAlgebraFulcrumLib.Utilities.Code.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Loggers.Progress;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.Utilities.Code;

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