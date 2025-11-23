using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical.Bivectors
{
    /// <summary>
    /// Efficient bivector exponential for Cl(4,0,0)
    /// Uses self-dual / anti-self-dual decomposition into two quaternions
    /// </summary>
    public static class Cl400BivectorExp
    {
        private static readonly XGaFloat64EuclideanProcessor Processor = 
            XGaFloat64Processor.Euclidean;


        // Hodge dual map: ⋆(e_ij) = ± e_kl where {i,j,k,l} = {1,2,3,4}
        // ⋆e12 = e34, ⋆e13 = -e24, ⋆e14 = e23
        // ⋆e23 = e14, ⋆e24 = -e13, ⋆e34 = e12
        //private static readonly int[] DualMapIndex = { 5, 4, 3, 2, 1, 0 }; // index in BladeIndices
        //private static readonly double[] DualSigns = { 1, -1, 1, 1, -1, 1 }; // ⋆e12 = +e34, ⋆e13 = -e24, etc.

        /// <summary>
        /// Compute exp(B) for a bivector in Cl(4,0,0)
        /// Uses self-dual decomposition: B = B⁺ + B⁻, then exp(B) = exp(B⁺)exp(B⁻)
        /// Each exp(B±) is a rotor like a quaternion
        /// </summary>
        public static XGaFloat64Multivector Exp(this XGaFloat64Bivector bv)
        {
            if (bv.VSpaceDimensions > 4)
                throw new ArgumentException("Bivector must be in 4D space.");

            // Compute self-dual (B⁺) and anti-self-dual (B⁻) parts
            var bvPlus = DenseVector.Create(3, 0);  // B⁺ = (b12 + b34, b13 - b24, b14 + b23)
            var bvMinus = DenseVector.Create(3, 0); // B⁻ = (b12 - b34, b13 + b24, b14 - b23)

            bvPlus[0] = bv[0, 1] + bv[2, 3];     // b12 + b34
            bvPlus[1] = bv[0, 2] - bv[1, 3];     // b13 - b24
            bvPlus[2] = bv[0, 3] + bv[1, 2];     // b14 + b23

            bvMinus[0] = bv[0, 1] - bv[2, 3];    // b12 - b34
            bvMinus[1] = bv[0, 2] + bv[1, 3];    // b13 + b24
            bvMinus[2] = bv[0, 3] - bv[1, 2];    // b14 - b23

            // Normalize to get rotation angles
            var normPlus = bvPlus.L2Norm();
            var normMinus = bvMinus.L2Norm();

            // Compute exp(B⁺) and exp(B⁻) as rotors
            var rPlus = normPlus < 1e-15
                ? Processor.ScalarOne
                : CreateRotorFromBivectorPart(bvPlus, normPlus);

            var rMinus = normMinus < 1e-15
                ? Processor.ScalarOne
                : CreateRotorFromBivectorPart(bvMinus, normMinus);

            // R = R+ * R-
            return rPlus.Gp(rMinus);
        }

        /// <summary>
        /// Create rotor from 3D-like bivector part: exp(-B/2) but here we do exp(B)
        /// Actually: exp(B) = cos|B| + B/|B| sin|B|
        /// But in GA, rotor for rotation by θ is exp(-B θ/2), but here B already contains magnitude
        /// So: exp(B) = cos(||B||) + (B/||B||) sin(||B||)
        /// </summary>
        private static XGaFloat64Multivector CreateRotorFromBivectorPart(Vector<double> b, double norm)
        {
            var cosTheta = Math.Cos(norm);
            var sinTheta = Math.Sin(norm);

            // Normalize bivector direction
            double[] u = [
                b[0] * sinTheta / norm, 
                b[1] * sinTheta / norm, 
                b[2] * sinTheta / norm
            ];

            // Reconstruct bivector in Cl(4,0,0)
            // The mapping: u[0] → e12, u[1] → e13, u[2] → e14  (for B⁺ or B⁻, but we must map back to full bivector)
            // But B⁺ and B⁻ are abstract 3D bivectors — we need to reconstruct their full form

            var rotorComposer = Processor.CreateMultivectorComposer(); 
            
            rotorComposer.SetScalarTerm(cosTheta);

            // Add bivector part: sinTheta * (u0*e12 + u1*e13 + u2*e14) — but this is only for one part!
            // Actually, B⁺ and B⁻ span different planes.

            // Instead: we reconstruct the full bivector corresponding to this 3D vector
            // For B⁺: (p1, p2, p3) ↔ p1*e12 + p2*e13 + p3*e14 + p1*e34 - p2*e24 + p3*e23
            // But easier: use the inverse decomposition

            // We'll reconstruct the full bivector from (u0, u1, u2) interpreted as B⁺ or B⁻ coefficients
            // For a self-dual-like bivector with coefficients (u0, u1, u2):
            // B = u0*e12 + u1*e13 + u2*e14 + u0*e34 - u1*e24 + u2*e23
            rotorComposer.SetBivectorTerm(0, 1, u[0]);
            rotorComposer.SetBivectorTerm(0, 2, u[1]);
            rotorComposer.SetBivectorTerm(0, 3, u[2]);
            rotorComposer.SetBivectorTerm(2, 3, u[0]);
            rotorComposer.SetBivectorTerm(1, 3, -u[1]);
            rotorComposer.SetBivectorTerm(1, 2, u[2]);

            return rotorComposer.GetMultivector();
        }

        /// <summary>
        /// Compute B^p for integer p
        /// </summary>
        public static XGaFloat64Multivector Power(this XGaFloat64Bivector bv, int p)
        {
            if (p == 0) return Processor.ScalarOne;
            if (p == 1) return bv;
            if (p < 0) throw new ArgumentException("Negative powers not supported");

            XGaFloat64Multivector result = Processor.ScalarOne;
            XGaFloat64Multivector baseMv = bv;
            var exp = p;

            while (exp > 0)
            {
                if ((exp & 1) == 1)
                    result = result.Gp(baseMv);
                baseMv = baseMv.Gp(baseMv);
                exp >>= 1;
            }

            return result;
        }
    }
}