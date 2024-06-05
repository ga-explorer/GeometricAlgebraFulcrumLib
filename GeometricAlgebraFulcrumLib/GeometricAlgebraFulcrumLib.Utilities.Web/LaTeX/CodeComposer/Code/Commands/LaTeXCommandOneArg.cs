using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code.Arguments;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code.Commands;

/// <summary>
/// A LaTeX command with no child commands and a single argument
/// </summary>
public class LaTeXCommandOneArg : LaTeXCommand
{
    public static LaTeXCommandOneArg Create(string commandName)
    {
        return new LaTeXCommandOneArg(commandName);
    }

    public static LaTeXCommandOneArg Create(string commandName, ILaTeXCodeElement argValue)
    {
        return new LaTeXCommandOneArg(commandName, false, argValue);
    }

    public static LaTeXCommandOneArg Create(string commandName, LaTeXArgument arg)
    {
        return new LaTeXCommandOneArg(commandName, arg);
    }

    public static LaTeXCommandOneArg Create(string commandName, bool isOptional)
    {
        return new LaTeXCommandOneArg(commandName, isOptional);
    }

    public static LaTeXCommandOneArg Create(string commandName, bool isOptional, ILaTeXCodeElement argValue)
    {
        return new LaTeXCommandOneArg(commandName, isOptional, argValue);
    }


    public LaTeXArgument Argument { get; }

    public override IEnumerable<LaTeXArgument> Arguments
    {
        get { yield return Argument; }
    }
        


    protected LaTeXCommandOneArg(string commandName) 
        : base(commandName)
    {
        Argument = LaTeXArgument.Create();
    }

    protected LaTeXCommandOneArg(string commandName, LaTeXArgument arg)
        : base(commandName)
    {
        Argument = arg ?? LaTeXArgument.Create();
    }

    protected LaTeXCommandOneArg(string commandName, bool isOptional) 
        : base(commandName)
    {
        Argument = LaTeXArgument.Create(isOptional);
    }

    protected LaTeXCommandOneArg(string commandName, ILaTeXCodeElement argValue) 
        : base(commandName)
    {
        Argument = LaTeXArgument.Create(argValue);
    }

    protected LaTeXCommandOneArg(string commandName, bool isOptional, ILaTeXCodeElement argValue) 
        : base(commandName)
    {
        Argument = LaTeXArgument.Create(isOptional, argValue);
    }


    public override void ToText(LinearTextComposer composer)
    {
        composer
            .AppendAtNewLine(@"\")
            .Append(CommandName);

        Argument.ToText(composer);
    }
}