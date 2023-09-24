using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices
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
        public static SquareMatrix3 CreateRotationMatrix2D(Float64PlanarAngle radianAngle)
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

        public static SquareMatrix3 CreateAxisToVectorRotationMatrix3D(LinUnitBasisVector3D axis, IFloat64Vector3D unitVector)
        {
            //Debug.Assert(unitVector.IsNearUnitVector());
            
            var x = unitVector.X;
            var y = unitVector.Y;
            var z = unitVector.Z;
            
            if (axis == LinUnitBasisVector3D.PositiveX)
            {
                var x1 = 1d / (x + 1d);
                var yz = -y * z * x1;

                var matrix = new SquareMatrix3()
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
            
            if (axis == LinUnitBasisVector3D.NegativeX)
            {
                var x1 = 1d / (x - 1d);
                var yz = y * z * x1;

                var matrix = new SquareMatrix3()
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
            
            if (axis == LinUnitBasisVector3D.PositiveY)
            {
                var y1 = 1d / (y + 1d);
                var xz = -x * z * y1;

                var matrix = new SquareMatrix3()
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
            
            if (axis == LinUnitBasisVector3D.NegativeY)
            {
                var y1 = 1d / (y - 1d);
                var xz = x * z * y1;

                var matrix = new SquareMatrix3()
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
            
            if (axis == LinUnitBasisVector3D.PositiveZ)
            {
                var z1 = 1d / (z + 1d);
                var xy = -x * y * z1;

                var matrix = new SquareMatrix3()
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
            
            if (axis == LinUnitBasisVector3D.NegativeZ)
            {
                var z1 = 1d / (z - 1d);
                var xy = x * y * z1;

                var matrix = new SquareMatrix3()
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

        public static SquareMatrix3 CreateVectorToAxisRotationMatrix3D(IFloat64Vector3D unitVector, LinUnitBasisVector3D axis)
        {
            //Debug.Assert(unitVector.IsValid() && unitVector.IsNearUnitVector());
            
            var x = unitVector.X;
            var y = unitVector.Y;
            var z = unitVector.Z;
            
            if (axis == LinUnitBasisVector3D.PositiveX)
            {
                var x1 = 1d / (x + 1d);
                var yz = -y * z * x1;

                var matrix = new SquareMatrix3()
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
            
            if (axis == LinUnitBasisVector3D.NegativeX)
            {
                var x1 = 1d / (x - 1d);
                var yz = y * z * x1;

                var matrix = new SquareMatrix3()
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
            
            if (axis == LinUnitBasisVector3D.PositiveY)
            {
                var y1 = 1d / (y + 1d);
                var xz = -x * z * y1;

                var matrix = new SquareMatrix3()
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
            
            if (axis == LinUnitBasisVector3D.NegativeY)
            {
                var y1 = 1d / (y - 1d);
                var xz = x * z * y1;

                var matrix = new SquareMatrix3()
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
            
            if (axis == LinUnitBasisVector3D.PositiveZ)
            {
                var z1 = 1d / (z + 1d);
                var xy = -x * y * z1;

                var matrix = new SquareMatrix3()
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
            
            if (axis == LinUnitBasisVector3D.NegativeZ)
            {
                var z1 = 1d / (z - 1d);
                var xy = x * y * z1;

                var matrix = new SquareMatrix3()
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

        public static SquareMatrix3 CreateVectorToVectorRotationMatrix3D(IFloat64Vector3D unitVector1, IFloat64Vector3D unitVector2)
        {
            //Debug.Assert(
            //    unitVector1.IsNearUnitVector() && 
            //    unitVector2.IsNearUnitVector()
            //);

            var sumVector = Float64Vector3D.Create(unitVector1.X + unitVector2.X,
                unitVector1.Y + unitVector2.Y,
                unitVector1.Z + unitVector2.Z);

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
                        ? LinUnitBasisVector3D.PositiveX 
                        : LinUnitBasisVector3D.NegativeX,

                    1 => sumVector.Y.IsPositive() 
                        ? LinUnitBasisVector3D.PositiveY 
                        : LinUnitBasisVector3D.NegativeY,

                    _ => sumVector.Z.IsPositive() 
                        ? LinUnitBasisVector3D.PositiveZ 
                        : LinUnitBasisVector3D.NegativeZ
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
        public static Float64Vector3D operator *(SquareMatrix3 m, IFloat64Vector3D vector)
        {
            var x = vector.X;
            var y = vector.Y;
            var z = vector.Z;

            return Float64Vector3D.Create(m.Scalar00 * x + m.Scalar01 * y + m.Scalar02 * z,
                m.Scalar10 * x + m.Scalar11 * y + m.Scalar12 * z,
                m.Scalar20 * x + m.Scalar21 * y + m.Scalar22 * z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D operator *(IFloat64Vector3D vector, SquareMatrix3 m)
        {
            var x = vector.X;
            var y = vector.Y;
            var z = vector.Z;

            return Float64Vector3D.Create(m.Scalar00 * x + m.Scalar10 * y + m.Scalar20 * z,
                m.Scalar01 * x + m.Scalar11 * y + m.Scalar21 * z,
                m.Scalar02 * x + m.Scalar12 * y + m.Scalar22 * z);
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
            Debug.Assert(m1.IsValid() && Float64Scalar.IsNaN(s));

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
            Debug.Assert(m1.IsValid() && !Float64Scalar.IsNaN(s));

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
            Debug.Assert(m1.IsValid() && !Float64Scalar.IsNaN(s));
            
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


        public Float64Scalar Scalar00 { get; set; }

        public Float64Scalar Scalar01 { get; set; }

        public Float64Scalar Scalar02 { get; set; }

        public Float64Scalar Scalar10 { get; set; }

        public Float64Scalar Scalar11 { get; set; }

        public Float64Scalar Scalar12 { get; set; }

        public Float64Scalar Scalar20 { get; set; }

        public Float64Scalar Scalar21 { get; set; }

        public Float64Scalar Scalar22 { get; set; }

        
        public Float64Scalar this[int i, int j]
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
                Debug.Assert(!Float64Scalar.IsNaN(value));

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
        public Float64Scalar FNormSquared 
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
        public Float64Scalar FNorm 
            => Math.Sqrt(FNormSquared);

        public Float64Scalar Determinant 
            => Scalar00 * (Scalar11 * Scalar22 - Scalar12 * Scalar21) +
               Scalar01 * (Scalar12 * Scalar20 - Scalar10 * Scalar22) +
               Scalar02 * (Scalar10 * Scalar21 - Scalar11 * Scalar20);
        
        public bool SwapsHandedness 
            => Determinant.IsNegative();


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
        
        public SquareMatrix3(double[,] m1)
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

        public SquareMatrix3 NearestOrthogonalMatrix()
        {
            var r1 = new SquareMatrix3(this);
            var r2 = r1;

            for (var i = 0; i < 100; i++)
            {
                r2 = 0.5.Lerp(r1, r1.InverseTranspose());

                var norm = (r1 - r2).FNormSquared;

                if (norm.Value.IsAlmostZero()) break;

                r1 = r2;
            }

            return r2;
        }

        /// <summary>
        /// https://github.com/mmp/pbrt-v3/blob/master/src/core/quaternion.cpp
        /// </summary>
        /// <returns></returns>
        public Float64Quaternion ToQuaternion()
        {
            var trace = Scalar00 + Scalar11 + Scalar22;
            if (trace > 0d)
            {
                // Compute w from matrix trace, then xyz
                // 4w^2 = m[0, 0] + m[1, 1] + m[2, 2] + m[3, 3] (but m[3, 3] == 1)
                var s = Math.Sqrt(trace + 1d);
                var w = 0.5d * s;
                s = 0.5d / s;

                return Float64Quaternion.Create((Scalar21 - Scalar12) * s,
                    (Scalar02 - Scalar20) * s,
                    (Scalar10 - Scalar01) * s,
                    w);
            }
            else
            {
                // Compute largest of x, y, or z, then remaining components
                //TODO: Replace this array with % computations
                var nxt = new[] { 1, 2, 0 };
                var q = new Float64Scalar[3];
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

                return Float64Quaternion.Create(q[0], q[1], q[2], w);
            }
        }

        /// <summary>
        /// https://github.com/mmp/pbrt-v2/blob/master/src/core/transform.cpp
        /// </summary>
        /// <returns></returns>
        public SquareMatrix3 Inverse()
        {
            var indexC = new int[3];
            var indexR = new int[3];
            var iPiv = new[] { 0, 0, 0 };
            var minV = new SquareMatrix3(this);

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
                if (minV[iCol, iCol].Value.IsAlmostZero())
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
        public SquareMatrix3 GetSquareMatrix3()
        {
            return new SquareMatrix3(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetArray2D()
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
        public Complex[,] GetComplexArray2D()
        {
            var array = new Complex[3, 3];
            
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
        public Matrix<double> GetMatrix()
        {
            return Matrix<double>.Build.DenseOfArray(
                GetArray2D()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<Complex> GetComplexMatrix()
        {
            return Matrix<Complex>.Build.DenseOfArray(
                GetComplexArray2D()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D DiagonalToTuple3D()
        {
            return Float64Vector3D.Create(Scalar00, Scalar11, Scalar22);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D RowToTuple3D(int rowIndex)
        {
            return rowIndex switch
            {
                0 => Float64Vector3D.Create(Scalar00, Scalar01, Scalar02),
                1 => Float64Vector3D.Create(Scalar10, Scalar11, Scalar12),
                2 => Float64Vector3D.Create(Scalar20, Scalar21, Scalar22),
                _ => throw new IndexOutOfRangeException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D ColumnToTuple3D(int colIndex)
        {
            return colIndex switch
            {
                0 => Float64Vector3D.Create(Scalar00, Scalar10, Scalar20),
                1 => Float64Vector3D.Create(Scalar01, Scalar11, Scalar21),
                2 => Float64Vector3D.Create(Scalar02, Scalar12, Scalar22),
                _ => throw new IndexOutOfRangeException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix3 SetDiagonal(ITriplet<double> vector)
        {
            Scalar00 = vector.Item1;
            Scalar11 = vector.Item2;
            Scalar22 = vector.Item3;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix3 SetRow(int rowIndex, ITriplet<double> vector)
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
        public SquareMatrix3 SetColumn(int colIndex, ITriplet<double> vector)
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
        public Float64Vector2D MapPoint(IFloat64Vector2D point)
        {
            Debug.Assert(IsAffine2D());

            return Float64Vector2D.Create(Scalar00 * point.X + Scalar01 * point.Y + Scalar02,
                Scalar10 * point.X + Scalar11 * point.Y + Scalar12); 
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D MapVector(IFloat64Vector2D vector)
        {
            Debug.Assert(IsAffine2D());

            return Float64Vector2D.Create(Scalar00 * vector.X + Scalar01 * vector.Y,
                Scalar10 * vector.X + Scalar11 * vector.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D MapNormal(IFloat64Vector2D normal)
        {
            return InverseTranspose().MapVector(normal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap2D GetInverseAffineMap()
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