namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayInt32Value :
    GrPovRayValue<int>
{
    public static implicit operator GrPovRayInt32Value(string valueText)
    {
        return new GrPovRayInt32Value(valueText);
    }

    public static implicit operator GrPovRayInt32Value(int value)
    {
        return new GrPovRayInt32Value(value);
    }
    
    public static implicit operator GrPovRayInt32Value(float value)
    {
        return new GrPovRayInt32Value((int) value);
    }
    
    public static implicit operator GrPovRayInt32Value(double value)
    {
        return new GrPovRayInt32Value((int) value);
    }


    private GrPovRayInt32Value(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayInt32Value(int value)
        : base(value)
    {
    }


    public override string GetPovRayCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetPovRayCode() 
            : ValueText;
    }
}