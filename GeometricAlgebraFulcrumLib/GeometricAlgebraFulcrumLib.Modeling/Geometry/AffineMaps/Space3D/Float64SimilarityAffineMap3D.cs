using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public sealed record Float64SimilarityAffineMap3D : 
    IFloat64AffineMap3D
{
    public static Float64SimilarityAffineMap3D Identity { get; }
        = new Float64SimilarityAffineMap3D()
        {
            Translate = LinFloat64Vector3D.Zero,
            HasReflection = false,
            RotationAngle = LinFloat64PolarAngle.Angle0,
            RotationAxis = LinFloat64Vector3D.E1,
            ScalingFactor = 1
        };


    //============================  Decomposition  ============================

    /// <summary>
    /// Decompose a 4x4 homogeneous similarity matrix into T * S * R * F and
    /// extract: uniform scale s, axis (unit), angle, translation t, and XY-plane reflection flag.
    /// F is diagonal(1,1,rfz,1) where rfz = -1 iff HasReflection (Z flip).
    /// </summary>
    public static Float64SimilarityAffineMap3D Decompose(Matrix<double> matrix)
    {
        if (matrix.RowCount != 4 || matrix.ColumnCount != 4)
            throw new ArgumentException("M must be 4x4.");
        
        const double epsScale = Float64Utils.DefaultTolerance;
        const double epsOrtho = Float64Utils.DefaultTolerance;
        
        if (Math.Abs(matrix[3,0]) > epsScale ||
            Math.Abs(matrix[3,1]) > epsScale ||
            Math.Abs(matrix[3,2]) > epsScale ||
            Math.Abs(matrix[3,3] - 1.0) > epsScale)
            throw new InvalidOperationException("Matrix is not a proper 4x4 homogeneous affine transform");

        // Extract A (upper-left 3x3) and t (first 3 of last column)
        double a11 = matrix[0,0], a12 = matrix[0,1], a13 = matrix[0,2], tx = matrix[0,3];
        double a21 = matrix[1,0], a22 = matrix[1,1], a23 = matrix[1,2], ty = matrix[1,3];
        double a31 = matrix[2,0], a32 = matrix[2,1], a33 = matrix[2,2], tz = matrix[2,3];

        // Uniform scale via Frobenius norm
        // s = sqrt( (sum of squares of A) / 3 )
        var s = Math.Sqrt((a11*a11 + a12*a12 + a13*a13 +
                           a21*a21 + a22*a22 + a23*a23 +
                           a31*a31 + a32*a32 + a33*a33) / 3.0);

        
        if (s < epsScale)
        {
            return new Float64SimilarityAffineMap3D
            {
                ScalingFactor = 0.0,
                HasReflection = false,
                RotationAngle = LinFloat64PolarAngle.Angle0,
                RotationAxis = LinFloat64Vector3D.E1,
                Translate = LinFloat64Vector3D.Create(tx, ty, tz)
            };
        }

        // Normalize linear part: Q = A / s
        double q11 = a11 / s, q12 = a12 / s, q13 = a13 / s;
        double q21 = a21 / s, q22 = a22 / s, q23 = a23 / s;
        double q31 = a31 / s, q32 = a32 / s, q33 = a33 / s;

        // Orthogonality check: Q^T Q ≈ I (tolerate det = ±1)
        // Compute columns dot-products and norms
        var c1C1 = q11*q11 + q21*q21 + q31*q31; // ||c1||^2
        var c2C2 = q12*q12 + q22*q22 + q32*q32; // ||c2||^2
        var c3C3 = q13*q13 + q23*q23 + q33*q33; // ||c3||^2
        var c1C2 = q11*q12 + q21*q22 + q31*q32;
        var c1C3 = q11*q13 + q21*q23 + q31*q33;
        var c2C3 = q12*q13 + q22*q23 + q32*q33;

        if (Math.Abs(c1C1 - 1) > epsOrtho ||
            Math.Abs(c2C2 - 1) > epsOrtho ||
            Math.Abs(c3C3 - 1) > epsOrtho ||
            Math.Abs(c1C2) > epsOrtho ||
            Math.Abs(c1C3) > epsOrtho ||
            Math.Abs(c2C3) > epsOrtho)
            throw new InvalidOperationException("Matrix is not a valid 3D similarity (orthogonality test failed).");

        // Reflection detection via det(A) sign
        var detA =
              a11*(a22*a33 - a23*a32)
            - a12*(a21*a33 - a23*a31)
            + a13*(a21*a32 - a22*a31);

        var hasReflection = detA < 0.0;

        // We need the proper rotation R (det=+1) to extract axis-angle.
        // If hasReflection, Q = R * F  with F = diagonal(1,1,-1) on the right,
        // hence R = Q * F where F flips the 3rd column.
        var r13 = hasReflection ? -q13 : q13;
        var r23 = hasReflection ? -q23 : q23;
        var r33 = hasReflection ? -q33 : q33;

        // Robust axis-angle from R
        // angle = acos((trace(R) - 1)/2); axis = (1/(2 sin angle)) * (R32-R23, R13-R31, R21-R12)
        var trace = q11 + q22 + r33;
        var cosTheta = (trace - 1.0) * 0.5;

        // Clamp for numerical safety
        cosTheta = Math.Max(-1.0, Math.Min(1.0, cosTheta));
        var theta = Math.Acos(cosTheta);

        double ax, ay, az;

        if (theta < Float64Utils.DefaultTolerance) // Near zero rotation
        {
            ax = 1.0; ay = 0.0; az = 0.0;
            theta = 0.0;
        }
        else if (Math.PI - theta < 1e-8) // Near 180 degrees: special handling
        {
            // For theta ≈ pi, axis direction from diagonal elements
            // Compute components from (R + I)/2 then normalize
            var xx = (q11 + 1.0) * 0.5;
            var yy = (q22 + 1.0) * 0.5;
            var zz = (r33 + 1.0) * 0.5;
            var xy = (q12 + q21) * 0.25;
            var xz = (r13 + q31) * 0.25;
            var yz = (r23 + q32) * 0.25;

            if (xx >= yy && xx >= zz)
            {
                ax = Math.Sqrt(Math.Max(0.0, xx));
                ay = xy / Math.Max(ax, epsOrtho);
                az = xz / Math.Max(ax, epsOrtho);
            }
            else if (yy >= xx && yy >= zz)
            {
                ay = Math.Sqrt(Math.Max(0.0, yy));
                ax = xy / Math.Max(ay, epsOrtho);
                az = yz / Math.Max(ay, epsOrtho);
            }
            else
            {
                az = Math.Sqrt(Math.Max(0.0, zz));
                ax = xz / Math.Max(az, epsOrtho);
                ay = yz / Math.Max(az, epsOrtho);
            }

            var n = Math.Sqrt(ax*ax + ay*ay + az*az);
            if (n > epsOrtho) { ax /= n; ay /= n; az /= n; }
            else { ax = 1.0; ay = 0.0; az = 0.0; theta = 0.0; }
        }
        else
        {
            var denom = 2.0 * Math.Sin(theta);
            ax = (q32 - r23) / denom;
            ay = (r13 - q31) / denom;
            az = (q21 - q12) / denom;

            // Normalize just in case
            var n = Math.Sqrt(ax*ax + ay*ay + az*az);
            if (n > epsOrtho) { ax /= n; ay /= n; az /= n; }
            else { ax = 1.0; ay = 0.0; az = 0.0; theta = 0.0; }
        }

        return new Float64SimilarityAffineMap3D
        {
            ScalingFactor = s,
            HasReflection = hasReflection,
            RotationAngle = theta.RadiansToPolarAngle(),
            RotationAxis = LinFloat64Vector3D.Create(ax, ay, az),
            Translate = LinFloat64Vector3D.Create(tx, ty, tz)
        };
    }

    //==========================  Stored parameters  ==========================
    
    /// <summary>
    /// XY-plane reflection (Z flip)
    /// </summary>
    public required bool HasReflection { get; init; }   
    
    public required LinFloat64PolarAngle RotationAngle { get; init; }
    
    public required LinFloat64Vector3D RotationAxis { get; init; }
    
    public required double ScalingFactor { get; init; }   // uniform > 0 else degenerate
    
    public required LinFloat64Vector3D Translate { get; init; }

    public bool IsDegenerate 
        => ScalingFactor <= Float64Utils.DefaultTolerance;

    //=================  Core helpers (Rodriguez, precompute)  ================

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RotationEntries(out double r00, out double r01, out double r02,
                                 out double r10, out double r11, out double r12,
                                 out double r20, out double r21, out double r22)
    {
        // Ensure axis is unit (assume stored as unit; fallback normalize)
        var ux = RotationAxis.X; 
        var uy = RotationAxis.Y; 
        var uz = RotationAxis.Z;

        var n = Math.Sqrt(ux*ux + uy*uy + uz*uz);
        if (Math.Abs(n - 1.0) > Float64Utils.DefaultTolerance)
        {
            ux /= n; 
            uy /= n; 
            uz /= n;
        }

        var c  = RotationAngle.CosValue;
        var s  = RotationAngle.SinValue;
        var ic = 1.0 - c;

        // Rodriguez' rotation matrix
        r00 = c + ux*ux*ic;
        r01 = ux*uy*ic - uz*s;
        r02 = ux*uz*ic + uy*s;

        r10 = uy*ux*ic + uz*s;
        r11 = c + uy*uy*ic;
        r12 = uy*uz*ic - ux*s;

        r20 = uz*ux*ic - uy*s;
        r21 = uz*uy*ic + ux*s;
        r22 = c + uz*uz*ic;
    }

    //===================  Matrix builders (allocation-free)  ==================

    /// <summary> S * R * F (F flips Z if HasReflection) </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseMatrix GetLinearMatrix()
    {
        var s = ScalingFactor;
        var rfz = HasReflection ? -1.0 : 1.0;

        RotationEntries(out var r00, out var r01, out var r02,
                        out var r10, out var r11, out var r12,
                        out var r20, out var r21, out var r22);

        // Right-multiply R by F: multiply 3rd column by rfz
        var m00 = s * r00;
        var m01 = s * r01;
        var m02 = s * (rfz * r02);

        var m10 = s * r10;
        var m11 = s * r11;
        var m12 = s * (rfz * r12);

        var m20 = s * r20;
        var m21 = s * r21;
        var m22 = s * (rfz * r22);

        return DenseMatrix.OfArray(new[,]
        {
            { m00, m01, m02, 0 },
            { m10, m11, m12, 0 },
            { m20, m21, m22, 0 },
            { 0,   0,   0,   1 }
        });
    }

    /// <summary> F * R^{-1} * S^{-1} (inverse of S*R*F) </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseMatrix GetLinearMatrixInverse()
    {
        if (IsDegenerate)
            throw new InvalidOperationException("Non-invertible: scale ≈ 0");

        var sInv = 1.0 / ScalingFactor;
        var rfz  = HasReflection ? -1.0 : 1.0;

        RotationEntries(out var r00, out var r01, out var r02,
                        out var r10, out var r11, out var r12,
                        out var r20, out var r21, out var r22);

        // R^{-1} = R^T; then left-multiply by F (scales the 3rd row by rfz)
        var m00 = sInv *  r00; var m01 = sInv *  r10; var m02 = sInv *  r20;
        var m10 = sInv *  r01; var m11 = sInv *  r11; var m12 = sInv *  r21;
        var m20 = sInv * (rfz * r02);
        var m21 = sInv * (rfz * r12);
        var m22 = sInv * (rfz * r22);

        return DenseMatrix.OfArray(new[,]
        {
            { m00, m01, m02, 0 },
            { m10, m11, m12, 0 },
            { m20, m21, m22, 0 },
            { 0,   0,   0,   1 }
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseMatrix GetFullMatrix()
    {
        var s   = ScalingFactor;
        var rfz = HasReflection ? -1.0 : 1.0;

        RotationEntries(out var r00, out var r01, out var r02,
                        out var r10, out var r11, out var r12,
                        out var r20, out var r21, out var r22);

        var tx = Translate.X; 
        var ty = Translate.Y; 
        var tz = Translate.Z;

        var m00 = s * r00;
        var m01 = s * r01;
        var m02 = s * (rfz * r02);
        var m10 = s * r10;
        var m11 = s * r11;
        var m12 = s * (rfz * r12);
        var m20 = s * r20;
        var m21 = s * r21;
        var m22 = s * (rfz * r22);

        return DenseMatrix.OfArray(new[,]
        {
            { m00, m01, m02, tx },
            { m10, m11, m12, ty },
            { m20, m21, m22, tz },
            { 0,   0,   0,   1  }
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseMatrix GetFullMatrixInverse()
    {
        if (IsDegenerate)
            throw new InvalidOperationException("Non-invertible: scale ≈ 0");

        var sInv = 1.0 / ScalingFactor;
        var rfz  = HasReflection ? -1.0 : 1.0;

        RotationEntries(out var r00, out var r01, out var r02,
                        out var r10, out var r11, out var r12,
                        out var r20, out var r21, out var r22);

        // L^{-1} = F * R^T * S^{-1}
        var l00 = sInv *  r00; var l01 = sInv *  r10; var l02 = sInv *  r20;
        var l10 = sInv *  r01; var l11 = sInv *  r11; var l12 = sInv *  r21;
        var l20 = sInv * (rfz * r02);
        var l21 = sInv * (rfz * r12);
        var l22 = sInv * (rfz * r22);

        // Inverse translation: -L^{-1} * t
        var tx = Translate.X; 
        var ty = Translate.Y; 
        var tz = Translate.Z;

        var itx = -(l00*tx + l01*ty + l02*tz);
        var ity = -(l10*tx + l11*ty + l12*tz);
        var itz = -(l20*tx + l21*ty + l22*tz);

        return DenseMatrix.OfArray(new[,]
        {
            { l00, l01, l02, itx },
            { l10, l11, l12, ity },
            { l20, l21, l22, itz },
            { 0,   0,   0,   1   }
        });
    }

    //=======================  Direct mapping (no memory allocations)  =====================

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapPoint(double px, double py, double pz)
    {
        var s   = ScalingFactor;
        var rfz = HasReflection ? -1.0 : 1.0;

        RotationEntries(out var r00, out var r01, out var r02,
                        out var r10, out var r11, out var r12,
                        out var r20, out var r21, out var r22);

        var tx = Translate.X; 
        var ty = Translate.Y; 
        var tz = Translate.Z;

        // Apply F (Z flip) via columns effect; same as using L = S*R*F
        var x = s * (r00*px + r01*py + (rfz*r02)*pz) + tx;
        var y = s * (r10*px + r11*py + (rfz*r12)*pz) + ty;
        var z = s * (r20*px + r21*py + (rfz*r22)*pz) + tz;

        return LinFloat64Vector3D.Create(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(double vx, double vy, double vz)
    {
        var s   = ScalingFactor;
        var rfz = HasReflection ? -1.0 : 1.0;

        RotationEntries(out var r00, out var r01, out var r02,
                        out var r10, out var r11, out var r12,
                        out var r20, out var r21, out var r22);

        var x = s * (r00*vx + r01*vy + (rfz*r02)*vz);
        var y = s * (r10*vx + r11*vy + (rfz*r12)*vz);
        var z = s * (r20*vx + r21*vy + (rfz*r22)*vz);

        return LinFloat64Vector3D.Create(x, y, z);
    }

    /// <summary>
    /// Normal transform uses (L^{-1})^T, ignoring translation. Optional normalization.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapNormal(double nx, double ny, double nz, bool makeUnit = false)
    {
        if (IsDegenerate)
            throw new InvalidOperationException("Non-invertible: scale ≈ 0");

        var sInv = 1.0 / ScalingFactor;
        var rfz  = HasReflection ? -1.0 : 1.0;

        RotationEntries(out var r00, out var r01, out var r02,
                        out var r10, out var r11, out var r12,
                        out var r20, out var r21, out var r22);

        // L^{-1} = F * R^T * S^{-1}; (L^{-1})^T = S^{-1} * R * F
        // But we can compute (L^{-1}) entries then multiply by its transpose:
        var l00 = sInv *  r00; var l01 = sInv *  r10; var l02 = sInv *  r20;
        var l10 = sInv *  r01; var l11 = sInv *  r11; var l12 = sInv *  r21;
        var l20 = sInv * (rfz * r02);
        var l21 = sInv * (rfz * r12);
        var l22 = sInv * (rfz * r22);

        // n' = (L^{-1})^T * n  => components:
        var x = l00*nx + l10*ny + l20*nz;
        var y = l01*nx + l11*ny + l21*nz;
        var z = l02*nx + l12*ny + l22*nz;

        if (!makeUnit) 
            return LinFloat64Vector3D.Create(x, y, z);

        var nn = Math.Sqrt(x*x + y*y + z*z);
        return nn > Float64Utils.DefaultTolerance 
            ? LinFloat64Vector3D.Create(x/nn, y/nn, z/nn) 
            : LinFloat64Vector3D.Create(x, y, z);
    }

    //=====================  Optional diagnostics (3D)  =======================

    public bool IsSimilarity(double tolerance = Float64Utils.DefaultTolerance)
    {
        if (Math.Abs(ScalingFactor) < tolerance) return false;

        // Build Q = (S*R*F)/s = R*F; check orthogonality via explicit entries
        var rfz = HasReflection ? -1.0 : 1.0;
        RotationEntries(out var r00, out var r01, out var r02,
                        out var r10, out var r11, out var r12,
                        out var r20, out var r21, out var r22);

        // Columns of Q = [c1 c2 c3] with c3 scaled by rfz
        var q13 = rfz*r02;
        var q23 = rfz*r12;
        var q33 = rfz*r22;

        var c1C1 = r00*r00 + r10*r10 + r20*r20;
        var c2C2 = r01*r01 + r11*r11 + r21*r21;
        var c3C3 = q13*q13 + q23*q23 + q33*q33;
        var c1C2 = r00*r01 + r10*r11 + r20*r21;
        var c1C3 = r00*q13 + r10*q23 + r20*q33;
        var c2C3 = r01*q13 + r11*q23 + r21*q33;

        return Math.Abs(c1C1 - 1) <= tolerance &&
               Math.Abs(c2C2 - 1) <= tolerance &&
               Math.Abs(c3C3 - 1) <= tolerance &&
               Math.Abs(c1C2)     <= tolerance &&
               Math.Abs(c1C3)     <= tolerance &&
               Math.Abs(c2C3)     <= tolerance;
    }

    public Float64SimilarityAffineMap3D GetInverseMap()
    {
        if (IsDegenerate)
            throw new InvalidOperationException("Non-invertible: scale ≈ 0");

        var sInv = 1.0 / ScalingFactor;

        // Rotation angle for the inverse (transpose of rotation matrix)
        var invAngle = -RotationAngle;

        // Rotation axis: flip z-component if reflection is present
        var ux = RotationAxis.X;
        var uy = RotationAxis.Y;
        var uz = RotationAxis.Z;

        if (HasReflection)
            uz = -uz;

        // Normalize the axis to unit length
        var n = Math.Sqrt(ux * ux + uy * uy + uz * uz);
        if (n > 0.0 && Math.Abs(n - 1.0) > Float64Utils.DefaultTolerance)
        {
            ux /= n;
            uy /= n;
            uz /= n;
        }

        // Compute inverse translation: -(F R^T S^{-1}) * t
        var c  = RotationAngle.CosValue;
        var sn = RotationAngle.SinValue;
        var rfz = HasReflection ? -1.0 : 1.0;

        // Apply S^{-1}
        var x1 = Translate.X * sInv;
        var y1 = Translate.Y * sInv;
        var z1 = Translate.Z * sInv;

        // Apply R^T
        var x2 =  c * x1 + sn * y1;
        var y2 = -sn * x1 + c * y1;
        var z2 = z1; // rotation around z-axis here

        // Apply F (reflection in xy-plane)
        var x3 = x2;
        var y3 = y2;
        var z3 = rfz * z2;

        var itx = -x3;
        var ity = -y3;
        var itz = -z3;

        return new Float64SimilarityAffineMap3D
        {
            ScalingFactor = sInv,
            RotationAxis  = LinFloat64Vector3D.Create(ux, uy, uz),
            RotationAngle = invAngle,
            HasReflection = HasReflection,
            Translate     = LinFloat64Vector3D.Create(itx, ity, itz)
        };
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimilarityAffineMap3D MapUsing(Float64SimilarityAffineMap3D m2)
    {
        return Decompose(
            m2.GetFullMatrix() * GetFullMatrix()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimilarityAffineMap3D Map(Float64SimilarityAffineMap3D m2)
    {
        return Decompose(
            GetFullMatrix() * m2.GetFullMatrix()
        );
    }



    //======================  Commented 4×4 helpers (kept)  ===================

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static double[,] Transpose(double[,] m)
    //{
    //    // Valid for 4x4
    //    return new[,]
    //    {
    //        { m[0,0], m[1,0], m[2,0], m[3,0] },
    //        { m[0,1], m[1,1], m[2,1], m[3,1] },
    //        { m[0,2], m[1,2], m[2,2], m[3,2] },
    //        { m[0,3], m[1,3], m[2,3], m[3,3] }
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static double[] Multiply(double[,] a, double[] b)
    //{
    //    // a: 4x4, b: length 4
    //    return new[]
    //    {
    //        a[0,0]*b[0] + a[0,1]*b[1] + a[0,2]*b[2] + a[0,3]*b[3],
    //        a[1,0]*b[0] + a[1,1]*b[1] + a[1,2]*b[2] + a[1,3]*b[3],
    //        a[2,0]*b[0] + a[2,1]*b[1] + a[2,2]*b[2] + a[2,3]*b[3],
    //        a[3,0]*b[0] + a[3,1]*b[1] + a[3,2]*b[2] + a[3,3]*b[3]
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static double[,] Multiply(double[,] a, double[,] b)
    //{
    //    // 4x4 * 4x4
    //    return new[,]
    //    {
    //        {
    //            a[0,0]*b[0,0] + a[0,1]*b[1,0] + a[0,2]*b[2,0] + a[0,3]*b[3,0],
    //            a[0,0]*b[0,1] + a[0,1]*b[1,1] + a[0,2]*b[2,1] + a[0,3]*b[3,1],
    //            a[0,0]*b[0,2] + a[0,1]*b[1,2] + a[0,2]*b[2,2] + a[0,3]*b[3,2],
    //            a[0,0]*b[0,3] + a[0,1]*b[1,3] + a[0,2]*b[2,3] + a[0,3]*b[3,3]
    //        },
    //        {
    //            a[1,0]*b[0,0] + a[1,1]*b[1,0] + a[1,2]*b[2,0] + a[1,3]*b[3,0],
    //            a[1,0]*b[0,1] + a[1,1]*b[1,1] + a[1,2]*b[2,1] + a[1,3]*b[3,1],
    //            a[1,0]*b[0,2] + a[1,1]*b[1,2] + a[1,2]*b[2,2] + a[1,3]*b[3,2],
    //            a[1,0]*b[0,3] + a[1,1]*b[1,3] + a[1,2]*b[2,3] + a[1,3]*b[3,3]
    //        },
    //        {
    //            a[2,0]*b[0,0] + a[2,1]*b[1,0] + a[2,2]*b[2,0] + a[2,3]*b[3,0],
    //            a[2,0]*b[0,1] + a[2,1]*b[1,1] + a[2,2]*b[2,1] + a[2,3]*b[3,1],
    //            a[2,0]*b[0,2] + a[2,1]*b[1,2] + a[2,2]*b[2,2] + a[2,3]*b[3,2],
    //            a[2,0]*b[0,3] + a[2,1]*b[1,3] + a[2,2]*b[2,3] + a[2,3]*b[3,3]
    //        },
    //        {
    //            a[3,0]*b[0,0] + a[3,1]*b[1,0] + a[3,2]*b[2,0] + a[3,3]*b[3,0],
    //            a[3,0]*b[0,1] + a[3,1]*b[1,1] + a[3,2]*b[2,1] + a[3,3]*b[3,1],
    //            a[3,0]*b[0,2] + a[3,1]*b[1,2] + a[3,2]*b[2,2] + a[3,3]*b[3,2],
    //            a[3,0]*b[0,3] + a[3,1]*b[1,3] + a[3,2]*b[2,3] + a[3,3]*b[3,3]
    //        }
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static double[,] Multiply(params double[][,] ms)
    //{
    //    if (ms == null || ms.Length == 0)
    //        throw new ArgumentException("No matrices");
    //    var result = ms[0];
    //    for (var i = 1; i < ms.Length; i++)
    //        result = Multiply(result, ms[i]);
    //    return result;
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return RotationAngle.IsValid() && 
               RotationAxis.IsValid() &&
               Translate.IsValid() &&
               ScalingFactor.IsValid() && 
               ScalingFactor > 0 &&
               IsSimilarity();
    }

    public bool SwapsHandedness 
        => HasReflection;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        return !SwapsHandedness &&
               ScalingFactor.IsOne() &&
               RotationAngle.IsZero() &&
               Translate.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearIdentity(double tolerance = Float64Utils.DefaultTolerance)
    {
        return !SwapsHandedness &&
               ScalingFactor.IsNearOne(tolerance) &&
               RotationAngle.IsNearZero(tolerance) &&
               Translate.IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point)
    {
        return MapPoint(
            point.Item1, 
            point.Item2, 
            point.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return MapVector(
            vector.Item1, 
            vector.Item2, 
            vector.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        return MapNormal(
            normal.Item1, 
            normal.Item2, 
            normal.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4 GetSquareMatrix4()
    {
        return new SquareMatrix4(GetFullMatrix());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix4x4 GetMatrix4x4()
    {
        var m = GetFullMatrix();

        return new Matrix4x4()
        {
            M11 = (float)m[0, 0],
            M21 = (float)m[1, 0],
            M31 = (float)m[2, 0],
            M41 = (float)m[3, 0],

            M12 = (float)m[0, 1],
            M22 = (float)m[1, 1],
            M32 = (float)m[2, 1],
            M42 = (float)m[3, 1],

            M13 = (float)m[0, 2],
            M23 = (float)m[1, 2],
            M33 = (float)m[2, 2],
            M43 = (float)m[3, 2],

            M14 = (float)m[0, 3],
            M24 = (float)m[1, 3],
            M34 = (float)m[2, 3],
            M44 = (float)m[3, 3]
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix<double> GetMatrix()
    {
        return GetFullMatrix();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap3D GetInverseAffineMap()
    {
        return GetInverseMap();
    }
}
