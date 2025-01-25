using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayImageMapInterpolateTypeValue :
    GrPovRayValue<GrPovRayImageMapInterpolateType>
{
    public static GrPovRayImageMapInterpolateTypeValue Bilinear { get; }
        = new GrPovRayImageMapInterpolateTypeValue(
            GrPovRayImageMapInterpolateType.Bilinear
        );

    public static GrPovRayImageMapInterpolateTypeValue Bicubic { get; }
        = new GrPovRayImageMapInterpolateTypeValue(
            GrPovRayImageMapInterpolateType.Bicubic
        );
    
    public static GrPovRayImageMapInterpolateTypeValue NormalizedDistance { get; }
        = new GrPovRayImageMapInterpolateTypeValue(
            GrPovRayImageMapInterpolateType.NormalizedDistance
        );



    public static implicit operator GrPovRayImageMapInterpolateTypeValue(string valueText)
    {
        return new GrPovRayImageMapInterpolateTypeValue(valueText);
    }

    public static implicit operator GrPovRayImageMapInterpolateTypeValue(GrPovRayImageMapInterpolateType value)
    {
        return new GrPovRayImageMapInterpolateTypeValue(value);
    }
    

    private GrPovRayImageMapInterpolateTypeValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayImageMapInterpolateTypeValue(GrPovRayImageMapInterpolateType value)
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