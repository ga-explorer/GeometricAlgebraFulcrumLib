namespace WebComposerLib.LaTeX.CodeComposer.Code.Commands;

/// <summary>
/// This interface represents a single LaTeX command block like the \begin ... \end LaTeX command.
/// </summary>
public interface ILaTeXCommandBlock : ILaTeXCommand
{
    /// <summary>
    /// The closing name of this command block; for example: end (to close a \begin command)
    /// </summary>
    string ClosingName { get; }

    /// <summary>
    /// The inner LaTeX commands inside this command block
    /// </summary>
    IEnumerable<ILaTeXCommand> InnerCommands { get; }
}