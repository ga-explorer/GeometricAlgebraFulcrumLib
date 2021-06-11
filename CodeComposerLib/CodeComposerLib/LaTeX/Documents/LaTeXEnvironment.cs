using CodeComposerLib.LaTeX.Code.Commands;
using CodeComposerLib.LaTeX.Constants;

namespace CodeComposerLib.LaTeX.Documents
{
    public class LaTeXEnvironment : LaTeXCommandBlockMultiArgs
    {
        public LaTeXEnvironment() 
            : base(LaTeXCommandTagNames.Begin, LaTeXCommandTagNames.End)
        {
            ArgumentsList.FixedArgumentsCount = 1;
        }
    }
}
