using GeometricAlgebraFulcrumLib.Utilities.Code.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Visitors;

public interface IMetaExpressionSyntaxConverter :
    IMetaExpressionDynamicVisitor<ISyntaxTreeElement>
{
    CclLanguageInfo SourceLanguageInfo { get; }

    CclLanguageInfo TargetLanguageInfo { get; }
}