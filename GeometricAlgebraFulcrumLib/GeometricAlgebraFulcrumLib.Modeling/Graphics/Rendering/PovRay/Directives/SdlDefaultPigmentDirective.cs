using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

public sealed class SdlDefaultPigmentDirective : GrPovRayDirective
{
    public IGrPovRayPigment Pigment { get; set; }

    public override string GetPovRayCode()
    {
        throw new NotImplementedException();
    }
}