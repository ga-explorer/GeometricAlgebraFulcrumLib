using System;
using System.Collections.Generic;
using System.Linq;

namespace TextComposerLib.Text.Region
{
    /// <summary>
    /// This class is a slot text tag inside a slot text region
    /// </summary>
    public sealed class RcSlotTag : IRcTag
    {
        /// <summary>
        /// The generated text lines of this tag
        /// </summary>
        private readonly List<string> _generatedLinesList;

        /// <summary>
        /// The text lines marking the baginning of this tag
        /// </summary>
        private readonly List<string> _tagStringLinesList;

        private readonly RcTemplateMarkers _markers;


        /// <summary>
        /// A fixed whitespace to be prefixed to each line in the generated text
        /// </summary>
        public string LinePrefix { get; internal set; }

        /// <summary>
        /// A tag text associated with this slot tag
        /// </summary>
        public string TagString
        {
            get { return _tagStringLinesList.Concatenate(Environment.NewLine); }
            set
            {
                _tagStringLinesList.Clear();

                if (string.IsNullOrEmpty(value)) return;

                _tagStringLinesList.AddRange(value.SplitLines());
            }
        }

        /// <summary>
        /// The marker of a slot tag inside a slot region
        /// </summary>
        public RcLineMarker SlotTagMarker => _markers.SlotTagMarker;

        /// <summary>
        /// The marker of begin joining several slot tags into a single multi-line slot tag 
        /// inside a slot region
        /// </summary>
        public RcLineMarker JoinSlotTagsBeginMarker => _markers.JoinSlotTagsBeginMarker;

        /// <summary>
        /// The marker of end joining several slot tags into a single multi-line slot tag 
        /// inside a slot region
        /// </summary>
        public RcLineMarker JoinSlotTagsEndMarker => _markers.JoinSlotTagsEndMarker;

        /// <summary>
        /// True if the TagString spans multiple lines
        /// </summary>
        public bool IsMultiLine => _tagStringLinesList.Count > 1;

        /// <summary>
        /// True if the TagString spans one line only
        /// </summary>
        public bool IsSingleLine => _tagStringLinesList.Count <= 1;

        public IEnumerable<string> TextLines
        {
            get
            {
                if (IsMultiLine)
                    yield return JoinSlotTagsBeginMarker.MarkerText;

                foreach (var textLine in _tagStringLinesList)
                    yield return SlotTagMarker.MarkerText + " " + textLine;

                foreach (var textLine in _generatedLinesList)
                    yield return textLine;

                if (IsMultiLine)
                    yield return JoinSlotTagsEndMarker.MarkerText;
            }
        }

        public IEnumerable<string> TemplateTextLines
        {
            get
            {
                if (IsMultiLine)
                    yield return JoinSlotTagsBeginMarker.MarkerText;

                foreach (var textLine in _tagStringLinesList)
                    yield return SlotTagMarker.MarkerText + textLine;

                if (IsMultiLine)
                    yield return JoinSlotTagsEndMarker.MarkerText;
            }
        }

        public IEnumerable<string> GeneratedTextLines => _generatedLinesList;

        public bool IsFixed => false;

        public bool IsSlot => true;


        internal RcSlotTag(RcTemplateMarkers markers)
        {
            _tagStringLinesList = new List<string>();
            _generatedLinesList = new List<string>();
            _markers = markers;
        }


        /// <summary>
        /// Create and Exact copy of this tag
        /// </summary>
        /// <param name="newMarkers"></param>
        /// <returns></returns>
        internal RcSlotTag CreateCopy(RcTemplateMarkers newMarkers)
        {
            var newTag = new RcSlotTag(newMarkers);

            newTag._tagStringLinesList.AddRange(_tagStringLinesList);
            newTag._generatedLinesList.AddRange(_generatedLinesList);

            return newTag;
        }

        /// <summary>
        /// Clear the generated text of this tag
        /// </summary>
        /// <returns></returns>
        public RcSlotTag ClearGeneratedText()
        {
            _generatedLinesList.Clear();

            return this;
        }

        /// <summary>
        /// Add generated text to this tag
        /// </summary>
        /// <param name="tsxt"></param>
        /// <returns></returns>
        public RcSlotTag AddGeneratedText(string tsxt)
        {
            if (string.IsNullOrEmpty(tsxt)) return this;

            var textLines = tsxt.SplitLines();

            _generatedLinesList.AddRange(
                string.IsNullOrEmpty(LinePrefix)
                ? textLines
                : textLines.Select(t => LinePrefix + t)
                );

            return this;
        }

        /// <summary>
        /// Add generated text lines of to this tag
        /// </summary>
        /// <param name="textLines"></param>
        /// <returns></returns>
        public RcSlotTag AddGeneratedTextLines(IEnumerable<string> textLines)
        {
            _generatedLinesList.AddRange(
                string.IsNullOrEmpty(LinePrefix)
                ? textLines
                : textLines.Select(t => LinePrefix + t)
                );

            return this;
        }

        /// <summary>
        /// Add generated text lines of to this tag 
        /// </summary>
        /// <param name="textLines"></param>
        /// <returns></returns>
        public RcSlotTag AddGeneratedTextLines(params string[] textLines)
        {
            _generatedLinesList.AddRange(
                string.IsNullOrEmpty(LinePrefix)
                ? textLines
                : textLines.Select(t => LinePrefix + t)
                );

            return this;
        }

        /// <summary>
        /// Set the generated text of this slot tag
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public RcSlotTag SetGeneratedText(string text)
        {
            ClearGeneratedText();
            return AddGeneratedText(text);
        }

        /// <summary>
        /// Set the generated text lines of this slot tag
        /// </summary>
        /// <param name="textLines"></param>
        /// <returns></returns>
        public RcSlotTag SetGeneratedTextLines(IEnumerable<string> textLines)
        {
            ClearGeneratedText();
            return AddGeneratedTextLines(textLines);
        }

        /// <summary>
        /// Set the generated text lines of this slot tag
        /// </summary>
        /// <param name="textLines"></param>
        /// <returns></returns>
        public RcSlotTag SetGeneratedTextLines(params string[] textLines)
        {
            ClearGeneratedText();
            return AddGeneratedTextLines(textLines);
        }

        public override string ToString()
        {
            return TextLines.Concatenate(Environment.NewLine);
        }
    }
}
