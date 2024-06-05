namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.SourceCode;

/// <summary>
/// This interface represents a single line of text within a code unit (file)
/// </summary>
public interface ISourceCodeUnitLine
{
    /// <summary>
    /// The parent code unit of this code line
    /// </summary>
    ISourceCodeUnit ParentCodeUnit { get; }

    /// <summary>
    /// The line number of this code line within the parent code unit
    /// </summary>
    int LineNumber { get; }

    /// <summary>
    /// The relative character position of the first character in this line relative to 
    /// the unit's source code text
    /// </summary>
    int FirstCharacterPosition { get; }

    /// <summary>
    /// The relative character position of the last character in this line relative to 
    /// the unit's source code text
    /// </summary>
    int LastCharacterPosition { get; }

    /// <summary>
    /// The number of characters in this code line
    /// </summary>
    int CharactersCount { get; }

    /// <summary>
    /// The text of this code line
    /// </summary>
    string LineCodeText { get; }
}