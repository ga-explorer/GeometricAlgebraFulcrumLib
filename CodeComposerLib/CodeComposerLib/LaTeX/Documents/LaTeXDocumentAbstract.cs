﻿using CodeComposerLib.LaTeX.Code;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.LaTeX.Documents
{
    public sealed class LaTeXDocumentAbstract : LaTeXEnvironment
    {
        public ILaTeXCodeElement AbstractCode { get; set; }

        public string AbstractTitle { get; set; } = string.Empty;

        public void ToText(LinearTextComposer composer)
        {
            if (!string.IsNullOrEmpty(AbstractTitle))
                composer
                    .AppendAtNewLine(@"\renewcommand{\abstractname}{")
                    .Append(AbstractTitle)
                    .AppendLine("}");

            composer
                .AppendLineAtNewLine(@"\begin{abstract}")
                .IncreaseIndentation();

            AbstractCode?.ToText(composer);

            composer
                .DecreaseIndentation()
                .AppendLineAtNewLine(@"\end{abstract}");
        }
    }
}