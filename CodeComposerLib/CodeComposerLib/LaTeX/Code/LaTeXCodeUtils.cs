using CodeComposerLib.LaTeX.Code.Arguments;
using CodeComposerLib.LaTeX.Code.Commands;

namespace CodeComposerLib.LaTeX.Code
{
    public static class LaTeXCodeUtils
    {
        public static bool IsNullOrEmpty(this ILaTeXCodeElement latexCode)
        {
            return ReferenceEquals(latexCode, null) || latexCode.IsEmpty();
        }

        public static LaTeXText ToLaTeXText(this string value)
        {
            return new LaTeXText() { Text = value };
        }

        public static LaTeXText ToLaTeXText(this int value)
        {
            return new LaTeXText() { Text = value.ToString() };
        }

        public static LaTeXText ToLaTeXText(this long value)
        {
            return new LaTeXText() { Text = value.ToString() };
        }

        public static LaTeXText ToLaTeXText(this double value)
        {
            return new LaTeXText() { Text = value.ToString("G") };
        }

        public static LaTeXText ToLaTeXText(this float value)
        {
            return new LaTeXText() { Text = value.ToString("G") };
        }


        public static LaTeXArgument ToLaTeXArgument(this ILaTeXCodeElement value)
        {
            return LaTeXArgument.Create(value);
        }

        public static LaTeXArgument ToLaTeXArgument(this string value)
        {
            return LaTeXArgument.Create(value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXArgument(this int value)
        {
            return LaTeXArgument.Create(value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXArgument(this long value)
        {
            return LaTeXArgument.Create(value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXArgument(this float value)
        {
            return LaTeXArgument.Create(value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXArgument(this double value)
        {
            return LaTeXArgument.Create(value.ToLaTeXText());
        }


        public static LaTeXArgument ToLaTeXArgument(this ILaTeXCodeElement value, bool isOptional)
        {
            return LaTeXArgument.Create(isOptional, value);
        }

        public static LaTeXArgument ToLaTeXArgument(this string value, bool isOptional)
        {
            return LaTeXArgument.Create(isOptional, value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXArgument(this int value, bool isOptional)
        {
            return LaTeXArgument.Create(isOptional, value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXArgument(this long value, bool isOptional)
        {
            return LaTeXArgument.Create(isOptional, value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXArgument(this float value, bool isOptional)
        {
            return LaTeXArgument.Create(isOptional, value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXArgument(this double value, bool isOptional)
        {
            return LaTeXArgument.Create(isOptional, value.ToLaTeXText());
        }


        public static LaTeXArgument ToLaTeXOptionalArgument(this ILaTeXCodeElement value)
        {
            return LaTeXArgument.Create(true, value);
        }

        public static LaTeXArgument ToLaTeXOptionalArgument(this string value)
        {
            return LaTeXArgument.Create(true, value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXOptionalArgument(this int value)
        {
            return LaTeXArgument.Create(true, value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXOptionalArgument(this long value)
        {
            return LaTeXArgument.Create(true, value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXOptionalArgument(this float value)
        {
            return LaTeXArgument.Create(true, value.ToLaTeXText());
        }

        public static LaTeXArgument ToLaTeXOptionalArgument(this double value)
        {
            return LaTeXArgument.Create(true, value.ToLaTeXText());
        }


        public static bool IsText(this ILaTeXCodeElement codeElement)
        {
            return !ReferenceEquals(codeElement as LaTeXText, null);
        }

        public static bool IsArgument(this ILaTeXCodeElement codeElement)
        {
            return !ReferenceEquals(codeElement as LaTeXArgument, null);
        }

        public static bool IsCodeSection(this ILaTeXCodeElement codeElement)
        {
            return !ReferenceEquals(codeElement as ILaTeXCodeSection, null);
        }


        public static bool IsCommand(this ILaTeXCodeElement codeElement)
        {
            return !ReferenceEquals(codeElement as ILaTeXCommand, null);
        }

        public static bool IsCommand(this ILaTeXCodeElement codeElement, string commandName)
        {
            var command = codeElement as ILaTeXCommand;

            return !ReferenceEquals(command, null) && command.CommandName == commandName;
        }

        public static bool IsCommandNoArgs(this ILaTeXCodeElement codeElement)
        {
            return !ReferenceEquals(codeElement as LaTeXCommandNoArgs, null);
        }

        public static bool IsCommandNoArgs(this ILaTeXCodeElement codeElement, string commandName)
        {
            var command = codeElement as LaTeXCommandNoArgs;

            return !ReferenceEquals(command, null) && command.CommandName == commandName;
        }

        public static bool IsCommandOneArg(this ILaTeXCodeElement codeElement)
        {
            return !ReferenceEquals(codeElement as LaTeXCommandOneArg, null);
        }

        public static bool IsCommandOneArg(this ILaTeXCodeElement codeElement, string commandName)
        {
            var command = codeElement as LaTeXCommandOneArg;

            return !ReferenceEquals(command, null) && command.CommandName == commandName;
        }

        public static bool IsCommandTwoArgs(this ILaTeXCodeElement codeElement)
        {
            return !ReferenceEquals(codeElement as LaTeXCommandTwoArgs, null);
        }

        public static bool IsCommandTwoArgs(this ILaTeXCodeElement codeElement, string commandName)
        {
            var command = codeElement as LaTeXCommandTwoArgs;

            return !ReferenceEquals(command, null) && command.CommandName == commandName;
        }


        public static bool IsCommandBlock(this ILaTeXCodeElement codeElement)
        {
            return !ReferenceEquals(codeElement as ILaTeXCommandBlock, null);
        }

        public static bool IsCommandBlock(this ILaTeXCodeElement codeElement, string commandName)
        {
            var command = codeElement as ILaTeXCommandBlock;

            return !ReferenceEquals(command, null) && command.CommandName == commandName;
        }

        public static bool IsCommandBlockNoArgs(this ILaTeXCodeElement codeElement)
        {
            return !ReferenceEquals(codeElement as LaTeXCommandBlockNoArgs, null);
        }

        public static bool IsCommandBlockNoArgs(this ILaTeXCodeElement codeElement, string commandName)
        {
            var command = codeElement as LaTeXCommandBlockNoArgs;

            return !ReferenceEquals(command, null) && command.CommandName == commandName;
        }

        public static bool IsCommandBlockOneArg(this ILaTeXCodeElement codeElement)
        {
            return !ReferenceEquals(codeElement as LaTeXCommandBlockOneArg, null);
        }

        public static bool IsCommandBlockOneArg(this ILaTeXCodeElement codeElement, string commandName)
        {
            var command = codeElement as LaTeXCommandBlockOneArg;

            return !ReferenceEquals(command, null) && command.CommandName == commandName;
        }

        public static bool IsCommandBlockTwoArgs(this ILaTeXCodeElement codeElement)
        {
            return !ReferenceEquals(codeElement as LaTeXCommandBlockTwoArgs, null);
        }

        public static bool IsCommandBlockTwoArgs(this ILaTeXCodeElement codeElement, string commandName)
        {
            var command = codeElement as LaTeXCommandBlockTwoArgs;

            return !ReferenceEquals(command, null) && command.CommandName == commandName;
        }
    }
}
