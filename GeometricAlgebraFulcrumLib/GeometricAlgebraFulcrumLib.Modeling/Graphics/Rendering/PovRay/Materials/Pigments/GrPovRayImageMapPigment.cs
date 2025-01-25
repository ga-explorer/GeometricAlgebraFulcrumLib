using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

public sealed class GrPovRayImageMapPigment :
    GrPovRayTransformablePigment
{
    public string BitmapFileName { get; }

    public GrPovRayImageMapBitmapType BitmapType { get; }
    
    public GrPovRayFloat32Value? Gamma { get; set; }

    public GrPovRayBooleanValue? PreMultiplied { get; set; }

    public GrPovRayImageMapPigmentProperties Properties { get; private set; }
        = new GrPovRayImageMapPigmentProperties();

    
    internal GrPovRayImageMapPigment(string bitmapFileName, GrPovRayImageMapBitmapType bitmapType = GrPovRayImageMapBitmapType.Undefined)
        : base(string.Empty)
    {
        BitmapType = bitmapType;
        BitmapFileName = bitmapFileName;
    }


    public GrPovRayImageMapPigment SetProperties(GrPovRayImageMapPigmentProperties properties)
    {
        Properties = new GrPovRayImageMapPigmentProperties(properties);

        return this;
    }

    public override bool IsEmptyCodeElement()
    {
        return false;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer()
            .AppendLine("pigment {")
            .IncreaseIndentation()
            .AppendLineAtNewLine("image_map {")
            .IncreaseIndentation()
            .AppendAtNewLine(BitmapType.GetPovRayCode() + " \"" + BitmapFileName + "\" ");
        
        if (Gamma is not null)
            composer.Append("gamma " + Gamma.GetPovRayCode() + " ");

        if (PreMultiplied is not null)
            composer.Append("premultiplied " + PreMultiplied.GetPovRayCode());

        if (!Properties.IsEmptyCodeElement())
            composer.AppendAtNewLine(Properties.GetPovRayCode());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        if (QuickColor is not null)
            composer.AppendAtNewLine("quick_" + QuickColor.GetPovRayCode());

        if (!Transform.IsNearIdentity())
            composer.AppendAtNewLine(Transform.GetPovRayMatrixCode());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}