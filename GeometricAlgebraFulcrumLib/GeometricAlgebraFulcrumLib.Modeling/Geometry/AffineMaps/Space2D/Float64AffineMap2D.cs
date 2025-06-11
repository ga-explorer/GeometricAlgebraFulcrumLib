using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

public sealed class Float64AffineMap2D :
    IFloat64AffineMap2D
{
    public static Float64AffineMap2D Create()
    {
        return new Float64AffineMap2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMap2D Create(SquareMatrix3 matrix)
    {
        return new Float64AffineMap2D(matrix);
    }


    private SquareMatrix3 _matrix;
    

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
    private Float64AffineMap2D()
    {
        _matrix = SquareMatrix3.CreateIdentityMatrix();
        
        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AffineMap2D(SquareMatrix3 matrix)
    {
        _matrix = new SquareMatrix3(matrix);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AffineMap2D(SquareMatrix3 matrix, SquareMatrix3 invMatrix)
    {
        _matrix = new SquareMatrix3(matrix);

        Debug.Assert(IsValid());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _matrix.IsValid() &&
               _matrix.Scalar20.IsNearZero() &&
               _matrix.Scalar21.IsNearZero() &&
               _matrix.Scalar22.IsNearOne();
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
    public Float64AffineMap2D SelfTranspose()
    {
        _matrix.SelfTranspose();

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D SelfInverse()
    {
        _matrix = _matrix.Inverse();

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D SelfInverseTranspose()
    {
        _matrix = _matrix.InverseTranspose();

        Debug.Assert(IsValid());

        return this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Reset()
    {
        _matrix = SquareMatrix3.CreateIdentityMatrix();

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Reset(SquareMatrix3 matrix)
    {
        _matrix = new SquareMatrix3(matrix);

        Debug.Assert(IsValid());

        return this;
    }

    public Float64AffineMap2D SetTranslationPart(IPair<Float64Scalar> dv)
    {
        PreTranslate(-FinalTranslation);
        Translate(dv);

        return this;
    }
    
    public Float64AffineMap2D SetTranslationPart(double dx, double dy)
    {
        PreTranslate(-FinalTranslation);
        Translate(dx, dy);

        return this;
    }

    
    private Float64AffineMap2D PrependMap(SquareMatrix3 matrix)
    {
        _matrix = matrix * _matrix;
        
        Debug.Assert(IsValid());

        return this;
    }

    private Float64AffineMap2D AppendMap(SquareMatrix3 matrix)
    {
        _matrix = _matrix * matrix;
        
        Debug.Assert(IsValid());

        return this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreTranslateX(double d)
    {
        return PrependMap(
            SquareMatrix3.CreateTranslationMatrix2D(d, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreTranslateY(double d)
    {
        return PrependMap(
            SquareMatrix3.CreateTranslationMatrix2D(0, d)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreTranslate(double dx, double dy)
    {
        return PrependMap(
            SquareMatrix3.CreateTranslationMatrix2D(dx, dy)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreTranslate(IPair<Float64Scalar> dv)
    {
        return PrependMap(
            SquareMatrix3.CreateTranslationMatrix2D(dv)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D TranslateX(double d)
    {
        return AppendMap(
            SquareMatrix3.CreateTranslationMatrix2D(d, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D TranslateY(double d)
    {
        return AppendMap(
            SquareMatrix3.CreateTranslationMatrix2D(0, d)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Translate(double dx, double dy)
    {
        return AppendMap(
            SquareMatrix3.CreateTranslationMatrix2D(dx, dy)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Translate(IPair<Float64Scalar> dv)
    {
        return AppendMap(
            SquareMatrix3.CreateTranslationMatrix2D(dv)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreRotate(LinFloat64Angle angle)
    {
        return PrependMap(
            SquareMatrix3.CreateRotationMatrix2D(angle)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Rotate(LinFloat64Angle angle)
    {
        return AppendMap(
            SquareMatrix3.CreateRotationMatrix2D(angle)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Rotate(LinBasisVector srcUnitVector, LinBasisVector dstUnitVector)
    {
        return AppendMap(
            SquareMatrix3.CreateRotationMatrix2D(srcUnitVector.ToLinVector2D(), dstUnitVector.ToLinVector2D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Rotate(ILinFloat64Vector2D srcUnitVector, ILinFloat64Vector2D dstUnitVector)
    {
        return AppendMap(
            SquareMatrix3.CreateRotationMatrix2D(srcUnitVector, dstUnitVector)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreScale(double sx, double sy)
    {
        return PrependMap(
            SquareMatrix3.CreateScalingMatrix2D(sx, sy)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreScale(double s)
    {
        return PrependMap(
            SquareMatrix3.CreateScalingMatrix2D(s)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Scale(double sx, double sy)
    {
        return AppendMap(
            SquareMatrix3.CreateScalingMatrix2D(sx, sy)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Scale(IPair<Float64Scalar> s)
    {
        return AppendMap(
            SquareMatrix3.CreateScalingMatrix2D(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Scale(double s)
    {
        return AppendMap(
            SquareMatrix3.CreateScalingMatrix2D(s)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreReflectX()
    {
        return PrependMap(
            SquareMatrix3.CreateXReflectionMatrix2D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreReflectY()
    {
        return PrependMap(
            SquareMatrix3.CreateYReflectionMatrix2D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreReflectOrigin()
    {
        return PrependMap(
            SquareMatrix3.CreateOriginReflectionMatrix2D()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D ReflectX()
    {
        return AppendMap(
            SquareMatrix3.CreateXReflectionMatrix2D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D ReflectY()
    {
        return AppendMap(
            SquareMatrix3.CreateYReflectionMatrix2D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D ReflectOrigin()
    {
        return AppendMap(
            SquareMatrix3.CreateOriginReflectionMatrix2D()
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreTransform(Float64AffineMap2D map)
    {
        return PrependMap(map._matrix);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D PreTransform(IFloat64AffineMap2D map)
    {
        return PrependMap(
            map.GetSquareMatrix3()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Transform(Float64AffineMap2D map)
    {
        return AppendMap(map._matrix);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap2D Transform(IFloat64AffineMap2D map)
    {
        return AppendMap(map.GetSquareMatrix3());
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
    public SquareMatrix3 ToMatrix()
    {
        return new SquareMatrix3();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap2D GetInverseAffineMap()
    {
        return new Float64AffineMap2D(_matrix.Inverse());
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

    // This is highly inefficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapNormal(ILinFloat64Vector2D normal)
    {
        var matrixInv = _matrix.Inverse();

        return LinFloat64Vector2D.Create(
            matrixInv.Scalar00 * normal.X + matrixInv.Scalar10 * normal.Y,
            matrixInv.Scalar01 * normal.X + matrixInv.Scalar11 * normal.Y
        );
    }

}