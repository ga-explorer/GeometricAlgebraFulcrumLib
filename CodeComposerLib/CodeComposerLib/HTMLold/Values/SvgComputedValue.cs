namespace CodeComposerLib.HTMLold.Values
{
    public abstract class HtmlComputedValue : IHtmlValue
    {
        public abstract string ValueText { get; }

        public override string ToString()
        {
            return ValueText;
        }
    }
}
