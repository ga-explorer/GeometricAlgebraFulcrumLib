using GraphicsComposerLib.GraphViz.LaTeX.Code;
using GraphicsComposerLib.GraphViz.LaTeX.Code.Commands;
using GraphicsComposerLib.GraphViz.LaTeX.Constants;

namespace GraphicsComposerLib.GraphViz.LaTeX.Documents
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