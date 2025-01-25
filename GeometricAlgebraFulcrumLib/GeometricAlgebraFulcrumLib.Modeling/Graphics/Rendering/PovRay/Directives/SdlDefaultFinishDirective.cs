using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

public sealed class SdlDefaultFinishDirective : GrPovRayDirective
{
    public GrPovRayFinish Finish { get; set; }


    public override string GetPovRayCode()
    {
        throw new NotImplementedException();
    }
}