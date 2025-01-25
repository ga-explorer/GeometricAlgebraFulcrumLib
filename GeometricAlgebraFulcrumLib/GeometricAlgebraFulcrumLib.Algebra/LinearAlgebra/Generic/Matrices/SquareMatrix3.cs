using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;

public sealed class SquareMatrix3<T> :
    ILinearAlgebraElement<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> CreateIdentityMatrix(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix3<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.One,
            Scalar11 = scalarProcessor.One,
            Scalar22 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> CreateTranslationMatrix2D(Scalar<T> dx, Scalar<T> dy)
    {
        var scalarProcessor = dx.ScalarProcessor;

        var m = new SquareMatrix3<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.One,
            Scalar11 = scalarProcessor.One,
            Scalar02 = dx,
            Scalar12 = dy,
            Scalar22 = scalarProcessor.One
        };

        return m;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> CreateScalingMatrix2D(Scalar<T> sx, Scalar<T> sy)
    {
        return new SquareMatrix3<T>(sx.ScalarProcessor)
        {
            Scalar00 = sx,
            Scalar11 = sy,
            Scalar22 = sx.ScalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> CreateScalingMatrix2D(Scalar<T> s)
    {
        return new SquareMatrix3<T>(s.ScalarProcessor)
        {
            Scalar00 = s,
            Scalar11 = s,
            Scalar22 = s.ScalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> CreateXReflectionMatrix2D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix3<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.One,
            Scalar11 = scalarProcessor.MinusOne,
            Scalar22 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> CreateYReflectionMatrix2D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix3<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.MinusOne,
            Scalar11 = scalarProcessor.One,
            Scalar22 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> CreateOriginReflectionMatrix2D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix3<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.MinusOne,
            Scalar11 = scalarProcessor.MinusOne,
            Scalar22 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> CreateRotationMatrix2D(LinAngle<T> radianAngle)
    {
        var scalarProcessor = radianAngle.ScalarProcessor;

        var m = new SquareMatrix3<T>(scalarProcessor);

        var cosAngle = radianAngle.Cos();
        var sinAngle = radianAngle.Sin();

        m.Scalar00 = cosAngle;
        m.Scalar10 = sinAngle;
        m.Scalar01 = -sinAngle;
        m.Scalar11 = cosAngle;
        m.Scalar22 = scalarProcessor.One;

        return m;
    }

    public static SquareMatrix3<T> CreateAxisToVectorRotationMatrix3D(LinBasisVector3D axis, ILinVector3D<T> unitVector)
    {
        //Debug.Assert(unitVector.IsNearUnitVector());

        var scalarProcessor = unitVector.ScalarProcessor;

        var x = unitVector.X;
        var y = unitVector.Y;
        var z = unitVector.Z;

        if (axis == LinBasisVector3D.Px)
        {
            var x1 = 1d / (x + 1d);
            var yz = -y * z * x1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = x,
                Scalar10 = y,
                Scalar20 = z,

                Scalar01 = -y,
                Scalar11 = 1 - y * y * x1,
                Scalar21 = yz,

                Scalar02 = -z,
                Scalar12 = yz,
                Scalar22 = 1 - z * z * x1
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple3D.E1));

            return matrix;
        }

        if (axis == LinBasisVector3D.Nx)
        {
            var x1 = 1d / (x - 1d);
            var yz = y * z * x1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = -x,
                Scalar10 = -y,
                Scalar20 = -z,

                Scalar01 = y,
                Scalar11 = 1 + y * y * x1,
                Scalar21 = yz,

                Scalar02 = z,
                Scalar12 = yz,
                Scalar22 = 1 + z * z * x1
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple3D.NegativeE1));

            return matrix;
        }

        if (axis == LinBasisVector3D.Py)
        {
            var y1 = 1d / (y + 1d);
            var xz = -x * z * y1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = 1 - x * x * y1,
                Scalar10 = -x,
                Scalar20 = xz,

                Scalar01 = x,
                Scalar11 = y,
                Scalar21 = z,

                Scalar02 = xz,
                Scalar12 = -z,
                Scalar22 = 1 - z * z * y1
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple3D.E2));

            return matrix;
        }

        if (axis == LinBasisVector3D.Ny)
        {
            var y1 = 1d / (y - 1d);
            var xz = x * z * y1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = 1 + x * x * y1,
                Scalar10 = x,
                Scalar20 = xz,

                Scalar01 = -x,
                Scalar11 = -y,
                Scalar21 = -z,

                Scalar02 = xz,
                Scalar12 = z,
                Scalar22 = 1 + z * z * y1
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple3D.NegativeE2));

            return matrix;
        }

        if (axis == LinBasisVector3D.Pz)
        {
            var z1 = 1d / (z + 1d);
            var xy = -x * y * z1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = 1 - x * x * z1,
                Scalar10 = xy,
                Scalar20 = -x,

                Scalar01 = xy,
                Scalar11 = 1 - y * y * z1,
                Scalar21 = -y,

                Scalar02 = x,
                Scalar12 = y,
                Scalar22 = z
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple3D.E3));

            return matrix;
        }

        if (axis == LinBasisVector3D.Nz)
        {
            var z1 = 1d / (z - 1d);
            var xy = x * y * z1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = 1 + x * x * z1,
                Scalar10 = xy,
                Scalar20 = x,

                Scalar01 = xy,
                Scalar11 = 1 + y * y * z1,
                Scalar21 = y,

                Scalar02 = -x,
                Scalar12 = -y,
                Scalar22 = -z
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple3D.NegativeE3));

            return matrix;
        }

        throw new InvalidOperationException();
    }

    public static SquareMatrix3<T> CreateVectorToAxisRotationMatrix3D(ILinVector3D<T> unitVector, LinBasisVector3D axis)
    {
        //Debug.Assert(unitVector.IsValid() && unitVector.IsNearUnitVector());
        
        var scalarProcessor = unitVector.ScalarProcessor;

        var x = unitVector.X;
        var y = unitVector.Y;
        var z = unitVector.Z;

        if (axis == LinBasisVector3D.Px)
        {
            var x1 = 1d / (x + 1d);
            var yz = -y * z * x1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = x,
                Scalar01 = y,
                Scalar02 = z,

                Scalar10 = -y,
                Scalar11 = 1 - y * y * x1,
                Scalar12 = yz,

                Scalar20 = -z,
                Scalar21 = yz,
                Scalar22 = 1 - z * z * x1
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple3D.E1));

            return matrix;
        }

        if (axis == LinBasisVector3D.Nx)
        {
            var x1 = 1d / (x - 1d);
            var yz = y * z * x1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = -x,
                Scalar01 = -y,
                Scalar02 = -z,

                Scalar10 = y,
                Scalar11 = 1 + y * y * x1,
                Scalar12 = yz,

                Scalar20 = z,
                Scalar21 = yz,
                Scalar22 = 1 + z * z * x1
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple3D.NegativeE1));

            return matrix;
        }

        if (axis == LinBasisVector3D.Py)
        {
            var y1 = 1d / (y + 1d);
            var xz = -x * z * y1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = 1 - x * x * y1,
                Scalar01 = -x,
                Scalar02 = xz,

                Scalar10 = x,
                Scalar11 = y,
                Scalar12 = z,

                Scalar20 = xz,
                Scalar21 = -z,
                Scalar22 = 1 - z * z * y1
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple3D.E2));

            return matrix;
        }

        if (axis == LinBasisVector3D.Ny)
        {
            var y1 = 1d / (y - 1d);
            var xz = x * z * y1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = 1 + x * x * y1,
                Scalar01 = x,
                Scalar02 = xz,

                Scalar10 = -x,
                Scalar11 = -y,
                Scalar12 = -z,

                Scalar20 = xz,
                Scalar21 = z,
                Scalar22 = 1 + z * z * y1
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple3D.NegativeE2));

            return matrix;
        }

        if (axis == LinBasisVector3D.Pz)
        {
            var z1 = 1d / (z + 1d);
            var xy = -x * y * z1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = 1d - x * x * z1,
                Scalar01 = xy,
                Scalar02 = -x,

                Scalar10 = xy,
                Scalar11 = 1d - y * y * z1,
                Scalar12 = -y,

                Scalar20 = x,
                Scalar21 = y,
                Scalar22 = z
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple3D.E3));

            return matrix;
        }

        if (axis == LinBasisVector3D.Nz)
        {
            var z1 = 1d / (z - 1d);
            var xy = x * y * z1;

            var matrix = new SquareMatrix3<T>(scalarProcessor)
            {
                Scalar00 = 1 + x * x * z1,
                Scalar01 = xy,
                Scalar02 = x,

                Scalar10 = xy,
                Scalar11 = 1 + y * y * z1,
                Scalar12 = y,

                Scalar20 = -x,
                Scalar21 = -y,
                Scalar22 = -z
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple3D.NegativeE3));

            return matrix;
        }

        throw new InvalidOperationException();
    }

    public static SquareMatrix3<T> CreateVectorToVectorRotationMatrix3D(ILinVector3D<T> unitVector1, ILinVector3D<T> unitVector2)
    {
        //Debug.Assert(
        //    unitVector1.IsNearUnitVector() && 
        //    unitVector2.IsNearUnitVector()
        //);

        var sumVector = LinVector3D<T>.Create(
            unitVector1.X + unitVector2.X,
            unitVector1.Y + unitVector2.Y,
            unitVector1.Z + unitVector2.Z
        );

        if (sumVector.IsZeroVector())
        {
            var normal = unitVector1.GetUnitNormal();

            var m1 = CreateVectorToVectorRotationMatrix3D(unitVector1, normal);
            var m2 = CreateVectorToVectorRotationMatrix3D(normal, unitVector2);

            //var matrix = m2 * m1;

            //Debug.Assert((matrix * unitVector1).IsNearVector(unitVector2));

            return m2 * m1;
        }
        else
        {
            var axis = sumVector.GetMaxAbsComponentIndex() switch
            {
                0 => sumVector.X.IsPositive()
                    ? LinBasisVector3D.Px
                    : LinBasisVector3D.Nx,

                1 => sumVector.Y.IsPositive()
                    ? LinBasisVector3D.Py
                    : LinBasisVector3D.Ny,

                _ => sumVector.Z.IsPositive()
                    ? LinBasisVector3D.Pz
                    : LinBasisVector3D.Nz
            };

            var m1 = CreateVectorToAxisRotationMatrix3D(unitVector1, axis);
            var m2 = CreateAxisToVectorRotationMatrix3D(axis, unitVector2);

            //var matrix = m2 * m1;

            //Debug.Assert((matrix * unitVector1).IsNearVector(unitVector2));

            return m2 * m1;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> operator -(SquareMatrix3<T> m1)
    {
        Debug.Assert(m1.IsValid());

        return new SquareMatrix3<T>(m1.ScalarProcessor)
        {
            Scalar00 = -m1.Scalar00,
            Scalar10 = -m1.Scalar10,
            Scalar20 = -m1.Scalar20,
            Scalar01 = -m1.Scalar01,
            Scalar11 = -m1.Scalar11,
            Scalar21 = -m1.Scalar21,
            Scalar02 = -m1.Scalar02,
            Scalar12 = -m1.Scalar12,
            Scalar22 = -m1.Scalar22
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> operator +(SquareMatrix3<T> m1, SquareMatrix3<T> m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        return new SquareMatrix3<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 + m2.Scalar00,
            Scalar10 = m1.Scalar10 + m2.Scalar10,
            Scalar20 = m1.Scalar20 + m2.Scalar20,
            Scalar01 = m1.Scalar01 + m2.Scalar01,
            Scalar11 = m1.Scalar11 + m2.Scalar11,
            Scalar21 = m1.Scalar21 + m2.Scalar21,
            Scalar02 = m1.Scalar02 + m2.Scalar02,
            Scalar12 = m1.Scalar12 + m2.Scalar12,
            Scalar22 = m1.Scalar22 + m2.Scalar22
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> operator -(SquareMatrix3<T> m1, SquareMatrix3<T> m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        return new SquareMatrix3<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 - m2.Scalar00,
            Scalar10 = m1.Scalar10 - m2.Scalar10,
            Scalar20 = m1.Scalar20 - m2.Scalar20,
            Scalar01 = m1.Scalar01 - m2.Scalar01,
            Scalar11 = m1.Scalar11 - m2.Scalar11,
            Scalar21 = m1.Scalar21 - m2.Scalar21,
            Scalar02 = m1.Scalar02 - m2.Scalar02,
            Scalar12 = m1.Scalar12 - m2.Scalar12,
            Scalar22 = m1.Scalar22 - m2.Scalar22
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator *(SquareMatrix3<T> m, ILinVector3D<T> vector)
    {
        var x = vector.X;
        var y = vector.Y;
        var z = vector.Z;

        return LinVector3D<T>.Create(m.Scalar00 * x + m.Scalar01 * y + m.Scalar02 * z,
            m.Scalar10 * x + m.Scalar11 * y + m.Scalar12 * z,
            m.Scalar20 * x + m.Scalar21 * y + m.Scalar22 * z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator *(ILinVector3D<T> vector, SquareMatrix3<T> m)
    {
        var x = vector.X;
        var y = vector.Y;
        var z = vector.Z;

        return LinVector3D<T>.Create(m.Scalar00 * x + m.Scalar10 * y + m.Scalar20 * z,
            m.Scalar01 * x + m.Scalar11 * y + m.Scalar21 * z,
            m.Scalar02 * x + m.Scalar12 * y + m.Scalar22 * z);
    }

    public static SquareMatrix3<T> operator *(SquareMatrix3<T> m1, SquareMatrix3<T> m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        return new SquareMatrix3<T>(m1.ScalarProcessor)
        {
            Scalar00 =
                m1.Scalar00 * m2.Scalar00 +
                m1.Scalar01 * m2.Scalar10 +
                m1.Scalar02 * m2.Scalar20,

            Scalar10 =
                m1.Scalar10 * m2.Scalar00 +
                m1.Scalar11 * m2.Scalar10 +
                m1.Scalar12 * m2.Scalar20,

            Scalar20 =
                m1.Scalar20 * m2.Scalar00 +
                m1.Scalar21 * m2.Scalar10 +
                m1.Scalar22 * m2.Scalar20,

            Scalar01 =
                m1.Scalar00 * m2.Scalar01 +
                m1.Scalar01 * m2.Scalar11 +
                m1.Scalar02 * m2.Scalar21,

            Scalar11 =
                m1.Scalar10 * m2.Scalar01 +
                m1.Scalar11 * m2.Scalar11 +
                m1.Scalar12 * m2.Scalar21,

            Scalar21 =
                m1.Scalar20 * m2.Scalar01 +
                m1.Scalar21 * m2.Scalar11 +
                m1.Scalar22 * m2.Scalar21,

            Scalar02 =
                m1.Scalar00 * m2.Scalar02 +
                m1.Scalar01 * m2.Scalar12 +
                m1.Scalar02 * m2.Scalar22,

            Scalar12 =
                m1.Scalar10 * m2.Scalar02 +
                m1.Scalar11 * m2.Scalar12 +
                m1.Scalar12 * m2.Scalar22,

            Scalar22 =
                m1.Scalar20 * m2.Scalar02 +
                m1.Scalar21 * m2.Scalar12 +
                m1.Scalar22 * m2.Scalar22
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> operator *(SquareMatrix3<T> m1, Scalar<T> s)
    {
        Debug.Assert(m1.IsValid() && s.IsValid());

        return new SquareMatrix3<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 * s,
            Scalar10 = m1.Scalar10 * s,
            Scalar20 = m1.Scalar20 * s,
            Scalar01 = m1.Scalar01 * s,
            Scalar11 = m1.Scalar11 * s,
            Scalar21 = m1.Scalar21 * s,
            Scalar02 = m1.Scalar02 * s,
            Scalar12 = m1.Scalar12 * s,
            Scalar22 = m1.Scalar22 * s
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> operator *(Scalar<T> s, SquareMatrix3<T> m1)
    {
        Debug.Assert(m1.IsValid() && s.IsValid());

        return new SquareMatrix3<T>(m1.ScalarProcessor)
        {
            Scalar00 = s * m1.Scalar00,
            Scalar10 = s * m1.Scalar10,
            Scalar20 = s * m1.Scalar20,
            Scalar01 = s * m1.Scalar01,
            Scalar11 = s * m1.Scalar11,
            Scalar21 = s * m1.Scalar21,
            Scalar02 = s * m1.Scalar02,
            Scalar12 = s * m1.Scalar12,
            Scalar22 = s * m1.Scalar22
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3<T> operator /(SquareMatrix3<T> m1, Scalar<T> s)
    {
        Debug.Assert(m1.IsValid() && s.IsValid());

        s = 1d / s;

        return new SquareMatrix3<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 * s,
            Scalar10 = m1.Scalar10 * s,
            Scalar20 = m1.Scalar20 * s,
            Scalar01 = m1.Scalar01 * s,
            Scalar11 = m1.Scalar11 * s,
            Scalar21 = m1.Scalar21 * s,
            Scalar02 = m1.Scalar02 * s,
            Scalar12 = m1.Scalar12 * s,
            Scalar22 = m1.Scalar22 * s
        };
    }

    
    public IScalarProcessor<T> ScalarProcessor 
        => Scalar00.ScalarProcessor;

    public int VSpaceDimensions 
        => 3;

    public Scalar<T> Scalar00 { get; set; }

    public Scalar<T> Scalar01 { get; set; }

    public Scalar<T> Scalar02 { get; set; }

    public Scalar<T> Scalar10 { get; set; }

    public Scalar<T> Scalar11 { get; set; }

    public Scalar<T> Scalar12 { get; set; }

    public Scalar<T> Scalar20 { get; set; }

    public Scalar<T> Scalar21 { get; set; }

    public Scalar<T> Scalar22 { get; set; }


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
                    _ => throw new IndexOutOfRangeException(nameof(j))
                },

                1 => j switch
                {
                    0 => Scalar10,
                    1 => Scalar11,
                    2 => Scalar12,
                    _ => throw new IndexOutOfRangeException(nameof(j))
                },

                2 => j switch
                {
                    0 => Scalar20,
                    1 => Scalar21,
                    2 => Scalar22,
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

                case 1 when j == 0:
                    Scalar10 = value;
                    break;
                case 1 when j == 1:
                    Scalar11 = value;
                    break;
                case 1 when j == 2:
                    Scalar12 = value;
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
           Scalar10 * Scalar10 +
           Scalar11 * Scalar11 +
           Scalar12 * Scalar12 +
           Scalar20 * Scalar20 +
           Scalar21 * Scalar21 +
           Scalar22 * Scalar22;

    /// <summary>
    /// The Frobenius norm of this matrix
    /// </summary>
    public Scalar<T> FNorm
        => FNormSquared.Sqrt();

    public Scalar<T> Determinant
        => Scalar00 * (Scalar11 * Scalar22 - Scalar12 * Scalar21) +
           Scalar01 * (Scalar12 * Scalar20 - Scalar10 * Scalar22) +
           Scalar02 * (Scalar10 * Scalar21 - Scalar11 * Scalar20);

    public bool SwapsHandedness
        => Determinant.IsNegative();


    public SquareMatrix3(IScalarProcessor<T> scalarProcessor)
    {
        Scalar00 = scalarProcessor.Zero;
        Scalar10 = scalarProcessor.Zero;
        Scalar20 = scalarProcessor.Zero;
        
        Scalar01 = scalarProcessor.Zero;
        Scalar11 = scalarProcessor.Zero;
        Scalar21 = scalarProcessor.Zero;
        
        Scalar02 = scalarProcessor.Zero;
        Scalar12 = scalarProcessor.Zero;
        Scalar22 = scalarProcessor.Zero;
    }

    public SquareMatrix3(SquareMatrix3<T> m1)
    {
        Scalar00 = m1.Scalar00;
        Scalar01 = m1.Scalar01;
        Scalar02 = m1.Scalar02;

        Scalar10 = m1.Scalar10;
        Scalar11 = m1.Scalar11;
        Scalar12 = m1.Scalar12;

        Scalar20 = m1.Scalar20;
        Scalar21 = m1.Scalar21;
        Scalar22 = m1.Scalar22;
    }

    public SquareMatrix3(Scalar<T>[,] m1)
    {
        Scalar00 = m1[0, 0];
        Scalar01 = m1[0, 1];
        Scalar02 = m1[0, 2];

        Scalar10 = m1[1, 0];
        Scalar11 = m1[1, 1];
        Scalar12 = m1[1, 2];

        Scalar20 = m1[2, 0];
        Scalar21 = m1[2, 1];
        Scalar22 = m1[2, 2];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar00.IsValid() &&
               Scalar01.IsValid() &&
               Scalar02.IsValid() &&
               Scalar10.IsValid() &&
               Scalar11.IsValid() &&
               Scalar12.IsValid() &&
               Scalar20.IsValid() &&
               Scalar21.IsValid() &&
               Scalar22.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAffine2D()
    {
        return IsValid() &&
               Scalar20 == 0d &&
               Scalar21 == 0d &&
               Scalar22 == 1d;
    }

    //public SquareMatrix3<T> NearestOrthogonalMatrix()
    //{
    //    var r1 = new SquareMatrix3<T>(this);
    //    var r2 = r1;
    //    var oneOver2 = ScalarProcessor.One.Divide(ScalarProcessor.TwoValue);

    //    for (var i = 0; i < 100; i++)
    //    {
    //        r2 = LinAngleUtils.Lerp(oneOver2, r1, r1.Inverse().Transpose());

    //        var norm = (r1 - r2).FNormSquared;

    //        if (norm.IsNearZero()) break;

    //        r1 = r2;
    //    }

    //    return r2;
    //}

    /// <summary>
    /// https://github.com/mmp/pbrt-v3/blob/master/src/core/quaternion.cpp
    /// </summary>
    /// <returns></returns>
    public LinQuaternion<T> ToQuaternion()
    {
        var trace = Scalar00 + Scalar11 + Scalar22;
        if (trace > 0d)
        {
            // Compute w from matrix trace, then xyz
            // 4w^2 = m[0, 0] + m[1, 1] + m[2, 2] + m[3, 3] (but m[3, 3] == 1)
            var s = (trace + 1).Sqrt();
            var w = s / 2;
            s = 1 / (2 * s);

            return LinQuaternion<T>.Create(
                (Scalar21 - Scalar12) * s,
                (Scalar02 - Scalar20) * s,
                (Scalar10 - Scalar01) * s,
                w
            );
        }
        else
        {
            // Compute largest of x, y, or z, then remaining components
            //TODO: Replace this array with % computations
            var nxt = new[] { 1, 2, 0 };
            var q = new Scalar<T>[3];
            var i = 0;
            if (Scalar11 > Scalar00) i = 1;
            if (Scalar22 > this[i, i]) i = 2;
            var j = nxt[i]; //j = (i + 1) % 3
            var k = nxt[j]; //k = (j + 1) % 3
            var s = (this[i, i] - (this[j, j] + this[k, k]) + 1).Sqrt();
            q[i] = s / 2;
            if (!s.IsNearZero()) s = 1 / (2 * s);
            var w = (this[k, j] - this[j, k]) * s;
            q[j] = (this[j, i] + this[i, j]) * s;
            q[k] = (this[k, i] + this[i, k]) * s;

            return LinQuaternion<T>.Create(q[0], q[1], q[2], w);
        }
    }

    /// <summary>
    /// https://github.com/mmp/pbrt-v2/blob/master/src/core/transform.cpp
    /// </summary>
    /// <returns></returns>
    public SquareMatrix3<T> Inverse()
    {
        var indexC = new int[3];
        var indexR = new int[3];
        var iPiv = new[] { 0, 0, 0 };
        var minV = new SquareMatrix3<T>(this);

        for (var i = 0; i < 3; i++)
        {
            int iRow = 0, iCol = 0;
            var big = ScalarProcessor.Zero;

            // Choose pivot
            for (var j = 0; j < 3; j++)
            {
                if (iPiv[j] == 1) continue;

                for (var k = 0; k < 3; k++)
                {
                    if (iPiv[k] == 0)
                    {
                        if (minV[j, k].Abs() < big) continue;

                        big = minV[j, k].Abs();
                        iRow = j;
                        iCol = k;
                    }
                    else if (iPiv[k] > 1)
                        throw new InvalidOperationException("Singular matrix");
                }
            }

            ++iPiv[iCol];

            // Swap rows _iRow_ and _iCol_ for pivot
            if (iRow != iCol)
            {
                for (var k = 0; k < 3; ++k)
                {
                    (minV[iRow, k], minV[iCol, k]) = (minV[iCol, k], minV[iRow, k]);
                }
            }

            indexR[i] = iRow;
            indexC[i] = iCol;
            if (minV[iCol, iCol].IsNearZero())
                throw new InvalidOperationException("Singular matrix");

            // Set minV[iCol, iCol] to one by scaling row _iCol_ appropriately
            var pivInv = 1d / minV[iCol, iCol];

            minV[iCol, iCol] = ScalarProcessor.One;
            for (var j = 0; j < 3; j++)
                minV[iCol, j] *= pivInv;

            // Subtract this row from others to zero out their columns
            for (var j = 0; j < 3; j++)
            {
                if (j == iCol) continue;

                var save = minV[j, iCol];

                minV[j, iCol] = ScalarProcessor.Zero;
                for (var k = 0; k < 3; k++)
                    minV[j, k] -= minV[iCol, k] * save;
            }
        }

        // Swap columns to reflect permutation
        for (var j = 2; j >= 0; j--)
        {
            if (indexR[j] == indexC[j]) continue;

            for (var k = 0; k < 3; k++)
            {
                (minV[k, indexR[j]], minV[k, indexC[j]]) = (minV[k, indexC[j]], minV[k, indexR[j]]);
            }
        }

        return minV;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3<T> SelfTranspose()
    {
        (Scalar01, Scalar10) = (Scalar10, Scalar01);
        (Scalar12, Scalar21) = (Scalar21, Scalar12);
        (Scalar20, Scalar02) = (Scalar02, Scalar20);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3<T> Transpose()
    {
        var m = new SquareMatrix3<T>(ScalarProcessor)
        {
            Scalar00 = Scalar00,
            Scalar01 = Scalar10,
            Scalar02 = Scalar20,

            Scalar10 = Scalar01,
            Scalar11 = Scalar11,
            Scalar12 = Scalar21,

            Scalar20 = Scalar02,
            Scalar21 = Scalar12,
            Scalar22 = Scalar22,
        };

        return m;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3<T> InverseTranspose()
    {
        return Inverse().SelfTranspose();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3<T> GetSquareMatrix3()
    {
        return new SquareMatrix3<T>(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T>[,] GetArray2D()
    {
        var array = new Scalar<T>[3, 3];

        array[0, 0] = Scalar00;
        array[1, 0] = Scalar10;
        array[2, 0] = Scalar20;

        array[0, 1] = Scalar01;
        array[1, 1] = Scalar11;
        array[2, 1] = Scalar21;

        array[0, 2] = Scalar02;
        array[1, 2] = Scalar12;
        array[2, 2] = Scalar22;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComplexNumber<T>[,] GetComplexArray2D()
    {
        var array = new ComplexNumber<T>[3, 3];

        array[0, 0] = ScalarProcessor.CreateComplexNumberReal(Scalar00);
        array[1, 0] = ScalarProcessor.CreateComplexNumberReal(Scalar10);
        array[2, 0] = ScalarProcessor.CreateComplexNumberReal(Scalar20);

        array[0, 1] = ScalarProcessor.CreateComplexNumberReal(Scalar01);
        array[1, 1] = ScalarProcessor.CreateComplexNumberReal(Scalar11);
        array[2, 1] = ScalarProcessor.CreateComplexNumberReal(Scalar21);

        array[0, 2] = ScalarProcessor.CreateComplexNumberReal(Scalar02);
        array[1, 2] = ScalarProcessor.CreateComplexNumberReal(Scalar12);
        array[2, 2] = ScalarProcessor.CreateComplexNumberReal(Scalar22);

        return array;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Matrix<Scalar<T>> GetMatrix()
    //{
    //    return Matrix<Scalar<T>>.Build.DenseOfArray(
    //        GetArray2D()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Matrix<Complex> GetComplexMatrix()
    //{
    //    return Matrix<Complex>.Build.DenseOfArray(
    //        GetComplexArray2D()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> DiagonalToLinVector3D()
    {
        return LinVector3D<T>.Create(Scalar00, Scalar11, Scalar22);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> RowToLinVector3D(int rowIndex)
    {
        return rowIndex switch
        {
            0 => LinVector3D<T>.Create(Scalar00, Scalar01, Scalar02),
            1 => LinVector3D<T>.Create(Scalar10, Scalar11, Scalar12),
            2 => LinVector3D<T>.Create(Scalar20, Scalar21, Scalar22),
            _ => throw new IndexOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> ColumnToLinVector3D(int colIndex)
    {
        return colIndex switch
        {
            0 => LinVector3D<T>.Create(Scalar00, Scalar10, Scalar20),
            1 => LinVector3D<T>.Create(Scalar01, Scalar11, Scalar21),
            2 => LinVector3D<T>.Create(Scalar02, Scalar12, Scalar22),
            _ => throw new IndexOutOfRangeException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3<T> SetDiagonal(ITriplet<Scalar<T>> vector)
    {
        Scalar00 = vector.Item1;
        Scalar11 = vector.Item2;
        Scalar22 = vector.Item3;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3<T> SetRow(int rowIndex, ITriplet<Scalar<T>> vector)
    {
        if (rowIndex == 0)
        {
            Scalar00 = vector.Item1;
            Scalar01 = vector.Item2;
            Scalar02 = vector.Item3;

            return this;
        }

        if (rowIndex == 1)
        {
            Scalar10 = vector.Item1;
            Scalar11 = vector.Item2;
            Scalar12 = vector.Item3;

            return this;
        }

        if (rowIndex == 2)
        {
            Scalar20 = vector.Item1;
            Scalar21 = vector.Item2;
            Scalar22 = vector.Item3;

            return this;
        }

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3<T> SetColumn(int colIndex, ITriplet<Scalar<T>> vector)
    {
        if (colIndex == 0)
        {
            Scalar00 = vector.Item1;
            Scalar10 = vector.Item2;
            Scalar20 = vector.Item3;

            return this;
        }

        if (colIndex == 1)
        {
            Scalar01 = vector.Item1;
            Scalar11 = vector.Item2;
            Scalar21 = vector.Item3;

            return this;
        }

        if (colIndex == 2)
        {
            Scalar02 = vector.Item1;
            Scalar12 = vector.Item2;
            Scalar22 = vector.Item3;

            return this;
        }

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> MapPoint(ILinVector2D<T> point)
    {
        Debug.Assert(IsAffine2D());

        return LinVector2D<T>.Create(Scalar00 * point.X + Scalar01 * point.Y + Scalar02,
            Scalar10 * point.X + Scalar11 * point.Y + Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> MapVector(ILinVector2D<T> vector)
    {
        Debug.Assert(IsAffine2D());

        return LinVector2D<T>.Create(Scalar00 * vector.X + Scalar01 * vector.Y,
            Scalar10 * vector.X + Scalar11 * vector.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> MapNormal(ILinVector2D<T> normal)
    {
        return InverseTranspose().MapVector(normal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3<T> GetInverseAffineMap()
    {
        return Inverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine("[")
            .AppendLine($"   {Scalar00:G}, {Scalar01:G}, {Scalar02:G};")
            .AppendLine($"   {Scalar10:G}, {Scalar11:G}, {Scalar12:G};")
            .AppendLine($"   {Scalar20:G}, {Scalar21:G}, {Scalar22:G};")
            .AppendLine("]")
            .ToString();
    }
}