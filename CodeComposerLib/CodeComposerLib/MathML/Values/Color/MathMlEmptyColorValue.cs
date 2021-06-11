namespace CodeComposerLib.MathML.Values.Color
{
    public sealed class MathMlEmptyColorValue : MathMlColorValue
    {
        public override string ValueText => string.Empty;

        internal MathMlEmptyColorValue()
        {
        }
    }
}