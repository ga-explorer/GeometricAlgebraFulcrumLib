using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class SdlBlobCylinder : GrPovRayObject, ISdlBlobComponent
{
    public GrPovRayFloat32Value Strength { get; set; }

    public GrPovRayVector3Value BasePoint { get; set; }

    public GrPovRayVector3Value CapPoint { get; set; }

    public GrPovRayFloat32Value Radius { get; set; }


    public SdlBlobCylinder()
    {
        Strength = Strength = GrPovRayFloat32Value.One;
    }

    public SdlBlobCylinder(GrPovRayFloat32Value strength)
    {
        Strength = strength;
    }

    public override string GetPovRayCode()
    {
        throw new NotImplementedException();
    }
}