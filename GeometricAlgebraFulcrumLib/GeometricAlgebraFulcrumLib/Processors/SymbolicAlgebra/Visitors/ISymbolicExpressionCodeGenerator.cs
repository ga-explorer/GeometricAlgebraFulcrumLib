using CodeComposerLib.Languages;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Visitors
{
    public interface ISymbolicExpressionCodeGenerator : 
        ISymbolicExpressionDynamicVisitor
    {
        LinearTextComposer TextComposer { get; }

        /// <summary>
        /// Target language information
        /// </summary>
        CclLanguageInfo LanguageInfo { get; }

        /// <summary>
        /// The indentation string for this language code composer
        /// </summary>
        string Indentation { get; }
    }
}