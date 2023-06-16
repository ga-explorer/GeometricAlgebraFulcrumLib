using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

public static class Float64Vector2DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToTuple2D(this LinUnitBasisVector2D axis)
    {
        return axis switch
        {
            LinUnitBasisVector2D.PositiveX => Float64Vector2D.E1,
            LinUnitBasisVector2D.NegativeX => Float64Vector2D.NegativeE1,
            LinUnitBasisVector2D.PositiveY => Float64Vector2D.E2,
            _ => Float64Vector2D.NegativeE2
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D RotateUsingAngle(this IFloat64Tuple2D vector, Float64PlanarAngle angle)
    {
        return angle.Rotate(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D TranslateBy(this IFloat64Tuple2D vector, double translationX, double translationY)
    {
        return new Float64Vector2D(
            translationX + vector.X,
            translationY + vector.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D TranslateBy(this IFloat64Tuple2D vector, IFloat64Tuple2D translationVector)
    {
        return new Float64Vector2D(
            translationVector.X + vector.X,
            translationVector.Y + vector.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ScaleBy(this IFloat64Tuple2D vector, double scaleFactor)
    {
        return new Float64Vector2D(
            scaleFactor * vector.X,
            scaleFactor * vector.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ScaleBy(this IFloat64Tuple2D vector, double scaleFactorX, double scaleFactorY)
    {
        return new Float64Vector2D(
            scaleFactorX * vector.X,
            scaleFactorY * vector.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ScaleBy(this IFloat64Tuple2D vector, IFloat64Tuple2D scaleFactorVector)
    {
        return new Float64Vector2D(
            scaleFactorVector.X * vector.X,
            scaleFactorVector.Y * vector.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D RotateBy(this IFloat64Tuple2D vector, Float64PlanarAngle angle)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        return new Float64Vector2D(
            vector.X * cosAngle - vector.Y * sinAngle,
            vector.X * sinAngle + vector.Y * cosAngle
        );
    }

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
    public static Complex ESp(this IComplexTuple2D v1, IComplexTuple2D v2)
    {
        return v1.X * v2.X.Conjugate() +
               v1.Y * v2.Y.Conjugate();
    }

    /// <summary>
    /// The absolute value of the Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar ESpAbs(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
    {
        return Math.Abs(v1.X * v2.X + v1.Y * v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D AnalyzeOnVectors(this IFloat64Tuple2D v, IFloat64Tuple2D u1, IFloat64Tuple2D u2)
    {
        var s1 = (v.X * u1.X + v.Y * u1.Y) /
                 Math.Sqrt(u1.X * u1.X + u1.Y * u1.Y);

        var s2 = (v.X * u2.X + v.Y * u2.Y) /
                 Math.Sqrt(u2.X * u2.X + u2.Y * u2.Y);

        return new Float64Vector2D(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ProjectOnVector(this IFloat64Tuple2D v, IFloat64Tuple2D u)
    {
        var s1 = v.X * u.X + v.Y * u.Y;
        var s2 = u.X * u.X + u.Y * u.Y;
        var s = s1 / s2;

        return new Float64Vector2D(u.X * s, u.Y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ProjectOnUnitVector(this IFloat64Tuple2D v, IFloat64Tuple2D u)
    {
        var s = v.X * u.X + v.Y * u.Y;

        return new Float64Vector2D(u.X * s, u.Y * s);
    }

    /// <summary>
    /// Returns a copy of this vector if its dot product with the other vector is positive, else
    /// it returns the vector's negative
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="directionVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D FaceDirection(this IFloat64Tuple2D vector, IFloat64Tuple2D directionVector)
    {
        Debug.Assert(!directionVector.IsZeroVector());

        return
            (vector.X * directionVector.X + vector.Y * directionVector.Y).IsNegative()
                ? new Float64Vector2D(-vector.X, -vector.Y)
                : new Float64Vector2D(vector.X, vector.Y);
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToNegativeVector(this IFloat64Tuple2D vector)
    {
        return new Float64Vector2D(-vector.X, -vector.Y);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ReflectVectorOnVector(this IFloat64Tuple2D reflectionVector, IFloat64Tuple2D vector)
    {
        var s = 2 * reflectionVector.ESp(vector) / reflectionVector.ENormSquared();

        return new Float64Vector2D(
            vector.X - s * reflectionVector.X,
            vector.Y - s * reflectionVector.Y
        );
    }

    public static IEnumerable<Float64Vector2D> ReflectVectorsOnVector(this IFloat64Tuple2D reflectionVector, params IFloat64Tuple2D[] vectorsList)
    {
        var s = 2 / reflectionVector.ENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.ESp(vector);

            yield return new Float64Vector2D(
                vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y
            );
        }
    }

    public static IEnumerable<Float64Vector2D> ReflectVectorsOnVector(this IFloat64Tuple2D reflectionVector, IEnumerable<IFloat64Tuple2D> vectorsList)
    {
        var s = 2 / reflectionVector.ENormSquared();

        foreach (var vector in vectorsList)
        {
            var s1 = s * reflectionVector.ESp(vector);

            yield return new Float64Vector2D(
                vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ReflectVectorOnUnitVector(this IFloat64Tuple2D reflectionVector, IFloat64Tuple2D vector)
    {
        var s = 2 * reflectionVector.ESp(vector);

        return new Float64Vector2D(
            vector.X - s * reflectionVector.X,
            vector.Y - s * reflectionVector.Y
        );
    }

    public static IEnumerable<Float64Vector2D> ReflectVectorsOnUnitVector(this IFloat64Tuple2D reflectionVector, params IFloat64Tuple2D[] vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.ESp(vector);

            yield return new Float64Vector2D(
                vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y
            );
        }
    }

    public static IEnumerable<Float64Vector2D> ReflectVectorsOnUnitVector(this IFloat64Tuple2D reflectionVector, IEnumerable<IFloat64Tuple2D> vectorsList)
    {
        foreach (var vector in vectorsList)
        {
            var s1 = 2 * reflectionVector.ESp(vector);

            yield return new Float64Vector2D(
                vector.X - s1 * reflectionVector.X,
                vector.Y - s1 * reflectionVector.Y
            );
        }
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, Float64Vector2D> ToLengthAndUnitDirection(this IFloat64Tuple2D vector)
    {
        var length = Math.Sqrt(
            vector.X * vector.X +
            vector.Y * vector.Y
        );

        var lengthInv = 1 / length;

        return Tuple.Create(
            length,
            new Float64Vector2D(
                vector.X * lengthInv,
                vector.Y * lengthInv
            )
        );
    }

    /// <summary>
    /// Returns a negative unit vector from the given one. If the length of the given vector is near 
    /// zero it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToNegativeUnitVector(this IFloat64Tuple2D vector)
    {
        var s = vector.ENorm();
        if (s.IsAlmostZero())
            return vector.ToLinVector2D();

        s = -1.0d / s;
        return new Float64Vector2D(vector.X * s, vector.Y * s);
    }

    /// <summary>
    /// Returns a new vector orthogonal to this one.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetNormal(this IFloat64Tuple2D vector)
    {
        return new Float64Vector2D(-vector.Y, vector.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetUnitNormal(this IFloat64Tuple2D vector)
    {
        var normInv = 1d / vector.ENorm();

        return new Float64Vector2D(
            -vector.Y * normInv,
            vector.X * normInv
        );
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Add(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
    {
        return new Float64Vector2D(
            v1.X + v2.X,
            v1.Y + v2.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Subtract(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
    {
        return new Float64Vector2D(
            v1.X - v2.X,
            v1.Y - v2.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Times(this IFloat64Tuple2D v1, double v2)
    {
        return new Float64Vector2D(
            v1.X * v2,
            v1.Y * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Times(this double v1, IFloat64Tuple2D v2)
    {
        return new Float64Vector2D(
            v1 * v2.X,
            v1 * v2.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Divide(this IFloat64Tuple2D v1, double v2)
    {
        v2 = 1d / v2;

        return new Float64Vector2D(
            v1.X * v2,
            v1.Y * v2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetFloat64Tuple2D(this System.Random random)
    {
        return new Float64Vector2D(
            random.NextDouble(),
            random.NextDouble()
        );
    }


    /// <summary>
    /// The Euclidean length of this tuple when it represents a vector
    /// </summary>
    public static double ENorm(this IFloat64Tuple2D vector)
    {
        return Math.Sqrt(
            vector.X * vector.X +
            vector.Y * vector.Y
        );
    }

    public static double ENorm(double vectorX, double vectorY)
    {
        return Math.Sqrt(
            vectorX * vectorX +
            vectorY * vectorY
        );
    }

    public static double ENorm(this IComplexTuple2D vector)
    {
        return Math.Sqrt(
            (vector.X * vector.X.Conjugate()).Real +
            (vector.Y * vector.Y.Conjugate()).Real
        );
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near unity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnitVector(this IFloat64Tuple2D vector)
    {
        return vector
            .ENormSquared()
            .IsOne();
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVector(this IFloat64Tuple2D vector)
    {
        return vector
            .ENormSquared()
            .IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Determinant(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
    {
        return v1.X * v2.Y - v1.Y * v2.X;
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar ESp(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y;
    }

    /// <summary>
    /// The Euclidean squared length of this tuple when it represents a vector
    /// </summary>
    public static Float64Scalar ENormSquared(this IFloat64Tuple2D vector)
    {
        return vector.X * vector.X +
               vector.Y * vector.Y;
    }

    public static double ENormSquared(double vectorX, double vectorY)
    {
        return vectorX * vectorX +
               vectorY * vectorY;
    }

    public static double ENormSquared(this IComplexTuple2D vector)
    {
        return (vector.X * vector.X.Conjugate()).Real +
               (vector.Y * vector.Y.Conjugate()).Real;
    }

    /// <summary>
    /// Returns a unit vector from the given one. If the length of the given vector is near zero
    /// it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="zeroAsSymmetric"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToUnitVector(this IFloat64Tuple2D vector, bool zeroAsSymmetric = true)
    {
        var s = vector.ENorm();

        if (s.IsZero())
            return zeroAsSymmetric
                ? Float64Vector2D.UnitSymmetric
                : Float64Vector2D.Zero;

        s = 1.0d / s;
        return new Float64Vector2D(vector.X * s, vector.Y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToUnitVector(double vectorX, double vectorY, bool zeroAsSymmetric = true)
    {
        var s = ENorm(vectorX, vectorY);

        if (s.IsZero())
            return zeroAsSymmetric
                ? Float64Vector2D.UnitSymmetric
                : Float64Vector2D.Zero;

        s = 1.0d / s;
        return new Float64Vector2D(vectorX * s, vectorY * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToTuple2D(this Complex complexNumber)
    {
        return new Float64Vector2D(
            complexNumber.Real,
            complexNumber.Imaginary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToTuple2D(this IFloat64PolarVector2D polarPosition)
    {
        return new Float64Vector2D(
            polarPosition.R * Math.Cos(polarPosition.Theta),
            polarPosition.R * Math.Sin(polarPosition.Theta)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PolarVector2D ToPolarPosition(this Complex complexNumber)
    {
        return new Float64PolarVector2D(
            complexNumber.Magnitude,
            complexNumber.Phase
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PolarVector2D ToPolarPosition(this IFloat64Tuple2D point)
    {
        return new Float64PolarVector2D(
            Math.Sqrt(point.X * point.X + point.Y * point.Y),
            Math.Atan2(point.Y, point.X)
        );
    }

    public static Float64Vector2D ToTuple2D(this IEnumerable<double> scalarList)
    {
        var scalarArray = new double[2];

        var i = 0;
        foreach (var scalar in scalarList)
            scalarArray[i++] = scalar;

        return new Float64Vector2D(
            scalarArray[0],
            scalarArray[1]
        );
    }

    public static Float64Vector2D GetCenterOfMassPoint(this IEnumerable<IFloat64Tuple2D> pointsList)
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

        return new Float64Vector2D(
            centerX * pointsCountInv,
            centerY * pointsCountInv
        );
    }

    public static Float64Vector2D GetCenterPoint(this IEnumerable<IFloat64Tuple2D> pointsList)
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

        return new Float64Vector2D(
            0.5 * (minX + maxX),
            0.5 * (minY + maxY)
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
    public static Float64Vector2D GetDirectionToPoint(this IFloat64Tuple2D p1, IFloat64Tuple2D p2)
    {
        return new Float64Vector2D(
            p2.X - p1.X,
            p2.Y - p1.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetDirectionToPoint(this IFloat64Tuple2D p1, double p2X, double p2Y)
    {
        return new Float64Vector2D(
            p2X - p1.X,
            p2Y - p1.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetDirectionFromPoint(this IFloat64Tuple2D p2, IFloat64Tuple2D p1)
    {
        return new Float64Vector2D(
            p2.X - p1.X,
            p2.Y - p1.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetDirectionFromPoint(this IFloat64Tuple2D p2, double p1X, double p1Y)
    {
        return new Float64Vector2D(
            p2.X - p1X,
            p2.Y - p1Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetUnitDirectionToPoint(this IFloat64Tuple2D p1, IFloat64Tuple2D p2)
    {
        var dx = p2.X - p1.X;
        var dy = p2.Y - p1.Y;
        var dInv = 1 / Math.Sqrt(dx * dx + dy * dy);

        return new Float64Vector2D(dx * dInv, dy * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetUnitDirectionFromPoint(this IFloat64Tuple2D p2, IFloat64Tuple2D p1)
    {
        var dx = p2.X - p1.X;
        var dy = p2.Y - p1.Y;
        var dInv = 1 / Math.Sqrt(dx * dx + dy * dy);

        return new Float64Vector2D(dx * dInv, dy * dInv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetPointInDirection(this IFloat64Tuple2D p, IFloat64Tuple2D v)
    {
        return new Float64Vector2D(
            p.X + v.X,
            p.Y + v.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetPointInDirection(this IFloat64Tuple2D p, IFloat64Tuple2D v, double t)
    {
        return new Float64Vector2D(
            p.X + t * v.X,
            p.Y + t * v.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetDirectionFrom(this IFloat64Tuple2D p2, IFloat64Tuple2D p1)
    {
        return new Float64Vector2D(
            p2.X - p1.X,
            p2.Y - p1.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Lerp(this double t, IFloat64Tuple2D v1, IFloat64Tuple2D v2)
    {
        var s = 1.0d - t;

        return new Float64Vector2D(
            s * v1.X + t * v2.X,
            s * v1.Y + t * v2.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Vector2D> Lerp(this IEnumerable<double> tList, IFloat64Tuple2D v1, IFloat64Tuple2D v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this IFloat64Tuple2D tuple)
    {
        return !(
            double.IsNaN(tuple.X) || double.IsInfinity(tuple.X) ||
            double.IsNaN(tuple.Y) || double.IsInfinity(tuple.Y)
        );
    }

    /// <summary>
    /// The value of the smallest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMinTupleComponent(this IFloat64Tuple2D tuple)
    {
        return tuple.X < tuple.Y ? tuple.X : tuple.Y;
    }

    /// <summary>
    /// The value of the largest component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMaxTupleComponent(this IFloat64Tuple2D tuple)
    {
        return tuple.X > tuple.Y ? tuple.X : tuple.Y;
    }

    /// <summary>
    /// The index of the smallest component of this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMinTupleComponentIndex(this IFloat64Tuple2D tuple)
    {
        return tuple.X < tuple.Y ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest component of this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxTupleComponentIndex(this IFloat64Tuple2D tuple)
    {
        return tuple.X > tuple.Y ? 0 : 1;
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
    /// The index of the largest absolute component in this tuple
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxAbsComponentIndex(this IFloat64Tuple2D tuple)
    {
        var absX = Math.Abs(tuple.X);
        var absY = Math.Abs(tuple.Y);

        return absX >= absY ? 0 : 1;
    }

    /// <summary>
    /// Returns a new tuple containing component-wise minimum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Min(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
    {
        return new Float64Vector2D(
            v1.X < v2.X ? v1.X : v2.X,
            v1.Y < v2.Y ? v1.Y : v2.Y
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise maximum values of the given tuples
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Max(this IFloat64Tuple2D v1, IFloat64Tuple2D v2)
    {
        return new Float64Vector2D(
            v1.X > v2.X ? v1.X : v2.X,
            v1.Y > v2.Y ? v1.Y : v2.Y
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise ceiling values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Ceiling(this IFloat64Tuple2D tuple)
    {
        return new Float64Vector2D(
            Math.Ceiling(tuple.X),
            Math.Ceiling(tuple.Y)
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise floor values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Floor(this IFloat64Tuple2D tuple)
    {
        return new Float64Vector2D(
            Math.Floor(tuple.X),
            Math.Floor(tuple.Y)
        );
    }

    /// <summary>
    /// Returns a new tuple containing component-wise absolute values of the given tuple
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Abs(this IPair<double> tuple)
    {
        return new Float64Vector2D(
            Math.Abs(tuple.Item1),
            Math.Abs(tuple.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MutableFloat64Tuple2D ToMutableTuple2D(this IFloat64Tuple2D tuple)
    {
        return new MutableFloat64Tuple2D(tuple.X, tuple.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D XyToTuple2D(this IFloat64Tuple3D tuple)
    {
        return new Float64Vector2D(tuple.X, tuple.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D YxToTuple2D(this IFloat64Tuple3D tuple)
    {
        return new Float64Vector2D(tuple.Y, tuple.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D XzToTuple2D(this IFloat64Tuple3D tuple)
    {
        return new Float64Vector2D(tuple.X, tuple.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ZxToTuple2D(this IFloat64Tuple3D tuple)
    {
        return new Float64Vector2D(tuple.Z, tuple.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D YzToTuple2D(this IFloat64Tuple3D tuple)
    {
        return new Float64Vector2D(tuple.Y, tuple.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ZyToTuple2D(this IFloat64Tuple3D tuple)
    {
        return new Float64Vector2D(tuple.Z, tuple.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToTuple2D(this IntTuple3D tuple)
    {
        return new Float64Vector2D(tuple.ItemX, tuple.ItemY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToTuple2D(this IFloat64Tuple4D tuple)
    {
        return new Float64Vector2D(tuple.X, tuple.Y);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToTuple2D(this IPair<int> tuple)
    {
        return new Float64Vector2D(
            tuple.Item1,
            tuple.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToTuple2D(this IPair<long> tuple)
    {
        return new Float64Vector2D(
            tuple.Item1,
            tuple.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToTuple2D(this IPair<float> tuple)
    {
        return new Float64Vector2D(
            tuple.Item1,
            tuple.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToLinVector2D(this IPair<double> tuple)
    {
        return tuple as Float64Vector2D
               ?? new Float64Vector2D(
                   tuple.Item1,
                   tuple.Item2
               );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToLinVector2D<T>(this IPair<T> tuple, Func<T, double> scalarMapping)
    {
        return tuple is Float64Vector2D vector
            ? vector
            : new Float64Vector2D(
                scalarMapping(tuple.Item1),
                scalarMapping(tuple.Item2)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetRealTuple(this IComplexTuple2D tuple)
    {
        return new Float64Vector2D(
            tuple.X.Real,
            tuple.Y.Real
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetImaginaryTuple(this IComplexTuple2D tuple)
    {
        return new Float64Vector2D(
            tuple.X.Imaginary,
            tuple.Y.Imaginary
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
    public static Float64Vector2D Permute(this Float64Vector2D tuple, int xIndex, int yIndex)
    {
        return new Float64Vector2D(tuple[xIndex], tuple[yIndex]);
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
    public static Float64Vector2D SafePermute(this Float64Vector2D tuple, int xIndex, int yIndex)
    {
        return new Float64Vector2D(tuple[xIndex.Mod(2)], tuple[yIndex.Mod(2)]);
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
    public static IEnumerable<double> GetComponents(this IFloat64Tuple2D tuple)
    {
        yield return tuple.X;
        yield return tuple.Y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetComponents(this IEnumerable<IFloat64Tuple2D> tuplesList)
    {
        return tuplesList.SelectMany(t => t.GetComponents());
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
}