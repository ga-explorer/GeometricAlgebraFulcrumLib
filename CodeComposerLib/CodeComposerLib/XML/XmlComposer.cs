using System.Text;
using DataStructuresLib;
using TextComposerLib;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.XML;

//TODO: Review and complete this
public sealed class XmlComposer
{
    //public XmlElement element

    public SparseTable<string, string> AttributesTable { get; } =
        new SparseTable<string, string>(string.Empty, string.IsNullOrEmpty);


    public string ElementName { get; }

    public string Contents { get; set; }

    public string Attributes
    {
        get
        {
            if (AttributesTable.Count == 0)
                return string.Empty;

            var composer = new StringBuilder();

            foreach (var pair in AttributesTable)
            {
                composer
                    .Append(pair.Key)
                    .Append("=")
                    .Append(pair.Value.ToHtmlSafeLiteral())
                    .Append(" ");
            }

            return composer.ToString().Trim();
        }
    }

    public string XmlBeginEndTag
    {
        get
        {
            var composer = new LinearTextComposer();

            composer
                .Append("<")
                .Append(ElementName);

            var attrText = Attributes;
            if (!string.IsNullOrEmpty(attrText))
                composer.Append(" ").Append(attrText);

            return composer.Append("/>").ToString();
        }
    }

    public string XmlBeginTag
    {
        get
        {
            var composer = new StringBuilder();

            composer
                .Append("<")
                .Append(ElementName);

            var attrText = Attributes;
            if (!string.IsNullOrEmpty(attrText))
                composer.Append(" ").Append(attrText);

            return composer.Append(">").ToString();
        }
    }

    public string XmlEndTag
        => new StringBuilder()
            .Append("<")
            .Append(ElementName)
            .Append("/>")
            .ToString();

    public string XmlTag
    {
        get
        {
            var contents = Contents.Trim();

            if (string.IsNullOrEmpty(contents))
                return XmlBeginEndTag;

            return new LinearTextComposer()
                .AppendLine(XmlBeginTag)
                .AppendLine(Contents)
                .Append(XmlEndTag)
                .ToString();
        }
    }

    public override string ToString()
    {
        return XmlTag;
    }
}