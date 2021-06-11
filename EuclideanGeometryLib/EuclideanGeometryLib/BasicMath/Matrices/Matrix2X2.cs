using System;
using System.Diagnostics;
using System.Linq;

namespace EuclideanGeometryLib.BasicMath.Matrices
{
    public sealed class Matrix2X2
    {
        public static Matrix2X2 operator -(Matrix2X2 m1)
        {
            Debug.Assert(!m1.HasNaNComponent);

            var m = new Matrix2X2();

            m._items[0] = -m1._items[0];
            m._items[1] = -m1._items[1];

            m._items[2] = -m1._items[2];
            m._items[3] = -m1._items[3];

            return m;
        }

        public static Matrix2X2 operator +(Matrix2X2 m1, Matrix2X2 m2)
        {
            Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

            var m = new Matrix2X2();

            m._items[0] = m1._items[0] + m2._items[0];
            m._items[1] = m1._items[1] + m2._items[1];

            m._items[2] = m1._items[2] + m2._items[2];
            m._items[3] = m1._items[3] + m2._items[3];

            return m;
        }

        public static Matrix2X2 operator -(Matrix2X2 m1, Matrix2X2 m2)
        {
            Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

            var m = new Matrix2X2();

            m._items[0] = m1._items[0] - m2._items[0];
            m._items[1] = m1._items[1] - m2._items[1];

            m._items[2] = m1._items[2] - m2._items[2];
            m._items[3] = m1._items[3] - m2._items[3];

            return m;
        }

        public static Matrix2X2 operator *(Matrix2X2 m1, Matrix2X2 m2)
        {
            Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

            var m = new Matrix2X2();

            m._items[0] =
                m1._items[0] * m2._items[0] +
                m1._items[2] * m2._items[1];

            m._items[1] =
                m1._items[1] * m2._items[0] +
                m1._items[3] * m2._items[1];


            m._items[2] =
                m1._items[0] * m2._items[2] +
                m1._items[2] * m2._items[3];

            m._items[3] =
                m1._items[1] * m2._items[2] +
                m1._items[3] * m2._items[3];

            return m;
        }

        public static Matrix2X2 operator *(Matrix2X2 m1, double s)
        {
            Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

            var m = new Matrix2X2();

            m._items[0] = s * m1._items[0];
            m._items[1] = s * m1._items[1];

            m._items[2] = s * m1._items[2];
            m._items[3] = s * m1._items[3];

            return m;
        }

        public static Matrix2X2 operator *(double s, Matrix2X2 m1)
        {
            Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

            var m = new Matrix2X2();

            m._items[0] = s * m1._items[0];
            m._items[1] = s * m1._items[1];

            m._items[2] = s * m1._items[2];
            m._items[3] = s * m1._items[3];

            return m;
        }

        public static Matrix2X2 operator /(Matrix2X2 m1, double s)
        {
            Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;

            var m = new Matrix2X2();

            m._items[0] = s * m1._items[0];
            m._items[1] = s * m1._items[1];

            m._items[2] = s * m1._items[2];
            m._items[3] = s * m1._items[3];

            return m;
        }


        private readonly double[] _items = new double[4];


        /// <summary>
        /// Get or set an item in this matrix using column-major order indexing
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
                Debug.Assert(i >= 0 && i <= 1 && j >= 0 && j <= 1);

                return _items[i + (j << 1)];
            }
            set
            {
                Debug.Assert(i >= 0 && i <= 1 && j >= 0 && j <= 1 && !double.IsNaN(value));

                _items[i + (j << 1)] = value;
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
            => _items[0] * _items[3] - _items[2] * _items[1];


        public Matrix2X2()
        {

        }

        public Matrix2X2(Matrix2X2 m1)
        {
            _items[0] = m1._items[0];
            _items[1] = m1._items[1];

            _items[2] = m1._items[2];
            _items[3] = m1._items[3];
        }


    }
}
