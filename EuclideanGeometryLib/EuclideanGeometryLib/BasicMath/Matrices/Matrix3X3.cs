using System;
using System.Diagnostics;
using System.Linq;
using EuclideanGeometryLib.BasicMath.Maps.Space2D;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Matrices
{
    public sealed class Matrix3X3 : IAffineMap2D
    {
        public static Matrix3X3 CreateIdentityMatrix()
        {
            var m = new Matrix3X3();

            m._items[0] = 1.0d;
            m._items[4] = 1.0d;
            m._items[8] = 1.0d;

            return m;
        }

        public static Matrix3X3 CreateTranslationMatrix(double dx, double dy)
        {
            var m = new Matrix3X3();

            m._items[0] = 1.0d;
            m._items[4] = 1.0d;
            m._items[8] = 1.0d;

            m._items[6] = dx;
            m._items[7] = dy;

            return m;
        }

        public static Matrix3X3 CreateScalingMatrix(double sx, double sy)
        {
            var m = new Matrix3X3();

            m._items[0] = sx;
            m._items[4] = sy;
            m._items[8] = 1.0d;

            return m;
        }

        public static Matrix3X3 CreateScalingMatrix(double s)
        {
            var m = new Matrix3X3();

            m._items[0] = s;
            m._items[4] = s;
            m._items[8] = 1.0d;

            return m;
        }

        public static Matrix3X3 CreateXReflectionMatrix()
        {
            var m = new Matrix3X3();

            m._items[0] = 1.0d;
            m._items[4] = -1.0d;
            m._items[8] = 1.0d;

            return m;
        }

        public static Matrix3X3 CreateYReflectionMatrix()
        {
            var m = new Matrix3X3();

            m._items[0] = -1.0d;
            m._items[4] = 1.0d;
            m._items[8] = 1.0d;

            return m;
        }

        public static Matrix3X3 CreateOriginReflectionMatrix()
        {
            var m = new Matrix3X3();

            m._items[0] = -1.0d;
            m._items[4] = -1.0d;
            m._items[8] = 1.0d;

            return m;
        }

        public static Matrix3X3 CreateRotationMatrix(double radianAngle)
        {
            var m = new Matrix3X3();

            var cosAngle = Math.Cos(radianAngle);
            var sinAngle = Math.Sin(radianAngle);

            m._items[0] = cosAngle;
            m._items[1] = sinAngle;
            m._items[3] = -sinAngle;
            m._items[4] = cosAngle;
            m._items[8] = 1.0d;

            return m;
        }


        public static Matrix3X3 operator -(Matrix3X3 m1)
        {
            Debug.Assert(!m1.HasNaNComponent);

            var m = new Matrix3X3();

            m._items[0] = -m1._items[0];
            m._items[1] = -m1._items[1];
            m._items[2] = -m1._items[2];

            m._items[3] = -m1._items[3];
            m._items[4] = -m1._items[4];
            m._items[5] = -m1._items[5];

            m._items[6] = -m1._items[6];
            m._items[7] = -m1._items[7];
            m._items[8] = -m1._items[8];

            return m;
        }

        public static Matrix3X3 operator +(Matrix3X3 m1, Matrix3X3 m2)
        {
            Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

            var m = new Matrix3X3();

            m._items[0] = m1._items[0] + m2._items[0];
            m._items[1] = m1._items[1] + m2._items[1];
            m._items[2] = m1._items[2] + m2._items[2];

            m._items[3] = m1._items[3] + m2._items[3];
            m._items[4] = m1._items[4] + m2._items[4];
            m._items[5] = m1._items[5] + m2._items[5];

            m._items[6] = m1._items[6] + m2._items[6];
            m._items[7] = m1._items[7] + m2._items[7];
            m._items[8] = m1._items[8] + m2._items[8];

            return m;
        }

        public static Matrix3X3 operator -(Matrix3X3 m1, Matrix3X3 m2)
        {
            Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

            var m = new Matrix3X3();

            m._items[0] = m1._items[0] - m2._items[0];
            m._items[1] = m1._items[1] - m2._items[1];
            m._items[2] = m1._items[2] - m2._items[2];

            m._items[3] = m1._items[3] - m2._items[3];
            m._items[4] = m1._items[4] - m2._items[4];
            m._items[5] = m1._items[5] - m2._items[5];

            m._items[6] = m1._items[6] - m2._items[6];
            m._items[7] = m1._items[7] - m2._items[7];
            m._items[8] = m1._items[8] - m2._items[8];

            return m;
        }

        public static Matrix3X3 operator *(Matrix3X3 m1, Matrix3X3 m2)
        {
            Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

            var m = new Matrix3X3();

            m._items[0] =
                m1._items[0] * m2._items[0] +
                m1._items[1] * m2._items[3] +
                m1._items[2] * m2._items[6];

            m._items[1] =
                m1._items[0] * m2._items[1] +
                m1._items[1] * m2._items[4] +
                m1._items[2] * m2._items[7];

            m._items[2] =
                m1._items[0] * m2._items[2] +
                m1._items[1] * m2._items[5] +
                m1._items[2] * m2._items[8];


            m._items[3] =
                m1._items[3] * m2._items[0] +
                m1._items[4] * m2._items[3] +
                m1._items[5] * m2._items[6];

            m._items[4] =
                m1._items[3] * m2._items[1] +
                m1._items[4] * m2._items[4] +
                m1._items[5] * m2._items[7];

            m._items[5] =
                m1._items[3] * m2._items[2] +
                m1._items[4] * m2._items[5] +
                m1._items[5] * m2._items[8];


            m._items[6] =
                m1._items[6] * m2._items[0] +
                m1._items[7] * m2._items[3] +
                m1._items[8] * m2._items[6];

            m._items[7] =
                m1._items[6] * m2._items[1] +
                m1._items[7] * m2._items[4] +
                m1._items[8] * m2._items[7];

            m._items[8] =
                m1._items[6] * m2._items[2] +
                m1._items[7] * m2._items[5] +
                m1._items[8] * m2._items[8];

            return m;
        }

        public static Matrix3X3 operator *(Matrix3X3 m1, double s)
        {
            Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

            var m = new Matrix3X3();

            m._items[0] = s * m1._items[0];
            m._items[1] = s * m1._items[1];
            m._items[2] = s * m1._items[2];

            m._items[3] = s * m1._items[3];
            m._items[4] = s * m1._items[4];
            m._items[5] = s * m1._items[5];

            m._items[6] = s * m1._items[6];
            m._items[7] = s * m1._items[7];
            m._items[8] = s * m1._items[8];

            return m;
        }

        public static Matrix3X3 operator *(double s, Matrix3X3 m1)
        {
            Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

            var m = new Matrix3X3();

            m._items[0] = s * m1._items[0];
            m._items[1] = s * m1._items[1];
            m._items[2] = s * m1._items[2];

            m._items[3] = s * m1._items[3];
            m._items[4] = s * m1._items[4];
            m._items[5] = s * m1._items[5];

            m._items[6] = s * m1._items[6];
            m._items[7] = s * m1._items[7];
            m._items[8] = s * m1._items[8];

            return m;
        }

        public static Matrix3X3 operator /(Matrix3X3 m1, double s)
        {
            Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;

            var m = new Matrix3X3();

            m._items[0] = s * m1._items[0];
            m._items[1] = s * m1._items[1];
            m._items[2] = s * m1._items[2];

            m._items[3] = s * m1._items[3];
            m._items[4] = s * m1._items[4];
            m._items[5] = s * m1._items[5];

            m._items[6] = s * m1._items[6];
            m._items[7] = s * m1._items[7];
            m._items[8] = s * m1._items[8];

            return m;
        }


        private readonly double[] _items = new double[9];

        /// <summary>
        /// Get or set an item in this matrix using column-major indexing
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
                Debug.Assert(i >= 0 && i <= 2 && j >= 0 && j <= 2);

                return _items[i + j * 3];
            }
            set
            {
                Debug.Assert(i >= 0 && i <= 2 && j >= 0 && j <= 2 && !double.IsNaN(value));

                _items[i + j * 3] = value;
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

        public bool HasNaNComponent =>
            _items.Any(double.IsNaN);

        public double Determinant
            => _items[0] * (_items[4] * _items[8] - _items[7] * _items[5]) +
               _items[3] * (_items[7] * _items[2] - _items[1] * _items[8]) +
               _items[6] * (_items[1] * _items[5] - _items[4] * _items[2]);


        public Matrix3X3()
        {

        }

        public Matrix3X3(Matrix3X3 m1)
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
        }


        public Matrix3X3 NearestOrthogonalMatrix()
        {
            var r1 = new Matrix3X3(this);
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

        public Tuple4D ToQuaternion()
        {
            var trace = this[0, 0] + this[1, 1] + this[2, 2];
            if (trace > 0.0d)
            {
                // Compute w from matrix trace, then xyz
                // 4w^2 = m[0, 0] + m[1, 1] + m[2, 2] + m[3, 3] (but m[3, 3] == 1)
                var s = Math.Sqrt(trace + 1.0d);
                var w = 0.5d * s;
                s = 0.5d / s;

                return new Tuple4D(
                    (this[2, 1] - this[1, 2]) * s,
                    (this[0, 2] - this[2, 0]) * s,
                    (this[1, 0] - this[0, 1]) * s,
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
                if (this[1, 1] > this[0, 0]) i = 1;
                if (this[2, 2] > this[i, i]) i = 2;
                var j = nxt[i]; //j = (i + 1) % 3
                var k = nxt[j]; //k = (j + 1) % 3
                var s = Math.Sqrt(this[i, i] - (this[j, j] + this[k, k]) + 1.0d);
                q[i] = s * 0.5d;
                if (!s.IsAlmostZero()) s = 0.5d / s;
                var w = (this[k, j] - this[j, k]) * s;
                q[j] = (this[j, i] + this[i, j]) * s;
                q[k] = (this[k, i] + this[i, k]) * s;

                return new Tuple4D(q[0], q[1], q[2], w);
            }
        }

        public Matrix3X3 Inverse()
        {
            var indxc = new int[3];
            var indxr = new int[3];
            var ipiv = new[] { 0, 0, 0 };
            var minv = new Matrix3X3(this);

            for (var i = 0; i < 3; i++)
            {
                int irow = 0, icol = 0;
                var big = 0.0d;

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
                var pivinv = 1.0d / minv[icol, icol];

                minv[icol, icol] = 1.0d;
                for (var j = 0; j < 3; j++)
                    minv[icol, j] *= pivinv;

                // Subtract this row from others to zero out their columns
                for (var j = 0; j < 3; j++)
                {
                    if (j == icol) continue;

                    var save = minv[j, icol];

                    minv[j, icol] = 0.0d;
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

        public Matrix3X3 SelfTranspose()
        {
            var s = _items[1];
            _items[1] = _items[3];
            _items[3] = s;

            s = _items[2];
            _items[2] = _items[6];
            _items[6] = s;

            s = _items[5];
            _items[5] = _items[7];
            _items[7] = s;

            return this;
        }

        public Matrix3X3 Transpose()
        {
            var m = new Matrix3X3();

            m._items[0] = _items[0];
            m._items[1] = _items[3];
            m._items[2] = _items[6];

            m._items[3] = _items[1];
            m._items[4] = _items[4];
            m._items[5] = _items[7];

            m._items[6] = _items[2];
            m._items[7] = _items[5];
            m._items[8] = _items[8];

            return m;
        }

        public Matrix3X3 InverseTranspose()
        {
            return Inverse().SelfTranspose();
        }


        public Matrix3X3 ToMatrix()
        {
            return new Matrix3X3(this);
        }

        public ITuple2D MapPoint(ITuple2D point)
        {
            return new Tuple2D(
                _items[0] * point.X + _items[3] * point.Y + _items[6],
                _items[1] * point.X + _items[4] * point.Y + _items[7]
            ); 
        }

        public ITuple2D MapVector(ITuple2D vector)
        {
            return new Tuple2D(
                _items[0] * vector.X + _items[3] * vector.Y,
                _items[1] * vector.X + _items[4] * vector.Y
            );
        }

        public ITuple2D MapNormal(ITuple2D normal)
        {
            throw new NotImplementedException();
        }

        public IAffineMap2D InverseMap()
        {
            throw new NotImplementedException();
        }
    }
}