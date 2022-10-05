using GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code;
using GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code.Commands;
using GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Constants;

namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Documents
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