using CodeComposerLib.Languages;
using CodeComposerLib.SyntaxTree;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Visitors
{
    public interface ISymbolicExpressionSyntaxConverter : 
        ISymbolicExpressionDynamicVisitor<ISyntaxTreeElement>
    {
        CclLanguageInfo SourceLanguageInfo { get; }

        CclLanguageInfo TargetLanguageInfo { get; }
    }
}