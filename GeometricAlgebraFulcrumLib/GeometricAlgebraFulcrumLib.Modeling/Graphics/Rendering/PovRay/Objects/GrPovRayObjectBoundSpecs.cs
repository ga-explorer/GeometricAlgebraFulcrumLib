using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

public sealed class GrPovRayObjectBoundSpecs :
    IGrPovRayCodeElement
{
    public static GrPovRayObjectBoundSpecs UsingClippingObject { get; }
        = new GrPovRayObjectBoundSpecs(null, true);

    public static GrPovRayObjectBoundSpecs UsingObject(IGrPovRayObject boundingObject)
    {
        return new GrPovRayObjectBoundSpecs(boundingObject, false);
    }


    public bool IsEmptyCodeElement() =>
        BoundingObject.IsNullOrEmpty() &&
        !SameAsClippingObject;

    public IGrPovRayObject? BoundingObject { get; }

    public bool SameAsClippingObject { get; }


    private GrPovRayObjectBoundSpecs(IGrPovRayObject? boundingObject, bool sameAsClippingObject)
    {
        BoundingObject = boundingObject;
        SameAsClippingObject = sameAsClippingObject;
    }


    public string GetPovRayCode()
    {
        if (SameAsClippingObject)
            return "bounded_by { clipped_by }";

        if (BoundingObject is null || BoundingObject.IsEmptyCodeElement())
            return string.Empty;

        var composer = new LinearTextComposer();

        composer
            .AppendLineAtNewLine("bounded_by {")
            .IncreaseIndentation()
            .Append(((IGrPovRayCodeElement)BoundingObject).GetPovRayCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");
        
        return composer.ToString();
    }
}