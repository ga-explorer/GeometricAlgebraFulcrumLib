using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicOperations;
using MathNet.Numerics;

namespace EuclideanGeometryLib.BasicMath
{
    public static class VectorAlgebraUtils
    {
        public static Tuple2D GetCenterOfMassPoint(this IEnumerable<ITuple2D> pointsList)
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

            return new Tuple2D(
                centerX * pointsCountInv, 
                centerY * pointsCountInv
            );
        }

        public static Tuple2D GetCenterPoint(this IEnumerable<ITuple2D> pointsList)
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

            return new Tuple2D(
                0.5 * (minX + maxX), 
                0.5 * (minY + maxY)
            );
        }

        public static Tuple3D GetCenterOfMassPoint(this IEnumerable<ITuple3D> pointsList)
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

            return new Tuple3D(
                centerX * pointsCountInv, 
                centerY * pointsCountInv,
                centerZ * pointsCountInv
            );
        }

        public static Tuple3D GetCenterPoint(this IEnumerable<ITuple3D> pointsList)
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

            return new Tuple3D(
                0.5 * (minX + maxX), 
                0.5 * (minY + maxY),
                0.5 * (minZ + maxZ)
            );
        }

        public static IEnumerable<double> GetDistancesToPoints(this ITuple2D point, IEnumerable<ITuple2D> pointsList)
            => pointsList.Select(p => GetDistanceToPoint(point, p));

        public static IEnumerable<double> GetDistancesSquaredToPoints(this ITuple2D point, IEnumerable<ITuple2D> pointsList)
            => pointsList.Select(p => GetDistanceSquaredToPoint(point, p));

        public static Tuple2D GetDirectionToPoint(this ITuple2D p1, ITuple2D p2)
            => new Tuple2D(
                p2.X - p1.X,
                p2.Y - p1.Y
            );

        public static Tuple2D GetDirectionToPoint(this ITuple2D p1, double p2X, double p2Y)
            => new Tuple2D(
                p2X - p1.X,
                p2Y - p1.Y
            );

        public static Tuple2D GetDirectionFromPoint(this ITuple2D p2, ITuple2D p1)
            => new Tuple2D(
                p2.X - p1.X,
                p2.Y - p1.Y
            );

        public static Tuple2D GetDirectionFromPoint(this ITuple2D p2, double p1X, double p1Y)
            => new Tuple2D(
                p2.X - p1X,
                p2.Y - p1Y
            );

        public static Tuple2D GetUnitDirectionToPoint(this ITuple2D p1, ITuple2D p2)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            var dInv = 1 / Math.Sqrt(dx * dx + dy * dy);

            return new Tuple2D(dx * dInv, dy * dInv);
        }

        public static Tuple2D GetUnitDirectionFromPoint(this ITuple2D p2, ITuple2D p1)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            var dInv = 1 / Math.Sqrt(dx * dx + dy * dy);

            return new Tuple2D(dx * dInv, dy * dInv);
        }

        public static Tuple2D GetPointInDirection(this ITuple2D p, ITuple2D v)
            => new Tuple2D(
                p.X + v.X,
                p.Y + v.Y
            );

        public static Tuple2D GetPointInDirection(this ITuple2D p, ITuple2D v, double t)
            => new Tuple2D(
                p.X + t * v.X,
                p.Y + t * v.Y
            );

        public static Tuple3D GetDirectionTo(this ITuple3D p1, ITuple3D p2)
            => new Tuple3D(
                p2.X - p1.X,
                p2.Y - p1.Y,
                p2.Z - p1.Z
            );

        public static Tuple3D GetDirectionFrom(this ITuple3D p2, double p1X, double p1Y, double p1Z)
            => new Tuple3D(
                p2.X - p1X,
                p2.Y - p1Y,
                p2.Z - p1Z
            );

        public static Tuple2D GetDirectionFrom(this ITuple2D p2, ITuple2D p1)
            => new Tuple2D(
                p2.X - p1.X,
                p2.Y - p1.Y
            );

        public static Tuple3D GetDirectionFrom(this ITuple3D p2, ITuple3D p1)
            => new Tuple3D(
                p2.X - p1.X,
                p2.Y - p1.Y,
                p2.Z - p1.Z
            );

        public static Tuple3D GetUnitDirectionTo(this ITuple3D p1, ITuple3D p2)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            var dz = p2.Z - p1.Z;
            var dInv = 1 / Math.Sqrt(dx * dx + dy * dy + dz * dz);

            return new Tuple3D(dx * dInv, dy * dInv, dz * dInv);
        }

        public static Tuple3D GetUnitDirectionFrom(this ITuple3D p2, ITuple3D p1)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            var dz = p2.Z - p1.Z;
            var dInv = 1 / Math.Sqrt(dx * dx + dy * dy + dz * dz);

            return new Tuple3D(dx * dInv, dy * dInv, dz * dInv);
        }

        public static Tuple3D GetPointInDirection(this ITuple3D p, ITuple3D v)
            => new Tuple3D(
                p.X + v.X,
                p.Y + v.Y,
                p.Z + v.Z
            );

        public static Tuple3D GetPointInDirection(this ITuple3D p, ITuple3D v, double t)
            => new Tuple3D(
                p.X + t * v.X,
                p.Y + t * v.Y,
                p.Z + t * v.Z
            );

        /// <summary>
        /// The Euclidean squared length of this tuple when it represents a vector
        /// </summary>
        public static double GetLengthSquared(this ITuple3D vector)
            => vector.X * vector.X + 
               vector.Y * vector.Y + 
               vector.Z * vector.Z;

        public static double GetLengthSquared(double vectorX, double vectorY, double vectorZ)
            => vectorX * vectorX + 
               vectorY * vectorY + 
               vectorZ * vectorZ;

        /// <summary>
        /// The Euclidean squared length of this tuple when it represents a vector
        /// </summary>
        public static double GetLengthSquared(this ITuple4D vector)
            => vector.X * vector.X + 
               vector.Y * vector.Y + 
               vector.Z * vector.Z + 
               vector.W * vector.W;

        public static double GetLengthSquared(double vectorX, double vectorY, double vectorZ, double vectorW)
            => vectorX * vectorX +
               vectorY * vectorY +
               vectorZ * vectorZ +
               vectorW * vectorW;

        /// <summary>
        /// True of the Euclidean squared length of this vector is near unity
        /// </summary>
        public static bool IsUnitVector(this ITuple2D vector)
            => vector
                .GetLengthSquared()
                .IsAlmostEqual(1.0d);

        /// <summary>
        /// True of the Euclidean squared length of this vector is near unity
        /// </summary>
        public static bool IsUnitVector(this ITuple3D vector)
            => vector
                .GetLengthSquared()
                .IsAlmostEqual(1.0d);

        /// <summary>
        /// True of the Euclidean squared length of this vector is near unity
        /// </summary>
        public static bool IsUnitVector(this ITuple4D vector)
            => vector
                .GetLengthSquared()
                .IsAlmostEqual(1.0d);

        /// <summary>
        /// True of the Euclidean squared length of this vector is near zero
        /// </summary>
        public static bool IsZeroVector(this ITuple2D vector)
            => vector
                .GetLengthSquared()
                .IsAlmostZero();

        /// <summary>
        /// True of the Euclidean squared length of this vector is near zero
        /// </summary>
        public static bool IsExactZeroVector(this ITuple3D vector)
            => vector.GetLengthSquared().IsExactZero();

        /// <summary>
        /// True of the Euclidean squared length of this vector is near zero
        /// </summary>
        public static bool IsZeroVector(this ITuple3D vector)
            => vector
                .GetLengthSquared()
                .IsAlmostZero();

        /// <summary>
        /// True of the Euclidean squared length of this vector is near zero
        /// </summary>
        public static bool IsZeroVector(this ITuple4D vector)
            => vector
                .GetLengthSquared()
                .IsAlmostZero();


        public static double Determinant(ITuple2D v1, ITuple2D v2) 
            => v1.X * v2.Y - v1.Y * v2.X;

        public static double Determinant(ITuple3D v1, ITuple3D v2, ITuple3D v3) 
            => v1.X * (v2.Y * v3.Z - v2.Z * v3.Y) + 
               v1.Y * (v2.Z * v3.X - v2.X * v3.Z) + 
               v1.Z * (v2.X * v3.Y - v2.Y * v3.X);


        /// <summary>
        /// The Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double VectorDot(this ITuple2D v1, ITuple2D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return v1.X * v2.X + v1.Y * v2.Y;
        }

        /// <summary>
        /// The Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
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
        public static double VectorDot(this ITuple3D v1, ITuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        /// <summary>
        /// The Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
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
        public static double VectorDot(this ITuple4D v1, ITuple4D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W;
        }

        public static Complex VectorDot(this IComplexTuple2D v1, IComplexTuple2D v2)
            => v1.X * v2.X.Conjugate() +
               v1.Y * v2.Y.Conjugate();

        public static Complex VectorDot(this IComplexTuple3D v1, IComplexTuple3D v2)
            => v1.X * v2.X.Conjugate() +
               v1.Y * v2.Y.Conjugate() +
               v1.Z * v2.Z.Conjugate();

        public static Complex VectorDot(this IComplexTuple3D v1, ITuple3D v2)
            => v1.X * v2.X +
               v1.Y * v2.Y +
               v1.Z * v2.Z;


        /// <summary>
        /// The absolute value of the Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double VectorAbsDot(this ITuple2D v1, ITuple2D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return Math.Abs(v1.X * v2.X + v1.Y * v2.Y);
        }

        /// <summary>
        /// The absolute value of the Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
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
        public static double VectorAbsDot(this Tuple3D v1, Tuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return Math.Abs(v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
        }

        /// <summary>
        /// The absolute value of the Euclidean dot product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
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
        public static double VectorAbsDot(this Tuple4D v1, Tuple4D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return Math.Abs(v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W);
        }


        public static Tuple2D AnalyzeOnVectors(this ITuple2D v, ITuple2D u1, ITuple2D u2)
        {
            var s1 = (v.X * u1.X + v.Y * u1.Y) / 
                     Math.Sqrt(u1.X * u1.X + u1.Y * u1.Y);

            var s2 = (v.X * u2.X + v.Y * u2.Y) / 
                    Math.Sqrt(u2.X * u2.X + u2.Y * u2.Y);

            return new Tuple2D(s1, s2);
        }

        public static Tuple2D ProjectOnVector(this ITuple2D v, ITuple2D u)
        {
            var s1 = v.X * u.X + v.Y * u.Y;
            var s2 = u.X * u.X + u.Y * u.Y;
            var s = s1 / s2;

            return new Tuple2D(u.X * s, u.Y * s);
        }

        public static Tuple2D ProjectOnUnitVector(this ITuple2D v, ITuple2D u)
        {
            var s = v.X * u.X + v.Y * u.Y;

            return new Tuple2D(u.X * s, u.Y * s);
        }

        public static Tuple3D ProjectOnVector(this ITuple3D v, ITuple3D u)
        {
            var s1 = v.X * u.X + v.Y * u.Y + v.Z * u.Z;
            var s2 = u.X * u.X + u.Y * u.Y + u.Z * u.Z;
            var s = s1 / s2;

            return new Tuple3D(
                u.X * s,
                u.Y * s,
                u.Z * s
            );
        }

        public static Tuple3D ProjectOnUnitVector(this ITuple3D v, ITuple3D u)
        {
            var s = v.X * u.X + v.Y * u.Y + v.Z * u.Z;

            return new Tuple3D(
                u.X * s,
                u.Y * s,
                u.Z * s
            );
        }

        public static Tuple4D ProjectOnVector(this ITuple4D v, ITuple4D u)
        {
            var s1 = v.X * u.X + v.Y * u.Y + v.Z * u.Z + v.W * u.W;
            var s2 = u.X * u.X + u.Y * u.Y + u.Z * u.Z + u.W * u.W;
            var s = s1 / s2;

            return new Tuple4D(
                u.X * s,
                u.Y * s,
                u.Z * s,
                u.W * s
            );
        }

        public static Tuple4D ProjectOnUnitVector(this ITuple4D v, ITuple4D u)
        {
            var s = v.X * u.X + v.Y * u.Y + v.Z * u.Z + v.W * u.W;

            return new Tuple4D(
                u.X * s,
                u.Y * s,
                u.Z * s,
                u.W * s
            );
        }


        /// <summary>
        /// Returns a copy of this vector if its dot product with the other vector is positive, else
        /// it returnes the vector's negative
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="planeNormal"></param>
        /// <returns></returns>
        public static Tuple2D FaceDirection(this ITuple2D vector, ITuple2D planeNormal)
        {
            Debug.Assert(!vector.IsInvalid && !planeNormal.IsInvalid);
            Debug.Assert(!planeNormal.IsZeroVector());

            return
                (vector.X * planeNormal.X + vector.Y * planeNormal.Y).IsDefiniteNegative()
                    ? new Tuple2D(-vector.X, -vector.Y)
                    : new Tuple2D(vector.X, vector.Y);
        }

        /// <summary>
        /// Returns a copy of this vector if its dot product with the other vector is positive, else
        /// it returns the vector's negative
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="planeNormal"></param>
        /// <returns></returns>
        public static Tuple3D FaceDirection(this ITuple3D vector, ITuple3D planeNormal)
        {
            Debug.Assert(!vector.IsInvalid && !planeNormal.IsInvalid);
            Debug.Assert(!planeNormal.IsZeroVector());

            return
                (vector.X * planeNormal.X + vector.Y * planeNormal.Y + vector.Z * planeNormal.Z).IsDefiniteNegative()
                    ? new Tuple3D(-vector.X, -vector.Y, -vector.Z)
                    : new Tuple3D(vector.X, vector.Y, vector.Z);
        }


        /// <summary>
        /// Returns a new vector orthogonal to this one.
        /// </summary>
        /// <returns></returns>
        public static Tuple2D GetNormal(this ITuple2D vector)
        {
            Debug.Assert(!vector.IsInvalid);

            return new Tuple2D(-vector.Y, vector.X);
        }

        /// <summary>
        /// Returns a new vector orthogonal to this one.
        /// </summary>
        /// <returns></returns>
        public static IntTuple2D GetNormal(this IntTuple2D vector)
        {
            return new IntTuple2D(-vector.Y, vector.X);
        }

        /// <summary>
        /// Returns a new vector orthogonal to this one.
        /// </summary>
        /// <returns></returns>
        public static Tuple2D GetUnitNormal(this ITuple2D vector)
        {
            Debug.Assert(!vector.IsInvalid);

            var s = vector.GetLength();
            if (s.IsAlmostZero())
                return new Tuple2D(-vector.Y, vector.X);

            s = 1.0d / s;
            return new Tuple2D(-vector.Y * s, vector.X * s);
        }


        /// <summary>
        /// The Euclidean cross product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Tuple3D VectorCross(this ITuple3D v1, ITuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return new Tuple3D(
                v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X
            );
        }

        public static double VectorCrossLength(this ITuple3D v1, ITuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            var x = v1.Y * v2.Z - v1.Z * v2.Y;
            var y = v1.Z * v2.X - v1.X * v2.Z;
            var z = v1.X * v2.Y - v1.Y * v2.X;

            return Math.Sqrt(x * x + y * y + z * z);
        }

        public static double VectorCrossLengthSquared(this ITuple3D v1, ITuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            var x = v1.Y * v2.Z - v1.Z * v2.Y;
            var y = v1.Z * v2.X - v1.X * v2.Z;
            var z = v1.X * v2.Y - v1.Y * v2.X;

            return x * x + y * y + z * z;
        }

        public static bool IsParallelTo(this ITuple3D v1, ITuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            var x = v1.Y * v2.Z - v1.Z * v2.Y;
            var y = v1.Z * v2.X - v1.X * v2.Z;
            var z = v1.X * v2.Y - v1.Y * v2.X;

            return (x * x + y * y + z * z).IsAlmostZero();
        }


        /// <summary>
        /// The Euclidean cross product between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
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
        public static Tuple3D VectorUnitCrossXy(this ITuple2D v1, ITuple2D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            var vz = v1.X * v2.Y - v1.Y * v2.X;

            return new Tuple3D(
                0, 
                0, 
                vz < 0 ? -1 : (vz > 0 ? 1 : 0)
            );
        }

        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// The first vector is assumed to have z=0 while the second x=0 and y=0
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2Z"></param>
        /// <returns></returns>
        public static Tuple3D VectorUnitCrossXy_Z(this ITuple2D v1, double v2Z)
        {
            var vx = v1.Y * v2Z;
            var vy = -v1.X * v2Z;

            var s = 1.0d / Math.Sqrt(vx * vx + vy * vy);

            return new Tuple3D(vx * s, vy * s, 0);
        }

        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Tuple3D VectorUnitCross(this ITuple3D v1, ITuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            var vx = v1.Y * v2.Z - v1.Z * v2.Y;
            var vy = v1.Z * v2.X - v1.X * v2.Z;
            var vz = v1.X * v2.Y - v1.Y * v2.X;

            var s = 1.0d / Math.Sqrt(vx * vx + vy * vy + vz * vz);

            return new Tuple3D(vx * s, vy * s, vz * s);
        }

        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2X"></param>
        /// <param name="v2Y"></param>
        /// <param name="v2Z"></param>
        /// <returns></returns>
        public static Tuple3D VectorUnitCross(this ITuple3D v1, double v2X, double v2Y, double v2Z)
        {
            var vx = v1.Y * v2Z - v1.Z * v2Y;
            var vy = v1.Z * v2X - v1.X * v2Z;
            var vz = v1.X * v2Y - v1.Y * v2X;

            var s = 1.0d / Math.Sqrt(vx * vx + vy * vy + vz * vz);

            return new Tuple3D(vx * s, vy * s, vz * s);
        }

        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// The second vector is assumed to have z=0
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2X"></param>
        /// <param name="v2Y"></param>
        /// <returns></returns>
        public static Tuple3D VectorUnitCrossXy(this ITuple3D v1, double v2X, double v2Y)
        {
            var vx = -v1.Z * v2Y;
            var vy = v1.Z * v2X;
            var vz = v1.X * v2Y - v1.Y * v2X;

            var s = 1.0d / Math.Sqrt(vx * vx + vy * vy + vz * vz);

            return new Tuple3D(vx * s, vy * s, vz * s);
        }

        /// <summary>
        /// Returns the Euclidean cross product between the given vectors as a unit vector
        /// The second vector is assumed to have z=0
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Tuple3D VectorUnitCrossXy(this ITuple3D v1, ITuple2D v2)
        {
            var vx = -v1.Z * v2.Y;
            var vy = v1.Z * v2.X;
            var vz = v1.X * v2.Y - v1.Y * v2.X;

            var s = 1.0d / Math.Sqrt(vx * vx + vy * vy + vz * vz);

            return new Tuple3D(vx * s, vy * s, vz * s);
        }


        /// <summary>
        /// Returns a unit vector from the given one. If the length of the given vector is near zero
        /// it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Tuple2D ToUnitVector(this ITuple2D vector)
        {
            Debug.Assert(!vector.IsInvalid);

            var s = vector.GetLength();
            if (s.IsAlmostZero())
                return vector.ToTuple2D();

            s = 1.0d / s;
            return new Tuple2D(vector.X * s, vector.Y * s);
        }

        public static Tuple2D ToUnitVector(double vectorX, double vectorY)
        {
            var s = DistanceUtils.GetLength(vectorX, vectorY);
            if (s.IsAlmostZero())
                return new Tuple2D(vectorX, vectorY);

            s = 1.0d / s;
            return new Tuple2D(vectorX * s, vectorY * s);
        }

        /// <summary>
        /// Returns a unit vector from the given one. If the length of the given vector is near zero
        /// it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Tuple3D ToUnitVector(this ITuple3D vector)
        {
            Debug.Assert(!vector.IsInvalid);

            var s = vector.GetLength();
            if (s.IsAlmostZero())
                return Tuple3D.Zero;

            s = 1.0d / s;
            return new Tuple3D(vector.X * s, vector.Y * s, vector.Z * s);
        }

        public static Tuple3D ToUnitVector(double vectorX, double vectorY, double vectorZ)
        {
            var s = DistanceUtils.GetLength(vectorX, vectorY, vectorZ);
            if (s.IsAlmostZero())
                return new Tuple3D(vectorX, vectorY, vectorZ);

            s = 1.0d / s;
            return new Tuple3D(vectorX * s, vectorY * s, vectorZ * s);
        }

        /// <summary>
        /// Returns a unit vector from the given one. If the length of the given vector is near zero
        /// it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Tuple4D ToUnitVector(this ITuple4D vector)
        {
            Debug.Assert(!vector.IsInvalid);

            var s = vector.GetLength();
            if (s.IsAlmostZero())
                return vector.ToTuple4D();

            s = 1.0d / s;
            return new Tuple4D(vector.X * s, vector.Y * s, vector.Z * s, vector.W * s);
        }

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Tuple2D ToNegativeVector(this ITuple2D vector)
            => new Tuple2D(-vector.X, -vector.Y);

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Tuple2D ToNegativeUnitVector(this ITuple2D vector)
        {
            Debug.Assert(!vector.IsInvalid);

            var s = vector.GetLength();
            if (s.IsAlmostZero())
                return vector.ToTuple2D();

            s = -1.0d / s;
            return new Tuple2D(vector.X * s, vector.Y * s);
        }

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Tuple3D ToNegativeVector(this ITuple3D vector)
            => new Tuple3D(-vector.X, -vector.Y, -vector.Z);

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Tuple3D ToNegativeUnitVector(this Tuple3D vector)
        {
            Debug.Assert(!vector.IsInvalid);

            var s = vector.GetLength();
            if (s.IsAlmostZero())
                return vector;

            s = 1.0d / s;
            return new Tuple3D(vector.X * s, vector.Y * s, vector.Z * s);
        }

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Tuple4D ToNegativeVector(this ITuple4D vector)
            => new Tuple4D(-vector.X, -vector.Y, -vector.Z, -vector.W);

        /// <summary>
        /// Returns a negative unit vector from the given one. If the length of the given vector is near 
        /// zero it's returned as-is
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Tuple4D ToNegativeUnitVector(this Tuple4D vector)
        {
            Debug.Assert(!vector.IsInvalid);

            var s = vector.GetLength();
            if (s.IsAlmostZero())
                return vector;

            s = 1.0d / s;
            return new Tuple4D(vector.X * s, vector.Y * s, vector.Z * s, vector.W * s);
        }

        public static Tuple<double, Tuple2D> ToLengthAndUnitDirection(this ITuple2D vector)
        {
            var length = Math.Sqrt(
                vector.X * vector.X +
                vector.Y * vector.Y
            );

            var lengthInv = 1 / length;

            return Tuple.Create(
                length,
                new Tuple2D(
                    vector.X * lengthInv,
                    vector.Y * lengthInv
                )
            );
        }

        public static Tuple<double, Tuple3D> ToLengthAndUnitDirection(this ITuple3D vector)
        {
            var length = Math.Sqrt(
                vector.X * vector.X +
                vector.Y * vector.Y +
                vector.Z * vector.Z
            );

            var lengthInv = 1 / length;

            return Tuple.Create(
                length,
                new Tuple3D(
                    vector.X * lengthInv,
                    vector.Y * lengthInv,
                    vector.Z * lengthInv
                )
            );
        }


        /// <summary>
        /// The Euclidean distance between the given vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double GetDistanceToPoint(this ITuple2D v1, ITuple2D v2)
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
        public static double GetDistanceToPoint(this ITuple2D v1, double v2X, double v2Y)
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
        public static double GetDistanceToPoint(this ITuple3D p1, ITuple3D p2)
        {
            var vX = p2.X - p1.X;
            var vY = p2.Y - p1.Y;
            var vZ = p2.Z - p1.Z;

            return Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
        }

        public static double GetDistanceToPoint(this ITuple3D p1, double p2X, double p2Y, double p2Z)
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
        public static double GetDistanceToPoint(this ITuple4D v1, ITuple4D v2)
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
        public static double GetDistanceSquaredToPoint(this ITuple2D v1, ITuple2D v2)
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
        public static double GetDistanceSquaredToPoint(this ITuple2D v1, double v2X, double v2Y)
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
        public static double GetDistanceSquaredToPoint(this Tuple3D v1, Tuple3D v2)
        {
            var vX = v2.X - v1.X;
            var vY = v2.Y - v1.Y;
            var vZ = v2.Z - v1.Z;

            return vX * vX + vY * vY + vZ * vZ;
        }

        public static double GetDistanceSquaredToPoint(this ITuple3D p1, double p2X, double p2Y, double p2Z)
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
        public static double GetDistanceSquaredToPoint(this Tuple4D v1, Tuple4D v2)
        {
            var vX = v2.X - v1.X;
            var vY = v2.Y - v1.Y;
            var vZ = v2.Z - v1.Z;
            var vW = v2.W - v1.W;

            return vX * vX + vY * vY + vZ * vZ + vW * vW;
        }

        public static Tuple2D ReflectVectorOnNormal(this ITuple2D vector, ITuple2D normal)
        {
            var s = 2.0d * 
                    (vector.X * normal.X + vector.Y * normal.Y) / 
                    (normal.X * normal.X + normal.Y * normal.Y);

            return new Tuple2D(
                vector.X - s * normal.X,
                vector.Y - s * normal.Y
            );
        }

        public static Tuple2D ReflectVectorOnUnitNormal(this ITuple2D vector, ITuple2D normal)
        {
            var s = 2.0d * (vector.X * normal.X + vector.Y * normal.Y);

            return new Tuple2D(
                vector.X - s * normal.X,
                vector.Y - s * normal.Y
            );
        }

        /// <summary>
        /// Create a rotation unit quaternion from this vector and the given angle. This vector must be
        /// first made of unit length before using this mathod
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        public static Tuple4D ToRotationQuaternion(this ITuple3D vector, double rotationAngle)
        {
            var halfTheta = 0.5 * rotationAngle;
            var cosHalfTheta = Math.Cos(halfTheta);
            var sinHalfTheta = Math.Sin(halfTheta);

            return new Tuple4D(
                cosHalfTheta,
                sinHalfTheta * vector.X,
                sinHalfTheta * vector.Y,
                sinHalfTheta * vector.Z
            );
        }

        /// <summary>
        /// Create a rotation unit quaternion from this vector. The angle of rotation is computed as
        /// 2 * Pi * Length of this vector.
        /// </summary>
        /// <returns></returns>
        public static Tuple4D ToRotationQuaternion(this ITuple3D vector)
        {
            //Compute the vector length and its inverse
            var vectorLength = vector.GetLength();
            var invVectorLength = 1.0d / vectorLength;

            //Compute the rotation angle
            var halfTheta = (vectorLength - Math.Floor(vectorLength)) * Constants.Pi;
            var cosHalfTheta = Math.Cos(halfTheta);
            var sinHalfTheta = Math.Sin(halfTheta);

            return new Tuple4D(
                cosHalfTheta,
                sinHalfTheta * vector.X * invVectorLength,
                sinHalfTheta * vector.Y * invVectorLength,
                sinHalfTheta * vector.Z * invVectorLength
            );
        }

        public static Tuple3D RotateUsing(this ITuple3D vector, ITuple4D rotationQuaternion)
        {
            //TODO: Generate this using GMac and compare with classical implementation.

            return new Tuple3D();
        }

        public static Tuple3D RotateUsing(this ITuple3D vector, ITuple3D rotationVector, double rotationAngle)
        {
            //TODO: Generate this using GMac and compare with classical implementation.

            return vector.RotateUsing(rotationVector.ToRotationQuaternion(rotationAngle));
        }

        public static Tuple3D RotateUsing(this ITuple3D vector, ITuple3D rotationVector)
        {
            //TODO: Generate this using GMac and compare with classical implementation.

            return vector.RotateUsing(rotationVector.ToRotationQuaternion());
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
        public static double GetVectorsAngle(this ITuple2D v1, ITuple2D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            var t1 = v1.X * v2.X + v1.Y * v2.Y;
            var t2 = v1.X * v1.X + v1.Y * v1.Y;
            var t3 = v2.X * v2.X + v2.Y * v2.Y;

            var cosAngle = t1 / Math.Sqrt(t2 * t3);

            return Math.Acos(cosAngle);
        }

        public static Tuple3D GetTriangleNormal(ITuple3D p1, ITuple3D p2, ITuple3D p3)
        {
            //TODO: Test this for numerical stability, maybe select two sides with largest lengths
            var v12 = new Tuple3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            var v23 = new Tuple3D(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

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

        public static Tuple3D GetTriangleUnitNormal(ITuple3D p1, ITuple3D p2, ITuple3D p3)
        {
            //TODO: Test this for numerical stability, maybe select two sides with largest lengths
            var v12 = new Tuple3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            var v23 = new Tuple3D(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

            return v12.VectorUnitCross(v23);
        }

        public static Tuple3D GetTriangleInverseUnitNormal(ITuple3D p1, ITuple3D p2, ITuple3D p3)
        {
            //TODO: Test this for numerical stability, maybe select two sides with largest lengths
            var v12 = new Tuple3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            var v23 = new Tuple3D(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

            return v12.VectorUnitCross(v23);
        }

        /// <summary>
        /// Find the angle between this vector and another
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double GetVectorsAngle(this ITuple3D v1, ITuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            var t1 = v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
            var t2 = v1.X * v1.X + v1.Y * v1.Y + v1.Z * v1.Z;
            var t3 = v2.X * v2.X + v2.Y * v2.Y + v2.Z * v2.Z;

            var cosAngle = t1 / Math.Sqrt(t2 * t3);

            return Math.Acos(cosAngle);
        }

        /// <summary>
        /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetPointsAngle(this ITuple3D p0, ITuple3D p1, ITuple3D p2)
        {
            return GetVectorsAngle(
                new Tuple3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z), 
                new Tuple3D(p2.X - p0.X, p2.Y - p0.Y, p2.Z - p0.Z)
            );
        }

        public static void GetCoordinateSystem(this Tuple3D v1, out Tuple3D v2, out Tuple3D v3)
        {
            v2 = Math.Abs(v1.X) > Math.Abs(v1.Y)
                ? new Tuple3D(-v1.Z, 0, v1.X) / Math.Sqrt(v1.X * v1.X + v1.Z * v1.Z)
                : new Tuple3D(0, v1.Z, -v1.Y) / Math.Sqrt(v1.Y * v1.Y + v1.Z * v1.Z);

            v3 = v1.VectorCross(v2);
        }
    }
}