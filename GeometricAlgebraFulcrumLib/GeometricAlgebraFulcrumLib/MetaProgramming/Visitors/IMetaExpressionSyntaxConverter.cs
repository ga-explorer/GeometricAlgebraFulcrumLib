using CodeComposerLib.Languages;
using CodeComposerLib.SyntaxTree;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Visitors;

public interface IMetaExpressionSyntaxConverter : 
    IMetaExpressionDynamicVisitor<ISyntaxTreeElement>
{
    CclLanguageInfo SourceLanguageInfo { get; }

    CclLanguageInfo TargetLanguageInfo { get; }
}