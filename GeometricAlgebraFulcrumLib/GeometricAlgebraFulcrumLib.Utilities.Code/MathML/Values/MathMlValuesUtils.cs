using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Color;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values;

public static class MathMlValuesUtils
{
    public static bool IsNullOrDefault(this MathMlColorValue value)
    {
        return ReferenceEquals(value, null) ||
               value is MathMlEmptyColorValue;
    }

}