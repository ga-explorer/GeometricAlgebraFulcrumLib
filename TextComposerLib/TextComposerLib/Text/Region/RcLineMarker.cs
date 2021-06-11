using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextComposerLib.Text.Region
{
    /// <summary>
    /// This class represents a starting marker for a text line
    /// </summary>
    public sealed class RcLineMarker
    {
        private string _markerText;

        private Regex _markerRegex;


        public string MarkerName { get; private set; }

        /// <summary>
        /// Get or set the marker parts that should be separated by spaces
        /// </summary>
        public string MarkerText
        {
            get
            {
                return _markerText;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Reset();
                    return;
                }

                var markerTextParts = 
                    value
                    .ToLower()
                    .Trim()
                    .Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

                if (markerTextParts.Length <= 0)
                {
                    Reset();
                    return;
                }

                _markerText = markerTextParts.Concatenate(" ");

                _markerRegex = new Regex(
                    markerTextParts.Select(Regex.Escape).Concatenate(@"\s*", @"^\s*", @"\s?")
                    );
            }
        }

        /// <summary>
        /// The Regex object of this marker
        /// </summary>
        internal Regex MarkerRegex => _markerRegex;

        /// <summary>
        /// True if this marker contains useful information
        /// </summary>
        public bool IsReady => _markerRegex != null;


        internal RcLineMarker(string markerName)
        {
            MarkerName = markerName;
            Reset();
        }


        /// <summary>
        /// Clear marker text
        /// </summary>
        public void Reset()
        {
            _markerText = string.Empty;
            _markerRegex = null;
        }

        public override string ToString()
        {
            return _markerText;
        }
    }
}
