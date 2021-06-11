namespace TextComposerLib.Text.Parametric
{
    public sealed class StringSequenceTemplate : ParametricTextComposer
    {
        public static readonly string IndexParamName = "index";

        public static readonly string NameParamName = "name";


        public string IndexFormatString { get; set; }

        public int IndexValue { get; set; }


        public string NameParamValue
        {
            get { return this[NameParamName]; }
            set { this[NameParamName] = value; }
        }


        public StringSequenceTemplate()
            : base("#", "#", "#name##index#")
        {
        }

        public StringSequenceTemplate(string templateText)
            : base("#", "#", templateText)
        {
        }

        public StringSequenceTemplate(string leftDelimiter, string rightDelimiter)
            : base(leftDelimiter, rightDelimiter)
        {
        }

        public StringSequenceTemplate(string leftDelimiter, string rightDelimiter, string templateText)
            : base(leftDelimiter, rightDelimiter, templateText)
        {
        }


        public void ClearFormatStrings()
        {
            IndexFormatString = null;
        }

        public string GenerateNextString()
        {
            if (ContainsParameter(IndexParamName))
                this[IndexParamName] =
                    GenerateFormattedText(IndexValue, IndexFormatString);

            IndexValue++;

            return GenerateText();
        }
    }
}
