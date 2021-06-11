using CodeComposerLib.SyntaxTree;

namespace CodeComposerLib.Languages
{
    public interface ILanguageCodeGenerator : ISteDynamicVisitor
    {
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