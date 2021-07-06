using CodeComposerLib.Languages;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Visitors
{
    public interface ISymbolicExpressionCodeGenerator : 
        ISymbolicExpressionDynamicVisitor
    {
        LinearTextComposer TextComposer { get; }

        /// <summary>
        /// Target language information
        /// </summary>
        LanguageInfo LanguageInfo { get; }

        /// <summary>
        /// The indentation string for this language code composer
        /// </summary>
        string Indentation { get; }
    }
}