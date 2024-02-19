using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Attributes;

public sealed class SvgAttributeInfo
{
    public static int AttributesCount { get; private set; }


    public int Id { get; }

    public string Name { get; }

    public bool IsCssAttribute { get; }

    public bool IsXmlAttribute 
        => !IsCssAttribute;

    public SvgValueAttributeType AttributeType
        => IsCssAttribute
            ? SvgValueAttributeType.Css
            : SvgValueAttributeType.Xml;

    internal SvgAttributeInfo(string name, bool isCssAttribute)
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