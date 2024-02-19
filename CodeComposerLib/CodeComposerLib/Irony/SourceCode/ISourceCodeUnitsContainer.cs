using System.Collections.Generic;

namespace CodeComposerLib.Irony.SourceCode;

/// <summary>
/// This interface represents the highest level of source code container 
/// (similar to a project file or solution file in .NET)
/// </summary>
public interface ISourceCodeUnitsContainer
{
    /// <summary>
    /// True if this is a project (possibly) containing multiple file
    /// </summary>
    bool IsProject { get; }

    /// <summary>
    /// True if this is a single source code text string or similar (not a multiple file project)
    /// </summary>
    bool IsText { get; }

    /// <summary>
    /// The list of all source code units text to be parsed
    /// </summary>
    List<string> SourceCodeUnitsText { get; }

    /// <summary>
    /// A list of all source code units (files)
    /// </summary>
    IEnumerable<ISourceCodeUnit> SourceCodeUnits { get; }

    /// <summary>
    /// True if this container has source code
    /// </summary>
    bool ContainsSourceCodeUnits { get; }

    /// <summary>
    /// True if the source code text requires to be updated (for example to load files from disk)
    /// </summary>
    bool RequiresSourceCodeTextUpdate { get; }

    /// <summary>
    /// Returns the currently active code unit of this container (for example during compilation)
    /// </summary>
    ISourceCodeUnit ActiveCodeUnit { get; set; }

    ///// <summary>
    ///// Given an absolute character position within a given unit's source code text this method
    ///// translates it into a character location object within a source code unit (file)
    ///// </summary>
    ///// <param name="codeUnit"></param>
    ///// <param name="absolutePos"></param>
    ///// <returns></returns>
    //LanguageCodeLocation TranslateCharacterLocation(ISourceCodeUnit codeUnit, int absolutePos);

    /// <summary>
    /// If the full source code text requires an update operation (for example to load files from disk)
    /// this method performs such update of the SourceCodeUnitsText property
    /// </summary>
    /// <returns></returns>
    void UpdateSourceCodeUnitsText();
}