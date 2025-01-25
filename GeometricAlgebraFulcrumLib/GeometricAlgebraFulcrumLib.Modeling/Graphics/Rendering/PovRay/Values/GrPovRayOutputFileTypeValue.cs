using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Constants;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayOutputFileTypeValue :
    GrPovRayValue<GrPovRayOutputFileType>
{
    public static GrPovRayOutputFileTypeValue Bitmap { get; }
        = new GrPovRayOutputFileTypeValue(
            GrPovRayOutputFileType.Bitmap
        );

    public static GrPovRayOutputFileTypeValue Png { get; }
        = new GrPovRayOutputFileTypeValue(
            GrPovRayOutputFileType.Png
        );

    public static GrPovRayOutputFileTypeValue Jpeg { get; }
        = new GrPovRayOutputFileTypeValue(
            GrPovRayOutputFileType.Jpeg
        );
    
    public static GrPovRayOutputFileTypeValue Ppm { get; }
        = new GrPovRayOutputFileTypeValue(
            GrPovRayOutputFileType.Ppm
        );
    
    public static GrPovRayOutputFileTypeValue CompressedTarGa24 { get; }
        = new GrPovRayOutputFileTypeValue(
            GrPovRayOutputFileType.CompressedTarGa24
        );
    
    public static GrPovRayOutputFileTypeValue UncompressedTarGa24 { get; }
        = new GrPovRayOutputFileTypeValue(
            GrPovRayOutputFileType.UncompressedTarGa24
        );
    
    public static GrPovRayOutputFileTypeValue OpenExrHdr { get; }
        = new GrPovRayOutputFileTypeValue(
            GrPovRayOutputFileType.OpenExrHdr
        );
    
    public static GrPovRayOutputFileTypeValue RadianceHdr { get; }
        = new GrPovRayOutputFileTypeValue(
            GrPovRayOutputFileType.RadianceHdr
        );
    
    public static GrPovRayOutputFileTypeValue SystemSpecific { get; }
        = new GrPovRayOutputFileTypeValue(
            GrPovRayOutputFileType.SystemSpecific
        );


    //public static implicit operator GrPovRayOutputFileTypeValue(string valueText)
    //{
    //    return new GrPovRayOutputFileTypeValue(valueText);
    //}

    public static implicit operator GrPovRayOutputFileTypeValue(GrPovRayOutputFileType value)
    {
        return new GrPovRayOutputFileTypeValue(value);
    }
    

    private GrPovRayOutputFileTypeValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayOutputFileTypeValue(GrPovRayOutputFileType value)
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