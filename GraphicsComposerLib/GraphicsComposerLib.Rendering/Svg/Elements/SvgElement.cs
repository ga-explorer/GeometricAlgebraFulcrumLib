using System.Text;
using GraphicsComposerLib.Rendering.Svg.Attributes;
using GraphicsComposerLib.Rendering.Svg.Content;
using GraphicsComposerLib.Rendering.Svg.Elements.Categories;
using GraphicsComposerLib.Rendering.Svg.Styles;
using GraphicsComposerLib.Rendering.Svg.Styles.SubStyles;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.Svg.Elements
{
    public abstract class SvgElement : ISvgElement
    {
        protected Dictionary<int, ISvgAttributeValue> AttributesTable { get; }
            = new Dictionary<int, ISvgAttributeValue>();

        public abstract string ElementName { get; }

        public bool IsContentText => false;

        public bool IsContentComment => false;

        public bool IsContentElement => true;

        public string Id
        {
            get =>
                AttributesTable.TryGetValue(SvgAttributeUtils.Id.Id, out var idAttrValue)
                    ? idAttrValue.ValueText
                    : string.Empty;
            set
            {
                var attrInfo = SvgAttributeUtils.Id;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                {
                    var idAttrValue = attrValue as SvgEavString<SvgElement>;

                    idAttrValue?.SetToText(value);

                    return;
                }

                var idAttrValue1 = new SvgEavString<SvgElement>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, idAttrValue1);

                idAttrValue1.SetToText(value);
            }
        }

        public SvgStyle Style
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Style;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgStyle;

                var attrValue1 = SvgStyle.Create(this);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public bool HasId =>
            AttributesTable.TryGetValue(SvgAttributeUtils.Id.Id, out var idAttrValue) && 
            !string.IsNullOrEmpty(idAttrValue.ValueText);

        public IEnumerable<ISvgAttributeValue> Attributes
            => AttributesTable.Values;

        public SvgContentsList Contents { get; }
            = new SvgContentsList();

        public IEnumerable<SvgElement> ChildElements
            => Contents
                .Select(c => c as SvgElement)
                .Where(c => !ReferenceEquals(c, null));

        public string ContentsText
            => Contents.ToString();

        public string AttributesText
        {
            get
            {
                if (AttributesTable.Count == 0)
                    return string.Empty;

                var composer = new StringBuilder();

                foreach (var pair in AttributesTable)
                    composer
                        .Append(pair.Value)
                        .Append(" ");

                return composer.ToString().Trim();
            }
        }

        public string BeginEndTagText
        {
            get
            {
                var composer = new LinearTextComposer();

                composer
                    .Append("<")
                    .Append(ElementName);

                var attrText = AttributesText;
                if (!string.IsNullOrEmpty(attrText))
                    composer.Append(" ").Append(attrText);

                return composer.Append("/>").ToString();
            }
        }

        public string BeginTagText
        {
            get
            {
                var composer = new StringBuilder();

                composer
                    .Append("<")
                    .Append(ElementName);

                var attrText = AttributesText;
                if (!string.IsNullOrEmpty(attrText))
                    composer.Append(" ").Append(attrText);

                return composer.Append(">").ToString();
            }
        }

        public string EndTagText
            => new StringBuilder()
                .Append("</")
                .Append(ElementName)
                .Append(">")
                .ToString();

        public string TagText
        {
            get
            {
                var contents = ContentsText.Trim();

                if (string.IsNullOrEmpty(contents))
                    return BeginEndTagText;

                var composer = new LinearTextComposer() {IndentationDefault = "  "};

                return composer
                    .AppendAtNewLine(BeginTagText)
                    .IncreaseIndentation()
                    .AppendAtNewLine(ContentsText)
                    .DecreaseIndentation()
                    .AppendAtNewLine(EndTagText)
                    .ToString();
            }
        }


        public ISvgElement ClearAttributes()
        {
            AttributesTable.Clear();

            return this;
        }

        public ISvgElement ClearAttribute(SvgAttributeInfo attributeInfo)
        {
            AttributesTable.Remove(attributeInfo.Id);

            return this;
        }

        public ISvgElement ClearAttributes(params SvgAttributeInfo[] attributeInfoList)
        {
            foreach (var attributeInfo in attributeInfoList)
                AttributesTable.Remove(attributeInfo.Id);

            return this;
        }

        public ISvgElement ClearDefaultAttributes(bool clearInChildren)
        {
            var attrIDs = 
                AttributesTable
                .Where(pair => pair.Value.IsNullOrDefault())
                .Select(pair => pair.Key);

            foreach (var attrId in attrIDs)
                AttributesTable.Remove(attrId);

            if (!clearInChildren)
                return this;

            foreach (var childElement in ChildElements)
                childElement.ClearDefaultAttributes(true);

            return this;
        }

        public SvgElement UpdateStyleFrom(SvgSubStyle sourceStyle)
        {
            sourceStyle.UpdateTargetStyle(Style);

            return this;
        }


        public override string ToString()
        {
            return TagText;
        }
    }
}
