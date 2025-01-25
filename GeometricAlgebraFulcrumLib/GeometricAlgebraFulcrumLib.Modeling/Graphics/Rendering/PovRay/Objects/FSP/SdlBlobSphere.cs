using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class SdlBlobSphere : GrPovRayObject, ISdlBlobComponent
{
    public GrPovRayFloat32Value Strength { get; set; }

    public GrPovRayVector3Value Center { get; set; }

    public GrPovRayFloat32Value Radius { get; set; }


    public SdlBlobSphere()
    {
        Strength = Strength = GrPovRayFloat32Value.One;
    }

    public SdlBlobSphere(GrPovRayFloat32Value strength)
    {
        Strength = strength;
    }

    public override string GetPovRayCode()
    {
        throw new NotImplementedException();
    }
}