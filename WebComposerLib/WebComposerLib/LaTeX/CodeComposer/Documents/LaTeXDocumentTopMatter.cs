using WebComposerLib.LaTeX.CodeComposer.Code;
using WebComposerLib.LaTeX.CodeComposer.Code.Commands;
using WebComposerLib.LaTeX.CodeComposer.Constants;

namespace WebComposerLib.LaTeX.CodeComposer.Documents
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