using System.Collections.Generic;

namespace CodeComposerLib.LaTeX.Code.Arguments
{
    public interface ILaTeXArgumentsList : ILaTeXCodeElement, IEnumerable<LaTeXArgument>
    {
        int ArgumentsCount { get; }

        int RequiredArgumentsCount { get; }

        int OptionalArgumentsCount { get; }

        bool HasArguments { get; }

        bool HasRequiredArguments { get; }

        bool HasOptionalArguments { get; }

        IEnumerable<LaTeXArgument> Arguments { get; }

        IEnumerable<LaTeXArgument> RequiredArguments { get; }

        IEnumerable<LaTeXArgument> OptionalArguments { get; }

        IEnumerable<ILaTeXCodeElement> ArgumentValues { get; }

        IEnumerable<ILaTeXCodeElement> RequiredArgumentValues { get; }

        IEnumerable<ILaTeXCodeElement> OptionalArgumentValues { get; }

        LaTeXArgument this[int argIndex] { get; }

    }
}