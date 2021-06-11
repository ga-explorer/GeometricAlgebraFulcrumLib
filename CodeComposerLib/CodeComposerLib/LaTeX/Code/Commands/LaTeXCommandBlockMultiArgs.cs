using System.Collections.Generic;
using CodeComposerLib.LaTeX.Code.Arguments;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.LaTeX.Code.Commands
{
    /// <summary>
    /// A LaTeX block command with inner commands and multiple arguments
    /// </summary>
    public class LaTeXCommandBlockMultiArgs : LaTeXCommandBlock
    {
        public static LaTeXCommandBlockMultiArgs Create(string commandName)
        {
            return new LaTeXCommandBlockMultiArgs(commandName);
        }

        public static LaTeXCommandBlockMultiArgs Create(string commandName, string closingName)
        {
            return new LaTeXCommandBlockMultiArgs(commandName, closingName);
        }


        public LaTeXArgumentsList ArgumentsList { get; }
            = new LaTeXArgumentsList();

        public override IEnumerable<LaTeXArgument> Arguments
            => ArgumentsList;


        public LaTeXCommandBlockMultiArgs(string name) 
            : base(name)
        {
        }

        protected LaTeXCommandBlockMultiArgs(string name, string closingName) 
            : base(name, closingName)
        {
        }


        public override void ToText(LinearTextComposer composer)
        {
            composer
                .AppendAtNewLine(@"\")
                .Append(CommandName);

            ArgumentsList.ToText(composer);

            if (SubSectionsList.Count > 0)
            {
                composer.IncreaseIndentation();

                SubSectionsList.ToText(composer);

                composer.DecreaseIndentation();
            }

            if (!string.IsNullOrEmpty(ClosingName))
                composer
                    .AppendAtNewLine(@"\")
                    .Append(ClosingName);
        }
    }
}