using GraphicsComposerLib.GraphViz.LaTeX.Code;
using GraphicsComposerLib.GraphViz.LaTeX.Documents;

namespace GraphicsComposerLib.GraphViz.LaTeX
{
    public static class LaTeXUtils
    {
        public static string GetName(this LaTeXDocumentMatterKind matterKind)
        {
            if (matterKind == LaTeXDocumentMatterKind.FromtMatter)
                return "frontmatter";

            if (matterKind == LaTeXDocumentMatterKind.MainMatter)
                return "mainmatter";

            if (matterKind == LaTeXDocumentMatterKind.Appendix)
                return "appendix";

            return "backmatter";
        }

        public static string GetName(this LaTeXDocumentSectionKind sectionKind, bool unnumbered)
        {
            if (unnumbered)
            {
                if (sectionKind == LaTeXDocumentSectionKind.Part)
                    return "part*";

                if (sectionKind == LaTeXDocumentSectionKind.Chapter)
                    return "chapter*";

                if (sectionKind == LaTeXDocumentSectionKind.Section)
                    return "section*";

                if (sectionKind == LaTeXDocumentSectionKind.SubSection)
                    return "subsection*";

                if (sectionKind == LaTeXDocumentSectionKind.SubSubSection)
                    return "subsubsection*";

                if (sectionKind == LaTeXDocumentSectionKind.Paragraph)
                    return "paragraph*";

                return "subparagraph*";
            }

            if (sectionKind == LaTeXDocumentSectionKind.Part)
                return "part";

            if (sectionKind == LaTeXDocumentSectionKind.Chapter)
                return "chapter";

            if (sectionKind == LaTeXDocumentSectionKind.Section)
                return "section";

            if (sectionKind == LaTeXDocumentSectionKind.SubSection)
                return "subsection";

            if (sectionKind == LaTeXDocumentSectionKind.SubSubSection)
                return "subsubsection";

            if (sectionKind == LaTeXDocumentSectionKind.Paragraph)
                return "paragraph";

            return "subparagraph";
        }



        public static bool IsPreamble(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXPreamble;

            return !ReferenceEquals(content, null);
        }

        public static bool IsDocumentClass(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentClass;

            return !ReferenceEquals(content, null);
        }

        public static bool IsDocument(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocument;

            return !ReferenceEquals(content, null);
        }

        public static bool IsTopMatter(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentTopMatter;

            return !ReferenceEquals(content, null);
        }

        public static bool IsAbstract(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentAbstract;

            return !ReferenceEquals(content, null);
        }

        public static bool IsDocumentSection(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentSection;

            return !ReferenceEquals(content, null);
        }

        public static bool IsDocumentSection(this ILaTeXCodeElement code, LaTeXDocumentSectionKind sectionKind)
        {
            var content = code as LaTeXDocumentSection;

            if (ReferenceEquals(content, null)) return false;

            return content.SectionKind == sectionKind;
        }

        public static bool IsPart(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentSection;

            if (ReferenceEquals(content, null)) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.Part;
        }

        public static bool IsChapter(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentSection;

            if (ReferenceEquals(content, null)) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.Chapter;
        }

        public static bool IsSection(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentSection;

            if (ReferenceEquals(content, null)) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.Section;
        }

        public static bool IsSubSection(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentSection;

            if (ReferenceEquals(content, null)) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.SubSection;
        }

        public static bool IsSubSubSection(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentSection;

            if (ReferenceEquals(content, null)) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.SubSubSection;
        }

        public static bool IsParagraph(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentSection;

            if (ReferenceEquals(content, null)) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.Paragraph;
        }

        public static bool IsSubParagraph(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentSection;

            if (ReferenceEquals(content, null)) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.SubParagraph;
        }

        public static bool IsDocumentMatter(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentMatter;

            return !ReferenceEquals(content, null);
        }

        public static bool IsDocumentMatter(this ILaTeXCodeElement code, LaTeXDocumentMatterKind matterKind)
        {
            var content = code as LaTeXDocumentMatter;

            if (ReferenceEquals(content, null)) return false;

            return content.MatterKind == matterKind;
        }

        public static bool IsFrontMatter(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentMatter;

            if (ReferenceEquals(content, null)) return false;

            return content.MatterKind == LaTeXDocumentMatterKind.FromtMatter;
        }

        public static bool IsMainMatter(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentMatter;

            if (ReferenceEquals(content, null)) return false;

            return content.MatterKind == LaTeXDocumentMatterKind.MainMatter;
        }

        public static bool IsAppendix(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentMatter;

            if (ReferenceEquals(content, null)) return false;

            return content.MatterKind == LaTeXDocumentMatterKind.Appendix;
        }

        public static bool IsBackMatter(this ILaTeXCodeElement code)
        {
            var content = code as LaTeXDocumentMatter;

            if (ReferenceEquals(content, null)) return false;

            return content.MatterKind == LaTeXDocumentMatterKind.BackMatter;
        }
    }
}
