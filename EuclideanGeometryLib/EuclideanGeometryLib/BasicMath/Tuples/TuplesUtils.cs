using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath.Tuples.Collections;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicMath.Tuples.Mutable;

namespace EuclideanGeometryLib.BasicMath.Tuples
{
    public static class TuplesUtils
    {
        public static bool IsFinite(this ITuple2D tuple)
            => !(
                double.IsNaN(tuple.X) || double.IsInfinity(tuple.X) ||
                double.IsNaN(tuple.Y) || double.IsInfinity(tuple.Y)
            );

        public static bool IsFinite(this ITuple3D tuple)
            => !(
                double.IsNaN(tuple.X) || double.IsInfinity(tuple.X) ||
                double.IsNaN(tuple.Y) || double.IsInfinity(tuple.Y) ||
                double.IsNaN(tuple.Z) || double.IsInfinity(tuple.Z)
            );

        public static bool IsFinite(this ITuple4D tuple)
            => !(
                double.IsNaN(tuple.X) || double.IsInfinity(tuple.X) ||
                double.IsNaN(tuple.Y) || double.IsInfinity(tuple.Y) ||
                double.IsNaN(tuple.Z) || double.IsInfinity(tuple.Z) ||
                double.IsNaN(tuple.W) || double.IsInfinity(tuple.W)
            );

        /// <summary>
        /// The value of the smallest component in this tuple
        /// </summary>
        public static double GetMinTupleComponent(this ITuple2D tuple)
            => (tuple.X < tuple.Y) ? tuple.X : tuple.Y;

        /// <summary>
        /// The value of the largest component in this tuple
        /// </summary>
        public static double GetMaxTupleComponent(this ITuple2D tuple)
            => (tuple.X > tuple.Y) ? tuple.X : tuple.Y;

        /// <summary>
        /// The index of the smallest component of this tuple
        /// </summary>
        public static int GetMinTupleComponentIndex(this ITuple2D tuple)
            => (tuple.X < tuple.Y) ? 0 : 1;

        /// <summary>
        /// The index of the largest component of this tuple
        /// </summary>
        public static int GetMaxTupleComponentIndex(this ITuple2D tuple)
            => (tuple.X > tuple.Y) ? 0 : 1;

        /// <summary>
        /// The index of the largest absolute component in this tuple
        /// </summary>
        public static int GetMaxAbsTupleComponentIndex(this ITuple2D tuple)
            => Math.Abs(tuple.X) > Math.Abs(tuple.Y) ? 0 : 1;

        /// <summary>
        /// The value of the smallest component in this tuple
        /// </summary>
        public static double GetMinComponent(this ITuple3D tuple)
            => (tuple.X < tuple.Y) ? (tuple.X < tuple.Z ? tuple.X : tuple.Z) : (tuple.Y < tuple.Z ? tuple.Y : tuple.Z);

        /// <summary>
        /// The value of the largest component in this tuple
        /// </summary>
        public static double GetMaxComponent(this ITuple3D tuple)
            => (tuple.X > tuple.Y) ? (tuple.X > tuple.Z ? tuple.X : tuple.Z) : (tuple.Y > tuple.Z ? tuple.Y : tuple.Z);

        /// <summary>
        /// The index of the smallest component in this tuple
        /// </summary>
        public static int GetMinComponentIndex(this ITuple3D tuple)
            => (tuple.X < tuple.Y) ? (tuple.X < tuple.Z ? 0 : 2) : (tuple.Y < tuple.Z ? 1 : 2);

        /// <summary>
        /// The index of the largest component in this tuple
        /// </summary>
        public static int GetMaxComponentIndex(this ITuple3D tuple)
            => (tuple.X > tuple.Y) ? (tuple.X > tuple.Z ? 0 : 2) : (tuple.Y > tuple.Z ? 1 : 2);

        /// <summary>
        /// The index of the largest absolute component in this tuple
        /// </summary>
        public static int GetMaxAbsComponentIndex(this ITuple3D tuple)
        {
            var absX = Math.Abs(tuple.X);
            var absY = Math.Abs(tuple.Y);
            var absZ = Math.Abs(tuple.Z);

            if (absX > absY)
                return absX > absZ ? 0 : 2;

            return absY > absZ ? 1 : 2;
        }

        /// <summary>
        /// Returns a new tuple containing component-wise minimum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Tuple2D Min(this ITuple2D v1, ITuple2D v2) 
            => new Tuple2D(
                v1.X < v2.X ? v1.X : v2.X,
                v1.Y < v2.Y ? v1.Y : v2.Y
            );

        /// <summary>
        /// Returns a new tuple containing component-wise minimum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static IntTuple2D Min(this IntTuple2D v1, IntTuple2D v2)
            => new IntTuple2D(
                v1.X < v2.X ? v1.X : v2.X,
                v1.Y < v2.Y ? v1.Y : v2.Y
            );

        /// <summary>
        /// Returns a new tuple containing component-wise minimum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Tuple3D Min(this ITuple3D v1, ITuple3D v2) 
            => new Tuple3D(
                v1.X < v2.X ? v1.X : v2.X,
                v1.Y < v2.Y ? v1.Y : v2.Y,
                v1.Z < v2.Z ? v1.Z : v2.Z
            );

        /// <summary>
        /// Returns a new tuple containing component-wise minimum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static IntTuple3D Min(this IntTuple3D v1, IntTuple3D v2) 
            => new IntTuple3D(
                v1.ItemX < v2.ItemX ? v1.ItemX : v2.ItemX,
                v1.ItemY < v2.ItemY ? v1.ItemY : v2.ItemY,
                v1.ItemZ < v2.ItemZ ? v1.ItemZ : v2.ItemZ
            );

        /// <summary>
        /// Returns a new tuple containing component-wise minimum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Tuple4D Min(this ITuple4D v1, ITuple4D v2) 
            => new Tuple4D(
                v1.X < v2.X ? v1.X : v2.X,
                v1.Y < v2.Y ? v1.Y : v2.Y,
                v1.Z < v2.Z ? v1.Z : v2.Z,
                v1.W < v2.W ? v1.W : v2.W
            );


        /// <summary>
        /// Returns a new tuple containing component-wise maximum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Tuple2D Max(this ITuple2D v1, ITuple2D v2) 
            => new Tuple2D(
                v1.X > v2.X ? v1.X : v2.X,
                v1.Y > v2.Y ? v1.Y : v2.Y
            );

        /// <summary>
        /// Returns a new tuple containing component-wise maximum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static IntTuple2D Max(this IntTuple2D v1, IntTuple2D v2) 
            => new IntTuple2D(
                v1.X > v2.X ? v1.X : v2.X,
                v1.Y > v2.Y ? v1.Y : v2.Y
            );

        /// <summary>
        /// Returns a new tuple containing component-wise maximum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Tuple3D Max(this ITuple3D v1, ITuple3D v2) 
            => new Tuple3D(
                v1.X > v2.X ? v1.X : v2.X,
                v1.Y > v2.Y ? v1.Y : v2.Y,
                v1.Z > v2.Z ? v1.Z : v2.Z
            );

        /// <summary>
        /// Returns a new tuple containing component-wise maximum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static IntTuple3D Max(this IntTuple3D v1, IntTuple3D v2) 
            => new IntTuple3D(
                v1.ItemX > v2.ItemX ? v1.ItemX : v2.ItemX,
                v1.ItemY > v2.ItemY ? v1.ItemY : v2.ItemY,
                v1.ItemZ > v2.ItemZ ? v1.ItemZ : v2.ItemZ
            );

        /// <summary>
        /// Returns a new tuple containing component-wise maximum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Tuple4D Max(this ITuple4D v1, ITuple4D v2) 
            => new Tuple4D(
                v1.X > v2.X ? v1.X : v2.X,
                v1.Y > v2.Y ? v1.Y : v2.Y,
                v1.Z > v2.Z ? v1.Z : v2.Z,
                v1.W > v2.W ? v1.W : v2.W
            );


        /// <summary>
        /// Returns a new tuple containing component-wise ceiling values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static Tuple2D Ceiling(this ITuple2D tuple) 
            => new Tuple2D(
                Math.Ceiling(tuple.X),
                Math.Ceiling(tuple.Y)
            );

        /// <summary>
        /// Returns a new tuple containing component-wise ceiling values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static Tuple3D Ceiling(this ITuple3D tuple) 
            => new Tuple3D(
                Math.Ceiling(tuple.X),
                Math.Ceiling(tuple.Y),
                Math.Ceiling(tuple.Z)
            );

        /// <summary>
        /// Returns a new tuple containing component-wise ceiling values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static Tuple4D Ceiling(this ITuple4D tuple) 
            => new Tuple4D(
                Math.Ceiling(tuple.X),
                Math.Ceiling(tuple.Y),
                Math.Ceiling(tuple.Z),
                Math.Ceiling(tuple.W)
            );


        /// <summary>
        /// Returns a new tuple containing component-wise floor values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static Tuple2D Floor(this ITuple2D tuple) 
            => new Tuple2D(
                Math.Floor(tuple.X),
                Math.Floor(tuple.Y)
            );

        /// <summary>
        /// Returns a new tuple containing component-wise floor values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static Tuple3D Floor(this ITuple3D tuple) 
            => new Tuple3D(
                Math.Floor(tuple.X),
                Math.Floor(tuple.Y),
                Math.Floor(tuple.Z)
            );

        /// <summary>
        /// Returns a new tuple containing component-wise floor values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static Tuple4D Floor(this ITuple4D tuple) 
            => new Tuple4D(
                Math.Floor(tuple.X),
                Math.Floor(tuple.Y),
                Math.Floor(tuple.Z),
                Math.Floor(tuple.W)
            );


        /// <summary>
        /// Returns a new tuple containing component-wise absolute values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static Tuple2D Abs(this ITuple2D tuple)
            => new Tuple2D(
                Math.Abs(tuple.X),
                Math.Abs(tuple.Y)
            );

        /// <summary>
        /// Returns a new tuple containing component-wise absolute values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static IntTuple2D Abs(this IntTuple2D tuple)
            => new IntTuple2D(
                Math.Abs(tuple.X),
                Math.Abs(tuple.Y)
            );

        /// <summary>
        /// Returns a new tuple containing component-wise absolute values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static Tuple3D Abs(this ITuple3D tuple) 
            => new Tuple3D(
                Math.Abs(tuple.X),
                Math.Abs(tuple.Y),
                Math.Abs(tuple.Z)
            );

        /// <summary>
        /// Returns a new tuple containing component-wise absolute values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static IntTuple3D Abs(this IntTuple3D tuple) 
            => new IntTuple3D(
                Math.Abs(tuple.ItemX),
                Math.Abs(tuple.ItemY),
                Math.Abs(tuple.ItemZ)
            );

        /// <summary>
        /// Returns a new tuple containing component-wise absolute values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static Tuple4D Abs(this ITuple4D tuple) 
            => new Tuple4D(
                Math.Abs(tuple.X),
                Math.Abs(tuple.Y),
                Math.Abs(tuple.Z),
                Math.Abs(tuple.W)
            );


        public static MutableTuple2D ToMutableTuple2D(this ITuple2D tuple)
            => new MutableTuple2D(tuple.X, tuple.Y);

        public static MutableTuple3D ToMutableTuple3D(this ITuple3D tuple)
            => new MutableTuple3D(tuple.X, tuple.Y, tuple.Z);

        public static Tuple2D ToTuple2D(this ITuple2D tuple) 
            => new Tuple2D(tuple.X, tuple.Y);

        public static Tuple2D ToTuple2D(this IntTuple2D tuple) 
            => new Tuple2D(tuple.X, tuple.Y);

        public static Tuple2D XyToTuple2D(this ITuple3D tuple) 
            => new Tuple2D(tuple.X, tuple.Y);

        public static Tuple2D YxToTuple2D(this ITuple3D tuple) 
            => new Tuple2D(tuple.Y, tuple.X);

        public static Tuple2D XzToTuple2D(this ITuple3D tuple) 
            => new Tuple2D(tuple.X, tuple.Z);

        public static Tuple2D ZxToTuple2D(this ITuple3D tuple) 
            => new Tuple2D(tuple.Z, tuple.X);

        public static Tuple2D YzToTuple2D(this ITuple3D tuple) 
            => new Tuple2D(tuple.Y, tuple.Z);

        public static Tuple2D ZyToTuple2D(this ITuple3D tuple) 
            => new Tuple2D(tuple.Z, tuple.Y);

        public static Tuple2D ToTuple2D(this IntTuple3D tuple) 
            => new Tuple2D(tuple.ItemX, tuple.ItemY);

        public static Tuple2D ToTuple2D(this ITuple4D tuple) 
            => new Tuple2D(tuple.X, tuple.Y);


        public static IntTuple2D ToTuple2DInt(this ITuple2D tuple) 
            => new IntTuple2D((int)tuple.X, (int)tuple.Y);

        public static IntTuple2D ToTuple2DInt(this IntTuple2D tuple) 
            => new IntTuple2D(tuple.X, tuple.Y);

        public static IntTuple2D ToTuple2DInt(this ITuple3D tuple) 
            => new IntTuple2D((int)tuple.X, (int)tuple.Y);

        public static IntTuple2D ToTuple2DInt(this IntTuple3D tuple) 
            => new IntTuple2D(tuple.ItemX, tuple.ItemY);

        public static IntTuple2D ToTuple2DInt(this ITuple4D tuple) 
            => new IntTuple2D((int)tuple.X, (int)tuple.Y);


        public static DistinctTuplesList2D ToDistinctTuplesList(this IEnumerable<ITuple2D> tuplesList)
            => new DistinctTuplesList2D(tuplesList);


        public static Tuple3D XyToTuple3D(this ITuple2D tuple) 
            => new Tuple3D(tuple.X, tuple.Y, 0.0d);

        public static Tuple3D XyToTuple3D(this IntTuple2D tuple) 
            => new Tuple3D(tuple.X, tuple.Y, 0.0d);

        public static Tuple3D ToTuple3D(this ITuple3D tuple) 
            => new Tuple3D(tuple.X, tuple.Y, tuple.Z);

        public static Tuple3D ToTuple3D(this IntTuple3D tuple) 
            => new Tuple3D(tuple.ItemX, tuple.ItemY, tuple.ItemZ);

        public static Tuple3D XyzToTuple3D(this ITuple4D tuple) 
            => new Tuple3D(tuple.X, tuple.Y, tuple.Z);


        public static IntTuple3D ToTuple3DInt(this ITuple2D tuple) 
            => new IntTuple3D((int)tuple.X, (int)tuple.Y, 0);

        public static IntTuple3D ToTuple3DInt(this IntTuple2D tuple) 
            => new IntTuple3D(tuple.X, tuple.Y, 0);

        public static IntTuple3D ToTuple3DInt(this ITuple3D tuple) 
            => new IntTuple3D((int)tuple.X, (int)tuple.Y, (int)tuple.Z);

        public static IntTuple3D ToTuple3DInt(this IntTuple3D tuple) 
            => new IntTuple3D(tuple.ItemX, tuple.ItemY, tuple.ItemZ);

        public static IntTuple3D ToTuple3DInt(this ITuple4D tuple) 
            => new IntTuple3D((int)tuple.X, (int)tuple.Y, (int)tuple.Z);


        public static DistinctTuplesList3D ToDistinctTuplesList(this IEnumerable<ITuple3D> tuplesList)
            => new DistinctTuplesList3D(tuplesList);


        public static Tuple4D ToTuple4D(this ITuple2D tuple) 
            => new Tuple4D(tuple.X, tuple.Y, 0.0d, 0.0d);

        public static Tuple4D ToTuple4D(this IntTuple2D tuple) 
            => new Tuple4D(tuple.X, tuple.Y, 0.0d, 0.0d);

        public static Tuple4D ToTuple4D(this ITuple3D tuple) 
            => new Tuple4D(tuple.X, tuple.Y, tuple.Z, 0.0d);

        public static Tuple4D ToTuple4D(this ITuple4D tuple) 
            => new Tuple4D(tuple.X, tuple.Y, tuple.Z, tuple.W);

        public static Tuple4D ToTuple4D(this IntTuple3D tuple) 
            => new Tuple4D(tuple.ItemX, tuple.ItemY, tuple.ItemZ, 0.0d);


        public static Tuple2D GetRealTuple(this IComplexTuple2D tuple)
            => new Tuple2D(
                tuple.X.Real,
                tuple.Y.Real
            );

        public static Tuple3D GetRealTuple(this IComplexTuple3D tuple)
            => new Tuple3D(
                tuple.X.Real,
                tuple.Y.Real,
                tuple.Z.Real
            );

        public static Tuple4D GetRealTuple(this IComplexTuple4D tuple)
            => new Tuple4D(
                tuple.X.Real,
                tuple.Y.Real,
                tuple.Z.Real,
                tuple.W.Real
            );

        public static Tuple2D GetImaginaryTuple(this IComplexTuple2D tuple)
            => new Tuple2D(
                tuple.X.Imaginary,
                tuple.Y.Imaginary
            );

        public static Tuple3D GetImaginaryTuple(this IComplexTuple3D tuple)
            => new Tuple3D(
                tuple.X.Imaginary,
                tuple.Y.Imaginary,
                tuple.Z.Imaginary
            );

        public static Tuple4D GetImaginaryTuple(this IComplexTuple4D tuple)
            => new Tuple4D(
                tuple.X.Imaginary,
                tuple.Y.Imaginary,
                tuple.Z.Imaginary,
                tuple.W.Imaginary
            );


        /// <summary>
        /// Returns a permuted version of the components of this tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        public static Tuple2D Permute(this Tuple2D tuple, int xIndex, int yIndex)
        {
            return new Tuple2D(tuple[xIndex], tuple[yIndex]);
        }

        /// <summary>
        /// Returns a permuted version of the components of this tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        public static IntTuple2D Permute(this IntTuple2D tuple, int xIndex, int yIndex)
        {
            return new IntTuple2D(tuple[xIndex], tuple[yIndex]);
        }

        /// <summary>
        /// Returns a permuted version of the components of this tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <param name="zIndex"></param>
        /// <returns></returns>
        public static Tuple3D Permute(this Tuple3D tuple, int xIndex, int yIndex, int zIndex)
        {
            return new Tuple3D(tuple[xIndex], tuple[yIndex], tuple[zIndex]);
        }

        /// <summary>
        /// Returns a permuted version of the components of this tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <param name="zIndex"></param>
        /// <returns></returns>
        public static IntTuple3D Permute(this IntTuple3D tuple, int xIndex, int yIndex, int zIndex)
        {
            return new IntTuple3D(tuple[xIndex], tuple[yIndex], tuple[zIndex]);
        }

        /// <summary>
        /// Returns a permuted version of the components of this tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <param name="zIndex"></param>
        /// <param name="wIndex"></param>
        /// <returns></returns>
        public static Tuple4D Permute(this Tuple4D tuple, int xIndex, int yIndex, int zIndex, int wIndex)
        {
            return new Tuple4D(tuple[xIndex], tuple[yIndex], tuple[zIndex], tuple[wIndex]);
        }


        /// <summary>
        /// Returns a permuted version of the components of this tuple. The given indices are always
        /// converted to a valid range using modulus operation
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        public static Tuple2D SafePermute(this Tuple2D tuple, int xIndex, int yIndex)
        {
            return new Tuple2D(tuple[xIndex.Mod(2)], tuple[yIndex.Mod(2)]);
        }

        /// <summary>
        /// Returns a permuted version of the components of this tuple. The given indices are always
        /// converted to a valid range using modulus operation
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        public static IntTuple2D SafePermute(this IntTuple2D tuple, int xIndex, int yIndex)
        {
            return new IntTuple2D(tuple[xIndex.Mod(2)], tuple[yIndex.Mod(2)]);
        }

        /// <summary>
        /// Returns a permuted version of the components of this tuple. The given indices are always
        /// converted to a valid range using modulus operation
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <param name="zIndex"></param>
        /// <returns></returns>
        public static Tuple3D SafePermute(this Tuple3D tuple, int xIndex, int yIndex, int zIndex)
        {
            return new Tuple3D(tuple[xIndex.Mod(3)], tuple[yIndex.Mod(3)], tuple[zIndex.Mod(3)]);
        }

        /// <summary>
        /// Returns a permuted version of the components of this tuple. The given indices are always
        /// converted to a valid range using modulus operation
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <param name="zIndex"></param>
        /// <returns></returns>
        public static IntTuple3D SafePermute(this IntTuple3D tuple, int xIndex, int yIndex, int zIndex)
        {
            return new IntTuple3D(tuple[xIndex.Mod(3)], tuple[yIndex.Mod(3)], tuple[zIndex.Mod(3)]);
        }

        /// <summary>
        /// Returns a permuted version of the components of this tuple. The given indices are always
        /// converted to a valid range using modulus operation
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <param name="zIndex"></param>
        /// <param name="wIndex"></param>
        /// <returns></returns>
        public static Tuple4D SafePermute(this Tuple4D tuple, int xIndex, int yIndex, int zIndex, int wIndex)
        {
            return new Tuple4D(tuple[xIndex.Mod(4)], tuple[yIndex.Mod(4)], tuple[zIndex.Mod(4)], tuple[wIndex.Mod(4)]);
        }

        public static IEnumerable<double> GetComponents(this ITuple2D tuple)
        {
            yield return tuple.X;
            yield return tuple.Y;
        }

        public static IEnumerable<double> GetComponents(this ITuple3D tuple)
        {
            yield return tuple.X;
            yield return tuple.Y;
            yield return tuple.Z;
        }

        public static IEnumerable<double> GetComponents(this ITuple4D tuple)
        {
            yield return tuple.X;
            yield return tuple.Y;
            yield return tuple.Z;
            yield return tuple.W;
        }

        public static IEnumerable<double> GetComponents(this IEnumerable<ITuple2D> tuplesList)
            => tuplesList.SelectMany(t => t.GetComponents());

        public static IEnumerable<double> GetComponents(this IEnumerable<ITuple3D> tuplesList)
            => tuplesList.SelectMany(t => t.GetComponents());

        public static IEnumerable<double> GetComponents(this IEnumerable<ITuple4D> tuplesList)
            => tuplesList.SelectMany(t => t.GetComponents());
    }
}
