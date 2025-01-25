using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.ISP;

public class SdlPolySurface : 
    GrPovRayPolynomialObject, 
    IGrPovRayInfiniteSolidObject
{
    public int Order { get; private set; }

    public GrPovRayFloat32Value[] Coefs { get; }

    public int CoefsCount 
        => Coefs.Length;


    public SdlPolySurface(int order)
    {
        Order = order;

        var n = (order + 1)*(order + 2)*(order + 3)/6;
        Coefs = new GrPovRayFloat32Value[n];
    }

    public override string GetPovRayCode()
    {
        throw new NotImplementedException();
    }
}