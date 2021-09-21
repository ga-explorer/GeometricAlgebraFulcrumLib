namespace GraphicsComposerLib.Svg.Values
{
    public abstract class SvgStoredValue : ISvgValue
    {
        public string ValueText { get; }

        protected SvgStoredValue(string value)
        {
            ValueText = value;
        }

        public override string ToString()
        {
            return ValueText;
        }
    }
}
