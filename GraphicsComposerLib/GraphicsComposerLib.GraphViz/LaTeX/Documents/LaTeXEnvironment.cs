using GraphicsComposerLib.GraphViz.LaTeX.Code.Commands;
using GraphicsComposerLib.GraphViz.LaTeX.Constants;

namespace GraphicsComposerLib.GraphViz.LaTeX.Documents
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
