using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.Optimizers;

public static class PhGenomeInitializer
{
    /// <summary>
    /// Build an initial genome from polyline waypoints.
    /// Creates one PH segment per polyline edge.
    /// Works for any degree >= 1.
    /// </summary>
    public static double[] FromWaypoints(List<LinFloat64Vector2D> waypoints, int degree, GenomeInitMode mode = GenomeInitMode.Spread)
    {
        if (waypoints.Count < 2)
            throw new ArgumentException("Need at least 2 waypoints.");

        if (degree < 1)
            throw new ArgumentException("Degree must be >= 1");

        var segmentCount = waypoints.Count - 1;
        var blockSize = 3 * (degree + 1);
        var genome = new double[segmentCount * blockSize];

        for (var s = 0; s < segmentCount; s++)
        {
            var wp0 = waypoints[s];
            var wp1 = waypoints[s + 1];

            var dp = wp1 - wp0;
            var len = dp.L2Norm();
            var (nx, ny) = dp / (dp.L2Norm() + 1e-12);

            //var (x0, y0) = waypoints[s];
            //var (x1, y1) = waypoints[s + 1];
            //var dx = x1 - x0;
            //var dy = y1 - y0;
            //var len = Math.Sqrt(dx * dx + dy * dy);

            // Normalize direction
            //var nx = dx / (len + 1e-12);
            //var ny = dy / (len + 1e-12);

            // PH encoding via complex sqrt
            var angle = Math.Atan2(ny, nx) / 2.0;
            var mag = Math.Sqrt(Math.Sqrt(nx * nx + ny * ny));
            var uDir = mag * Math.Cos(angle);
            var vDir = mag * Math.Sin(angle);

            var baseIdx = s * blockSize;

            for (var i = 0; i <= degree; i++)
            {
                var uCoeff = uDir;
                var vCoeff = vDir;

                if (mode == GenomeInitMode.Spread)
                {
                    // [0,1]
                    var t = i / (double)degree;

                    // gentle variation
                    var bias = 1.0 + 0.2 * (t - 0.5); 

                    uCoeff *= bias;
                    vCoeff *= bias;
                }

                // u coefficients
                genome[baseIdx + i] = uCoeff;

                // v coefficients
                genome[baseIdx + degree + 1 + i] = vCoeff;

                // w coefficients
                genome[baseIdx + 2 * (degree + 1) + i] = len;
            }
        }

        return genome;
    }

    public static void Example()
    {
        var waypoints = new List<LinFloat64Vector2D> { (0, 0), (5, 3), (10, 0) };
        var degree = 3;

        // Flat initialization (rigid, polyline-like)
        var genomeFlat = FromWaypoints(waypoints, degree, GenomeInitMode.Flat);

        // Spread initialization (more flexible)
        var genomeSpread = FromWaypoints(waypoints, degree, GenomeInitMode.Spread);

        // Map to PH curve
        var curveFlat = PhGenomeMapper.GenomeToPolyCurve(genomeFlat, waypoints.Count - 1, degree, enforceG2: true);
        var curveSpread = PhGenomeMapper.GenomeToPolyCurve(genomeSpread, waypoints.Count - 1, degree, enforceG2: true);

        Console.WriteLine("Flat curve length ≈ " + curveFlat.GetArcLength());
        Console.WriteLine("Spread curve length ≈ " + curveSpread.GetArcLength());
    }
}