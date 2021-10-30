using System.Collections.Generic;
using GraphicsComposerLib.GraphViz.LaTeX.Code.Arguments;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.GraphViz.LaTeX.Code.Commands
{
    /// <summary>
    /// A LaTeX block command with child commands and two arguments
    /// </summary>
    public class LaTeXCommandBlockTwoArgs : LaTeXCommandBlock
    {
        public static LaTeXCommandBlockTwoArgs Create(string commandName)
        {
            return new LaTeXCommandBlockTwoArgs(commandName);
        }

        public static LaTeXCommandBlockTwoArgs Create(string commandName, string closingName)
        {
            return new LaTeXCommandBlockTwoArgs(commandName, closingName);
        }

        public static LaTeXCommandBlockTwoArgs Create(string commandName, LaTeXArgument arg1, LaTeXArgument arg2)
        {
            return new LaTeXCommandBlockTwoArgs(commandName, arg1, arg2);
        }

        public static LaTeXCommandBlockTwoArgs Create(string commandName, string closingName, LaTeXArgument arg1, LaTeXArgument arg2)
        {
            return new LaTeXCommandBlockTwoArgs(commandName, closingName, arg1, arg2);
        }


        public LaTeXArgument Argument1 { get; }

        public LaTeXArgument Argument2 { get; }

        public override IEnumerable<LaTeXArgument> Arguments
        {
            get
            {
                yield return Argument1;
                yield return Argument2;
            }
        }


        protected LaTeXCommandBlockTwoArgs(string commandName)
            : base(commandName)
        {
            Argument1 = LaTeXArgument.Create();
            Argument2 = LaTeXArgument.Create();
        }

        protected LaTeXCommandBlockTwoArgs(string commandName, string closingName) 
            : base(commandName, closingName)
        {
            Argument1 = LaTeXArgument.Create();
            Argument2 = LaTeXArgument.Create();
        }

        protected LaTeXCommandBlockTwoArgs(string commandName, LaTeXArgument arg1, LaTeXArgument arg2)
            : base(commandName)
        {
            Argument1 = arg1 ?? LaTeXArgument.Create();
            Argument2 = arg2 ?? LaTeXArgument.Create();
        }

        protected LaTeXCommandBlockTwoArgs(string commandName, string closingName, LaTeXArgument arg1, LaTeXArgument arg2)
            : base(commandName, closingName)
        {
            Argument1 = arg1 ?? LaTeXArgument.Create();
            Argument2 = arg2 ?? LaTeXArgument.Create();
        }


        public override void ToText(LinearTextComposer composer)
        {
            composer
                .AppendAtNewLine(@"\")
                .Append(CommandName);

            Argument1.ToText(composer);
            Argument2.ToText(composer);

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