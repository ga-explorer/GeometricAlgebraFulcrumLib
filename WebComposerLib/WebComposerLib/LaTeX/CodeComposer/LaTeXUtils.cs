using WebComposerLib.LaTeX.CodeComposer.Code;
using WebComposerLib.LaTeX.CodeComposer.Documents;

namespace WebComposerLib.LaTeX.CodeComposer
{
    public static class LaTeXUtils
    {
        public static string GetName(this LaTeXDocumentMatterKind matterKind)
        {
            return matterKind switch
            {
                LaTeXDocumentMatterKind.FromtMatter => "frontmatter",
                LaTeXDocumentMatterKind.MainMatter => "mainmatter",
                LaTeXDocumentMatterKind.Appendix => "appendix",
                _ => "backmatter"
            };
        }

        public static string GetName(this LaTeXDocumentSectionKind sectionKind, bool unnumbered)
        {
            if (unnumbered)
            {
                return sectionKind switch
                {
                    LaTeXDocumentSectionKind.Part => "part*",
                    LaTeXDocumentSectionKind.Chapter => "chapter*",
                    LaTeXDocumentSectionKind.Section => "section*",
                    LaTeXDocumentSectionKind.SubSection => "subsection*",
                    LaTeXDocumentSectionKind.SubSubSection => "subsubsection*",
                    LaTeXDocumentSectionKind.Paragraph => "paragraph*",
                    _ => "subparagraph*"
                };
            }

            return sectionKind switch
            {
                LaTeXDocumentSectionKind.Part => "part",
                LaTeXDocumentSectionKind.Chapter => "chapter",
                LaTeXDocumentSectionKind.Section => "section",
                LaTeXDocumentSectionKind.SubSection => "subsection",
                LaTeXDocumentSectionKind.SubSubSection => "subsubsection",
                LaTeXDocumentSectionKind.Paragraph => "paragraph",
                _ => "subparagraph"
            };
        }



        public static bool IsPreamble(this ILaTeXCodeElement code)
        {
            return code is LaTeXPreamble;
        }

        public static bool IsDocumentClass(this ILaTeXCodeElement code)
        {
            return code is LaTeXDocumentClass;
        }

        public static bool IsDocument(this ILaTeXCodeElement code)
        {
            return code is LaTeXDocument;
        }

        public static bool IsTopMatter(this ILaTeXCodeElement code)
        {
            return code is LaTeXDocumentTopMatter;
        }

        public static bool IsAbstract(this ILaTeXCodeElement code)
        {
            return code is LaTeXDocumentAbstract;
        }

        public static bool IsDocumentSection(this ILaTeXCodeElement code)
        {
            return code is LaTeXDocumentSection;
        }

        public static bool IsDocumentSection(this ILaTeXCodeElement code, LaTeXDocumentSectionKind sectionKind)
        {
            if (code is not LaTeXDocumentSection content) return false;

            return content.SectionKind == sectionKind;
        }

        public static bool IsPart(this ILaTeXCodeElement code)
        {
            if (code is not LaTeXDocumentSection content) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.Part;
        }

        public static bool IsChapter(this ILaTeXCodeElement code)
        {
            if (code is not LaTeXDocumentSection content) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.Chapter;
        }

        public static bool IsSection(this ILaTeXCodeElement code)
        {
            if (code is not LaTeXDocumentSection content) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.Section;
        }

        public static bool IsSubSection(this ILaTeXCodeElement code)
        {
            if (code is not LaTeXDocumentSection content) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.SubSection;
        }

        public static bool IsSubSubSection(this ILaTeXCodeElement code)
        {
            if (code is not LaTeXDocumentSection content) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.SubSubSection;
        }

        public static bool IsParagraph(this ILaTeXCodeElement code)
        {
            if (code is not LaTeXDocumentSection content) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.Paragraph;
        }

        public static bool IsSubParagraph(this ILaTeXCodeElement code)
        {
            if (code is not LaTeXDocumentSection content) return false;

            return content.SectionKind == LaTeXDocumentSectionKind.SubParagraph;
        }

        public static bool IsDocumentMatter(this ILaTeXCodeElement code)
        {
            return code is LaTeXDocumentMatter;
        }

        public static bool IsDocumentMatter(this ILaTeXCodeElement code, LaTeXDocumentMatterKind matterKind)
        {
            if (code is not LaTeXDocumentMatter content) return false;

            return content.MatterKind == matterKind;
        }

        public static bool IsFrontMatter(this ILaTeXCodeElement code)
        {
            if (code is not LaTeXDocumentMatter content) return false;

            return content.MatterKind == LaTeXDocumentMatterKind.FromtMatter;
        }

        public static bool IsMainMatter(this ILaTeXCodeElement code)
        {
            if (code is not LaTeXDocumentMatter content) return false;

            return content.MatterKind == LaTeXDocumentMatterKind.MainMatter;
        }

        public static bool IsAppendix(this ILaTeXCodeElement code)
        {
            if (code is not LaTeXDocumentMatter content) return false;

            return content.MatterKind == LaTeXDocumentMatterKind.Appendix;
        }

        public static bool IsBackMatter(this ILaTeXCodeElement code)
        {
            if (code is not LaTeXDocumentMatter content) return false;

            return content.MatterKind == LaTeXDocumentMatterKind.BackMatter;
        }
    }
}
