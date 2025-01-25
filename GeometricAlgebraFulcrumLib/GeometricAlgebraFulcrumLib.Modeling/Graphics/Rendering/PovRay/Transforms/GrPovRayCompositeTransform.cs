using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Transforms;

/// <summary>
/// http://www.povray.org/documentation/3.7.0/r3_3.html#r3_3_1_12
/// </summary>
public sealed class GrPovRayCompositeTransform : 
    GrPovRayTransform
{
    public string BaseTransformName { get; }

    public bool Inverse { get; set; }

    public GrPovRayTransformList Transforms { get; } 
        = new GrPovRayTransformList();

    
    internal GrPovRayCompositeTransform(bool inverse = false)
    {
        BaseTransformName = string.Empty;
        Inverse = inverse;
    }

    internal GrPovRayCompositeTransform(string baseTransformName, bool inverse = false)
    {
        BaseTransformName = baseTransformName;
        Inverse = inverse;
    }


    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("transform {")
            .IncreaseIndentation();

        if (!BaseTransformName.IsNullOrEmpty())
            composer.AppendAtNewLine(BaseTransformName);

        composer.AppendAtNewLine(Transforms.GetPovRayCode());

        if (Inverse)
            composer.AppendAtNewLine("inverse");

        composer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");

        return composer.ToString();
    }

    
}