﻿using System;
using System.Diagnostics;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;
using MathNet.Numerics.LinearAlgebra;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;

public sealed class SquareMatrix4 //: IAffineMap3D
{
    
    public static SquareMatrix4 CreateZeroMatrix()
    {
        return new SquareMatrix4();
    }

    
    public static SquareMatrix4 CreateIdentityMatrix()
    {
        return new SquareMatrix4
        {
            Scalar00 = 1d,
            Scalar11 = 1d,
            Scalar22 = 1d,
            Scalar33 = 1d
        };
    }

    
    public static SquareMatrix4 CreateAffineMatrix3D(SquareMatrix3 m)
    {
        return new SquareMatrix4
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
            Scalar33 = 1d
        };
    }

    
    public static SquareMatrix4 CreateTranslationMatrix3D(double dx, double dy, double dz)
    {
        return new SquareMatrix4
        {
            Scalar00 = 1d,
            Scalar11 = 1d,
            Scalar22 = 1d,
            Scalar33 = 1d,
            Scalar03 = dx,
            Scalar13 = dy,
            Scalar23 = dz
        };
    }

    
    public static SquareMatrix4 CreateTranslationMatrix3D(ITriplet<double> translationVector)
    {
        return new SquareMatrix4
        {
            Scalar00 = 1d,
            Scalar11 = 1d,
            Scalar22 = 1d,
            Scalar33 = 1d,
            Scalar03 = translationVector.Item1,
            Scalar13 = translationVector.Item2,
            Scalar23 = translationVector.Item3
        };
    }

    
    public static SquareMatrix4 CreateScalingMatrix3D(double sx, double sy, double sz)
    {
        return new SquareMatrix4
        {
            Scalar00 = sx,
            Scalar11 = sy,
            Scalar22 = sz,
            Scalar33 = 1d
        };
    }

    
    public static SquareMatrix4 CreateScalingMatrix3D(double s)
    {
        return new SquareMatrix4
        {
            Scalar00 = s,
            Scalar11 = s,
            Scalar22 = s,
            Scalar33 = 1d
        };
    }

    
    public static SquareMatrix4 CreateXyReflectionMatrix3D()
    {
        return new SquareMatrix4
        {
            Scalar00 = 1d,
            Scalar11 = 1d,
            Scalar22 = -1d,
            Scalar33 = 1d
        };
    }

    
    public static SquareMatrix4 CreateYzReflectionMatrix3D()
    {
        return new SquareMatrix4
        {
            Scalar00 = -1d,
            Scalar11 = 1d,
            Scalar22 = 1d,
            Scalar33 = 1d
        };
    }

    
    public static SquareMatrix4 CreateZxReflectionMatrix3D()
    {
        return new SquareMatrix4
        {
            Scalar00 = 1d,
            Scalar11 = -1d,
            Scalar22 = 1d,
            Scalar33 = 1d
        };
    }

    
    public static SquareMatrix4 CreateXReflectionMatrix3D()
    {
        return new SquareMatrix4
        {
            Scalar00 = 1d,
            Scalar11 = -1d,
            Scalar22 = -1d,
            Scalar33 = 1d
        };
    }

    
    public static SquareMatrix4 CreateYReflectionMatrix3D()
    {
        return new SquareMatrix4
        {
            Scalar00 = -1d,
            Scalar11 = 1d,
            Scalar22 = -1d,
            Scalar33 = 1d
        };
    }

    
    public static SquareMatrix4 CreateZReflectionMatrix3D()
    {
        return new SquareMatrix4
        {
            Scalar00 = -1d,
            Scalar11 = -1d,
            Scalar22 = 1d,
            Scalar33 = 1d
        };
    }

    
    public static SquareMatrix4 CreateOriginReflectionMatrix3D()
    {
        return new SquareMatrix4
        {
            Scalar00 = -1d,
            Scalar11 = -1d,
            Scalar22 = -1d,
            Scalar33 = 1d
        };
    }

    public static SquareMatrix4 CreateXRotationMatrix3D(LinFloat64Angle angle)
    {
        Debug.Assert(angle.IsValid());

        if (angle.IsZero())
            return CreateIdentityMatrix();

        var m = new SquareMatrix4();

        var cosAngle = angle.CosValue;
        var sinAngle = angle.SinValue;

        m.Scalar00 = 1d;

        m.Scalar11 = cosAngle;
        m.Scalar21 = sinAngle;

        m.Scalar12 = -sinAngle;
        m.Scalar22 = cosAngle;

        m.Scalar33 = 1d;

        return m;
    }

    public static SquareMatrix4 CreateYRotationMatrix3D(LinFloat64Angle angle)
    {
        Debug.Assert(angle.IsValid());

        if (angle.IsZero())
            return CreateIdentityMatrix();

        var m = new SquareMatrix4();

        var cosAngle = angle.CosValue;
        var sinAngle = angle.SinValue;

        m.Scalar00 = cosAngle;
        m.Scalar20 = -sinAngle;

        m.Scalar11 = 1d;

        m.Scalar02 = sinAngle;
        m.Scalar22 = cosAngle;

        m.Scalar33 = 1d;

        return m;
    }

    public static SquareMatrix4 CreateZRotationMatrix3D(LinFloat64Angle angle)
    {
        Debug.Assert(angle.IsValid());

        if (angle.IsZero())
            return CreateIdentityMatrix();

        var m = new SquareMatrix4();

        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        m.Scalar00 = cosAngle;
        m.Scalar10 = sinAngle;

        m.Scalar01 = -sinAngle;
        m.Scalar11 = cosAngle;

        m.Scalar22 = 1d;

        m.Scalar33 = 1d;

        return m;
    }

    public static SquareMatrix4 CreateRotationMatrix3D(LinFloat64Quaternion quaternion)
    {
        return quaternion.ToSquareMatrix4();
    }

    public static SquareMatrix4 CreateRotationMatrix3D(ILinFloat64Vector3D unitAxis, LinFloat64Angle angle)
    {
        if (angle.IsZero())
            return CreateIdentityMatrix();

        Debug.Assert(unitAxis.IsUnitVector());

        var m = new SquareMatrix4();

        var x = unitAxis.X;
        var y = unitAxis.Y;
        var z = unitAxis.Z;
        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();
        var oneMinusCosAngle = 1d - cosAngle;
        var xx = x * x;
        var yy = y * y;
        var zz = z * z;
        var xy = x * y;
        var xz = x * z;
        var yz = y * z;

        m.Scalar00 = xx + (1d - xx) * cosAngle;
        m.Scalar10 = xy * oneMinusCosAngle + z * sinAngle;
        m.Scalar20 = xz * oneMinusCosAngle - y * sinAngle;

        m.Scalar01 = xy * oneMinusCosAngle - z * sinAngle;
        m.Scalar11 = yy + (1d - yy) * cosAngle;
        m.Scalar21 = yz * oneMinusCosAngle + x * sinAngle;

        m.Scalar02 = xz * oneMinusCosAngle + y * sinAngle;
        m.Scalar12 = yz * oneMinusCosAngle - x * sinAngle;
        m.Scalar22 = zz + (1d - zz) * cosAngle;

        m.Scalar33 = 1d;

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
    public static SquareMatrix4 CreateRotationMatrix3D(ILinFloat64Vector3D srcUnitVector, ILinFloat64Vector3D dstUnitVector)
    {
        var angle =
            srcUnitVector.GetAngle(dstUnitVector);

        var axis =
            srcUnitVector.VectorCross(dstUnitVector);

        if (!axis.IsZeroVector())
            return CreateRotationMatrix3D(axis.ToUnitLinVector3D(), angle);

        return angle.RadiansValue.IsZero()
            ? CreateIdentityMatrix()
            : CreateRotationMatrix3D(
                srcUnitVector.GetUnitNormal(),
                angle
            );
    }

    /// <summary>
    /// Create a rotation matrix that rotates the unit vectors <see cref="basisVectors"/>
    /// into the unit vectors <see cref="unitVector1"/>, <see cref="unitVector1"/>
    /// </summary>
    /// <param name="basisVectors"></param>
    /// <param name="unitVector1"></param>
    /// <param name="unitVector2"></param>
    /// <returns></returns>
    public static SquareMatrix4 CreateRotationMatrix3D(LinBasisVectorPair3D basisVectors, ILinFloat64Vector3D unitVector1, ILinFloat64Vector3D unitVector2)
    {
        var q = 
            basisVectors.VectorPairToVectorPairRotationQuaternion(
                unitVector1, 
                unitVector2
            );

        return q.ToSquareMatrix4();
    }
    
    /// <summary>
    /// Create an affine homogeneous transformation matrix given the columns
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <param name="c3"></param>
    /// <returns></returns>
    public static SquareMatrix4 CreateAffineFromColumns(ITriplet<double> c1, ITriplet<double> c2, ITriplet<double> c3)
    {
        return new SquareMatrix4()
        {
            Scalar00 = c1.Item1,
            Scalar10 = c1.Item2,
            Scalar20 = c1.Item3,
            Scalar30 = 0d,

            Scalar01 = c2.Item1,
            Scalar11 = c2.Item2,
            Scalar21 = c2.Item3,
            Scalar31 = 0d,

            Scalar02 = c3.Item1,
            Scalar12 = c3.Item2,
            Scalar22 = c3.Item3,
            Scalar32 = 0d,

            Scalar03 = 0d,
            Scalar13 = 0d,
            Scalar23 = 0d,
            Scalar33 = 1d
        };
    }

    /// <summary>
    /// Create an affine homogeneous transformation matrix given the columns
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <param name="c3"></param>
    /// <param name="c4"></param>
    /// <returns></returns>
    public static SquareMatrix4 CreateAffineFromColumns(ITriplet<double> c1, ITriplet<double> c2, ITriplet<double> c3, ITriplet<double> c4)
    {
        return new SquareMatrix4()
        {
            Scalar00 = c1.Item1,
            Scalar10 = c1.Item2,
            Scalar20 = c1.Item3,
            Scalar30 = 0d,

            Scalar01 = c2.Item1,
            Scalar11 = c2.Item2,
            Scalar21 = c2.Item3,
            Scalar31 = 0d,

            Scalar02 = c3.Item1,
            Scalar12 = c3.Item2,
            Scalar22 = c3.Item3,
            Scalar32 = 0d,

            Scalar03 = c4.Item1,
            Scalar13 = c4.Item2,
            Scalar23 = c4.Item3,
            Scalar33 = 1d
        };
    }


    
    public static SquareMatrix4 operator -(SquareMatrix4 m1)
    {
        return new SquareMatrix4
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

    
    public static SquareMatrix4 operator +(SquareMatrix4 m1, SquareMatrix4 m2)
    {
        return new SquareMatrix4
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

    
    public static SquareMatrix4 operator -(SquareMatrix4 m1, SquareMatrix4 m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        var m = new SquareMatrix4
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

    
    public static SquareMatrix4 operator *(SquareMatrix4 m1, SquareMatrix4 m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        var m = new SquareMatrix4
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

    
    public static LinFloat64Vector4D operator *(SquareMatrix4 m, ILinFloat64Vector4D vector)
    {
        var x = vector.X;
        var y = vector.Y;
        var z = vector.Z;
        var w = vector.W;

        return LinFloat64Vector4D.Create(m.Scalar00 * x + m.Scalar01 * y + m.Scalar02 * z + m.Scalar03 * w,
            m.Scalar10 * x + m.Scalar11 * y + m.Scalar12 * z + m.Scalar13 * w,
            m.Scalar20 * x + m.Scalar21 * y + m.Scalar22 * z + m.Scalar23 * w,
            m.Scalar30 * x + m.Scalar31 * y + m.Scalar32 * z + m.Scalar33 * w);
    }

    
    public static LinFloat64Vector4D operator *(ILinFloat64Vector4D vector, SquareMatrix4 m)
    {
        var x = vector.X;
        var y = vector.Y;
        var z = vector.Z;
        var w = vector.W;

        return LinFloat64Vector4D.Create(m.Scalar00 * x + m.Scalar10 * y + m.Scalar20 * z + m.Scalar30 * w,
            m.Scalar01 * x + m.Scalar11 * y + m.Scalar21 * z + m.Scalar31 * w,
            m.Scalar02 * x + m.Scalar12 * y + m.Scalar22 * z + m.Scalar32 * w,
            m.Scalar03 * x + m.Scalar13 * y + m.Scalar23 * z + m.Scalar33 * w);
    }

    
    public static SquareMatrix4 operator *(SquareMatrix4 m1, double s)
    {
        Debug.Assert(m1.IsValid() && !double.IsNaN(s));

        var m = new SquareMatrix4
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

    
    public static SquareMatrix4 operator *(double s, SquareMatrix4 m1)
    {
        Debug.Assert(m1.IsValid() && !double.IsNaN(s));

        var m = new SquareMatrix4
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

    
    public static SquareMatrix4 operator /(SquareMatrix4 m1, double s)
    {
        Debug.Assert(m1.IsValid() && !double.IsNaN(s));

        Debug.Assert(!s.IsZero());

        s = 1.0d / s;

        var m = new SquareMatrix4
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


    public double Scalar00 { get; set; }

    public double Scalar01 { get; set; }

    public double Scalar02 { get; set; }

    public double Scalar03 { get; set; }

    public double Scalar10 { get; set; }

    public double Scalar11 { get; set; }

    public double Scalar12 { get; set; }

    public double Scalar13 { get; set; }

    public double Scalar20 { get; set; }

    public double Scalar21 { get; set; }

    public double Scalar22 { get; set; }

    public double Scalar23 { get; set; }

    public double Scalar30 { get; set; }

    public double Scalar31 { get; set; }

    public double Scalar32 { get; set; }

    public double Scalar33 { get; set; }

    public double this[int i, int j]
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
            Debug.Assert(!double.IsNaN(value));

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
    public double FNormSquared
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
    public double FNorm
        => Math.Sqrt(FNormSquared);

    public bool IsIdentity()
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

    public bool IsNearIdentity(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (!Scalar00.IsNearOne(zeroEpsilon)) return false;
        if (!Scalar10.IsNearZero(zeroEpsilon)) return false;
        if (!Scalar20.IsNearZero(zeroEpsilon)) return false;
        if (!Scalar30.IsNearZero(zeroEpsilon)) return false;

        if (!Scalar01.IsNearZero(zeroEpsilon)) return false;
        if (!Scalar11.IsNearOne(zeroEpsilon)) return false;
        if (!Scalar21.IsNearZero(zeroEpsilon)) return false;
        if (!Scalar31.IsNearZero(zeroEpsilon)) return false;

        if (!Scalar02.IsNearZero(zeroEpsilon)) return false;
        if (!Scalar12.IsNearZero(zeroEpsilon)) return false;
        if (!Scalar22.IsNearOne(zeroEpsilon)) return false;
        if (!Scalar32.IsNearZero(zeroEpsilon)) return false;

        if (!Scalar03.IsNearZero(zeroEpsilon)) return false;
        if (!Scalar13.IsNearZero(zeroEpsilon)) return false;
        if (!Scalar23.IsNearZero(zeroEpsilon)) return false;
        if (!Scalar33.IsNearOne(zeroEpsilon)) return false;

        return true;
    }

    public bool ContainsScaling
    {
        get
        {
            var x = Scalar00 * Scalar00 + Scalar01 * Scalar01 + Scalar02 * Scalar02;
            if (!(x - 1.0d).IsZero()) return false;

            x = Scalar10 * Scalar10 + Scalar11 * Scalar11 + Scalar12 * Scalar12;
            if (!(x - 1.0d).IsZero()) return false;

            x = Scalar20 * Scalar20 + Scalar21 * Scalar21 + Scalar22 * Scalar22;
            if (!(x - 1.0d).IsZero()) return false;

            return true;
        }
    }

    /// <summary>
    /// http://www.euclideanspace.com/maths/algebra/matrix/functions/determinant/fourD/index.htm
    /// </summary>
    public double Determinant
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

    public LinFloat64Vector3D UpperRightBlock3X1
    {
        get => LinFloat64Vector3D.Create(Scalar30, Scalar31, Scalar32);
        set
        {
            Scalar30 = value.X;
            Scalar31 = value.Y;
            Scalar32 = value.Z;
        }
    }

    public SquareMatrix3 UpperLeftBlock3X3
    {
        get
        {
            var x3 = new SquareMatrix3
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


    
    public SquareMatrix4()
    {
        Scalar00 = 0d;
        Scalar10 = 0d;
        Scalar20 = 0d;
        Scalar30 = 0d;

        Scalar01 = 0d;
        Scalar11 = 0d;
        Scalar21 = 0d;
        Scalar31 = 0d;

        Scalar02 = 0d;
        Scalar12 = 0d;
        Scalar22 = 0d;
        Scalar32 = 0d;

        Scalar03 = 0d;
        Scalar13 = 0d;
        Scalar23 = 0d;
        Scalar33 = 0d;
    }

    
    public SquareMatrix4(SquareMatrix4 m1)
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

    
    public SquareMatrix4(Matrix4x4 m1)
    {
        Scalar00 = m1.M11;
        Scalar10 = m1.M21;
        Scalar20 = m1.M31;
        Scalar30 = m1.M41;

        Scalar01 = m1.M12;
        Scalar11 = m1.M22;
        Scalar21 = m1.M32;
        Scalar31 = m1.M42;

        Scalar02 = m1.M13;
        Scalar12 = m1.M23;
        Scalar22 = m1.M33;
        Scalar32 = m1.M43;

        Scalar03 = m1.M14;
        Scalar13 = m1.M24;
        Scalar23 = m1.M34;
        Scalar33 = m1.M44;
    }

    
    public SquareMatrix4(double[,] m1)
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

    
    public SquareMatrix4 ResetToIdentity()
    {
        Scalar00 = 1;
        Scalar10 = 0;
        Scalar20 = 0;
        Scalar30 = 0;

        Scalar01 = 0;
        Scalar11 = 1;
        Scalar21 = 0;
        Scalar31 = 0;

        Scalar02 = 0;
        Scalar12 = 0;
        Scalar22 = 1;
        Scalar32 = 0;

        Scalar03 = 0;
        Scalar13 = 0;
        Scalar23 = 0;
        Scalar33 = 1;

        return this;
    }

    
    public SquareMatrix4 ResetTo(SquareMatrix4 m1)
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

    
    public SquareMatrix4 SelfSwapRows(int rowIndex1, int rowIndex2)
    {
        var row1 = RowToTuple4D(rowIndex1);
        var row2 = RowToTuple4D(rowIndex2);

        SetRow(rowIndex1, row2);
        SetRow(rowIndex2, row1);

        return this;
    }

    
    public SquareMatrix4 SelfSwapColumns(int colIndex1, int colIndex2)
    {
        var col1 = RowToTuple4D(colIndex1);
        var col2 = RowToTuple4D(colIndex2);

        SetRow(colIndex1, col2);
        SetRow(colIndex2, col1);

        return this;
    }

    
    public SquareMatrix4 SelfTranspose()
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

    
    public SquareMatrix4 Transpose()
    {
        return new SquareMatrix4
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

    public SquareMatrix4 Inverse()
    {
        var colIndex = new int[4];
        var rowIndex = new int[4];
        var pivotIndex = new[] { 0, 0, 0, 0 };
        var minValue = new SquareMatrix4(this);

        for (var i = 0; i < 4; i++)
        {
            int iRow = 0, iCol = 0;
            var big = 0.0d;

            // Choose pivot
            for (var j = 0; j < 4; j++)
            {
                if (pivotIndex[j] == 1) continue;

                for (var k = 0; k < 4; k++)
                {
                    if (pivotIndex[k] == 0)
                    {
                        if (Math.Abs(minValue[j, k]) < big) continue;

                        big = Math.Abs(minValue[j, k]);
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
            var pivotInv = 1.0d / minValue[iCol, iCol];

            minValue[iCol, iCol] = 1.0d;
            for (var j = 0; j < 4; j++)
                minValue[iCol, j] *= pivotInv;

            // Subtract this row from others to zero out their columns
            for (var j = 0; j < 4; j++)
            {
                if (j == iCol) continue;

                var save = minValue[j, iCol];

                minValue[j, iCol] = 0.0d;
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

    
    public SquareMatrix4 InverseTranspose()
    {
        return Inverse().SelfTranspose();
    }


    
    public SquareMatrix4 GetSquareMatrix4()
    {
        return new SquareMatrix4(this);
    }

    
    public Matrix4x4 GetMatrix4x4()
    {
        return new Matrix4x4(
            (float)Scalar00,
            (float)Scalar01,
            (float)Scalar02,
            (float)Scalar03,

            (float)Scalar10,
            (float)Scalar11,
            (float)Scalar12,
            (float)Scalar13,

            (float)Scalar20,
            (float)Scalar21,
            (float)Scalar22,
            (float)Scalar23,

            (float)Scalar30,
            (float)Scalar31,
            (float)Scalar32,
            (float)Scalar33
        );
    }


    
    public Matrix<Complex> GetComplexMatrix()
    {
        return Matrix<Complex>.Build.DenseOfArray(
            GetComplexArray2D()
        );
    }

    public double[,] GetArray2D()
    {
        var array = new double[4, 4];

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

    public Complex[,] GetComplexArray2D()
    {
        var array = new Complex[4, 4];

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

    
    public LinFloat64Vector4D DiagonalToTuple4D()
    {
        return LinFloat64Vector4D.Create(Scalar00,
            Scalar11,
            Scalar22,
            Scalar33);
    }

    
    public LinFloat64Vector4D RowToTuple4D(int rowIndex)
    {
        return rowIndex switch
        {
            0 => LinFloat64Vector4D.Create(Scalar00, Scalar01, Scalar02, Scalar03),
            1 => LinFloat64Vector4D.Create(Scalar10, Scalar11, Scalar12, Scalar13),
            2 => LinFloat64Vector4D.Create(Scalar20, Scalar21, Scalar22, Scalar23),
            3 => LinFloat64Vector4D.Create(Scalar30, Scalar31, Scalar32, Scalar33),
            _ => throw new IndexOutOfRangeException()
        };
    }

    
    public LinFloat64Vector4D ColumnToTuple4D(int colIndex)
    {
        return colIndex switch
        {
            0 => LinFloat64Vector4D.Create(Scalar00, Scalar10, Scalar20, Scalar30),
            1 => LinFloat64Vector4D.Create(Scalar01, Scalar11, Scalar21, Scalar31),
            2 => LinFloat64Vector4D.Create(Scalar02, Scalar12, Scalar22, Scalar32),
            3 => LinFloat64Vector4D.Create(Scalar03, Scalar13, Scalar23, Scalar33),
            _ => throw new IndexOutOfRangeException()
        };
    }

    
    public SquareMatrix4 SetDiagonal(IQuad<double> vector)
    {
        Scalar00 = vector.Item1;
        Scalar11 = vector.Item2;
        Scalar22 = vector.Item3;

        return this;
    }

    
    public SquareMatrix4 SetRow(int rowIndex, IQuad<double> vector)
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

    
    public SquareMatrix4 SetColumn(int colIndex, IQuad<double> vector)
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


    
    public LinFloat64Vector4D MapProjectivePoint(LinFloat64Vector3D point)
    {
        var x = Scalar00 * point.X + Scalar01 * point.Y + Scalar02 * point.Z + Scalar03;
        var y = Scalar10 * point.X + Scalar11 * point.Y + Scalar12 * point.Z + Scalar13;
        var z = Scalar20 * point.X + Scalar21 * point.Y + Scalar22 * point.Z + Scalar23;
        var w = Scalar30 * point.X + Scalar31 * point.Y + Scalar32 * point.Z + Scalar33;

        return LinFloat64Vector4D.Create(x, y, z, w);
    }

    
    public LinFloat64Vector4D MapProjectiveVector(LinFloat64Vector3D vector)
    {
        var x = Scalar00 * vector.X + Scalar01 * vector.Y + Scalar02 * vector.Z;
        var y = Scalar10 * vector.X + Scalar11 * vector.Y + Scalar12 * vector.Z;
        var z = Scalar20 * vector.X + Scalar21 * vector.Y + Scalar22 * vector.Z;
        var w = Scalar30 * vector.X + Scalar31 * vector.Y + Scalar32 * vector.Z;

        return LinFloat64Vector4D.Create(x, y, z, w);
    }

    
    public LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point)
    {
        var pointX = Scalar00 * point.X + Scalar01 * point.Y + Scalar02 * point.Z + Scalar03;
        var pointY = Scalar10 * point.X + Scalar11 * point.Y + Scalar12 * point.Z + Scalar13;
        var pointZ = Scalar20 * point.X + Scalar21 * point.Y + Scalar22 * point.Z + Scalar23;
        var pointW = Scalar30 * point.X + Scalar31 * point.Y + Scalar32 * point.Z + Scalar33;

        if ((pointW - 1.0d).IsZero())
            return LinFloat64Vector3D.Create(pointX, pointY, pointZ);

        var s = 1.0d / pointW;
        return LinFloat64Vector3D.Create(pointX * s, pointY * s, pointZ * s);
    }

    
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return LinFloat64Vector3D.Create(Scalar00 * vector.X + Scalar01 * vector.Y + Scalar02 * vector.Z,
            Scalar10 * vector.X + Scalar11 * vector.Y + Scalar12 * vector.Z,
            Scalar20 * vector.X + Scalar21 * vector.Y + Scalar22 * vector.Z);
    }

    
    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        var invMatrix = Inverse();

        return LinFloat64Vector3D.Create(invMatrix.Scalar00 * normal.X + invMatrix.Scalar10 * normal.Y + invMatrix.Scalar20 * normal.Z,
            invMatrix.Scalar01 * normal.X + invMatrix.Scalar11 * normal.Y + invMatrix.Scalar21 * normal.Z,
            invMatrix.Scalar02 * normal.X + invMatrix.Scalar12 * normal.Y + invMatrix.Scalar22 * normal.Z);
    }

    //
    //public IAffineMap3D GetInverseAffineMap()
    //{
    //    return Inverse();
    //}
}