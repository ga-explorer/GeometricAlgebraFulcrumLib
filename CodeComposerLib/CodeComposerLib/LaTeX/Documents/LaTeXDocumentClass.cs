using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.LaTeX.Code;
using CodeComposerLib.LaTeX.Code.Arguments;
using CodeComposerLib.LaTeX.Code.Commands;
using CodeComposerLib.LaTeX.Constants;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.LaTeX.Documents
{
    /// <summary>
    /// https://en.wikibooks.org/wiki/LaTeX/Document_Structure#Document_classes
    /// https://texblog.org/2013/02/13/latex-documentclass-options-illustrated/
    /// </summary>
    public sealed class LaTeXDocumentClass : LaTeXCommand
    {
        public static LaTeXDocumentClass Create(string className)
        {
            return new LaTeXDocumentClass(className);
        }

        public static LaTeXDocumentClass Create(string className, params string[] optionsTextList)
        {
            return new LaTeXDocumentClass(className).AddOptions(optionsTextList);
        }

        public static LaTeXDocumentClass Create(string className, IEnumerable<string> optionsTextList)
        {
            return new LaTeXDocumentClass(className).AddOptions(optionsTextList);
        }



        private readonly List<LaTeXArgument> _classOptionsList 
            = new List<LaTeXArgument>();


        public override IEnumerable<LaTeXArgument> Arguments
        {
            get
            {
                foreach (var arg in _classOptionsList)
                    yield return arg;

                yield return ClassName.ToLaTeXArgument();
            }
        }

        public string ClassName { get; }

        public bool HasOptions 
            => _classOptionsList.Count > 0;

        public IEnumerable<LaTeXArgument> ClassOptions 
            => _classOptionsList;


        private LaTeXDocumentClass(string className) 
            : base(LaTeXCommandTagNames.DocumentClass)
        {
            ClassName = className;
        }


        public LaTeXDocumentClass ClearOptions()
        {
            _classOptionsList.Clear();

            return this;
        }

        public LaTeXDocumentClass AddOption(string optionText)
        {
            _classOptionsList.Add(optionText.ToLaTeXArgument());

            return this;
        }

        public LaTeXDocumentClass AddOptions(params string[] optionsTextList)
        {
            _classOptionsList.AddRange(optionsTextList.Select(t => t.ToLaTeXArgument()));

            return this;
        }

        public LaTeXDocumentClass AddOptions(IEnumerable<string> optionsTextList)
        {
            _classOptionsList.AddRange(optionsTextList.Select(t => t.ToLaTeXArgument()));

            return this;
        }

        public LaTeXDocumentClass SetOptions(params string[] optionsTextList)
        {
            _classOptionsList.Clear();
            _classOptionsList.AddRange(optionsTextList.Select(t => t.ToLaTeXArgument()));

            return this;
        }

        public LaTeXDocumentClass SetOptions(IEnumerable<string> optionsTextList)
        {
            _classOptionsList.Clear();
            _classOptionsList.AddRange(optionsTextList.Select(t => t.ToLaTeXArgument()));

            return this;
        }

        public override void ToText(LinearTextComposer composer)
        {
            composer
                .AppendAtNewLine(@"\")
                .Append(CommandName);

            foreach (var arg in Arguments)
                arg.ToText(composer);
        }
    }
}