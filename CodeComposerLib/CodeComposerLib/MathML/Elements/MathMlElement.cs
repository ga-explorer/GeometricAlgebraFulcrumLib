namespace CodeComposerLib.MathML.Elements
{
    public abstract class MathMlElement : IMathMlElement
    {
        public abstract bool IsToken { get; }

        public bool IsLayout => !IsToken;

        public abstract string XmlTagName { get; }

        public abstract string ContentsText { get; }

        public string Class { get; set; }

        public string Id { get; set; }

        public string Style { get; set; }


        internal virtual void UpdateAttributesComposer(MathMlAttributesComposer composer)
        {
            composer.SetAttributeValue(
                "class", 
                Class, 
                string.IsNullOrEmpty
            );

            composer.SetAttributeValue(
                "id", 
                Id,
                string.IsNullOrEmpty
            );

            composer.SetAttributeValue(
                "style", 
                Style,
                string.IsNullOrEmpty
            );
        }

        public override string ToString()
        {
            var composer = new MathMlAttributesComposer
            {
                IndentationDefault = "  "
            };

            UpdateAttributesComposer(composer);

            return composer
                .AppendTagCode(XmlTagName, ContentsText, IsToken)
                .ToString();
        }
    }
}