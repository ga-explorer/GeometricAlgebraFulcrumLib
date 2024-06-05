using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages;

public interface ICclLanguageSyntaxConverter : 
    ISteDynamicVisitor<ISyntaxTreeElement>
{
    CclLanguageInfo SourceLanguageInfo { get; }

    CclLanguageInfo TargetLanguageInfo { get; }
}