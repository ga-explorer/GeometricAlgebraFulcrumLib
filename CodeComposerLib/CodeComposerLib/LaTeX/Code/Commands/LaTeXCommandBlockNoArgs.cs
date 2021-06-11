﻿using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.LaTeX.Code.Arguments;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.LaTeX.Code.Commands
{
    /// <summary>
    /// A LaTeX block command with child commands but no arguments
    /// </summary>
    public class LaTeXCommandBlockNoArgs : LaTeXCommandBlock
    {
        public static LaTeXCommandBlockNoArgs Create(string commandName)
        {
            return new LaTeXCommandBlockNoArgs(commandName);
        }

        public static LaTeXCommandBlockNoArgs Create(string commandName, string closingName)
        {
            return new LaTeXCommandBlockNoArgs(commandName, closingName);
        }


        public override IEnumerable<LaTeXArgument> Arguments 
            => Enumerable.Empty<LaTeXArgument>();


        protected LaTeXCommandBlockNoArgs(string commandName)
            : base(commandName)
        {
        }

        protected LaTeXCommandBlockNoArgs(string commandName, string closingName) 
            : base(commandName, closingName)
        {
        }


        public override void ToText(LinearTextComposer composer)
        {
            composer
                .AppendAtNewLine(@"\")
                .Append(CommandName);

            if (SubSectionsList.Count > 0)
            {
                composer.IncreaseIndentation();

                SubSectionsList.ToText(composer);

                composer.DecreaseIndentation();
            }

            if (!string.IsNullOrEmpty(ClosingName))
                composer
                    .AppendAtNewLine(@"\")
                    .Append(ClosingName);
        }
    }
}