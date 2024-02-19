using WebComposerLib.LaTeX.CodeComposer.Code.Commands;

namespace WebComposerLib.LaTeX.CodeComposer.Code;

/// <summary>
/// Represents one or more consequtive commands in a logical section of LaTeX code. The commands
/// can be organized into logical subsections in a heirarchical structure
/// </summary>
public interface ILaTeXCodeSection : ILaTeXCodeElement
{
    /// <summary>
    /// The logical LaTeX code sub-sections inside this code section
    /// </summary>
    IEnumerable<ILaTeXCodeSection> SubSections { get; }

    /// <summary>
    /// The actual LaTeX commands comprising this code section
    /// </summary>
    IEnumerable<ILaTeXCommand> SectionCommands { get; }
}