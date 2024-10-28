using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Geometry.CGa;

public static class CGa5DIntersectionSamples
{
    public static CGaFloat64GeometricSpace5D Ga
        => CGaFloat64GeometricSpace5D.Instance;


    private static IEnumerable<CGaFloat64Flat> GetFlatElements()
    {
        var point1 = LinFloat64Vector3D.Create(-1.2, 1.4, 0.25);
        var point2 = LinFloat64Vector3D.Create(1.3, -2.4, -3.25);

        yield return Ga.DefineFlatPoint(
            point1
        );

        yield return Ga.DefineFlatPoint(
            point2
        );

        yield return Ga.DefineFlatLine(
            point1,
            LinFloat64Vector3D.Create(-1.1, 1.4, -2.1)
        );

        yield return Ga.DefineFlatLine(
            point2,
            LinFloat64Vector3D.Create(2.1, -1.1, 1.1)
        );

        yield return Ga.DefineFlatPlane(
            point1,
            LinFloat64Bivector3D.Create(-1.1, 1.4, -2.1)
        );

        yield return Ga.DefineFlatPlane(
            point2,
            LinFloat64Bivector3D.Create(2.1, -1.1, 1.1)
        );

        yield return Ga.DefineFlatVolume(
            point1
        );

        yield return Ga.DefineFlatVolume(
            point2
        );
    }

    private static IEnumerable<CGaFloat64Round> GetRoundElements()
    {
        var point1 = LinFloat64Vector3D.Create(-1.2, 1.4, 0.25);
        var point2 = LinFloat64Vector3D.Create(1.3, -2.4, -3.25);

        yield return Ga.DefineRoundPoint(
            point1
        );

        yield return Ga.DefineRoundPoint(
            point2
        );

        yield return Ga.DefineRoundPointPair(
            6,
            point1,
            LinFloat64Vector3D.Create(-1.1, 1.4, -2.1)
        );

        yield return Ga.DefineRoundPointPair(
            5,
            point2,
            LinFloat64Vector3D.Create(2.1, -1.1, 1.1)
        );

        yield return Ga.DefineRoundCircle(
            4,
            point1,
            LinFloat64Bivector3D.Create(-1.1, 1.4, -2.1)
        );

        yield return Ga.DefineRoundCircle(
            3,
            point2,
            LinFloat64Bivector3D.Create(2.1, -1.1, 1.1)
        );

        yield return Ga.DefineRoundSphere(
            6,
            point1
        );

        yield return Ga.DefineRoundSphere(
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