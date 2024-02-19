using WebComposerLib.LaTeX.CodeComposer.Code;

namespace WebComposerLib.LaTeX.CodeComposer.Documents;

/// <summary>
/// https://en.wikibooks.org/wiki/LaTeX/Document_Structure
/// </summary>
public sealed class LaTeXDocumentSection : LaTeXEnvironment
{
    public LaTeXDocumentSection Create(LaTeXDocumentSectionKind sectionKind, string title)
    {
        return new LaTeXDocumentSection(sectionKind)
        {
            SectionTitle = title.ToLaTeXText()
        };
    }

    public LaTeXDocumentSection CreatePart(string title)
    {
        return new LaTeXDocumentSection(LaTeXDocumentSectionKind.Part)
        {
            SectionTitle = title.ToLaTeXText()
        };
    }

    public LaTeXDocumentSection CreateChapter(string title)
    {
        return new LaTeXDocumentSection(LaTeXDocumentSectionKind.Chapter)
        {
            SectionTitle = title.ToLaTeXText()
        };
    }

    public LaTeXDocumentSection CreateSection(string title)
    {
        return new LaTeXDocumentSection(LaTeXDocumentSectionKind.Section)
        {
            SectionTitle = title.ToLaTeXText()
        };
    }

    public LaTeXDocumentSection CreateSubSection(string title)
    {
        return new LaTeXDocumentSection(LaTeXDocumentSectionKind.SubSection)
        {
            SectionTitle = title.ToLaTeXText()
        };
    }

    public LaTeXDocumentSection CreateSubSubSection(string title)
    {
        return new LaTeXDocumentSection(LaTeXDocumentSectionKind.SubSubSection)
        {
            SectionTitle = title.ToLaTeXText()
        };
    }

    public LaTeXDocumentSection CreateParagraph(string title)
    {
        return new LaTeXDocumentSection(LaTeXDocumentSectionKind.Paragraph)
        {
            SectionTitle = title.ToLaTeXText()
        };
    }

    public LaTeXDocumentSection CreateSubParagraph(string title)
    {
        return new LaTeXDocumentSection(LaTeXDocumentSectionKind.SubParagraph)
        {
            SectionTitle = title.ToLaTeXText()
        };
    }


    public LaTeXDocumentSectionKind SectionKind { get; }

    public int SectionKindLevel 
        => (int) SectionKind;

    public string SectionKindName 
        => SectionKind.GetName(Unnumbered);

    public ILaTeXCodeElement SectionTitle { get; set; }

    public ILaTeXCodeElement SectionTocTitle { get; set; }

    public bool Unnumbered { get; set; }

    public bool HideInToc { get; set; }


    private LaTeXDocumentSection(LaTeXDocumentSectionKind sectionKind)
    {
        SectionKind = sectionKind;

        ArgumentsList.Set(0, SectionKindName.ToLaTeXText());
    }
}