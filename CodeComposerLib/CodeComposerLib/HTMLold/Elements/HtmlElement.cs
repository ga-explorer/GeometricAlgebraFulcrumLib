using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.HTMLold.Attributes;
using CodeComposerLib.HTMLold.Content;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.HTMLold.Elements
{
    public abstract class HtmlElement : IHtmlElement
    {
        protected Dictionary<int, IHtmlAttributeValue> AttributesTable { get; }
            = new Dictionary<int, IHtmlAttributeValue>();

        public abstract string ElementName { get; }

        public bool IsContentText => false;

        public bool IsContentComment => false;

        public bool IsContentElement => true;

        public string Id
        {
            get
            {
                return AttributesTable.TryGetValue(HtmlAttributeUtils.Id.Id, out var idAttrValue)
                    ? idAttrValue.ValueText
                    : string.Empty;
            }
            set
            {
                var attrInfo = HtmlAttributeUtils.Id;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                {
                    var idAttrValue = attrValue as Attributes.HtmlEavString<HtmlElement>;

                    idAttrValue?.SetToText(value);

                    return;
                }

                var idAttrValue1 = new Attributes.HtmlEavString<HtmlElement>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, idAttrValue1);

                idAttrValue1.SetToText(value);
            }
        }

        //public HtmlStyle Style
        //{
        //    get
        //    {
        //        var attrInfo = HtmlAttributeUtils.Style;

        //        IHtmlAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as HtmlStyle;

        //        var attrValue1 = HtmlStyle.Create(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public bool HasId
        {
            get
            {
                return AttributesTable.TryGetValue(HtmlAttributeUtils.Id.Id, out var idAttrValue) && 
                       !string.IsNullOrEmpty(idAttrValue.ValueText);
            }
        }

        public IEnumerable<IHtmlAttributeValue> Attributes
            => AttributesTable.Values;

        public HtmlContentsList Contents { get; }
            = new HtmlContentsList();

        public IEnumerable<HtmlElement> ChildElements
            => Contents
                .Select(c => c as HtmlElement)
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


        public IHtmlElement ClearAttributes()
        {
            AttributesTable.Clear();

            return this;
        }

        public IHtmlElement ClearAttribute(HtmlAttributeInfo attributeInfo)
        {
            AttributesTable.Remove(attributeInfo.Id);

            return this;
        }

        public IHtmlElement ClearAttributes(params HtmlAttributeInfo[] attributeInfoList)
        {
            foreach (var attributeInfo in attributeInfoList)
                AttributesTable.Remove(attributeInfo.Id);

            return this;
        }

        public IHtmlElement ClearDefaultAttributes(bool clearInChildren)
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

        //public HtmlElement UpdateStyleFrom(HtmlSubStyle sourceStyle)
        //{
        //    sourceStyle.UpdateTargetStyle(Style);

        //    return this;
        //}


        public override string ToString()
        {
            return TagText;
        }
    }
}