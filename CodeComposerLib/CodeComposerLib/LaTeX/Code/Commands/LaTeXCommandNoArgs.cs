using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.LaTeX.Code.Arguments;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.LaTeX.Code.Commands
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