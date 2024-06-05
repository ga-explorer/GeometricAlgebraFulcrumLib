using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves.Spherical;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Interpolation;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Operations;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Samples.Modeling.Geometry.Conformal;

public static class CGa5DConstructionSamples
{
    public static RGaConformalSpace5D Ga
        => RGaConformalSpace5D.Instance;


    public static void Example1()
    {
        // Encode a point-pair OPNS blade
        var pointPair1Blade = Ga.EncodeOpnsRoundPointPair(
            LinFloat64Vector3D.Create(1, 2, 1),
            LinFloat64Vector3D.Create(-1, 0, 2)
        );

        // Encode a flat point OPNS blade
        var point1Blade = Ga.EncodeOpnsFlatPoint(
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

        Console.WriteLine(pointPair1Blade.DecodeOpnsRound());
        Console.WriteLine(pointPair2Blade.DecodeOpnsRound());
        Console.WriteLine(bivector1.DecodeOpnsElement());
    }


    private static IParametricCurve3D GetPositionCurve1(double maxTime)
    {
        var freqHz = 1 / maxTime;
        var freq = 2 * Math.PI * freqHz;

        var parameterRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = ConstantParametricScalar.Create(5);
        var thetaCurve = LinearParametricScalar.Create(freq);
        var phiCurve = ConstantParametricScalar.Create(0);

        var curve = SphericalCurve3D.Create(
            parameterRange,
            rCurve,
            thetaCurve,
            phiCurve
        );

        return curve;
    }

    private static IParametricCurve3D GetPositionCurve2(double maxTime)
    {
        var freqHz = 1 / maxTime;
        var freq = 2 * Math.PI * freqHz;

        var parameterRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = ConstantParametricScalar.Create(5);
        var thetaCurve = LinearParametricScalar.Create(-freq);
        var phiCurve = ConstantParametricScalar.Create(0);

        var curve = SphericalCurve3D.Create(
            parameterRange,
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

        var animationSpecs =
            GrVisualAnimationSpecs.Create(frameRate, maxTime);

        var curve1 = GetPositionCurve1(maxTime);
        var curve2 = GetPositionCurve2(maxTime);
        var curve3 = ConstantParametricCurve3D.Create(0, 0, 0);

        var circle1 =
            Ga.DefineRoundCircleFromPoints(
                curve1,
                curve2,
                curve3
            );

        var frameIndexTimePairs =
            animationSpecs.FrameIndexTimePairs.ToImmutableArray();

        var circleList =
            frameIndexTimePairs.Select(p =>
                circle1.GetElement(p.Value)
            ).ToImmutableArray();

        var isCircleList =
            circleList.SelectToImmutableArray(c =>
                c is RGaConformalRound { IsRoundCircle: true, Weight: > 0 }
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
                (RGaConformalRound)circleList[index - 1],
                (RGaConformalRound)circleList[index + 1]
            )
        );
    }
}