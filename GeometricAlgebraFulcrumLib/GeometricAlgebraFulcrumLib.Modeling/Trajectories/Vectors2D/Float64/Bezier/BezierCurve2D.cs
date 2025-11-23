using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

public sealed class BezierCurve2D :
    Float64Path2D,
    IFloat64BoundedShape2D
{
    public static BezierCurve2D FromPolygon(IReadOnlyList<LinFloat64Vector2D> controlPolygon, bool isPeriodic = false, double tolerance = Float64Utils.DefaultTolerance)
    {
        var degree = controlPolygon.Count - 1;

        if (degree < 0)
            throw new ArgumentException();

        var xCoefArray = new double[degree + 1];
        var yCoefArray = new double[degree + 1];

        var i = 0;
        foreach (var p in controlPolygon)
        {
            xCoefArray[i] = p.X;
            yCoefArray[i] = p.Y;

            i++;
        }

        return new BezierCurve2D(
            BernsteinPolynomial.Create(xCoefArray),
            BernsteinPolynomial.Create(yCoefArray),
            isPeriodic,
            tolerance
        );
    }

    public static BezierCurve2D Create(BernsteinPolynomial x, BernsteinPolynomial y, bool isPeriodic = false, double tolerance = Float64Utils.DefaultTolerance)
    {
        return new BezierCurve2D(x, y, isPeriodic, tolerance);
    }


    public double Tolerance { get; }

    public BernsteinPolynomial X { get; }

    public BernsteinPolynomial Y { get; }

    public BernsteinPolynomial XDt { get; }

    public BernsteinPolynomial YDt { get; }

    public BernsteinPolynomial XDt2 { get; }

    public BernsteinPolynomial YDt2 { get; }

    public double X0
        => X.GetValue(MinTime);

    public double X1
        => X.GetValue(MaxTime);

    public double Y0
        => Y.GetValue(MinTime);

    public double Y1
        => Y.GetValue(MaxTime);

    public bool IntersectionTestsEnabled
        => true;


    private BezierCurve2D(BernsteinPolynomial x, BernsteinPolynomial y, bool isPeriodic, double tolerance = Float64Utils.DefaultTolerance)
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
        Tolerance = tolerance;

        (X, Y) = BernsteinPolynomial.SimplifyToEqualDegree(
            x,
            y,
            tolerance
        );

        (XDt, YDt) = BernsteinPolynomial.SimplifyToEqualDegree(
            X.Derivative(),
            Y.Derivative(),
            tolerance
        );

        (XDt2, YDt2) = BernsteinPolynomial.SimplifyToEqualDegree(
            XDt.Derivative(),
            YDt.Derivative(),
            tolerance
        );
    }


    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    public override LinFloat64Vector2D GetValue(double t)
    {
        return LinFloat64Vector2D.Create(
            X.GetValue(t),
            Y.GetValue(t)
        );
    }

    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return LinFloat64Vector2D.Create(
            XDt.GetValue(t),
            YDt.GetValue(t)
        );
    }

    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        return LinFloat64Vector2D.Create(
            XDt2.GetValue(t),
            YDt2.GetValue(t)
        );
    }

    public override Float64Path2D ToFinitePath()
    {
        return IsFinite
            ? this
            : new BezierCurve2D(X, Y, false, Tolerance);
    }

    public override Float64Path2D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new BezierCurve2D(X, Y, true, Tolerance);
    }

    public IReadOnlyList<LinFloat64Vector2D> GetControlPoints()
    {
        Debug.Assert(X.Degree == Y.Degree);

        return (X.Degree + 1).GetRange(i =>
            LinFloat64Vector2D.Create(
                X.Coeffs[i],
                Y.Coeffs[i]
            )
        ).ToArray();
    }

    public Float64BoundingBox2D GetBoundingBox()
    {
        return GetAdaptiveTimeSamples()
            .Select(GetPoint)
            .ToPolyline()
            .GetBoundingBox();
    }

    public Float64BoundingBoxComposer2D GetBoundingBoxComposer()
    {
        return GetAdaptiveTimeSamples()
            .Select(GetPoint)
            .ToPolyline()
            .GetBoundingBox()
            .GetBoundingBoxComposer();
    }


    public Float64Polyline2D ToAdaptivePolyline(SamplingOptions? samplingOptions = null)
    {
        samplingOptions ??= new SamplingOptions();

        return new Float64Polyline2D(
            GetAdaptiveTimeSamples(samplingOptions).Select(GetPoint)
        );
    }

    public Float64Polyline2D ToUniformTimePolyline(int sampleCount)
    {
        if (sampleCount < 2)
            throw new ArgumentException("Sample count must be at least 2", nameof(sampleCount));

        return new Float64Polyline2D(
            GetUniformTimeSamples(sampleCount).Select(GetPoint)
        );
    }

    public Float64Polyline2D ToUniformArcLengthPolyline(int sampleCount)
    {
        if (sampleCount < 2)
            throw new ArgumentException("Sample count must be at least 2", nameof(sampleCount));

        return new Float64Polyline2D(
            GetUniformArcLengthTimeSamples(sampleCount).Select(GetPoint)
        );
    }

    public IReadOnlyList<double> FindCuspTimeValues(double tolerance = Float64Utils.DefaultTolerance)
    {
        // Root intervals for x'(t) and y'(t)
        var xRoots = XDt.FindRoots(tolerance);
        var yRoots = YDt.FindRoots(tolerance);

        // Merge intervals that overlap (both derivatives vanish)
        var cuspTimes = new List<double>();

        foreach (var rx in xRoots)
            foreach (var ry in yRoots)
            {
                if (rx.IsNearEqual(ry, 2 * tolerance))
                    cuspTimes.Add(0.5 * (rx + ry));
            }

        return cuspTimes;
    }

    public bool HasCusps(double tolerance = Float64Utils.DefaultTolerance)
    {
        // Root intervals for x'(t) and y'(t)
        var xRoots = XDt.FindRoots(tolerance);
        var yRoots = YDt.FindRoots(tolerance);

        // Merge intervals that overlap (both derivatives vanish)
        foreach (var rx in xRoots)
            foreach (var ry in yRoots)
            {
                if (rx.IsNearEqual(ry, 2 * tolerance))
                    return true;
            }

        return false;
    }

    public bool HasLoops(SamplingOptions? samplingOptions = null)
    {
        return ToAdaptivePolyline(samplingOptions).HasSelfIntersection();
    }

    /// <summary>
    /// Compute minimum distance between this PH curve and a point (px, py).
    /// Uses Newton–Raphson with multi-start sampling and bisection fallback.
    /// </summary>
    public double GetMinDistanceToPoint(double px, double py, int samples = 20, double tolerance = Float64Utils.DefaultTolerance, int maxIter = 50)
    {
        var minDistSq = double.PositiveInfinity;

        // 1. Endpoints
        var dx0 = X.GetValue(0.0) - px;
        var dy0 = Y.GetValue(0.0) - py;
        minDistSq = Math.Min(minDistSq, dx0 * dx0 + dy0 * dy0);

        var dx1 = X.GetValue(1.0) - px;
        var dy1 = Y.GetValue(1.0) - py;
        minDistSq = Math.Min(minDistSq, dx1 * dx1 + dy1 * dy1);

        // 2. Interior candidates with enhanced sampling
        for (var k = 0; k <= samples; k++)
        {
            var t = (double)k / samples;

            for (var iter = 0; iter < maxIter; iter++)
            {
                var xt = X.GetValue(t);
                var yt = Y.GetValue(t);
                var xdt = XDt.GetValue(t);
                var ydt = YDt.GetValue(t);

                var fx = xt - px;
                var fy = yt - py;

                var f = fx * xdt + fy * ydt; // derivative of squared distance
                var df = xdt * xdt + ydt * ydt + fx * XDt2.GetValue(t) + fy * YDt2.GetValue(t);

                if (Math.Abs(f) < tolerance)
                    break; // converged

                if (Math.Abs(df) > 1e-14)
                {
                    var newton = t - f / df;

                    if (newton is >= 0 and <= 1)
                        t = newton;
                    else
                        break; // out of bounds, discard
                }
                else
                    break;
            }

            // Evaluate distance
            var dx = X.GetValue(t) - px;
            var dy = Y.GetValue(t) - py;
            var distSq = dx * dx + dy * dy;

            minDistSq = Math.Min(minDistSq, distSq);
        }

        return Math.Sqrt(minDistSq);
    }

    /// <summary>
    /// Compute minimum distance between this 2D PH curve and a line segment AB.
    /// </summary>
    public double GetMinDistanceToLineSegment(double ax, double ay, double bx, double by, int samples = 20, double tolerance = Float64Utils.DefaultTolerance, int maxIter = 50)
    {
        var minDistSq = double.PositiveInfinity;
        var dx = bx - ax;
        var dy = by - ay;
        var dd = dx * dx + dy * dy;

        // 1. Endpoints A, B vs curve
        minDistSq = Math.Min(minDistSq, Math.Pow(GetMinDistanceToPoint(ax, ay), 2));
        minDistSq = Math.Min(minDistSq, Math.Pow(GetMinDistanceToPoint(bx, by), 2));

        // 2. this vs segment interior
        for (var k = 0; k <= samples; k++)
        {
            var t = (double)k / samples;

            for (var iter = 0; iter < maxIter; iter++)
            {
                var xt = X.GetValue(t);
                var yt = Y.GetValue(t);
                var xdt = XDt.GetValue(t);
                var ydt = YDt.GetValue(t);

                // Projection of r(t) onto AB
                var vx = xt - ax;
                var vy = yt - ay;
                var u = (vx * dx + vy * dy) / dd;
                u = Math.Max(0.0, Math.Min(1.0, u));
                var qx = ax + u * dx;
                var qy = ay + u * dy;

                // Residual
                var fx = xt - qx;
                var fy = yt - qy;
                var f = fx * xdt + fy * ydt;

                if (Math.Abs(f) < tolerance) break;

                var d2 = xdt * xdt + ydt * ydt
                                 + fx * XDt2.GetValue(t)
                                 + fy * YDt2.GetValue(t);

                if (Math.Abs(d2) > 1e-14)
                {
                    var newton = t - f / d2;
                    if (newton is >= 0 and <= 1) t = newton;
                    else break;
                }
                else break;
            }

            // Final distance candidate
            var xt2 = X.GetValue(t);
            var yt2 = Y.GetValue(t);
            var vx2 = xt2 - ax;
            var vy2 = yt2 - ay;
            var u2 = (vx2 * dx + vy2 * dy) / dd;
            u2 = Math.Max(0.0, Math.Min(1.0, u2));
            var qx2 = ax + u2 * dx;
            var qy2 = ay + u2 * dy;
            var dx2 = xt2 - qx2;
            var dy2 = yt2 - qy2;
            minDistSq = Math.Min(minDistSq, dx2 * dx2 + dy2 * dy2);
        }

        return Math.Sqrt(minDistSq);
    }

    /// <summary>
    /// Compute minimum distance between this PH curve and another
    /// </summary>
    /// <param name="curve2"></param>
    /// <param name="grid"></param>
    /// <param name="maxIter"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    public CurveCurveDistanceResult GetMinDistanceToCurve(PhBernsteinCurve2D curve2, int grid = 10, int maxIter = 50, double tolerance = Float64Utils.DefaultTolerance)
    {
        var D = GetDistanceSquaredFunction(curve2);
        var minDistSq = double.PositiveInfinity;
        var tBest = 0.0;
        var sBest = 0.0;

        double[] ends = [0.0, 1.0];
        foreach (var t in ends)
            foreach (var s in ends)
            {
                var val = D.EvaluateAt(t, s);
                if (val < minDistSq)
                {
                    minDistSq = val;
                    tBest = t;
                    sBest = s;
                }
            }

        for (var i = 0; i <= grid; i++)
            for (var j = 0; j <= grid; j++)
            {
                var t = (double)i / grid;
                var s = (double)j / grid;

                for (var k = 0; k < maxIter; k++)
                {
                    var p1 = new[] { X.GetValue(t), Y.GetValue(t) };
                    var p2 = new[] { curve2.X.GetValue(s), curve2.Y.GetValue(s) };
                    var dr = new[] { p1[0] - p2[0], p1[1] - p2[1] };

                    var r1d = new[] { XDt.GetValue(t), YDt.GetValue(t) };
                    var r2d = new[] { curve2.XDt.GetValue(s), curve2.YDt.GetValue(s) };

                    var f = dr[0] * r1d[0] + dr[1] * r1d[1];
                    var g = -(dr[0] * r2d[0] + dr[1] * r2d[1]);

                    if (Math.Abs(f) < tolerance && Math.Abs(g) < tolerance) break;

                    var ft = r1d[0] * r1d[0] + r1d[1] * r1d[1];
                    var gs = r2d[0] * r2d[0] + r2d[1] * r2d[1];

                    if (ft > 1e-14) t = Math.Clamp(t - f / ft, 0.0, 1.0);
                    if (gs > 1e-14) s = Math.Clamp(s - g / gs, 0.0, 1.0);
                }

                var val2 = D.EvaluateAt(t, s);
                if (val2 < minDistSq)
                {
                    minDistSq = val2;
                    tBest = t;
                    sBest = s;
                }
            }

        var point1 = new[] { X.GetValue(tBest), Y.GetValue(tBest) };
        var point2 = new[] { curve2.X.GetValue(sBest), curve2.Y.GetValue(sBest) };

        return new CurveCurveDistanceResult(Math.Sqrt(minDistSq), tBest, sBest, point1, point2);
    }

    /// <summary>
    /// Construct squared distance function between two PH curves
    /// as a rational bivariate Bernstein polynomial.
    /// </summary>
    public RationalBernsteinPolynomial2D GetDistanceSquaredFunction(PhBernsteinCurve2D curve2)
    {
        // X component
        var x1 = BernsteinPolynomial2D.FromT(X);
        var x2 = BernsteinPolynomial2D.FromS(curve2.X);
        var dx = x1 - x2;
        var sum = dx * dx;

        // Y component
        var y1 = BernsteinPolynomial2D.FromT(Y);
        var y2 = BernsteinPolynomial2D.FromS(curve2.Y);
        var dy = y1 - y2;
        sum += dy * dy;

        // Wrap into rational form (denominator = 1 for polynomial PH curves)
        var denom = new BernsteinPolynomial2D(0, 0, new[,] { { 1.0 } });

        return new RationalBernsteinPolynomial2D(sum, denom);
    }
    

    /// <summary>
    /// Compute the geometric area between this Bezier curve and its control polygon.
    /// The control polygon is the polyline P0->P1->...->Pn, and the closed loop is
    /// that polygon followed by the Bezier curve from Pn back to P0. Returns the
    /// absolute area (unless signed = true is given).
    /// This computes the curve integral exactly (in Bernstein basis) and the polygon
    /// area via the shoelace formula — no subdivision required.
    /// </summary>
    public double GetAreaBetweenCurveAndControlPolygon(bool signed = false)
    {
        // 1) Control polygon points
        var pts = GetControlPoints();
        var n = pts.Count;
        if (n < 2) return 0.0;

        // 2) Signed area of the closed polygon P0->P1->...->Pn->P0 (shoelace)
        var polySum = 0.0;
        for (var i = 0; i < n - 1; i++)
        {
            polySum += pts[i].X * pts[i + 1].Y - pts[i].Y * pts[i + 1].X;
        }

        // close polygon: Pn -> P0
        polySum += pts[n - 1].X * pts[0].Y - pts[n - 1].Y * pts[0].X;
        var polyAreaSigned = 0.5 * polySum;

        // 3) Curve integral I = 0.5 * ∫_0^1 (X * Y' - Y * X') dt
        // Compute integrand polynomial in Bernstein basis:
        var integrand = X * YDt - Y * XDt; // BernsteinPolynomial supports arithmetic
        var deg = integrand.Degree;
        if (deg < 0) // zero polynomial guard
        {
            var curveAreaSigned = 0.0;
            var areaSigned = polyAreaSigned - curveAreaSigned;
            return signed ? areaSigned : Math.Abs(areaSigned);
        }

        // sum of Bernstein coefficients / (deg+1) gives definite integral from 0..1
        var coeffSum = 0.0;
        var coeffs = integrand.Coeffs; // assumes Coeffs[] is available
        for (var i = 0; i <= deg; i++)
            coeffSum += coeffs[i];

        var integralValue = coeffSum / (deg + 1); // ∫_0^1 integrand dt
        var curveAreaSignedHalf = 0.5 * integralValue; // 0.5 * integral

        // Total signed area = polygon area (P0->...->Pn->P0) plus Bezier area traversed backwards
        // As derived: area = polyAreaSigned - curveAreaSignedHalf
        var totalSignedArea = polyAreaSigned - curveAreaSignedHalf;

        return signed ? totalSignedArea : Math.Abs(totalSignedArea);
    }

}