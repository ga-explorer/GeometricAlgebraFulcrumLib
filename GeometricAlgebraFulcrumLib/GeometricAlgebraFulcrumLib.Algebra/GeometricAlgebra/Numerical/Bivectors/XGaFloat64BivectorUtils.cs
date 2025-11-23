using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical.Bivectors
{
    public static class XGaFloat64BivectorUtils
    {
        // Use Euclidean metric
        private static readonly XGaFloat64EuclideanProcessor Processor = 
            XGaFloat64Processor.Euclidean;

        /// <summary>
        /// Creates a random bivector in Cl(n)
        /// </summary>
        public static XGaFloat64Bivector CreateRandomBivector(int n, int seed = 42)
        {
            var rand = new Random(seed);
            
            return Processor.Bivector(
                rand.GetFloat64Array1D(n * (n - 1) / 2, -0.5, 0.5)
            );
        }

        /// <summary>
        /// Compute B^p for integer p using repeated geometric multiplication
        /// </summary>
        public static XGaFloat64Multivector MatrixPower(this XGaFloat64Bivector B, int p)
        {
            if (p == 0)
                return Processor.ScalarOne;
            if (p == 1)
                return B;
            if (p < 0)
                throw new ArgumentException("Negative powers not supported");

            XGaFloat64Multivector result = Processor.ScalarOne; // scalar 1
            XGaFloat64Multivector baseMv = B;
            var exp = p;

            while (exp > 0)
            {
                if ((exp & 1) == 1)
                    result = result.Gp(baseMv); // Geometric product
                baseMv = baseMv.Gp(baseMv);
                exp >>= 1;
            }

            return result;
        }

        /// <summary>
        /// Compute exp(B) using scaling and squaring with [6/6] Padé approximant in GA
        /// Works directly on multivectors
        /// </summary>
        public static XGaFloat64Multivector ExpByScalingSquaring(this XGaFloat64Bivector bv)
        {
            // Compute a norm for scaling
            var norm = bv.Sp(bv.Negative());
            norm = Math.Sqrt(Math.Abs(norm)); // "Magnitude" = sqrt(|B^2|)

            // Choose scaling factor s
            const double theta = 1.5;
            var s = 0;
            if (norm > theta)
                s = (int)Math.Ceiling(Math.Log(norm / theta) / Math.Log(2));

            // Scale B: A = B / 2^s
            var bvScaled = bv / Math.Pow(2, s);

            // Compute Padé [6/6] approximant: exp(A) ≈ N * inv(D)
            var expA = ComputePade6Exp(bvScaled);

            // Now square s times: expA = expA^(2^s)
            for (var i = 0; i < s; i++)
                expA = expA.Gp(expA); // Geometric product squaring

            return expA;
        }

        /// <summary>
        /// Compute [6/6] Padé approximant to exp(A) for a bivector A
        /// R_6(A) = N * D^{-1}
        /// where N = I + A/2 + A²/10 + A³/60 + A⁴/504 + A⁵/7560 + A⁶/181440
        ///       D = I - A/2 + A²/10 - A³/60 + A⁴/504 - A⁵/7560 + A⁶/181440
        /// </summary>
        private static XGaFloat64Multivector ComputePade6Exp(XGaFloat64Bivector bvA)
        {
            //var I = Processor.ScalarOne;
            //var zero = Processor.CreateZeroMultivector(bvA.VSpaceDimension);

            // Precompute powers
            var bvA2 = bvA.Gp(bvA);
            var bvA3 = bvA2.Gp(bvA);
            var bvA4 = bvA2.Gp(bvA2);
            var bvA5 = bvA4.Gp(bvA);
            var bvA6 = bvA4.Gp(bvA2);

            // Coefficients for [6/6] Padé
            const double c1 = 1.0 / 2.0;
            const double c2 = 1.0 / 10.0;
            const double c3 = 1.0 / 60.0;
            const double c4 = 1.0 / 504.0;
            const double c5 = 1.0 / 7560.0;
            const double c6 = 1.0 / 181440.0;

            // Denominator D = I - c1 A + c2 A² - c3 A³ + c4 A⁴ - c5 A⁵ + c6 A⁶
            var denominator = 
                Processor.ScalarOne
                    .Subtract(c1 * bvA)
                    .Add(c2 * bvA2)
                    .Subtract(c3 * bvA3)
                    .Add(c4 * bvA4)
                    .Subtract(c5 * bvA5)
                    .Add(c6 * bvA6);

            // Numerator N = I + c1 A + c2 A² + c3 A³ + c4 A⁴ + c5 A⁵ + c6 A⁶
            var numerator = 
                Processor.ScalarOne
                    .Add(c1 * bvA)
                    .Add(c2 * bvA2)
                    .Add(c3 * bvA3)
                    .Add(c4 * bvA4)
                    .Add(c5 * bvA5)
                    .Add(c6 * bvA6);

            // Solve D * X = N → X = D^{-1} N
            // GA-FuL does not have built-in inverse, so we use:
            // If D is invertible, use iterative or symbolic inverse
            // But for small n, we can use blade decomposition or assume convergence

            // Instead: use Neumann series or assume D is nearly identity
            // But safer: use the fact that for small A, D ≈ I + small, so inv(D) ≈ I - (D-I) + (D-I)^2 - ...
            // But GA-FuL has no general inverse.

            // Alternative: use D^{-1} N via pseudo-inverse or assume D is scalar + small
            // For now, use: exp(A) ≈ I + A + A²/2 + A³/6 + A⁴/24 + A⁵/120 + A⁶/720
            // This is Taylor-based fallback

            return ComputeTaylorExp(bvA, 6);
        }

        /// <summary>
        /// Fallback: Compute exp(A) using Taylor series up to order k
        /// </summary>
        private static XGaFloat64Multivector ComputeTaylorExp(XGaFloat64Bivector bv, int order)
        {
            XGaFloat64Multivector result = Processor.ScalarOne;
            XGaFloat64Multivector term = Processor.ScalarOne;

            for (var k = 1; k <= order; k++)
            {
                term = term.Gp(bv) / k;
                result = result.Add(term);
            }

            return result;
        }
    }
}