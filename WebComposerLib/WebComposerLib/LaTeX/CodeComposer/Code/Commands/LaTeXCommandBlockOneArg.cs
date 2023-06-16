using TextComposerLib.Text.Linear;
using WebComposerLib.LaTeX.CodeComposer.Code.Arguments;

namespace WebComposerLib.LaTeX.CodeComposer.Code.Commands
{
    public class LaTeXCommandBlockOneArg : LaTeXCommandBlock
    {
        public static LaTeXCommandBlockOneArg Create(string commandName)
        {
            return new LaTeXCommandBlockOneArg(commandName);
        }

        public static LaTeXCommandBlockOneArg Create(string commandName, ILaTeXCodeElement argValue)
        {
            return new LaTeXCommandBlockOneArg(commandName, false, argValue);
        }

        public static LaTeXCommandBlockOneArg Create(string commandName, LaTeXArgument arg)
        {
            return new LaTeXCommandBlockOneArg(commandName, arg);
        }

        public static LaTeXCommandBlockOneArg Create(string commandName, bool isOptional)
        {
            return new LaTeXCommandBlockOneArg(commandName, isOptional);
        }

        public static LaTeXCommandBlockOneArg Create(string commandName, bool isOptional, ILaTeXCodeElement argValue)
        {
            return new LaTeXCommandBlockOneArg(commandName, isOptional, argValue);
        }

        public static LaTeXCommandBlockOneArg Create(string commandName, string closingName, ILaTeXCodeElement argValue)
        {
            return new LaTeXCommandBlockOneArg(commandName, closingName, false, argValue);
        }

        public static LaTeXCommandBlockOneArg Create(string commandName, string closingName, LaTeXArgument arg)
        {
            return new LaTeXCommandBlockOneArg(commandName, closingName, arg);
        }

        public static LaTeXCommandBlockOneArg Create(string commandName, string closingName, bool isOptional)
        {
            return new LaTeXCommandBlockOneArg(commandName, closingName, isOptional);
        }

        public static LaTeXCommandBlockOneArg Create(string commandName, string closingName, bool isOptional, ILaTeXCodeElement argValue)
        {
            return new LaTeXCommandBlockOneArg(commandName, closingName, isOptional, argValue);
        }


        public LaTeXArgument Argument { get; }

        public override IEnumerable<LaTeXArgument> Arguments
        {
            get { yield return Argument; }
        }


        protected LaTeXCommandBlockOneArg(string commandName)
            : base(commandName)
        {
            Argument = LaTeXArgument.Create();
        }

        protected LaTeXCommandBlockOneArg(string commandName, string closingName)
            : base(commandName, closingName)
        {
            Argument = LaTeXArgument.Create();
        }

        protected LaTeXCommandBlockOneArg(string commandName, bool isOptional)
            : base(commandName)
        {
            Argument = LaTeXArgument.Create(isOptional);
        }

        protected LaTeXCommandBlockOneArg(string commandName, string closingName, bool isOptional)
            : base(commandName, closingName)
        {
            Argument = LaTeXArgument.Create(isOptional);
        }

        protected LaTeXCommandBlockOneArg(string commandName, ILaTeXCodeElement argValue)
            : base(commandName)
        {
            Argument = LaTeXArgument.Create(argValue);
        }

        protected LaTeXCommandBlockOneArg(string commandName, string closingName, ILaTeXCodeElement argValue)
            : base(commandName, closingName)
        {
            Argument = LaTeXArgument.Create(argValue);
        }

        protected LaTeXCommandBlockOneArg(string commandName, LaTeXArgument arg)
            : base(commandName)
        {
            Argument = arg ?? LaTeXArgument.Create();
        }

        protected LaTeXCommandBlockOneArg(string commandName, string closingName, LaTeXArgument arg)
            : base(commandName, closingName)
        {
            Argument = arg ?? LaTeXArgument.Create();
        }

        protected LaTeXCommandBlockOneArg(string commandName, bool isOptional, ILaTeXCodeElement argValue)
            : base(commandName)
        {
            Argument = LaTeXArgument.Create(isOptional, argValue);
        }

        protected LaTeXCommandBlockOneArg(string commandName, string closingName, bool isOptional, ILaTeXCodeElement argValue)
            : base(commandName, closingName)
        {
            Argument = LaTeXArgument.Create(isOptional, argValue);
        }


        public override void ToText(LinearTextComposer composer)
        {
            composer
                .AppendAtNewLine(@"\")
                .Append(CommandName);

            Argument.ToText(composer);

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