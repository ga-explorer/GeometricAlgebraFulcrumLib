using WebComposerLib.LaTeX.CodeComposer.Code.Commands;

namespace WebComposerLib.LaTeX.CodeComposer.Documents
{
    /// <summary>
    /// https://en.wikibooks.org/wiki/LaTeX/Document_Structure
    /// </summary>
    public sealed class LaTeXDocumentMatter : LaTeXCommandBlockNoArgs
    {

        public static LaTeXDocumentMatter CreateFrontMatter()
        {
            return new LaTeXDocumentMatter(LaTeXDocumentMatterKind.FromtMatter);
        }

        public static LaTeXDocumentMatter CreateMainMatter()
        {
            return new LaTeXDocumentMatter(LaTeXDocumentMatterKind.MainMatter);
        }

        public static LaTeXDocumentMatter CreateAppendix()
        {
            return new LaTeXDocumentMatter(LaTeXDocumentMatterKind.Appendix);
        }

        public static LaTeXDocumentMatter CreateBackMatter()
        {
            return new LaTeXDocumentMatter(LaTeXDocumentMatterKind.BackMatter);
        }

        public static LaTeXDocumentMatter Create(LaTeXDocumentMatterKind matterKind)
        {
            return new LaTeXDocumentMatter(matterKind);
        }


        public LaTeXDocumentMatterKind MatterKind { get; }

        public string MatterKindName 
            => MatterKind.GetName();


        private LaTeXDocumentMatter(LaTeXDocumentMatterKind matterKind)
            : base(matterKind.GetName())
        {
            MatterKind = matterKind;
        }
    }
}