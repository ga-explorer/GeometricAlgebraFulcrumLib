using CodeComposerLib.MathML.Values.Color;

namespace CodeComposerLib.MathML.Values
{
    public static class MathMlValuesUtils
    {
        public static bool IsNullOrDefault(this MathMlColorValue value)
        {
            return ReferenceEquals(value, null) ||
                   value is MathMlEmptyColorValue;
        }

    }
}
