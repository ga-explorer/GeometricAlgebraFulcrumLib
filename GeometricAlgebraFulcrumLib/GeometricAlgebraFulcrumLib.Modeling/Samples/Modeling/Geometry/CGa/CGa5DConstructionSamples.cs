using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Interpolation;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Geometry.CGa;

public static class CGa5DConstructionSamples
{
    public static CGaFloat64GeometricSpace5D Ga
        => CGaFloat64GeometricSpace5D.Instance;


    public static void Example1()
    {
        // Encode a point-pair OPNS blade
        var pointPair1Blade = Ga.Encode.OpnsRound.PointPair(
            LinFloat64Vector3D.Create(1, 2, 1),
            LinFloat64Vector3D.Create(-1, 0, 2)
        );

        // Encode a flat point OPNS blade
        var point1Blade = Ga.Encode.OpnsFlat.Point(
            LinFloat64Vector3D.Create(3, -2, 3)
        );

        // Construct the plane OPNS blade passing through the point and point-pair
        var plane1 =
            point1Blade.Op(pointPair1Blade);

        // Get the direction bivector OPNS blade of the plane
        var bivector1 =
            Ga.Ei.Lcp(plane1).Negative();

        // TODO: Can we use CGA directly to create a circle from the center and
        // passing through the point pair?

        var pointPair2Blade =
            pointPair1Blade.ReflectOpnsOnIpns(point1Blade);

        Console.WriteLine(pointPair1Blade.DecodeOpnsRound.Element());
        Console.WriteLine(pointPair2Blade.DecodeOpnsRound.Element());
        Console.WriteLine(bivector1.Decode.OpnsElement());
    }


    private static Float64Path3D GetPositionCurve1(double maxTime)
    {
        var freqHz = 1 / maxTime;
        var freq = Math.Tau * freqHz;

        var timeRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = Float64ScalarSignal.FiniteConstant(timeRange, 5);
        var thetaCurve = Float64ScalarSignal.FiniteRamp(timeRange, 0, freq);
        var phiCurve = Float64ScalarSignal.FiniteZero(timeRange);

        var curve = Float64SphericalPath3D.Finite(
            timeRange,
            rCurve,
            thetaCurve,
            phiCurve
        );

        return curve;
    }

    private static Float64Path3D GetPositionCurve2(double maxTime)
    {
        var freqHz = 1 / maxTime;
        var freq = Math.Tau * freqHz;

        var timeRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = Float64ScalarSignal.FiniteConstant(timeRange, 5);
        var thetaCurve = Float64ScalarSignal.FiniteRamp(timeRange, 0, -freq);
        var phiCurve = Float64ScalarSignal.FiniteZero(timeRange);

        var curve = Float64SphericalPath3D.Finite(
            timeRange,
            rCurve,
            thetaCurve,
            phiCurve
        );

        return curve;
    }

    public static void Example2()
    {
        const double maxTime = 24d;
        const int frameRate = 50;

        var samplingSpecs =
            Float64SamplingSpecs.Create(frameRate, maxTime);

        var curve1 = GetPositionCurve1(maxTime);
        var curve2 = GetPositionCurve2(maxTime);
        var curve3 = Float64ConstantPath3D.Finite(0, 0, 0);

        var circle1 =
            Ga.DefineRoundCircleFromPoints(
                curve1,
                curve2,
                curve3
            );

        var frameIndexTimePairs =
            samplingSpecs.SampleIndexTimePairs.ToImmutableArray();

        var circleList =
            frameIndexTimePairs.Select(p =>
                circle1.GetElement(p.Value)
            ).ToImmutableArray();

        var isCircleList =
            circleList.SelectToImmutableArray(c =>
                c is CGaFloat64Round { IsRoundCircle: true, Weight: > 0 }
            );

        var indexList =
            isCircleList
                .Select((b, i) => Tuple.Create(i, b))
                .Where(t => !t.Item2)
                .SelectToImmutableArray(t => t.Item1);

        var index = indexList[1];
        Console.WriteLine(circleList[index - 1]);
        Console.WriteLine(circleList[index + 1]);
        Console.WriteLine(
            0.5d.LerpCircle3D(
                (CGaFloat64Round)circleList[index - 1],
                (CGaFloat64Round)circleList[index + 1]
            )
        );
    }
}