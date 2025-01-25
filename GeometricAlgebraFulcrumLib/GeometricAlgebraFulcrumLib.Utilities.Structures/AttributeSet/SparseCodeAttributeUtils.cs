namespace GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

public static class SparseCodeAttributeUtils
{
    public static bool IsNullOrEmpty(this ISparseCodeAttributeValue? attributeValue)
    {
        return attributeValue is null || attributeValue.IsEmpty;
    }

    public static T GetValueOrDefault<T>(this ISparseCodeAttributeValue<T>? attributeValue, T defaultValue)
    {
        return attributeValue is null || attributeValue.IsEmpty
            ? defaultValue 
            : attributeValue.Value;
    }
}