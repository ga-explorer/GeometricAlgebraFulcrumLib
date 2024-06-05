using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;
using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages;

public interface ICclIntoLanguageSyntaxConverter<in T> :
    IDynamicTreeVisitor<T, ISyntaxTreeElement> where T : class
{
    CclLanguageInfo TargetLanguageInfo { get; }
}