namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public class GrPovRayCodeValue :
    GrPovRayValue
{
    public static implicit operator GrPovRayCodeValue(string valueText)
    {
        return new GrPovRayCodeValue(valueText);
    }

    
    public GrPovRayCodeValue(string valueText) 
        : base(valueText)
    {
    }


    public override bool IsEmpty 
        => string.IsNullOrEmpty(ValueText);

    public override string GetPovRayCode()
    {
        return ValueText;
    }
}