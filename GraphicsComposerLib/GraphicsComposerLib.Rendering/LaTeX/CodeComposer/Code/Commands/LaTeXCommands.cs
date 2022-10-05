using GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Constants;

namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code.Commands
{
    public static class LaTeXCommands
    {
        public static LaTeXCommandOneArg Title()
        {
            return LaTeXCommandOneArg.Create(
                LaTeXCommandTagNames.Title
            );
        }

        public static LaTeXCommandOneArg Title(string argValue)
        {
            return LaTeXCommandOneArg.Create(
                LaTeXCommandTagNames.Title,
                argValue.ToLaTeXArgument()
            );
        }

        public static LaTeXCommandOneArg Title(ILaTeXCodeElement argValue)
        {
            return LaTeXCommandOneArg.Create(
                LaTeXCommandTagNames.Title,
                argValue.ToLaTeXArgument()
            );
        }


        public static LaTeXCommandOneArg Author()
        {
            return LaTeXCommandOneArg.Create(
                LaTeXCommandTagNames.Author
            );
        }

        public static LaTeXCommandOneArg Author(string argValue)
        {
            return LaTeXCommandOneArg.Create(
                LaTeXCommandTagNames.Author,
                argValue.ToLaTeXArgument()
            );
        }

        public static LaTeXCommandOneArg Author(ILaTeXCodeElement argValue)
        {
            return LaTeXCommandOneArg.Create(
                LaTeXCommandTagNames.Author,
                argValue.ToLaTeXArgument()
            );
        }


        public static LaTeXCommandOneArg Date()
        {
            return LaTeXCommandOneArg.Create(
                LaTeXCommandTagNames.Date
            );
        }

        public static LaTeXCommandOneArg Date(string argValue)
        {
            return LaTeXCommandOneArg.Create(
                LaTeXCommandTagNames.Date, 
                argValue.ToLaTeXArgument()
            );
        }

        public static LaTeXCommandOneArg Date(ILaTeXCodeElement argValue)
        {
            return LaTeXCommandOneArg.Create(
                LaTeXCommandTagNames.Date, 
                argValue.ToLaTeXArgument()
            );
        }


        public static LaTeXCommandTwoArgs RenewCommand(string argValue1, string argValue2)
        {
            return LaTeXCommandTwoArgs.Create(
                LaTeXCommandTagNames.RenewCommand, 
                argValue1.ToLaTeXArgument(),
                argValue2.ToLaTeXArgument()
            );
        }

        public static LaTeXCommandTwoArgs RenewCommand(string argValue1, ILaTeXCodeElement argValue2)
        {
            return LaTeXCommandTwoArgs.Create(
                LaTeXCommandTagNames.RenewCommand,
                argValue1.ToLaTeXArgument(),
                argValue2.ToLaTeXArgument()
            );
        }


        public static LaTeXCommandTwoArgs SetCounter(string argValue1, string argValue2)
        {
            return LaTeXCommandTwoArgs.Create(
                LaTeXCommandTagNames.SetCounter,
                argValue1.ToLaTeXArgument(),
                argValue2.ToLaTeXArgument()
            );
        }

        public static LaTeXCommandTwoArgs SetCounter(string argValue1, int argValue2)
        {
            return LaTeXCommandTwoArgs.Create(
                LaTeXCommandTagNames.SetCounter,
                argValue1.ToLaTeXArgument(),
                argValue2.ToLaTeXArgument()
            );
        }


        public static LaTeXCommandOneArg UsePackage(string argValue)
        {
            return LaTeXCommandOneArg.Create(LaTeXCommandTagNames.UsePackage, argValue.ToLaTeXText());
        }

        public static LaTeXCommandOneArg LineSpread(string argValue)
        {
            return LaTeXCommandOneArg.Create(LaTeXCommandTagNames.LineSpread, argValue.ToLaTeXText());
        }

        public static LaTeXCommandNoArgs SingleSpacing()
        {
            return LaTeXCommandNoArgs.Create(LaTeXCommandTagNames.SingleSpacing);
        }

        public static LaTeXCommandNoArgs OneHalfSpacing()
        {
            return LaTeXCommandNoArgs.Create(LaTeXCommandTagNames.OneHalfSpacing);
        }

        public static LaTeXCommandNoArgs DoubleSpacing()
        {
            return LaTeXCommandNoArgs.Create(LaTeXCommandTagNames.DoubleSpacing);
        }

        public static LaTeXCommandOneArg SetStretch(double argValue)
        {
            return LaTeXCommandOneArg.Create(LaTeXCommandTagNames.SetStretch, argValue.ToLaTeXText());
        }


    }
}
