using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRaySizeValue :
    GrPovRayValue<SizeF>
{
    internal static GrPovRaySizeValue Create(SizeF value)
    {
        return new GrPovRaySizeValue(value);
    }


    public static implicit operator GrPovRaySizeValue(string valueText)
    {
        return new GrPovRaySizeValue(valueText);
    }

    public static implicit operator GrPovRaySizeValue(SizeF value)
    {
        return new GrPovRaySizeValue(value);
    }

        
    public bool IsSquare 
        => !IsEmpty && Value.Width == Value.Height;


    private GrPovRaySizeValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRaySizeValue(SizeF value)
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