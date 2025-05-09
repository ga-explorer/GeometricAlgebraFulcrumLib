using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;

public sealed class SquareMatrix2<T> :
    ILinearAlgebraElement<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> CreateIdentityMatrix(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix2<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.One,
            Scalar11 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> CreateScalingMatrix2D(Scalar<T> sx, Scalar<T> sy)
    {
        return new SquareMatrix2<T>(sx.ScalarProcessor)
        {
            Scalar00 = sx,
            Scalar11 = sy
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> CreateScalingMatrix2D(Scalar<T> s)
    {
        return new SquareMatrix2<T>(s.ScalarProcessor)
        {
            Scalar00 = s,
            Scalar11 = s
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> CreateXReflectionMatrix2D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix2<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.One,
            Scalar11 = scalarProcessor.MinusOne
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> CreateYReflectionMatrix2D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix2<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.MinusOne,
            Scalar11 = scalarProcessor.One
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> CreateOriginReflectionMatrix2D(IScalarProcessor<T> scalarProcessor)
    {
        return new SquareMatrix2<T>(scalarProcessor)
        {
            Scalar00 = scalarProcessor.MinusOne,
            Scalar11 = scalarProcessor.MinusOne
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> CreateRotationMatrix2D(LinAngle<T> angle)
    {
        var m = new SquareMatrix2<T>(angle.ScalarProcessor);

        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        m.Scalar00 = cosAngle;
        m.Scalar10 = sinAngle;
        m.Scalar01 = -sinAngle;
        m.Scalar11 = cosAngle;

        return m;
    }

    public static SquareMatrix2<T> CreateAxisToVectorRotationMatrix2D(LinBasisVector2D axis, ILinVector2D<T> unitVector)
    {
        //Debug.Assert(unitVector.IsNearUnitVector());

        var x = unitVector.X;
        var y = unitVector.Y;

        if (axis == LinBasisVector2D.Px)
        {
            var x1 = 1d / (x + 1d);

            var matrix = new SquareMatrix2<T>(unitVector.ScalarProcessor)
            {
                Scalar00 = x,
                Scalar10 = y,

                Scalar01 = -y,
                Scalar11 = 1 - y * y * x1
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple2D.E1));

            return matrix;
        }

        if (axis == LinBasisVector2D.Nx)
        {
            var x1 = 1d / (x - 1d);

            var matrix = new SquareMatrix2<T>(unitVector.ScalarProcessor)
            {
                Scalar00 = -x,
                Scalar10 = -y,

                Scalar01 = y,
                Scalar11 = 1 + y * y * x1
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple2D.NegativeE1));

            return matrix;
        }

        if (axis == LinBasisVector2D.Py)
        {
            var y1 = 1d / (y + 1d);

            var matrix = new SquareMatrix2<T>(unitVector.ScalarProcessor)
            {
                Scalar00 = 1 - x * x * y1,
                Scalar10 = -x,

                Scalar01 = x,
                Scalar11 = y
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple2D.E2));

            return matrix;
        }

        if (axis == LinBasisVector2D.Ny)
        {
            var y1 = 1d / (y - 1d);

            var matrix = new SquareMatrix2<T>(unitVector.ScalarProcessor)
            {
                Scalar00 = 1 + x * x * y1,
                Scalar10 = x,

                Scalar01 = -x,
                Scalar11 = -y
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple2D.NegativeE2));

            return matrix;
        }

        throw new InvalidOperationException();
    }

    public static SquareMatrix2<T> CreateVectorToAxisRotationMatrix2D(ILinVector2D<T> unitVector, LinBasisVector2D axis)
    {
        //Debug.Assert(unitVector.IsValid() && unitVector.IsNearUnitVector());

        var x = unitVector.X;
        var y = unitVector.Y;

        if (axis == LinBasisVector2D.Px)
        {
            var x1 = 1d / (x + 1d);

            var matrix = new SquareMatrix2<T>(unitVector.ScalarProcessor)
            {
                Scalar00 = x,
                Scalar01 = y,

                Scalar10 = -y,
                Scalar11 = 1 - y * y * x1
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple2D.E1));

            return matrix;
        }

        if (axis == LinBasisVector2D.Nx)
        {
            var x1 = 1d / (x - 1d);

            var matrix = new SquareMatrix2<T>(unitVector.ScalarProcessor)
            {
                Scalar00 = -x,
                Scalar01 = -y,

                Scalar10 = y,
                Scalar11 = 1 + y * y * x1
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple2D.NegativeE1));

            return matrix;
        }

        if (axis == LinBasisVector2D.Py)
        {
            var y1 = 1d / (y + 1d);

            var matrix = new SquareMatrix2<T>(unitVector.ScalarProcessor)
            {
                Scalar00 = 1 - x * x * y1,
                Scalar01 = -x,

                Scalar10 = x,
                Scalar11 = y
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple2D.E2));

            return matrix;
        }

        if (axis == LinBasisVector2D.Ny)
        {
            var y1 = 1d / (y - 1d);

            var matrix = new SquareMatrix2<T>(unitVector.ScalarProcessor)
            {
                Scalar00 = 1 + x * x * y1,
                Scalar01 = x,

                Scalar10 = -x,
                Scalar11 = -y
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple2D.NegativeE2));

            return matrix;
        }

        throw new InvalidOperationException();
    }

    public static SquareMatrix2<T> CreateVectorToVectorRotationMatrix2D(ILinVector2D<T> unitVector1, ILinVector2D<T> unitVector2)
    {
        //Debug.Assert(
        //    unitVector1.IsNearUnitVector() && 
        //    unitVector2.IsNearUnitVector()
        //);

        var sumVector = LinVector2D<T>.Create(unitVector1.X + unitVector2.X,
            unitVector1.Y + unitVector2.Y);

        if (sumVector.VectorENormSquared().IsNearZero())
        {
            var normal = unitVector1.GetUnitNormal();

            var m1 = CreateVectorToVectorRotationMatrix2D(unitVector1, normal);
            var m2 = CreateVectorToVectorRotationMatrix2D(normal, unitVector2);

            //var matrix = m2 * m1;

            //Debug.Assert((matrix * unitVector1).IsNearVector(unitVector2));

            return m2 * m1;
        }
        else
        {
            var axis = sumVector.GetMaxAbsComponentIndex() switch
            {
                0 => sumVector.X.IsPositive()
                    ? LinBasisVector2D.Px
                    : LinBasisVector2D.Nx,

                _ => sumVector.Y.IsPositive()
                    ? LinBasisVector2D.Py
                    : LinBasisVector2D.Ny
            };

            var m1 = CreateVectorToAxisRotationMatrix2D(unitVector1, axis);
            var m2 = CreateAxisToVectorRotationMatrix2D(axis, unitVector2);

            //var matrix = m2 * m1;

            //Debug.Assert((matrix * unitVector1).IsNearVector(unitVector2));

            return m2 * m1;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> operator -(SquareMatrix2<T> m1)
    {
        Debug.Assert(m1.IsValid());

        return new SquareMatrix2<T>(m1.ScalarProcessor)
        {
            Scalar00 = -m1.Scalar00,
            Scalar10 = -m1.Scalar10,
            Scalar01 = -m1.Scalar01,
            Scalar11 = -m1.Scalar11
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> operator +(SquareMatrix2<T> m1, SquareMatrix2<T> m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        return new SquareMatrix2<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 + m2.Scalar00,
            Scalar10 = m1.Scalar10 + m2.Scalar10,
            Scalar01 = m1.Scalar01 + m2.Scalar01,
            Scalar11 = m1.Scalar11 + m2.Scalar11
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> operator -(SquareMatrix2<T> m1, SquareMatrix2<T> m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        return new SquareMatrix2<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 - m2.Scalar00,
            Scalar10 = m1.Scalar10 - m2.Scalar10,
            Scalar01 = m1.Scalar01 - m2.Scalar01,
            Scalar11 = m1.Scalar11 - m2.Scalar11
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(SquareMatrix2<T> m, ILinVector2D<T> vector)
    {
        var x = vector.X;
        var y = vector.Y;

        return LinVector2D<T>.Create(m.Scalar00 * x + m.Scalar01 * y,
            m.Scalar10 * x + m.Scalar11 * y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(ILinVector2D<T> vector, SquareMatrix2<T> m)
    {
        var x = vector.X;
        var y = vector.Y;

        return LinVector2D<T>.Create(m.Scalar00 * x + m.Scalar10 * y,
            m.Scalar01 * x + m.Scalar11 * y);
    }

    public static SquareMatrix2<T> operator *(SquareMatrix2<T> m1, SquareMatrix2<T> m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        return new SquareMatrix2<T>(m1.ScalarProcessor)
        {
            Scalar00 =
                m1.Scalar00 * m2.Scalar00 +
                m1.Scalar01 * m2.Scalar10,

            Scalar10 =
                m1.Scalar10 * m2.Scalar00 +
                m1.Scalar11 * m2.Scalar10,

            Scalar01 =
                m1.Scalar00 * m2.Scalar01 +
                m1.Scalar01 * m2.Scalar11,

            Scalar11 =
                m1.Scalar10 * m2.Scalar01 +
                m1.Scalar11 * m2.Scalar11
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> operator *(SquareMatrix2<T> m1, Scalar<T> s)
    {
        Debug.Assert(m1.IsValid() && s.IsValid());

        return new SquareMatrix2<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 * s,
            Scalar10 = m1.Scalar10 * s,
            Scalar01 = m1.Scalar01 * s,
            Scalar11 = m1.Scalar11 * s
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> operator *(Scalar<T> s, SquareMatrix2<T> m1)
    {
        Debug.Assert(m1.IsValid() && s.IsValid());

        return new SquareMatrix2<T>(m1.ScalarProcessor)
        {
            Scalar00 = s * m1.Scalar00,
            Scalar10 = s * m1.Scalar10,
            Scalar01 = s * m1.Scalar01,
            Scalar11 = s * m1.Scalar11
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2<T> operator /(SquareMatrix2<T> m1, Scalar<T> s)
    {
        Debug.Assert(m1.IsValid() && s.IsValid());

        return new SquareMatrix2<T>(m1.ScalarProcessor)
        {
            Scalar00 = m1.Scalar00 / s,
            Scalar10 = m1.Scalar10 / s,
            Scalar01 = m1.Scalar01 / s,
            Scalar11 = m1.Scalar11 / s
        };
    }


    public IScalarProcessor<T> ScalarProcessor 
        => Scalar00.ScalarProcessor;

    public int VSpaceDimensions 
        => 2;

    public Scalar<T> Scalar00 { get; set; }

    public Scalar<T> Scalar01 { get; set; }

    public Scalar<T> Scalar10 { get; set; }

    public Scalar<T> Scalar11 { get; set; }


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
                    _ => throw new IndexOutOfRangeException(nameof(j))
                },

                1 => j switch
                {
                    0 => Scalar10,
                    1 => Scalar11,
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

                case 1 when j == 0:
                    Scalar10 = value;
                    break;
                case 1 when j == 1:
                    Scalar11 = value;
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
           Scalar10 * Scalar10 +
           Scalar11 * Scalar11;

    /// <summary>
    /// The Frobenius norm of this matrix
    /// </summary>
    public Scalar<T> FNorm
        => FNormSquared.Sqrt();

    public Scalar<T> Determinant
        => Scalar00 * Scalar11 - Scalar01 * Scalar10;


    public SquareMatrix2(IScalarProcessor<T> scalarProcessor)
    {
        Scalar00 = scalarProcessor.Zero;
        Scalar01 = scalarProcessor.Zero;
        Scalar10 = scalarProcessor.Zero;
        Scalar11 = scalarProcessor.Zero;
    }

    public SquareMatrix2(SquareMatrix2<T> m1)
    {
        Scalar00 = m1.Scalar00;
        Scalar01 = m1.Scalar01;

        Scalar10 = m1.Scalar10;
        Scalar11 = m1.Scalar11;
    }

    public SquareMatrix2(Scalar<T>[,] m1)
    {
        Scalar00 = m1[0, 0];
        Scalar01 = m1[0, 1];

        Scalar10 = m1[1, 0];
        Scalar11 = m1[1, 1];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar00.IsValid() &&
               Scalar01.IsValid() &&
               Scalar10.IsValid() &&
               Scalar11.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAffine2D()
    {
        return IsValid();
    }

    //public SquareMatrix2<T> NearestOrthogonalMatrix()
    //{
    //    var r1 = new SquareMatrix2<T>(this);
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
    /// https://github.com/mmp/pbrt-v2/blob/master/src/core/transform.cpp
    /// </summary>
    /// <returns></returns>
    public SquareMatrix2<T> Inverse()
    {
        var indexC = new int[3];
        var indexR = new int[3];
        var iPiv = new[] { 0, 0, 0 };
        var minV = new SquareMatrix2<T>(this);

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
    public SquareMatrix2<T> SelfTranspose()
    {
        (Scalar01, Scalar10) = (Scalar10, Scalar01);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix2<T> Transpose()
    {
        var m = new SquareMatrix2<T>(ScalarProcessor)
        {
            Scalar00 = Scalar00,
            Scalar01 = Scalar10,

            Scalar10 = Scalar01,
            Scalar11 = Scalar11
        };

        return m;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix2<T> InverseTranspose()
    {
        return Inverse().SelfTranspose();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix2<T> GetSquareMatrix2()
    {
        return new SquareMatrix2<T>(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3<T> GetSquareMatrix3()
    {
        return new SquareMatrix3<T>(ScalarProcessor)
        {
            Scalar00 = Scalar00,
            Scalar01 = Scalar10,

            Scalar10 = Scalar01,
            Scalar11 = Scalar11,

            Scalar22 = ScalarProcessor.One
        };
    }

    public Scalar<T>[,] GetArray2D()
    {
        var array = new Scalar<T>[2, 2];

        array[0, 0] = Scalar00;
        array[1, 0] = Scalar10;

        array[0, 1] = Scalar01;
        array[1, 1] = Scalar11;

        return array;
    }

    public bool SwapsHandedness
        => Determinant < 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> MapPoint(ILinVector2D<T> point)
    {
        Debug.Assert(IsAffine2D());

        return LinVector2D<T>.Create(Scalar00 * point.X + Scalar01 * point.Y,
            Scalar10 * point.X + Scalar11 * point.Y);
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
    public SquareMatrix2<T> GetInverseAffineMap()
    {
        return Inverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine("[")
            .AppendLine($"   {Scalar00:G}, {Scalar01:G};")
            .AppendLine($"   {Scalar10:G}, {Scalar11:G};")
            .AppendLine("]")
            .ToString();
    }
}
