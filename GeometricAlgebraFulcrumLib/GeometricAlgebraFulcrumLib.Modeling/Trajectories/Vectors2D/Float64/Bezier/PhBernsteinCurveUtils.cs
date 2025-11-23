using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

public static class PhBernsteinCurveUtils
{
    public static double GetArcLength(this IPhBernsteinCurve curve, double t)
    {
        return curve.S.GetValue(t);
    }
    
    public static double GetArcSpeed(this IPhBernsteinCurve curve, double t)
    {
        return curve.SDt.GetValue(t);
    }

    /// <summary>
    /// Compute parameter t corresponding to arc length s using Newton–Raphson
    /// with bisection fallback for robustness.
    /// </summary>
    /// <param name="curve"></param>
    /// <param name="s">Target arc length (0 &lt;= s &lt;= total arc length)</param>
    /// <param name="tolerance">Convergence tolerance</param>
    /// <param name="maxIterations">Max iterations</param>
    public static double ArcLengthToParameter(this IPhBernsteinCurve curve, double s, double tolerance = Float64Utils.DefaultTolerance, int maxIterations = 50)
    {
        var sTotal = curve.S.GetValue(1.0);
        if (s <= 0) return 0.0;
        if (s >= sTotal) return 1.0;

        // Initial guess: linear proportion
        var t = s / sTotal;

        double low = 0.0, high = 1.0;

        for (var i = 0; i < maxIterations; i++)
        {
            var sAtT = curve.S.GetValue(t);
            var error = sAtT - s;

            if (Math.Abs(error) < tolerance)
                return t;

            var speed = curve.SDt.GetValue(t); // derivative of S(t)
            if (speed > 1e-14) // safeguard
            {
                var newton = t - error / speed;

                if (newton > low && newton < high)
                    t = newton; // accept Newton step
                else
                    t = 0.5 * (low + high); // fallback to bisection
            }
            else
            {
                t = 0.5 * (low + high); // fallback if speed ~0
            }

            if (error > 0) 
                high = t;
            else 
                low = t;
        }

        return t; // last best approximation
    }

    /// <summary>
    /// Compute parameters t[] corresponding to arc lengths s[] using
    /// Newton–Raphson with bisection fallback. Input sValues must be sorted.
    /// </summary>
    public static double[] ArcLengthsToParameters(this IPhBernsteinCurve curve, double[] sValues, double tolerance = Float64Utils.DefaultTolerance, int maxIterations = 50)
    {
        var sTotal = curve.S.GetValue(1.0);
        var n = sValues.Length;
        var tValues = new double[n];

        var low = 0.0;
        var high = 1.0;

        for (var j = 0; j < n; j++)
        {
            var s = sValues[j];
            if (s <= 0) { tValues[j] = 0.0; continue; }
            if (s >= sTotal) { tValues[j] = 1.0; continue; }

            // Initial guess proportional to arc length
            var t = s / sTotal;

            var lo = low;
            var hi = high;

            for (var i = 0; i < maxIterations; i++)
            {
                var sAtT = curve.S.GetValue(t);
                var error = sAtT - s;

                if (Math.Abs(error) < tolerance)
                    break;

                var speed = curve.SDt.GetValue(t);
                if (speed > 1e-14)
                {
                    var newton = t - error / speed;

                    if (newton > lo && newton < hi)
                        t = newton;
                    else
                        t = 0.5 * (lo + hi);
                }
                else
                {
                    t = 0.5 * (lo + hi);
                }

                if (error > 0) 
                    hi = t;
                else 
                    lo = t;
            }

            tValues[j] = t;
            low = t; // monotonic: next search can start from here
        }

        return tValues;
    }


    /// <summary>
    /// Compute minimum distances between one PH curve and a set of other PH curves.
    /// </summary>
    public static CurveCurveDistanceResult[] ComputeMinDistances(
        dynamic curve,
        IEnumerable<dynamic> others,
        int grid = 10,
        int maxIter = 50,
        double tolerance = Float64Utils.DefaultTolerance)
    {
        var results = new List<CurveCurveDistanceResult>();

        foreach (var other in others)
        {
            var result = curve.GetMinDistance(other, grid, maxIter, tolerance);
            results.Add(result);
        }

        return results.ToArray();
    }



    public static CurveCurveDistanceResult ComputeMinDistance(
        PhBernsteinCurve3D curve1,
        PhBernsteinCurve3D curve2,
        int grid = 10,
        int maxIter = 50,
        double tolerance = Float64Utils.DefaultTolerance)
    {
        var D = DistanceSquaredFunction(curve1, curve2);
        var minDistSq = double.PositiveInfinity;
        double tBest = 0.0, sBest = 0.0;

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
                    var p1 = new[] { curve1.X.GetValue(t), curve1.Y.GetValue(t), curve1.Z.GetValue(t) };
                    var p2 = new[] { curve2.X.GetValue(s), curve2.Y.GetValue(s), curve2.Z.GetValue(s) };
                    var dr = new[] { p1[0] - p2[0], p1[1] - p2[1], p1[2] - p2[2] };

                    var r1d = new[] { curve1.XDt.GetValue(t), curve1.YDt.GetValue(t), curve1.ZDt.GetValue(t) };
                    var r2d = new[] { curve2.XDt.GetValue(s), curve2.YDt.GetValue(s), curve2.ZDt.GetValue(s) };

                    var f = dr[0] * r1d[0] + dr[1] * r1d[1] + dr[2] * r1d[2];
                    var g = -(dr[0] * r2d[0] + dr[1] * r2d[1] + dr[2] * r2d[2]);

                    if (Math.Abs(f) < tolerance && Math.Abs(g) < tolerance) break;

                    var ft = r1d[0] * r1d[0] + r1d[1] * r1d[1] + r1d[2] * r1d[2];
                    var gs = r2d[0] * r2d[0] + r2d[1] * r2d[1] + r2d[2] * r2d[2];

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

        var point1 = new[] { curve1.X.GetValue(tBest), curve1.Y.GetValue(tBest), curve1.Z.GetValue(tBest) };
        var point2 = new[] { curve2.X.GetValue(sBest), curve2.Y.GetValue(sBest), curve2.Z.GetValue(sBest) };

        return new CurveCurveDistanceResult(Math.Sqrt(minDistSq), tBest, sBest, point1, point2);
    }

    public static CurveCurveDistanceResult ComputeMinDistance(
        PhBernsteinCurve4D curve1,
        PhBernsteinCurve4D curve2,
        int grid = 10,
        int maxIter = 50,
        double tolerance = Float64Utils.DefaultTolerance)
    {
        var D = DistanceSquaredFunction(curve1, curve2);
        var minDistSq = double.PositiveInfinity;
        double tBest = 0.0, sBest = 0.0;

        double[] ends = [0.0, 1.0];
        foreach (var t in ends)
            foreach (var s in ends)
            {
                var val = D.EvaluateAt(t, s);
                if (val < minDistSq) { minDistSq = val; tBest = t; sBest = s; }
            }

        for (var i = 0; i <= grid; i++)
            for (var j = 0; j <= grid; j++)
            {
                var t = (double)i / grid;
                var s = (double)j / grid;

                for (var k = 0; k < maxIter; k++)
                {
                    var p1 = new[] { curve1.X.GetValue(t), curve1.Y.GetValue(t), curve1.Z.GetValue(t), curve1.W.GetValue(t) };
                    var p2 = new[] { curve2.X.GetValue(s), curve2.Y.GetValue(s), curve2.Z.GetValue(s), curve2.W.GetValue(s) };
                    var dr = new[] { p1[0] - p2[0], p1[1] - p2[1], p1[2] - p2[2], p1[3] - p2[3] };

                    var r1d = new[] { curve1.XDt.GetValue(t), curve1.YDt.GetValue(t), curve1.ZDt.GetValue(t), curve1.WDt.GetValue(t) };
                    var r2d = new[] { curve2.XDt.GetValue(s), curve2.YDt.GetValue(s), curve2.ZDt.GetValue(s), curve2.WDt.GetValue(s) };

                    var f = dr[0] * r1d[0] + dr[1] * r1d[1] + dr[2] * r1d[2] + dr[3] * r1d[3];
                    var g = -(dr[0] * r2d[0] + dr[1] * r2d[1] + dr[2] * r2d[2] + dr[3] * r2d[3]);

                    if (Math.Abs(f) < tolerance && Math.Abs(g) < tolerance) break;

                    var ft = r1d[0] * r1d[0] + r1d[1] * r1d[1] + r1d[2] * r1d[2] + r1d[3] * r1d[3];
                    var gs = r2d[0] * r2d[0] + r2d[1] * r2d[1] + r2d[2] * r2d[2] + r2d[3] * r2d[3];

                    if (ft > 1e-14) t = Math.Clamp(t - f / ft, 0.0, 1.0);
                    if (gs > 1e-14) s = Math.Clamp(s - g / gs, 0.0, 1.0);
                }

                var val2 = D.EvaluateAt(t, s);
                if (val2 < minDistSq) { minDistSq = val2; tBest = t; sBest = s; }
            }

        var point1 = new[] { curve1.X.GetValue(tBest), curve1.Y.GetValue(tBest), curve1.Z.GetValue(tBest), curve1.W.GetValue(tBest) };
        var point2 = new[] { curve2.X.GetValue(sBest), curve2.Y.GetValue(sBest), curve2.Z.GetValue(sBest), curve2.W.GetValue(sBest) };

        return new CurveCurveDistanceResult(Math.Sqrt(minDistSq), tBest, sBest, point1, point2);
    }


    
    /// <summary>
    /// Construct squared distance function between two PH curves
    /// as a rational bivariate Bernstein polynomial.
    /// </summary>
    public static RationalBernsteinPolynomial2D DistanceSquaredFunction(PhBernsteinCurve3D curve1, PhBernsteinCurve3D curve2)
    {
        // X component
        var x1 = BernsteinPolynomial2D.FromT(curve1.X);
        var x2 = BernsteinPolynomial2D.FromS(curve2.X);
        var dx = x1 - x2;
        var sum = dx * dx;

        // Y component
        var y1 = BernsteinPolynomial2D.FromT(curve1.Y);
        var y2 = BernsteinPolynomial2D.FromS(curve2.Y);
        var dy = y1 - y2;
        sum += dy * dy;

        // Z component
        var z1 = BernsteinPolynomial2D.FromT(curve1.Z);
        var z2 = BernsteinPolynomial2D.FromS(curve2.Z);
        var dz = z1 - z2;
        sum += dz * dz;
    
        // Wrap into rational form (denominator = 1 for polynomial PH curves)
        var denom = new BernsteinPolynomial2D(0, 0, new[,] { { 1.0 } });

        return new RationalBernsteinPolynomial2D(sum, denom);
    }

    /// <summary>
    /// Construct squared distance function between two PH curves
    /// as a rational bivariate Bernstein polynomial.
    /// </summary>
    public static RationalBernsteinPolynomial2D DistanceSquaredFunction(PhBernsteinCurve4D curve1, PhBernsteinCurve4D curve2)
    {
        // X component
        var x1 = BernsteinPolynomial2D.FromT(curve1.X);
        var x2 = BernsteinPolynomial2D.FromS(curve2.X);
        var dx = x1 - x2;
        var sum = dx * dx;

        // Y component
        var y1 = BernsteinPolynomial2D.FromT(curve1.Y);
        var y2 = BernsteinPolynomial2D.FromS(curve2.Y);
        var dy = y1 - y2;
        sum += dy * dy;

        // Z component
        var z1 = BernsteinPolynomial2D.FromT(curve1.Z);
        var z2 = BernsteinPolynomial2D.FromS(curve2.Z);
        var dz = z1 - z2;
        sum += dz * dz;
    
        // W component
        var w1 = BernsteinPolynomial2D.FromT(curve1.W);
        var w2 = BernsteinPolynomial2D.FromS(curve2.W);
        var dw = w1 - w2;
        sum += dw * dw;

        // Wrap into rational form (denominator = 1 for polynomial PH curves)
        var denom = new BernsteinPolynomial2D(0, 0, new[,] { { 1.0 } });

        return new RationalBernsteinPolynomial2D(sum, denom);
    }
}