using System;

namespace TextComposerLib.TextExpressions.Ast
{
    public sealed class TeIdentifier : ISimpleTextExpression
    {
        public string Text { get; set; }

        public string TextLiteral => Text.ValueToQuotedLiteral();

        public bool IsSimple => true;

        public bool IsComposite => false;

        public bool IsLiteral => false;

        public bool IsLiteralNumber => false;

        public bool IsLiteralString => false;

        public bool IsIdentifier => true;

        public bool IsSingleIdentifier => Text.Contains('.') == false;

        public bool IsQualifiedIdentifier => Text.Contains('.');

        public bool IsList => false;

        public bool IsNamedList => false;

        public bool IsNamelessList => false;

        public bool IsDictionary => false;

        public bool IsNamedDictionary => false;

        public bool IsNamelessDictionary => false;

        public bool HasName => string.IsNullOrEmpty(Text) == false;

        public string[] Split => Text.Split(new[] { '.' }, StringSplitOptions.None);


        public TeIdentifier(string name)
        {
            Text = name;
        }


        public ITextExpression CreateCopy()
        {
            return new TeIdentifier(Text);
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
