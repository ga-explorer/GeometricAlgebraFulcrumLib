using CodeComposerLib.LaTeX.Code;
using CodeComposerLib.LaTeX.Code.Commands;
using CodeComposerLib.LaTeX.Constants;

namespace CodeComposerLib.LaTeX.Documents
{
    public sealed class LaTeXDocumentTopMatter : LaTeXCodeSectionsList
    {
        public LaTeXCommandOneArg Title
            => FindOrAddCommandOneArg(LaTeXCommandTagNames.Title);

        public LaTeXCommandOneArg Author
            => FindOrAddCommandOneArg(LaTeXCommandTagNames.Author);

        public LaTeXCommandOneArg Date
            => FindOrAddCommandOneArg(LaTeXCommandTagNames.Date);
    }
}