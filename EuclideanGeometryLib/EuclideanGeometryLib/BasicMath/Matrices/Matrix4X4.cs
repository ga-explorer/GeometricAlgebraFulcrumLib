using System;
using System.Diagnostics;
using System.Linq;
using EuclideanGeometryLib.BasicMath.Maps.Space3D;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Matrices
{
    public sealed class Matrix4X4 : IAffineMap3D
    {
        public static Matrix4X4 CreateZeroMatrix()
        {
            return new Matrix4X4();
        }

        public static Matrix4X4 CreateIdentityMatrix()
        {
            var m = new Matrix4X4();

            m._items[0] = 1.0d;
            m._items[5] = 1.0d;
            m._items[10] = 1.0d;
            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateTranslationMatrix(double dx, double dy, double dz)
        {
            Debug.Assert(!double.IsNaN(dx) && !double.IsNaN(dy) && !double.IsNaN(dz));

            var m = new Matrix4X4();

            m._items[0] = 1.0d;
            m._items[5] = 1.0d;
            m._items[10] = 1.0d;
            m._items[15] = 1.0d;

            m._items[12] = dx;
            m._items[13] = dy;
            m._items[14] = dz;

            return m;
        }

        public static Matrix4X4 CreateScalingMatrix(double sx, double sy, double sz)
        {
            Debug.Assert(!double.IsNaN(sx) && !double.IsNaN(sy) && !double.IsNaN(sz));

            var m = new Matrix4X4();

            m._items[0] = sx;
            m._items[5] = sy;
            m._items[10] = sz;
            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateScalingMatrix(double s)
        {
            Debug.Assert(!double.IsNaN(s) && !double.IsNaN(s) && !double.IsNaN(s));

            var m = new Matrix4X4();

            m._items[0] = s;
            m._items[5] = s;
            m._items[10] = s;
            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateXyReflectionMatrix()
        {
            var m = new Matrix4X4();

            m._items[0] = 1.0d;
            m._items[5] = 1.0d;
            m._items[10] = -1.0d;
            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateYzReflectionMatrix()
        {
            var m = new Matrix4X4();

            m._items[0] = -1.0d;
            m._items[5] = 1.0d;
            m._items[10] = 1.0d;
            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateZxReflectionMatrix()
        {
            var m = new Matrix4X4();

            m._items[0] = 1.0d;
            m._items[5] = -1.0d;
            m._items[10] = 1.0d;
            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateXReflectionMatrix()
        {
            var m = new Matrix4X4();

            m._items[0] = 1.0d;
            m._items[5] = -1.0d;
            m._items[10] = -1.0d;
            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateYReflectionMatrix()
        {
            var m = new Matrix4X4();

            m._items[0] = -1.0d;
            m._items[5] = 1.0d;
            m._items[10] = -1.0d;
            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateZReflectionMatrix()
        {
            var m = new Matrix4X4();

            m._items[0] = -1.0d;
            m._items[5] = -1.0d;
            m._items[10] = 1.0d;
            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateOriginReflectionMatrix()
        {
            var m = new Matrix4X4();

            m._items[0] = -1.0d;
            m._items[5] = -1.0d;
            m._items[10] = -1.0d;
            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateXRotationMatrix(double radianAngle)
        {
            Debug.Assert(!double.IsNaN(radianAngle));

            var m = new Matrix4X4();

            var cosAngle = Math.Cos(radianAngle);
            var sinAngle = Math.Sin(radianAngle);

            m._items[0] = 1.0d;

            m._items[5] = cosAngle;
            m._items[6] = sinAngle;

            m._items[9] = -sinAngle;
            m._items[10] = cosAngle;

            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateYRotationMatrix(double radianAngle)
        {
            Debug.Assert(!double.IsNaN(radianAngle));

            var m = new Matrix4X4();

            var cosAngle = Math.Cos(radianAngle);
            var sinAngle = Math.Sin(radianAngle);

            m._items[0] = cosAngle;
            m._items[2] = -sinAngle;

            m._items[5] = 1.0d;

            m._items[8] = sinAngle;
            m._items[10] = cosAngle;

            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateZRotationMatrix(double radianAngle)
        {
            Debug.Assert(!double.IsNaN(radianAngle));

            var m = new Matrix4X4();

            var cosAngle = Math.Cos(radianAngle);
            var sinAngle = Math.Sin(radianAngle);

            m._items[0] = cosAngle;
            m._items[1] = sinAngle;

            m._items[4] = -sinAngle;
            m._items[5] = cosAngle;

            m._items[10] = 1.0d;

            m._items[15] = 1.0d;

            return m;
        }

        public static Matrix4X4 CreateRotationMatrix(Tuple3D unitAxis, double radianAngle)
        {
            Debug.Assert(!unitAxis.IsInvalid && !double.IsNaN(radianAngle));

            Debug.Assert(unitAxis.IsUnitVector());

            var m = new Matrix4X4();

            var cosAngle = Math.Cos(radianAngle);
            var sinAngle = Math.Sin(radianAngle);
            var oneMinusCosAngle = 1.0d - cosAngle;

            m._items[0] = cosAngle + unitAxis.X * unitAxis.X * oneMinusCosAngle;
            m._items[1] = unitAxis.Y * unitAxis.X * oneMinusCosAngle + unitAxis.Z * sinAngle;
            m._items[2] = unitAxis.Z * unitAxis.X * oneMinusCosAngle - unitAxis.Y * sinAngle;

            m._items[4] = unitAxis.X * unitAxis.Y * oneMinusCosAngle - unitAxis.Z * sinAngle;
            m._items[5] = cosAngle + unitAxis.Y * unitAxis.Y * oneMinusCosAngle;
            m._items[6] = unitAxis.Z * unitAxis.Y * oneMinusCosAngle + unitAxis.X * sinAngle;

            m._items[8] = unitAxis.X * unitAxis.Z * oneMinusCosAngle + unitAxis.Y * sinAngle;
            m._items[9] = unitAxis.Y * unitAxis.Z * oneMinusCosAngle - unitAxis.X * sinAngle;
            m._items[10] = cosAngle + unitAxis.Z * unitAxis.Z * oneMinusCosAngle;

            return m;
        }


        public static Matrix4X4 operator -(Matrix4X4 m1)
        {
            Debug.Assert(!m1.HasNaNComponents);

            var m = new Matrix4X4();

            m._items[0] = -m1._items[0];
            m._items[1] = -m1._items[1];
            m._items[2] = -m1._items[2];
            m._items[3] = -m1._items[3];

            m._items[4] = -m1._items[4];
            m._items[5] = -m1._items[5];
            m._items[6] = -m1._items[6];
            m._items[7] = -m1._items[7];

            m._items[8] = -m1._items[8];
            m._items[9] = -m1._items[9];
            m._items[10] = -m1._items[10];
            m._items[11] = -m1._items[11];

            m._items[12] = -m1._items[12];
            m._items[13] = -m1._items[13];
            m._items[14] = -m1._items[14];
            m._items[15] = -m1._items[15];

            return m;
        }

        public static Matrix4X4 operator +(Matrix4X4 m1, Matrix4X4 m2)
        {
            Debug.Assert(!m1.HasNaNComponents && !m2.HasNaNComponents);

            var m = new Matrix4X4();

            m._items[0] = m1._items[0] + m2._items[0];
            m._items[1] = m1._items[1] + m2._items[1];
            m._items[2] = m1._items[2] + m2._items[2];
            m._items[3] = m1._items[3] + m2._items[3];

            m._items[4] = m1._items[4] + m2._items[4];
            m._items[5] = m1._items[5] + m2._items[5];
            m._items[6] = m1._items[6] + m2._items[6];
            m._items[7] = m1._items[7] + m2._items[7];

            m._items[8] = m1._items[8] + m2._items[8];
            m._items[9] = m1._items[9] + m2._items[9];
            m._items[10] = m1._items[10] + m2._items[10];
            m._items[11] = m1._items[11] + m2._items[11];

            m._items[12] = m1._items[12] + m2._items[12];
            m._items[13] = m1._items[13] + m2._items[13];
            m._items[14] = m1._items[14] + m2._items[14];
            m._items[15] = m1._items[15] + m2._items[15];

            return m;
        }

        public static Matrix4X4 operator -(Matrix4X4 m1, Matrix4X4 m2)
        {
            Debug.Assert(!m1.HasNaNComponents && !m2.HasNaNComponents);

            var m = new Matrix4X4();

            m._items[0] = m1._items[0] - m2._items[0];
            m._items[1] = m1._items[1] - m2._items[1];
            m._items[2] = m1._items[2] - m2._items[2];
            m._items[3] = m1._items[3] - m2._items[3];

            m._items[4] = m1._items[4] - m2._items[4];
            m._items[5] = m1._items[5] - m2._items[5];
            m._items[6] = m1._items[6] - m2._items[6];
            m._items[7] = m1._items[7] - m2._items[7];

            m._items[8] = m1._items[8] - m2._items[8];
            m._items[9] = m1._items[9] - m2._items[9];
            m._items[10] = m1._items[10] - m2._items[10];
            m._items[11] = m1._items[11] - m2._items[11];

            m._items[12] = m1._items[12] - m2._items[12];
            m._items[13] = m1._items[13] - m2._items[13];
            m._items[14] = m1._items[14] - m2._items[14];
            m._items[15] = m1._items[15] - m2._items[15];

            return m;
        }

        public static Matrix4X4 operator *(Matrix4X4 m1, Matrix4X4 m2)
        {
            Debug.Assert(!m1.HasNaNComponents && !m2.HasNaNComponents);

            var m = new Matrix4X4();

            m._items[0] =
                m1._items[0] * m2._items[0] +
                m1._items[1] * m2._items[4] +
                m1._items[2] * m2._items[8] +
                m1._items[3] * m2._items[12];

            m._items[1] =
                m1._items[0] * m2._items[1] +
                m1._items[1] * m2._items[5] +
                m1._items[2] * m2._items[9] +
                m1._items[3] * m2._items[13];

            m._items[2] =
                m1._items[0] * m2._items[2] +
                m1._items[1] * m2._items[6] +
                m1._items[2] * m2._items[10] +
                m1._items[3] * m2._items[14];

            m._items[3] =
                m1._items[0] * m2._items[3] +
                m1._items[1] * m2._items[7] +
                m1._items[2] * m2._items[11] +
                m1._items[3] * m2._items[15];


            m._items[4] =
                m1._items[4] * m2._items[0] +
                m1._items[5] * m2._items[4] +
                m1._items[6] * m2._items[8] +
                m1._items[7] * m2._items[12];

            m._items[5] =
                m1._items[4] * m2._items[1] +
                m1._items[5] * m2._items[5] +
                m1._items[6] * m2._items[9] +
                m1._items[7] * m2._items[13];

            m._items[6] =
                m1._items[4] * m2._items[2] +
                m1._items[5] * m2._items[6] +
                m1._items[6] * m2._items[10] +
                m1._items[7] * m2._items[14];

            m._items[7] =
                m1._items[4] * m2._items[3] +
                m1._items[5] * m2._items[7] +
                m1._items[6] * m2._items[11] +
                m1._items[7] * m2._items[15];


            m._items[8] =
                m1._items[8] * m2._items[0] +
                m1._items[9] * m2._items[4] +
                m1._items[10] * m2._items[8] +
                m1._items[11] * m2._items[12];

            m._items[9] =
                m1._items[8] * m2._items[1] +
                m1._items[9] * m2._items[5] +
                m1._items[10] * m2._items[9] +
                m1._items[11] * m2._items[13];

            m._items[10] =
                m1._items[8] * m2._items[2] +
                m1._items[9] * m2._items[6] +
                m1._items[10] * m2._items[10] +
                m1._items[11] * m2._items[14];

            m._items[11] =
                m1._items[8] * m2._items[3] +
                m1._items[9] * m2._items[7] +
                m1._items[10] * m2._items[11] +
                m1._items[11] * m2._items[15];


            m._items[12] =
                m1._items[12] * m2._items[0] +
                m1._items[13] * m2._items[4] +
                m1._items[14] * m2._items[8] +
                m1._items[15] * m2._items[12];

            m._items[13] =
                m1._items[12] * m2._items[1] +
                m1._items[13] * m2._items[5] +
                m1._items[14] * m2._items[9] +
                m1._items[15] * m2._items[13];

            m._items[14] =
                m1._items[12] * m2._items[2] +
                m1._items[13] * m2._items[6] +
                m1._items[14] * m2._items[10] +
                m1._items[15] * m2._items[14];

            m._items[15] =
                m1._items[12] * m2._items[3] +
                m1._items[13] * m2._items[7] +
                m1._items[14] * m2._items[11] +
                m1._items[15] * m2._items[15];

            return m;
        }

        public static Matrix4X4 operator *(Matrix4X4 m1, double s)
        {
            Debug.Assert(!m1.HasNaNComponents && !double.IsNaN(s));

            var m = new Matrix4X4();

            m._items[0] = s * m1._items[0];
            m._items[1] = s * m1._items[1];
            m._items[2] = s * m1._items[2];
            m._items[3] = s * m1._items[3];

            m._items[4] = s * m1._items[4];
            m._items[5] = s * m1._items[5];
            m._items[6] = s * m1._items[6];
            m._items[7] = s * m1._items[7];

            m._items[8] = s * m1._items[8];
            m._items[9] = s * m1._items[9];
            m._items[10] = s * m1._items[10];
            m._items[11] = s * m1._items[11];

            m._items[12] = s * m1._items[12];
            m._items[13] = s * m1._items[13];
            m._items[14] = s * m1._items[14];
            m._items[15] = s * m1._items[15];

            return m;
        }

        public static Matrix4X4 operator *(double s, Matrix4X4 m1)
        {
            Debug.Assert(!m1.HasNaNComponents && !double.IsNaN(s));

            var m = new Matrix4X4();

            m._items[0] = s * m1._items[0];
            m._items[1] = s * m1._items[1];
            m._items[2] = s * m1._items[2];
            m._items[3] = s * m1._items[3];

            m._items[4] = s * m1._items[4];
            m._items[5] = s * m1._items[5];
            m._items[6] = s * m1._items[6];
            m._items[7] = s * m1._items[7];

            m._items[8] = s * m1._items[8];
            m._items[9] = s * m1._items[9];
            m._items[10] = s * m1._items[10];
            m._items[11] = s * m1._items[11];

            m._items[12] = s * m1._items[12];
            m._items[13] = s * m1._items[13];
            m._items[14] = s * m1._items[14];
            m._items[15] = s * m1._items[15];

            return m;
        }

        public static Matrix4X4 operator /(Matrix4X4 m1, double s)
        {
            Debug.Assert(!m1.HasNaNComponents && !double.IsNaN(s));

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;

            var m = new Matrix4X4();

            m._items[0] = s * m1._items[0];
            m._items[1] = s * m1._items[1];
            m._items[2] = s * m1._items[2];
            m._items[3] = s * m1._items[3];

            m._items[4] = s * m1._items[4];
            m._items[5] = s * m1._items[5];
            m._items[6] = s * m1._items[6];
            m._items[7] = s * m1._items[7];

            m._items[8] = s * m1._items[8];
            m._items[9] = s * m1._items[9];
            m._items[10] = s * m1._items[10];
            m._items[11] = s * m1._items[11];

            m._items[12] = s * m1._items[12];
            m._items[13] = s * m1._items[13];
            m._items[14] = s * m1._items[14];
            m._items[15] = s * m1._items[15];

            return m;
        }


        private readonly double[] _items = new double[16];


        public double this[int index]
        {
            get { return _items[index]; }
            set
            {
                Debug.Assert(!double.IsNaN(value));

                _items[index] = value;
            }
        }

        public double this[int i, int j]
        {
            get
            {
                Debug.Assert(i >= 0 && i <= 3 && j >= 0 && j <= 3);

                return _items[i + (j << 2)];
            }
            set
            {
                Debug.Assert(i >= 0 && i <= 3 && j >= 0 && j <= 3 && !double.IsNaN(value));

                _items[i + (j << 2)] = value;
            }
        }

        /// <summary>
        /// The squared Frobenius norm of this matrix
        /// </summary>
        public double FNormSquared
            => _items.Sum(t => t * t);

        /// <summary>
        /// The Frobenius norm of this matrix
        /// </summary>
        public double FNorm
            => Math.Sqrt(_items.Sum(t => t * t));

        public bool IsExactIdentity
        {
            get
            {
                if (_items[0] != 1) return false;
                if (_items[1] != 0) return false;
                if (_items[2] != 0) return false;
                if (_items[3] != 0) return false;

                if (_items[4] != 0) return false;
                if (_items[5] != 1) return false;
                if (_items[6] != 0) return false;
                if (_items[7] != 0) return false;

                if (_items[8] != 0) return false;
                if (_items[9] != 0) return false;
                if (_items[10] != 1) return false;
                if (_items[11] != 0) return false;

                if (_items[12] != 0) return false;
                if (_items[13] != 1) return false;
                if (_items[14] != 0) return false;
                if (_items[15] != 1) return false;

                return true;
            }
        }

        public bool IsIdentity
        {
            get
            {
                if (!(_items[0] - 1.0d).IsAlmostZero()) return false;
                if (!_items[1].IsAlmostZero()) return false;
                if (!_items[2].IsAlmostZero()) return false;
                if (!_items[3].IsAlmostZero()) return false;

                if (!_items[4].IsAlmostZero()) return false;
                if (!(_items[5] - 1.0d).IsAlmostZero()) return false;
                if (!_items[6].IsAlmostZero()) return false;
                if (!_items[7].IsAlmostZero()) return false;

                if (!_items[8].IsAlmostZero()) return false;
                if (!_items[9].IsAlmostZero()) return false;
                if (!(_items[10] - 1.0d).IsAlmostZero()) return false;
                if (!_items[11].IsAlmostZero()) return false;

                if (!_items[12].IsAlmostZero()) return false;
                if (!_items[13].IsAlmostZero()) return false;
                if (!_items[14].IsAlmostZero()) return false;
                if (!(_items[15] - 1.0d).IsAlmostZero()) return false;

                return true;
            }
        }

        public bool ContainsScaling
        {
            get
            {
                var x = _items[0] * _items[0] + _items[4] * _items[4] + _items[8] * _items[8];
                if (!(x - 1.0d).IsAlmostZero()) return false;

                x = _items[1] * _items[1] + _items[5] * _items[5] + _items[9] * _items[9];
                if (!(x - 1.0d).IsAlmostZero()) return false;

                x = _items[2] * _items[2] + _items[6] * _items[6] + _items[10] * _items[10];
                if (!(x - 1.0d).IsAlmostZero()) return false;

                return true;
            }
        }

        public bool HasNaNComponents =>
            _items.Any(double.IsNaN);

        /// <summary>
        /// http://www.euclideanspace.com/maths/algebra/matrix/functions/determinant/fourD/index.htm
        /// </summary>
        public double Determinant
        {
            get
            {
                var m0 = _items[0];
                var m1 = _items[1];
                var m2 = _items[2];
                var m3 = _items[3];
                var m4 = _items[4];
                var m5 = _items[5];
                var m6 = _items[6];
                var m7 = _items[7];

                var m8 = _items[8];
                var m9 = _items[9];
                var m10 = _items[10];
                var m11 = _items[11];

                return
                    ((m3 * m6 - m2 * m7) * m9 - (m3 * m5 + m1 * m7) * m10 + (m2 * m5 - m1 * m6) * m11) * _items[12] -
                    ((m3 * m6 + m2 * m7) * m8 + (m3 * m4 - m0 * m7) * m10 - (m2 * m4 + m0 * m6) * m11) * _items[13] +
                    ((m3 * m5 - m1 * m7) * m8 - (m3 * m4 + m0 * m7) * m9  + (m1 * m4 - m0 * m5) * m11) * _items[14] -
                    ((m2 * m5 + m1 * m6) * m8 + (m2 * m4 - m0 * m6) * m9  - (m1 * m4 + m0 * m5) * m10) * _items[15];
            }
        }

        public bool SwapsHandedness
            => Determinant < 0.0d;

        public Tuple3D UpperRightBlock3X1
        {
            get
            {
                return new Tuple3D(_items[3], _items[7], _items[11]);
            }
            set
            {
                _items[3] = value.X;
                _items[7] = value.Y;
                _items[11] = value.Z;
            }
        }

        public Matrix3X3 UpperLeftBlock3X3
        {
            get
            {
                return new Matrix3X3
                {
                    [0] = _items[0],
                    [1] = _items[1],
                    [2] = _items[2],

                    [3] = _items[4],
                    [4] = _items[5],
                    [5] = _items[6],

                    [6] = _items[8],
                    [7] = _items[9],
                    [8] = _items[10]
                };
            }
            set
            {
                _items[0] = value[0];
                _items[1] = value[1];
                _items[2] = value[2];

                _items[4] = value[3];
                _items[5] = value[4];
                _items[6] = value[5];

                _items[8] = value[6];
                _items[9] = value[7];
                _items[10] = value[8];
            }
        }


        public Matrix4X4()
        {

        }

        public Matrix4X4(Matrix4X4 m1)
        {
            _items[0] = m1._items[0];
            _items[1] = m1._items[1];
            _items[2] = m1._items[2];
            _items[3] = m1._items[3];

            _items[4] = m1._items[4];
            _items[5] = m1._items[5];
            _items[6] = m1._items[6];
            _items[7] = m1._items[7];

            _items[8] = m1._items[8];
            _items[9] = m1._items[9];
            _items[10] = m1._items[10];
            _items[11] = m1._items[11];

            _items[12] = m1._items[12];
            _items[13] = m1._items[13];
            _items[14] = m1._items[14];
            _items[15] = m1._items[15];
        }


        public Matrix4X4 ResetToIdentity()
        {
            _items[0] = 1;
            _items[1] = 0;
            _items[2] = 0;
            _items[3] = 0;

            _items[4] = 0;
            _items[5] = 1;
            _items[6] = 0;
            _items[7] = 0;

            _items[8] = 0;
            _items[9] = 0;
            _items[10] = 1;
            _items[11] = 0;

            _items[12] = 0;
            _items[13] = 0;
            _items[14] = 0;
            _items[15] = 1;

            return this;
        }

        public Matrix4X4 ResetTo(Matrix4X4 m1)
        {
            _items[0] = m1._items[0];
            _items[1] = m1._items[1];
            _items[2] = m1._items[2];
            _items[3] = m1._items[3];

            _items[4] = m1._items[4];
            _items[5] = m1._items[5];
            _items[6] = m1._items[6];
            _items[7] = m1._items[7];

            _items[8] = m1._items[8];
            _items[9] = m1._items[9];
            _items[10] = m1._items[10];
            _items[11] = m1._items[11];

            _items[12] = m1._items[12];
            _items[13] = m1._items[13];
            _items[14] = m1._items[14];
            _items[15] = m1._items[15];

            return this;
        }

        public Tuple4D GetColumn(int columnIndex)
        {
            var j = columnIndex << 2;

            return new Tuple4D(
                _items[j],
                _items[1 + j],
                _items[2 + j],
                _items[3 + j]
            );
        }

        public Matrix4X4 SetColumn(int columnIndex, Tuple4D columnVector)
        {
            var j = columnIndex << 2;

            _items[j] = columnVector.X;
            _items[1 + j] = columnVector.Y;
            _items[2 + j] = columnVector.Z;
            _items[3 + j] = columnVector.W;

            return this;
        }

        public Matrix4X4 SelfSwapRows(int j1, int j2)
        {
            Debug.Assert(j1 >= 0 && j1 <= 3 && j2 >= 0 && j2 <= 3);

            if (j1 == j2) return this;

            var s = _items[j1];
            _items[j1] = _items[j2];
            _items[j2] = s;

            var index1 = j1 + 4;
            var index2 = j2 + 4;
            s = _items[index1];
            _items[index1] = _items[index2];
            _items[index2] = s;

            index1 = index1 + 4;
            index2 = index2 + 4;
            s = _items[index1];
            _items[index1] = _items[index2];
            _items[index2] = s;

            index1 = index1 + 4;
            index2 = index2 + 4;
            s = _items[index1];
            _items[index1] = _items[index2];
            _items[index2] = s;

            return this;
        }

        public Matrix4X4 SelfSwapColumns(int i1, int i2)
        {
            Debug.Assert(i1 >= 0 && i1 <= 3 && i2 >= 0 && i2 <= 3);

            if (i1 == i2) return this;

            var index1 = i1 << 2;
            var index2 = i2 << 2;
            var s = _items[index1];
            _items[index1] = _items[index2];
            _items[index2] = s;

            index1++;
            index2++;
            s = _items[index1];
            _items[index1] = _items[index2];
            _items[index2] = s;

            index1++;
            index2++;
            s = _items[index1];
            _items[index1] = _items[index2];
            _items[index2] = s;

            index1++;
            index2++;
            s = _items[index1];
            _items[index1] = _items[index2];
            _items[index2] = s;

            return this;
        }

        public Matrix4X4 SelfTranspose()
        {
            var s = _items[1];
            _items[1] = _items[4];
            _items[4] = s;

            s = _items[2];
            _items[2] = _items[8];
            _items[8] = s;

            s = _items[3];
            _items[3] = _items[12];
            _items[12] = s;

            s = _items[6];
            _items[6] = _items[9];
            _items[9] = s;

            s = _items[7];
            _items[7] = _items[13];
            _items[13] = s;

            s = _items[11];
            _items[11] = _items[14];
            _items[14] = s;

            return this;
        }

        public Matrix4X4 Transpose()
        {
            var m = new Matrix4X4();

            m._items[0] = _items[0];
            m._items[1] = _items[4];
            m._items[2] = _items[8];
            m._items[3] = _items[12];

            m._items[4] = _items[1];
            m._items[5] = _items[5];
            m._items[6] = _items[9];
            m._items[7] = _items[13];

            m._items[8] = _items[2];
            m._items[9] = _items[6];
            m._items[10] = _items[10];
            m._items[11] = _items[14];

            m._items[12] = _items[3];
            m._items[13] = _items[7];
            m._items[14] = _items[11];
            m._items[15] = _items[15];

            return m;
        }

        public Matrix4X4 Inverse()
        {
            var indxc = new int[4];
            var indxr = new int[4];
            var ipiv = new[] { 0, 0, 0, 0 };
            var minv = new Matrix4X4(this);

            for (var i = 0; i < 4; i++)
            {
                int irow = 0, icol = 0;
                var big = 0.0d;

                // Choose pivot
                for (var j = 0; j < 4; j++)
                {
                    if (ipiv[j] == 1) continue;

                    for (var k = 0; k < 4; k++)
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
                    for (var k = 0; k < 4; ++k)
                    {
                        (minv[irow, k], minv[icol, k]) = (minv[icol, k], minv[irow, k]);
                    }
                }

                indxr[i] = irow;
                indxc[i] = icol;
                if (minv[icol, icol].IsAlmostZero())
                    throw new InvalidOperationException("Singular matrix");

                // Set minv[icol, icol] to one by scaling row _icol_ appropriately
                var pivinv = 1.0d / minv[icol, icol];

                minv[icol, icol] = 1.0d;
                for (var j = 0; j < 4; j++)
                    minv[icol, j] *= pivinv;

                // Subtract this row from others to zero out their columns
                for (var j = 0; j < 4; j++)
                {
                    if (j == icol) continue;

                    var save = minv[j, icol];

                    minv[j, icol] = 0.0d;
                    for (var k = 0; k < 4; k++)
                        minv[j, k] -= minv[icol, k] * save;
                }
            }

            // Swap columns to reflect permutation
            for (var j = 3; j >= 0; j--)
            {
                if (indxr[j] == indxc[j]) continue;

                for (var k = 0; k < 4; k++)
                {
                    (minv[k, indxr[j]], minv[k, indxc[j]]) = (minv[k, indxc[j]], minv[k, indxr[j]]);
                }
            }

            return minv;
        }

        public Matrix4X4 InverseTranspose()
        {
            return Inverse().SelfTranspose();
        }


        public Matrix4X4 ToMatrix()
        {
            return new Matrix4X4(this);
        }

        public Tuple4D MapProjectivePoint(Tuple3D point)
        {
            var x = _items[0] * point.X + _items[4] * point.Y + _items[8] * point.Z + _items[12];
            var y = _items[1] * point.X + _items[5] * point.Y + _items[9] * point.Z + _items[13];
            var z = _items[2] * point.X + _items[6] * point.Y + _items[10] * point.Z + _items[14];
            var w = _items[3] * point.X + _items[7] * point.Y + _items[11] * point.Z + _items[15];

            return new Tuple4D(x, y, z, w);
        }

        public Tuple4D MapProjectiveVector(Tuple3D vector)
        {
            var x = _items[0] * vector.X + _items[4] * vector.Y + _items[8] * vector.Z;
            var y = _items[1] * vector.X + _items[5] * vector.Y + _items[9] * vector.Z;
            var z = _items[2] * vector.X + _items[6] * vector.Y + _items[10] * vector.Z;
            var w = _items[3] * vector.X + _items[7] * vector.Y + _items[11] * vector.Z;

            return new Tuple4D(x, y, z, w);
        }

        public ITuple3D MapPoint(ITuple3D point)
        {
            var pointX = _items[0] * point.X + _items[4] * point.Y + _items[8] * point.Z + _items[12];
            var pointY = _items[1] * point.X + _items[5] * point.Y + _items[9] * point.Z + _items[13];
            var pointZ = _items[2] * point.X + _items[6] * point.Y + _items[10] * point.Z + _items[14];
            var pointW = _items[3] * point.X + _items[7] * point.Y + _items[11] * point.Z + _items[15];

            if ((pointW - 1.0d).IsAlmostZero())
                return new Tuple3D(pointX, pointY, pointZ);

            var s = 1.0d / pointW;
            return new Tuple3D(pointX * s, pointY * s, pointZ * s);
        }

        public ITuple3D MapVector(ITuple3D vector)
        {
            return new Tuple3D(
                _items[0] * vector.X + _items[4] * vector.Y + _items[8] * vector.Z,
                _items[1] * vector.X + _items[5] * vector.Y + _items[9] * vector.Z,
                _items[2] * vector.X + _items[6] * vector.Y + _items[10] * vector.Z
            );
        }

        public ITuple3D MapNormal(ITuple3D normal)
        {
            var invMatrix = Inverse();

            return new Tuple3D(
                invMatrix._items[0] * normal.X + invMatrix._items[1] * normal.Y + invMatrix._items[2] * normal.Z,
                invMatrix._items[4] * normal.X + invMatrix._items[5] * normal.Y + invMatrix._items[6] * normal.Z,
                invMatrix._items[8] * normal.X + invMatrix._items[9] * normal.Y + invMatrix._items[10] * normal.Z
            );
        }

        public IAffineMap3D InverseMap()
        {
            return Inverse();
        }
    }
}