using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Maps.Space2D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace NumericalGeometryLib.BasicMath.Matrices
{
    public sealed class SquareMatrix3 : 
        IAffineMap2D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SquareMatrix3 CreateIdentityMatrix()
        {
            return new SquareMatrix3
            {
                Scalar00 = 1d,
                Scalar11 = 1d,
                Scalar22 = 1d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SquareMatrix3 CreateTranslationMatrix2D(double dx, double dy)
        {
            var m = new SquareMatrix3
            {
                Scalar00 = 1d,
                Scalar11 = 1d,
                Scalar02 = dx,
                Scalar12 = dy,
                Scalar22 = 1d
            };

            return m;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SquareMatrix3 CreateScalingMatrix2D(double sx, double sy)
        {
            return new SquareMatrix3
            {
                Scalar00 = sx,
                Scalar11 = sy,
                Scalar22 = 1d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SquareMatrix3 CreateScalingMatrix2D(double s)
        {
            return new SquareMatrix3
            {
                Scalar00 = s,
                Scalar11 = s,
                Scalar22 = 1d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SquareMatrix3 CreateXReflectionMatrix2D()
        {
            return new SquareMatrix3
            {
                Scalar00 = 1d,
                Scalar11 = -1d,
                Scalar22 = 1d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SquareMatrix3 CreateYReflectionMatrix2D()
        {
            return new SquareMatrix3
            {
                Scalar00 = -1d,
                Scalar11 = 1d,
                Scalar22 = 1d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SquareMatrix3 CreateOriginReflectionMatrix2D()
        {
            return new SquareMatrix3
            {
                Scalar00 = -1d,
                Scalar11 = -1d,
                Scalar22 = 1d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SquareMatrix3 CreateRotationMatrix2D(double radianAngle)
        {
            var m = new SquareMatrix3();

            var cosAngle = Math.Cos(radianAngle);
            var sinAngle = Math.Sin(radianAngle);

            m.Scalar00 = cosAngle;
            m.Scalar10 = sinAngle;
            m.Scalar01 = -sinAngle;
            m.Scalar11 = cosAngle;
            m.Scalar22 = 1d;

            return m;
        }

        public static SquareMatrix3 CreateAxisToVectorRotationMatrix3D(Axis3D axis, ITuple3D unitVector)
        {
            //Debug.Assert(unitVector.IsNearUnitVector());
            
            var x = unitVector.X;
            var y = unitVector.Y;
            var z = unitVector.Z;

            SquareMatrix3 matrix = null;

            if (axis == Axis3D.PositiveX)
            {
                var x1 = 1d / (x + 1d);
                var yz = -y * z * x1;

                matrix = new SquareMatrix3()
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
            }
            
            if (axis == Axis3D.NegativeX)
            {
                var x1 = 1d / (x - 1d);
                var yz = y * z * x1;

                matrix = new SquareMatrix3()
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
            }
            
            if (axis == Axis3D.PositiveY)
            {
                var y1 = 1d / (y + 1d);
                var xz = -x * z * y1;

                matrix = new SquareMatrix3()
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
            }
            
            if (axis == Axis3D.NegativeY)
            {
                var y1 = 1d / (y - 1d);
                var xz = x * z * y1;

                matrix = new SquareMatrix3()
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
            }
            
            if (axis == Axis3D.PositiveZ)
            {
                var z1 = 1d / (z + 1d);
                var xy = -x * y * z1;

                matrix = new SquareMatrix3()
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
            }
            
            if (axis == Axis3D.NegativeZ)
            {
                var z1 = 1d / (z - 1d);
                var xy = x * y * z1;

                matrix = new SquareMatrix3()
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
            }

            return matrix;
        }

        public static SquareMatrix3 CreateVectorToAxisRotationMatrix3D(ITuple3D unitVector, Axis3D axis)
        {
            //Debug.Assert(unitVector.IsValid() && unitVector.IsNearUnitVector());
            
            var x = unitVector.X;
            var y = unitVector.Y;
            var z = unitVector.Z;

            SquareMatrix3 matrix = null;

            if (axis == Axis3D.PositiveX)
            {
                var x1 = 1d / (x + 1d);
                var yz = -y * z * x1;

                matrix = new SquareMatrix3()
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
            }
            
            if (axis == Axis3D.NegativeX)
            {
                var x1 = 1d / (x - 1d);
                var yz = y * z * x1;

                matrix = new SquareMatrix3()
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
            }
            
            if (axis == Axis3D.PositiveY)
            {
                var y1 = 1d / (y + 1d);
                var xz = -x * z * y1;

                matrix = new SquareMatrix3()
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
            }
            
            if (axis == Axis3D.NegativeY)
            {
                var y1 = 1d / (y - 1d);
                var xz = x * z * y1;

                matrix = new SquareMatrix3()
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
            }
            
            if (axis == Axis3D.PositiveZ)
            {
                var z1 = 1d / (z + 1d);
                var xy = -x * y * z1;

                matrix = new SquareMatrix3()
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
            }
            
            if (axis == Axis3D.NegativeZ)
            {
                var z1 = 1d / (z - 1d);
                var xy = x * y * z1;

                matrix = new SquareMatrix3()
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
            }

            return matrix;
        }

        public static SquareMatrix3 CreateVectorToVectorRotationMatrix3D(ITuple3D unitVector1, ITuple3D unitVector2)
        {
            //Debug.Assert(
            //    unitVector1.IsNearUnitVector() && 
            //    unitVector2.IsNearUnitVector()
            //);

            var sumVector = new Tuple3D(
                unitVector1.X + unitVector2.X,
                unitVector1.Y + unitVector2.Y,
                unitVector1.Z + unitVector2.Z
            );

            if (sumVector.IsAlmostZero())
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
                    0 => sumVector.X > 0 ? Axis3D.PositiveX : Axis3D.NegativeX,
                    1 => sumVector.Y > 0 ? Axis3D.PositiveY : Axis3D.NegativeY,
                    _ => sumVector.Z > 0 ? Axis3D.PositiveZ : Axis3D.NegativeZ
                };

                var m1 = CreateVectorToAxisRotationMatrix3D(unitVector1, axis);
                var m2 = CreateAxisToVectorRotationMatrix3D(axis, unitVector2);
                
                //var matrix = m2 * m1;

                //Debug.Assert((matrix * unitVector1).IsNearVector(unitVector2));

                return m2 * m1;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SquareMatrix3 operator -(SquareMatrix3 m1)
        {
            Debug.Assert(m1.IsValid());

            return new SquareMatrix3
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
        public static SquareMatrix3 operator +(SquareMatrix3 m1, SquareMatrix3 m2)
        {
            Debug.Assert(m1.IsValid() && m2.IsValid());

            return new SquareMatrix3
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
        public static SquareMatrix3 operator -(SquareMatrix3 m1, SquareMatrix3 m2)
        {
            Debug.Assert(m1.IsValid() && m2.IsValid());
            
            return new SquareMatrix3
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
        public static Tuple3D operator *(SquareMatrix3 m, ITuple3D vector)
        {
            var x = vector.X;
            var y = vector.Y;
            var z = vector.Z;

            return new Tuple3D(
                m.Scalar00 * x + m.Scalar01 * y + m.Scalar02 * z,
                m.Scalar10 * x + m.Scalar11 * y + m.Scalar12 * z,
                m.Scalar20 * x + m.Scalar21 * y + m.Scalar22 * z
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D operator *(ITuple3D vector, SquareMatrix3 m)
        {
            var x = vector.X;
            var y = vector.Y;
            var z = vector.Z;

            return new Tuple3D(
                m.Scalar00 * x + m.Scalar10 * y + m.Scalar20 * z,
                m.Scalar01 * x + m.Scalar11 * y + m.Scalar21 * z,
                m.Scalar02 * x + m.Scalar12 * y + m.Scalar22 * z
            );
        }

        public static SquareMatrix3 operator *(SquareMatrix3 m1, SquareMatrix3 m2)
        {
            Debug.Assert(m1.IsValid() && m2.IsValid());

            return new SquareMatrix3
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
        public static SquareMatrix3 operator *(SquareMatrix3 m1, double s)
        {
            Debug.Assert(m1.IsValid() && double.IsNaN(s));

            return new SquareMatrix3
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
        public static SquareMatrix3 operator *(double s, SquareMatrix3 m1)
        {
            Debug.Assert(m1.IsValid() && !double.IsNaN(s));

            return new SquareMatrix3
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
        public static SquareMatrix3 operator /(SquareMatrix3 m1, double s)
        {
            Debug.Assert(m1.IsValid() && !double.IsNaN(s));
            
            s = 1d / s;

            return new SquareMatrix3
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


        public double Scalar00 { get; set; }

        public double Scalar01 { get; set; }

        public double Scalar02 { get; set; }

        public double Scalar10 { get; set; }

        public double Scalar11 { get; set; }

        public double Scalar12 { get; set; }

        public double Scalar20 { get; set; }

        public double Scalar21 { get; set; }

        public double Scalar22 { get; set; }


        //private readonly double[]  = new double[9];

        ///// <summary>
        ///// Get or set an item in this matrix using column-major indexing
        ///// </summary>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //public double this[int index]
        //{
        //    get { return [index]; }
        //    set
        //    {
        //        Debug.Assert(!double.IsNaN(value));

        //        [index] = value;
        //    }
        //}

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
        public double FNormSquared 
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
        public double FNorm 
            => Math.Sqrt(FNormSquared);

        public double Determinant 
            => Scalar00 * (Scalar11 * Scalar22 - Scalar12 * Scalar21) +
               Scalar01 * (Scalar12 * Scalar20 - Scalar10 * Scalar22) +
               Scalar02 * (Scalar10 * Scalar21 - Scalar11 * Scalar20);


        public SquareMatrix3()
        {
        }

        public SquareMatrix3(SquareMatrix3 m1)
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

        public SquareMatrix3 NearestOrthogonalMatrix()
        {
            var r1 = new SquareMatrix3(this);
            var r2 = r1;

            for (var i = 0; i < 100; i++)
            {
                r2 = 0.5.Lerp(r1, r1.InverseTranspose());

                var norm = (r1 - r2).FNormSquared;

                if (norm.IsAlmostZero()) break;

                r1 = r2;
            }

            return r2;
        }

        /// <summary>
        /// https://github.com/mmp/pbrt-v3/blob/master/src/core/quaternion.cpp
        /// </summary>
        /// <returns></returns>
        public Tuple4D ToQuaternion()
        {
            var trace = Scalar00 + Scalar11 + Scalar22;
            if (trace > 0d)
            {
                // Compute w from matrix trace, then xyz
                // 4w^2 = m[0, 0] + m[1, 1] + m[2, 2] + m[3, 3] (but m[3, 3] == 1)
                var s = Math.Sqrt(trace + 1d);
                var w = 0.5d * s;
                s = 0.5d / s;

                return new Tuple4D(
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
                var q = new double[3];
                var i = 0;
                if (Scalar11 > Scalar00) i = 1;
                if (Scalar22 > this[i, i]) i = 2;
                var j = nxt[i]; //j = (i + 1) % 3
                var k = nxt[j]; //k = (j + 1) % 3
                var s = Math.Sqrt(this[i, i] - (this[j, j] + this[k, k]) + 1d);
                q[i] = s * 0.5d;
                if (!s.IsAlmostZero()) s = 0.5d / s;
                var w = (this[k, j] - this[j, k]) * s;
                q[j] = (this[j, i] + this[i, j]) * s;
                q[k] = (this[k, i] + this[i, k]) * s;

                return new Tuple4D(q[0], q[1], q[2], w);
            }
        }

        /// <summary>
        /// https://github.com/mmp/pbrt-v2/blob/master/src/core/transform.cpp
        /// </summary>
        /// <returns></returns>
        public SquareMatrix3 Inverse()
        {
            var indxc = new int[3];
            var indxr = new int[3];
            var ipiv = new[] { 0, 0, 0 };
            var minv = new SquareMatrix3(this);

            for (var i = 0; i < 3; i++)
            {
                int irow = 0, icol = 0;
                var big = 0d;

                // Choose pivot
                for (var j = 0; j < 3; j++)
                {
                    if (ipiv[j] == 1) continue;

                    for (var k = 0; k < 3; k++)
                    {
                        if (ipiv[k] == 0)
                        {
                            if (Math.Abs(minv[j, k]) < big) continue;

                            big = Math.Abs(minv[j, k]);
                            irow = j;
                            icol = k;
                        }
                        else if (ipiv[k] > 1)
                            throw new InvalidOperationException("Singular matrix");
                    }
                }

                ++ipiv[icol];

                // Swap rows _irow_ and _icol_ for pivot
                if (irow != icol)
                {
                    for (var k = 0; k < 3; ++k)
                    {
                        (minv[irow, k], minv[icol, k]) = (minv[icol, k], minv[irow, k]);
                    }
                }

                indxr[i] = irow;
                indxc[i] = icol;
                if (minv[icol, icol].IsAlmostZero())
                    throw new InvalidOperationException("Singular matrix");

                // Set minv[icol, icol] to one by scaling row _icol_ appropriately
                var pivinv = 1d / minv[icol, icol];

                minv[icol, icol] = 1d;
                for (var j = 0; j < 3; j++)
                    minv[icol, j] *= pivinv;

                // Subtract this row from others to zero out their columns
                for (var j = 0; j < 3; j++)
                {
                    if (j == icol) continue;

                    var save = minv[j, icol];

                    minv[j, icol] = 0d;
                    for (var k = 0; k < 3; k++)
                        minv[j, k] -= minv[icol, k] * save;
                }
            }

            // Swap columns to reflect permutation
            for (var j = 2; j >= 0; j--)
            {
                if (indxr[j] == indxc[j]) continue;

                for (var k = 0; k < 3; k++)
                {
                    (minv[k, indxr[j]], minv[k, indxc[j]]) = (minv[k, indxc[j]], minv[k, indxr[j]]);
                }
            }

            return minv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix3 SelfTranspose()
        {
            (Scalar01, Scalar10) = (Scalar10, Scalar01);
            (Scalar12, Scalar21) = (Scalar21, Scalar12);
            (Scalar20, Scalar02) = (Scalar02, Scalar20);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix3 Transpose()
        {
            var m = new SquareMatrix3
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
        public SquareMatrix3 InverseTranspose()
        {
            return Inverse().SelfTranspose();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix3 ToSquareMatrix3()
        {
            return new SquareMatrix3(this);
        }

        public double[,] ToArray2D()
        {
            var array = new double[3, 3];
            
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
        public Tuple2D MapPoint(ITuple2D point)
        {
            Debug.Assert(IsAffine2D());

            return new Tuple2D(
                Scalar00 * point.X + Scalar01 * point.Y + Scalar02,
                Scalar10 * point.X + Scalar11 * point.Y + Scalar12
            ); 
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple2D MapVector(ITuple2D vector)
        {
            Debug.Assert(IsAffine2D());

            return new Tuple2D(
                Scalar00 * vector.X + Scalar01 * vector.Y,
                Scalar10 * vector.X + Scalar11 * vector.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple2D MapNormal(ITuple2D normal)
        {
            return InverseTranspose().MapVector(normal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap2D InverseMap()
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
}