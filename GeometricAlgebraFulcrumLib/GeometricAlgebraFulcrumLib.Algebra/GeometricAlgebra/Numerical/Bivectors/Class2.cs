//using System;
//using System.Linq;
//using MathNet.Numerics;
//using MathNet.Numerics.LinearAlgebra;
//using MathNet.Numerics.LinearAlgebra.Double;
//using Complex = System.Numerics.Complex;

//public class BivectorRotator
//{
//    // Converts a bivector (given as skew-symmetric matrix) to a 6D vector [xy, xz, xw, yz, yw, zw]
//    public static double[] BivectorTo6D(Matrix<double> B)
//    {
//        return new double[]
//        {
//            B[0,1], // xy
//            B[0,2], // xz
//            B[0,3], // xw
//            B[1,2], // yz
//            B[1,3], // yw
//            B[2,3]  // zw
//        };
//    }

//    // Converts 6D bivector back to 4x4 skew-symmetric matrix
//    public static Matrix<double> BivectorFrom6D(double[] b)
//    {
//        var B = Matrix<double>.Build.Dense(4, 4);
//        B[0,1] = b[0]; B[1,0] = -b[0];
//        B[0,2] = b[1]; B[2,0] = -b[1];
//        B[0,3] = b[2]; B[3,0] = -b[2];
//        B[1,2] = b[3]; B[2,1] = -b[3];
//        B[1,3] = b[4]; B[3,1] = -b[4];
//        B[2,3] = b[5]; B[3,2] = -b[5];
//        return B;
//    }

//    // Normalize bivector (as skew-symmetric matrix) to unit norm
//    public static Matrix<double> NormalizeBivector(Matrix<double> B)
//    {
//        var b = BivectorTo6D(B);
//        var norm = Math.Sqrt(b.Sum(x => x * x));
//        if (norm < 1e-10) throw new ArgumentException("Bivector is zero");
//        return BivectorFrom6D(b.Select(x => x / norm).ToArray());
//    }

//    // Extract orthonormal basis for the plane of a simple bivector via SVD
//    public static Matrix<double> GetPlaneBasis(Matrix<double> B)
//    {
//        // B is skew-symmetric; its nonzero eigenvalues are ±iθ, and real SVD gives 2-plane
//        var svd = B.Svd();
//        var U = svd.U;
//        // Take first two left singular vectors (they span the plane if B is simple and rank 2)
//        var basis = Matrix<double>.Build.Dense(4, 2);
//        for (int i = 0; i < 4; i++)
//        {
//            basis[i, 0] = U[i, 0];
//            basis[i, 1] = U[i, 1];
//        }
//        // Ensure right-handed orientation? Not critical; we'll fix via Procrustes
//        return basis;
//    }

//    // Compute optimal rotation from frame A to frame B (3D/4D Procrustes)
//    public static Matrix<double> ProcrustesRotation(Matrix<double> A, Matrix<double> B)
//    {
//        // A and B are 4x2 matrices: orthonormal bases of two planes
//        // Compute 4x4 rotation (or closest) that maps A to B
//        var M = A * B.Transpose(); // 4x4
//        var svd = M.Svd(true);
//        var U = svd.U;
//        var V = svd.VT.Transpose();
//        var R = V * U.Transpose();

//        // Ensure det(R) = 1
//        var det = R.Determinant();
//        if (det < 0)
//        {
//            var Umod = U.Clone();
//            Umod.SetColumn(3, -U.Column(3)); // flip last column
//            R = V * Umod.Transpose();
//        }
//        return R;
//    }

//    // Decompose SO(4) rotation into two commuting simple rotations
//    // Using the fact: SO(4) = (SU(2)×SU(2))/Z2 → any rotation is R = R1 R2 with R1, R2 simple and commuting
//    // We use bivector decomposition of the rotation generator
//    public static (Matrix<double> R1, Matrix<double> R2) DecomposeIntoSimpleRotations(Matrix<double> R)
//    {
//        // Step: Compute log(R) → skew-symmetric matrix (bivector)
//        var logR = MatrixLogSO4(R);

//        // Step: Diagonalize the bivector (in 4D, skew-symmetric matrices can be block-diagonalized)
//        // Find two orthogonal planes of rotation
//        var (B1, B2, theta1, theta2) = ExtractRotationPlanes(logR);

//        // Exponentiate each simple bivector separately
//        var R1 = MatrixExpSkew(B1 * theta1);
//        var R2 = MatrixExpSkew(B2 * theta2);

//        // Since they commute, R ≈ R2 * R1 (order may vary)
//        return (R1, R2);
//    }

//    // Compute matrix logarithm of SO(4) matrix (returns skew-symmetric)
//    public static Matrix<double> MatrixLogSO4(Matrix<double> R)
//    {
//        // Use eigen-decomposition
//        var evd = R.ToComplex().Evd();
//        var D = evd.Determinant;
//        var V = evd.EigenVectors;
//        var Lambda = evd.EigenValues;

//        var logLambda = Vector<Complex>.Build.Dense(4);
//        for (int i = 0; i < 4; i++)
//        {
//            logLambda[i] = Complex.Log(Lambda[i]);
//        }

//        // Reconstruct log(R) = V * log(Λ) * V⁻¹
//        var logR_complex = V * (Vector<Complex>.Build.DiagonalMatrix(logLambda) * V.Inverse());
//        // Take imaginary part? Or convert to real skew-symmetric
//        // Better: use real decomposition into 2D blocks

//        // Alternative: use closed-form for skew-symmetric log
//        // We use the formula for SO(n) logarithm
//        // For 4D, we find the bivector via angle extraction

//        // Instead, we use: log(R) = 0.5*(R - R^T) when small, but generally:
//        // Use Rodrigues-like formula in 4D via skew extraction and scaling

//        // Simpler: return an approximate log via skew-symmetric part scaled by angle
//        var skew = (R - R.Transpose()) * 0.5;
//        var angles = ExtractRotationAngles(R);
//        var totalAngle = (angles.Item1 + angles.Item2) / 2.0; // rough scaling

//        // This is approximate; for accuracy, use proper Lie algebra mapping
//        // But for now, we use the skew matrix as generator
//        return skew; // Not normalized — we’ll recompute angles
//    }

//    // Extract two rotation angles and planes from SO(4) matrix
//    public static (Matrix<double> plane1, Matrix<double> plane2, double angle1, double angle2) ExtractRotationPlanes(Matrix<double> logR)
//    {
//        // logR is skew-symmetric → diagonalize via block decomposition
//        var b = BivectorTo6D(logR);
//        var norm = Math.Sqrt(b.Select(x => x * x).Sum());
//        if (norm < 1e-10)
//            return (Matrix<double>.Build.Dense(4,4), Matrix<double>.Build.Dense(4,4), 0, 0);

//        // Normalize to get unit bivector direction
//        var b_unit = b.Select(x => x / norm).ToArray();
//        var B_unit = BivectorFrom6D(b_unit);

//        // In 4D, a generic skew-symmetric matrix has two orthogonal eigenplanes
//        // We compute the two simple bivectors via SVD or eigen-decomposition

//        // Use: the matrix B_unit can be block-diagonalized by orthogonal transformation
//        // We compute its action and find invariant planes

//        // Approximate: use SVD of B_unit to get dominant plane
//        var svd = B_unit.Svd();
//        var U = svd.U;

//        // First rotation plane: span of first two columns of U
//        var basis1 = U.SubMatrix(0, 4, 0, 2);
//        var proj1 = basis1 * basis1.Transpose(); // projector

//        // Orthogonal complement
//        var I = Matrix<double>.Build.DenseIdentity(4);
//        var residual = (I - proj1) * B_unit * (I - proj1);
//        var svd2 = residual.Svd();
//        var U2 = svd2.U;
//        var basis2 = U2.SubMatrix(0, 4, 0, 2); // second plane

//        // Normalize planes
//        var B1 = basis1 * basis1.Transpose(); // rough bivector proxy
//        var B2 = basis2 * basis2.Transpose();

//        // Extract angles from original R
//        var (theta1, theta2) = ExtractRotationAngles(R);

//        // Convert basis1 to actual bivector (via wedge of orthonormal cols)
//        var q1 = basis1.Column(0).Normalize(2);
//        var q2 = basis1.Column(1).Normalize(2);
//        var angle1 = Vector<double>.Angle(q1, basis1.Column(1)); // not reliable

//        // Better: use angle extraction
//        (theta1, theta2) = ExtractRotationAngles(R);

//        // Construct bivectors from orthonormal pairs
//        var biv1 = WedgeToSkew(q1, q2);
//        var biv2 = WedgeToSkew(basis2.Column(0).Normalize(2), basis2.Column(1).Normalize(2));

//        return (biv1, biv2, theta1, theta2);
//    }

//    // Compute rotation angles from SO(4) matrix
//    public static (double, double) ExtractRotationAngles(Matrix<double> R)
//    {
//        // Eigenvalues of SO(4) matrix are e^{±iθ1}, e^{±iθ2}
//        var evd = R.ToComplex().Evd();
//        var angles = evd.EigenValues
//            .Where(ev => ev.Imaginary > 1e-10)
//            .Select(ev => Math.Atan2(ev.Imaginary, ev.Real))
//            .OrderByDescending(x => x)
//            .ToArray();

//        if (angles.Length < 2) return (0, 0);
//        return (angles[0], angles[1]);
//    }

//    // Exponentiate a skew-symmetric matrix (simple rotation)
//    public static Matrix<double> MatrixExpSkew(Matrix<double> B)
//    {
//        var b = BivectorTo6D(B);
//        var norm = Math.Sqrt(b.Select(x => x * x).Sum());
//        if (norm < 1e-10) return Matrix<double>.Build.DenseIdentity(4);

//        var axis = b.Select(x => x / norm).ToArray();
//        var B_unit = BivectorFrom6D(axis);

//        // Rodrigues in 4D: for simple rotation, exp(theta * B_unit)
//        // But 4D Rodrigues is more complex. We use: exp(B) = I + sinθ B_unit + (1 - cosθ)(B_unit²)
//        // But only if B_unit² has known structure

//        // Instead, use: if B_unit is rank-2 skew, then exp(B) = I + sinθ B_unit + (1 - cosθ) B_unit²
//        // But B_unit² is symmetric negative semi-definite

//        var theta = norm;
//        var B_sq = B_unit * B_unit;

//        return Matrix<double>.Build.DenseIdentity(4)
//             + Math.Sin(theta) * B_unit
//             + (1 - Math.Cos(theta)) * B_sq;
//    }

//    // Create skew-symmetric matrix from u ∧ v
//    public static Matrix<double> WedgeToSkew(Vector<double> u, Vector<double> v)
//    {
//        return u.ToRowMatrix().Transpose() * v.ToRowMatrix()
//             - v.ToRowMatrix().Transpose() * u.ToRowMatrix();
//    }

//    // MAIN METHOD: Compute R1, R2 such that R2*R1 rotates B1 to B2 via geodesic
//    public static (Matrix<double> R1, Matrix<double> R2) RotateBivector(
//        Matrix<double> B1, Matrix<double> B2)
//    {
//        // Normalize inputs
//        B1 = NormalizeBivector(B1);
//        B2 = NormalizeBivector(B2);

//        // Get orthonormal bases for the planes
//        var frame1 = GetPlaneBasis(B1);
//        var frame2 = GetPlaneBasis(B2);

//        // Compute Procrustes rotation from frame1 to frame2
//        var R_geodesic = ProcrustesRotation(frame1, frame2);

//        // Decompose R_geodesic into two simple rotations
//        var (R1, R2) = DecomposeIntoSimpleRotations(R_geodesic);

//        // Return R1 then R2 so that R = R2 * R1
//        return (R1, R2);
//    }
//}