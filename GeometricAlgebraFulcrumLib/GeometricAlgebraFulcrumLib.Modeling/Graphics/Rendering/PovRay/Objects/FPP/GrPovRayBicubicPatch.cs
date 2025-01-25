using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FPP;

public class GrPovRayBicubicPatch :
    GrPovRayObject,
    IGrPovRayFinitePatchObject
{
    public bool PreprocessSubPatches { get; }

    public GrPovRayInt32Value? USteps { get; set; }

    public GrPovRayInt32Value? VSteps { get; set; }

    public GrPovRayFloat32Value? Flatness { get; set; }

    public GrPovRayVector3Value[,] ConvexHull { get; } 
    

    internal GrPovRayBicubicPatch(bool preprocessSubPatches, GrPovRayVector3Value[,] convexHull)
    {
        if (convexHull.GetLength(0) != 4 || convexHull.GetLength(1) != 4)
            throw new ArgumentException(nameof(convexHull));

        PreprocessSubPatches = preprocessSubPatches;
        ConvexHull = convexHull;
    }


    public GrPovRayBicubicPatch SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }
    
    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("bicubic_patch {")
            .IncreaseIndentation()
            .AppendAtNewLine(PreprocessSubPatches ? "1" : "0");

        if (USteps is not null)
            composer.AppendAtNewLine("u_steps " + USteps.GetAttributeValueCode());

        if (VSteps is not null)
            composer.AppendAtNewLine("v_steps " + VSteps.GetAttributeValueCode());

        if (Flatness is not null)
            composer.AppendAtNewLine("flatness " + Flatness.GetAttributeValueCode());

        for (var row = 0; row < 4; row++)
        {
            var rowText = ConvexHull.GetRowItems(
                v => v.GetAttributeValueCode(),
                row
            ).Concatenate(",");

            composer.AppendAtNewLine(rowText);

            if (row < 3) composer.Append(",");
        }

        composer
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}