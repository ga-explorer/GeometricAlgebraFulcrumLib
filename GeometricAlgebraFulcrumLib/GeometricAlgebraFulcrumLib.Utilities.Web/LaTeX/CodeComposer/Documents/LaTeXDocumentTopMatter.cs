using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code.Commands;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Constants;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Documents;

public sealed class LaTeXDocumentTopMatter : LaTeXCodeSectionsList
{
    public LaTeXCommandOneArg Title
        => FindOrAddCommandOneArg(LaTeXCommandTagNames.Title);

    public LaTeXCommandOneArg Author
        => FindOrAddCommandOneArg(LaTeXCommandTagNames.Author);

    public LaTeXCommandOneArg Date
        => FindOrAddCommandOneArg(LaTeXCommandTagNames.Date);
}