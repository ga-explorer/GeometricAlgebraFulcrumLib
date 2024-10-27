using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Geometry.CGa;

public static class CGa5DProjectionSamples
{
    public static CGaFloat64GeometricSpace5D CGa
        => CGaFloat64GeometricSpace5D.Instance;


    public static void FlatPointProjectionExample()
    {
        // Encode an IPNS flat point blade
        var ipnsPoint =
            CGa.Encode.IpnsRound.Point(1, 2, 3);


        // Encode a IPNS flat point
        var ipnsFlatPointBlade =
            CGa.Encode.IpnsFlat.Point(
                LinFloat64Vector3D.Create(3.1, 2.4, -3.5)
            );

        // Encode a, IPNS flat line
        var ipnsFlatLineBlade =
            CGa.Encode.IpnsFlat.Line(
                LinFloat64Vector3D.Create(3, 3, 3),
                LinFloat64Vector3D.Create(1, 2, -1)
            );

        // Encode a, IPNS flat plane
        var ipnsFlatPlaneBlade =
            CGa.Encode.IpnsFlat.Plane(
                LinFloat64Vector3D.Create(3, 3, 3),
                LinFloat64Vector3D.Create(1, 2, -1)
            );

        // Encode a, IPNS flat volume
        var ipnsFlatVolumeBlade =
            CGa.Encode.IpnsFlat.Volume(
                LinFloat64Vector3D.Create(3, 3, 3),
                LinFloat64Trivector3D.E123
            );


        // Encode a IPNS round point
        var ipnsRoundPointBlade =
            CGa.Encode.IpnsRound.Point(
                LinFloat64Vector3D.Create(3.1, 2.4, -3.5)
            );

        // Encode a, IPNS round point pair
        var ipnsRoundPointPairBlade =
            CGa.Encode.IpnsRound.PointPair(
                4,
                LinFloat64Vector3D.Create(3, 3, 3).ToRGaFloat64Vector(),
                LinFloat64Vector3D.Create(1, 2, -1).ToRGaFloat64Vector()
            );

        // Encode a, IPNS round circle
        var ipnsRoundCircleBlade =
            CGa.Encode.IpnsRound.Circle(
                4,
                LinFloat64Vector3D.Create(3, 3, 3),
                LinFloat64Vector3D.Create(1, 2, -1)
            );

        // Encode a, IPNS round sphere
        var ipnsRoundSphereBlade =
            CGa.Encode.IpnsRound.Sphere(
                5,
                LinFloat64Vector3D.Create(3, 3, 3)
            );

        // Project the point on the element and decode the resulting element
        var projectedElement =
            ipnsPoint.ProjectIpnsOn(ipnsRoundSphereBlade).Decode.IpnsElement();

        Console.WriteLine(projectedElement);
        Console.WriteLine();
    }
}