using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code;

/// <summary>
/// Represents a LaTeX code element, like logical code sections, LaTeX commands, text, and command
/// arguments.
/// </summary>
public interface ILaTeXCodeElement
{
    bool IsEmpty();

    void ToText(LinearTextComposer composer);
}