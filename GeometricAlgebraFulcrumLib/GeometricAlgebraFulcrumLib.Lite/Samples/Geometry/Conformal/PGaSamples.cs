using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Geometry.Conformal;

public static class PGaSamples
{
    public static void Example1()
    {
        var cga = RGaConformalSpace.Space5D;

        var plane1 = cga.DefineFlatPlane(
            Float64Vector3D.Create(1, 2, -1), 
            Float64Vector3D.Create(1, -1, 1)
        );


    }
}