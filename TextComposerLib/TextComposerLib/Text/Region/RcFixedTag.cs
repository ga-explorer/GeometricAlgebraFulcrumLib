using System;
using System.Collections.Generic;
using System.Linq;

namespace TextComposerLib.Text.Region
{
    /// <summary>
    /// This class is a fixed text tag inside a slot text region
    /// </summary>
    public sealed class RcFixedTag : IRcTag
    {
        /// <summary>
        /// The fixed text lines of this tag
        /// </summary>
        private readonly List<string> _fixedLinesList;

        private readonly RcTemplateMarkers _markers;


        /// <summary>
        /// The text line marking the baginning of this tag
        /// </summary>
        public string BeginTagLine => FixedTagMarker.MarkerText;

        public IEnumerable<string> TextLines
        {
            get
            {
                yield return BeginTagLine;

                foreach (var textLine in _fixedLinesList)
                    yield return textLine;
            }
        }

        public IEnumerable<string> TemplateTextLines => TextLines;

        public IEnumerable<string> GeneratedTextLines => _fixedLinesList;

        public bool IsFixed => true;

        public bool IsSlot => false;

        /// <summary>
        /// The marker of a fixed tag inside a slot region
        /// </summary>
        public RcLineMarker FixedTagMarker => _markers.FixedTagMarker;


        internal RcFixedTag(RcTemplateMarkers markers)
        {
            _fixedLinesList = new List<string>();
            _markers = markers;
        }


        /// <summary>
        /// Create an exact copy of this tag
        /// </summary>
        /// <returns></returns>
        internal RcFixedTag CreateCopy(RcTemplateMarkers newMarkers)
        {
            var newTag = new RcFixedTag(newMarkers);

            newTag._fixedLinesList.AddRange(_fixedLinesList);

            return newTag;
        }

        /// <summary>
        /// Clear all text of this tag
        /// </summary>
        /// <returns></returns>
        public RcFixedTag ClearText()
        {
            _fixedLinesList.Clear();
            return this;
        }

        /// <summary>
        /// Add text to this tag
        /// </summary>
        /// <param name="text"></param>
        /// <param name="linePrefix"></param>
        /// <returns></returns>
        public RcFixedTag AddText(string text, string linePrefix = null)
        {
            if (string.IsNullOrEmpty(text)) return this;

            var textLines = text.SplitLines();

            _fixedLinesList.AddRange(
                string.IsNullOrEmpty(linePrefix)
                ? textLines
                : textLines.Select(t => linePrefix + t)
                );

            return this;
        }

        /// <summary>
        /// Add text lines to this tag
        /// </summary>
        /// <param name="textLines"></param>
        /// <returns></returns>
        public RcFixedTag AddTextLines(IEnumerable<string> textLines)
        {
            _fixedLinesList.AddRange(textLines);

            return this;
        }

        /// <summary>
        /// Add text lines to this tag
        /// </summary>
        /// <param name="textLines"></param>
        /// <returns></returns>
        public RcFixedTag AddTextLines(params string[] textLines)
        {
            _fixedLinesList.AddRange(textLines);

            return this;
        }

        /// <summary>
        /// Set text of this tag
        /// </summary>
        /// <param name="text"></param>
        /// <param name="linePrefix"></param>
        /// <returns></returns>
        public RcFixedTag SetText(string text, string linePrefix = null)
        {
            ClearText();
            return AddText(text, linePrefix);
        }

        public override string ToString()
        {
            return TextLines.Concatenate(Environment.NewLine);
        }
    }
}
