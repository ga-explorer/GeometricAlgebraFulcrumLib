﻿using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.LaTeX.Code.Arguments;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.LaTeX.Code.Commands
{
    /// <summary>
    /// This abstract class represents a single LaTeX command with no child commands,
    /// but may contain arguments
    /// </summary>
    public abstract class LaTeXCommand : ILaTeXCommand
    {
        public string CommandName { get; }

        public abstract IEnumerable<LaTeXArgument> Arguments { get; }

        public virtual IEnumerable<ILaTeXCodeSection> SubSections
            => Enumerable.Empty<ILaTeXCodeSection>();

        public IEnumerable<ILaTeXCommand> SectionCommands
        {
            get { yield return this; }
        }


        protected LaTeXCommand(string commandName)
        {
            CommandName = commandName;
        }


        public bool IsEmpty()
        {
            return false;
        }

        public abstract void ToText(LinearTextComposer composer);

        public override string ToString()
        {
            var composer = new LinearTextComposer();

            ToText(composer);

            return composer.ToString();
        }
    }
}