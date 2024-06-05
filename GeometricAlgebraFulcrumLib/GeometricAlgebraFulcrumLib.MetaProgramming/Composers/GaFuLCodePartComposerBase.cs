using GeometricAlgebraFulcrumLib.Utilities.Code.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Text.Loggers.Progress;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Composers;

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

    public GaFuLLanguageServerBase GeoLanguage 
        => CodeComposer.GeoLanguage;

    public CclLanguageCodeGeneratorBase CodeGenerator 
        => CodeComposer.GeoLanguage.CodeGenerator;

    public CclLanguageSyntaxFactory SyntaxFactory 
        => CodeComposer.GeoLanguage.SyntaxFactory;


    protected GaFuLCodePartComposerBase(IGaFuLCodeComposer codeLibraryComposer)
    {
        CodeComposer = codeLibraryComposer;
    }
}