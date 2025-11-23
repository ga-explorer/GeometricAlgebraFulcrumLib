using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.Optimizers;

public static class PhGenomeMapper
{
    /// <summary>
    /// Map genome (u,v,w coefficients stacked per segment) into PhMultiSegmentCurve2D.
    /// Expects genome length = segments * 3 * (degree + 1)
    /// If enforceG2=true will call PhGenomeG2Repair.EnforceG2 before mapping.
    /// </summary>
    public static PhBernsteinPolyCurve2D GenomeToPolyCurve(double[] genome, int segments, int degree, bool enforceG2 = true)
    {
        if (genome == null) 
            throw new ArgumentNullException(nameof(genome));

        var block = 3 * (degree + 1);
        if (genome.Length != segments * block)
            throw new ArgumentException("Genome length mismatch.");

        var working = (double[])genome.Clone();

        if (enforceG2)
        {
            working = 
                PhGenomeG2Repair.EnforceG2(
                    working, 
                    segments, 
                    degree, 
                    maxItersPerJoin: 6
                );
        }

        var multi = new List<PhBernsteinCurve2D>(segments);

        for (var s = 0; s < segments; s++)
        {
            var baseIdx = s * block;
            var uCoeffs = new double[degree + 1];
            var vCoeffs = new double[degree + 1];
            var wCoeffs = new double[degree + 1];

            for (var i = 0; i <= degree; i++)
            {
                uCoeffs[i] = working[baseIdx + i];
                vCoeffs[i] = working[baseIdx + degree + 1 + i];
                wCoeffs[i] = working[baseIdx + 2 * (degree + 1) + i];
            }

            var u = BernsteinPolynomial.Create(uCoeffs);
            var v = BernsteinPolynomial.Create(vCoeffs);
            var w = BernsteinPolynomial.Create(wCoeffs);

            var generators = new PhBernsteinCurve2D.GeneratorData
            {
                U = u,
                V = v,
                W = w, 
                P0 = LinFloat64Vector2D.Zero, 
                S0 = 0
            };

            var seg = new PhBernsteinCurve2D(generators, false);
            multi.Add(seg);
        }

        return new PhBernsteinPolyCurve2D(multi, false);
    }
}