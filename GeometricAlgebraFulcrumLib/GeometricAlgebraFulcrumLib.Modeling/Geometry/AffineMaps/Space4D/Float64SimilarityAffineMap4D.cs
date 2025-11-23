using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space4D
{
    public sealed record Float64SimilarityAffineMap4D
    {
        public static Float64SimilarityAffineMap4D Identity { get; }
            = new Float64SimilarityAffineMap4D
            {
                Translate = LinFloat64Vector4D.Zero,
                HasReflection = false,
                RotationAngle1 = 0.0,
                Plane1U = LinFloat64Vector4D.E1,
                Plane1V = LinFloat64Vector4D.E2,
                RotationAngle2 = 0.0,
                Plane2U = LinFloat64Vector4D.E3,
                Plane2V = LinFloat64Vector4D.E4,
                ScalingFactor = 1.0
            };

        //============================  Decomposition  ============================

        /// <summary>
        /// Decompose a 5x5 homogeneous similarity matrix into T * S * R * F and
        /// extract: uniform scale s, two plane rotations (plane bases + angles),
        /// reflection flag, translation.
        /// </summary>
        public static Float64SimilarityAffineMap4D Decompose(Matrix<double> matrix)
        {
            if (matrix.RowCount != 5 || matrix.ColumnCount != 5)
                throw new ArgumentException("Matrix must be 5x5.");

            const double epsScale = Float64Utils.DefaultTolerance;
            const double epsOrtho = Float64Utils.DefaultTolerance;

            // Check bottom row is [0 0 0 0 1]
            for (var j = 0; j < 4; j++)
            {
                if (Math.Abs(matrix[4, j]) > epsScale)
                    throw new InvalidOperationException("Matrix is not a proper 5x5 homogeneous affine transform");
            }

            if (Math.Abs(matrix[4, 4] - 1.0) > epsScale)
                throw new InvalidOperationException("Matrix is not a proper 5x5 homogeneous affine transform");

            // Translation
            var tx = matrix[0, 4];
            var ty = matrix[1, 4];
            var tz = matrix[2, 4];
            var tw = matrix[3, 4];
            var translate = LinFloat64Vector4D.Create(tx, ty, tz, tw);

            // Linear part A (4x4)
            var A = matrix.SubMatrix(0, 4, 0, 4).ToDense();

            // Compute uniform scale as Frobenius norm / sqrt(4)
            var frob = A.FrobeniusNorm();
            var s = frob / Math.Sqrt(4.0); // same as sqrt(sum squares / 4)

            if (s < epsScale)
            {
                // Degenerate linear part: treat as zero scale
                return new Float64SimilarityAffineMap4D
                {
                    ScalingFactor = 0.0,
                    HasReflection = false,
                    RotationAngle1 = 0.0,
                    Plane1U = LinFloat64Vector4D.E1,
                    Plane1V = LinFloat64Vector4D.E2,
                    RotationAngle2 = 0.0,
                    Plane2U = LinFloat64Vector4D.E3,
                    Plane2V = LinFloat64Vector4D.E4,
                    Translate = translate
                };
            }

            // Normalize to get orthogonal part Q ≈ R (maybe det = ±1)
            var Q = (DenseMatrix)(A / s);

            // Orthogonality test: Q^T Q ≈ I
            var shouldBeI = Q.TransposeThisAndMultiply(Q);
            for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                if (Math.Abs(shouldBeI[i, j] - (i == j ? 1.0 : 0.0)) > 1e-6)
                    throw new InvalidOperationException("Matrix is not a valid 4D similarity (orthogonality test failed).");

            // Reflection detection via determinant sign
            var detQ = Q.Determinant();
            var hasReflection = detQ < 0.0;
            var R = (DenseMatrix)(hasReflection ? (-1.0 * Q) : Q); // make proper rotation if reflection present

            // Eigen-decomposition of R (complex eigenpairs)
            var evd = R.ToComplex().Evd(); // returns complex eigenvalues/eigenvectors
            var evals = evd.EigenValues;                 // Vector<Complex>
            var evecs = evd.EigenVectors;                // Matrix<Complex>

            // We expect two conjugate complex pairs (or degenerate)
            // Collect eigenindex with positive imaginary part as representative of conjugate pairs
            var pairs = new List<int>();
            var used = new bool[evals.Count];

            for (var k = 0; k < evals.Count; k++)
            {
                if (used[k]) continue;
                var ev = evals[k];
                // if imaginary part > eps -> take it (representing a plane)
                if (Math.Abs(ev.Imaginary) > 1e-8)
                {
                    // find conjugate partner index
                    var conjFound = -1;
                    for (var j = k + 1; j < evals.Count; j++)
                    {
                        if (used[j]) continue;
                        if (Complex.Abs(evals[j] - Complex.Conjugate(ev)) < 1e-8)
                        {
                            conjFound = j;
                            break;
                        }
                    }

                    // record the representative (k) if imaginary part positive; otherwise record the conjugate
                    if (ev.Imaginary > 0)
                    {
                        pairs.Add(k);
                        used[k] = true;
                        if (conjFound >= 0) used[conjFound] = true;
                    }
                    else if (conjFound >= 0)
                    {
                        pairs.Add(conjFound);
                        used[conjFound] = true;
                        used[k] = true;
                    }
                    else
                    {
                        // no conjugate found (numerical). still use it if imag != 0
                        pairs.Add(k);
                        used[k] = true;
                    }
                }
            }

            // Ensure we have up to 2 planes. If fewer, fallback to creating orthonormal leftover planes.
            LinFloat64Vector4D p1u = LinFloat64Vector4D.E1, p1v = LinFloat64Vector4D.E2;
            LinFloat64Vector4D p2u = LinFloat64Vector4D.E3, p2v = LinFloat64Vector4D.E4;
            double theta1 = 0.0, theta2 = 0.0;

            if (pairs.Count >= 1)
            {
                var idx = pairs[0];
                var lambda = evals[idx];
                theta1 = Math.Atan2(lambda.Imaginary, lambda.Real);

                // eigenvector column idx
                var col = evecs.Column(idx);
                var uReal = col.Map(c => c.Real).ToArray();
                var uImag = col.Map(c => c.Imaginary).ToArray();

                p1u = LinFloat64Vector4D.Create(uReal[0], uReal[1], uReal[2], uReal[3]);
                p1v = LinFloat64Vector4D.Create(uImag[0], uImag[1], uImag[2], uImag[3]);

                // orthonormalize plane basis and normalize
                OrthonormalizePair(ref p1u, ref p1v, epsOrtho: 1e-12);
            }

            if (pairs.Count >= 2)
            {
                var idx = pairs[1];
                var lambda = evals[idx];
                theta2 = Math.Atan2(lambda.Imaginary, lambda.Real);

                var col = evecs.Column(idx);
                var uReal = col.Map(c => c.Real).ToArray();
                var uImag = col.Map(c => c.Imaginary).ToArray();

                p2u = LinFloat64Vector4D.Create(uReal[0], uReal[1], uReal[2], uReal[3]);
                p2v = LinFloat64Vector4D.Create(uImag[0], uImag[1], uImag[2], uImag[3]);

                OrthonormalizePair(ref p2u, ref p2v, epsOrtho: 1e-12);
            }
            else
            {
                // pairs.Count == 0 or 1: construct a second orthonormal plane
                // Make p2 orthogonal to p1
                var basis = BuildOrthogonalPlaneFrom(p1u, p1v);
                p2u = basis.Item1;
                p2v = basis.Item2;
                theta2 = 0.0;
            }

            // If reflection was present, we stored R = -Q and HasReflection = true.
            // Store the map with extracted planes and angles.
            return new Float64SimilarityAffineMap4D
            {
                ScalingFactor = s,
                HasReflection = hasReflection,
                RotationAngle1 = theta1,
                Plane1U = NormalizeVector(p1u),
                Plane1V = NormalizeVector(p1v),
                RotationAngle2 = theta2,
                Plane2U = NormalizeVector(p2u),
                Plane2V = NormalizeVector(p2v),
                Translate = translate
            };
        }

        //==========================  Stored parameters  ==========================

        public required bool HasReflection { get; init; }

        // Plane 1 basis + angle
        public required double RotationAngle1 { get; init; }  // radians
        public required LinFloat64Vector4D Plane1U { get; init; }
        public required LinFloat64Vector4D Plane1V { get; init; }

        // Plane 2 basis + angle
        public required double RotationAngle2 { get; init; } // radians
        public required LinFloat64Vector4D Plane2U { get; init; }
        public required LinFloat64Vector4D Plane2V { get; init; }

        public required double ScalingFactor { get; init; }   // uniform > 0 else degenerate

        public required LinFloat64Vector4D Translate { get; init; }

        public bool IsDegenerate => ScalingFactor <= Float64Utils.DefaultTolerance;

        //=================  Core helpers (plane rotation & matrix assembly)  ================

        private static LinFloat64Vector4D NormalizeVector(LinFloat64Vector4D v)
        {
            var norm = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z + v.W * v.W);
            if (norm <= Float64Utils.DefaultTolerance) return v; // don't change zero vector
            return LinFloat64Vector4D.Create(v.X / norm, v.Y / norm, v.Z / norm, v.W / norm);
        }

        // Orthonormalize pair u,v in place (Gram-Schmidt)
        private static void OrthonormalizePair(ref LinFloat64Vector4D u, ref LinFloat64Vector4D v, double epsOrtho = 1e-12)
        {
            u = NormalizeVector(u);
            // v <- v - proj_u(v)
            var dot = u.X * v.X + u.Y * v.Y + u.Z * v.Z + u.W * v.W;
            v = LinFloat64Vector4D.Create(
                v.X - dot * u.X,
                v.Y - dot * u.Y,
                v.Z - dot * u.Z,
                v.W - dot * u.W
            );
            var normV = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z + v.W * v.W);
            if (normV <= epsOrtho)
            {
                // choose an arbitrary orthonormal vector to u
                var alt = ChooseOrthogonalTo(u);
                v = alt;
            }
            else
            {
                v = LinFloat64Vector4D.Create(v.X / normV, v.Y / normV, v.Z / normV, v.W / normV);
            }
        }

        // Choose an arbitrary vector orthogonal to u (simple heuristic)
        private static LinFloat64Vector4D ChooseOrthogonalTo(LinFloat64Vector4D u)
        {
            // pick the smallest absolute component and swap
            var ax = Math.Abs(u.X);
            var ay = Math.Abs(u.Y);
            var az = Math.Abs(u.Z);
            var aw = Math.Abs(u.W);

            if (ax <= ay && ax <= az && ax <= aw)
            {
                // set x = 0, use (0, -uz, uy, 0) kind of vector
                return NormalizeVector(LinFloat64Vector4D.Create(0, -u.Z, u.Y, 0));
            }
            if (ay <= ax && ay <= az && ay <= aw)
            {
                return NormalizeVector(LinFloat64Vector4D.Create(-u.Z, 0, u.X, 0));
            }
            if (az <= ax && az <= ay && az <= aw)
            {
                return NormalizeVector(LinFloat64Vector4D.Create(-u.Y, u.X, 0, 0));
            }

            return NormalizeVector(LinFloat64Vector4D.Create(0, -u.W, 0, u.Y));
        }

        // Build a plane orthonormal to given plane u,v (simple heuristic)
        private static (LinFloat64Vector4D, LinFloat64Vector4D) BuildOrthogonalPlaneFrom(LinFloat64Vector4D u, LinFloat64Vector4D v)
        {
            // Pick a vector not in span(u,v)
            var candidate = ChooseOrthogonalTo(u);
            // Make candidate orthogonal to both u and v via Gram-Schmidt
            var dotU = candidate.X * u.X + candidate.Y * u.Y + candidate.Z * u.Z + candidate.W * u.W;
            var dotV = candidate.X * v.X + candidate.Y * v.Y + candidate.Z * v.Z + candidate.W * v.W;

            var w = LinFloat64Vector4D.Create(
                candidate.X - dotU * u.X - dotV * v.X,
                candidate.Y - dotU * u.Y - dotV * v.Y,
                candidate.Z - dotU * u.Z - dotV * v.Z,
                candidate.W - dotU * u.W - dotV * v.W
            );
            var wnorm = Math.Sqrt(w.X * w.X + w.Y * w.Y + w.Z * w.Z + w.W * w.W);
            if (wnorm <= Float64Utils.DefaultTolerance)
            {
                // fallback orthonormal basis
                var a = ChooseOrthogonalTo(u);
                var b1 = ChooseOrthogonalTo(a);
                OrthonormalizePair(ref a, ref b1);
                return (a, b1);
            }

            w = LinFloat64Vector4D.Create(w.X / wnorm, w.Y / wnorm, w.Z / wnorm, w.W / wnorm);

            // second vector orthonormal to u,v,w
            var b = ChooseOrthogonalTo(u);
            var dotUw = b.X * u.X + b.Y * u.Y + b.Z * u.Z + b.W * u.W;
            var dotVw = b.X * v.X + b.Y * v.Y + b.Z * v.Z + b.W * v.W;
            var dotWw = b.X * w.X + b.Y * w.Y + b.Z * w.Z + b.W * w.W;

            var bvec = LinFloat64Vector4D.Create(
                b.X - dotUw * u.X - dotVw * v.X - dotWw * w.X,
                b.Y - dotUw * u.Y - dotVw * v.Y - dotWw * w.Y,
                b.Z - dotUw * u.Z - dotVw * v.Z - dotWw * w.Z,
                b.W - dotUw * u.W - dotVw * v.W - dotWw * w.W
            );
            var bnorm = Math.Sqrt(bvec.X * bvec.X + bvec.Y * bvec.Y + bvec.Z * bvec.Z + bvec.W * bvec.W);
            if (bnorm <= Float64Utils.DefaultTolerance)
            {
                var alt = ChooseOrthogonalTo(w);
                OrthonormalizePair(ref w, ref alt);
                return (w, alt);
            }

            bvec = LinFloat64Vector4D.Create(bvec.X / bnorm, bvec.Y / bnorm, bvec.Z / bnorm, bvec.W / bnorm);
            return (w, bvec);
        }

        // Build rotation matrix from two plane rotations
        private DenseMatrix GetRotationMatrix()
        {
            // Start with identity
            var m = DenseMatrix.CreateIdentity(4);

            // Plane 1 rotation contribution
            var u1 = Plane1U; var v1 = Plane1V;
            var c1 = Math.Cos(RotationAngle1);
            var s1 = Math.Sin(RotationAngle1);

            // Outer products u1⊗u1, v1⊗v1, u1⊗v1, v1⊗u1
            var u1u1 = Outer(u1, u1);
            var v1v1 = Outer(v1, v1);
            var u1v1 = Outer(u1, v1);
            var v1u1 = Outer(v1, u1);

            // Add (c1 - 1)*(u1u1 + v1v1) + s1*(u1v1 - v1u1)
            m += (c1 - 1.0) * (u1u1 + v1v1);
            m += s1 * (u1v1 - v1u1);

            // Plane 2 rotation contribution
            var u2 = Plane2U; var v2 = Plane2V;
            var c2 = Math.Cos(RotationAngle2);
            var s2 = Math.Sin(RotationAngle2);

            var u2u2 = Outer(u2, u2);
            var v2v2 = Outer(v2, v2);
            var u2v2 = Outer(u2, v2);
            var v2u2 = Outer(v2, u2);

            m += (c2 - 1.0) * (u2u2 + v2v2);
            m += s2 * (u2v2 - v2u2);

            return m;
        }

        // Outer product u ⊗ v returning DenseMatrix 4x4
        private static DenseMatrix Outer(LinFloat64Vector4D u, LinFloat64Vector4D v)
        {
            var m = DenseMatrix.Create(4, 4, 0.0);

            m[0, 0] = u.X * v.X; m[0, 1] = u.X * v.Y; m[0, 2] = u.X * v.Z; m[0, 3] = u.X * v.W;
            m[1, 0] = u.Y * v.X; m[1, 1] = u.Y * v.Y; m[1, 2] = u.Y * v.Z; m[1, 3] = u.Y * v.W;
            m[2, 0] = u.Z * v.X; m[2, 1] = u.Z * v.Y; m[2, 2] = u.Z * v.Z; m[2, 3] = u.Z * v.W;
            m[3, 0] = u.W * v.X; m[3, 1] = u.W * v.Y; m[3, 2] = u.W * v.Z; m[3, 3] = u.W * v.W;

            return m;
        }

        //===================  Matrix builders (allocation-free)  ==================

        /// <summary> S * R * F (F is a simple reflection operator if HasReflection) </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DenseMatrix GetLinearMatrix()
        {
            var s = ScalingFactor;
            var rot = GetRotationMatrix();

            // Build simple reflection matrix F: flip 4th axis if HasReflection
            var rf = DenseMatrix.CreateIdentity(4);
            if (HasReflection) rf[3, 3] = -1.0;

            return (DenseMatrix)(s * rot * rf);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DenseMatrix GetLinearMatrixInverse()
        {
            if (IsDegenerate)
                throw new InvalidOperationException("Non-invertible: scale ≈ 0");

            var sInv = 1.0 / ScalingFactor;
            var rot = GetRotationMatrix();
            // inverse rotation is rot^T (rot is orthogonal)
            var rotT = (DenseMatrix)rot.Transpose();

            var rf = DenseMatrix.CreateIdentity(4);
            if (HasReflection) rf[3, 3] = -1.0;

            // inverse linear = F * R^T * S^{-1}
            return (DenseMatrix)(sInv * rf * rotT);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DenseMatrix GetFullMatrix()
        {
            var L = GetLinearMatrix();
            var tx = Translate.X; var ty = Translate.Y; var tz = Translate.Z; var tw = Translate.W;

            return DenseMatrix.OfArray(new[,]
            {
                { L[0,0], L[0,1], L[0,2], L[0,3], tx },
                { L[1,0], L[1,1], L[1,2], L[1,3], ty },
                { L[2,0], L[2,1], L[2,2], L[2,3], tz },
                { L[3,0], L[3,1], L[3,2], L[3,3], tw },
                { 0.0,    0.0,    0.0,    0.0,    1.0 }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DenseMatrix GetFullMatrixInverse()
        {
            if (IsDegenerate)
                throw new InvalidOperationException("Non-invertible: scale ≈ 0");

            var Linv = GetLinearMatrixInverse();

            var tx = Translate.X; var ty = Translate.Y; var tz = Translate.Z; var tw = Translate.W;

            // Inverse translation: -L^{-1} * t
            var tvec = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(new[] { tx, ty, tz, tw });
            var it = Linv.Multiply(tvec).Multiply(-1.0);

            return DenseMatrix.OfArray(new[,]
            {
                { Linv[0,0], Linv[0,1], Linv[0,2], Linv[0,3], it[0] },
                { Linv[1,0], Linv[1,1], Linv[1,2], Linv[1,3], it[1] },
                { Linv[2,0], Linv[2,1], Linv[2,2], Linv[2,3], it[2] },
                { Linv[3,0], Linv[3,1], Linv[3,2], Linv[3,3], it[3] },
                { 0.0,       0.0,       0.0,       0.0,       1.0 }
            });
        }

        //=======================  Direct mapping (no allocations)  =====================

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector4D MapPoint(double px, double py, double pz, double pw)
        {
            var L = GetLinearMatrix();

            var x = L[0, 0] * px + L[0, 1] * py + L[0, 2] * pz + L[0, 3] * pw + Translate.X;
            var y = L[1, 0] * px + L[1, 1] * py + L[1, 2] * pz + L[1, 3] * pw + Translate.Y;
            var z = L[2, 0] * px + L[2, 1] * py + L[2, 2] * pz + L[2, 3] * pw + Translate.Z;
            var w = L[3, 0] * px + L[3, 1] * py + L[3, 2] * pz + L[3, 3] * pw + Translate.W;

            return LinFloat64Vector4D.Create(x, y, z, w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector4D MapVector(double vx, double vy, double vz, double vw)
        {
            var L = GetLinearMatrix();

            var x = L[0, 0] * vx + L[0, 1] * vy + L[0, 2] * vz + L[0, 3] * vw;
            var y = L[1, 0] * vx + L[1, 1] * vy + L[1, 2] * vz + L[1, 3] * vw;
            var z = L[2, 0] * vx + L[2, 1] * vy + L[2, 2] * vz + L[2, 3] * vw;
            var w = L[3, 0] * vx + L[3, 1] * vy + L[3, 2] * vz + L[3, 3] * vw;

            return LinFloat64Vector4D.Create(x, y, z, w);
        }

        /// <summary>
        /// Normal transform uses (L^{-1})^T, ignoring translation. Optional normalization.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector4D MapNormal(double nx, double ny, double nz, double nw, bool makeUnit = false)
        {
            if (IsDegenerate)
                throw new InvalidOperationException("Non-invertible: scale ≈ 0");

            var Linv = GetLinearMatrixInverse();
            var LinvT = (DenseMatrix)Linv.Transpose();

            var x = LinvT[0, 0] * nx + LinvT[0, 1] * ny + LinvT[0, 2] * nz + LinvT[0, 3] * nw;
            var y = LinvT[1, 0] * nx + LinvT[1, 1] * ny + LinvT[1, 2] * nz + LinvT[1, 3] * nw;
            var z = LinvT[2, 0] * nx + LinvT[2, 1] * ny + LinvT[2, 2] * nz + LinvT[2, 3] * nw;
            var w = LinvT[3, 0] * nx + LinvT[3, 1] * ny + LinvT[3, 2] * nz + LinvT[3, 3] * nw;

            if (!makeUnit)
                return LinFloat64Vector4D.Create(x, y, z, w);

            var nn = Math.Sqrt(x * x + y * y + z * z + w * w);
            return nn > Float64Utils.DefaultTolerance
                ? LinFloat64Vector4D.Create(x / nn, y / nn, z / nn, w / nn)
                : LinFloat64Vector4D.Create(x, y, z, w);
        }

        //=====================  Optional diagnostics (4D)  =======================

        public bool IsSimilarity(double tolerance = Float64Utils.DefaultTolerance)
        {
            if (Math.Abs(ScalingFactor) < tolerance) return false;

            // Check orthogonality of rotation * F
            var rf = DenseMatrix.CreateIdentity(4);
            if (HasReflection) rf[3, 3] = -1.0;

            var rot = GetRotationMatrix();
            var Q = rot * rf;

            var shouldBeI = Q.TransposeThisAndMultiply(Q);
            for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                if (Math.Abs(shouldBeI[i, j] - (i == j ? 1.0 : 0.0)) > tolerance)
                    return false;

            return true;
        }

        public bool IsValid()
        {
            // angles finite, planes valid, translation valid, scale positive and similarity holds
            var okAngles = double.IsFinite(RotationAngle1) && double.IsFinite(RotationAngle2);
            var okPlanes = Plane1U.IsValid() && Plane1V.IsValid() && Plane2U.IsValid() && Plane2V.IsValid();
            var okTranslate = Translate.IsValid();
            var okScale = double.IsFinite(ScalingFactor) && ScalingFactor > 0;
            return okAngles && okPlanes && okTranslate && okScale && IsSimilarity();
        }

        public bool SwapsHandedness => HasReflection;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsIdentity()
        {
            return !SwapsHandedness &&
                   ScalingFactor.IsOne() &&
                   Math.Abs(RotationAngle1) < Float64Utils.DefaultTolerance &&
                   Math.Abs(RotationAngle2) < Float64Utils.DefaultTolerance &&
                   Translate.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearIdentity(double tolerance = Float64Utils.DefaultTolerance)
        {
            return !SwapsHandedness &&
                   ScalingFactor.IsNearOne(tolerance) &&
                   Math.Abs(RotationAngle1) <= tolerance &&
                   Math.Abs(RotationAngle2) <= tolerance &&
                   Translate.IsNearZero(tolerance);
        }

        //======================  Composition & inverse  =======================

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64SimilarityAffineMap4D GetInverseMap()
        {
            if (IsDegenerate) throw new InvalidOperationException("Non-invertible: scale ≈ 0");

            var sInv = 1.0 / ScalingFactor;

            // Inverse rotation: invert angles, but must handle reflection flip on one plane if HasReflection
            // We'll compute inverse linear matrix and decompose it (simpler, robust)
            var invFull = GetFullMatrixInverse();
            return Decompose(invFull);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64SimilarityAffineMap4D MapUsing(Float64SimilarityAffineMap4D m2)
        {
            // m2 * this
            var product = m2.GetFullMatrix() * GetFullMatrix();
            return Decompose(product);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64SimilarityAffineMap4D Map(Float64SimilarityAffineMap4D m2)
        {
            // this * m2
            var product = GetFullMatrix() * m2.GetFullMatrix();
            return Decompose(product);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DenseMatrix GetMatrix()
        {
            return GetFullMatrix();
        }
    }
}
