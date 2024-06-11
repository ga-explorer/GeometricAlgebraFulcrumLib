using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;

public sealed class SquareMatrix2 //: IAffineMap2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 CreateIdentityMatrix()
    {
        return new SquareMatrix2
        {
            Scalar00 = 1d,
            Scalar11 = 1d
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 CreateScalingMatrix2D(double sx, double sy)
    {
        return new SquareMatrix2
        {
            Scalar00 = sx,
            Scalar11 = sy
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 CreateScalingMatrix2D(double s)
    {
        return new SquareMatrix2
        {
            Scalar00 = s,
            Scalar11 = s
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 CreateXReflectionMatrix2D()
    {
        return new SquareMatrix2
        {
            Scalar00 = 1d,
            Scalar11 = -1d
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 CreateYReflectionMatrix2D()
    {
        return new SquareMatrix2
        {
            Scalar00 = -1d,
            Scalar11 = 1d
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 CreateOriginReflectionMatrix2D()
    {
        return new SquareMatrix2
        {
            Scalar00 = -1d,
            Scalar11 = -1d
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 CreateRotationMatrix2D(LinFloat64DirectedAngle angle)
    {
        var m = new SquareMatrix2();

        var cosAngle = angle.Cos();
        var sinAngle = angle.Sin();

        m.Scalar00 = cosAngle;
        m.Scalar10 = sinAngle;
        m.Scalar01 = -sinAngle;
        m.Scalar11 = cosAngle;

        return m;
    }

    public static SquareMatrix2 CreateAxisToVectorRotationMatrix2D(LinUnitBasisVector2D axis, ILinFloat64Vector2D unitVector)
    {
        //Debug.Assert(unitVector.IsNearUnitVector());

        var x = unitVector.X;
        var y = unitVector.Y;

        if (axis == LinUnitBasisVector2D.PositiveX)
        {
            var x1 = 1d / (x + 1d);

            var matrix = new SquareMatrix2()
            {
                Scalar00 = x,
                Scalar10 = y,

                Scalar01 = -y,
                Scalar11 = 1 - y * y * x1
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple2D.E1));

            return matrix;
        }

        if (axis == LinUnitBasisVector2D.NegativeX)
        {
            var x1 = 1d / (x - 1d);

            var matrix = new SquareMatrix2()
            {
                Scalar00 = -x,
                Scalar10 = -y,

                Scalar01 = y,
                Scalar11 = 1 + y * y * x1
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple2D.NegativeE1));

            return matrix;
        }

        if (axis == LinUnitBasisVector2D.PositiveY)
        {
            var y1 = 1d / (y + 1d);

            var matrix = new SquareMatrix2()
            {
                Scalar00 = 1 - x * x * y1,
                Scalar10 = -x,

                Scalar01 = x,
                Scalar11 = y
            };

            //Debug.Assert((matrix.Transpose() * unitVector).IsAlmostVector(Tuple2D.E2));

            return matrix;
        }

        if (axis == LinUnitBasisVector2D.NegativeY)
        {
            var y1 = 1d / (y - 1d);

            var matrix = new SquareMatrix2()
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

    public static SquareMatrix2 CreateVectorToAxisRotationMatrix2D(ILinFloat64Vector2D unitVector, LinUnitBasisVector2D axis)
    {
        //Debug.Assert(unitVector.IsValid() && unitVector.IsNearUnitVector());

        var x = unitVector.X;
        var y = unitVector.Y;

        if (axis == LinUnitBasisVector2D.PositiveX)
        {
            var x1 = 1d / (x + 1d);

            var matrix = new SquareMatrix2()
            {
                Scalar00 = x,
                Scalar01 = y,

                Scalar10 = -y,
                Scalar11 = 1 - y * y * x1
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple2D.E1));

            return matrix;
        }

        if (axis == LinUnitBasisVector2D.NegativeX)
        {
            var x1 = 1d / (x - 1d);

            var matrix = new SquareMatrix2()
            {
                Scalar00 = -x,
                Scalar01 = -y,

                Scalar10 = y,
                Scalar11 = 1 + y * y * x1
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple2D.NegativeE1));

            return matrix;
        }

        if (axis == LinUnitBasisVector2D.PositiveY)
        {
            var y1 = 1d / (y + 1d);

            var matrix = new SquareMatrix2()
            {
                Scalar00 = 1 - x * x * y1,
                Scalar01 = -x,

                Scalar10 = x,
                Scalar11 = y
            };

            //Debug.Assert((matrix * unitVector).IsAlmostVector(Tuple2D.E2));

            return matrix;
        }

        if (axis == LinUnitBasisVector2D.NegativeY)
        {
            var y1 = 1d / (y - 1d);

            var matrix = new SquareMatrix2()
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

    public static SquareMatrix2 CreateVectorToVectorRotationMatrix2D(ILinFloat64Vector2D unitVector1, ILinFloat64Vector2D unitVector2)
    {
        //Debug.Assert(
        //    unitVector1.IsNearUnitVector() && 
        //    unitVector2.IsNearUnitVector()
        //);

        var sumVector = LinFloat64Vector2D.Create(unitVector1.X + unitVector2.X,
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
                    ? LinUnitBasisVector2D.PositiveX
                    : LinUnitBasisVector2D.NegativeX,

                _ => sumVector.Y.IsPositive()
                    ? LinUnitBasisVector2D.PositiveY
                    : LinUnitBasisVector2D.NegativeY
            };

            var m1 = CreateVectorToAxisRotationMatrix2D(unitVector1, axis);
            var m2 = CreateAxisToVectorRotationMatrix2D(axis, unitVector2);

            //var matrix = m2 * m1;

            //Debug.Assert((matrix * unitVector1).IsNearVector(unitVector2));

            return m2 * m1;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 operator -(SquareMatrix2 m1)
    {
        Debug.Assert(m1.IsValid());

        return new SquareMatrix2
        {
            Scalar00 = -m1.Scalar00,
            Scalar10 = -m1.Scalar10,
            Scalar01 = -m1.Scalar01,
            Scalar11 = -m1.Scalar11
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 operator +(SquareMatrix2 m1, SquareMatrix2 m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        return new SquareMatrix2
        {
            Scalar00 = m1.Scalar00 + m2.Scalar00,
            Scalar10 = m1.Scalar10 + m2.Scalar10,
            Scalar01 = m1.Scalar01 + m2.Scalar01,
            Scalar11 = m1.Scalar11 + m2.Scalar11
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 operator -(SquareMatrix2 m1, SquareMatrix2 m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        return new SquareMatrix2
        {
            Scalar00 = m1.Scalar00 - m2.Scalar00,
            Scalar10 = m1.Scalar10 - m2.Scalar10,
            Scalar01 = m1.Scalar01 - m2.Scalar01,
            Scalar11 = m1.Scalar11 - m2.Scalar11
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator *(SquareMatrix2 m, ILinFloat64Vector2D vector)
    {
        var x = vector.X;
        var y = vector.Y;

        return LinFloat64Vector2D.Create(m.Scalar00 * x + m.Scalar01 * y,
            m.Scalar10 * x + m.Scalar11 * y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator *(ILinFloat64Vector2D vector, SquareMatrix2 m)
    {
        var x = vector.X;
        var y = vector.Y;

        return LinFloat64Vector2D.Create(m.Scalar00 * x + m.Scalar10 * y,
            m.Scalar01 * x + m.Scalar11 * y);
    }

    public static SquareMatrix2 operator *(SquareMatrix2 m1, SquareMatrix2 m2)
    {
        Debug.Assert(m1.IsValid() && m2.IsValid());

        return new SquareMatrix2
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
    public static SquareMatrix2 operator *(SquareMatrix2 m1, double s)
    {
        Debug.Assert(m1.IsValid() && double.IsNaN(s));

        return new SquareMatrix2
        {
            Scalar00 = m1.Scalar00 * s,
            Scalar10 = m1.Scalar10 * s,
            Scalar01 = m1.Scalar01 * s,
            Scalar11 = m1.Scalar11 * s
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 operator *(double s, SquareMatrix2 m1)
    {
        Debug.Assert(m1.IsValid() && !double.IsNaN(s));

        return new SquareMatrix2
        {
            Scalar00 = s * m1.Scalar00,
            Scalar10 = s * m1.Scalar10,
            Scalar01 = s * m1.Scalar01,
            Scalar11 = s * m1.Scalar11
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 operator /(SquareMatrix2 m1, double s)
    {
        Debug.Assert(m1.IsValid() && !double.IsNaN(s));

        s = 1d / s;

        return new SquareMatrix2
        {
            Scalar00 = m1.Scalar00 * s,
            Scalar10 = m1.Scalar10 * s,
            Scalar01 = m1.Scalar01 * s,
            Scalar11 = m1.Scalar11 * s
        };
    }


    public double Scalar00 { get; set; }

    public double Scalar01 { get; set; }

    public double Scalar10 { get; set; }

    public double Scalar11 { get; set; }


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
            Debug.Assert(!double.IsNaN(value));

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
    public double FNormSquared
        => Scalar00 * Scalar00 +
           Scalar01 * Scalar01 +
           Scalar10 * Scalar10 +
           Scalar11 * Scalar11;

    /// <summary>
    /// The Frobenius norm of this matrix
    /// </summary>
    public double FNorm
        => Math.Sqrt(FNormSquared);

    public double Determinant
        => Scalar00 * Scalar11 - Scalar01 * Scalar10;


    public SquareMatrix2()
    {
    }

    public SquareMatrix2(SquareMatrix2 m1)
    {
        Scalar00 = m1.Scalar00;
        Scalar01 = m1.Scalar01;

        Scalar10 = m1.Scalar10;
        Scalar11 = m1.Scalar11;
    }

    public SquareMatrix2(double[,] m1)
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

    public SquareMatrix2 NearestOrthogonalMatrix()
    {
        var r1 = new SquareMatrix2(this);
        var r2 = r1;

        for (var i = 0; i < 100; i++)
        {
            r2 = 0.5.Lerp(r1, r1.InverseTranspose());

            var norm = (r1 - r2).FNormSquared;

            if (norm.IsNearZero()) break;

            r1 = r2;
        }

        return r2;
    }

    /// <summary>
    /// https://github.com/mmp/pbrt-v2/blob/master/src/core/transform.cpp
    /// </summary>
    /// <returns></returns>
    public SquareMatrix2 Inverse()
    {
        var indexC = new int[3];
        var indexR = new int[3];
        var iPiv = new[] { 0, 0, 0 };
        var minV = new SquareMatrix2(this);

        for (var i = 0; i < 3; i++)
        {
            int iRow = 0, iCol = 0;
            var big = 0d;

            // Choose pivot
            for (var j = 0; j < 3; j++)
            {
                if (iPiv[j] == 1) continue;

                for (var k = 0; k < 3; k++)
                {
                    if (iPiv[k] == 0)
                    {
                        if (Math.Abs(minV[j, k]) < big) continue;

                        big = Math.Abs(minV[j, k]);
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

            minV[iCol, iCol] = 1d;
            for (var j = 0; j < 3; j++)
                minV[iCol, j] *= pivInv;

            // Subtract this row from others to zero out their columns
            for (var j = 0; j < 3; j++)
            {
                if (j == iCol) continue;

                var save = minV[j, iCol];

                minV[j, iCol] = 0d;
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
    public SquareMatrix2 SelfTranspose()
    {
        (Scalar01, Scalar10) = (Scalar10, Scalar01);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix2 Transpose()
    {
        var m = new SquareMatrix2
        {
            Scalar00 = Scalar00,
            Scalar01 = Scalar10,

            Scalar10 = Scalar01,
            Scalar11 = Scalar11
        };

        return m;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix2 InverseTranspose()
    {
        return Inverse().SelfTranspose();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix2 GetSquareMatrix2()
    {
        return new SquareMatrix2(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3 GetSquareMatrix3()
    {
        return new SquareMatrix3()
        {
            Scalar00 = Scalar00,
            Scalar01 = Scalar10,

            Scalar10 = Scalar01,
            Scalar11 = Scalar11,

            Scalar22 = 1d
        };
    }

    public double[,] GetArray2D()
    {
        var array = new double[3, 3];

        array[0, 0] = Scalar00;
        array[1, 0] = Scalar10;

        array[0, 1] = Scalar01;
        array[1, 1] = Scalar11;

        return array;
    }

    public bool SwapsHandedness
        => Determinant < 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapPoint(ILinFloat64Vector2D point)
    {
        Debug.Assert(IsAffine2D());

        return LinFloat64Vector2D.Create(Scalar00 * point.X + Scalar01 * point.Y,
            Scalar10 * point.X + Scalar11 * point.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapVector(ILinFloat64Vector2D vector)
    {
        Debug.Assert(IsAffine2D());

        return LinFloat64Vector2D.Create(Scalar00 * vector.X + Scalar01 * vector.Y,
            Scalar10 * vector.X + Scalar11 * vector.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapNormal(ILinFloat64Vector2D normal)
    {
        return InverseTranspose().MapVector(normal);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public IAffineMap2D GetInverseAffineMap()
    //{
    //    return Inverse();
    //}

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

//public sealed class SquareMatrix2
//{
//    public static SquareMatrix2 operator -(SquareMatrix2 m1)
//    {
//        Debug.Assert(!m1.HasNaNComponent);

//        var m = new SquareMatrix2
//        {
//            _items =
//            {
//                [0] = -m1._items[0],
//                [1] = -m1._items[1],
//                [2] = -m1._items[2],
//                [3] = -m1._items[3]
//            }
//        };

//        return m;
//    }

//    public static SquareMatrix2 operator +(SquareMatrix2 m1, SquareMatrix2 m2)
//    {
//        Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

//        var m = new SquareMatrix2
//        {
//            _items =
//            {
//                [0] = m1._items[0] + m2._items[0],
//                [1] = m1._items[1] + m2._items[1],
//                [2] = m1._items[2] + m2._items[2],
//                [3] = m1._items[3] + m2._items[3]
//            }
//        };

//        return m;
//    }

//    public static SquareMatrix2 operator -(SquareMatrix2 m1, SquareMatrix2 m2)
//    {
//        Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

//        var m = new SquareMatrix2
//        {
//            _items =
//            {
//                [0] = m1._items[0] - m2._items[0],
//                [1] = m1._items[1] - m2._items[1],
//                [2] = m1._items[2] - m2._items[2],
//                [3] = m1._items[3] - m2._items[3]
//            }
//        };

//        return m;
//    }

//    public static SquareMatrix2 operator *(SquareMatrix2 m1, SquareMatrix2 m2)
//    {
//        Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

//        var m = new SquareMatrix2
//        {
//            _items =
//            {
//                [0] = m1._items[0] * m2._items[0] +
//                      m1._items[2] * m2._items[1],
//                [1] = m1._items[1] * m2._items[0] +
//                      m1._items[3] * m2._items[1],
//                [2] = m1._items[0] * m2._items[2] +
//                      m1._items[2] * m2._items[3],
//                [3] = m1._items[1] * m2._items[2] +
//                      m1._items[3] * m2._items[3]
//            }
//        };


//        return m;
//    }

//    public static SquareMatrix2 operator *(SquareMatrix2 m1, double s)
//    {
//        Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

//        var m = new SquareMatrix2
//        {
//            _items =
//            {
//                [0] = s * m1._items[0],
//                [1] = s * m1._items[1],
//                [2] = s * m1._items[2],
//                [3] = s * m1._items[3]
//            }
//        };

//        return m;
//    }

//    public static SquareMatrix2 operator *(double s, SquareMatrix2 m1)
//    {
//        Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

//        var m = new SquareMatrix2
//        {
//            _items =
//            {
//                [0] = s * m1._items[0],
//                [1] = s * m1._items[1],
//                [2] = s * m1._items[2],
//                [3] = s * m1._items[3]
//            }
//        };

//        return m;
//    }

//    public static SquareMatrix2 operator /(SquareMatrix2 m1, double s)
//    {
//        Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

//        Debug.Assert(!s.IsAlmostZero());

//        s = 1.0d / s;

//        var m = new SquareMatrix2
//        {
//            _items =
//            {
//                [0] = s * m1._items[0],
//                [1] = s * m1._items[1],
//                [2] = s * m1._items[2],
//                [3] = s * m1._items[3]
//            }
//        };

//        return m;
//    }


//    private readonly double[] _items = new double[4];


//    /// <summary>
//    /// Get or set an item in this matrix using column-major order indexing
//    /// </summary>
//    /// <param name="index"></param>
//    /// <returns></returns>
//    public double this[int index]
//    {
//        get => _items[index];
//        set
//        {
//            Debug.Assert(!double.IsNaN(value));

//            _items[index] = value;
//        }
//    }

//    public double this[int i, int j]
//    {
//        get
//        {
//            Debug.Assert(i is >= 0 and <= 1 && j is >= 0 and <= 1);

//            return _items[i + (j << 1)];
//        }
//        set
//        {
//            Debug.Assert(i is >= 0 and <= 1 && j is >= 0 and <= 1 && !double.IsNaN(value));

//            _items[i + (j << 1)] = value;
//        }
//    }

//    /// <summary>
//    /// The squared Frobenius norm of this matrix
//    /// </summary>
//    public double FNormSquared
//    {
//        get { return _items.Sum(t => t * t); }
//    }

//    /// <summary>
//    /// The Frobenius norm of this matrix
//    /// </summary>
//    public double FNorm
//    {
//        get { return Math.Sqrt(_items.Sum(t => t * t)); }
//    }

//    public bool HasNaNComponent => _items.Any(double.IsNaN);

//    public double Determinant => _items[0] * _items[3] - _items[2] * _items[1];


//    public SquareMatrix2()
//    {

//    }

//    public SquareMatrix2(SquareMatrix2 m1)
//    {
//        _items[0] = m1._items[0];
//        _items[1] = m1._items[1];

//        _items[2] = m1._items[2];
//        _items[3] = m1._items[3];
//    }


//}