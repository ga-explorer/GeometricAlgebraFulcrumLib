using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Constants;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples
{
    public static class EuclideanFloat64TupleUtils
    {
        #region MathNet Numerics Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNorm(this MathNet.Numerics.LinearAlgebra.Vector<double> vector)
        {
            return vector.DotProduct(vector).Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNormSquared(this MathNet.Numerics.LinearAlgebra.Vector<double> vector)
        {
            return vector.DotProduct(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorDot(this MathNet.Numerics.LinearAlgebra.Vector<double> vector1, MathNet.Numerics.LinearAlgebra.Vector<double> vector2)
        {
            return vector1.DotProduct(vector2);
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Add(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            return new Float64Tuple2D(
                v1.X + v2.X,
                v1.Y + v2.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Subtract(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            return new Float64Tuple2D(
                v1.X - v2.X,
                v1.Y - v2.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Times(this IFloat64Tuple2D v1, double v2)
        {
            return new Float64Tuple2D(
                v1.X * v2,
                v1.Y * v2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Times(this double v1, IFloat64Tuple2D v2)
        {
            return new Float64Tuple2D(
                v1 * v2.X,
                v1 * v2.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D Divide(this IFloat64Tuple2D v1, double v2)
        {
            v2 = 1d / v2;

            return new Float64Tuple2D(
                v1.X * v2,
                v1.Y * v2
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Add(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return new Float64Tuple3D(
                v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Subtract(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return new Float64Tuple3D(
                v1.X - v2.X,
                v1.Y - v2.Y,
                v1.Z - v2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Times(this IFloat64Tuple3D v1, double v2)
        {
            return new Float64Tuple3D(
                v1.X * v2,
                v1.Y * v2,
                v1.Z * v2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Times(this double v1, IFloat64Tuple3D v2)
        {
            return new Float64Tuple3D(
                v1 * v2.X,
                v1 * v2.Y,
                v1 * v2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Divide(this IFloat64Tuple3D v1, double v2)
        {
            v2 = 1d / v2;

            return new Float64Tuple3D(
                v1.X * v2,
                v1.Y * v2,
                v1.Z * v2
            );
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Add(this IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            return new Float64Tuple4D(
                v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z,
                v1.W + v2.W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Subtract(this IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            return new Float64Tuple4D(
                v1.X - v2.X,
                v1.Y - v2.Y,
                v1.Z - v2.Z,
                v1.W - v2.W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Times(this IFloat64Tuple4D v1, double v2)
        {
            return new Float64Tuple4D(
                v1.X * v2,
                v1.Y * v2,
                v1.Z * v2,
                v1.W * v2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Times(this double v1, IFloat64Tuple4D v2)
        {
            return new Float64Tuple4D(
                v1 * v2.X,
                v1 * v2.Y,
                v1 * v2.Z,
                v1 * v2.W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D Divide(this IFloat64Tuple4D v1, double v2)
        {
            v2 = 1d / v2;

            return new Float64Tuple4D(
                v1.X * v2,
                v1.Y * v2,
                v1.Z * v2,
                v1.W * v2
            );
        }


        public static Float64Tuple2D GetCenterOfMassPoint(this IEnumerable<IFloat64Tuple2D> pointsList)
        {
            var centerX = 0.0d;
            var centerY = 0.0d;

            var pointsCount = 0;
            foreach (var point in pointsList)
            {
                centerX += point.X;
                centerY += point.Y;

                pointsCount++;
            }

            var pointsCountInv = 1.0d / pointsCount;

            return new Float64Tuple2D(
                centerX * pointsCountInv,
                centerY * pointsCountInv
            );
        }

        public static Float64Tuple2D GetCenterPoint(this IEnumerable<IFloat64Tuple2D> pointsList)
        {
            var minX = double.PositiveInfinity;
            var minY = double.PositiveInfinity;

            var maxX = double.NegativeInfinity;
            var maxY = double.NegativeInfinity;

            foreach (var point in pointsList)
            {
                if (point.X < minX) minX = point.X;
                if (point.X > maxX) maxX = point.X;

                if (point.Y < minY) minY = point.Y;
                if (point.Y > maxY) maxY = point.Y;
            }

            return new Float64Tuple2D(
                0.5 * (minX + maxX),
                0.5 * (minY + maxY)
            );
        }

        public static Float64Tuple3D GetCenterOfMassPoint(this IEnumerable<IFloat64Tuple3D> pointsList)
        {
            var centerX = 0.0d;
            var centerY = 0.0d;
            var centerZ = 0.0d;

            var pointsCount = 0;
            foreach (var point in pointsList)
            {
                centerX += point.X;
                centerY += point.Y;
                centerZ += point.Z;

                pointsCount++;
            }

            var pointsCountInv = 1.0d / pointsCount;

            return new Float64Tuple3D(
                centerX * pointsCountInv,
                centerY * pointsCountInv,
                centerZ * pointsCountInv
            );
        }

        public static Float64Tuple3D GetCenterPoint(this IEnumerable<IFloat64Tuple3D> pointsList)
        {
            var minX = double.PositiveInfinity;
            var minY = double.PositiveInfinity;
            var minZ = double.PositiveInfinity;

            var maxX = double.NegativeInfinity;
            var maxY = double.NegativeInfinity;
            var maxZ = double.NegativeInfinity;

            foreach (var point in pointsList)
            {
                if (point.X < minX) minX = point.X;
                if (point.X > maxX) maxX = point.X;

                if (point.Y < minY) minY = point.Y;
                if (point.Y > maxY) maxY = point.Y;

                if (point.Z < minZ) minZ = point.Z;
                if (point.Z > maxZ) maxZ = point.Z;
            }

            return new Float64Tuple3D(
                0.5 * (minX + maxX),
                0.5 * (minY + maxY),
                0.5 * (minZ + maxZ)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetDistancesToPoints(this IFloat64Tuple2D point, IEnumerable<IFloat64Tuple2D> pointsList)
        {
            return pointsList.Select(p => point.GetDistanceToPoint(p));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetDistancesSquaredToPoints(this IFloat64Tuple2D point, IEnumerable<IFloat64Tuple2D> pointsList)
        {
            return pointsList.Select(p => point.GetDistanceSquaredToPoint(p));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetDirectionToPoint(this IFloat64Tuple2D p1, IFloat64Tuple2D p2)
        {
            return new Float64Tuple2D(
                p2.X - p1.X,
                p2.Y - p1.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetDirectionToPoint(this IFloat64Tuple2D p1, double p2X, double p2Y)
        {
            return new Float64Tuple2D(
                p2X - p1.X,
                p2Y - p1.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetDirectionFromPoint(this IFloat64Tuple2D p2, IFloat64Tuple2D p1)
        {
            return new Float64Tuple2D(
                p2.X - p1.X,
                p2.Y - p1.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetDirectionFromPoint(this IFloat64Tuple2D p2, double p1X, double p1Y)
        {
            return new Float64Tuple2D(
                p2.X - p1X,
                p2.Y - p1Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetUnitDirectionToPoint(this IFloat64Tuple2D p1, IFloat64Tuple2D p2)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            var dInv = 1 / Math.Sqrt(dx * dx + dy * dy);

            return new Float64Tuple2D(dx * dInv, dy * dInv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetUnitDirectionFromPoint(this IFloat64Tuple2D p2, IFloat64Tuple2D p1)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            var dInv = 1 / Math.Sqrt(dx * dx + dy * dy);

            return new Float64Tuple2D(dx * dInv, dy * dInv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetPointInDirection(this IFloat64Tuple2D p, IFloat64Tuple2D v)
        {
            return new Float64Tuple2D(
                p.X + v.X,
                p.Y + v.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetPointInDirection(this IFloat64Tuple2D p, IFloat64Tuple2D v, double t)
        {
            return new Float64Tuple2D(
                p.X + t * v.X,
                p.Y + t * v.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetDirectionTo(this IFloat64Tuple3D p1, IFloat64Tuple3D p2)
        {
            return new Float64Tuple3D(
                p2.X - p1.X,
                p2.Y - p1.Y,
                p2.Z - p1.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetDirectionFrom(this IFloat64Tuple3D p2, double p1X, double p1Y, double p1Z)
        {
            return new Float64Tuple3D(
                p2.X - p1X,
                p2.Y - p1Y,
                p2.Z - p1Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetDirectionFrom(this IFloat64Tuple2D p2, IFloat64Tuple2D p1)
        {
            return new Float64Tuple2D(
                p2.X - p1.X,
                p2.Y - p1.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetDirectionFrom(this IFloat64Tuple3D p2, IFloat64Tuple3D p1)
        {
            return new Float64Tuple3D(
                p2.X - p1.X,
                p2.Y - p1.Y,
                p2.Z - p1.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetUnitDirectionTo(this IFloat64Tuple3D p1, IFloat64Tuple3D p2)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            var dz = p2.Z - p1.Z;
            var dInv = 1 / Math.Sqrt(dx * dx + dy * dy + dz * dz);

            return new Float64Tuple3D(dx * dInv, dy * dInv, dz * dInv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetUnitDirectionFrom(this IFloat64Tuple3D p2, IFloat64Tuple3D p1)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            var dz = p2.Z - p1.Z;
            var dInv = 1 / Math.Sqrt(dx * dx + dy * dy + dz * dz);

            return new Float64Tuple3D(dx * dInv, dy * dInv, dz * dInv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetPointInDirection(this IFloat64Tuple3D p, IFloat64Tuple3D v)
        {
            return new Float64Tuple3D(
                p.X + v.X,
                p.Y + v.Y,
                p.Z + v.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetPointInDirection(this IFloat64Tuple3D p, IFloat64Tuple3D v, double t)
        {
            return new Float64Tuple3D(
                p.X + t * v.X,
                p.Y + t * v.Y,
                p.Z + t * v.Z
            );
        }

        /// <summary>
        /// The Euclidean length of this tuple when it represents a vector
        /// </summary>
        public static double GetVectorNorm(this IFloat64Tuple2D vector)
        {
            return Math.Sqrt(
                vector.X * vector.X +
                vector.Y * vector.Y
            );
        }

        public static double GetLength(double vectorX, double vectorY)
        {
            return Math.Sqrt(
                vectorX * vectorX +
                vectorY * vectorY
            );
        }

        public static double GetLength(this IComplexTuple2D vector)
        {
            return Math.Sqrt(
                (vector.X * vector.X.Conjugate()).Real +
                (vector.Y * vector.Y.Conjugate()).Real
            );
        }

        public static double GetLength(this IComplexTuple3D vector)
        {
            return Math.Sqrt(
                (vector.X * vector.X.Conjugate()).Real +
                (vector.Y * vector.Y.Conjugate()).Real +
                (vector.Z * vector.Z.Conjugate()).Real
            );
        }

        /// <summary>
        /// The Euclidean squared length of this tuple when it represents a vector
        /// </summary>
        public static double GetVectorNormSquared(this IFloat64Tuple2D vector)
        {
            return vector.X * vector.X +
                   vector.Y * vector.Y;
        }

        public static double GetLengthSquared(double vectorX, double vectorY)
        {
            return vectorX * vectorX +
                   vectorY * vectorY;
        }

        public static double GetVectorNormSquared(this IComplexTuple2D vector)
        {
            return (vector.X * vector.X.Conjugate()).Real +
                   (vector.Y * vector.Y.Conjugate()).Real;
        }

        public static double GetVectorNormSquared(this IComplexTuple3D vector)
        {
            return (vector.X * vector.X.Conjugate()).Real +
                   (vector.Y * vector.Y.Conjugate()).Real +
                   (vector.Z * vector.Z.Conjugate()).Real;
        }

        /// <summary>
        /// The Euclidean squared length of this tuple when it represents a vector
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNorm(this IFloat64Tuple3D vector)
        {
            return Math.Sqrt(
                vector.X * vector.X +
                vector.Y * vector.Y +
                vector.Z * vector.Z
            );
        }

        /// <summary>
        /// The Euclidean squared length of this tuple when it represents a vector
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<Float64Tuple3D, double> GetUnitVectorNormTuple(this IFloat64Tuple3D vector)
        {
            var length = Math.Sqrt(
                vector.X * vector.X +
                vector.Y * vector.Y +
                vector.Z * vector.Z
            );

            if (length == 0d)
                return new Tuple<Float64Tuple3D, double>(vector.ToTuple3D(), length);

            var s = 1d / length;
            var unitVector = new Float64Tuple3D(
                vector.X * s,
                vector.Y * s,
                vector.Z * s
            );

            return new Tuple<Float64Tuple3D, double>(unitVector, length);
        }

        /// <summary>
        /// The Euclidean squared length of this tuple when it represents a vector
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNormSquared(this IFloat64Tuple3D vector)
        {
            return vector.X * vector.X +
                   vector.Y * vector.Y +
                   vector.Z * vector.Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNorm(double vectorX, double vectorY, double vectorZ)
        {
            return Math.Sqrt(
                vectorX * vectorX +
                vectorY * vectorY +
                vectorZ * vectorZ
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNormSquared(double vectorX, double vectorY, double vectorZ)
        {
            return vectorX * vectorX +
                   vectorY * vectorY +
                   vectorZ * vectorZ;
        }

        /// <summary>
        /// The Euclidean squared length of this tuple when it represents a vector
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNorm(this IFloat64Tuple4D vector)
        {
            return Math.Sqrt(
                vector.X * vector.X +
                vector.Y * vector.Y +
                vector.Z * vector.Z +
                vector.W * vector.W
            );
        }

        /// <summary>
        /// The Euclidean squared length of this tuple when it represents a vector
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNormSquared(this IFloat64Tuple4D vector)
        {
            return vector.X * vector.X +
                   vector.Y * vector.Y +
                   vector.Z * vector.Z +
                   vector.W * vector.W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNorm(double vectorX, double vectorY, double vectorZ, double vectorW)
        {
            return Math.Sqrt(
                vectorX * vectorX +
                vectorY * vectorY +
                vectorZ * vectorZ +
                vectorW * vectorW
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorNormSquared(double vectorX, double vectorY, double vectorZ, double vectorW)
        {
            return vectorX * vectorX +
                   vectorY * vectorY +
                   vectorZ * vectorZ +
                   vectorW * vectorW;
        }

        /// <summary>
        /// True of the Euclidean squared length of this vector is near unity
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(this IFloat64Tuple2D vector)
        {
            return vector
                .GetVectorNormSquared()
                .IsAlmostEqual(1.0d);
        }

        /// <summary>
        /// True of the Euclidean squared length of this vector is near unity
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(this IFloat64Tuple3D vector)
        {
            return vector
                .GetVectorNormSquared()
                .IsAlmostEqual(1.0d);
        }

        /// <summary>
        /// True of the Euclidean squared length of this vector is near unity
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearUnitVector(this IFloat64Tuple3D vector)
        {
            return vector
                .GetVectorNormSquared()
                .IsNearEqual(1.0d);
        }

        /// <summary>
        /// True of the Euclidean squared length of this vector is near unity
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(this IFloat64Tuple4D vector)
        {
            return vector
                .GetVectorNormSquared()
                .IsAlmostEqual(1.0d);
        }

        /// <summary>
        /// True of the Euclidean squared length of this vector is near zero
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroVector(this IFloat64Tuple2D vector)
        {
            return vector
                .GetVectorNormSquared()
                .IsAlmostZero();
        }

        /// <summary>
        /// True of the Euclidean squared length of this vector is near zero
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsExactZeroVector(this IFloat64Tuple3D vector)
        {
            return vector.GetVectorNormSquared().IsZero();
        }

        /// <summary>
        /// True of the Euclidean squared length of this vector is near zero
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostZeroVector(this IFloat64Tuple3D vector)
        {
            return vector
                .GetVectorNormSquared()
                .IsAlmostZero();
        }

        /// <summary>
        /// True of the Euclidean squared length of this vector is near zero
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostZeroVector(this IFloat64Tuple4D vector)
        {
            return vector
                .GetVectorNormSquared()
                .IsAlmostZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostVector(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
        {
            return vector1.X.IsAlmostEqual(vector2.X) &&
                   vector1.Y.IsAlmostEqual(vector2.Y) &&
                   vector1.Z.IsAlmostEqual(vector2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearVector(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
        {
            return vector1.X.IsNearEqual(vector2.X) &&
                   vector1.Y.IsNearEqual(vector2.Y) &&
                   vector1.Z.IsNearEqual(vector2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostVectorNegative(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
        {
            return vector1.X.IsAlmostEqual(-vector2.X) &&
                   vector1.Y.IsAlmostEqual(-vector2.Y) &&
                   vector1.Z.IsAlmostEqual(-vector2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearVectorNegative(this IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
        {
            return vector1.X.IsNearEqual(-vector2.X) &&
                   vector1.Y.IsNearEqual(-vector2.Y) &&
                   vector1.Z.IsNearEqual(-vector2.Z);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(IFloat64Tuple3D v1, IFloat64Tuple3D v2, IFloat64Tuple3D v3)
        {
            return v1.X * (v2.Y * v3.Z - v2.Z * v3.Y) +
                   v1.Y * (v2.Z * v3.X - v2.X * v3.Z) +
                   v1.Z * (v2.X * v3.Y - v2.Y * v3.X);
        }


        /// <summary>
        /// The Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorDot(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        /// <summary>
        /// The Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int VectorDot(this IntTuple2D v1, IntTuple2D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        /// <summary>
        /// The Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorDot(this Axis3D v1, IFloat64Tuple3D v2)
        {
            return v1 switch
            {
                Axis3D.PositiveX => v2.X,
                Axis3D.PositiveY => v2.Y,
                Axis3D.PositiveZ => v2.Z,
                Axis3D.NegativeX => -v2.X,
                Axis3D.NegativeY => -v2.Y,
                _ => -v2.Z,
            };
        }

        /// <summary>
        /// The Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorDot(this IFloat64Tuple3D v1, Axis3D v2)
        {
            return v2 switch
            {
                Axis3D.PositiveX => v1.X,
                Axis3D.PositiveY => v1.Y,
                Axis3D.PositiveZ => v1.Z,
                Axis3D.NegativeX => -v1.X,
                Axis3D.NegativeY => -v1.Y,
                _ => -v1.Z,
            };
        }

        /// <summary>
        /// The Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorDot(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        /// <summary>
        /// The Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int VectorDot(this IntTuple3D v1, IntTuple3D v2)
        {
            return v1.ItemX * v2.ItemX + v1.ItemY * v2.ItemY + v1.ItemZ * v2.ItemZ;
        }

        /// <summary>
        /// The Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorDot(this IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex VectorDot(this IComplexTuple2D v1, IComplexTuple2D v2)
        {
            return v1.X * v2.X.Conjugate() +
                   v1.Y * v2.Y.Conjugate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex VectorDot(this IComplexTuple3D v1, IComplexTuple3D v2)
        {
            return v1.X * v2.X.Conjugate() +
                   v1.Y * v2.Y.Conjugate() +
                   v1.Z * v2.Z.Conjugate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex VectorDot(this IComplexTuple3D v1, IFloat64Tuple3D v2)
        {
            return v1.X * v2.X +
                   v1.Y * v2.Y +
                   v1.Z * v2.Z;
        }


        /// <summary>
        /// The absolute value of the Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorAbsDot(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            return Math.Abs(v1.X * v2.X + v1.Y * v2.Y);
        }

        /// <summary>
        /// The absolute value of the Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int VectorAbsDot(this IntTuple2D v1, IntTuple2D v2)
        {
            return Math.Abs(v1.X * v2.X + v1.Y * v2.Y);
        }

        /// <summary>
        /// The absolute value of the Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorAbsDot(this Float64Tuple3D v1, Float64Tuple3D v2)
        {
            return Math.Abs(v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
        }

        /// <summary>
        /// The absolute value of the Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int VectorAbsDot(this IntTuple3D v1, IntTuple3D v2)
        {
            return Math.Abs(v1.ItemX * v2.ItemX + v1.ItemY * v2.ItemY + v1.ItemZ * v2.ItemZ);
        }

        /// <summary>
        /// The absolute value of the Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorAbsDot(this Float64Tuple4D v1, Float64Tuple4D v2)
        {
            return Math.Abs(v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D AnalyzeOnVectors(this IFloat64Tuple2D v, IFloat64Tuple2D u1, IFloat64Tuple2D u2)
        {
            var s1 = (v.X * u1.X + v.Y * u1.Y) /
                     Math.Sqrt(u1.X * u1.X + u1.Y * u1.Y);

            var s2 = (v.X * u2.X + v.Y * u2.Y) /
                    Math.Sqrt(u2.X * u2.X + u2.Y * u2.Y);

            return new Float64Tuple2D(s1, s2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ProjectOnVector(this IFloat64Tuple2D v, IFloat64Tuple2D u)
        {
            var s1 = v.X * u.X + v.Y * u.Y;
            var s2 = u.X * u.X + u.Y * u.Y;
            var s = s1 / s2;

            return new Float64Tuple2D(u.X * s, u.Y * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ProjectOnUnitVector(this IFloat64Tuple2D v, IFloat64Tuple2D u)
        {
            var s = v.X * u.X + v.Y * u.Y;

            return new Float64Tuple2D(u.X * s, u.Y * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ProjectOnVector(this IFloat64Tuple3D v, IFloat64Tuple3D u)
        {
            var s1 = v.X * u.X + v.Y * u.Y + v.Z * u.Z;
            var s2 = u.X * u.X + u.Y * u.Y + u.Z * u.Z;
            var s = s1 / s2;

            return new Float64Tuple3D(
                u.X * s,
                u.Y * s,
                u.Z * s
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D RejectOnVector(this IFloat64Tuple3D v, IFloat64Tuple3D u)
        {
            var s1 = v.X * u.X + v.Y * u.Y + v.Z * u.Z;
            var s2 = u.X * u.X + u.Y * u.Y + u.Z * u.Z;
            var s = s1 / s2;

            return new Float64Tuple3D(
                v.X - u.X * s,
                v.Y - u.Y * s,
                v.Z - u.Z * s
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ProjectOnUnitVector(this IFloat64Tuple3D v, IFloat64Tuple3D u)
        {
            var s = v.X * u.X + v.Y * u.Y + v.Z * u.Z;

            return new Float64Tuple3D(
                u.X * s,
                u.Y * s,
                u.Z * s
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D RejectOnUnitVector(this IFloat64Tuple3D v, IFloat64Tuple3D u)
        {
            var s = v.X * u.X + v.Y * u.Y + v.Z * u.Z;

            return new Float64Tuple3D(
                v.X - u.X * s,
                v.Y - u.Y * s,
                v.Z - u.Z * s
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ProjectOnVector(this IFloat64Tuple4D v, IFloat64Tuple4D u)
        {
            var s1 = v.X * u.X + v.Y * u.Y + v.Z * u.Z + v.W * u.W;
            var s2 = u.X * u.X + u.Y * u.Y + u.Z * u.Z + u.W * u.W;
            var s = s1 / s2;

            return new Float64Tuple4D(
                u.X * s,
                u.Y * s,
                u.Z * s,
                u.W * s
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ProjectOnUnitVector(this IFloat64Tuple4D v, IFloat64Tuple4D u)
        {
            var s = v.X * u.X + v.Y * u.Y + v.Z * u.Z + v.W * u.W;

            return new Float64Tuple4D(
                u.X * s,
                u.Y * s,
                u.Z * s,
                u.W * s
            );
        }


        /// <summary>
        /// Returns a copy of this vector if its dot product with the other vector is positive, else
        /// it returns the vector's negative
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="directionVector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D FaceDirection(this IFloat64Tuple2D vector, IFloat64Tuple2D directionVector)
        {
            Debug.Assert(!directionVector.IsZeroVector());

            return
                (vector.X * directionVector.X + vector.Y * directionVector.Y).IsDefiniteNegative()
                    ? new Float64Tuple2D(-vector.X, -vector.Y)
                    : new Float64Tuple2D(vector.X, vector.Y);
        }

        /// <summary>
        /// Returns a copy of this vector if its dot product with the other vector is positive, else
        /// it returns the vector's negative
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="directionVector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D FaceDirection(this IFloat64Tuple3D vector, IFloat64Tuple3D directionVector)
        {
            Debug.Assert(!directionVector.IsAlmostZeroVector());

            return
                (vector.X * directionVector.X + vector.Y * directionVector.Y + vector.Z * directionVector.Z).IsDefiniteNegative()
                    ? new Float64Tuple3D(-vector.X, -vector.Y, -vector.Z)
                    : new Float64Tuple3D(vector.X, vector.Y, vector.Z);
        }


        /// <summary>
        /// Returns a new vector orthogonal to this one.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetNormal(this IFloat64Tuple2D vector)
        {
            return new Float64Tuple2D(-vector.Y, vector.X);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D GetUnitNormal(this IFloat64Tuple2D vector)
        {
            return vector.GetNormal().ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Axis3D GetNormal(this Axis3D vector)
        {
            return vector switch
            {
                Axis3D.PositiveX => Axis3D.PositiveY,
                Axis3D.PositiveY => Axis3D.PositiveZ,
                Axis3D.PositiveZ => Axis3D.PositiveX,
                Axis3D.NegativeX => Axis3D.NegativeY,
                Axis3D.NegativeY => Axis3D.NegativeZ,
                _ => Axis3D.NegativeX
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Axis3D GetUnitNormal(this Axis3D vector)
        {
            return vector switch
            {
                Axis3D.PositiveX => Axis3D.PositiveY,
                Axis3D.PositiveY => Axis3D.PositiveZ,
                Axis3D.PositiveZ => Axis3D.PositiveX,
                Axis3D.NegativeX => Axis3D.NegativeY,
                Axis3D.NegativeY => Axis3D.NegativeZ,
                _ => Axis3D.NegativeX
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetNormal(this IFloat64Tuple3D vector)
        {
            var x = vector.X;
            var y = vector.Y;
            var z = vector.Z;

            if (x == 0)
                return new Float64Tuple3D(0, -z, y);

            if (y == 0)
                return new Float64Tuple3D(-z, 0, x);

            if (z == 0)
                return new Float64Tuple3D(-y, x, 0);

            var minComponentIndex =
                vector.GetMinAbsComponentIndex();

            return minComponentIndex switch
            {
                0 => new Float64Tuple3D(-(y + z), x, x),
                1 => new Float64Tuple3D(y, -(x + z), y),
                _ => new Float64Tuple3D(z, z, -(x + y))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetUnitNormal(this IFloat64Tuple3D vector)
        {
            return vector.GetNormal().ToUnitVector();
        }

        /// <summary>
        /// Returns a new vector orthogonal to this one.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple2D GetNormal(this IntTuple2D vector)
        {
            return new IntTuple2D(-vector.Y, vector.X);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearParallelTo(this IFloat64Tuple2D v1, IFloat64Tuple2D v2, double epsilon = 1e-12d)
        {
            return (v1.X * v2.Y - v1.Y * v2.X).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearNormalTo(this IFloat64Tuple2D v1, IFloat64Tuple2D v2, double epsilon = 1e-12d)
        {
            return (v1.X * v2.X + v1.Y * v2.Y).IsNearZero(epsilon);
        }


        /// <summary>
        /// The Euclidean cross product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D VectorCross(this Axis3D v1, IFloat64Tuple3D v2)
        {
            return v1 switch
            {
                Axis3D.PositiveX => new Float64Tuple3D(0, -v2.Z, v2.Y),
                Axis3D.PositiveY => new Float64Tuple3D(v2.Z, 0, -v2.X),
                Axis3D.PositiveZ => new Float64Tuple3D(-v2.Y, v2.X, 0),
                Axis3D.NegativeX => new Float64Tuple3D(0, v2.Z, -v2.Y),
                Axis3D.NegativeY => new Float64Tuple3D(-v2.Z, 0, v2.X),
                _ => new Float64Tuple3D(v2.Y, -v2.X, 0)
            };
        }

        /// <summary>
        /// The Euclidean cross product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D VectorCross(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return new Float64Tuple3D(
                v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorCrossNorm(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            var x = v1.Y * v2.Z - v1.Z * v2.Y;
            var y = v1.Z * v2.X - v1.X * v2.Z;
            var z = v1.X * v2.Y - v1.Y * v2.X;

            return Math.Sqrt(x * x + y * y + z * z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorCrossNormSquared(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            var x = v1.Y * v2.Z - v1.Z * v2.Y;
            var y = v1.Z * v2.X - v1.X * v2.Z;
            var z = v1.X * v2.Y - v1.Y * v2.X;

            return x * x + y * y + z * z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearParallelTo(this IFloat64Tuple3D v1, IFloat64Tuple3D v2, double epsilon = 1e-12d)
        {
            var x = v1.Y * v2.Z - v1.Z * v2.Y;
            var y = v1.Z * v2.X - v1.X * v2.Z;
            var z = v1.X * v2.Y - v1.Y * v2.X;

            return (x * x + y * y + z * z).IsNearZero(epsilon);
        }


        /// <summary>
        /// The Euclidean cross product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntTuple3D VectorCross(this IntTuple3D v1, IntTuple3D v2)
        {
            return new IntTuple3D(
                v1.ItemY * v2.ItemZ - v1.ItemZ * v2.ItemY,
                v1.ItemZ * v2.ItemX - v1.ItemX * v2.ItemZ,
                v1.ItemX * v2.ItemY - v1.ItemY * v2.ItemX
            );
        }


        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// Both vectors are assumed to have z=0 components
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D VectorUnitCrossXy(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            var vz = v1.X * v2.Y - v1.Y * v2.X;

            return new Float64Tuple3D(
                0,
                0,
                vz < 0 ? -1 : vz > 0 ? 1 : 0
            );
        }

        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// The first vector is assumed to have z=0 while the second x=0 and y=0
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2Z"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D VectorUnitCrossXy_Z(this IFloat64Tuple2D v1, double v2Z)
        {
            var vx = v1.Y * v2Z;
            var vy = -v1.X * v2Z;

            var s = 1.0d / Math.Sqrt(vx * vx + vy * vy);

            return new Float64Tuple3D(vx * s, vy * s, 0);
        }

        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D VectorUnitCross(this Axis3D v1, IFloat64Tuple3D v2)
        {
            return v1.VectorCross(v2).ToUnitVector();
        }

        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D VectorUnitCross(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            var vx = v1.Y * v2.Z - v1.Z * v2.Y;
            var vy = v1.Z * v2.X - v1.X * v2.Z;
            var vz = v1.X * v2.Y - v1.Y * v2.X;

            var s = 1.0d / Math.Sqrt(vx * vx + vy * vy + vz * vz);

            return double.IsInfinity(s)
                ? Float64Tuple3D.Zero
                : new Float64Tuple3D(vx * s, vy * s, vz * s);
        }

        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2X"></param>
        /// <param name="v2Y"></param>
        /// <param name="v2Z"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D VectorUnitCross(this IFloat64Tuple3D v1, double v2X, double v2Y, double v2Z)
        {
            var vx = v1.Y * v2Z - v1.Z * v2Y;
            var vy = v1.Z * v2X - v1.X * v2Z;
            var vz = v1.X * v2Y - v1.Y * v2X;

            var s = 1.0d / Math.Sqrt(vx * vx + vy * vy + vz * vz);

            return new Float64Tuple3D(vx * s, vy * s, vz * s);
        }

        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// The second vector is assumed to have z=0
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2X"></param>
        /// <param name="v2Y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D VectorUnitCrossXy(this IFloat64Tuple3D v1, double v2X, double v2Y)
        {
            var vx = -v1.Z * v2Y;
            var vy = v1.Z * v2X;
            var vz = v1.X * v2Y - v1.Y * v2X;

            var s = 1.0d / Math.Sqrt(vx * vx + vy * vy + vz * vz);

            return new Float64Tuple3D(vx * s, vy * s, vz * s);
        }

        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// The second vector is assumed to have z=0
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D VectorUnitCrossXy(this IFloat64Tuple3D v1, IFloat64Tuple2D v2)
        {
            var vx = -v1.Z * v2.Y;
            var vy = v1.Z * v2.X;
            var vz = v1.X * v2.Y - v1.Y * v2.X;

            var s = 1.0d / Math.Sqrt(vx * vx + vy * vy + vz * vz);

            return new Float64Tuple3D(vx * s, vy * s, vz * s);
        }


        /// <summary>
        /// Returns a unit vector from the given one. If the length of the given vector is near zero
        /// it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ToUnitVector(this IFloat64Tuple2D vector)
        {
            var s = vector.GetVectorNorm();
            if (s.IsAlmostZero())
                return vector.ToTuple2D();

            s = 1.0d / s;
            return new Float64Tuple2D(vector.X * s, vector.Y * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ToUnitVector(double vectorX, double vectorY)
        {
            var s = GetLength(vectorX, vectorY);
            if (s.IsAlmostZero())
                return new Float64Tuple2D(vectorX, vectorY);

            s = 1.0d / s;
            return new Float64Tuple2D(vectorX * s, vectorY * s);
        }

        /// <summary>
        /// Returns a unit vector from the given one. If the length of the given vector is near zero
        /// it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ToUnitVector(this IFloat64Tuple3D vector)
        {
            var s = vector.GetVectorNorm();
            if (s.IsAlmostZero())
                return Float64Tuple3D.Zero;

            s = 1.0d / s;
            return new Float64Tuple3D(vector.X * s, vector.Y * s, vector.Z * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ToUnitVector(double vectorX, double vectorY, double vectorZ)
        {
            var s = GetVectorNorm(vectorX, vectorY, vectorZ);
            if (s.IsAlmostZero())
                return new Float64Tuple3D(vectorX, vectorY, vectorZ);

            s = 1.0d / s;
            return new Float64Tuple3D(vectorX * s, vectorY * s, vectorZ * s);
        }

        /// <summary>
        /// Returns a unit vector from the given one. If the length of the given vector is near zero
        /// it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToUnitVector(this IFloat64Tuple4D vector)
        {
            var s = vector.GetVectorNorm();
            if (s.IsAlmostZero())
                return vector.ToTuple4D();

            s = 1.0d / s;
            return new Float64Tuple4D(vector.X * s, vector.Y * s, vector.Z * s, vector.W * s);
        }

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ToNegativeVector(this IFloat64Tuple2D vector)
        {
            return new Float64Tuple2D(-vector.X, -vector.Y);
        }

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ToNegativeUnitVector(this IFloat64Tuple2D vector)
        {
            var s = vector.GetVectorNorm();
            if (s.IsAlmostZero())
                return vector.ToTuple2D();

            s = -1.0d / s;
            return new Float64Tuple2D(vector.X * s, vector.Y * s);
        }

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ToNegativeVector(this IFloat64Tuple3D vector)
        {
            return new Float64Tuple3D(-vector.X, -vector.Y, -vector.Z);
        }

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ToNegativeUnitVector(this IFloat64Tuple3D vector)
        {
            var s = vector.GetVectorNorm();
            if (s.IsAlmostZero())
                return vector.ToTuple3D();

            s = 1.0d / s;
            return new Float64Tuple3D(vector.X * s, vector.Y * s, vector.Z * s);
        }

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToNegativeVector(this IFloat64Tuple4D vector)
        {
            return new Float64Tuple4D(-vector.X, -vector.Y, -vector.Z, -vector.W);
        }

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToNegativeUnitVector(this IFloat64Tuple4D vector)
        {
            var s = vector.GetVectorNorm();
            if (s.IsAlmostZero())
                return vector.ToTuple4D();

            s = 1.0d / s;
            return new Float64Tuple4D(vector.X * s, vector.Y * s, vector.Z * s, vector.W * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, Float64Tuple2D> ToLengthAndUnitDirection(this IFloat64Tuple2D vector)
        {
            var length = Math.Sqrt(
                vector.X * vector.X +
                vector.Y * vector.Y
            );

            var lengthInv = 1 / length;

            return Tuple.Create(
                length,
                new Float64Tuple2D(
                    vector.X * lengthInv,
                    vector.Y * lengthInv
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, Float64Tuple3D> ToLengthAndUnitDirection(this IFloat64Tuple3D vector)
        {
            var length = Math.Sqrt(
                vector.X * vector.X +
                vector.Y * vector.Y +
                vector.Z * vector.Z
            );

            var lengthInv = 1 / length;

            return Tuple.Create(
                length,
                new Float64Tuple3D(
                    vector.X * lengthInv,
                    vector.Y * lengthInv,
                    vector.Z * lengthInv
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D SetLength(this IFloat64Tuple3D vector, double newLength)
        {
            var s = vector.GetVectorNorm();
            if (s.IsAlmostZero())
                return Float64Tuple3D.Zero;

            s = newLength / s;
            return new Float64Tuple3D(vector.X * s, vector.Y * s, vector.Z * s);
        }


        /// <summary>
        /// The Euclidean distance between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceToPoint(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            var vX = v2.X - v1.X;
            var vY = v2.Y - v1.Y;

            return Math.Sqrt(vX * vX + vY * vY);
        }

        /// <summary>
        /// The Euclidean distance between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2X"></param>
        /// <param name="v2Y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceToPoint(this IFloat64Tuple2D v1, double v2X, double v2Y)
        {
            var vX = v2X - v1.X;
            var vY = v2Y - v1.Y;

            return Math.Sqrt(vX * vX + vY * vY);
        }

        /// <summary>
        /// The Euclidean distance between the given vectors
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceToPoint(this IFloat64Tuple3D p1, IFloat64Tuple3D p2)
        {
            var vX = p2.X - p1.X;
            var vY = p2.Y - p1.Y;
            var vZ = p2.Z - p1.Z;

            return Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceToPoint(this IFloat64Tuple3D p1, double p2X, double p2Y, double p2Z)
        {
            var vX = p2X - p1.X;
            var vY = p2Y - p1.Y;
            var vZ = p2Z - p1.Z;

            return Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
        }

        /// <summary>
        /// The Euclidean distance between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceToPoint(this IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            var vX = v2.X - v1.X;
            var vY = v2.Y - v1.Y;
            var vZ = v2.Z - v1.Z;
            var vW = v2.W - v1.W;

            return Math.Sqrt(vX * vX + vY * vY + vZ * vZ + vW * vW);
        }


        /// <summary>
        /// The Euclidean distance between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceSquaredToPoint(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            var vX = v2.X - v1.X;
            var vY = v2.Y - v1.Y;

            return vX * vX + vY * vY;
        }

        /// <summary>
        /// The Euclidean distance between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2X"></param>
        /// <param name="v2Y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceSquaredToPoint(this IFloat64Tuple2D v1, double v2X, double v2Y)
        {
            var vX = v2X - v1.X;
            var vY = v2Y - v1.Y;

            return vX * vX + vY * vY;
        }

        /// <summary>
        /// The Euclidean distance between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDistanceSquaredToPoint(this IntTuple2D v1, IntTuple2D v2)
        {
            return (v2 - v1).VectorLengthSquared;
        }

        /// <summary>
        /// The Euclidean distance between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceSquaredToPoint(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            var vX = v2.X - v1.X;
            var vY = v2.Y - v1.Y;
            var vZ = v2.Z - v1.Z;

            return vX * vX + vY * vY + vZ * vZ;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceSquaredToPoint(this IFloat64Tuple3D p1, double p2X, double p2Y, double p2Z)
        {
            var vX = p2X - p1.X;
            var vY = p2Y - p1.Y;
            var vZ = p2Z - p1.Z;

            return vX * vX + vY * vY + vZ * vZ;
        }

        /// <summary>
        /// The Euclidean distance between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDistanceSquaredToPoint(this IntTuple3D v1, IntTuple3D v2)
        {
            return (v2 - v1).VectorLengthSquared;
        }

        /// <summary>
        /// The Euclidean distance between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistanceSquaredToPoint(this IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            var vX = v2.X - v1.X;
            var vY = v2.Y - v1.Y;
            var vZ = v2.Z - v1.Z;
            var vW = v2.W - v1.W;

            return vX * vX + vY * vY + vZ * vZ + vW * vW;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ReflectVectorOnVector(this IFloat64Tuple2D reflectionVector, IFloat64Tuple2D vector)
        {
            var s = 2 * reflectionVector.VectorDot(vector) / reflectionVector.GetVectorNormSquared();

            return new Float64Tuple2D(
                vector.X - s * reflectionVector.X,
                vector.Y - s * reflectionVector.Y
            );
        }

        public static IEnumerable<Float64Tuple2D> ReflectVectorsOnVector(this IFloat64Tuple2D reflectionVector, params IFloat64Tuple2D[] vectorsList)
        {
            var s = 2 / reflectionVector.GetVectorNormSquared();

            foreach (var vector in vectorsList)
            {
                var s1 = s * reflectionVector.VectorDot(vector);

                yield return new Float64Tuple2D(
                    vector.X - s1 * reflectionVector.X,
                    vector.Y - s1 * reflectionVector.Y
                );
            }
        }

        public static IEnumerable<Float64Tuple2D> ReflectVectorsOnVector(this IFloat64Tuple2D reflectionVector, IEnumerable<IFloat64Tuple2D> vectorsList)
        {
            var s = 2 / reflectionVector.GetVectorNormSquared();

            foreach (var vector in vectorsList)
            {
                var s1 = s * reflectionVector.VectorDot(vector);

                yield return new Float64Tuple2D(
                    vector.X - s1 * reflectionVector.X,
                    vector.Y - s1 * reflectionVector.Y
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ReflectVectorOnVector(this IFloat64Tuple3D reflectionVector, IFloat64Tuple3D vector)
        {
            var s = 2 * reflectionVector.VectorDot(vector) / reflectionVector.GetVectorNormSquared();

            return new Float64Tuple3D(
                vector.X - s * reflectionVector.X,
                vector.Y - s * reflectionVector.Y,
                vector.Z - s * reflectionVector.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<Float64Tuple3D> ReflectVectorsOnVector(this IFloat64Tuple3D reflectionVector, Triplet<IFloat64Tuple3D> vectorsTriplet)
        {
            var (v1, v2, v3) = vectorsTriplet;

            var s = 2 / reflectionVector.GetVectorNormSquared();

            var s1 = s * reflectionVector.VectorDot(v1);
            var s2 = s * reflectionVector.VectorDot(v2);
            var s3 = s * reflectionVector.VectorDot(v3);

            var rv1 = new Float64Tuple3D(
                v1.X - s1 * reflectionVector.X,
                v1.Y - s1 * reflectionVector.Y,
                v1.Z - s1 * reflectionVector.Z
            );

            var rv2 = new Float64Tuple3D(
                v2.X - s2 * reflectionVector.X,
                v2.Y - s2 * reflectionVector.Y,
                v2.Z - s2 * reflectionVector.Z
            );

            var rv3 = new Float64Tuple3D(
                v3.X - s3 * reflectionVector.X,
                v3.Y - s3 * reflectionVector.Y,
                v3.Z - s3 * reflectionVector.Z
            );

            return new Triplet<Float64Tuple3D>(rv1, rv2, rv3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<Float64Tuple3D> ReflectVectorsOnVector(this IFloat64Tuple3D reflectionVector, IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            var s = 2 / reflectionVector.GetVectorNormSquared();

            var s1 = s * reflectionVector.VectorDot(v1);
            var s2 = s * reflectionVector.VectorDot(v2);

            var rv1 = new Float64Tuple3D(
                v1.X - s1 * reflectionVector.X,
                v1.Y - s1 * reflectionVector.Y,
                v1.Z - s1 * reflectionVector.Z
            );

            var rv2 = new Float64Tuple3D(
                v2.X - s2 * reflectionVector.X,
                v2.Y - s2 * reflectionVector.Y,
                v2.Z - s2 * reflectionVector.Z
            );

            return new Pair<Float64Tuple3D>(rv1, rv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<Float64Tuple3D> ReflectVectorsOnVector(this IFloat64Tuple3D reflectionVector, IFloat64Tuple3D v1, IFloat64Tuple3D v2, IFloat64Tuple3D v3)
        {
            var s = 2 / reflectionVector.GetVectorNormSquared();

            var s1 = s * reflectionVector.VectorDot(v1);
            var s2 = s * reflectionVector.VectorDot(v2);
            var s3 = s * reflectionVector.VectorDot(v3);

            var rv1 = new Float64Tuple3D(
                v1.X - s1 * reflectionVector.X,
                v1.Y - s1 * reflectionVector.Y,
                v1.Z - s1 * reflectionVector.Z
            );

            var rv2 = new Float64Tuple3D(
                v2.X - s2 * reflectionVector.X,
                v2.Y - s2 * reflectionVector.Y,
                v2.Z - s2 * reflectionVector.Z
            );

            var rv3 = new Float64Tuple3D(
                v3.X - s3 * reflectionVector.X,
                v3.Y - s3 * reflectionVector.Y,
                v3.Z - s3 * reflectionVector.Z
            );

            return new Triplet<Float64Tuple3D>(rv1, rv2, rv3);
        }

        public static IEnumerable<Float64Tuple3D> ReflectVectorsOnVector(this IFloat64Tuple3D reflectionVector, params IFloat64Tuple3D[] vectorsList)
        {
            var s = 2 / reflectionVector.GetVectorNormSquared();

            foreach (var vector in vectorsList)
            {
                var s1 = s * reflectionVector.VectorDot(vector);

                yield return new Float64Tuple3D(
                    vector.X - s1 * reflectionVector.X,
                    vector.Y - s1 * reflectionVector.Y,
                    vector.Z - s1 * reflectionVector.Z
                );
            }
        }

        public static IEnumerable<Float64Tuple3D> ReflectVectorsOnVector(this IFloat64Tuple3D reflectionVector, IEnumerable<IFloat64Tuple3D> vectorsList)
        {
            var s = 2 / reflectionVector.GetVectorNormSquared();

            foreach (var vector in vectorsList)
            {
                var s1 = s * reflectionVector.VectorDot(vector);

                yield return new Float64Tuple3D(
                    vector.X - s1 * reflectionVector.X,
                    vector.Y - s1 * reflectionVector.Y,
                    vector.Z - s1 * reflectionVector.Z
                );
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ReflectVectorOnUnitVector(this IFloat64Tuple2D reflectionVector, IFloat64Tuple2D vector)
        {
            var s = 2 * reflectionVector.VectorDot(vector);

            return new Float64Tuple2D(
                vector.X - s * reflectionVector.X,
                vector.Y - s * reflectionVector.Y
            );
        }

        public static IEnumerable<Float64Tuple2D> ReflectVectorsOnUnitVector(this IFloat64Tuple2D reflectionVector, params IFloat64Tuple2D[] vectorsList)
        {
            foreach (var vector in vectorsList)
            {
                var s1 = 2 * reflectionVector.VectorDot(vector);

                yield return new Float64Tuple2D(
                    vector.X - s1 * reflectionVector.X,
                    vector.Y - s1 * reflectionVector.Y
                );
            }
        }

        public static IEnumerable<Float64Tuple2D> ReflectVectorsOnUnitVector(this IFloat64Tuple2D reflectionVector, IEnumerable<IFloat64Tuple2D> vectorsList)
        {
            foreach (var vector in vectorsList)
            {
                var s1 = 2 * reflectionVector.VectorDot(vector);

                yield return new Float64Tuple2D(
                    vector.X - s1 * reflectionVector.X,
                    vector.Y - s1 * reflectionVector.Y
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ReflectVectorOnUnitVector(this IFloat64Tuple3D reflectionVector, IFloat64Tuple3D vector)
        {
            var s = 2 * reflectionVector.VectorDot(vector);

            return new Float64Tuple3D(
                vector.X - s * reflectionVector.X,
                vector.Y - s * reflectionVector.Y,
                vector.Z - s * reflectionVector.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<Float64Tuple3D> ReflectVectorsOnUnitVector(this IFloat64Tuple3D reflectionVector, Triplet<IFloat64Tuple3D> vectorsTriplet)
        {
            var (v1, v2, v3) = vectorsTriplet;

            var s1 = 2 * reflectionVector.VectorDot(v1);
            var s2 = 2 * reflectionVector.VectorDot(v2);
            var s3 = 2 * reflectionVector.VectorDot(v3);

            var rv1 = new Float64Tuple3D(
                v1.X - s1 * reflectionVector.X,
                v1.Y - s1 * reflectionVector.Y,
                v1.Z - s1 * reflectionVector.Z
            );

            var rv2 = new Float64Tuple3D(
                v2.X - s2 * reflectionVector.X,
                v2.Y - s2 * reflectionVector.Y,
                v2.Z - s2 * reflectionVector.Z
            );

            var rv3 = new Float64Tuple3D(
                v3.X - s3 * reflectionVector.X,
                v3.Y - s3 * reflectionVector.Y,
                v3.Z - s3 * reflectionVector.Z
            );

            return new Triplet<Float64Tuple3D>(rv1, rv2, rv3);
        }

        public static IEnumerable<Float64Tuple3D> ReflectVectorsOnUnitVector(this IFloat64Tuple3D reflectionVector, params IFloat64Tuple3D[] vectorsList)
        {
            foreach (var vector in vectorsList)
            {
                var s1 = 2 * reflectionVector.VectorDot(vector);

                yield return new Float64Tuple3D(
                    vector.X - s1 * reflectionVector.X,
                    vector.Y - s1 * reflectionVector.Y,
                    vector.Z - s1 * reflectionVector.Z
                );
            }
        }

        public static IEnumerable<Float64Tuple3D> ReflectVectorsOnUnitVector(this IFloat64Tuple3D reflectionVector, IEnumerable<IFloat64Tuple3D> vectorsList)
        {
            foreach (var vector in vectorsList)
            {
                var s1 = 2 * reflectionVector.VectorDot(vector);

                yield return new Float64Tuple3D(
                    vector.X - s1 * reflectionVector.X,
                    vector.Y - s1 * reflectionVector.Y,
                    vector.Z - s1 * reflectionVector.Z
                );
            }
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D TranslateBy(this IFloat64Tuple2D vector, double translationX, double translationY)
        {
            return new Float64Tuple2D(
                translationX + vector.X,
                translationY + vector.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D TranslateBy(this IFloat64Tuple3D vector, double translationX, double translationY, double translationZ)
        {
            return new Float64Tuple3D(
                translationX + vector.X,
                translationY + vector.Y,
                translationZ + vector.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D TranslateBy(this IFloat64Tuple2D vector, IFloat64Tuple2D translationVector)
        {
            return new Float64Tuple2D(
                translationVector.X + vector.X,
                translationVector.Y + vector.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D TranslateBy(this IFloat64Tuple3D vector, IFloat64Tuple3D translationVector)
        {
            return new Float64Tuple3D(
                translationVector.X + vector.X,
                translationVector.Y + vector.Y,
                translationVector.Z + vector.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ScaleBy(this IFloat64Tuple3D vector, double scaleFactor)
        {
            return new Float64Tuple3D(
                scaleFactor * vector.X,
                scaleFactor * vector.Y,
                scaleFactor * vector.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ScaleBy(this IFloat64Tuple2D vector, double scaleFactor)
        {
            return new Float64Tuple2D(
                scaleFactor * vector.X,
                scaleFactor * vector.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ScaleBy(this IFloat64Tuple2D vector, double scaleFactorX, double scaleFactorY)
        {
            return new Float64Tuple2D(
                scaleFactorX * vector.X,
                scaleFactorY * vector.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ScaleBy(this IFloat64Tuple3D vector, double scaleFactorX, double scaleFactorY, double scaleFactorZ)
        {
            return new Float64Tuple3D(
                scaleFactorX * vector.X,
                scaleFactorY * vector.Y,
                scaleFactorZ * vector.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D ScaleBy(this IFloat64Tuple2D vector, IFloat64Tuple2D scaleFactorVector)
        {
            return new Float64Tuple2D(
                scaleFactorVector.X * vector.X,
                scaleFactorVector.Y * vector.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ScaleBy(this IFloat64Tuple3D vector, IFloat64Tuple3D scaleFactorVector)
        {
            return new Float64Tuple3D(
                scaleFactorVector.X * vector.X,
                scaleFactorVector.Y * vector.Y,
                scaleFactorVector.Z * vector.Z
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D RotateBy(this IFloat64Tuple2D vector, Float64PlanarAngle angle)
        {
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            return new Float64Tuple2D(
                vector.X * cosAngle - vector.Y * sinAngle,
                vector.X * sinAngle + vector.Y * cosAngle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D XRotateBy(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
        {
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            return new Float64Tuple3D(
                vector.X,
                vector.Y * cosAngle - vector.Z * sinAngle,
                vector.Y * sinAngle + vector.Z * cosAngle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D YRotateBy(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
        {
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            return new Float64Tuple3D(
                vector.X * cosAngle + vector.Z * sinAngle,
                vector.Y,
                -vector.X * sinAngle + vector.Z * cosAngle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ZRotateBy(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
        {
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            return new Float64Tuple3D(
                vector.X * cosAngle - vector.Y * sinAngle,
                vector.X * sinAngle + vector.Y * cosAngle,
                vector.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D XRotateByDegrees(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
        {
            return vector.XRotateBy(angle * Math.PI / 180);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D YRotateByDegrees(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
        {
            return vector.YRotateBy(angle * Math.PI / 180);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D ZRotateByDegrees(this IFloat64Tuple3D vector, Float64PlanarAngle angle)
        {
            return vector.ZRotateBy(angle * Math.PI / 180);
        }


        //public static Tuple3D VectorCross(this ITuple3D v1, ITuple3D v2)
        //{
        //    Debug.Assert(!v1.HasNaNComponent && !v2.HasNaNComponent);

        //    return new Tuple3D(
        //        v1.Y * v2.Z - v1.Z * v2.Y,
        //        -v1.X * v2.Z + v1.Z * v2.X,
        //        v1.X * v2.Y - v1.Y * v2.X
        //    );
        //}

        /// <summary>
        /// Find the angle between this vector and another
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle GetVectorsAngle(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
        {
            var t1 = v1.X * v2.X + v1.Y * v2.Y;
            var t2 = v1.X * v1.X + v1.Y * v1.Y;
            var t3 = v2.X * v2.X + v2.Y * v2.Y;

            var cosAngle = t1 / Math.Sqrt(t2 * t3);

            return Math.Acos(cosAngle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetTriangleNormal(IFloat64Tuple3D p1, IFloat64Tuple3D p2, IFloat64Tuple3D p3)
        {
            //TODO: Test this for numerical stability, maybe select two sides with largest lengths
            var v12 = new Float64Tuple3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            var v23 = new Float64Tuple3D(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

            return v12.VectorCross(v23);

            ////Find vector sides of triangle
            //var v12 = p2 - p1;
            //var v23 = p3 - p2;
            //var v31 = p1 - p3;

            ////Find squared side lengths of triangle
            //var side12 = v12.LengthSquared;
            //var side23 = v23.LengthSquared;
            //var side31 = v31.LengthSquared;

            //double normalX;
            //double normalY;
            //double normalZ;

            ////Find normal to triangle
            //if (side12 < side23)
            //{
            //    if (side12 < side31)
            //        return v23.Cross(v31);

            //    return 
            //}
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetTriangleUnitNormal(IFloat64Tuple3D p1, IFloat64Tuple3D p2, IFloat64Tuple3D p3)
        {
            //TODO: Test this for numerical stability, maybe select two sides with largest lengths
            var v12 = new Float64Tuple3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            var v23 = new Float64Tuple3D(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

            return v12.VectorUnitCross(v23);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetTriangleInverseUnitNormal(IFloat64Tuple3D p1, IFloat64Tuple3D p2, IFloat64Tuple3D p3)
        {
            //TODO: Test this for numerical stability, maybe select two sides with largest lengths
            var v12 = new Float64Tuple3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            var v23 = new Float64Tuple3D(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

            return v12.VectorUnitCross(v23);
        }


        /// <summary>
        /// Find the angle between this vector and another
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetVectorsAngleCos(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            var t1 = v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
            var t2 = v1.X * v1.X + v1.Y * v1.Y + v1.Z * v1.Z;
            var t3 = v2.X * v2.X + v2.Y * v2.Y + v2.Z * v2.Z;

            return (t1 / Math.Sqrt(t2 * t3)).Clamp(-1d, 1d);
        }

        /// <summary>
        /// Find the angle between this vector and another
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle GetVectorsAngle(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return Math.Acos(
                v1.GetVectorsAngleCos(v2)
            );
        }

        /// <summary>
        /// Find the angle between this vector and another
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetUnitVectorsAngleCos(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            Debug.Assert(
                v1.IsNearUnitVector() &&
                v2.IsNearUnitVector()
            );

            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        /// <summary>
        /// Find the angle between this vector and another
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle GetUnitVectorsAngle(this IFloat64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return Math.Acos(
                v1.GetUnitVectorsAngleCos(v2)
            );
        }

        /// <summary>
        /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle GetPointsAngle(this IFloat64Tuple3D p0, IFloat64Tuple3D p1, IFloat64Tuple3D p2)
        {
            return new Float64Tuple3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z).GetVectorsAngle(
                new Float64Tuple3D(p2.X - p0.X, p2.Y - p0.Y, p2.Z - p0.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetCoordinateSystem(this Float64Tuple3D v1, out Float64Tuple3D v2, out Float64Tuple3D v3)
        {
            v2 = Math.Abs(v1.X) > Math.Abs(v1.Y)
                ? new Float64Tuple3D(-v1.Z, 0, v1.X) / Math.Sqrt(v1.X * v1.X + v1.Z * v1.Z)
                : new Float64Tuple3D(0, v1.Z, -v1.Y) / Math.Sqrt(v1.Y * v1.Y + v1.Z * v1.Z);

            v3 = v1.VectorCross(v2);
        }
    }
}