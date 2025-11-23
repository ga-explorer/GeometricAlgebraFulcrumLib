using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Geometry.CGa;

public static class CGa5DIntersectionSamples
{
    public static CGaFloat64GeometricSpace5D Ga
        => CGaFloat64GeometricSpace5D.Instance;


    private static IEnumerable<CGaFloat64Flat> GetFlatElements()
    {
        var point1 = LinFloat64Vector3D.Create(-1.2, 1.4, 0.25);
        var point2 = LinFloat64Vector3D.Create(1.3, -2.4, -3.25);

        yield return Ga.FlatPoint(
            point1
        );

        yield return Ga.FlatPoint(
            point2
        );

        yield return Ga.FlatLine(
            point1,
            LinFloat64Vector3D.Create(-1.1, 1.4, -2.1)
        );

        yield return Ga.FlatLine(
            point2,
            LinFloat64Vector3D.Create(2.1, -1.1, 1.1)
        );

        yield return Ga.FlatPlane(
            point1,
            LinFloat64Bivector3D.Create(-1.1, 1.4, -2.1)
        );

        yield return Ga.FlatPlane(
            point2,
            LinFloat64Bivector3D.Create(2.1, -1.1, 1.1)
        );

        yield return Ga.FlatVolume(
            point1
        );

        yield return Ga.FlatVolume(
            point2
        );
    }

    private static IEnumerable<CGaFloat64Round> GetRoundElements()
    {
        var point1 = LinFloat64Vector3D.Create(-1.2, 1.4, 0.25);
        var point2 = LinFloat64Vector3D.Create(1.3, -2.4, -3.25);

        yield return Ga.RoundPoint(
            point1
        );

        yield return Ga.RoundPoint(
            point2
        );

        yield return Ga.RoundPointPair(
            6,
            point1,
            LinFloat64Vector3D.Create(-1.1, 1.4, -2.1)
        );

        yield return Ga.RoundPointPair(
            5,
            point2,
            LinFloat64Vector3D.Create(2.1, -1.1, 1.1)
        );

        yield return Ga.RoundCircle(
            4,
            point1,
            LinFloat64Bivector3D.Create(-1.1, 1.4, -2.1)
        );

        yield return Ga.RoundCircle(
            3,
            point2,
            LinFloat64Bivector3D.Create(2.1, -1.1, 1.1)
        );

        yield return Ga.RoundSphere(
            6,
            point1
        );

        yield return Ga.RoundSphere(
            4,
            point2
        );
    }


    public static void FlatWithFlatIntersection()
    {
        var flatList =
            GetFlatElements().ToImmutableArray();

        foreach (var flat1 in flatList)
            foreach (var flat2 in flatList)
            {
                var intersectionElement =
                    flat1.EncodeIpnsBlade().MeetIpns(
                        flat2.EncodeIpnsBlade()
                    ).Decode.IpnsElement();

                Console.WriteLine("First Element:");
                Console.WriteLine(flat1);
                Console.WriteLine();

                Console.WriteLine("Second Element:");
                Console.WriteLine(flat2);
                Console.WriteLine();

                Console.WriteLine("Intersection Element:");
                Console.WriteLine(intersectionElement);
                Console.WriteLine();
            }
    }

    public static void FlatWithRoundIntersection()
    {
        var flatList =
            GetFlatElements().ToImmutableArray();

        var roundList =
            GetRoundElements().ToImmutableArray();

        foreach (var flat1 in flatList)
            foreach (var round2 in roundList)
            {
                var intersectionElement =
                    flat1.EncodeIpnsBlade().MeetIpns(
                        round2.EncodeIpnsBlade()
                    ).Decode.IpnsElement();

                Console.WriteLine("First Element:");
                Console.WriteLine(flat1);
                Console.WriteLine();

                Console.WriteLine("Second Element:");
                Console.WriteLine(round2);
                Console.WriteLine();

                Console.WriteLine("Intersection Element:");
                Console.WriteLine(intersectionElement);
                Console.WriteLine();
            }
    }

    public static void RoundWithRoundIntersection()
    {
        var roundList =
            GetRoundElements().ToImmutableArray();

        foreach (var round1 in roundList)
            foreach (var round2 in roundList)
            {
                var intersectionElement =
                    round1.EncodeIpnsBlade().MeetIpns(
                        round2.EncodeIpnsBlade()
                    ).Decode.IpnsElement();

                Console.WriteLine("First Element:");
                Console.WriteLine(round1);
                Console.WriteLine();

                Console.WriteLine("Second Element:");
                Console.WriteLine(round2);
                Console.WriteLine();

                Console.WriteLine("Intersection Element:");
                Console.WriteLine(intersectionElement);
                Console.WriteLine();
            }
    }
}