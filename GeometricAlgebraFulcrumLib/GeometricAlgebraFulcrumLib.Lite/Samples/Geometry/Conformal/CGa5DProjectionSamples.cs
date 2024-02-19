using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Operations;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Geometry.Conformal;

public static class CGa5DProjectionSamples
{
    public static RGaConformalSpace5D CGa
        => RGaConformalSpace5D.Instance;


    public static void FlatPointProjectionExample()
    {
        // Encode an IPNS flat point blade
        var ipnsPoint = 
            CGa.EncodeIpnsRoundPoint(1, 2, 3);
        

        // Encode a IPNS flat point
        var ipnsFlatPointBlade =
            CGa.EncodeIpnsFlatPoint(
                Float64Vector3D.Create(3.1, 2.4, -3.5)
            );

        // Encode a, IPNS flat line
        var ipnsFlatLineBlade =
            CGa.EncodeIpnsFlatLine(
                Float64Vector3D.Create(3, 3, 3), 
                Float64Vector3D.Create(1, 2, -1)
            );
        
        // Encode a, IPNS flat plane
        var ipnsFlatPlaneBlade =
            CGa.EncodeIpnsFlatPlane(
                Float64Vector3D.Create(3, 3, 3), 
                Float64Vector3D.Create(1, 2, -1)
            );
        
        // Encode a, IPNS flat volume
        var ipnsFlatVolumeBlade =
            CGa.EncodeIpnsFlatVolume(
                Float64Vector3D.Create(3, 3, 3), 
                Float64Trivector3D.E123
            );

        
        // Encode a IPNS round point
        var ipnsRoundPointBlade =
            CGa.EncodeIpnsRoundPoint(
                Float64Vector3D.Create(3.1, 2.4, -3.5)
            );
        
        // Encode a, IPNS round point pair
        var ipnsRoundPointPairBlade =
            CGa.EncodeIpnsRoundPointPair(
                4,
                Float64Vector3D.Create(3, 3, 3).ToRGaFloat64Vector(), 
                Float64Vector3D.Create(1, 2, -1).ToRGaFloat64Vector()
            );
        
        // Encode a, IPNS round circle
        var ipnsRoundCircleBlade =
            CGa.EncodeIpnsRoundCircle(
                4,
                Float64Vector3D.Create(3, 3, 3), 
                Float64Vector3D.Create(1, 2, -1)
            );
        
        // Encode a, IPNS round sphere
        var ipnsRoundSphereBlade =
            CGa.EncodeIpnsRoundSphere(
                5,
                Float64Vector3D.Create(3, 3, 3)
            );
        
        // Project the point on the element and decode the resulting element
        var projectedElement =
            ipnsPoint.ProjectIpnsOn(ipnsRoundSphereBlade).DecodeIpnsElement();

        Console.WriteLine(projectedElement);
        Console.WriteLine();
    }
}