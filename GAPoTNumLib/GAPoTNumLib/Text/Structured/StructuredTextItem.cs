using System;
using System.Text;

namespace GAPoTNumLib.Text.Structured
{
    public sealed class StructuredTextItem
    {
        public static StructuredTextItem Empty { get; private set; }

        static StructuredTextItem()
        {
            Empty = new StructuredTextItem(String.Empty);
        }


        public string Prefix { get; }

        public string Suffix { get; }

        public string Text { get; }


        public StructuredTextItem(string text)
        {
            Prefix = String.Empty;
            Suffix = String.Empty;
            Text = text ?? String.Empty;
        }

        public StructuredTextItem(string prefix, string text, string suffix)
        {
            Prefix = prefix ?? String.Empty;
            Suffix = suffix ?? String.Empty;
            Text = text ?? String.Empty;
        }


        public override string ToString()
        {
            return 
                new StringBuilder(Prefix.Length + Text.Length + Suffix.Length)
                .Append(Prefix)
                .Append(Text)
                .Append(Suffix)
                .ToString();
        }
    }
}
