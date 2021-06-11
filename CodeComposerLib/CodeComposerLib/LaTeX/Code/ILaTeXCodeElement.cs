﻿using TextComposerLib.Text.Linear;

namespace CodeComposerLib.LaTeX.Code
{
    /// <summary>
    /// Represents a LaTeX code element, like logical code sections, LaTeX commands, text, and command
    /// arguments.
    /// </summary>
    public interface ILaTeXCodeElement
    {
        bool IsEmpty();

        void ToText(LinearTextComposer composer);
    }
}