using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code.Commands;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Constants;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Documents;

public sealed class LaTeXPreamble : LaTeXCodeSectionsList
{
    public LaTeXPreamble AddRenameElementTitle(string elementName, string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(elementName, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenameAbstract(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.Abstract, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenameAppendix(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.Appendix, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenameBibliography(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.Bibliography, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenameChapter(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.Chapter, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenameContents(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.Contents, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenameFigure(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.Figure, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenameIndex(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.Index, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenameListOfFigures(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.ListOfFigures, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenameListOfTables(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.ListOfTables, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenamePart(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.Part, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenameReferences(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.References, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddRenameTable(string newValue)
    {
        SubSectionsList.Add(
            LaTeXCommands.RenewCommand(LaTeXElementNames.Table, newValue)
        );

        return this;
    }

    public LaTeXPreamble AddSetCounter(string counterName, int value)
    {
        SubSectionsList.Add(
            LaTeXCommands.SetCounter(counterName, value)
        );

        return this;
    }

    //public LaTeXPreamble AddSetSectionNumberDepthCounter(int value)
    //{
    //    SubSectionsList.Add(
    //        LaTeXSetCounter.CreateSectionNumberDepth(value)
    //    );

    //    return this;
    //}

    //public LaTeXPreamble AddSetTableOfContentsDepthCounter(int value)
    //{
    //    SubSectionsList.Add(
    //        LaTeXSetCounter.CreateTableOfContentsDepth(value)
    //    );

    //    return this;
    //}

    public LaTeXPreamble AddUsePackage(string packageName)
    {
        SubSectionsList.Add(
            LaTeXCommands.UsePackage(packageName)
        );

        return this;
    }
}