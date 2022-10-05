using GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code.Arguments;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code.Commands
{
    /// <summary>
    /// A LaTeX command with no child commands and no arguments
    /// </summary>
    public class LaTeXCommandNoArgs : LaTeXCommand
    {
        public static LaTeXCommandNoArgs Create(string commandName)
        {
            return new LaTeXCommandNoArgs(commandName);
        }


        public override IEnumerable<LaTeXArgument> Arguments
            => Enumerable.Empty<LaTeXArgument>();


        protected LaTeXCommandNoArgs(string commandName) 
            : base(commandName)
        {
        }


        public override void ToText(LinearTextComposer composer)
        {
            composer
                .AppendAtNewLine(@"\")
                .Append(CommandName);
        }
    }
}