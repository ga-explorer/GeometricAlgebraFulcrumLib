using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayImageMapTypeValue :
    GrPovRayValue<GrPovRayImageMapType>
{
    public static GrPovRayImageMapTypeValue Planar { get; }
        = new GrPovRayImageMapTypeValue(
            GrPovRayImageMapType.Planar
        );

    public static GrPovRayImageMapTypeValue Spherical { get; }
        = new GrPovRayImageMapTypeValue(
            GrPovRayImageMapType.Spherical
        );
    
    public static GrPovRayImageMapTypeValue Cylindrical { get; }
        = new GrPovRayImageMapTypeValue(
            GrPovRayImageMapType.Cylindrical
        );
    
    public static GrPovRayImageMapTypeValue Torus { get; }
        = new GrPovRayImageMapTypeValue(
            GrPovRayImageMapType.Torus
        );


    public static implicit operator GrPovRayImageMapTypeValue(string valueText)
    {
        return new GrPovRayImageMapTypeValue(valueText);
    }

    public static implicit operator GrPovRayImageMapTypeValue(GrPovRayImageMapType value)
    {
        return new GrPovRayImageMapTypeValue(value);
    }
    

    private GrPovRayImageMapTypeValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayImageMapTypeValue(GrPovRayImageMapType value)
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