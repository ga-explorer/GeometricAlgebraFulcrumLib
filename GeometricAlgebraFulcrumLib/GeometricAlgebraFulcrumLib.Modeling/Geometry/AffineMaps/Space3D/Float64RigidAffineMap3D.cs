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

/// <summary>
/// TODO: Study how to implement a TransformCache for this class.
/// This class represents a Linear map using 4x4 homogeneous matrices internally
/// </summary>
public sealed class Float64RigidAffineMap3D :
    IFloat64AffineMap3D
{
    public static Float64RigidAffineMap3D Create()
    {
        return new Float64RigidAffineMap3D();
    }


    private SquareMatrix4 _matrix;
    private SquareMatrix4 _matrixInv;


    public double this[int i, int j, bool useInvMatrix]
        => useInvMatrix ? _matrixInv[i, j] : _matrix[i, j];

    public double this[int i, int j]
        => _matrix[i, j];

    public bool ContainsScaling
        => _matrix.ContainsScaling;

    public bool SwapsHandedness
        => _matrix.Determinant.IsNegative();

    public LinFloat64Vector3D FinalTranslation 
        => LinFloat64Vector3D.Create(
            _matrix.Scalar03, 
            _matrix.Scalar13, 
            _matrix.Scalar23
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64RigidAffineMap3D()
    {
        _matrix = SquareMatrix4.CreateIdentityMatrix();
        _matrixInv = SquareMatrix4.CreateIdentityMatrix();
        
        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64RigidAffineMap3D(SquareMatrix4 matrix)
    {
        _matrix = new SquareMatrix4(matrix);
        _matrixInv = matrix.Inverse();
        
        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64RigidAffineMap3D(SquareMatrix4 matrix, SquareMatrix4 invMatrix)
    {
        _matrix = matrix;
        _matrixInv = invMatrix;
        
        Debug.Assert(IsValid());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _matrix.IsValid() &&
               _matrixInv.IsValid() &&
               (_matrix * _matrixInv).IsNearIdentity() &&
               _matrix.UpperLeftBlock3X3.IsNearRotation() &&
               _matrix.Scalar30.IsNearZero() &&
               _matrix.Scalar31.IsNearZero() &&
               _matrix.Scalar32.IsNearZero() &&
               _matrix.Scalar33.IsNearOne() &&
               _matrixInv.UpperLeftBlock3X3.IsNearRotation() &&
               _matrixInv.Scalar30.IsNearZero() &&
               _matrixInv.Scalar31.IsNearZero() &&
               _matrixInv.Scalar32.IsNearZero() &&
               _matrixInv.Scalar33.IsNearOne();
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
    public Float64RigidAffineMap3D SelfTranspose()
    {
        _matrix.SelfTranspose();
        _matrixInv.SelfTranspose();

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D SelfInverse()
    {
        (_matrix, _matrixInv) = (_matrixInv, _matrix);

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D SelfInverseTranspose()
    {
        (_matrix, _matrixInv) = (_matrixInv, _matrix);

        _matrix.SelfTranspose();
        _matrixInv.SelfTranspose();

        Debug.Assert(IsValid());

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D Reset()
    {
        _matrix = SquareMatrix4.CreateIdentityMatrix();
        _matrixInv = SquareMatrix4.CreateIdentityMatrix();

        Debug.Assert(IsValid());

        return this;
    }

    public Float64RigidAffineMap3D SetTranslationPart(ITriplet<Float64Scalar> dv)
    {
        PreTranslate(-FinalTranslation);
        Translate(dv);

        return this;
    }
    
    public Float64RigidAffineMap3D SetTranslationPart(double dx, double dy, double dz)
    {
        PreTranslate(-FinalTranslation);
        Translate(dx, dy, dz);

        return this;
    }

    
    private Float64RigidAffineMap3D PrependMap(SquareMatrix4 matrix, SquareMatrix4 matrixInv)
    {
        _matrix = matrix * _matrix;
        _matrixInv = _matrixInv * matrixInv;
        
        Debug.Assert(IsValid());

        return this;
    }

    private Float64RigidAffineMap3D AppendMap(SquareMatrix4 matrix, SquareMatrix4 matrixInv)
    {
        _matrix = _matrix * matrix;
        _matrixInv = matrixInv * _matrixInv;
        
        Debug.Assert(IsValid());

        return this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreTranslateX(double d)
    {
        return PrependMap(
            SquareMatrix4.CreateTranslationMatrix3D(d, 0, 0),
            SquareMatrix4.CreateTranslationMatrix3D(-d, 0, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreTranslateY(double d)
    {
        return PrependMap(
            SquareMatrix4.CreateTranslationMatrix3D(0, d, 0),
            SquareMatrix4.CreateTranslationMatrix3D(0, -d, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreTranslateZ(double d)
    {
        return PrependMap(
            SquareMatrix4.CreateTranslationMatrix3D(0, 0, d),
            SquareMatrix4.CreateTranslationMatrix3D(0, 0, -d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreTranslate(double dx, double dy, double dz)
    {
        return PrependMap(
            SquareMatrix4.CreateTranslationMatrix3D(dx, dy, dz),
            SquareMatrix4.CreateTranslationMatrix3D(-dx, -dy, -dz)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreTranslate(ITriplet<Float64Scalar> dv)
    {
        return PrependMap(
            SquareMatrix4.CreateTranslationMatrix3D(dv),
            SquareMatrix4.CreateTranslationMatrix3D(dv.VectorNegative())
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D TranslateX(double d)
    {
        return AppendMap(
            SquareMatrix4.CreateTranslationMatrix3D(d, 0, 0),
            SquareMatrix4.CreateTranslationMatrix3D(-d, 0, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D TranslateY(double d)
    {
        return AppendMap(
            SquareMatrix4.CreateTranslationMatrix3D(0, d, 0),
            SquareMatrix4.CreateTranslationMatrix3D(0, -d, 0)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D TranslateZ(double d)
    {
        return AppendMap(
            SquareMatrix4.CreateTranslationMatrix3D(0, 0, d),
            SquareMatrix4.CreateTranslationMatrix3D(0, 0, -d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D Translate(double dx, double dy, double dz)
    {
        return AppendMap(
            SquareMatrix4.CreateTranslationMatrix3D(dx, dy, dz),
            SquareMatrix4.CreateTranslationMatrix3D(-dx, -dy, -dz)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D Translate(ITriplet<Float64Scalar> dv)
    {
        return AppendMap(
            SquareMatrix4.CreateTranslationMatrix3D(dv),
            SquareMatrix4.CreateTranslationMatrix3D(dv.VectorNegative())
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreRotateX(LinFloat64Angle angle)
    {
        return PrependMap(
            SquareMatrix4.CreateXRotationMatrix3D(angle),
            SquareMatrix4.CreateXRotationMatrix3D(angle.NegativeAngle())
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreRotateY(LinFloat64Angle angle)
    {
        return PrependMap(
            SquareMatrix4.CreateYRotationMatrix3D(angle),
            SquareMatrix4.CreateYRotationMatrix3D(angle.NegativeAngle())
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreRotateZ(LinFloat64Angle angle)
    {
        return PrependMap(
            SquareMatrix4.CreateZRotationMatrix3D(angle),
            SquareMatrix4.CreateZRotationMatrix3D(angle.NegativeAngle())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreRotate(ILinFloat64Vector3D unitAxis, LinFloat64Angle angle)
    {
        return PrependMap(
            SquareMatrix4.CreateRotationMatrix3D(unitAxis, angle),
            SquareMatrix4.CreateRotationMatrix3D(unitAxis.VectorNegative(), angle)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreRotate(LinFloat64Quaternion quaternion)
    {
        return PrependMap(
            SquareMatrix4.CreateRotationMatrix3D(quaternion),
            SquareMatrix4.CreateRotationMatrix3D(quaternion.Conjugate())
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreRotate(ILinFloat64Vector3D srcUnitVector, ILinFloat64Vector3D dstUnitVector)
    {
        return PrependMap(
            SquareMatrix4.CreateRotationMatrix3D(srcUnitVector, dstUnitVector),
            SquareMatrix4.CreateRotationMatrix3D(dstUnitVector, srcUnitVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreRotate(LinBasisVectorPair3D srcVectorPair, LinBasisVectorPair3D dstVectorPair)
    {
        var q = srcVectorPair.VectorPairToVectorPairRotationQuaternion(dstVectorPair);

        return PrependMap(
            SquareMatrix4.CreateRotationMatrix3D(q),
            SquareMatrix4.CreateRotationMatrix3D(q.Conjugate())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreRotate(LinBasisVectorPair3D srcVectorPair, ILinFloat64Vector3D dstVector1, ILinFloat64Vector3D dstVector2)
    {
        var q = srcVectorPair.VectorPairToVectorPairRotationQuaternion(dstVector1, dstVector2);

        return PrependMap(
            SquareMatrix4.CreateRotationMatrix3D(q),
            SquareMatrix4.CreateRotationMatrix3D(q.Conjugate())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D RotateX(LinFloat64Angle angle)
    {
        return AppendMap(
            SquareMatrix4.CreateXRotationMatrix3D(angle),
            SquareMatrix4.CreateXRotationMatrix3D(angle.NegativeAngle())
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D RotateY(LinFloat64Angle angle)
    {
        return AppendMap(
            SquareMatrix4.CreateYRotationMatrix3D(angle),
            SquareMatrix4.CreateYRotationMatrix3D(angle.NegativeAngle())
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D RotateZ(LinFloat64Angle angle)
    {
        return AppendMap(
            SquareMatrix4.CreateZRotationMatrix3D(angle),
            SquareMatrix4.CreateZRotationMatrix3D(angle.NegativeAngle())
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D Rotate(ILinFloat64Vector3D unitAxis, LinFloat64Angle angle)
    {
        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(unitAxis, angle),
            SquareMatrix4.CreateRotationMatrix3D(unitAxis.VectorNegative(), angle)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D Rotate(LinFloat64Quaternion quaternion)
    {
        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(quaternion),
            SquareMatrix4.CreateRotationMatrix3D(quaternion.Conjugate())
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D Rotate(LinBasisVector srcUnitVector, LinBasisVector dstUnitVector)
    {
        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(srcUnitVector.ToLinVector3D(), dstUnitVector.ToLinVector3D()),
            SquareMatrix4.CreateRotationMatrix3D(dstUnitVector.ToLinVector3D(), srcUnitVector.ToLinVector3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D Rotate(ILinFloat64Vector3D srcUnitVector, ILinFloat64Vector3D dstUnitVector)
    {
        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(srcUnitVector, dstUnitVector),
            SquareMatrix4.CreateRotationMatrix3D(dstUnitVector, srcUnitVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D Rotate(LinBasisVectorPair3D srcVectorPair, LinBasisVectorPair3D dstVectorPair)
    {
        var q = 
            srcVectorPair.VectorPairToVectorPairRotationQuaternion(dstVectorPair);

        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(q),
            SquareMatrix4.CreateRotationMatrix3D(q.Conjugate())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D Rotate(LinBasisVectorPair3D srcVectorPair, ILinFloat64Vector3D dstVector1, ILinFloat64Vector3D dstVector2)
    {
        var q = 
            srcVectorPair.VectorPairToVectorPairRotationQuaternion(dstVector1, dstVector2);

        return AppendMap(
            SquareMatrix4.CreateRotationMatrix3D(q),
            SquareMatrix4.CreateRotationMatrix3D(q.Conjugate())
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D PreTransform(Float64RigidAffineMap3D map)
    {
        return PrependMap(
            map._matrix, 
            map._matrixInv
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D Transform(Float64RigidAffineMap3D map)
    {
        return AppendMap(
            map._matrix, 
            map._matrixInv
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
    public SquareMatrix4 ToMatrix(bool useInvMatrix)
    {
        return new SquareMatrix4(useInvMatrix ? _matrixInv : _matrix);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap3D GetInverseAffineMap()
    {
        return new Float64RigidAffineMap3D(_matrixInv, _matrix);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RigidAffineMap3D GetRotationPart()
    {
        var matrix = new SquareMatrix4
        {
            Scalar00 = _matrix.Scalar00,
            Scalar01 = _matrix.Scalar01,
            Scalar02 = _matrix.Scalar02,
            Scalar03 = Float64Scalar.Zero,

            Scalar10 = _matrix.Scalar10,
            Scalar11 = _matrix.Scalar11,
            Scalar12 = _matrix.Scalar12,
            Scalar13 = Float64Scalar.Zero,

            Scalar20 = _matrix.Scalar20,
            Scalar21 = _matrix.Scalar21,
            Scalar22 = _matrix.Scalar22,
            Scalar23 = Float64Scalar.Zero,

            Scalar30 = Float64Scalar.Zero,
            Scalar31 = Float64Scalar.Zero,
            Scalar32 = Float64Scalar.Zero,
            Scalar33 = Float64Scalar.One
        };

        return new Float64RigidAffineMap3D(matrix, matrix.Transpose())
;   }

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
        return LinFloat64Vector3D.Create(_matrix.Scalar00 * vector.X + _matrix.Scalar01 * vector.Y + _matrix.Scalar02 * vector.Z,
            _matrix.Scalar10 * vector.X + _matrix.Scalar11 * vector.Y + _matrix.Scalar12 * vector.Z,
            _matrix.Scalar20 * vector.X + _matrix.Scalar21 * vector.Y + _matrix.Scalar22 * vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        return LinFloat64Vector3D.Create(_matrixInv.Scalar00 * normal.X + _matrixInv.Scalar10 * normal.Y + _matrixInv.Scalar20 * normal.Z,
            _matrixInv.Scalar01 * normal.X + _matrixInv.Scalar11 * normal.Y + _matrixInv.Scalar21 * normal.Z,
            _matrixInv.Scalar02 * normal.X + _matrixInv.Scalar12 * normal.Y + _matrixInv.Scalar22 * normal.Z);
    }


}