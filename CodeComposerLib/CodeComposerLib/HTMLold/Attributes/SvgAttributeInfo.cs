using CodeComposerLib.HTMLold.Values;

namespace CodeComposerLib.HTMLold.Attributes
{
    public sealed class HtmlAttributeInfo
    {
        public static int AttributesCount { get; private set; }


        public int Id { get; }

        public string Name { get; }

        public bool IsCssAttribute { get; }

        public bool IsXmlAttribute 
            => !IsCssAttribute;

        public HtmlValueAttributeType AttributeType
            => IsCssAttribute
                ? HtmlValueAttributeType.Css
                : HtmlValueAttributeType.Xml;

        internal HtmlAttributeInfo(string name, bool isCssAttribute)
        {
            Id = AttributesCount++;
            Name = name;
            IsCssAttribute = isCssAttribute;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}