﻿using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public sealed class Float64AffineMap3D :
    IFloat64AffineMap3D
{
    public static Float64AffineMap3D Create()
    {
        return new Float64AffineMap3D();
    }
    
    public static Float64AffineMap3D Create(SquareMatrix4 matrix)
    {
        return new Float64AffineMap3D(matrix);
    }


    private SquareMatrix4 _matrix;
    

    public double this[int i, int j]
        => _matrix[i, j];

    public bool ContainsScale
        => _matrix.ContainsScaling;

    public bool SwapsHandedness
        => _matrix.Determinant.IsNegative();

    public LinFloat64Vector3D FinalTranslation 
        => LinFloat64Vector3D.Create(
            _matrix.Scalar30, 
            _matrix.Scalar31, 
            _matrix.Scalar32
        );
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AffineMap3D()
    {
        _matrix = SquareMatrix4.CreateIdentityMatrix();
        
        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AffineMap3D(SquareMatrix4 matrix)
    {
        _matrix = new SquareMatrix4(matrix);

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _matrix.IsValid() &&
               _matrix.Scalar30.IsNearZero() &&
               _matrix.Scalar31.IsNearZero() &&
               _matrix.Scalar32.IsNearZero() &&
               _matrix.Scalar33.IsNearOne();
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
    public Float64AffineMap3D SelfTranspose()
    {
        _matrix.SelfTranspose();

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D SelfInverse()
    {
        _matrix = _matrix.Inverse();

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D SelfInverseTranspose()
    {
        _matrix = _matrix.Inverse().SelfTranspose();
        
        Debug.Assert(IsValid());

        return this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Reset()
    {
        _matrix = SquareMatrix4.CreateIdentityMatrix();

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Reset(SquareMatrix4 matrix)
    {
        _matrix = new SquareMatrix4(matrix);

        Debug.Assert(IsValid());

        return this;
    }

    public Float64AffineMap3D SetTranslationPart(ITriplet<Float64Scalar> dv)
    {
        PreTranslate(-FinalTranslation);
        Translate(dv);

        return this;
    }
    
    public Float64AffineMap3D SetTranslationPart(double dx, double dy, double dz)
    {
        PreTranslate(-FinalTranslation);
        Translate(dx, dy, dz);

        return this;
    }

    
    private Float64AffineMap3D PrependMap(SquareMatrix4 matrix)
    {
        _matrix = matrix * _matrix;
        
        Debug.Assert(IsValid());

        return this;
    }

    private Float64AffineMap3D AppendMap(SquareMatrix4 matrix)
    {
        _matrix = _matrix * matrix;
        
        Debug.Assert(IsValid());

        return this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreTranslateX(double d)
    {
        return PrependMap(
            SquareMatrix4.CreateTranslationMatrix3D(d, 0, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreTranslateY(double d)
    {
        return PrependMap(
            SquareMatrix4.CreateTranslationMatrix3D(0, d, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreTranslateZ(double d)
    {
        return PrependMap(
            SquareMatrix4.CreateTranslationMatrix3D(0, 0, d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreTranslate(double dx, double dy, double dz)
    {
        return PrependMap(
            SquareMatrix4.CreateTranslationMatrix3D(dx, dy, dz)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreTranslate(ITriplet<Float64Scalar> dv)
    {
        return PrependMap(
            SquareMatrix4.CreateTranslationMatrix3D(dv)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D TranslateX(double d)
    {
        return AppendMap(
            SquareMatrix4.CreateTranslationMatrix3D(d, 0, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D TranslateY(double d)
    {
        return AppendMap(
            SquareMatrix4.CreateTranslationMatrix3D(0, d, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D TranslateZ(double d)
    {
        return AppendMap(
            SquareMatrix4.CreateTranslationMatrix3D(0, 0, d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Translate(double dx, double dy, double dz)
    {
        return AppendMap(
            SquareMatrix4.CreateTranslationMatrix3D(dx, dy, dz)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Translate(ITriplet<Float64Scalar> dv)
    {
        return AppendMap(
            SquareMatrix4.CreateTranslationMatrix3D(dv)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreRotateX(LinFloat64Angle angle)
    {
        return PrependMap(
            SquareMatrix4.CreateXRotationMatrix3D(angle)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreRotateY(LinFloat64Angle angle)
    {
        return PrependMap(
            SquareMatrix4.CreateYRotationMatrix3D(angle)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreRotateZ(LinFloat64Angle angle)
    {
        return PrependMap(
            SquareMatrix4.CreateZRotationMatrix3D(angle)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreRotate(ILinFloat64Vector3D unitAxis, LinFloat64Angle angle)
    {
        return PrependMap(
            SquareMatrix4.CreateRotationMatrix3D(unitAxis, angle)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreRotate(LinFloat64Quaternion quaternion)
    {
        return PrependMap(
            SquareMatrix4.CreateRotationMatrix3D(quaternion)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreRotate(ILinFloat64Vector3D srcUnitVector, ILinFloat64Vector3D dstUnitVector)
    {
        return PrependMap(
            SquareMatrix4.CreateRotationMatrix3D(srcUnitVector, dstUnitVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreRotate(LinBasisVectorPair3D srcVectorPair, LinBasisVectorPair3D dstVectorPair)
    {
        var q = srcVectorPair.VectorPairToVectorPairRotationQuaternion(dstVectorPair);

        return PrependMap(
            SquareMatrix4.CreateRotationMatrix3D(q)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreRotate(LinBasisVectorPair3D srcVectorPair, ILinFloat64Vector3D dstVector1, ILinFloat64Vector3D dstVector2)
    {
        var q = srcVectorPair.VectorPairToVectorPairRotationQuaternion(dstVector1, dstVector2);

        return PrependMap(
            SquareMatrix4.CreateRotationMatrix3D(q)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D RotateX(LinFloat64Angle angle)
    {
        return AppendMap(
            SquareMatrix4.CreateXRotationMatrix3D(angle)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D RotateY(LinFloat64Angle angle)
    {
        return AppendMap(
            SquareMatrix4.CreateYRotationMatrix3D(angle)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D RotateZ(LinFloat64Angle angle)
    {
        return AppendMap(
            SquareMatrix4.CreateZRotationMatrix3D(angle)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Rotate(ILinFloat64Vector3D unitAxis, LinFloat64Angle angle)
    {
        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(unitAxis, angle)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Rotate(LinFloat64Quaternion quaternion)
    {
        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(quaternion)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Rotate(LinBasisVector srcUnitVector, LinBasisVector dstUnitVector)
    {
        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(srcUnitVector.ToLinVector3D(), dstUnitVector.ToLinVector3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Rotate(ILinFloat64Vector3D srcUnitVector, ILinFloat64Vector3D dstUnitVector)
    {
        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(srcUnitVector, dstUnitVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Rotate(LinBasisVectorPair3D srcVectorPair, LinBasisVectorPair3D dstVectorPair)
    {
        var q = 
            srcVectorPair.VectorPairToVectorPairRotationQuaternion(dstVectorPair);

        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(q)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Rotate(LinBasisVectorPair3D srcVectorPair, ILinFloat64Vector3D dstVector1, ILinFloat64Vector3D dstVector2)
    {
        var q = 
            srcVectorPair.VectorPairToVectorPairRotationQuaternion(dstVector1, dstVector2);

        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(q)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreScale(double sx, double sy, double sz)
    {
        return PrependMap(
            SquareMatrix4.CreateScalingMatrix3D(sx, sy, sz)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreScale(double s)
    {
        return PrependMap(
            SquareMatrix4.CreateScalingMatrix3D(s)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Scale(double sx, double sy, double sz)
    {
        return AppendMap(
            SquareMatrix4.CreateScalingMatrix3D(sx, sy, sz)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Scale(double s)
    {
        return AppendMap(
            SquareMatrix4.CreateScalingMatrix3D(s)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreReflectXy()
    {
        return PrependMap(
            SquareMatrix4.CreateXyReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreReflectYz()
    {
        return PrependMap(
            SquareMatrix4.CreateYzReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreReflectZx()
    {
        return PrependMap(
            SquareMatrix4.CreateZxReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreReflectX()
    {
        return PrependMap(
            SquareMatrix4.CreateXReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreReflectY()
    {
        return PrependMap(
            SquareMatrix4.CreateYReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreReflectZ()
    {
        return PrependMap(
            SquareMatrix4.CreateZReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreReflectOrigin()
    {
        return PrependMap(
            SquareMatrix4.CreateOriginReflectionMatrix3D()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D ReflectXy()
    {
        return AppendMap(
            SquareMatrix4.CreateXyReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D ReflectYz()
    {
        return AppendMap(
            SquareMatrix4.CreateYzReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D ReflectZx()
    {
        return AppendMap(
            SquareMatrix4.CreateZxReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D ReflectX()
    {
        return AppendMap(
            SquareMatrix4.CreateXReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D ReflectY()
    {
        return AppendMap(
            SquareMatrix4.CreateYReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D ReflectZ()
    {
        return AppendMap(
            SquareMatrix4.CreateZReflectionMatrix3D()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D ReflectOrigin()
    {
        return AppendMap(
            SquareMatrix4.CreateOriginReflectionMatrix3D()
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreTransform(Float64AffineMap3D map)
    {
        return PrependMap(
            map._matrix
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D PreTransform(IFloat64AffineMap3D map)
    {
        return PrependMap(
            map.GetSquareMatrix4()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Transform(Float64AffineMap3D map)
    {
        return AppendMap(
            map._matrix
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AffineMap3D Transform(IFloat64AffineMap3D map)
    {
        return AppendMap(
            map.GetSquareMatrix4()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4 GetSquareMatrix4()
    {
        return new SquareMatrix4(_matrix);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix4x4 GetMatrix4x4()
    {
        return _matrix.GetMatrix4x4();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        return _matrix.GetArray2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4 ToMatrix()
    {
        return new SquareMatrix4(_matrix);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap3D GetInverseAffineMap()
    {
        return new Float64AffineMap3D(_matrix.Inverse());
    }


    public LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point)
    {
        var pointX = _matrix.Scalar00 * point.X + _matrix.Scalar01 * point.Y + _matrix.Scalar02 * point.Z + _matrix.Scalar03;
        var pointY = _matrix.Scalar10 * point.X + _matrix.Scalar11 * point.Y + _matrix.Scalar12 * point.Z + _matrix.Scalar13;
        var pointZ = _matrix.Scalar20 * point.X + _matrix.Scalar21 * point.Y + _matrix.Scalar22 * point.Z + _matrix.Scalar23;
        var pointW = _matrix.Scalar30 * point.X + _matrix.Scalar31 * point.Y + _matrix.Scalar32 * point.Z + _matrix.Scalar33;

        if (pointW.IsOne())
            return LinFloat64Vector3D.Create(pointX, pointY, pointZ);

        var s = 1.0d / pointW;
        return LinFloat64Vector3D.Create(pointX * s, pointY * s, pointZ * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return LinFloat64Vector3D.Create(
            _matrix.Scalar00 * vector.X + _matrix.Scalar01 * vector.Y + _matrix.Scalar02 * vector.Z,
            _matrix.Scalar10 * vector.X + _matrix.Scalar11 * vector.Y + _matrix.Scalar12 * vector.Z,
            _matrix.Scalar20 * vector.X + _matrix.Scalar21 * vector.Y + _matrix.Scalar22 * vector.Z
        );
    }

    // TODO: This is highly inefficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        var matrixInv = _matrix.Inverse();

        return LinFloat64Vector3D.Create(
            matrixInv.Scalar00 * normal.X + matrixInv.Scalar10 * normal.Y + matrixInv.Scalar20 * normal.Z,
            matrixInv.Scalar01 * normal.X + matrixInv.Scalar11 * normal.Y + matrixInv.Scalar21 * normal.Z,
            matrixInv.Scalar02 * normal.X + matrixInv.Scalar12 * normal.Y + matrixInv.Scalar22 * normal.Z
        );
    }

}