using TextComposerLib.Text.Linear;
using WebComposerLib.LaTeX.CodeComposer.Code.Arguments;

namespace WebComposerLib.LaTeX.CodeComposer.Code.Commands
{
    /// <summary>
    /// A LaTeX command with no child commands and multiple arguments
    /// </summary>
    public class LaTeXCommandMultiArgs : LaTeXCommand
    {
        public static LaTeXCommandMultiArgs Create(string commandName)
        {
            return new LaTeXCommandMultiArgs(commandName);
        }


        public LaTeXArgumentsList ArgumentsList { get; }
            = new LaTeXArgumentsList();

        public override IEnumerable<LaTeXArgument> Arguments 
            => ArgumentsList;


        protected LaTeXCommandMultiArgs(string commandName) 
            : base(commandName)
        {
        }


        public override void ToText(LinearTextComposer composer)
        {
            composer
                .AppendAtNewLine(@"\")
                .Append(CommandName);

            ArgumentsList.ToText(composer);
        }
    }
}