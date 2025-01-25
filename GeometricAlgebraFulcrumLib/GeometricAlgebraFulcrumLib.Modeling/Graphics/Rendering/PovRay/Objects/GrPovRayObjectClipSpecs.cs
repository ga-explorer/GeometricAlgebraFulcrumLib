using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

public sealed class GrPovRayObjectClipSpecs :
    IGrPovRayCodeElement
{
    public static GrPovRayObjectClipSpecs UsingBoundingObject { get; }
        = new GrPovRayObjectClipSpecs(null, true);
    
    public static GrPovRayObjectClipSpecs UsingObject(IGrPovRayObject clippingObject)
    {
        return new GrPovRayObjectClipSpecs(clippingObject, false);
    }


    public bool IsEmptyCodeElement() =>
        ClippingObject.IsNullOrEmpty() &&
        !SameAsBoundingObject;

    public IGrPovRayObject? ClippingObject { get; }

    public bool SameAsBoundingObject { get; }


    private GrPovRayObjectClipSpecs(IGrPovRayObject? clippingObject, bool sameAsBoundingObject)
    {
        ClippingObject = clippingObject;
        SameAsBoundingObject = sameAsBoundingObject;
    }


    public string GetPovRayCode()
    {
        if (SameAsBoundingObject)
            return "clipped_by { bounded_by }";

        if (ClippingObject is null || ClippingObject.IsEmptyCodeElement())
            return string.Empty;

        var composer = new LinearTextComposer();

        composer
            .AppendLineAtNewLine("clipped_by {")
            .IncreaseIndentation()
            .Append(((IGrPovRayCodeElement)ClippingObject).GetPovRayCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");
        
        return composer.ToString();
    }
}