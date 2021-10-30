using GraphicsComposerLib.GraphViz.Dot.Label.Text;

namespace GraphicsComposerLib.GraphViz.Dot.Label
{
    /// <summary>
    /// This class represents a formatting tag like bold, italic, etc.
    /// </summary>
    public interface IDotHtmlTagFormatting : IDotHtmlTag
    {
        /// <summary>
        /// Apply this tag to the given HTML text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string FormatText(DotHtmlText text);
    }
}
