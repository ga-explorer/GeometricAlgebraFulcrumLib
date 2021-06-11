using System.Collections.Generic;
using CodeComposerLib.LaTeX.Code.Arguments;

namespace CodeComposerLib.LaTeX.Code.Commands
{
    /// <summary>
    /// This interface represents a single LaTeX command.
    /// </summary>
    public interface ILaTeXCommand : ILaTeXCodeSection
    {
        /// <summary>
        /// The name of the command; for example: begin, documentclass, usepackage, maketitle, etc.
        /// </summary>
        string CommandName { get; }

        /// <summary>
        /// The arguments of this command
        /// </summary>
        IEnumerable<LaTeXArgument> Arguments { get; }
    }
}