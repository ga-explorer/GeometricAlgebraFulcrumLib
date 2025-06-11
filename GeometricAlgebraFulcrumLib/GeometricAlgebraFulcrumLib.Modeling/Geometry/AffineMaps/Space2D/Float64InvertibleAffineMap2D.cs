using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

/// <summary>
/// TODO: Study how to implement a TransformCache for this class.
/// This class represents a Linear map using 4x4 homogeneous matrices internally
/// </summary>
public sealed class Float64InvertibleAffineMap2D :
    IFloat64AffineMap2D
{
    public static Float64InvertibleAffineMap2D Create()
    {
        return new Float64InvertibleAffineMap2D();
    }


    private SquareMatrix3 _matrix;
    private SquareMatrix3 _matrixInv;


    public double this[int i, int j, bool useInvMatrix]
        => useInvMatrix ? _matrixInv[i, j] : _matrix[i, j];

    public double this[int i, int j]
        => _matrix[i, j];

    public bool ContainsScale
        => _matrix.ContainsScaling;

    public bool SwapsHandedness
        => _matrix.Determinant.IsNegative();

    public LinFloat64Vector2D FinalTranslation 
        => LinFloat64Vector2D.Create(
            _matrix.Scalar20, 
            _matrix.Scalar21
        );
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64InvertibleAffineMap2D()
    {
        _matrix = SquareMatrix3.CreateIdentityMatrix();
        _matrixInv = SquareMatrix3.CreateIdentityMatrix();
        
        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64InvertibleAffineMap2D(SquareMatrix3 matrix)
    {
        _matrix = new SquareMatrix3(matrix);
        _matrixInv = matrix.Inverse();

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64InvertibleAffineMap2D(SquareMatrix3 matrix, SquareMatrix3 invMatrix)
    {
        _matrix = new SquareMatrix3(matrix);
        _matrixInv = new SquareMatrix3(invMatrix);

        Debug.Assert(IsValid());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _matrix.IsValid() &&
               _matrixInv.IsValid() &&
               (_matrix * _matrixInv).IsNearIdentity() &&
               _matrix.Scalar20.IsNearZero() &&
               _matrix.Scalar21.IsNearZero() &&
               _matrix.Scalar22.IsNearOne() &&
               _matrixInv.Scalar20.IsNearZero() &&
               _matrixInv.Scalar21.IsNearZero() &&
               _matrixInv.Scalar22.IsNearOne();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        return _matrix.IsIdentity();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearIdentity(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return _matrix.IsNearIdentity(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D SelfTranspose()
    {
        _matrix.SelfTranspose();
        _matrixInv.SelfTranspose();

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D SelfInverse()
    {
        (_matrix, _matrixInv) = (_matrixInv, _matrix);

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D SelfInverseTranspose()
    {
        (_matrix, _matrixInv) = (_matrixInv, _matrix);

        _matrix.SelfTranspose();
        _matrixInv.SelfTranspose();

        Debug.Assert(IsValid());

        return this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Reset()
    {
        _matrix = SquareMatrix3.CreateIdentityMatrix();
        _matrixInv = SquareMatrix3.CreateIdentityMatrix();

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Reset(SquareMatrix3 matrix)
    {
        _matrix = new SquareMatrix3(matrix);
        _matrixInv = _matrix.Inverse();

        Debug.Assert(IsValid());

        return this;
    }

    public Float64InvertibleAffineMap2D SetTranslationPart(IPair<Float64Scalar> dv)
    {
        PreTranslate(-FinalTranslation);
        Translate(dv);

        return this;
    }
    
    public Float64InvertibleAffineMap2D SetTranslationPart(double dx, double dy)
    {
        PreTranslate(-FinalTranslation);
        Translate(dx, dy);

        return this;
    }

    
    private Float64InvertibleAffineMap2D PrependMap(SquareMatrix3 matrix, SquareMatrix3 matrixInv)
    {
        _matrix = matrix * _matrix;
        _matrixInv = _matrixInv * matrixInv;
        
        Debug.Assert(IsValid());

        return this;
    }

    private Float64InvertibleAffineMap2D AppendMap(SquareMatrix3 matrix, SquareMatrix3 matrixInv)
    {
        _matrix = _matrix * matrix;
        _matrixInv = matrixInv * _matrixInv;
        
        Debug.Assert(IsValid());

        return this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreTranslateX(double d)
    {
        return PrependMap(
            SquareMatrix3.CreateTranslationMatrix2D(d, 0),
            SquareMatrix3.CreateTranslationMatrix2D(-d, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreTranslateY(double d)
    {
        return PrependMap(
            SquareMatrix3.CreateTranslationMatrix2D(0, d),
            SquareMatrix3.CreateTranslationMatrix2D(0, -d)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreTranslate(double dx, double dy)
    {
        return PrependMap(
            SquareMatrix3.CreateTranslationMatrix2D(dx, dy),
            SquareMatrix3.CreateTranslationMatrix2D(-dx, -dy)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreTranslate(IPair<Float64Scalar> dv)
    {
        return PrependMap(
            SquareMatrix3.CreateTranslationMatrix2D(dv),
            SquareMatrix3.CreateTranslationMatrix2D(dv.VectorNegative())
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D TranslateX(double d)
    {
        return AppendMap(
            SquareMatrix3.CreateTranslationMatrix2D(d, 0),
            SquareMatrix3.CreateTranslationMatrix2D(-d, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D TranslateY(double d)
    {
        return AppendMap(
            SquareMatrix3.CreateTranslationMatrix2D(0, d),
            SquareMatrix3.CreateTranslationMatrix2D(0, -d)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Translate(double dx, double dy)
    {
        return AppendMap(
            SquareMatrix3.CreateTranslationMatrix2D(dx, dy),
            SquareMatrix3.CreateTranslationMatrix2D(-dx, -dy)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Translate(IPair<Float64Scalar> dv)
    {
        return AppendMap(
            SquareMatrix3.CreateTranslationMatrix2D(dv),
            SquareMatrix3.CreateTranslationMatrix2D(dv.VectorNegative())
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreRotate(LinFloat64Angle angle)
    {
        return PrependMap(
            SquareMatrix3.CreateRotationMatrix2D(angle),
            SquareMatrix3.CreateRotationMatrix2D(angle.NegativeAngle())
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Rotate(LinFloat64Angle angle)
    {
        return AppendMap(
            SquareMatrix3.CreateRotationMatrix2D(angle),
            SquareMatrix3.CreateRotationMatrix2D(angle.NegativeAngle())
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Rotate(LinBasisVector srcUnitVector, LinBasisVector dstUnitVector)
    {
        return AppendMap(
            SquareMatrix3.CreateRotationMatrix2D(srcUnitVector.ToLinVector2D(), dstUnitVector.ToLinVector2D()),
            SquareMatrix3.CreateRotationMatrix2D(dstUnitVector.ToLinVector2D(), srcUnitVector.ToLinVector2D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Rotate(ILinFloat64Vector2D srcUnitVector, ILinFloat64Vector2D dstUnitVector)
    {
        return AppendMap(
            SquareMatrix3.CreateRotationMatrix2D(srcUnitVector, dstUnitVector),
            SquareMatrix3.CreateRotationMatrix2D(dstUnitVector, srcUnitVector)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreScale(double sx, double sy)
    {
        return PrependMap(
            SquareMatrix3.CreateScalingMatrix2D(sx, sy),
            SquareMatrix3.CreateScalingMatrix2D(1.0d / sx, 1.0d / sy)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreScale(double s)
    {
        return PrependMap(
            SquareMatrix3.CreateScalingMatrix2D(s),
            SquareMatrix3.CreateScalingMatrix2D(1.0d / s)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Scale(double sx, double sy)
    {
        return AppendMap(
            SquareMatrix3.CreateScalingMatrix2D(sx, sy),
            SquareMatrix3.CreateScalingMatrix2D(1.0d / sx, 1.0d / sy)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Scale(IPair<Float64Scalar> s)
    {
        return AppendMap(
            SquareMatrix3.CreateScalingMatrix2D(s),
            SquareMatrix3.CreateScalingMatrix2D(1.0d / s.Item1, 1.0d / s.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Scale(double s)
    {
        return AppendMap(
            SquareMatrix3.CreateScalingMatrix2D(s),
            SquareMatrix3.CreateScalingMatrix2D(1.0d / s)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreReflectX()
    {
        return PrependMap(
            SquareMatrix3.CreateXReflectionMatrix2D(),
            SquareMatrix3.CreateXReflectionMatrix2D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreReflectY()
    {
        return PrependMap(
            SquareMatrix3.CreateYReflectionMatrix2D(),
            SquareMatrix3.CreateYReflectionMatrix2D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreReflectOrigin()
    {
        return PrependMap(
            SquareMatrix3.CreateOriginReflectionMatrix2D(),
            SquareMatrix3.CreateOriginReflectionMatrix2D()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D ReflectX()
    {
        return AppendMap(
            SquareMatrix3.CreateXReflectionMatrix2D(),
            SquareMatrix3.CreateXReflectionMatrix2D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D ReflectY()
    {
        return AppendMap(
            SquareMatrix3.CreateYReflectionMatrix2D(),
            SquareMatrix3.CreateYReflectionMatrix2D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D ReflectOrigin()
    {
        return AppendMap(
            SquareMatrix3.CreateOriginReflectionMatrix2D(),
            SquareMatrix3.CreateOriginReflectionMatrix2D()
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreTransform(Float64InvertibleAffineMap2D map)
    {
        return PrependMap(
            map._matrix, 
            map._matrixInv
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D PreTransform(IFloat64AffineMap2D map)
    {
        return PrependMap(
            map.GetSquareMatrix3(), 
            map.GetInverseAffineMap().GetSquareMatrix3()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Transform(Float64InvertibleAffineMap2D map)
    {
        return AppendMap(
            map._matrix,
            map._matrixInv
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64InvertibleAffineMap2D Transform(IFloat64AffineMap2D map)
    {
        return AppendMap(
            map.GetSquareMatrix3(), 
            map.GetInverseAffineMap().GetSquareMatrix3()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3 GetSquareMatrix3()
    {
        return new SquareMatrix3(_matrix);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Matrix4x4 GetMatrix4x4()
    //{
    //    return _matrix.GetMatrix4x4();
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        return _matrix.GetArray2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3 ToMatrix(bool useInvMatrix)
    {
        return new SquareMatrix3(useInvMatrix ? _matrixInv : _matrix);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap2D GetInverseAffineMap()
    {
        return new Float64InvertibleAffineMap2D(_matrixInv, _matrix);
    }


    public LinFloat64Vector2D MapPoint(ILinFloat64Vector2D point)
    {
        var pointX = _matrix.Scalar00 * point.X + _matrix.Scalar01 * point.Y + _matrix.Scalar02;
        var pointY = _matrix.Scalar10 * point.X + _matrix.Scalar11 * point.Y + _matrix.Scalar12;
        var pointW = _matrix.Scalar20 * point.X + _matrix.Scalar21 * point.Y + _matrix.Scalar22;

        if (pointW.IsOne())
            return LinFloat64Vector2D.Create(pointX, pointY);

        var s = 1.0d / pointW;
        return LinFloat64Vector2D.Create(pointX * s, pointY * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapVector(ILinFloat64Vector2D vector)
    {
        return LinFloat64Vector2D.Create(
            _matrix.Scalar00 * vector.X + _matrix.Scalar01 * vector.Y,
            _matrix.Scalar10 * vector.X + _matrix.Scalar11 * vector.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapNormal(ILinFloat64Vector2D normal)
    {
        return LinFloat64Vector2D.Create(
            _matrixInv.Scalar00 * normal.X + _matrixInv.Scalar10 * normal.Y,
            _matrixInv.Scalar01 * normal.X + _matrixInv.Scalar11 * normal.Y
        );
    }
}