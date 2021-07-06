using CodeComposerLib.Languages;
using CodeComposerLib.SyntaxTree;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Visitors
{
    public interface ISymbolicExpressionSyntaxConverter : 
        ISymbolicExpressionDynamicVisitor<ISyntaxTreeElement>
    {
        LanguageInfo SourceLanguageInfo { get; }

        LanguageInfo TargetLanguageInfo { get; }
    }
}