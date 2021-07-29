using CodeComposerLib.Languages;
using CodeComposerLib.SyntaxTree;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Visitors
{
    public interface ISymbolicExpressionSyntaxConverter : 
        ISymbolicExpressionDynamicVisitor<ISyntaxTreeElement>
    {
        CclLanguageInfo SourceLanguageInfo { get; }

        CclLanguageInfo TargetLanguageInfo { get; }
    }
}