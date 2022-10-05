using GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code.Commands;
using GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Constants;

namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Documents
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
