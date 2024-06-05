using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages;

public interface ICclExpressionToLanguageConverter<in T> :
    IDynamicTreeVisitor<T, SteExpression> where T : class
{
    CclLanguageInfo TargetLanguageInfo { get; }
}