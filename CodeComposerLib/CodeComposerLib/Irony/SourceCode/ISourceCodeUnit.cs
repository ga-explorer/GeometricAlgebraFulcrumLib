using System.Collections.Generic;
using System.Text;

namespace CodeComposerLib.Irony.SourceCode
{
    /// <summary>
    /// This interface represents a unit of source code text (for example a single code file in a project)
    /// </summary>
    public interface ISourceCodeUnit
    {
        /// <summary>
        /// True if this unit is a text file on disk
        /// </summary>
        bool IsFile { get; }

        /// <summary>
        /// True if this unit is a memory string
        /// </summary>
        bool IsText { get; }

        /// <summary>
        /// Returns the full disk path if this is a file code unit
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// The text encoding of the code unit
        /// </summary>
        Encoding TextEncoding { get; }

        /// <summary>
        /// The number of characters of this code unit
        /// </summary>
        int CharactersCount { get; }

        /// <summary>
        /// The number of code lines of this code unit
        /// </summary>
        int LinesCount { get; }

        /// <summary>
        /// The text if this code unit
        /// </summary>
        string CodeText { get; }

        /// <summary>
        /// A list of code lines of this unit
        /// </summary>
        IEnumerable<ISourceCodeUnitLine> CodeUnitLines { get; }

        /// <summary>
        /// Convert an absolute character position within the unit's code text into a code location object 
        /// within this code unit
        /// </summary>
        /// <param name="absolutePos"></param>
        /// <returns></returns>
        LanguageCodeLocation TranslateCharacterLocation(int absolutePos);

        /// <summary>
        /// Convert a line : column location within this code unit into an absolute character position within
        /// the unit's source code text
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="columnNumber"></param>
        /// <param name="absolutePos"></param>
        void TranslateCharacterLocation(int lineNumber, int columnNumber, out int absolutePos);

        /// <summary>
        /// Convert an absolute character position within the unit's code text into a line : column location
        /// within this code unit
        /// </summary>
        /// <param name="absolutePos"></param>
        /// <param name="lineNumber"></param>
        /// <param name="columnNumber"></param>
        void TranslateCharacterLocation(int absolutePos, out int lineNumber, out int columnNumber);

        /// <summary>
        /// Convert a line : column location within this code unit into a code location object within
        /// this code unit
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        LanguageCodeLocation TranslateCharacterLocation(int lineNumber, int columnNumber);
    }
}