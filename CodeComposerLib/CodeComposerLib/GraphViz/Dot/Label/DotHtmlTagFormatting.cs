using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.GraphViz.Dot.Label.Text;

namespace CodeComposerLib.GraphViz.Dot.Label
{
    /// <summary>
    /// This class represents an HTML formatting tag in the dot language like bold, italic, etc.
    /// See http://www.graphviz.org/content/node-shapes#html for more details
    /// </summary>
    public sealed class DotHtmlTagFormatting : DotHtmlTag, IDotHtmlTagFormatting
    {
        public override IEnumerable<KeyValuePair<string, string>> Attributes => Enumerable.Empty<KeyValuePair<string, string>>();


        internal DotHtmlTagFormatting(string name)
            : base(name)
        {
        }


        public string FormatText(DotHtmlText text)
        {
            var s = new StringBuilder();

            s.Append("<")
                .Append(TagName)
                .Append(">");

            s.Append(ReferenceEquals(text, null) ? "" : text.ToString());

            return s
                .Append("</")
                .Append(TagName)
                .Append(">")
                .ToString();
        }

        public override string ToString()
        {
            return FormatText(null);
        }
    }
}
