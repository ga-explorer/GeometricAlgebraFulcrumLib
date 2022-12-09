using DataStructuresLib.Random;
using NumericalGeometryLib.BasicMath.Coordinates;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;
using NumericalGeometryLib.BasicMath.Tuples.Collections;

namespace NumericalGeometryLib.BasicMath.Tuples
{
    public static class Float64TupleUtils
    {
        #region Random Operations

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple GetFloat64Tuple(this System.Random random, int dimensions)
        {
            var vector =
                MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(
                    dimensions, 
                    _ => random.NextDouble()
                );

            return Float64Tuple.Create(vector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple GetFloat64Tuple(this System.Random random, int dimensions, double minValue, double maxValue)
        {
            var vector =
                MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(
                    dimensions, 
                    _ => random.NextDouble(minValue, maxValue)
                );

            return Float64Tuple.Create(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple GetSparseFloat64Tuple(this System.Random random, int dimensions)
        {
            var vector =
                MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Sparse(dimensions);

            var count = random.Next(1, dimensions);
            var indexList = random.GetUniqueIndices(count, dimensions);

            foreach (var index in indexList)
                vector[index] = random.NextDouble();

            return Float64Tuple.Create(vector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple GetSparseFloat64Tuple(this System.Random random, int dimensions, int count)
        {
            if (count > dimensions)
                count = dimensions;

            var vector =
                MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Sparse(dimensions);

            var indexList = random.GetUniqueIndices(count, dimensions);

            foreach (var index in indexList)
                vector[index] = random.NextDouble();

            return Float64Tuple.Create(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple GetSparseFloat64Tuple(this System.Random random, int dimensions, double minValue, double maxValue)
        {
            var vector =
                MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Sparse(dimensions);

            var count = random.Next(1, dimensions);
            var indexList = random.GetUniqueIndices(count, dimensions);

            foreach (var index in indexList)
                vector[index] = random.NextDouble(minValue, maxValue);

            return Float64Tuple.Create(vector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorDirectionalScaling GetVectorDirectionalScaling(this System.Random random, int dimensions, double minValue, double maxValue)
        {
            return VectorDirectionalScaling.Create(
                random.GetNumber(minValue, maxValue),
                random.GetFloat64Tuple(dimensions).InPlaceNormalize()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HyperPlaneNormalReflection GetHyperPlaneNormalReflection(this System.Random random, int dimensions)
        {
            return HyperPlaneNormalReflection.Create(
                random.GetFloat64Tuple(dimensions).InPlaceNormalize()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorToVectorRotation GetVectorToVectorRotation(this System.Random random, int dimensions)
        {
            var u = random.GetFloat64Tuple(dimensions).InPlaceNormalize();
            var v = random.GetFloat64Tuple(dimensions).InPlaceNormalize();

            return VectorToVectorRotation.Create(u, v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HyperPlaneNormalReflection GetHyperPlaneReflection(this System.Random random, int dimensions)
        {
            var u = random.GetFloat64Tuple(dimensions).InPlaceNormalize();

            return HyperPlaneNormalReflection.Create(u);
        }
        #endregion


        #region Coordinates Operations
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ToTuple2D(this Complex complexNumber)
        {
            return new Float64Tuple2D(
                complexNumber.Real,
                complexNumber.Imaginary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ToTuple2D(this IPolarPosition2D polarPosition)
        {
            return new Float64Tuple2D(
                polarPosition.R * Math.Cos(polarPosition.Theta),
                polarPosition.R * Math.Sin(polarPosition.Theta)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolarPosition2D ToPolarPosition(this Complex complexNumber)
        {
            return new PolarPosition2D(
                complexNumber.Magnitude,
                complexNumber.Phase
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolarPosition2D ToPolarPosition(this IFloat64Tuple2D point)
        {
            return new PolarPosition2D(
                Math.Sqrt(point.X * point.X + point.Y * point.Y),
                Math.Atan2(point.Y, point.X)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ToTuple3D(this UnitSphericalPosition3D sphericalPosition)
        {
            var sinTheta = Math.Sin(sphericalPosition.Theta);

            return new Float64Tuple3D(
                sinTheta * Math.Cos(sphericalPosition.Phi),
                sinTheta * Math.Sin(sphericalPosition.Phi),
                Math.Cos(sphericalPosition.Theta)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ToTuple3D(this UnitSphericalPosition3D sphericalPosition, double r)
        {
            var rSinTheta = r * Math.Sin(sphericalPosition.Theta);

            return new Float64Tuple3D(
                rSinTheta * Math.Cos(sphericalPosition.Phi),
                rSinTheta * Math.Sin(sphericalPosition.Phi),
                r * Math.Cos(sphericalPosition.Theta)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ToTuple3D(this ISphericalPosition3D sphericalPosition)
        {
            var rSinTheta = sphericalPosition.R * Math.Sin(sphericalPosition.Theta);

            return new Float64Tuple3D(
                rSinTheta * Math.Cos(sphericalPosition.Phi),
                rSinTheta * Math.Sin(sphericalPosition.Phi),
                sphericalPosition.R * Math.Cos(sphericalPosition.Theta)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SphericalPosition3D ToSphericalPosition(this IFloat64Tuple3D position)
        {
            var r = Math.Sqrt(
                position.X * position.X +
                position.Y * position.Y +
                position.Z * position.Z
            );

            return new SphericalPosition3D(
                Math.Acos(r / position.Z),
                Math.Atan2(position.Y, position.X),
                r
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitSphericalPosition3D ToUnitSphericalPosition(this IFloat64Tuple3D position)
        {
            var r = Math.Sqrt(
                position.X * position.X +
                position.Y * position.Y +
                position.Z * position.Z
            );

            return new UnitSphericalPosition3D(
                Math.Acos(r / position.Z),
                Math.Atan2(position.Y, position.X)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitSphericalPosition3D ToUnitSphericalPosition(this ISphericalPosition3D sphericalPosition)
        {
            return new UnitSphericalPosition3D(
                sphericalPosition.Theta,
                sphericalPosition.Phi
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SphericalPosition3D ToSphericalPosition(this UnitSphericalPosition3D sphericalPosition, double r)
        {
            return new SphericalPosition3D(
                sphericalPosition.Theta,
                sphericalPosition.Phi,
                r
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetUnitVectorR(this ISphericalPosition3D sphericalPosition)
        {
            var sinTheta = Math.Sin(sphericalPosition.Theta);
            var cosTheta = Math.Cos(sphericalPosition.Theta);

            var sinPhi = Math.Sin(sphericalPosition.Phi);
            var cosPhi = Math.Cos(sphericalPosition.Phi);

            return new Float64Tuple3D(
                sinTheta * cosPhi,
                sinTheta * sinPhi,
                cosTheta
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetUnitVectorR(this IFloat64Tuple3D vector)
        {
            var r = vector.GetVectorNorm();

            var cosTheta = r / vector.Z;
            var sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            var phi = Math.Atan2(vector.Y, vector.X);
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return new Float64Tuple3D(
                sinTheta * cosPhi,
                sinTheta * sinPhi,
                cosTheta
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetUnitVectorTheta(this ISphericalPosition3D sphericalPosition)
        {
            var sinTheta = Math.Sin(sphericalPosition.Theta);
            var cosTheta = Math.Cos(sphericalPosition.Theta);

            var sinPhi = Math.Sin(sphericalPosition.Phi);
            var cosPhi = Math.Cos(sphericalPosition.Phi);

            return new Float64Tuple3D(
                cosTheta * cosPhi,
                cosTheta * sinPhi,
                -sinTheta
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetUnitVectorTheta(this IFloat64Tuple3D vector)
        {
            var r = vector.GetVectorNorm();

            var cosTheta = vector.Z / r;
            var sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            var phi = Math.Atan2(vector.Y, vector.X);
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return new Float64Tuple3D(
                cosTheta * cosPhi,
                cosTheta * sinPhi,
                -sinTheta
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetUnitVectorPhi(this ISphericalPosition3D sphericalPosition)
        {
            var sinPhi = Math.Sin(sphericalPosition.Phi);
            var cosPhi = Math.Cos(sphericalPosition.Phi);

            return new Float64Tuple3D(-sinPhi, cosPhi, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetUnitVectorPhi(this IFloat64Tuple3D vector)
        {
            var phi = Math.Atan2(vector.Y, vector.X);
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return new Float64Tuple3D(-sinPhi, cosPhi, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple CreateTuple(this double[] itemArray, bool normalize = false)
        {
            if (normalize) 
                itemArray.VectorNormalizeInPlace();

            return Float64Tuple.Create(itemArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<bool, double, Axis> TryToAxis(this Float64Tuple vector)
        {
            return TryVectorToAxis(vector.ScalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<bool, double, Axis> TryToNearAxis(this Float64Tuple vector, double epsilon = 1e-12)
        {
            return TryVectorToNearAxis(vector.ScalarArray, epsilon);
        }

        public static Tuple<bool, double, Axis> TryVectorToAxis(this double[] itemArray)
        {
            var dimensions = itemArray.Length;

            // Find if the given scaling vector is parallel to a basis vector
            var basisIndex = -1;
            for (var i = 0; i < dimensions; i++)
            {
                if (itemArray[i].IsExactZero()) continue;

                if (basisIndex >= 0)
                {
                    basisIndex = -2;
                    break;
                }

                basisIndex = i;
            }

            if (basisIndex < 0)
                return new Tuple<bool, double, Axis>(
                    false,
                    0d,
                    new Axis(dimensions, 0)
                );

            var scalar = itemArray[basisIndex];
            return new Tuple<bool, double, Axis>(
                true,
                scalar.Abs(),
                new Axis(dimensions, basisIndex, scalar < 0)
            );
        }

        public static Tuple<bool, double, Axis> TryVectorToNearAxis(this double[] itemArray, double epsilon = 1e-12)
        {
            var dimensions = itemArray.Length;

            // Find if the given scaling vector is parallel to a basis vector
            var basisIndex = -1;
            for (var i = 0; i < dimensions; i++)
            {
                if (itemArray[i].IsNearZero(epsilon)) continue;

                if (basisIndex >= 0)
                {
                    basisIndex = -2;
                    break;
                }

                basisIndex = i;
            }

            if (basisIndex < 0)
                return new Tuple<bool, double, Axis>(
                    false,
                    0d,
                    new Axis(dimensions, 0)
                );

            var scalar = itemArray[basisIndex];
            return new Tuple<bool, double, Axis>(
                true,
                scalar.Abs(),
                new Axis(dimensions, basisIndex, scalar < 0)
            );
        }

        #endregion

        #region Interpolation
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple Lerp(this double t, Float64Tuple v1, Float64Tuple v2)
        {
            var s = 1d - t;

            var a1 = v1.ScalarArray;
            var a2 = v2.ScalarArray;
            var scalarArray = new double[a1.Length];

            for (var i = 0; i < a1.Length;)
                scalarArray[i] = s * a1[i] + t * a2[i];

            return Float64Tuple.Create(scalarArray);
        }
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Float64SparseTuple Lerp(this double t, Float64SparseTuple v1, Float64SparseTuple v2)
        //{
        //    return (1.0d - t) * v1 + t * v2;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Lerp(this double t, IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            var s = 1.0d - t;

            return new Float64Tuple2D(
                s * v1.X + t * v2.X,
                s * v1.Y + t * v2.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Lerp(this double t, IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            var s = 1.0d - t;

            return new Float64Tuple3D(
                s * v1.X + t * v2.X,
                s * v1.Y + t * v2.Y,
                s * v1.Z + t * v2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Lerp(this double t, IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            var s = 1.0d - t;

            return new Float64Tuple4D(
                s * v1.X + t * v2.X,
                s * v1.Y + t * v2.Y,
                s * v1.Z + t * v2.Z,
                s * v1.W + t * v2.W
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Tuple2D> Lerp(this IEnumerable<double> tList, IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            return tList.Select(t => t.Lerp(v1, v2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Tuple3D> Lerp(this IEnumerable<double> tList, IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return tList.Select(t => t.Lerp(v1, v2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Tuple4D> Lerp(this IEnumerable<double> tList, IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            return tList.Select(t => t.Lerp(v1, v2));
        }

        /// <summary>
        /// Spherical Linear Interpolation of two normalized quaternions.
        /// See https://en.wikipedia.org/wiki/Slerp for details
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Float64Tuple4D Slerp(this double t, IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            //Compute the cosine of the angle between the two vectors.
            var s = 1.0d - t;
            var cosTheta = v1.VectorDot(v2);

            if (cosTheta > 0.9995d)
            {
                //If the inputs are too close for comfort, linearly interpolate and normalize the result.
                var x = s * v1.X + t * v2.X;
                var y = s * v1.Y + t * v2.Y;
                var z = s * v1.Z + t * v2.Z;
                var w = s * v1.W + t * v2.W;

                var d = 1.0d / Math.Sqrt(x * x + y * y + z * z + w * w);

                return new Float64Tuple4D(x * d, y * d, z * d, w * d);
            }

            //If the dot product is negative, the quaternions have opposite handedness
            //and slerp won't take the shorter path. Fix by reversing one quaternion.
            if (cosTheta < 0.0d)
            {
                v1 = v1.ToNegativeVector();
                cosTheta = -cosTheta;
            }

            //Robustness: Stay within domain of acos()
            // theta = angle between input quaternions, interpreted as vectors
            var theta = Math.Acos(cosTheta.Clamp(-1.0d, 1.0d));

            //thetaP = angle between value1 and result quaternion, interpreted as vectors
            var thetaP = theta * t;
            var cosThetaP = Math.Cos(thetaP);
            var sinThetaP = Math.Sin(thetaP);

            //Make { value1, qPerp } an orthogonal basis in quaternion vector space
            var qPerpX = v2.X - v1.X * cosTheta;
            var qPerpY = v2.Y - v1.Y * cosTheta;
            var qPerpZ = v2.Z - v1.Z * cosTheta;
            var qPerpW = v2.W - v1.W * cosTheta;
            var qPerpInvLength = 1.0d / Math.Sqrt(qPerpX * qPerpX + qPerpY * qPerpY + qPerpZ * qPerpZ + qPerpW * qPerpW);

            //Final result
            return new Float64Tuple4D(
                v1.X * cosThetaP + qPerpX * qPerpInvLength * sinThetaP,
                v1.Y * cosThetaP + qPerpY * qPerpInvLength * sinThetaP,
                v1.Z * cosThetaP + qPerpZ * qPerpInvLength * sinThetaP,
                v1.W * cosThetaP + qPerpW * qPerpInvLength * sinThetaP
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Tuple4D> Slerp(this IEnumerable<double> tList, IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            return tList.Select(t => t.Slerp(v1, v2));
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFinite(this IFloat64Tuple2D tuple)
        {
            return !(
                double.IsNaN(tuple.X) || double.IsInfinity(tuple.X) ||
                double.IsNaN(tuple.Y) || double.IsInfinity(tuple.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFinite(this IFloat64Tuple3D tuple)
        {
            return !(
                double.IsNaN(tuple.X) || double.IsInfinity(tuple.X) ||
                double.IsNaN(tuple.Y) || double.IsInfinity(tuple.Y) ||
                double.IsNaN(tuple.Z) || double.IsInfinity(tuple.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFinite(this IFloat64Tuple4D tuple)
        {
            return !(
                double.IsNaN(tuple.X) || double.IsInfinity(tuple.X) ||
                double.IsNaN(tuple.Y) || double.IsInfinity(tuple.Y) ||
                double.IsNaN(tuple.Z) || double.IsInfinity(tuple.Z) ||
                double.IsNaN(tuple.W) || double.IsInfinity(tuple.W)
            );
        }

        /// <summary>
        /// The value of the smallest component in this tuple
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetMinTupleComponent(this IFloat64Tuple2D tuple)
        {
            return (tuple.X < tuple.Y) ? tuple.X : tuple.Y;
        }

        /// <summary>
        /// The value of the largest component in this tuple
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetMaxTupleComponent(this IFloat64Tuple2D tuple)
        {
            return (tuple.X > tuple.Y) ? tuple.X : tuple.Y;
        }

        /// <summary>
        /// The index of the smallest component of this tuple
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMinTupleComponentIndex(this IFloat64Tuple2D tuple)
        {
            return (tuple.X < tuple.Y) ? 0 : 1;
        }

        /// <summary>
        /// The index of the largest component of this tuple
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMaxTupleComponentIndex(this IFloat64Tuple2D tuple)
        {
            return (tuple.X > tuple.Y) ? 0 : 1;
        }

        /// <summary>
        /// The index of the largest absolute component in this tuple
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMaxAbsTupleComponentIndex(this IFloat64Tuple2D tuple)
        {
            return Math.Abs(tuple.X) > Math.Abs(tuple.Y) ? 0 : 1;
        }

        /// <summary>
        /// The value of the smallest component in this tuple
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetMinComponent(this IFloat64Tuple3D tuple)
        {
            return (tuple.X < tuple.Y)
                ? (tuple.X < tuple.Z ? tuple.X : tuple.Z)
                : (tuple.Y < tuple.Z ? tuple.Y : tuple.Z);
        }

        /// <summary>
        /// The value of the largest component in this tuple
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetMaxComponent(this IFloat64Tuple3D tuple)
        {
            return (tuple.X > tuple.Y)
                ? (tuple.X > tuple.Z ? tuple.X : tuple.Z)
                : (tuple.Y > tuple.Z ? tuple.Y : tuple.Z);
        }

        /// <summary>
        /// The index of the smallest component in this tuple
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMinComponentIndex(this IFloat64Tuple3D tuple)
        {
            return (tuple.X < tuple.Y) ? (tuple.X < tuple.Z ? 0 : 2) : (tuple.Y < tuple.Z ? 1 : 2);
        }

        /// <summary>
        /// The index of the largest component in this tuple
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMaxComponentIndex(this IFloat64Tuple3D tuple)
        {
            return (tuple.X > tuple.Y) ? (tuple.X > tuple.Z ? 0 : 2) : (tuple.Y > tuple.Z ? 1 : 2);
        }

        /// <summary>
        /// The index of the largest absolute component in this tuple
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMaxAbsComponentIndex(this IFloat64Tuple3D tuple)
        {
            var absX = Math.Abs(tuple.X);
            var absY = Math.Abs(tuple.Y);
            var absZ = Math.Abs(tuple.Z);

            if (absX > absY)
                return absX > absZ ? 0 : 2;

            return absY > absZ ? 1 : 2;
        }
        
        /// <summary>
        /// The index of the largest absolute component in this tuple
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMinAbsComponentIndex(this IFloat64Tuple3D tuple)
        {
            var absX = Math.Abs(tuple.X);
            var absY = Math.Abs(tuple.Y);
            var absZ = Math.Abs(tuple.Z);

            if (absX < absY)
                return absX < absZ ? 0 : 2;

            return absY < absZ ? 1 : 2;
        }

        /// <summary>
        /// Returns a new tuple containing component-wise minimum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Min(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            return new Float64Tuple2D(
                v1.X < v2.X ? v1.X : v2.X,
                v1.Y < v2.Y ? v1.Y : v2.Y
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise minimum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple2D Min(this IntTuple2D v1, IntTuple2D v2)
        {
            return new IntTuple2D(
                v1.X < v2.X ? v1.X : v2.X,
                v1.Y < v2.Y ? v1.Y : v2.Y
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise minimum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Min(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return new Float64Tuple3D(
                v1.X < v2.X ? v1.X : v2.X,
                v1.Y < v2.Y ? v1.Y : v2.Y,
                v1.Z < v2.Z ? v1.Z : v2.Z
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise minimum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple3D Min(this IntTuple3D v1, IntTuple3D v2)
        {
            return new IntTuple3D(
                v1.ItemX < v2.ItemX ? v1.ItemX : v2.ItemX,
                v1.ItemY < v2.ItemY ? v1.ItemY : v2.ItemY,
                v1.ItemZ < v2.ItemZ ? v1.ItemZ : v2.ItemZ
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise minimum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Min(this IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            return new Float64Tuple4D(
                v1.X < v2.X ? v1.X : v2.X,
                v1.Y < v2.Y ? v1.Y : v2.Y,
                v1.Z < v2.Z ? v1.Z : v2.Z,
                v1.W < v2.W ? v1.W : v2.W
            );
        }


        /// <summary>
        /// Returns a new tuple containing component-wise maximum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Max(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            return new Float64Tuple2D(
                v1.X > v2.X ? v1.X : v2.X,
                v1.Y > v2.Y ? v1.Y : v2.Y
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise maximum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple2D Max(this IntTuple2D v1, IntTuple2D v2)
        {
            return new IntTuple2D(
                v1.X > v2.X ? v1.X : v2.X,
                v1.Y > v2.Y ? v1.Y : v2.Y
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise maximum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Max(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return new Float64Tuple3D(
                v1.X > v2.X ? v1.X : v2.X,
                v1.Y > v2.Y ? v1.Y : v2.Y,
                v1.Z > v2.Z ? v1.Z : v2.Z
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise maximum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple3D Max(this IntTuple3D v1, IntTuple3D v2)
        {
            return new IntTuple3D(
                v1.ItemX > v2.ItemX ? v1.ItemX : v2.ItemX,
                v1.ItemY > v2.ItemY ? v1.ItemY : v2.ItemY,
                v1.ItemZ > v2.ItemZ ? v1.ItemZ : v2.ItemZ
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise maximum values of the given tuples
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Max(this IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            return new Float64Tuple4D(
                v1.X > v2.X ? v1.X : v2.X,
                v1.Y > v2.Y ? v1.Y : v2.Y,
                v1.Z > v2.Z ? v1.Z : v2.Z,
                v1.W > v2.W ? v1.W : v2.W
            );
        }


        /// <summary>
        /// Returns a new tuple containing component-wise ceiling values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Ceiling(this IFloat64Tuple2D tuple)
        {
            return new Float64Tuple2D(
                Math.Ceiling(tuple.X),
                Math.Ceiling(tuple.Y)
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise ceiling values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Ceiling(this IFloat64Tuple3D tuple)
        {
            return new Float64Tuple3D(
                Math.Ceiling(tuple.X),
                Math.Ceiling(tuple.Y),
                Math.Ceiling(tuple.Z)
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise ceiling values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Ceiling(this IFloat64Tuple4D tuple)
        {
            return new Float64Tuple4D(
                Math.Ceiling(tuple.X),
                Math.Ceiling(tuple.Y),
                Math.Ceiling(tuple.Z),
                Math.Ceiling(tuple.W)
            );
        }


        /// <summary>
        /// Returns a new tuple containing component-wise floor values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Floor(this IFloat64Tuple2D tuple)
        {
            return new Float64Tuple2D(
                Math.Floor(tuple.X),
                Math.Floor(tuple.Y)
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise floor values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Floor(this IFloat64Tuple3D tuple)
        {
            return new Float64Tuple3D(
                Math.Floor(tuple.X),
                Math.Floor(tuple.Y),
                Math.Floor(tuple.Z)
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise floor values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Floor(this IFloat64Tuple4D tuple)
        {
            return new Float64Tuple4D(
                Math.Floor(tuple.X),
                Math.Floor(tuple.Y),
                Math.Floor(tuple.Z),
                Math.Floor(tuple.W)
            );
        }


        /// <summary>
        /// Returns a new tuple containing component-wise absolute values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Abs(this IFloat64Tuple2D tuple)
        {
            return new Float64Tuple2D(
                Math.Abs(tuple.X),
                Math.Abs(tuple.Y)
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise absolute values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple2D Abs(this IntTuple2D tuple)
        {
            return new IntTuple2D(
                Math.Abs(tuple.X),
                Math.Abs(tuple.Y)
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise absolute values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Abs(this IFloat64Tuple3D tuple)
        {
            return new Float64Tuple3D(
                Math.Abs(tuple.X),
                Math.Abs(tuple.Y),
                Math.Abs(tuple.Z)
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise absolute values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple3D Abs(this IntTuple3D tuple)
        {
            return new IntTuple3D(
                Math.Abs(tuple.ItemX),
                Math.Abs(tuple.ItemY),
                Math.Abs(tuple.ItemZ)
            );
        }

        /// <summary>
        /// Returns a new tuple containing component-wise absolute values of the given tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Abs(this IFloat64Tuple4D tuple)
        {
            return new Float64Tuple4D(
                Math.Abs(tuple.X),
                Math.Abs(tuple.Y),
                Math.Abs(tuple.Z),
                Math.Abs(tuple.W)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableFloat64Tuple2D ToMutableTuple2D(this IFloat64Tuple2D tuple)
        {
            return new MutableFloat64Tuple2D(tuple.X, tuple.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableFloat64Tuple3D ToMutableTuple3D(this IFloat64Tuple3D tuple)
        {
            return new MutableFloat64Tuple3D(tuple.X, tuple.Y, tuple.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ToTuple2D(this IPair<double> tuple)
        {
            return tuple is Float64Tuple2D t
                ? t
                : new Float64Tuple2D(tuple.Item1, tuple.Item2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ToTuple2D(this IntTuple2D tuple)
        {
            return new Float64Tuple2D(tuple.X, tuple.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D XyToTuple2D(this IFloat64Tuple3D tuple)
        {
            return new Float64Tuple2D(tuple.X, tuple.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D YxToTuple2D(this IFloat64Tuple3D tuple)
        {
            return new Float64Tuple2D(tuple.Y, tuple.X);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D XzToTuple2D(this IFloat64Tuple3D tuple)
        {
            return new Float64Tuple2D(tuple.X, tuple.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ZxToTuple2D(this IFloat64Tuple3D tuple)
        {
            return new Float64Tuple2D(tuple.Z, tuple.X);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D YzToTuple2D(this IFloat64Tuple3D tuple)
        {
            return new Float64Tuple2D(tuple.Y, tuple.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ZyToTuple2D(this IFloat64Tuple3D tuple)
        {
            return new Float64Tuple2D(tuple.Z, tuple.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ToTuple2D(this IntTuple3D tuple)
        {
            return new Float64Tuple2D(tuple.ItemX, tuple.ItemY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ToTuple2D(this IFloat64Tuple4D tuple)
        {
            return new Float64Tuple2D(tuple.X, tuple.Y);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple2D ToTuple2DInt(this IFloat64Tuple2D tuple)
        {
            return new IntTuple2D((int) tuple.X, (int) tuple.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple2D ToTuple2DInt(this IntTuple2D tuple)
        {
            return new IntTuple2D(tuple.X, tuple.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple2D ToTuple2DInt(this IFloat64Tuple3D tuple)
        {
            return new IntTuple2D((int) tuple.X, (int) tuple.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple2D ToTuple2DInt(this IntTuple3D tuple)
        {
            return new IntTuple2D(tuple.ItemX, tuple.ItemY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple2D ToTuple2DInt(this IFloat64Tuple4D tuple)
        {
            return new IntTuple2D((int) tuple.X, (int) tuple.Y);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DistinctTuplesList2D ToDistinctTuplesList(this IEnumerable<IFloat64Tuple2D> tuplesList)
        {
            return new DistinctTuplesList2D(tuplesList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D XyToTuple3D(this IFloat64Tuple2D tuple)
        {
            return new Float64Tuple3D(tuple.X, tuple.Y, 0.0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D XyToTuple3D(this IntTuple2D tuple)
        {
            return new Float64Tuple3D(tuple.X, tuple.Y, 0.0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ToTuple3D(this ITriplet<double> tuple)
        {
            return tuple is Float64Tuple3D t
                ? t
                : new Float64Tuple3D(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ToTuple3D(this IntTuple3D tuple)
        {
            return new Float64Tuple3D(tuple.ItemX, tuple.ItemY, tuple.ItemZ);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D XyzToTuple3D(this IFloat64Tuple4D tuple)
        {
            return new Float64Tuple3D(tuple.X, tuple.Y, tuple.Z);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple3D ToTuple3DInt(this IFloat64Tuple2D tuple)
        {
            return new IntTuple3D((int) tuple.X, (int) tuple.Y, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple3D ToTuple3DInt(this IntTuple2D tuple)
        {
            return new IntTuple3D(tuple.X, tuple.Y, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple3D ToTuple3DInt(this IFloat64Tuple3D tuple)
        {
            return new IntTuple3D((int) tuple.X, (int) tuple.Y, (int) tuple.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple3D ToTuple3DInt(this IntTuple3D tuple)
        {
            return new IntTuple3D(tuple.ItemX, tuple.ItemY, tuple.ItemZ);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple3D ToTuple3DInt(this IFloat64Tuple4D tuple)
        {
            return new IntTuple3D((int) tuple.X, (int) tuple.Y, (int) tuple.Z);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DistinctTuplesList3D ToDistinctTuplesList(this IEnumerable<IFloat64Tuple3D> tuplesList)
        {
            return new DistinctTuplesList3D(tuplesList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToTuple4D(this IFloat64Tuple2D tuple)
        {
            return new Float64Tuple4D(tuple.X, tuple.Y, 0.0d, 0.0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToTuple4D(this IntTuple2D tuple)
        {
            return new Float64Tuple4D(tuple.X, tuple.Y, 0.0d, 0.0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToTuple4D(this IFloat64Tuple3D tuple)
        {
            return new Float64Tuple4D(tuple.X, tuple.Y, tuple.Z, 0.0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToTuple4D(this IFloat64Tuple4D tuple)
        {
            return tuple is Float64Tuple4D t
                ? t
                : new Float64Tuple4D(tuple.X, tuple.Y, tuple.Z, tuple.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToTuple4D(this IntTuple3D tuple)
        {
            return new Float64Tuple4D(tuple.ItemX, tuple.ItemY, tuple.ItemZ, 0.0d);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetRealTuple(this IComplexTuple2D tuple)
        {
            return new Float64Tuple2D(
                tuple.X.Real,
                tuple.Y.Real
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetRealTuple(this IComplexTuple3D tuple)
        {
            return new Float64Tuple3D(
                tuple.X.Real,
                tuple.Y.Real,
                tuple.Z.Real
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D GetRealTuple(this IComplexTuple4D tuple)
        {
            return new Float64Tuple4D(
                tuple.X.Real,
                tuple.Y.Real,
                tuple.Z.Real,
                tuple.W.Real
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetImaginaryTuple(this IComplexTuple2D tuple)
        {
            return new Float64Tuple2D(
                tuple.X.Imaginary,
                tuple.Y.Imaginary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetImaginaryTuple(this IComplexTuple3D tuple)
        {
            return new Float64Tuple3D(
                tuple.X.Imaginary,
                tuple.Y.Imaginary,
                tuple.Z.Imaginary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D GetImaginaryTuple(this IComplexTuple4D tuple)
        {
            return new Float64Tuple4D(
                tuple.X.Imaginary,
                tuple.Y.Imaginary,
                tuple.Z.Imaginary,
                tuple.W.Imaginary
            );
        }


        /// <summary>
        /// Returns a permuted version of the components of this tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Permute(this Float64Tuple2D tuple, int xIndex, int yIndex)
        {
            return new Float64Tuple2D(tuple[xIndex], tuple[yIndex]);
        }

        /// <summary>
        /// Returns a permuted version of the components of this tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Permute(this Float64Tuple3D tuple, int xIndex, int yIndex, int zIndex)
        {
            return new Float64Tuple3D(tuple[xIndex], tuple[yIndex], tuple[zIndex]);
        }

        /// <summary>
        /// Returns a permuted version of the components of this tuple
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <param name="zIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Permute(this Float64Tuple4D tuple, int xIndex, int yIndex, int zIndex, int wIndex)
        {
            return new Float64Tuple4D(tuple[xIndex], tuple[yIndex], tuple[zIndex], tuple[wIndex]);
        }


        /// <summary>
        /// Returns a permuted version of the components of this tuple. The given indices are always
        /// converted to a valid range using modulus operation
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D SafePermute(this Float64Tuple2D tuple, int xIndex, int yIndex)
        {
            return new Float64Tuple2D(tuple[xIndex.Mod(2)], tuple[yIndex.Mod(2)]);
        }

        /// <summary>
        /// Returns a permuted version of the components of this tuple. The given indices are always
        /// converted to a valid range using modulus operation
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D SafePermute(this Float64Tuple3D tuple, int xIndex, int yIndex, int zIndex)
        {
            return new Float64Tuple3D(tuple[xIndex.Mod(3)], tuple[yIndex.Mod(3)], tuple[zIndex.Mod(3)]);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D SafePermute(this Float64Tuple4D tuple, int xIndex, int yIndex, int zIndex, int wIndex)
        {
            return new Float64Tuple4D(tuple[xIndex.Mod(4)], tuple[yIndex.Mod(4)], tuple[zIndex.Mod(4)], tuple[wIndex.Mod(4)]);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetComponent(this IFloat64Tuple2D tuple, int index)
        {
            return index switch
            {
                0 => tuple.X,
                1 => tuple.Y,
                _ => 0d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetComponent(this IFloat64Tuple3D tuple, int index)
        {
            return index switch
            {
                0 => tuple.X,
                1 => tuple.Y,
                2 => tuple.Z,
                _ => 0d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetX(this Axis3D tuple)
        {
            return tuple switch
            {
                Axis3D.PositiveX => 1d,
                Axis3D.NegativeX => -1d,
                _ => 0d
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetY(this Axis3D tuple)
        {
            return tuple switch
            {
                Axis3D.PositiveY => 1d,
                Axis3D.NegativeY => -1d,
                _ => 0d
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetZ(this Axis3D tuple)
        {
            return tuple switch
            {
                Axis3D.PositiveZ => 1d,
                Axis3D.NegativeZ => -1d,
                _ => 0d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetComponent(this Axis3D tuple, Axis3D axis)
        {
            return axis switch
            {
                Axis3D.PositiveX => tuple.GetX(),
                Axis3D.NegativeX => -tuple.GetX(),
                Axis3D.PositiveY => tuple.GetY(),
                Axis3D.NegativeY => -tuple.GetY(),
                Axis3D.PositiveZ => tuple.GetZ(),
                Axis3D.NegativeZ => -tuple.GetZ(),
                _ => 0d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetComponent(this IFloat64Tuple3D tuple, Axis3D axis)
        {
            return axis switch
            {
                Axis3D.PositiveX => tuple.X,
                Axis3D.NegativeX => -tuple.X,
                Axis3D.PositiveY => tuple.Y,
                Axis3D.NegativeY => -tuple.Y,
                Axis3D.PositiveZ => tuple.Z,
                Axis3D.NegativeZ => -tuple.Z,
                _ => 0d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetComponent(this IFloat64Tuple4D tuple, int index)
        {
            return index switch
            {
                0 => tuple.X,
                1 => tuple.Y,
                2 => tuple.Z,
                3 => tuple.W,
                _ => 0d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetComponents(this IFloat64Tuple2D tuple)
        {
            yield return tuple.X;
            yield return tuple.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetComponents(this IFloat64Tuple3D tuple)
        {
            yield return tuple.X;
            yield return tuple.Y;
            yield return tuple.Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetComponents(this IFloat64Tuple4D tuple)
        {
            yield return tuple.X;
            yield return tuple.Y;
            yield return tuple.Z;
            yield return tuple.W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetComponents(this IEnumerable<IFloat64Tuple2D> tuplesList)
        {
            return tuplesList.SelectMany(t => t.GetComponents());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetComponents(this IEnumerable<IFloat64Tuple3D> tuplesList)
        {
            return tuplesList.SelectMany(t => t.GetComponents());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetComponents(this IEnumerable<IFloat64Tuple4D> tuplesList)
        {
            return tuplesList.SelectMany(t => t.GetComponents());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D MapComponents(this IFloat64Tuple3D tuple, Func<double, double> scalarMapping)
        {
            return new Float64Tuple3D(
                scalarMapping(tuple.X),
                scalarMapping(tuple.Y),
                scalarMapping(tuple.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ComponentsMin(this IFloat64Tuple3D tuple, double scalar)
        {
            return new Float64Tuple3D(
                Math.Min(tuple.X, scalar),
                Math.Min(tuple.Y, scalar),
                Math.Min(tuple.Z, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ComponentsMax(this IFloat64Tuple3D tuple, double scalar)
        {
            return new Float64Tuple3D(
                Math.Max(tuple.X, scalar),
                Math.Max(tuple.Y, scalar),
                Math.Max(tuple.Z, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ComponentsMin(this IFloat64Tuple3D tuple)
        {
            return Math.Min(tuple.X, Math.Min(tuple.Y, tuple.Z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ComponentsMax(this IFloat64Tuple3D tuple)
        {
            return Math.Max(tuple.X, Math.Max(tuple.Y, tuple.Z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ComponentsAbs(this IFloat64Tuple3D tuple)
        {
            return new Float64Tuple3D(
                Math.Abs(tuple.X),
                Math.Abs(tuple.Y),
                Math.Abs(tuple.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ComponentsProduct(this IFloat64Tuple3D tuple1, IFloat64Tuple3D tuple2)
        {
            return new Float64Tuple3D(
                tuple1.X * tuple2.X,
                tuple1.Y * tuple2.Y,
                tuple1.Z * tuple2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ComponentsProduct(this IFloat64Tuple3D tuple1, IFloat64Tuple3D tuple2, IFloat64Tuple3D tuple3)
        {
            return new Float64Tuple3D(
                tuple1.X * tuple2.X * tuple3.X,
                tuple1.Y * tuple2.Y * tuple3.Y,
                tuple1.Z * tuple2.Z * tuple3.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ComponentsProduct(this IFloat64Tuple4D tuple1, IFloat64Tuple4D tuple2)
        {
            return new Float64Tuple4D(
                tuple1.X * tuple2.X,
                tuple1.Y * tuple2.Y,
                tuple1.Z * tuple2.Z,
                tuple1.W * tuple2.W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ComponentsProduct(this IFloat64Tuple4D tuple1, IFloat64Tuple4D tuple2, IFloat64Tuple4D tuple3)
        {
            return new Float64Tuple4D(
                tuple1.X * tuple2.X * tuple3.X,
                tuple1.Y * tuple2.Y * tuple3.Y,
                tuple1.Z * tuple2.Z * tuple3.Z,
                tuple1.W * tuple2.W * tuple3.W
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<double> GetTupleXPair(this IReadOnlyList<IFloat64Tuple2D> itemArray, int index)
        {
            return new Pair<double>(
                itemArray[index].X,
                itemArray[index + 1].X
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<IFloat64Tuple2D> itemArray, int index)
        {
            return new Triplet<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<double> GetTupleXQuad(this IReadOnlyList<IFloat64Tuple2D> itemArray, int index)
        {
            return new Quad<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X,
                itemArray[index + 3].X
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<double> GetTupleXQuint(this IReadOnlyList<IFloat64Tuple2D> itemArray, int index)
        {
            return new Quint<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X,
                itemArray[index + 3].X,
                itemArray[index + 4].X
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Hexad<double> GetTupleXHexad(this IReadOnlyList<IFloat64Tuple2D> itemArray, int index)
        {
            return new Hexad<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X,
                itemArray[index + 3].X,
                itemArray[index + 4].X,
                itemArray[index + 5].X
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<double> GetTupleYPair(this IReadOnlyList<IFloat64Tuple2D> itemArray, int index)
        {
            return new Pair<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<IFloat64Tuple2D> itemArray, int index)
        {
            return new Triplet<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<double> GetTupleYQuad(this IReadOnlyList<IFloat64Tuple2D> itemArray, int index)
        {
            return new Quad<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y,
                itemArray[index + 3].Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<double> GetTupleYQuint(this IReadOnlyList<IFloat64Tuple2D> itemArray, int index)
        {
            return new Quint<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y,
                itemArray[index + 3].Y,
                itemArray[index + 4].Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Hexad<double> GetTupleYHexad(this IReadOnlyList<IFloat64Tuple2D> itemArray, int index)
        {
            return new Hexad<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y,
                itemArray[index + 3].Y,
                itemArray[index + 4].Y,
                itemArray[index + 5].Y
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<double> GetTupleXPair(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Pair<double>(
                itemArray[index].X,
                itemArray[index + 1].X
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Triplet<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<double> GetTupleXQuad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Quad<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X,
                itemArray[index + 3].X
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<double> GetTupleXQuint(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Quint<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X,
                itemArray[index + 3].X,
                itemArray[index + 4].X
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Hexad<double> GetTupleXHexad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Hexad<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X,
                itemArray[index + 3].X,
                itemArray[index + 4].X,
                itemArray[index + 5].X
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<double> GetTupleYPair(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Pair<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Triplet<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<double> GetTupleYQuad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Quad<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y,
                itemArray[index + 3].Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<double> GetTupleYQuint(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Quint<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y,
                itemArray[index + 3].Y,
                itemArray[index + 4].Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Hexad<double> GetTupleYHexad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Hexad<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y,
                itemArray[index + 3].Y,
                itemArray[index + 4].Y,
                itemArray[index + 5].Y
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<double> GetTupleZPair(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Pair<double>(
                itemArray[index].Z,
                itemArray[index + 1].Z
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<double> GetTupleZTriplet(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Triplet<double>(
                itemArray[index].Z,
                itemArray[index + 1].Z,
                itemArray[index + 2].Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<double> GetTupleZQuad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Quad<double>(
                itemArray[index].Z,
                itemArray[index + 1].Z,
                itemArray[index + 2].Z,
                itemArray[index + 3].Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<double> GetTupleZQuint(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Quint<double>(
                itemArray[index].Z,
                itemArray[index + 1].Z,
                itemArray[index + 2].Z,
                itemArray[index + 3].Z,
                itemArray[index + 4].Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Hexad<double> GetTupleZHexad(this IReadOnlyList<IFloat64Tuple3D> itemArray, int index)
        {
            return new Hexad<double>(
                itemArray[index].Z,
                itemArray[index + 1].Z,
                itemArray[index + 2].Z,
                itemArray[index + 3].Z,
                itemArray[index + 4].Z,
                itemArray[index + 5].Z
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<double> GetTupleXPair(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Pair<double>(
                itemArray[index].X,
                itemArray[index + 1].X
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Triplet<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<double> GetTupleXQuad(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Quad<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X,
                itemArray[index + 3].X
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<double> GetTupleXQuint(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Quint<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X,
                itemArray[index + 3].X,
                itemArray[index + 4].X
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Hexad<double> GetTupleXHexad(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Hexad<double>(
                itemArray[index].X,
                itemArray[index + 1].X,
                itemArray[index + 2].X,
                itemArray[index + 3].X,
                itemArray[index + 4].X,
                itemArray[index + 5].X
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<double> GetTupleYPair(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Pair<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Triplet<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<double> GetTupleYQuad(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Quad<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y,
                itemArray[index + 3].Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<double> GetTupleYQuint(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Quint<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y,
                itemArray[index + 3].Y,
                itemArray[index + 4].Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Hexad<double> GetTupleYHexad(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Hexad<double>(
                itemArray[index].Y,
                itemArray[index + 1].Y,
                itemArray[index + 2].Y,
                itemArray[index + 3].Y,
                itemArray[index + 4].Y,
                itemArray[index + 5].Y
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<double> GetTupleZPair(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Pair<double>(
                itemArray[index].Z,
                itemArray[index + 1].Z
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<double> GetTupleZTriplet(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Triplet<double>(
                itemArray[index].Z,
                itemArray[index + 1].Z,
                itemArray[index + 2].Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<double> GetTupleZQuad(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Quad<double>(
                itemArray[index].Z,
                itemArray[index + 1].Z,
                itemArray[index + 2].Z,
                itemArray[index + 3].Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<double> GetTupleZQuint(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Quint<double>(
                itemArray[index].Z,
                itemArray[index + 1].Z,
                itemArray[index + 2].Z,
                itemArray[index + 3].Z,
                itemArray[index + 4].Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Hexad<double> GetTupleZHexad(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Hexad<double>(
                itemArray[index].Z,
                itemArray[index + 1].Z,
                itemArray[index + 2].Z,
                itemArray[index + 3].Z,
                itemArray[index + 4].Z,
                itemArray[index + 5].Z
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<double> GetTupleWPair(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Pair<double>(
                itemArray[index].W,
                itemArray[index + 1].W
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<double> GetTupleWTriplet(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Triplet<double>(
                itemArray[index].W,
                itemArray[index + 1].W,
                itemArray[index + 2].W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<double> GetTupleWQuad(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Quad<double>(
                itemArray[index].W,
                itemArray[index + 1].W,
                itemArray[index + 2].W,
                itemArray[index + 3].W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<double> GetTupleWQuint(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Quint<double>(
                itemArray[index].W,
                itemArray[index + 1].W,
                itemArray[index + 2].W,
                itemArray[index + 3].W,
                itemArray[index + 4].W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Hexad<double> GetTupleWHexad(this IReadOnlyList<IFloat64Tuple4D> itemArray, int index)
        {
            return new Hexad<double>(
                itemArray[index].W,
                itemArray[index + 1].W,
                itemArray[index + 2].W,
                itemArray[index + 3].W,
                itemArray[index + 4].W,
                itemArray[index + 5].W
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<double> GetTupleItemPair(this IReadOnlyList<Float64Tuple> itemArray, int index, int itemIndex)
        {
            return new Pair<double>(
                itemArray[index][itemIndex],
                itemArray[index + 1][itemIndex]
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<double> GetTupleItemTriplet(this IReadOnlyList<Float64Tuple> itemArray, int index, int itemIndex)
        {
            return new Triplet<double>(
                itemArray[index][itemIndex],
                itemArray[index + 1][itemIndex],
                itemArray[index + 2][itemIndex]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<double> GetTupleItemQuad(this IReadOnlyList<Float64Tuple> itemArray, int index, int itemIndex)
        {
            return new Quad<double>(
                itemArray[index][itemIndex],
                itemArray[index + 1][itemIndex],
                itemArray[index + 2][itemIndex],
                itemArray[index + 3][itemIndex]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<double> GetTupleItemQuint(this IReadOnlyList<Float64Tuple> itemArray, int index, int itemIndex)
        {
            return new Quint<double>(
                itemArray[index][itemIndex],
                itemArray[index + 1][itemIndex],
                itemArray[index + 2][itemIndex],
                itemArray[index + 3][itemIndex],
                itemArray[index + 4][itemIndex]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Hexad<double> GetTupleItemHexad(this IReadOnlyList<Float64Tuple> itemArray, int index, int itemIndex)
        {
            return new Hexad<double>(
                itemArray[index][itemIndex],
                itemArray[index + 1][itemIndex],
                itemArray[index + 2][itemIndex],
                itemArray[index + 3][itemIndex],
                itemArray[index + 4][itemIndex],
                itemArray[index + 5][itemIndex]
            );
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Pair<double> GetTupleItemPair(this IReadOnlyList<Float64SparseTuple> itemArray, int index, int itemIndex)
        //{
        //    return new Pair<double>(
        //        itemArray[index][itemIndex],
        //        itemArray[index + 1][itemIndex]
        //    );
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Triplet<double> GetTupleItemTriplet(this IReadOnlyList<Float64SparseTuple> itemArray, int index, int itemIndex)
        //{
        //    return new Triplet<double>(
        //        itemArray[index][itemIndex],
        //        itemArray[index + 1][itemIndex],
        //        itemArray[index + 2][itemIndex]
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Quad<double> GetTupleItemQuad(this IReadOnlyList<Float64SparseTuple> itemArray, int index, int itemIndex)
        //{
        //    return new Quad<double>(
        //        itemArray[index][itemIndex],
        //        itemArray[index + 1][itemIndex],
        //        itemArray[index + 2][itemIndex],
        //        itemArray[index + 3][itemIndex]
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Quint<double> GetTupleItemQuint(this IReadOnlyList<Float64SparseTuple> itemArray, int index, int itemIndex)
        //{
        //    return new Quint<double>(
        //        itemArray[index][itemIndex],
        //        itemArray[index + 1][itemIndex],
        //        itemArray[index + 2][itemIndex],
        //        itemArray[index + 3][itemIndex],
        //        itemArray[index + 4][itemIndex]
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Hexad<double> GetTupleItemHexad(this IReadOnlyList<Float64SparseTuple> itemArray, int index, int itemIndex)
        //{
        //    return new Hexad<double>(
        //        itemArray[index][itemIndex],
        //        itemArray[index + 1][itemIndex],
        //        itemArray[index + 2][itemIndex],
        //        itemArray[index + 3][itemIndex],
        //        itemArray[index + 4][itemIndex],
        //        itemArray[index + 5][itemIndex]
        //    );
        //}
    }
}
