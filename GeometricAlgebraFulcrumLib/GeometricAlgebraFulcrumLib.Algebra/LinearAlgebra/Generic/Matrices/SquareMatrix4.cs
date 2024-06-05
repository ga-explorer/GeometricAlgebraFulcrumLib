using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;

public sealed class SquareMatrix4<T> :
    ILinearAlgebraElement<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateZeroMatrix(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix4<T>(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateIdentityMatrix(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix4<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.One,
            Scalar11 = scalarProcessor.One,
            Scalar22 = scalarProcessor.One,
            Scalar33 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateAffineMatrix3D(SquareMatrix3<T> m)
    {
        return new SquareMatrix4<T>(m.ScalarProcessor)
        {
            Scalar00 = m.Scalar00,
            Scalar10 = m.Scalar10,
            Scalar20 = m.Scalar20,
            Scalar01 = m.Scalar01,
            Scalar11 = m.Scalar11,
            Scalar21 = m.Scalar21,
            Scalar02 = m.Scalar02,
            Scalar12 = m.Scalar12,
            Scalar22 = m.Scalar22,
            Scalar33 = m.ScalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateTranslationMatrix3D(Scalar<T> dx, Scalar<T> dy, Scalar<T> dz)
    {
        var scalarProcessor = dx.ScalarProcessor;

        return new SquareMatrix4<T>(dx.ScalarProcessor)
        {
            Scalar00 = scalarProcessor.One,
            Scalar11 = scalarProcessor.One,
            Scalar22 = scalarProcessor.One,
            Scalar33 = scalarProcessor.One,
            Scalar03 = dx,
            Scalar13 = dy,
            Scalar23 = dz
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateTranslationMatrix3D(ILinVector3D<T> translationVector)
    {
        var scalarProcessor = translationVector.ScalarProcessor;

        return new SquareMatrix4<T>(translationVector.ScalarProcessor)
        {
            Scalar00 = scalarProcessor.One,
            Scalar11 = scalarProcessor.One,
            Scalar22 = scalarProcessor.One,
            Scalar33 = scalarProcessor.One,
            Scalar03 = translationVector.X,
            Scalar13 = translationVector.Y,
            Scalar23 = translationVector.Z
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateScalingMatrix3D(Scalar<T> sx, Scalar<T> sy, Scalar<T> sz)
    {
        return new SquareMatrix4<T>(sx.ScalarProcessor)
        {
            Scalar00 = sx,
            Scalar11 = sy,
            Scalar22 = sz,
            Scalar33 = sx.ScalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateScalingMatrix3D(Scalar<T> s)
    {
        return new SquareMatrix4<T>(s.ScalarProcessor)
        {
            Scalar00 = s,
            Scalar11 = s,
            Scalar22 = s,
            Scalar33 = s.ScalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateXyReflectionMatrix3D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix4<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.One,
            Scalar11 = scalarProcessor.One,
            Scalar22 = scalarProcessor.MinusOne,
            Scalar33 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateYzReflectionMatrix3D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix4<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.MinusOne,
            Scalar11 = scalarProcessor.One,
            Scalar22 = scalarProcessor.One,
            Scalar33 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateZxReflectionMatrix3D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix4<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.One,
            Scalar11 = scalarProcessor.MinusOne,
            Scalar22 = scalarProcessor.One,
            Scalar33 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateXReflectionMatrix3D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix4<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.One,
            Scalar11 = scalarProcessor.MinusOne,
            Scalar22 = scalarProcessor.MinusOne,
            Scalar33 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateYReflectionMatrix3D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix4<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.MinusOne,
            Scalar11 = scalarProcessor.One,
            Scalar22 = scalarProcessor.MinusOne,
            Scalar33 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateZReflectionMatrix3D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix4<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.MinusOne,
            Scalar11 = scalarProcessor.MinusOne,
            Scalar22 = scalarProcessor.One,
            Scalar33 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> CreateOriginReflectionMatrix3D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix4<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.MinusOne,
            Scalar11 = scalarProcessor.MinusOne,
            Scalar22 = scalarProcessor.MinusOne,
            Scalar33 = scalarProcessor.One
        };
    }

    public static SquareMatrix4<T> CreateXRotationMatrix3D(LinAngle<T> radianAngle)
    {
        Debug.Assert(radianAngle.IsValid());

        var scalarProcessor = radianAngle.ScalarProcessor;

        if (radianAngle.Radians.IsZero())
            return CreateIdentityMatrix(scalarProcessor);

        var m = new SquareMatrix4<T>(scalarProcessor);

        var cosAngle = radianAngle.Cos();
        var sinAngle = radianAngle.Sin();

        m.Scalar00 = scalarProcessor.One;

        m.Scalar11 = cosAngle;
        m.Scalar21 = sinAngle;

        m.Scalar12 = -sinAngle;
        m.Scalar22 = cosAngle;

        m.Scalar33 = scalarProcessor.One;

        return m;
    }

    public static SquareMatrix4<T> CreateYRotationMatrix3D(LinAngle<T> radianAngle)
    {
        Debug.Assert(radianAngle.IsValid());
        
        var scalarProcessor = radianAngle.ScalarProcessor;

        if (radianAngle.Radians.IsZero())
            return CreateIdentityMatrix(scalarProcessor);

        var m = new SquareMatrix4<T>(scalarProcessor);

        var cosAngle = radianAngle.Cos();
        var sinAngle = radianAngle.Sin();

        m.Scalar00 = cosAngle;
        m.Scalar20 = -sinAngle;

        m.Scalar11 = scalarProcessor.One;

        m.Scalar02 = sinAngle;
        m.Scalar22 = cosAngle;

        m.Scalar33 = scalarProcessor.One;

        return m;
    }

    public static SquareMatrix4<T> CreateZRotationMatrix3D(LinAngle<T> radianAngle)
    {
        Debug.Assert(radianAngle.IsValid());
        
        var scalarProcessor = radianAngle.ScalarProcessor;

        if (radianAngle.Radians.IsZero())
            return CreateIdentityMatrix(scalarProcessor);

        var m = new SquareMatrix4<T>(scalarProcessor);

        var cosAngle = radianAngle.Cos();
        var sinAngle = radianAngle.Sin();

        m.Scalar00 = cosAngle;
        m.Scalar10 = sinAngle;

        m.Scalar01 = -sinAngle;
        m.Scalar11 = cosAngle;

        m.Scalar22 = scalarProcessor.One;

        m.Scalar33 = scalarProcessor.One;

        return m;
    }

    public static SquareMatrix4<T> CreateRotationMatrix3D(ILinVector3D<T> unitAxis, LinPolarAngle<T> radianAngle)
    {
        var scalarProcessor = radianAngle.ScalarProcessor;

        if (radianAngle.Radians.IsZero())
            return CreateIdentityMatrix(scalarProcessor);
        
        Debug.Assert(unitAxis.IsUnitVector());

        var m = new SquareMatrix4<T>(scalarProcessor);

        var x = unitAxis.X;
        var y = unitAxis.Y;
        var z = unitAxis.Z;
        var cosAngle = radianAngle.Cos();
        var sinAngle = radianAngle.Sin();
        var oneMinusCosAngle = scalarProcessor.One - cosAngle;
        var xx = x * x;
        var yy = y * y;
        var zz = z * z;
        var xy = x * y;
        var xz = x * z;
        var yz = y * z;

        m.Scalar00 = xx + (1 - xx) * cosAngle;
        m.Scalar10 = xy * oneMinusCosAngle + z * sinAngle;
        m.Scalar20 = xz * oneMinusCosAngle - y * sinAngle;

        m.Scalar01 = xy * oneMinusCosAngle - z * sinAngle;
        m.Scalar11 = yy + (1 - yy) * cosAngle;
        m.Scalar21 = yz * oneMinusCosAngle + x * sinAngle;

        m.Scalar02 = xz * oneMinusCosAngle + y * sinAngle;
        m.Scalar12 = yz * oneMinusCosAngle - x * sinAngle;
        m.Scalar22 = zz + (1 - zz) * cosAngle;

        m.Scalar33 = scalarProcessor.One;

        //m.Scalar00 = cosAngle + unitAxis.X * unitAxis.X * oneMinusCosAngle;
        //m.Scalar10 = unitAxis.Y * unitAxis.X * oneMinusCosAngle + unitAxis.Z * sinAngle;
        //m.Scalar20 = unitAxis.Z * unitAxis.X * oneMinusCosAngle - unitAxis.Y * sinAngle;

        //m.Scalar01 = unitAxis.X * unitAxis.Y * oneMinusCosAngle - unitAxis.Z * sinAngle;
        //m.Scalar11 = cosAngle + unitAxis.Y * unitAxis.Y * oneMinusCosAngle;
        //m.Scalar21 = unitAxis.Z * unitAxis.Y * oneMinusCosAngle + unitAxis.X * sinAngle;

        //m.Scalar02 = unitAxis.X * unitAxis.Z * oneMinusCosAngle + unitAxis.Y * sinAngle;
        //m.Scalar12 = unitAxis.Y * unitAxis.Z * oneMinusCosAngle - unitAxis.X * sinAngle;
        //m.Scalar22 = cosAngle + unitAxis.Z * unitAxis.Z * oneMinusCosAngle;

        return m;
    }

    /// <summary>
    /// Create a rotation matrix that rotates the unit vector <see cref="srcUnitVector"/>
    /// into the unit vector <see cref="dstUnitVector"/>
    /// </summary>
    /// <param name="srcUnitVector"></param>
    /// <param name="dstUnitVector"></param>
    /// <returns></returns>
    public static SquareMatrix4<T> CreateRotationMatrix3D(ILinVector3D<T> srcUnitVector, ILinVector3D<T> dstUnitVector)
    {
        var angle =
            srcUnitVector.GetAngle(dstUnitVector);

        var axis =
            srcUnitVector.VectorCross(dstUnitVector);

        if (!axis.IsZeroVector())
            return CreateRotationMatrix3D(axis.ToUnitVector(), angle);

        return angle.Radians.IsZero()
            ? CreateIdentityMatrix(srcUnitVector.ScalarProcessor)
            : CreateRotationMatrix3D(
                srcUnitVector.GetUnitNormal(),
                angle
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> operator -(SquareMatrix4<T> m1)
    {
        return new SquareMatrix4<T>(m1.ScalarProcessor)
        {
            Scalar00 = -m1.Scalar00,
            Scalar10 = -m1.Scalar10,
            Scalar20 = -m1.Scalar20,
            Scalar30 = -m1.Scalar30,
            Scalar01 = -m1.Scalar01,
            Scalar11 = -m1.Scalar11,
            Scalar21 = -m1.Scalar21,
            Scalar31 = -m1.Scalar31,
            Scalar02 = -m1.Scalar02,
            Scalar12 = -m1.Scalar12,
            Scalar22 = -m1.Scalar22,
            Scalar32 = -m1.Scalar32,
            Scalar03 = -m1.Scalar03,
            Scalar13 = -m1.Scalar13,
            Scalar23 = -m1.Scalar23,
            Scalar33 = -m1.Scalar33
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> operator +(SquareMatrix4<T> m1, SquareMatrix4<T> m2)
    {
        return new SquareMatrix4<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 + m2.Scalar00,
            Scalar10 = m1.Scalar10 + m2.Scalar10,
            Scalar20 = m1.Scalar20 + m2.Scalar20,
            Scalar30 = m1.Scalar30 + m2.Scalar30,
            Scalar01 = m1.Scalar01 + m2.Scalar01,
            Scalar11 = m1.Scalar11 + m2.Scalar11,
            Scalar21 = m1.Scalar21 + m2.Scalar21,
            Scalar31 = m1.Scalar31 + m2.Scalar31,
            Scalar02 = m1.Scalar02 + m2.Scalar02,
            Scalar12 = m1.Scalar12 + m2.Scalar12,
            Scalar22 = m1.Scalar22 + m2.Scalar22,
            Scalar32 = m1.Scalar32 + m2.Scalar32,
            Scalar03 = m1.Scalar03 + m2.Scalar03,
            Scalar13 = m1.Scalar13 + m2.Scalar13,
            Scalar23 = m1.Scalar23 + m2.Scalar23,
            Scalar33 = m1.Scalar33 + m2.Scalar33
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> operator -(SquareMatrix4<T> m1, SquareMatrix4<T> m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        var m = new SquareMatrix4<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 - m2.Scalar00,
            Scalar10 = m1.Scalar10 - m2.Scalar10,
            Scalar20 = m1.Scalar20 - m2.Scalar20,
            Scalar30 = m1.Scalar30 - m2.Scalar30,
            Scalar01 = m1.Scalar01 - m2.Scalar01,
            Scalar11 = m1.Scalar11 - m2.Scalar11,
            Scalar21 = m1.Scalar21 - m2.Scalar21,
            Scalar31 = m1.Scalar31 - m2.Scalar31,
            Scalar02 = m1.Scalar02 - m2.Scalar02,
            Scalar12 = m1.Scalar12 - m2.Scalar12,
            Scalar22 = m1.Scalar22 - m2.Scalar22,
            Scalar32 = m1.Scalar32 - m2.Scalar32,
            Scalar03 = m1.Scalar03 - m2.Scalar03,
            Scalar13 = m1.Scalar13 - m2.Scalar13,
            Scalar23 = m1.Scalar23 - m2.Scalar23,
            Scalar33 = m1.Scalar33 - m2.Scalar33
        };

        return m;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> operator *(SquareMatrix4<T> m1, SquareMatrix4<T> m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        var m = new SquareMatrix4<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 * m2.Scalar00 +
                       m1.Scalar10 * m2.Scalar01 +
                       m1.Scalar20 * m2.Scalar02 +
                       m1.Scalar30 * m2.Scalar03,
            Scalar10 = m1.Scalar00 * m2.Scalar10 +
                       m1.Scalar10 * m2.Scalar11 +
                       m1.Scalar20 * m2.Scalar12 +
                       m1.Scalar30 * m2.Scalar13,
            Scalar20 = m1.Scalar00 * m2.Scalar20 +
                       m1.Scalar10 * m2.Scalar21 +
                       m1.Scalar20 * m2.Scalar22 +
                       m1.Scalar30 * m2.Scalar23,
            Scalar30 = m1.Scalar00 * m2.Scalar30 +
                       m1.Scalar10 * m2.Scalar31 +
                       m1.Scalar20 * m2.Scalar32 +
                       m1.Scalar30 * m2.Scalar33,
            Scalar01 = m1.Scalar01 * m2.Scalar00 +
                       m1.Scalar11 * m2.Scalar01 +
                       m1.Scalar21 * m2.Scalar02 +
                       m1.Scalar31 * m2.Scalar03,
            Scalar11 = m1.Scalar01 * m2.Scalar10 +
                       m1.Scalar11 * m2.Scalar11 +
                       m1.Scalar21 * m2.Scalar12 +
                       m1.Scalar31 * m2.Scalar13,
            Scalar21 = m1.Scalar01 * m2.Scalar20 +
                       m1.Scalar11 * m2.Scalar21 +
                       m1.Scalar21 * m2.Scalar22 +
                       m1.Scalar31 * m2.Scalar23,
            Scalar31 = m1.Scalar01 * m2.Scalar30 +
                       m1.Scalar11 * m2.Scalar31 +
                       m1.Scalar21 * m2.Scalar32 +
                       m1.Scalar31 * m2.Scalar33,
            Scalar02 = m1.Scalar02 * m2.Scalar00 +
                       m1.Scalar12 * m2.Scalar01 +
                       m1.Scalar22 * m2.Scalar02 +
                       m1.Scalar32 * m2.Scalar03,
            Scalar12 = m1.Scalar02 * m2.Scalar10 +
                       m1.Scalar12 * m2.Scalar11 +
                       m1.Scalar22 * m2.Scalar12 +
                       m1.Scalar32 * m2.Scalar13,
            Scalar22 = m1.Scalar02 * m2.Scalar20 +
                       m1.Scalar12 * m2.Scalar21 +
                       m1.Scalar22 * m2.Scalar22 +
                       m1.Scalar32 * m2.Scalar23,
            Scalar32 = m1.Scalar02 * m2.Scalar30 +
                       m1.Scalar12 * m2.Scalar31 +
                       m1.Scalar22 * m2.Scalar32 +
                       m1.Scalar32 * m2.Scalar33,
            Scalar03 = m1.Scalar03 * m2.Scalar00 +
                       m1.Scalar13 * m2.Scalar01 +
                       m1.Scalar23 * m2.Scalar02 +
                       m1.Scalar33 * m2.Scalar03,
            Scalar13 = m1.Scalar03 * m2.Scalar10 +
                       m1.Scalar13 * m2.Scalar11 +
                       m1.Scalar23 * m2.Scalar12 +
                       m1.Scalar33 * m2.Scalar13,
            Scalar23 = m1.Scalar03 * m2.Scalar20 +
                       m1.Scalar13 * m2.Scalar21 +
                       m1.Scalar23 * m2.Scalar22 +
                       m1.Scalar33 * m2.Scalar23,
            Scalar33 = m1.Scalar03 * m2.Scalar30 +
                       m1.Scalar13 * m2.Scalar31 +
                       m1.Scalar23 * m2.Scalar32 +
                       m1.Scalar33 * m2.Scalar33
        };


        return m;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator *(SquareMatrix4<T> m, ILinVector4D<T> vector)
    {
        var x = vector.X;
        var y = vector.Y;
        var z = vector.Z;
        var w = vector.W;

        return LinVector4D<T>.Create(m.Scalar00 * x + m.Scalar01 * y + m.Scalar02 * z + m.Scalar03 * w,
            m.Scalar10 * x + m.Scalar11 * y + m.Scalar12 * z + m.Scalar13 * w,
            m.Scalar20 * x + m.Scalar21 * y + m.Scalar22 * z + m.Scalar23 * w,
            m.Scalar30 * x + m.Scalar31 * y + m.Scalar32 * z + m.Scalar33 * w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator *(ILinVector4D<T> vector, SquareMatrix4<T> m)
    {
        var x = vector.X;
        var y = vector.Y;
        var z = vector.Z;
        var w = vector.W;

        return LinVector4D<T>.Create(m.Scalar00 * x + m.Scalar10 * y + m.Scalar20 * z + m.Scalar30 * w,
            m.Scalar01 * x + m.Scalar11 * y + m.Scalar21 * z + m.Scalar31 * w,
            m.Scalar02 * x + m.Scalar12 * y + m.Scalar22 * z + m.Scalar32 * w,
            m.Scalar03 * x + m.Scalar13 * y + m.Scalar23 * z + m.Scalar33 * w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> operator *(SquareMatrix4<T> m1, Scalar<T> s)
    {
        Debug.Assert(m1.IsValid() && s.IsValid());

        var m = new SquareMatrix4<T>(m1.ScalarProcessor)
        {
            Scalar00 = s * m1.Scalar00,
            Scalar10 = s * m1.Scalar10,
            Scalar20 = s * m1.Scalar20,
            Scalar30 = s * m1.Scalar30,
            Scalar01 = s * m1.Scalar01,
            Scalar11 = s * m1.Scalar11,
            Scalar21 = s * m1.Scalar21,
            Scalar31 = s * m1.Scalar31,
            Scalar02 = s * m1.Scalar02,
            Scalar12 = s * m1.Scalar12,
            Scalar22 = s * m1.Scalar22,
            Scalar32 = s * m1.Scalar32,
            Scalar03 = s * m1.Scalar03,
            Scalar13 = s * m1.Scalar13,
            Scalar23 = s * m1.Scalar23,
            Scalar33 = s * m1.Scalar33
        };

        return m;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> operator *(Scalar<T> s, SquareMatrix4<T> m1)
    {
        Debug.Assert(m1.IsValid() && s.IsValid());

        var m = new SquareMatrix4<T>(m1.ScalarProcessor)
        {
            Scalar00 = s * m1.Scalar00,
            Scalar10 = s * m1.Scalar10,
            Scalar20 = s * m1.Scalar20,
            Scalar30 = s * m1.Scalar30,
            Scalar01 = s * m1.Scalar01,
            Scalar11 = s * m1.Scalar11,
            Scalar21 = s * m1.Scalar21,
            Scalar31 = s * m1.Scalar31,
            Scalar02 = s * m1.Scalar02,
            Scalar12 = s * m1.Scalar12,
            Scalar22 = s * m1.Scalar22,
            Scalar32 = s * m1.Scalar32,
            Scalar03 = s * m1.Scalar03,
            Scalar13 = s * m1.Scalar13,
            Scalar23 = s * m1.Scalar23,
            Scalar33 = s * m1.Scalar33
        };

        return m;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix4<T> operator /(SquareMatrix4<T> m1, Scalar<T> s)
    {
        Debug.Assert(m1.IsValid() && s.IsValid());

        Debug.Assert(!s.IsZero());

        var m = new SquareMatrix4<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 / s,
            Scalar10 = m1.Scalar10 / s,
            Scalar20 = m1.Scalar20 / s,
            Scalar30 = m1.Scalar30 / s,
            Scalar01 = m1.Scalar01 / s,
            Scalar11 = m1.Scalar11 / s,
            Scalar21 = m1.Scalar21 / s,
            Scalar31 = m1.Scalar31 / s,
            Scalar02 = m1.Scalar02 / s,
            Scalar12 = m1.Scalar12 / s,
            Scalar22 = m1.Scalar22 / s,
            Scalar32 = m1.Scalar32 / s,
            Scalar03 = m1.Scalar03 / s,
            Scalar13 = m1.Scalar13 / s,
            Scalar23 = m1.Scalar23 / s,
            Scalar33 = m1.Scalar33 / s
        };

        return m;
    }

    
    public IScalarProcessor<T> ScalarProcessor 
        => Scalar00.ScalarProcessor;

    public int VSpaceDimensions 
        => 4;

    public Scalar<T> Scalar00 { get; set; }

    public Scalar<T> Scalar01 { get; set; }

    public Scalar<T> Scalar02 { get; set; }

    public Scalar<T> Scalar03 { get; set; }

    public Scalar<T> Scalar10 { get; set; }

    public Scalar<T> Scalar11 { get; set; }

    public Scalar<T> Scalar12 { get; set; }

    public Scalar<T> Scalar13 { get; set; }

    public Scalar<T> Scalar20 { get; set; }

    public Scalar<T> Scalar21 { get; set; }

    public Scalar<T> Scalar22 { get; set; }

    public Scalar<T> Scalar23 { get; set; }

    public Scalar<T> Scalar30 { get; set; }

    public Scalar<T> Scalar31 { get; set; }

    public Scalar<T> Scalar32 { get; set; }

    public Scalar<T> Scalar33 { get; set; }

    public Scalar<T> this[int i, int j]
    {
        get
        {
            return i switch
            {
                0 => j switch
                {
                    0 => Scalar00,
                    1 => Scalar01,
                    2 => Scalar02,
                    3 => Scalar03,
                    _ => throw new IndexOutOfRangeException(nameof(j))
                },

                1 => j switch
                {
                    0 => Scalar10,
                    1 => Scalar11,
                    2 => Scalar12,
                    3 => Scalar13,
                    _ => throw new IndexOutOfRangeException(nameof(j))
                },

                2 => j switch
                {
                    0 => Scalar20,
                    1 => Scalar21,
                    2 => Scalar22,
                    3 => Scalar23,
                    _ => throw new IndexOutOfRangeException(nameof(j))
                },

                3 => j switch
                {
                    0 => Scalar30,
                    1 => Scalar31,
                    2 => Scalar32,
                    3 => Scalar33,
                    _ => throw new IndexOutOfRangeException(nameof(j))
                },

                _ =>
                    throw new IndexOutOfRangeException(nameof(i))
            };
        }
        set
        {
            Debug.Assert(value.IsValid());

            switch (i)
            {
                case 0 when j == 0:
                    Scalar00 = value;
                    break;
                case 0 when j == 1:
                    Scalar01 = value;
                    break;
                case 0 when j == 2:
                    Scalar02 = value;
                    break;
                case 0 when j == 3:
                    Scalar03 = value;
                    break;

                case 1 when j == 0:
                    Scalar10 = value;
                    break;
                case 1 when j == 1:
                    Scalar11 = value;
                    break;
                case 1 when j == 2:
                    Scalar12 = value;
                    break;
                case 1 when j == 3:
                    Scalar13 = value;
                    break;

                case 2 when j == 0:
                    Scalar20 = value;
                    break;
                case 2 when j == 1:
                    Scalar21 = value;
                    break;
                case 2 when j == 2:
                    Scalar22 = value;
                    break;
                case 2 when j == 3:
                    Scalar23 = value;
                    break;

                case 3 when j == 0:
                    Scalar30 = value;
                    break;
                case 3 when j == 1:
                    Scalar31 = value;
                    break;
                case 3 when j == 2:
                    Scalar32 = value;
                    break;
                case 3 when j == 3:
                    Scalar33 = value;
                    break;

                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// The squared Frobenius norm of this matrix
    /// </summary>
    public Scalar<T> FNormSquared
        => Scalar00 * Scalar00 +
           Scalar01 * Scalar01 +
           Scalar02 * Scalar02 +
           Scalar03 * Scalar03 +

           Scalar10 * Scalar10 +
           Scalar11 * Scalar11 +
           Scalar12 * Scalar12 +
           Scalar13 * Scalar13 +

           Scalar20 * Scalar20 +
           Scalar21 * Scalar21 +
           Scalar22 * Scalar22 +
           Scalar23 * Scalar23 +

           Scalar30 * Scalar30 +
           Scalar31 * Scalar31 +
           Scalar32 * Scalar32 +
           Scalar33 * Scalar33;

    /// <summary>
    /// The Frobenius norm of this matrix
    /// </summary>
    public Scalar<T> FNorm
        => FNormSquared.Sqrt();

    public bool IsExactIdentity
    {
        get
        {
            if (!Scalar00.IsOne()) return false;
            if (!Scalar10.IsZero()) return false;
            if (!Scalar20.IsZero()) return false;
            if (!Scalar30.IsZero()) return false;

            if (!Scalar01.IsZero()) return false;
            if (!Scalar11.IsOne()) return false;
            if (!Scalar21.IsZero()) return false;
            if (!Scalar31.IsZero()) return false;

            if (!Scalar02.IsZero()) return false;
            if (!Scalar12.IsZero()) return false;
            if (!Scalar22.IsOne()) return false;
            if (!Scalar32.IsZero()) return false;

            if (!Scalar03.IsZero()) return false;
            if (!Scalar13.IsZero()) return false;
            if (!Scalar23.IsZero()) return false;
            if (!Scalar33.IsOne()) return false;

            return true;
        }
    }

    public bool IsIdentity
    {
        get
        {
            if (!Scalar00.IsOne()) return false;
            if (!Scalar10.IsZero()) return false;
            if (!Scalar20.IsZero()) return false;
            if (!Scalar30.IsZero()) return false;

            if (!Scalar01.IsZero()) return false;
            if (!Scalar11.IsOne()) return false;
            if (!Scalar21.IsZero()) return false;
            if (!Scalar31.IsZero()) return false;

            if (!Scalar02.IsZero()) return false;
            if (!Scalar12.IsZero()) return false;
            if (!Scalar22.IsOne()) return false;
            if (!Scalar32.IsZero()) return false;

            if (!Scalar03.IsZero()) return false;
            if (!Scalar13.IsZero()) return false;
            if (!Scalar23.IsZero()) return false;
            if (!Scalar33.IsOne()) return false;

            return true;
        }
    }

    public bool ContainsScaling
    {
        get
        {
            var x = Scalar00 * Scalar00 + Scalar01 * Scalar01 + Scalar02 * Scalar02;
            if (!(x - ScalarProcessor.One).IsZero()) return false;

            x = Scalar10 * Scalar10 + Scalar11 * Scalar11 + Scalar12 * Scalar12;
            if (!(x - ScalarProcessor.One).IsZero()) return false;

            x = Scalar20 * Scalar20 + Scalar21 * Scalar21 + Scalar22 * Scalar22;
            if (!(x - ScalarProcessor.One).IsZero()) return false;

            return true;
        }
    }

    /// <summary>
    /// http://www.euclideanspace.com/maths/algebra/matrix/functions/determinant/fourD/index.htm
    /// </summary>
    public Scalar<T> Determinant
    {
        get
        {
            var m0 = Scalar00;
            var m1 = Scalar10;
            var m2 = Scalar20;
            var m3 = Scalar30;
            var m4 = Scalar01;
            var m5 = Scalar11;
            var m6 = Scalar21;
            var m7 = Scalar31;
            var m8 = Scalar02;
            var m9 = Scalar12;
            var m10 = Scalar22;
            var m11 = Scalar32;

            return
                ((m3 * m6 - m2 * m7) * m9 - (m3 * m5 + m1 * m7) * m10 + (m2 * m5 - m1 * m6) * m11) * Scalar03 -
                ((m3 * m6 + m2 * m7) * m8 + (m3 * m4 - m0 * m7) * m10 - (m2 * m4 + m0 * m6) * m11) * Scalar13 +
                ((m3 * m5 - m1 * m7) * m8 - (m3 * m4 + m0 * m7) * m9 + (m1 * m4 - m0 * m5) * m11) * Scalar23 -
                ((m2 * m5 + m1 * m6) * m8 + (m2 * m4 - m0 * m6) * m9 - (m1 * m4 + m0 * m5) * m10) * Scalar33;
        }
    }

    public bool SwapsHandedness
        => Determinant.IsNegative();

    public LinVector3D<T> UpperRightBlock3X1
    {
        get => LinVector3D<T>.Create(Scalar30, Scalar31, Scalar32);
        set
        {
            Scalar30 = value.X;
            Scalar31 = value.Y;
            Scalar32 = value.Z;
        }
    }

    public SquareMatrix3<T> UpperLeftBlock3X3
    {
        get
        {
            var x3 = new SquareMatrix3<T>(ScalarProcessor)
            {
                Scalar00 = Scalar00,
                Scalar10 = Scalar10,
                Scalar20 = Scalar20,
                Scalar01 = Scalar01,
                Scalar11 = Scalar11,
                Scalar21 = Scalar21,
                Scalar02 = Scalar02,
                Scalar12 = Scalar12,
                Scalar22 = Scalar22
            };
            return x3;
        }
        set
        {
            Scalar00 = value.Scalar00;
            Scalar10 = value.Scalar10;
            Scalar20 = value.Scalar20;

            Scalar01 = value.Scalar01;
            Scalar11 = value.Scalar11;
            Scalar21 = value.Scalar21;

            Scalar02 = value.Scalar02;
            Scalar12 = value.Scalar12;
            Scalar22 = value.Scalar22;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4(IScalarProcessor<T> scalarProcessor)
    {
        Scalar00 = scalarProcessor.Zero;
        Scalar10 = scalarProcessor.Zero;
        Scalar20 = scalarProcessor.Zero;
        Scalar30 = scalarProcessor.Zero;

        Scalar01 = scalarProcessor.Zero;
        Scalar11 = scalarProcessor.Zero;
        Scalar21 = scalarProcessor.Zero;
        Scalar31 = scalarProcessor.Zero;

        Scalar02 = scalarProcessor.Zero;
        Scalar12 = scalarProcessor.Zero;
        Scalar22 = scalarProcessor.Zero;
        Scalar32 = scalarProcessor.Zero;

        Scalar03 = scalarProcessor.Zero;
        Scalar13 = scalarProcessor.Zero;
        Scalar23 = scalarProcessor.Zero;
        Scalar33 = scalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4(SquareMatrix4<T> m1)
    {
        Scalar00 = m1.Scalar00;
        Scalar10 = m1.Scalar10;
        Scalar20 = m1.Scalar20;
        Scalar30 = m1.Scalar30;

        Scalar01 = m1.Scalar01;
        Scalar11 = m1.Scalar11;
        Scalar21 = m1.Scalar21;
        Scalar31 = m1.Scalar31;

        Scalar02 = m1.Scalar02;
        Scalar12 = m1.Scalar12;
        Scalar22 = m1.Scalar22;
        Scalar32 = m1.Scalar32;

        Scalar03 = m1.Scalar03;
        Scalar13 = m1.Scalar13;
        Scalar23 = m1.Scalar23;
        Scalar33 = m1.Scalar33;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public SquareMatrix4(Matrix4x4 m1)
    //{
    //    Scalar00 = m1.M11;
    //    Scalar10 = m1.M21;
    //    Scalar20 = m1.M31;
    //    Scalar30 = m1.M41;

    //    Scalar01 = m1.M12;
    //    Scalar11 = m1.M22;
    //    Scalar21 = m1.M32;
    //    Scalar31 = m1.M42;

    //    Scalar02 = m1.M13;
    //    Scalar12 = m1.M23;
    //    Scalar22 = m1.M33;
    //    Scalar32 = m1.M43;

    //    Scalar03 = m1.M14;
    //    Scalar13 = m1.M24;
    //    Scalar23 = m1.M34;
    //    Scalar33 = m1.M44;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4(Scalar<T>[,] m1)
    {
        Scalar00 = m1[0, 0];
        Scalar10 = m1[1, 0];
        Scalar20 = m1[2, 0];
        Scalar30 = m1[3, 0];

        Scalar01 = m1[0, 1];
        Scalar11 = m1[1, 1];
        Scalar21 = m1[2, 1];
        Scalar31 = m1[3, 1];

        Scalar02 = m1[0, 2];
        Scalar12 = m1[1, 2];
        Scalar22 = m1[2, 2];
        Scalar32 = m1[3, 2];

        Scalar03 = m1[0, 3];
        Scalar13 = m1[1, 3];
        Scalar23 = m1[2, 3];
        Scalar33 = m1[3, 3];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar00.IsValid() &&
               Scalar10.IsValid() &&
               Scalar20.IsValid() &&
               Scalar30.IsValid() &&
               Scalar01.IsValid() &&
               Scalar11.IsValid() &&
               Scalar21.IsValid() &&
               Scalar31.IsValid() &&
               Scalar02.IsValid() &&
               Scalar12.IsValid() &&
               Scalar22.IsValid() &&
               Scalar32.IsValid() &&
               Scalar03.IsValid() &&
               Scalar13.IsValid() &&
               Scalar23.IsValid() &&
               Scalar33.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> ResetToIdentity()
    {
        Scalar00 = ScalarProcessor.One;
        Scalar10 = ScalarProcessor.Zero;
        Scalar20 = ScalarProcessor.Zero;
        Scalar30 = ScalarProcessor.Zero;

        Scalar01 = ScalarProcessor.Zero;
        Scalar11 = ScalarProcessor.One;
        Scalar21 = ScalarProcessor.Zero;
        Scalar31 = ScalarProcessor.Zero;

        Scalar02 = ScalarProcessor.Zero;
        Scalar12 = ScalarProcessor.Zero;
        Scalar22 = ScalarProcessor.One;
        Scalar32 = ScalarProcessor.Zero;

        Scalar03 = ScalarProcessor.Zero;
        Scalar13 = ScalarProcessor.Zero;
        Scalar23 = ScalarProcessor.Zero;
        Scalar33 = ScalarProcessor.One;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> ResetTo(SquareMatrix4<T> m1)
    {
        Scalar00 = m1.Scalar00;
        Scalar10 = m1.Scalar10;
        Scalar20 = m1.Scalar20;
        Scalar30 = m1.Scalar30;

        Scalar01 = m1.Scalar01;
        Scalar11 = m1.Scalar11;
        Scalar21 = m1.Scalar21;
        Scalar31 = m1.Scalar31;

        Scalar02 = m1.Scalar02;
        Scalar12 = m1.Scalar12;
        Scalar22 = m1.Scalar22;
        Scalar32 = m1.Scalar32;

        Scalar03 = m1.Scalar03;
        Scalar13 = m1.Scalar13;
        Scalar23 = m1.Scalar23;
        Scalar33 = m1.Scalar33;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> SelfSwapRows(int rowIndex1, int rowIndex2)
    {
        var row1 = RowToLinVector4D(rowIndex1);
        var row2 = RowToLinVector4D(rowIndex2);

        SetRow(rowIndex1, row2);
        SetRow(rowIndex2, row1);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> SelfSwapColumns(int colIndex1, int colIndex2)
    {
        var col1 = RowToLinVector4D(colIndex1);
        var col2 = RowToLinVector4D(colIndex2);

        SetRow(colIndex1, col2);
        SetRow(colIndex2, col1);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> SelfTranspose()
    {
        var s = Scalar10;
        Scalar10 = Scalar01;
        Scalar01 = s;

        s = Scalar20;
        Scalar20 = Scalar02;
        Scalar02 = s;

        s = Scalar30;
        Scalar30 = Scalar03;
        Scalar03 = s;

        s = Scalar21;
        Scalar21 = Scalar12;
        Scalar12 = s;

        s = Scalar31;
        Scalar31 = Scalar13;
        Scalar13 = s;

        s = Scalar32;
        Scalar32 = Scalar23;
        Scalar23 = s;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> Transpose()
    {
        return new SquareMatrix4<T>(ScalarProcessor)
        {
            Scalar00 = Scalar00,
            Scalar10 = Scalar01,
            Scalar20 = Scalar02,
            Scalar30 = Scalar03,
            Scalar01 = Scalar10,
            Scalar11 = Scalar11,
            Scalar21 = Scalar12,
            Scalar31 = Scalar13,
            Scalar02 = Scalar20,
            Scalar12 = Scalar21,
            Scalar22 = Scalar22,
            Scalar32 = Scalar23,
            Scalar03 = Scalar30,
            Scalar13 = Scalar31,
            Scalar23 = Scalar32,
            Scalar33 = Scalar33
        };
    }

    public SquareMatrix4<T> Inverse()
    {
        var colIndex = new int[4];
        var rowIndex = new int[4];
        var pivotIndex = new[] { 0, 0, 0, 0 };
        var minValue = new SquareMatrix4<T>(this);

        for (var i = 0; i < 4; i++)
        {
            int iRow = 0, iCol = 0;
            var big = ScalarProcessor.Zero;

            // Choose pivot
            for (var j = 0; j < 4; j++)
            {
                if (pivotIndex[j] == 1) continue;

                for (var k = 0; k < 4; k++)
                {
                    if (pivotIndex[k] == 0)
                    {
                        if (minValue[j, k].Abs() < big) continue;

                        big = minValue[j, k].Abs();
                        iRow = j;
                        iCol = k;
                    }
                    else if (pivotIndex[k] > 1)
                        throw new InvalidOperationException("Singular matrix");
                }
            }

            ++pivotIndex[iCol];

            // Swap rows iRow and iCol for pivot
            if (iRow != iCol)
            {
                for (var k = 0; k < 4; ++k)
                {
                    (minValue[iRow, k], minValue[iCol, k]) = (minValue[iCol, k], minValue[iRow, k]);
                }
            }

            rowIndex[i] = iRow;
            colIndex[i] = iCol;
            if (minValue[iCol, iCol].IsZero())
                throw new InvalidOperationException("Singular matrix");

            // Set minValue[iCol, iCol] to one by scaling row iCol appropriately
            var pivotInv = ScalarProcessor.One / minValue[iCol, iCol];

            minValue[iCol, iCol] = ScalarProcessor.One;
            for (var j = 0; j < 4; j++)
                minValue[iCol, j] *= pivotInv;

            // Subtract this row from others to zero out their columns
            for (var j = 0; j < 4; j++)
            {
                if (j == iCol) continue;

                var save = minValue[j, iCol];

                minValue[j, iCol] = ScalarProcessor.Zero;
                for (var k = 0; k < 4; k++)
                    minValue[j, k] -= minValue[iCol, k] * save;
            }
        }

        // Swap columns to reflect permutation
        for (var j = 3; j >= 0; j--)
        {
            if (rowIndex[j] == colIndex[j]) continue;

            for (var k = 0; k < 4; k++)
            {
                (minValue[k, rowIndex[j]], minValue[k, colIndex[j]]) = (minValue[k, colIndex[j]], minValue[k, rowIndex[j]]);
            }
        }

        return minValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> InverseTranspose()
    {
        return Inverse().SelfTranspose();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> GetSquareMatrix4()
    {
        return new SquareMatrix4<T>(this);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Matrix4x4 GetMatrix4x4()
    //{
    //    return new Matrix4x4(
    //        (float)Scalar00,
    //        (float)Scalar01,
    //        (float)Scalar02,
    //        (float)Scalar03,

    //        (float)Scalar10,
    //        (float)Scalar11,
    //        (float)Scalar12,
    //        (float)Scalar13,

    //        (float)Scalar20,
    //        (float)Scalar21,
    //        (float)Scalar22,
    //        (float)Scalar23,

    //        (float)Scalar30,
    //        (float)Scalar31,
    //        (float)Scalar32,
    //        (float)Scalar33
    //    );
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Matrix<Complex> GetComplexMatrix()
    //{
    //    return Matrix<Complex>.Build.DenseOfArray(
    //        GetComplexArray2D()
    //    );
    //}

    public Scalar<T>[,] GetArray2D()
    {
        var array = new Scalar<T>[4, 4];

        array[0, 0] = Scalar00;
        array[1, 0] = Scalar10;
        array[2, 0] = Scalar20;
        array[3, 0] = Scalar30;

        array[0, 1] = Scalar01;
        array[1, 1] = Scalar11;
        array[2, 1] = Scalar21;
        array[3, 1] = Scalar31;

        array[0, 2] = Scalar02;
        array[1, 2] = Scalar12;
        array[2, 2] = Scalar22;
        array[3, 2] = Scalar32;

        array[0, 3] = Scalar03;
        array[1, 3] = Scalar13;
        array[2, 3] = Scalar23;
        array[3, 3] = Scalar33;

        return array;
    }

    public ComplexNumber<T>[,] GetComplexArray2D()
    {
        var array = new ComplexNumber<T>[4, 4];

        array[0, 0] = ScalarProcessor.CreateComplexNumberReal(Scalar00);
        array[1, 0] = ScalarProcessor.CreateComplexNumberReal(Scalar10);
        array[2, 0] = ScalarProcessor.CreateComplexNumberReal(Scalar20);
        array[3, 0] = ScalarProcessor.CreateComplexNumberReal(Scalar30);

        array[0, 1] = ScalarProcessor.CreateComplexNumberReal(Scalar01);
        array[1, 1] = ScalarProcessor.CreateComplexNumberReal(Scalar11);
        array[2, 1] = ScalarProcessor.CreateComplexNumberReal(Scalar21);
        array[3, 1] = ScalarProcessor.CreateComplexNumberReal(Scalar31);

        array[0, 2] = ScalarProcessor.CreateComplexNumberReal(Scalar02);
        array[1, 2] = ScalarProcessor.CreateComplexNumberReal(Scalar12);
        array[2, 2] = ScalarProcessor.CreateComplexNumberReal(Scalar22);
        array[3, 2] = ScalarProcessor.CreateComplexNumberReal(Scalar32);

        array[0, 3] = ScalarProcessor.CreateComplexNumberReal(Scalar03);
        array[1, 3] = ScalarProcessor.CreateComplexNumberReal(Scalar13);
        array[2, 3] = ScalarProcessor.CreateComplexNumberReal(Scalar23);
        array[3, 3] = ScalarProcessor.CreateComplexNumberReal(Scalar33);

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4D<T> DiagonalToLinVector4D()
    {
        return LinVector4D<T>.Create(Scalar00,
            Scalar11,
            Scalar22,
            Scalar33);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4D<T> RowToLinVector4D(int rowIndex)
    {
        return rowIndex switch
        {
            0 => LinVector4D<T>.Create(Scalar00, Scalar01, Scalar02, Scalar03),
            1 => LinVector4D<T>.Create(Scalar10, Scalar11, Scalar12, Scalar13),
            2 => LinVector4D<T>.Create(Scalar20, Scalar21, Scalar22, Scalar23),
            3 => LinVector4D<T>.Create(Scalar30, Scalar31, Scalar32, Scalar33),
            _ => throw new IndexOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4D<T> ColumnToLinVector4D(int colIndex)
    {
        return colIndex switch
        {
            0 => LinVector4D<T>.Create(Scalar00, Scalar10, Scalar20, Scalar30),
            1 => LinVector4D<T>.Create(Scalar01, Scalar11, Scalar21, Scalar31),
            2 => LinVector4D<T>.Create(Scalar02, Scalar12, Scalar22, Scalar32),
            3 => LinVector4D<T>.Create(Scalar03, Scalar13, Scalar23, Scalar33),
            _ => throw new IndexOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> SetDiagonal(IQuad<Scalar<T>> vector)
    {
        Scalar00 = vector.Item1;
        Scalar11 = vector.Item2;
        Scalar22 = vector.Item3;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> SetRow(int rowIndex, IQuad<Scalar<T>> vector)
    {
        if (rowIndex == 0)
        {
            Scalar00 = vector.Item1;
            Scalar01 = vector.Item2;
            Scalar02 = vector.Item3;
            Scalar03 = vector.Item4;

            return this;
        }

        if (rowIndex == 1)
        {
            Scalar10 = vector.Item1;
            Scalar11 = vector.Item2;
            Scalar12 = vector.Item3;
            Scalar13 = vector.Item4;

            return this;
        }

        if (rowIndex == 2)
        {
            Scalar20 = vector.Item1;
            Scalar21 = vector.Item2;
            Scalar22 = vector.Item3;
            Scalar23 = vector.Item4;

            return this;
        }

        if (rowIndex == 3)
        {
            Scalar30 = vector.Item1;
            Scalar31 = vector.Item2;
            Scalar32 = vector.Item3;
            Scalar33 = vector.Item4;

            return this;
        }

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> SetColumn(int colIndex, IQuad<Scalar<T>> vector)
    {
        if (colIndex == 0)
        {
            Scalar00 = vector.Item1;
            Scalar10 = vector.Item2;
            Scalar20 = vector.Item3;
            Scalar30 = vector.Item4;

            return this;
        }

        if (colIndex == 1)
        {
            Scalar01 = vector.Item1;
            Scalar11 = vector.Item2;
            Scalar21 = vector.Item3;
            Scalar31 = vector.Item4;

            return this;
        }

        if (colIndex == 2)
        {
            Scalar02 = vector.Item1;
            Scalar12 = vector.Item2;
            Scalar22 = vector.Item3;
            Scalar32 = vector.Item4;

            return this;
        }

        if (colIndex == 3)
        {
            Scalar03 = vector.Item1;
            Scalar13 = vector.Item2;
            Scalar23 = vector.Item3;
            Scalar33 = vector.Item4;

            return this;
        }

        throw new InvalidOperationException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4D<T> MapProjectivePoint(LinVector3D<T> point)
    {
        var x = Scalar00 * point.X + Scalar01 * point.Y + Scalar02 * point.Z + Scalar03;
        var y = Scalar10 * point.X + Scalar11 * point.Y + Scalar12 * point.Z + Scalar13;
        var z = Scalar20 * point.X + Scalar21 * point.Y + Scalar22 * point.Z + Scalar23;
        var w = Scalar30 * point.X + Scalar31 * point.Y + Scalar32 * point.Z + Scalar33;

        return LinVector4D<T>.Create(x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4D<T> MapProjectiveVector(LinVector3D<T> vector)
    {
        var x = Scalar00 * vector.X + Scalar01 * vector.Y + Scalar02 * vector.Z;
        var y = Scalar10 * vector.X + Scalar11 * vector.Y + Scalar12 * vector.Z;
        var z = Scalar20 * vector.X + Scalar21 * vector.Y + Scalar22 * vector.Z;
        var w = Scalar30 * vector.X + Scalar31 * vector.Y + Scalar32 * vector.Z;

        return LinVector4D<T>.Create(x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> MapPoint(ILinVector3D<T> point)
    {
        var pointX = Scalar00 * point.X + Scalar01 * point.Y + Scalar02 * point.Z + Scalar03;
        var pointY = Scalar10 * point.X + Scalar11 * point.Y + Scalar12 * point.Z + Scalar13;
        var pointZ = Scalar20 * point.X + Scalar21 * point.Y + Scalar22 * point.Z + Scalar23;
        var pointW = Scalar30 * point.X + Scalar31 * point.Y + Scalar32 * point.Z + Scalar33;

        if ((pointW - ScalarProcessor.One).IsZero())
            return LinVector3D<T>.Create(pointX, pointY, pointZ);

        var s = ScalarProcessor.One / pointW;
        return LinVector3D<T>.Create(pointX * s, pointY * s, pointZ * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> MapVector(ILinVector3D<T> vector)
    {
        return LinVector3D<T>.Create(Scalar00 * vector.X + Scalar01 * vector.Y + Scalar02 * vector.Z,
            Scalar10 * vector.X + Scalar11 * vector.Y + Scalar12 * vector.Z,
            Scalar20 * vector.X + Scalar21 * vector.Y + Scalar22 * vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> MapNormal(ILinVector3D<T> normal)
    {
        var invMatrix = Inverse();

        return LinVector3D<T>.Create(invMatrix.Scalar00 * normal.X + invMatrix.Scalar10 * normal.Y + invMatrix.Scalar20 * normal.Z,
            invMatrix.Scalar01 * normal.X + invMatrix.Scalar11 * normal.Y + invMatrix.Scalar21 * normal.Z,
            invMatrix.Scalar02 * normal.X + invMatrix.Scalar12 * normal.Y + invMatrix.Scalar22 * normal.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4<T> GetInverseAffineMap()
    {
        return Inverse();
    }
}