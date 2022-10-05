using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace NumericalGeometryLib.BasicMath.Tuples.Mutable
{
    public sealed class SparseTuple : 
        IReadOnlyList<double>
    {
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator Tuple2D(SparseTuple tuple)
        //{
        //    return new Tuple2D(
        //        tuple[0],
        //        tuple[1]
        //    );
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator Tuple3D(SparseTuple tuple)
        //{
        //    return new Tuple3D(
        //        tuple[0],
        //        tuple[1],
        //        tuple[2]
        //    );
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator Tuple4D(SparseTuple tuple)
        //{
        //    return new Tuple4D(
        //        tuple[0],
        //        tuple[1],
        //        tuple[2],
        //        tuple[3]
        //    );
        //}
        
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator SparseTuple(Tuple2D tuple)
        //{
        //    return new SparseTuple(
        //        tuple.Item1, 
        //        tuple.Item2
        //    );
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator SparseTuple(Tuple3D tuple)
        //{
        //    return new SparseTuple(
        //        tuple.Item1, 
        //        tuple.Item2, 
        //        tuple.Item3
        //    );
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator SparseTuple(Tuple4D tuple)
        //{
        //    return new SparseTuple(
        //        tuple.Item1, 
        //        tuple.Item2, 
        //        tuple.Item3, 
        //        tuple.Item4
        //    );
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(SparseTuple tuple1)
        {
            return new SparseTuple(
                tuple1
                    ._itemDictionary
                    .Select(p => 
                        new KeyValuePair<int, double>(p.Key, -p.Value)
                    )
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator +(SparseTuple tuple1, IPair<double> tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            tuple[0] += tuple2.Item1;
            tuple[1] += tuple2.Item2;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator +(IPair<double> tuple1, SparseTuple tuple2)
        {
            var tuple = new SparseTuple(tuple2);

            tuple[0] += tuple1.Item1;
            tuple[1] += tuple1.Item2;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator +(SparseTuple tuple1, ITriplet<double> tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            tuple[0] += tuple2.Item1;
            tuple[1] += tuple2.Item2;
            tuple[2] += tuple2.Item3;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator +(ITriplet<double> tuple1, SparseTuple tuple2)
        {
            var tuple = new SparseTuple(tuple2);

            tuple[0] += tuple1.Item1;
            tuple[1] += tuple1.Item2;
            tuple[2] += tuple1.Item3;

            return tuple;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator +(SparseTuple tuple1, IQuad<double> tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            tuple[0] += tuple2.Item1;
            tuple[1] += tuple2.Item2;
            tuple[2] += tuple2.Item3;
            tuple[3] += tuple2.Item4;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator +(IQuad<double> tuple1, SparseTuple tuple2)
        {
            var tuple = new SparseTuple(tuple2);

            tuple[0] += tuple1.Item1;
            tuple[1] += tuple1.Item2;
            tuple[2] += tuple1.Item3;
            tuple[3] += tuple1.Item4;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator +(SparseTuple tuple1, IQuint<double> tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            tuple[0] += tuple2.Item1;
            tuple[1] += tuple2.Item2;
            tuple[2] += tuple2.Item3;
            tuple[3] += tuple2.Item4;
            tuple[4] += tuple2.Item5;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator +(IQuint<double> tuple1, SparseTuple tuple2)
        {
            var tuple = new SparseTuple(tuple2);

            tuple[0] += tuple1.Item1;
            tuple[1] += tuple1.Item2;
            tuple[2] += tuple1.Item3;
            tuple[3] += tuple1.Item4;
            tuple[4] += tuple1.Item5;

            return tuple;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator +(SparseTuple tuple1, IHexad<double> tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            tuple[0] += tuple2.Item1;
            tuple[1] += tuple2.Item2;
            tuple[2] += tuple2.Item3;
            tuple[3] += tuple2.Item4;
            tuple[4] += tuple2.Item5;
            tuple[5] += tuple2.Item6;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator +(IHexad<double> tuple1, SparseTuple tuple2)
        {
            var tuple = new SparseTuple(tuple2);

            tuple[0] += tuple1.Item1;
            tuple[1] += tuple1.Item2;
            tuple[2] += tuple1.Item3;
            tuple[3] += tuple1.Item4;
            tuple[4] += tuple1.Item5;
            tuple[5] += tuple1.Item6;

            return tuple;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator +(SparseTuple tuple1, SparseTuple tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            return tuple.InPlaceAdd(tuple2);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(SparseTuple tuple1, IPair<double> tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            tuple[0] -= tuple2.Item1;
            tuple[1] -= tuple2.Item2;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(IPair<double> tuple1, SparseTuple tuple2)
        {
            var tuple = -tuple2;

            tuple[0] += tuple1.Item1;
            tuple[1] += tuple1.Item2;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(SparseTuple tuple1, ITriplet<double> tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            tuple[0] -= tuple2.Item1;
            tuple[1] -= tuple2.Item2;
            tuple[2] -= tuple2.Item3;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(ITriplet<double> tuple1, SparseTuple tuple2)
        {
            var tuple = -tuple2;

            tuple[0] += tuple1.Item1;
            tuple[1] += tuple1.Item2;
            tuple[2] += tuple1.Item3;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(SparseTuple tuple1, IQuad<double> tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            tuple[0] -= tuple2.Item1;
            tuple[1] -= tuple2.Item2;
            tuple[2] -= tuple2.Item3;
            tuple[3] -= tuple2.Item4;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(IQuad<double> tuple1, SparseTuple tuple2)
        {
            var tuple = -tuple2;

            tuple[0] += tuple1.Item1;
            tuple[1] += tuple1.Item2;
            tuple[2] += tuple1.Item3;
            tuple[3] += tuple1.Item4;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(SparseTuple tuple1, IQuint<double> tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            tuple[0] -= tuple2.Item1;
            tuple[1] -= tuple2.Item2;
            tuple[2] -= tuple2.Item3;
            tuple[3] -= tuple2.Item4;
            tuple[4] -= tuple2.Item5;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(IQuint<double> tuple1, SparseTuple tuple2)
        {
            var tuple = -tuple2;

            tuple[0] += tuple1.Item1;
            tuple[1] += tuple1.Item2;
            tuple[2] += tuple1.Item3;
            tuple[3] += tuple1.Item4;
            tuple[4] += tuple1.Item5;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(SparseTuple tuple1, IHexad<double> tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            tuple[0] -= tuple2.Item1;
            tuple[1] -= tuple2.Item2;
            tuple[2] -= tuple2.Item3;
            tuple[3] -= tuple2.Item4;
            tuple[4] -= tuple2.Item5;
            tuple[5] -= tuple2.Item6;

            return tuple;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(IHexad<double> tuple1, SparseTuple tuple2)
        {
            var tuple = -tuple2;

            tuple[0] += tuple1.Item1;
            tuple[1] += tuple1.Item2;
            tuple[2] += tuple1.Item3;
            tuple[3] += tuple1.Item4;
            tuple[4] += tuple1.Item5;
            tuple[5] += tuple1.Item6;

            return tuple;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator -(SparseTuple tuple1, SparseTuple tuple2)
        {
            var tuple = new SparseTuple(tuple1);

            return tuple.InPlaceSubtract(tuple2);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator *(SparseTuple tuple1, double scalar)
        {
            var tuple = new SparseTuple(tuple1);

            return tuple.InPlaceScale(scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator *(double scalar, SparseTuple tuple1)
        {
            var tuple = new SparseTuple(tuple1);

            return tuple.InPlaceScale(scalar);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTuple operator /(SparseTuple tuple1, double scalar)
        {
            var tuple = new SparseTuple(tuple1);

            return tuple.InPlaceScale(1d / scalar);
        }


        private readonly SortedDictionary<int, double> _itemDictionary
            = new SortedDictionary<int, double>();


        public int Count 
            => _itemDictionary.Keys.LastOrDefault();

        public double this[int index]
        {
            get
            {
                if (index < 0)
                    throw new IndexOutOfRangeException(nameof(index));

                return _itemDictionary.TryGetValue(index, out var item) 
                    ? item : 0d;
            }
            set
            {
                if (index < 0)
                    throw new IndexOutOfRangeException(nameof(index));

                Debug.Assert(value.IsNotNaN());

                if (value == 0d)
                    _itemDictionary.Remove(index);

                else if (_itemDictionary.ContainsKey(index))
                    _itemDictionary[index] = value;

                else
                    _itemDictionary.Add(index, value);
            }
        }

        public double X
        {
            get => this[0];
            set => this[0] = value;
        }
        
        public double Y
        {
            get => this[1];
            set => this[1] = value;
        }
        
        public double Z
        {
            get => this[2];
            set => this[2] = value;
        }
        
        public double W
        {
            get => this[3];
            set => this[3] = value;
        }

        public int SparseCount 
            => _itemDictionary.Count;

        public IEnumerable<int> Keys 
            => _itemDictionary.Keys;
        
        public IEnumerable<double> Values 
            => _itemDictionary.Values;

        public IEnumerable<KeyValuePair<int, double>> IndexItemPairs
            => _itemDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple()
        {
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(double item0)
        {
            Debug.Assert(item0.IsNotNaN());

            _itemDictionary.Add(0, item0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(double item0, double item1)
        {
            Debug.Assert(
                item0.IsNotNaN() &&
                item1.IsNotNaN()
            );

            _itemDictionary.Add(0, item0);
            _itemDictionary.Add(1, item1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(double item0, double item1, double item2)
        {
            Debug.Assert(
                item0.IsNotNaN() &&
                item1.IsNotNaN() &&
                item2.IsNotNaN()
            );

            _itemDictionary.Add(0, item0);
            _itemDictionary.Add(1, item1);
            _itemDictionary.Add(2, item2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(double item0, double item1, double item2, double item3)
        {
            Debug.Assert(
                item0.IsNotNaN() &&
                item1.IsNotNaN() &&
                item2.IsNotNaN() &&
                item3.IsNotNaN()
            );

            _itemDictionary.Add(0, item0);
            _itemDictionary.Add(1, item1);
            _itemDictionary.Add(2, item2);
            _itemDictionary.Add(3, item2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(IEnumerable<KeyValuePair<int, double>> indexItemList)
        {
            foreach (var (index, item) in indexItemList)
            {
                Debug.Assert(item.IsNotNaN());

                _itemDictionary.Add(index, item);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(SparseTuple tuple)
        {
            foreach (var (index, item) in tuple._itemDictionary)
                _itemDictionary.Add(index, item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(params double[] itemArray)
        {
            for (var index = 0; index < itemArray.Length; index++)
            {
                Debug.Assert(itemArray[index].IsNotNaN());

                _itemDictionary.Add(index, itemArray[index]);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(IEnumerable<double> itemList)
        {
            var index = 0;
            foreach (var item in itemList)
            {
                Debug.Assert(item.IsNotNaN());

                _itemDictionary.Add(index, item);

                index++;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(IPair<double> tuple)
        {
            Debug.Assert(
                tuple.Item1.IsNotNaN() &&
                tuple.Item2.IsNotNaN()
            );

            _itemDictionary.Add(0, tuple.Item1);
            _itemDictionary.Add(1, tuple.Item2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(ITriplet<double> tuple)
        {
            Debug.Assert(
                tuple.Item1.IsNotNaN() &&
                tuple.Item2.IsNotNaN() &&
                tuple.Item3.IsNotNaN()
            );

            _itemDictionary.Add(0, tuple.Item1);
            _itemDictionary.Add(1, tuple.Item2);
            _itemDictionary.Add(2, tuple.Item3);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(IQuad<double> tuple)
        {
            Debug.Assert(
                tuple.Item1.IsNotNaN() &&
                tuple.Item2.IsNotNaN() &&
                tuple.Item3.IsNotNaN() &&
                tuple.Item4.IsNotNaN()
            );

            _itemDictionary.Add(0, tuple.Item1);
            _itemDictionary.Add(1, tuple.Item2);
            _itemDictionary.Add(2, tuple.Item3);
            _itemDictionary.Add(3, tuple.Item4);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(IQuint<double> tuple)
        {
            Debug.Assert(
                tuple.Item1.IsNotNaN() &&
                tuple.Item2.IsNotNaN() &&
                tuple.Item3.IsNotNaN() &&
                tuple.Item4.IsNotNaN() &&
                tuple.Item5.IsNotNaN()
            );

            _itemDictionary.Add(0, tuple.Item1);
            _itemDictionary.Add(1, tuple.Item2);
            _itemDictionary.Add(2, tuple.Item3);
            _itemDictionary.Add(3, tuple.Item4);
            _itemDictionary.Add(4, tuple.Item5);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(IHexad<double> tuple)
        {
            Debug.Assert(
                tuple.Item1.IsNotNaN() &&
                tuple.Item2.IsNotNaN() &&
                tuple.Item3.IsNotNaN() &&
                tuple.Item4.IsNotNaN() &&
                tuple.Item5.IsNotNaN() &&
                tuple.Item6.IsNotNaN()
            );

            _itemDictionary.Add(0, tuple.Item1);
            _itemDictionary.Add(1, tuple.Item2);
            _itemDictionary.Add(2, tuple.Item3);
            _itemDictionary.Add(3, tuple.Item4);
            _itemDictionary.Add(4, tuple.Item5);
            _itemDictionary.Add(5, tuple.Item6);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple(Color color)
        {
            var tuple = color.ToPixel<Rgba32>();
            
            _itemDictionary.Add(0, tuple.R / 255d);
            _itemDictionary.Add(1, tuple.G / 255d);
            _itemDictionary.Add(2, tuple.B / 255d);
            _itemDictionary.Add(3, tuple.A / 255d);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return _itemDictionary.Count == 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1e-12)
        {
            return GetLength().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple Clear()
        {
            _itemDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple2D ToTuple2D()
        {
            return new Tuple2D(
                this[0], 
                this[1]
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D ToTuple3D()
        {
            return new Tuple3D(
                this[0], 
                this[1], 
                this[2]
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple4D ToTuple4D()
        {
            return new Tuple4D(
                this[0], 
                this[1], 
                this[2], 
                this[4]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color ToColorRgba(SparseTuple tuple)
        {
            Debug.Assert(
                tuple[0] >= 0d && tuple[0] <= 1d &&
                tuple[1] >= 0d && tuple[1] <= 1d &&
                tuple[2] >= 0d && tuple[2] <= 1d &&
                tuple[3] >= 0d && tuple[3] <= 1d
            );

            return Color.FromRgba(
                (byte) (tuple[0] * 255),
                (byte) (tuple[1] * 255),
                (byte) (tuple[2] * 255),
                (byte) (tuple[3] * 255)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color ToColorRgb(SparseTuple tuple)
        {
            Debug.Assert(
                tuple[0] >= 0d && tuple[0] <= 1d &&
                tuple[1] >= 0d && tuple[1] <= 1d &&
                tuple[2] >= 0d && tuple[2] <= 1d
            );

            return Color.FromRgb(
                (byte) (tuple[0] * 255),
                (byte) (tuple[1] * 255),
                (byte) (tuple[2] * 255)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple InPlaceAdd(SparseTuple tuple)
        {
            foreach (var (index, item) in tuple._itemDictionary)
                this[index] += item;

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple InPlaceSubtract(SparseTuple tuple)
        {
            foreach (var (index, item) in tuple._itemDictionary)
                this[index] -= item;

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple InPlaceNegative(double scalar)
        {
            foreach (var index in _itemDictionary.Keys)
                _itemDictionary[index] *= -1d;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple InPlaceScale(double scalar)
        {
            Debug.Assert(scalar.IsNotNaN());

            foreach (var index in _itemDictionary.Keys)
                _itemDictionary[index] *= scalar;

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetLengthSquared()
        {
            return _itemDictionary
                .Values
                .Aggregate(
                    0d, 
                    (length, item) => length + item * item
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetLength()
        {
            return GetLengthSquared().Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDistanceSquaredToPoint(SparseTuple point)
        {
            return (this - point).GetLengthSquared();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDistanceToPoint(SparseTuple point)
        {
            return (this - point).GetLength();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseTuple ToUnitVector()
        {
            return IsZero() ? new SparseTuple(this) : this / GetLength();
        }


        public IEnumerator<double> GetEnumerator()
        {
            var i = 0;
            foreach (var (index, item) in _itemDictionary)
            {
                while (i < index)
                {
                    yield return 0;
                    i++;
                }

                yield return item;
                i++;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
