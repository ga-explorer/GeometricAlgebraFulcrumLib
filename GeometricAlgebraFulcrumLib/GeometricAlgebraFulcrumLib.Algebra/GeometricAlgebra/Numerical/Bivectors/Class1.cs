using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical.Bivectors
{
    public class Bivector
    {
        public double E12, E13, E14, E23, E24, E34;

        public Bivector(double e12, double e13, double e14, double e23, double e24, double e34)
        {
            (E12, E13, E14, E23, E24, E34) = (e12, e13, e14, e23, e24, e34);
            Normalize();
        }

        public void Normalize()
        {
            var norm = Norm();
            if (!(norm > 1e-12)) return;

            E12 /= norm; 
            E13 /= norm; 
            E14 /= norm; 
            E23 /= norm; 
            E24 /= norm; 
            E34 /= norm;
        }

        public double Norm() => Math.Sqrt(E12*E12 + E13*E13 + E14*E14 + E23*E23 + E24*E24 + E34*E34);

        public static Bivector FromRotationPlane(Vector<double> u, Vector<double> v)
        {
            var b = new Bivector(0,0,0,0,0,0);
            var uv = u.ToColumnMatrix() * v.ToRowMatrix() - v.ToColumnMatrix() * u.ToRowMatrix();
            b.E12 = uv[0,1]; b.E13 = uv[0,2]; b.E14 = uv[0,3];
            b.E23 = uv[1,2]; b.E24 = uv[1,3]; b.E34 = uv[2,3];
            b.Normalize();
            return b;
        }

        public Matrix<double> ToSkewSymmetric()
        {
            var m = DenseMatrix.Create(4, 4, 0.0);

            m[0,1] = E12; m[1,0] = -E12;
            m[0,2] = E13; m[2,0] = -E13;
            m[0,3] = E14; m[3,0] = -E14;
            m[1,2] = E23; m[2,1] = -E23;
            m[1,3] = E24; m[3,1] = -E24;
            m[2,3] = E34; m[3,2] = -E34;

            return m;
        }

        public static Bivector FromSkewSymmetric(Matrix<double> m)
        {
            return new Bivector(m[0,1], m[0,2], m[0,3], m[1,2], m[1,3], m[2,3]);
        }

        public override string ToString() => $"({E12:F4}, {E13:F4}, {E14:F4}, {E23:F4}, {E24:F4}, {E34:F4})";
    }

    public class SimpleRotation
    {
        public Vector<double> U { get; }
        public Vector<double> V { get; }
        public double Angle { get; }

        public SimpleRotation(Vector<double> u, Vector<double> v, double angle)
        {
            U = u.Normalize(2);
            V = v.Normalize(2);
            Angle = angle;
            // Ensure orthonormal
            if (Math.Abs(U.DotProduct(V)) > 1e-10)
                throw new ArgumentException("Rotation plane vectors must be orthogonal.");
        }

        public Matrix<double> ToMatrix()
        {
            var r = DenseMatrix.CreateIdentity(4);
            double c = Math.Cos(Angle), s = Math.Sin(Angle);
            var planeBasis = BuildOrthonormalBasis(U, V);

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    double val = 0;
                    for (var a = 0; a < 2; a++)
                    for (var b = 0; b < 2; b++)
                    {
                        var rot = a == b ? c : a == 0 && b == 1 ? s : -s;
                        val += rot * planeBasis.Column(a)[i] * planeBasis.Column(b)[j];
                    }
                    r[i, j] += val - (i == j ? 1 : 0); // Replace identity part
                }
            }
            return r;
        }

        private Matrix<double> BuildOrthonormalBasis(Vector<double> u, Vector<double> v)
        {
            var uN = u.Normalize(2);
            var vN = v - u * u.DotProduct(v) / u.DotProduct(u);
            vN = vN.Normalize(2);
            var basis = DenseMatrix.Create(4, 2, 0.0);
            basis.SetColumn(0, uN);
            basis.SetColumn(1, vN);
            return basis;
        }
    }

    public class GeodesicRotationComposer
    {
        public SimpleRotation R1 { get; private set; }

        public SimpleRotation R2 { get; private set; }

        public Matrix<double> TotalRotation { get; private set; }


        public bool Decompose(Bivector b1, Bivector b2, out string error)
        {
            error = string.Empty;

            try
            {
                if (b1.Norm() < 1e-12 || b2.Norm() < 1e-12)
                {
                    error = "Bivectors must be non-zero.";
                    return false;
                }

                b1.Normalize(); 
                b2.Normalize();

                // Handle special cases
                var dot = 
                    b1.E12*b2.E12 + 
                    b1.E13*b2.E13 + 
                    b1.E14*b2.E14 +
                    b1.E23*b2.E23 + 
                    b1.E24*b2.E24 + 
                    b1.E34*b2.E34;

                if (Math.Abs(dot - 1.0) < 1e-10)
                {
                    // B1 == B2 → identity rotation
                    R1 = new SimpleRotation(
                        Vector<double>.Build.DenseOfArray([1,0,0,0]),
                        Vector<double>.Build.DenseOfArray([0,1,0,0]), 
                        0
                    );

                    R2 = new SimpleRotation(
                        Vector<double>.Build.DenseOfArray([1,0,0,0]),
                        Vector<double>.Build.DenseOfArray([0,0,1,0]), 
                        0
                    );

                    TotalRotation = DenseMatrix.CreateIdentity(4);

                    return true;
                }

                if (Math.Abs(dot + 1.0) < 1e-10)
                {
                    // B1 = -B2 → need 180° rotation in B1's plane
                    var b1Mat = b1.ToSkewSymmetric();
                    var (u, v) = ExtractPlaneFromSkew(b1Mat);
                    R1 = new SimpleRotation(u, v, Math.PI);
                    R2 = new SimpleRotation(Vector<double>.Build.DenseOfArray([1,0,0,0]),
                                            Vector<double>.Build.DenseOfArray([0,1,0,0]), 0);
                    TotalRotation = R1.ToMatrix();
                    return true;
                }

                // General case: geodesic in SO(4)
                var omega1 = b1.ToSkewSymmetric();
                var omega2 = b2.ToSkewSymmetric();

                // The generator of geodesic rotation: log map from B1 to B2
                // We seek rotation R such that R * B1 * R^T = B2 (in matrix form)
                // The generator is proportional to Omega2 - proj_{B1}(Omega2), but better to compute via eigen-decomposition

                // Compute relative rotation generator
                // Use: exp(θ J) B1 exp(-θ J) = B2 ⇒ J generates rotation from B1 to B2
                // We approximate J via skew-symmetric part of log(B2 B1†) but in bivector space

                // Instead: use the fact that in 4D, the rotation from B1 to B2 lies in SO(4), and its generator
                // is a bivector that commutes appropriately.

                // Alternative: compute the rotation matrix that maps the plane of B1 to B2

                var (u1, v1) = ExtractPlaneFromSkew(b1.ToSkewSymmetric());
                var (u2, v2) = ExtractPlaneFromSkew(b2.ToSkewSymmetric());

                // Build rotation from plane1 to plane2
                var rDirect = FindRotationBetweenPlanes(u1, v1, u2, v2);

                // Now decompose R_direct ∈ SO(4) into two simple rotations in orthogonal planes
                if (!DecomposeSO4Rotation(rDirect, out var rot1, out var rot2, out error))
                    return false;

                R1 = rot1;
                R2 = rot2;
                TotalRotation = R2.ToMatrix() * R1.ToMatrix();

                return true;
            }
            catch (Exception ex)
            {
                error = "Exception in decomposition: " + ex.Message;
                return false;
            }
        }

        private static (Vector<double>, Vector<double>) ExtractPlaneFromSkew(Matrix<double> M)
        {
            var svd = M.Svd();
            var U = svd.U;
            var singularVals = svd.S;

            // Find largest singular value → dominant plane
            var maxIdx = 0;
            for (var i = 1; i < 4; i++)
                if (singularVals[i] > singularVals[maxIdx]) maxIdx = i;

            // Use first two columns corresponding to the largest singular value pair?
            // Actually: skew-symmetric matrices have eigenvalues ±iλ, ±iμ
            // But SVD gives us singular vectors

            // Take first two left singular vectors as plane basis
            var u = U.Column(maxIdx % 4); // rough
            var v = U.Column((maxIdx + 1) % 4);

            // Ensure orthogonality
            v = v - u * u.DotProduct(v);
            v = v.Normalize(2);
            u = u.Normalize(2);

            return (u, v);
        }

        private Matrix<double> FindRotationBetweenPlanes(
            Vector<double> u1, Vector<double> v1,
            Vector<double> u2, Vector<double> v2)
        {
            var from = DenseMatrix.OfColumnVectors(u1, v1);
            var to = DenseMatrix.OfColumnVectors(u2, v2);

            // QR to get orthonormal bases
            var fromQ = from.QR().Q;
            var toQ = to.QR().Q;

            // Complete to full 4D orthonormal basis
            var fullFrom = ExtendToOrthonormalBasis((DenseMatrix)fromQ);
            var fullTo = ExtendToOrthonormalBasis((DenseMatrix)toQ);

            // Rotation: R = fullTo * fullFrom^T
            return fullTo * fullFrom.Transpose();
        }

        private DenseMatrix ExtendToOrthonormalBasis(DenseMatrix twoBasis)
        {
            var basis = DenseMatrix.Create(4, 4, 0.0);
            basis.SetSubMatrix(0, 0, twoBasis);

            for (var i = 2; i < 4; i++)
            {
                var e = Vector<double>.Build.Dense(4); e[i - 2] = 1;
                var v = e;
                for (var j = 0; j < i; j++)
                {
                    var proj = basis.Column(j) * basis.Column(j).DotProduct(v);
                    v = v - proj;
                }
                if (v.L2Norm() < 1e-10) continue;
                v = v.Normalize(2);
                basis.SetColumn(i, v);
            }

            return (DenseMatrix)basis.QR().Q;
        }

        private bool DecomposeSO4Rotation(Matrix<double> r, out SimpleRotation R1, out SimpleRotation R2, out string error)
        {
            R1 = null; R2 = null; error = null;

            try
            {
                // Eigenvalues of SO(4) matrix come in complex conjugate pairs: e^{±iθ}, e^{±iφ}
                var eigen = r.ToComplex().Evd();
                var d = eigen.EigenValues;

                // Extract angles
                var angles = new List<double>();
                var planes = new List<(Vector<double>, Vector<double>)>();

                for (var i = 0; i < 4; i++)
                {
                    if (!(Math.Abs(d[i].Imaginary) > 1e-10)) continue;

                    var angle = Math.Atan2(d[i].Imaginary, d[i].Real);
                    angles.Add(angle);

                    // Real and imaginary parts of eigenvector give rotation plane
                    var vec = eigen.EigenVectors.Column(i);
                    var realPart = vec.Real().Normalize(2);
                    var imagPart = vec.Imaginary().Normalize(2);

                    // Orthogonalize
                    imagPart = imagPart - realPart * realPart.DotProduct(imagPart);
                    if (imagPart.L2Norm() > 1e-10) imagPart = imagPart.Normalize(2);

                    planes.Add((realPart, imagPart));

                    i++; // Skip conjugate
                }

                if (angles.Count != 2)
                {
                    error = $"Expected 2 rotation planes, found {angles.Count}";
                    return false;
                }

                // Ensure planes are orthogonal
                var ortho = planes[0].Item1.DotProduct(planes[1].Item1) +
                            planes[0].Item1.DotProduct(planes[1].Item2) +
                            planes[0].Item2.DotProduct(planes[1].Item1) +
                            planes[0].Item2.DotProduct(planes[1].Item2);

                if (Math.Abs(ortho) > 1e-2)
                {
                    error = "Rotation planes are not orthogonal.";
                    return false;
                }

                R1 = new SimpleRotation(planes[0].Item1, planes[0].Item2, angles[0]);
                R2 = new SimpleRotation(planes[1].Item1, planes[1].Item2, angles[1]);

                return true;
            }
            catch (Exception ex)
            {
                error = "Eigen-decomposition failed: " + ex.Message;
                return false;
            }
        }

        // Validation
        public bool Validate(Bivector b1, Bivector b2, out string msg)
        {
            msg = string.Empty;

            var r1Mat = R1.ToMatrix();
            var r2Mat = R2.ToMatrix();
            var r = r2Mat * r1Mat;

            // Check orthogonality
            var i = DenseMatrix.CreateIdentity(4);
            if ((r * r.Transpose() - i).L2Norm() > 1e-8)
            {
                msg = "Composite rotation is not orthogonal.";
                return false;
            }

            // Transform B1: R B1 R^T
            var b1Mat = b1.ToSkewSymmetric();
            var b1Rotated = r * b1Mat * r.Transpose();
            var b1RotVec = new Bivector(b1Rotated[0,1], b1Rotated[0,2], b1Rotated[0,3],
                                        b1Rotated[1,2], b1Rotated[1,3], b1Rotated[2,3]);

            var diff = (b1RotVec.E12 - b2.E12) * (b1RotVec.E12 - b2.E12) +
                       (b1RotVec.E13 - b2.E13) * (b1RotVec.E13 - b2.E13) +
                       (b1RotVec.E14 - b2.E14) * (b1RotVec.E14 - b2.E14) +
                       (b1RotVec.E23 - b2.E23) * (b1RotVec.E23 - b2.E23) +
                       (b1RotVec.E24 - b2.E24) * (b1RotVec.E24 - b2.E24) +
                       (b1RotVec.E34 - b2.E34) * (b1RotVec.E34 - b2.E34);

            if (diff > 1e-8)
            {
                msg = $"Rotation does not map B1 to B2: diff = {Math.Sqrt(diff)}";
                return false;
            }

            // Check geodesic distance
            var cosTheta = b1.E12*b2.E12 + b1.E13*b2.E13 + b1.E14*b2.E14 +
                           b1.E23*b2.E23 + b1.E24*b2.E24 + b1.E34*b2.E34;
            cosTheta = Math.Max(-1, Math.Min(1, cosTheta));
            var expectedAngle = Math.Acos(cosTheta);

            var totalAngle = Math.Abs(R1.Angle) + Math.Abs(R2.Angle);
            if (Math.Abs(totalAngle - expectedAngle) > 1e-8)
            {
                msg = $"Geodesic length mismatch: expected {expectedAngle}, got {totalAngle}";
                return false;
            }

            msg = "Validation passed.";
            return true;
        }
    }
}