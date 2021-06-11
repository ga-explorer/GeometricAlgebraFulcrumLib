namespace TextComposerLib.TextExpressions.Ast
{
    public sealed class TeLiteralString : ISimpleTextExpression
    {
        public string Text { get; set; }

        public string TextLiteral => Text.ValueToQuotedLiteral();

        public bool IsSimple => true;

        public bool IsComposite => false;

        public bool IsLiteral => true;

        public bool IsLiteralNumber => false;

        public bool IsLiteralString => true;

        public bool IsIdentifier => false;

        public bool IsSingleIdentifier => false;

        public bool IsQualifiedIdentifier => false;

        public bool IsList => false;

        public bool IsNamedList => false;

        public bool IsNamelessList => false;

        public bool IsDictionary => false;

        public bool IsNamedDictionary => false;

        public bool IsNamelessDictionary => false;


        public TeLiteralString(string text)
        {
            Text = text;
        }


        public ITextExpression CreateCopy()
        {
            return new TeLiteralString(Text);
        }

        public override string ToString()
        {
            return TextLiteral;
        }
    }
}
