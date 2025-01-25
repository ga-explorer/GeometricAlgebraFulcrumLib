using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Textures;

public class GrPovRayPlainTexture : 
    GrPovRayTexture
{
    public IGrPovRayPigment? Pigment { get; set; }

    public IGrPovRayFinish? Finish { get; set; }

    public override bool IsEmptyCodeElement() =>
        BaseElementIdentifier.IsNullOrEmpty() &&
        Pigment.IsNullOrEmpty() &&
        Finish.IsNullOrEmpty();


    public GrPovRayPlainTexture()
        : base(string.Empty)
    {
    }

    public GrPovRayPlainTexture(string identifier)
        : base(identifier)
    {
    }


    public override string GetPovRayCode(bool isInterior)
    {
        var composer = new LinearTextComposer();

        if (isInterior) 
            composer.Append("interior_");

        composer
            .AppendLine("texture {")
            .IncreaseIndentation();

        if (!string.IsNullOrEmpty(BaseElementIdentifier))
            composer.AppendAtNewLine(BaseElementIdentifier);

        if (Pigment is not null && !Pigment.IsEmptyCodeElement())
            composer.AppendAtNewLine(Pigment.GetPovRayCode());

        if (Finish is not null && !Finish.IsEmptyCodeElement())
            composer.AppendAtNewLine(Finish.GetPovRayCode());

        if (!Transform.IsNearIdentity())
            composer.AppendAtNewLine(Transform.GetPovRayMatrixCode());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}