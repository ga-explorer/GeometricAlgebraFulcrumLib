using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class SdlBlob : 
    GrPovRayPolynomialObject, 
    IGrPovRayFiniteSolidObject
{
    public List<ISdlBlobComponent> Components { get; private set; }

    public GrPovRayFloat32Value Threshold { get; set; }


    public SdlBlob()
    {
        Components = new List<ISdlBlobComponent>();
    }

    public override string GetPovRayCode()
    {
        throw new NotImplementedException();
    }
}