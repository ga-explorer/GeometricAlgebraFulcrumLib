using CodeComposerLib.Languages;
using CodeComposerLib.SyntaxTree;

namespace GeometricAlgebraLib.SymbolicExpressions.Visitors
{
    public interface ISymbolicExpressionSyntaxConverter : 
        ISymbolicExpressionDynamicVisitor<ISyntaxTreeElement>
    {
        LanguageInfo SourceLanguageInfo { get; }

        LanguageInfo TargetLanguageInfo { get; }
    }
}