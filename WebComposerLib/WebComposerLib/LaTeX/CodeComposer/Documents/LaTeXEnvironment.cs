using WebComposerLib.LaTeX.CodeComposer.Code.Commands;
using WebComposerLib.LaTeX.CodeComposer.Constants;

namespace WebComposerLib.LaTeX.CodeComposer.Documents;

public class LaTeXEnvironment : LaTeXCommandBlockMultiArgs
{
    public LaTeXEnvironment() 
        : base(LaTeXCommandTagNames.Begin, LaTeXCommandTagNames.End)
    {
        ArgumentsList.FixedArgumentsCount = 1;
    }
}