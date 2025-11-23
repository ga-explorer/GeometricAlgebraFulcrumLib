using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

public sealed record Float64SimilarityAffineMap2D : 
    IFloat64AffineMap2D
{
    public static Float64SimilarityAffineMap2D Identity { get; }
        = new Float64SimilarityAffineMap2D()
        {
            Translate = LinFloat64Vector2D.Zero,
            HasReflection = false,
            RotationAngle = LinFloat64PolarAngle.Angle0,
            ScalingFactor = 1
        };

    public static Float64SimilarityAffineMap2D Scaling(double scalingFactor)
        => new Float64SimilarityAffineMap2D
        {
            Translate = LinFloat64Vector2D.Zero,
            HasReflection = false,
            RotationAngle = LinFloat64PolarAngle.Angle0,
            ScalingFactor = scalingFactor
        };
    
    public static Float64SimilarityAffineMap2D Translation(double x, double y)
        => new Float64SimilarityAffineMap2D
        {
            Translate = LinFloat64Vector2D.Create(x, y),
            HasReflection = false,
            RotationAngle = LinFloat64PolarAngle.Angle0,
            ScalingFactor = 1
        };
    
    public static Float64SimilarityAffineMap2D Rotation(LinFloat64Angle angle)
        => new Float64SimilarityAffineMap2D
        {
            Translate = LinFloat64Vector2D.Zero,
            HasReflection = false,
            RotationAngle = angle,
            ScalingFactor = 1
        };

    public static Float64SimilarityAffineMap2D Create(double scalingFactor, LinFloat64Angle angle, double tx = 0d, double ty = 0d)
    {
        return new Float64SimilarityAffineMap2D()
        {
            HasReflection = false,
            ScalingFactor = scalingFactor,
            RotationAngle = angle,
            Translate = LinFloat64Vector2D.Create(tx, ty)
        };
    }
    
    public static Float64SimilarityAffineMap2D Create(double scalingFactor, LinFloat64Angle angle, LinFloat64Vector2D translation)
    {
        return new Float64SimilarityAffineMap2D()
        {
            HasReflection = false,
            ScalingFactor = scalingFactor,
            RotationAngle = angle,
            Translate = translation
        };
    }

    public static Float64SimilarityAffineMap2D CreateFromLineSegments(Float64LineSegment2D lineSegment1, Float64LineSegment2D lineSegment2, double tolerance = Float64Utils.DefaultTolerance)
    {
        var (p1, q1) = lineSegment1;
        var (p2, q2) = lineSegment2;

        // Direction vectors
        var v1 = q1 - p1;
        var v2 = q2 - p2;

        // Check degenerate cases
        var len1 = v1.L2Norm();
        var len2 = v2.L2Norm();

        if (len1 <= tolerance || len2 <= tolerance)
            throw new InvalidOperationException("Degenerate line segment(s)");

        // Scale
        var scalingFactor = len2 / len1;

        // Rotation (angle from v1 to v2)
        var u1 = v1 / len1;
        var u2 = v2 / len2;

        // Compute rotation matrix directly
        var cosA = u1.X * u2.X + u1.Y * u2.Y;
        var sinA = u1.X * u2.Y - u1.Y * u2.X;

        var rotationAngle = LinFloat64PolarAngle.CreateFromCosSin(cosA, sinA);

        //var angle1 = LinFloat64PolarAngle.CreateFromCosSin(v1.X / len1, v1.Y / len1);
        //var angle2 = LinFloat64PolarAngle.CreateFromCosSin(v2.X / len2, v2.Y / len2);
        //var rotationAngle = angle2.RadiansValue - angle1.RadiansValue;

        // Compute translation
        var cos = rotationAngle.Cos();
        var sin = rotationAngle.Sin();

        var x1 = p1.X;
        var y1 = p1.Y;

        var tx = p2.X - scalingFactor * (cos * x1 - sin * y1);
        var ty = p2.Y - scalingFactor * (sin * x1 + cos * y1);

        return new Float64SimilarityAffineMap2D
        {
            HasReflection = false,
            ScalingFactor = scalingFactor,
            RotationAngle = rotationAngle.RadiansToDirectedAngle(),
            Translate = LinFloat64Vector2D.Create(tx, ty)
        };
    }


    /// <summary>
    /// Decompose a 3x3 homogeneous similarity matrix into the product
    /// of 4 matrices T S R F then extracts main transformation parameters.
    /// S: a uniform scaling matrix (if identity, no scaling is present)
    /// R: a rotation around the origin matrix (if identity, rotation angle = 0)
    /// T: translation matrix (if identity, no translation is present)
    /// F: either a reflection on the x-axis diagonal(1, -1, 1) or the
    ///    identity matrix depending on the sign of the determinant of the input matrix
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Float64SimilarityAffineMap2D Decompose(Matrix<double> matrix)
    {
        if (matrix.RowCount != 3 || matrix.ColumnCount != 3)
            throw new ArgumentException("M must be 3x3.");
        
        // Robust epsilons
        const double epsScale = Float64Utils.DefaultTolerance;
        const double epsOrtho = Float64Utils.DefaultTolerance;

        if (Math.Abs(matrix[2,0]) > epsOrtho ||
            Math.Abs(matrix[2,1]) > epsOrtho ||
            Math.Abs(matrix[2,2] - 1.0) > epsOrtho)
            throw new InvalidOperationException("Matrix is not a proper homogeneous 2D affine transform");

        // Extract A and t from M
        double a11 = matrix[0, 0], a12 = matrix[0, 1], tx = matrix[0, 2];
        double a21 = matrix[1, 0], a22 = matrix[1, 1], ty = matrix[1, 2];

        // Compute scale s via Frobenius norm
        var s = Math.Sqrt(
            (a11 * a11 + a12 * a12 + a21 * a21 + a22 * a22) / 2
        );

        // If s is ~0, the linear part is (near) zero. Treat as degenerate:
        // we'll report s=0, R=I, angle=0, center=(0,0), reflection=false.
        if (s < epsScale)
        {
            return new Float64SimilarityAffineMap2D
            {
                ScalingFactor = 0.0,
                HasReflection = false,
                RotationAngle = LinFloat64PolarAngle.Angle0,
                Translate = LinFloat64Vector2D.Create(tx, ty)
            };
        }

        // After s and Q = A / s:
        var q11 = a11 / s;
        var q21 = a21 / s;

        var q12 = a12 / s;
        var q22 = a22 / s;

        // Expected for proper rotation: [q11 q12; q21 q22] ≈ [ c -s; s  c ]
        // For improper (with fixed F):  ≈ [ c  s;  s -c ]
        var dot = q11 * q12 + q21 * q22;                // should be ~0
        var n1 = Math.Sqrt(q11 * q11 + q21 * q21) - 1; // should be ~0
        var n2 = Math.Sqrt(q12 * q12 + q22 * q22) - 1; // should be ~0

        if (Math.Abs(dot) > epsOrtho || Math.Abs(n1) > epsOrtho || Math.Abs(n2) > epsOrtho)
            throw new InvalidOperationException("Matrix is not a valid similarity transformation");

        var detA = a11 * a22 - a12 * a21;
        var hasReflection = detA < 0.0;

        // Build proper rotation R that rotates the x-axis
        var r11 = q11;
        var r21 = q21;

        // Optional re-orthonormalization (Gram-Schmidt lite)
        var c1 = Math.Sqrt(r11 * r11 + r21 * r21);
        if (c1 > epsOrtho) { r11 /= c1; r21 /= c1; }

        // Angle from the rotation matrix
        var theta = LinFloat64PolarAngle.CreateFromCosSin(r11, r21);

        return new Float64SimilarityAffineMap2D
        {
            ScalingFactor = s,
            HasReflection = hasReflection,
            RotationAngle = theta,
            Translate = LinFloat64Vector2D.Create(tx, ty)
        };
    }
    

    public required bool HasReflection { get; init; }

    public required LinFloat64Angle RotationAngle { get; init; }

    public required double ScalingFactor { get; init; }

    public required LinFloat64Vector2D Translate { get; init; }

    public bool IsDegenerate
        => ScalingFactor <= Float64Utils.DefaultTolerance;
    
    public bool SwapsHandedness 
        => HasReflection;


    public bool IsSimilarity(double tolerance = Float64Utils.DefaultTolerance)
    {
        if (Math.Abs(ScalingFactor) < tolerance)
            return false;

        var a = GetLinearMatrix();
        var s = ScalingFactor;

        var q11 = a[0, 0] / s;
        var q12 = a[0, 1] / s;
        var q21 = a[1, 0] / s;
        var q22 = a[1, 1] / s;

        var dot = q11 * q12 + q21 * q22;
        var n1 = Math.Sqrt(q11 * q11 + q21 * q21);
        var n2 = Math.Sqrt(q12 * q12 + q22 * q22);

        return Math.Abs(dot) <= tolerance &&
               Math.Abs(n1 - 1) <= tolerance &&
               Math.Abs(n2 - 1) <= tolerance;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetScalingMatrix()
    {
        var s = ScalingFactor;

        return new[,]
        {
            { s, 0, 0 },
            { 0, s, 0 },
            { 0, 0, 1 }
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetScalingMatrixInverse()
    {
        if (IsDegenerate)
            throw new InvalidOperationException("Non-invertible: scale ≈ 0");

        var s = 1d / ScalingFactor;

        return new[,]
        {
            { s, 0, 0 },
            { 0, s, 0 },
            { 0, 0, 1 }
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetRotationMatrix()
    {
        var c = RotationAngle.CosValue;
        var s = RotationAngle.SinValue;

        return new[,]
        {
            { c, -s, 0 },
            { s,  c, 0 },
            { 0,  0, 1 }
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetRotationMatrixInverse()
    {
        var c = RotationAngle.CosValue;
        var s = RotationAngle.SinValue;

        return new[,]
        {
            {  c, s, 0 },
            { -s, c, 0 },
            {  0, 0, 1 }
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetReflectionMatrix()
    {
        return new double[,]
        {
            { 1,  0, 0 },
            { 0, HasReflection ? -1 : 1, 0 },
            { 0,  0, 1 }
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetTranslationMatrix()
    {
        var tx = Translate.X;
        var ty = Translate.Y;

        return new[,]
        {
            { 1, 0, tx },
            { 0, 1, ty },
            { 0, 0, 1 }
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetTranslationMatrixInverse()
    {
        var tx = -Translate.X;
        var ty = -Translate.Y;

        return new[,]
        {
            { 1, 0, tx },
            { 0, 1, ty },
            { 0, 0, 1 }
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseMatrix GetLinearMatrix()
    {
        var s = ScalingFactor;
        var c = RotationAngle.CosValue;
        var sn = RotationAngle.SinValue;
        var rf = HasReflection ? -1.0 : 1.0;

        return DenseMatrix.OfArray(new[,]
        {
            { s * c,   -s * sn * rf, 0 },
            { s * sn,   s * c * rf,  0 },
            { 0,        0,           1 }
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseMatrix GetLinearMatrixInverse()
    {
        if (IsDegenerate)
            throw new InvalidOperationException("Non-invertible: scale ≈ 0");

        var sInv = 1.0 / ScalingFactor;
        var c = RotationAngle.CosValue;
        var sn = RotationAngle.SinValue;
        var rf = HasReflection ? -1.0 : 1.0;

        // F * R^-1 * S^-1
        return DenseMatrix.OfArray(new[,]
        {
            {  sInv * c,       sInv * sn,     0 },
            { -rf * sInv * sn, rf * sInv * c, 0 },
            {  0,              0,             1 }
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseMatrix GetFullMatrix()
    {
        var s = ScalingFactor;
        var c = RotationAngle.CosValue;
        var sn = RotationAngle.SinValue;
        var rf = HasReflection ? -1.0 : 1.0;
        var tx = Translate.X;
        var ty = Translate.Y;

        return DenseMatrix.OfArray(new[,]
        {
            { s * c,   -s * sn * rf, tx },
            { s * sn,   s * c * rf,  ty },
            { 0,        0,           1 }
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseMatrix GetFullMatrixInverse()
    {
        if (IsDegenerate)
            throw new InvalidOperationException("Non-invertible: scale ≈ 0");

        var sInv = 1.0 / ScalingFactor;
        var c = RotationAngle.CosValue;
        var sn = RotationAngle.SinValue;
        var rf = HasReflection ? -1.0 : 1.0;
        var tx = Translate.X;
        var ty = Translate.Y;

        // First compute linear inverse
        var m00 = sInv * c;
        var m01 = sInv * sn;
        var m10 = -rf * sInv * sn;
        var m11 = rf * sInv * c;

        // Translation inverse is -L^-1 * t
        var itx = -(m00 * tx + m01 * ty);
        var ity = -(m10 * tx + m11 * ty);

        return DenseMatrix.OfArray(new[,]
        {
            { m00, m01, itx },
            { m10, m11, ity },
            { 0,   0,   1 }
        });
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimilarityAffineMap2D MapUsing(Float64SimilarityAffineMap2D m2)
    {
        return Decompose(
            m2.GetFullMatrix() * GetFullMatrix()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimilarityAffineMap2D Map(Float64SimilarityAffineMap2D m2)
    {
        return Decompose(
            GetFullMatrix() * m2.GetFullMatrix()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapPoint(double px, double py)
    {
        var s = ScalingFactor;
        var c = RotationAngle.CosValue;
        var sn = RotationAngle.SinValue;
        var rf = HasReflection ? -1.0 : 1.0;
        var tx = Translate.X;
        var ty = Translate.Y;

        var x = s * c * px - s * sn * rf * py + tx;
        var y = s * sn * px + s * c * rf * py + ty;

        return LinFloat64Vector2D.Create(x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapVector(double vx, double vy)
    {
        var s = ScalingFactor;
        var c = RotationAngle.CosValue;
        var sn = RotationAngle.SinValue;
        var rf = HasReflection ? -1.0 : 1.0;

        var x = s * c * vx - s * sn * rf * vy;
        var y = s * sn * vx + s * c * rf * vy;

        return LinFloat64Vector2D.Create(x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapNormal(double nx, double ny, bool makeUnit = false)
    {
        if (IsDegenerate)
            throw new InvalidOperationException("Non-invertible: scale ≈ 0");

        var sInv = 1.0 / ScalingFactor;
        var c = RotationAngle.CosValue;
        var sn = RotationAngle.SinValue;
        var rf = HasReflection ? -1.0 : 1.0;

        // Use (L^-1)^T
        var x = sInv * (c * nx - rf * sn * ny);
        var y = sInv * (sn * nx + rf * c * ny);

        if (!makeUnit)
            return LinFloat64Vector2D.Create(x, y);

        var n = Math.Sqrt(x * x + y * y);

        return n > Float64Utils.DefaultTolerance
            ? LinFloat64Vector2D.Create(x / n, y / n) 
            : LinFloat64Vector2D.Create(x, y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle MapAngle(double angle)
    {
        var vx = Math.Cos(angle);
        var vy = Math.Sin(angle);

        var c = RotationAngle.CosValue;
        var sn = RotationAngle.SinValue;
        var rf = HasReflection ? -1.0 : 1.0;

        var x =  c * vx - sn * rf * vy;
        var y =  sn * vx + c  * rf * vy;

        var n = Math.Sqrt(x * x + y * y);

        return LinFloat64PolarAngle.CreateFromCosSin(x / n, y / n);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SimilarityAffineMap2D GetInverseMap()
    {
        if (IsDegenerate)
            throw new InvalidOperationException("Non-invertible: scale ≈ 0");
        
        var sInv = 1.0 / ScalingFactor;

        // angle rule depends on handedness
        var thetaInv = 
            HasReflection ? RotationAngle : RotationAngle.NegativeAngle();

        // Compute inverse translation: -(F * R^{-1} * S^{-1}) * t
        var c = RotationAngle.CosValue;
        var sn = RotationAngle.SinValue;

        var x1 = Translate.X * sInv;          // S^{-1}
        var y1 = Translate.Y * sInv;

        var x2 =  c * x1 + sn * y1;          // R^{-1}
        var y2 = -sn * x1 + c * y1;

        var rf = HasReflection ? -1.0 : 1.0; // F
        var x3 =  x2;
        var y3 =  rf * y2;

        return new Float64SimilarityAffineMap2D
        {
            ScalingFactor = sInv,
            RotationAngle = thetaInv,
            HasReflection = HasReflection,
            Translate = LinFloat64Vector2D.Create(-x3, -y3)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return RotationAngle.IsValid() && 
               Translate.IsValid() &&
               ScalingFactor.IsValid() && 
               ScalingFactor > 0 &&
               IsSimilarity();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        return !SwapsHandedness &&
               ScalingFactor.IsOne() &&
               RotationAngle.IsZero() &&
               Translate.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapPoint(ILinFloat64Vector2D point)
    {
        return MapPoint(
            point.Item1, 
            point.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapVector(ILinFloat64Vector2D vector)
    {
        return MapVector(
            vector.Item1, 
            vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapNormal(ILinFloat64Vector2D normal)
    {
        return MapNormal(
            normal.Item1, 
            normal.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3 GetSquareMatrix3()
    {
        return new SquareMatrix3(GetArray2D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        return GetFullMatrix().AsArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap2D GetInverseAffineMap()
    {
        return GetInverseMap();
    }



    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static double[,] Transpose(double[,] m)
    //{
    //    return new[,]
    //    {
    //        { m[0, 0], m[1, 0], m[2, 0] },
    //        { m[0, 1], m[1, 1], m[2, 1] },
    //        { m[0, 2], m[1, 2], m[2, 2] },
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static double[] Multiply(double[,] a, double[] b)
    //{
    //    return
    //    [
    //        a[0,0] * b[0] + a[0,1] * b[1] + a[0,2] * b[2],
    //        a[1,0] * b[0] + a[1,1] * b[1] + a[1,2] * b[2],
    //        a[2,0] * b[0] + a[2,1] * b[1] + a[2,2] * b[2]
    //    ];
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static double[] Multiply(double[] a, double[,] b)
    //{
    //    return
    //    [
    //        a[0] * b[0,0] + a[1] * b[1,0] + a[2] * b[2,0],
    //        a[0] * b[0,1] + a[1] * b[1,1] + a[2] * b[2,1],
    //        a[0] * b[0,2] + a[1] * b[1,2] + a[2] * b[2,2]
    //    ];
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static double[,] Multiply(double[,] a, double[,] b)
    //{
    //    return new[,]
    //    {
    //        {
    //            a[0,0] * b[0,0] + a[0,1] * b[1,0] + a[0,2] * b[2,0],
    //            a[0,0] * b[0,1] + a[0,1] * b[1,1] + a[0,2] * b[2,1],
    //            a[0,0] * b[0,2] + a[0,1] * b[1,2] + a[0,2] * b[2,2]
    //        },
    //        {
    //            a[1,0] * b[0,0] + a[1,1] * b[1,0] + a[1,2] * b[2,0],
    //            a[1,0] * b[0,1] + a[1,1] * b[1,1] + a[1,2] * b[2,1],
    //            a[1,0] * b[0,2] + a[1,1] * b[1,2] + a[1,2] * b[2,2]
    //        },
    //        {
    //            a[2,0] * b[0,0] + a[2,1] * b[1,0] + a[2,2] * b[2,0],
    //            a[2,0] * b[0,1] + a[2,1] * b[1,1] + a[2,2] * b[2,1],
    //            a[2,0] * b[0,2] + a[2,1] * b[1,2] + a[2,2] * b[2,2]
    //        }
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static double[,] Multiply(params double[][,] ms)
    //{
    //    if (ms == null || ms.Length == 0)
    //        throw new ArgumentException("No matrices");

    //    var result = ms[0];

    //    // multiply in listed order
    //    for (var i = 1; i < ms.Length; i++)
    //        result = Multiply(result, ms[i]);

    //    return result;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public double[,] GetLinearMatrix()
    //{
    //    return Multiply(
    //        GetScalingMatrix(),
    //        GetRotationMatrix(),
    //        GetReflectionMatrix()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public double[,] GetLinearMatrixInverse()
    //{
    //    return Multiply(
    //        GetReflectionMatrix(),
    //        GetRotationMatrixInverse(), 
    //        GetScalingMatrixInverse()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public double[,] GetFullMatrix()
    //{
    //    return Multiply(
    //        GetTranslationMatrix(),
    //        GetScalingMatrix(),
    //        GetRotationMatrix(),
    //        GetReflectionMatrix()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public double[,] GetFullMatrixInverse()
    //{
    //    return Multiply(
    //        GetReflectionMatrix(),
    //        GetRotationMatrixInverse(), 
    //        GetScalingMatrixInverse(),
    //        GetTranslationMatrixInverse()
    //    );
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public (double, double) MapPoint(double px, double py)
    //{
    //    var p = Multiply(GetFullMatrix(), [px, py, 1]);

    //    return (p[0], p[1]);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public (double, double) MapVector(double vx, double vy)
    //{
    //    var v = Multiply(GetFullMatrix(), [vx, vy, 0]);

    //    return (v[0], v[1]);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public (double, double) MapNormal(double nx, double ny)
    //{
    //    // Use inverse-transpose of the linear part; ignore translation
    //    var invLinT = Transpose(GetLinearMatrixInverse());

    //    var v = Multiply([nx, ny, 0], invLinT);

    //    return (v[0], v[1]);
    //}

}