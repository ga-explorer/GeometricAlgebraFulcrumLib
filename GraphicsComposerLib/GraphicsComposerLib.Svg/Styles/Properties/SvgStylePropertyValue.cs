using System.Text;
using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Svg.Styles.Properties
{
    /// <summary>
    /// This class holds information about the value of a style property for a parent style
    /// </summary>
    public abstract class SvgStylePropertyValue : ISvgAttributeValue
    {
        public SvgStyle ParentStyle { get; }

        protected abstract string ValueComputedText { get; }

        private string _valueStoredText = string.Empty;
        protected string ValueStoredText
        {
            get { return _valueStoredText; }
            set
            {
                _valueStoredText = value ?? string.Empty;
                IsValueComputed = false;
            }
        }

        public bool IsValueComputed { get; protected set; }

        public bool IsValueStored => !IsValueComputed;

        public bool IsValueEmpty => string.IsNullOrEmpty(ValueText);

        public string ValueText
        {
            get
            {
                if (IsValueStored)
                    return ValueStoredText;

                ValueStoredText = ValueComputedText;
                IsValueComputed = false;

                return ValueStoredText;
            }
        }

        public SvgAttributeInfo AttributeInfo { get; }

        public int AttributeId => AttributeInfo.Id;

        public string AttributeName => AttributeInfo.Name;


        protected SvgStylePropertyValue(SvgStyle parentStyle, SvgAttributeInfo attributeInfo)
        {
            ParentStyle = parentStyle;
            AttributeInfo = attributeInfo;
        }


        public abstract SvgStylePropertyValue CreateCopy();

        public abstract SvgStylePropertyValue UpdateFrom(SvgStylePropertyValue sourcePropertyValue);

        public SvgStyle SetToText(string value)
        {
            ValueStoredText = value;

            return ParentStyle;
        }

        public SvgStyle SetToText(ISvgValue value)
        {
            ValueStoredText = value.ValueText;

            return ParentStyle;
        }

        public SvgStyle SetToInherit()
        {
            ValueStoredText = "inherit";

            return ParentStyle;
        }

        public SvgStyle SetToEmptyDefault()
        {
            ValueStoredText = string.Empty;

            return ParentStyle;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(AttributeInfo.Name)
                .Append(": ")
                .Append(ValueText.ToHtmlSafeString())
                .Append(";")
                .ToString();
        }
    }
}