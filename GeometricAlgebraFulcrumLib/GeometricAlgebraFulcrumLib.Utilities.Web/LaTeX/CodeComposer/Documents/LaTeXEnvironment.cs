using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code.Commands;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Constants;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Documents;

public class LaTeXEnvironment : LaTeXCommandBlockMultiArgs
{
    public LaTeXEnvironment() 
        : base(LaTeXCommandTagNames.Begin, LaTeXCommandTagNames.End)
    {
        ArgumentsList.FixedArgumentsCount = 1;
    }
}