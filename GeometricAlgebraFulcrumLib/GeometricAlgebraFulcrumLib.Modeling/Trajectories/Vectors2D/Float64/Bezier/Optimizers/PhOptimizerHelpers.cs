using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.Optimizers;

public static class PhOptimizerHelpers
{
    /// <summary>
    /// Compute squared distance sum from waypoints to curve (nearest point approx by sampling).
    /// </summary>
    public static double WaypointProximityEnergy(PhBernsteinPolyCurve2D multi, List<LinFloat64Vector2D> waypoints, PhOptimizerOptions opts)
    {
        if (multi == null) throw new ArgumentNullException(nameof(multi));
        if (waypoints == null) throw new ArgumentNullException(nameof(waypoints));

        // Use sampler and coarse nearest by parameter sampling
        var total = 0.0;
        // create a sampler over the entire multi-segment curve
        // Here we assume PhMultiSegmentCurve2D exposes EvaluateAtGlobalParameter etc.
        // Simpler: sample evenly per segment
        var segments = multi.Count;
        for (var i = 0; i < waypoints.Count; i++)
        {
            var wp = waypoints[i];
            var bestSq = double.PositiveInfinity;
            var samplesPer = opts.SamplesPerSegment;
            for (var s = 0; s < segments; s++)
            {
                var seg = multi[s];
                for (var k = 0; k <= samplesPer; k++)
                {
                    var t = k / (double)samplesPer;
                    var px = seg.X.GetValue(t);
                    var py = seg.Y.GetValue(t);
                    double dx = px - wp.X, dy = py - wp.Y;
                    var sq = dx * dx + dy * dy;
                    if (sq < bestSq) bestSq = sq;
                }
            }
            total += bestSq;
        }
        return total;
    }

    /// <summary>
    /// Sample curvature (max abs) and integrated bending energy (integral kappa^2 * speed) using Gauss quadrature via provided RationalBernsteinPolynomial utilities.
    /// Returns (maxAbsKappa, integratedEnergy).
    /// </summary>
    public static (double maxAbsKappa, double integratedEnergy) SampleCurvatureAndEnergy(PhBernsteinPolyCurve2D multi, PhOptimizerOptions opts)
    {
        var maxK = 0.0;
        var energy = 0.0;

        for (var s = 0; s < multi.Count; s++)
        {
            var seg = multi[s];

            // use sampler to generate an approximate arc-length and curvature; but we can compute curvature formula:
            // kappa = (x' y'' - y' x'') / ( (x'^2 + y'^2)^(3/2) )
            // x',y', x'',y'' are rational/Bernstein expressions but we can sample numerically.

            var samples = Math.Max(16, opts.SamplesPerSegment);
            for (var i = 0; i <= samples; i++)
            {
                var t = i / (double)samples;
                var xp = seg.XDt.GetValue(t);
                var yp = seg.YDt.GetValue(t);
                var xpp = seg.XDt2.GetValue(t);
                var ypp = seg.YDt2.GetValue(t);

                var denom = Math.Pow(xp * xp + yp * yp, 1.5);
                var k = 0.0;

                if (denom > 1e-14) 
                    k = (xp * ypp - yp * xpp) / denom;

                maxK = Math.Max(maxK, Math.Abs(k));

                // approximate energy integrand: k^2 * |r'(t)|
                var speed = Math.Sqrt(xp * xp + yp * yp);
                energy += k * k * speed / samples;
            }
        }

        return (maxK, energy);
    }
}