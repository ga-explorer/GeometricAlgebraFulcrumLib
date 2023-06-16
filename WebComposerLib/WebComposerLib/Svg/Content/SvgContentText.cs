using TextComposerLib;

namespace WebComposerLib.Svg.Content
{
    public class SvgContentText : ISvgContent
    {
        public static SvgContentText Create(string text)
        {
            return new SvgContentText(text);
        }


        public bool IsContentText => true;

        public bool IsContentComment => false;

        public bool IsContentElement => false;

        private string _value;
        public string Value
        {
            get => _value;
            set => _value = value?.ToHtmlSafeString() ?? string.Empty;
        }


        private SvgContentText(string value)
        {
            _value = value?.ToHtmlSafeString() ?? string.Empty;
        }


        public override string ToString()
        {
            return _value;
        }
    }
}