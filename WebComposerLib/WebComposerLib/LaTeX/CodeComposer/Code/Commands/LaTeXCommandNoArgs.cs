using TextComposerLib.Text.Linear;
using WebComposerLib.LaTeX.CodeComposer.Code.Arguments;

namespace WebComposerLib.LaTeX.CodeComposer.Code.Commands
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