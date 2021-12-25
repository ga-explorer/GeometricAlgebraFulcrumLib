using System.Collections.Generic;
using CodeComposerLib.GraphViz.Dot.Value;
using TextComposerLib;

namespace CodeComposerLib.GraphViz.Dot.Label.Table
{
    /// <summary>
    /// This class represents the cell image inside an HTML table tag in the dot language
    /// See http://www.graphviz.org/content/node-shapes#html for more details
    /// </summary>
    public sealed class DotHtmlCellImage : DotHtmlTag, IDotHtmlCellContents
    {
        /// <summary>
        /// The source file path of the image
        /// </summary>
        public string ImageSource { get; private set; }

        /// <summary>
        /// The image scale
        /// </summary>
        public string ImageScale { get; private set; }


        public override IEnumerable<KeyValuePair<string, string>> Attributes
        {
            get
            {
                if (string.IsNullOrEmpty(ImageScale) == false)
                    yield return new KeyValuePair<string, string>("SCALE", ImageScale);

                if (string.IsNullOrEmpty(ImageSource) == false)
                    yield return new KeyValuePair<string, string>("SRC", ImageSource);
            }
        }


        internal DotHtmlCellImage()
            : base("IMG")
        {
            ImageSource = string.Empty;
            ImageScale = string.Empty;
        }


        public DotHtmlCellImage ClearAttributes()
        {
            ImageSource = string.Empty;
            ImageScale = string.Empty;

            return this;
        }

        public DotHtmlCellImage ClearImageSource()
        {
            ImageSource = string.Empty;

            return this;
        }

        public DotHtmlCellImage ClearImageScale()
        {
            ImageScale = string.Empty;

            return this;
        }


        public DotHtmlCellImage SetImageSource(string filePath)
        {
            ImageSource = filePath.ValueToQuotedLiteral();

            return this;
        }

        public DotHtmlCellImage SetImageScale(bool flag)
        {
            ImageScale = (flag ? "TRUE" : "FALSE").DoubleQuote();

            return this;
        }

        public DotHtmlCellImage SetImageScale(DotNodeImageScale value)
        {
            ImageScale = value.QuotedValue.ToUpper();

            return this;
        }

        public DotHtmlCellImage SetImageScaleFalse()
        {
            ImageScale = "\"FALSE\"";

            return this;
        }

        public DotHtmlCellImage SetImageScaleTrue()
        {
            ImageScale = "\"TRUE\"";

            return this;
        }

        public DotHtmlCellImage SetImageScaleWidth()
        {
            ImageScale = "\"WIDTH\"";

            return this;
        }

        public DotHtmlCellImage SetImageScaleHeight()
        {
            ImageScale = "\"HEIGHT\"";

            return this;
        }

        public DotHtmlCellImage SetImageScaleBoth()
        {
            ImageScale = "\"BOTH\"";

            return this;
        }


        public override string ToString()
        {
            return TagNoContentsText;
        }
    }
}
