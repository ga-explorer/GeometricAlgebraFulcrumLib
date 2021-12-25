using System;
using System.Diagnostics;
using System.Linq;

namespace NumericalGeometryLib.BasicMath.Matrices
{
    public sealed class SquareMatrix2
    {
        public static SquareMatrix2 operator -(SquareMatrix2 m1)
        {
            Debug.Assert(!m1.HasNaNComponent);

            var m = new SquareMatrix2
            {
                _items =
                {
                    [0] = -m1._items[0],
                    [1] = -m1._items[1],
                    [2] = -m1._items[2],
                    [3] = -m1._items[3]
                }
            };

            return m;
        }

        public static SquareMatrix2 operator +(SquareMatrix2 m1, SquareMatrix2 m2)
        {
            Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

            var m = new SquareMatrix2
            {
                _items =
                {
                    [0] = m1._items[0] + m2._items[0],
                    [1] = m1._items[1] + m2._items[1],
                    [2] = m1._items[2] + m2._items[2],
                    [3] = m1._items[3] + m2._items[3]
                }
            };

            return m;
        }

        public static SquareMatrix2 operator -(SquareMatrix2 m1, SquareMatrix2 m2)
        {
            Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

            var m = new SquareMatrix2
            {
                _items =
                {
                    [0] = m1._items[0] - m2._items[0],
                    [1] = m1._items[1] - m2._items[1],
                    [2] = m1._items[2] - m2._items[2],
                    [3] = m1._items[3] - m2._items[3]
                }
            };

            return m;
        }

        public static SquareMatrix2 operator *(SquareMatrix2 m1, SquareMatrix2 m2)
        {
            Debug.Assert(!m1.HasNaNComponent && !m2.HasNaNComponent);

            var m = new SquareMatrix2
            {
                _items =
                {
                    [0] = m1._items[0] * m2._items[0] +
                          m1._items[2] * m2._items[1],
                    [1] = m1._items[1] * m2._items[0] +
                          m1._items[3] * m2._items[1],
                    [2] = m1._items[0] * m2._items[2] +
                          m1._items[2] * m2._items[3],
                    [3] = m1._items[1] * m2._items[2] +
                          m1._items[3] * m2._items[3]
                }
            };


            return m;
        }

        public static SquareMatrix2 operator *(SquareMatrix2 m1, double s)
        {
            Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

            var m = new SquareMatrix2
            {
                _items =
                {
                    [0] = s * m1._items[0],
                    [1] = s * m1._items[1],
                    [2] = s * m1._items[2],
                    [3] = s * m1._items[3]
                }
            };

            return m;
        }

        public static SquareMatrix2 operator *(double s, SquareMatrix2 m1)
        {
            Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

            var m = new SquareMatrix2
            {
                _items =
                {
                    [0] = s * m1._items[0],
                    [1] = s * m1._items[1],
                    [2] = s * m1._items[2],
                    [3] = s * m1._items[3]
                }
            };

            return m;
        }

        public static SquareMatrix2 operator /(SquareMatrix2 m1, double s)
        {
            Debug.Assert(!m1.HasNaNComponent && !double.IsNaN(s));

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;

            var m = new SquareMatrix2
            {
                _items =
                {
                    [0] = s * m1._items[0],
                    [1] = s * m1._items[1],
                    [2] = s * m1._items[2],
                    [3] = s * m1._items[3]
                }
            };

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
        {
            get { return _items.Sum(t => t * t); }
        }

        /// <summary>
        /// The Frobenius norm of this matrix
        /// </summary>
        public double FNorm
        {
            get { return Math.Sqrt(_items.Sum(t => t * t)); }
        }

        public bool HasNaNComponent
        {
            get { return _items.Any(double.IsNaN); }
        }

        public double Determinant
        {
            get { return _items[0] * _items[3] - _items[2] * _items[1]; }
        }


        public SquareMatrix2()
        {

        }

        public SquareMatrix2(SquareMatrix2 m1)
        {
            _items[0] = m1._items[0];
            _items[1] = m1._items[1];

            _items[2] = m1._items[2];
            _items[3] = m1._items[3];
        }


    }
}
