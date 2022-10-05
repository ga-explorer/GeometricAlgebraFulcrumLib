using GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code.Arguments;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code.Commands
{
    /// <summary>
    /// A LaTeX block command with child commands and two arguments
    /// </summary>
    public class LaTeXCommandTwoArgs : LaTeXCommand
    {
        public static LaTeXCommandTwoArgs Create(string commandName)
        {
            return new LaTeXCommandTwoArgs(commandName);
        }

        public static LaTeXCommandTwoArgs Create(string commandName, LaTeXArgument arg1, LaTeXArgument arg2)
        {
            return new LaTeXCommandTwoArgs(commandName, arg1, arg2);
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


        protected LaTeXCommandTwoArgs(string commandName)
            : base(commandName)
        {
            Argument1 = LaTeXArgument.Create();
            Argument2 = LaTeXArgument.Create();
        }

        protected LaTeXCommandTwoArgs(string commandName, LaTeXArgument arg1, LaTeXArgument arg2)
            : base(commandName)
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
        }
    }
}
