using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Combinations;

namespace GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra
{
    public static class ScalarProcessorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> CreateScalarComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return ScalarComposer<T>.Create(scalarProcessor);
        }

        //public static bool ValidateEqual<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, T scalar2)
        //{
        //    scalarProcessor.Subtract(scalar1, scalar2).
        //}

        public static T[] CreateArrayZero1D<T>(this IScalarProcessor<T> scalarProcessor, int count)
        {
            var exprArray = new T[count];

            for (var i = 0; i < count; i++)
                exprArray[i] = scalarProcessor.ScalarZero;

            return exprArray;
        }

        public static T[,] CreateArrayZero2D<T>(this IScalarProcessor<T> scalarProcessor, int count)
        {
            var exprArray = new T[count, count];

            for (var i = 0; i < count; i++)
                for (var j = 0; j < count; j++)
                    exprArray[i, j] = scalarProcessor.ScalarZero;

            return exprArray;
        }

        public static T[,] CreateArrayZero2D<T>(this IScalarProcessor<T> scalarProcessor, int count1, int count2)
        {
            var exprArray = new T[count1, count2];

            for (var i = 0; i < count1; i++)
                for (var j = 0; j < count2; j++)
                    exprArray[i, j] = scalarProcessor.ScalarZero;

            return exprArray;
        }

        public static T[,] CreateArrayIdentity2D<T>(this IScalarProcessor<T> scalarProcessor, int size)
        {
            var matrix = new T[size, size];

            for (var i = 0; i < size; i++)
            {
                matrix[i, i] = scalarProcessor.ScalarOne;

                for (var j = 0; j < i; j++)
                    matrix[i, j] = scalarProcessor.ScalarZero;

                for (var j = i + 1; j < size; j++)
                    matrix[i, j] = scalarProcessor.ScalarZero;
            }

            return matrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HaveOppositeSign<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return scalarProcessor.IsPositive(scalar1) && scalarProcessor.IsNegative(scalar2) ||
                   scalarProcessor.IsNegative(scalar1) && scalarProcessor.IsPositive(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllSameSign<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return scalarProcessor.AllPositive(scalar1, scalar2) ||
                   scalarProcessor.AllNegative(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllSameSign<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
        {
            return scalarProcessor.AllPositive(scalar1, scalar2, scalar3) ||
                   scalarProcessor.AllNegative(scalar1, scalar2, scalar3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllSameSign<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
        {
            return scalarList.All(scalarProcessor.IsPositive) ||
                   scalarList.All(scalarProcessor.IsNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllZeroOrSameSign<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return scalarProcessor.AllZeroOrPositive(scalar1, scalar2) ||
                   scalarProcessor.AllZeroOrNegative(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllZeroOrSameSign<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
        {
            return scalarProcessor.AllZeroOrPositive(scalar1, scalar2, scalar3) ||
                   scalarProcessor.AllZeroOrNegative(scalar1, scalar2, scalar3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllZeroOrSameSign<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
        {
            return scalarList.All(scalarProcessor.IsZeroOrPositive) ||
                   scalarList.All(scalarProcessor.IsZeroOrNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return scalarProcessor.IsPositive(scalar1) &&
                   scalarProcessor.IsPositive(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
        {
            return scalarProcessor.IsPositive(scalar1) &&
                   scalarProcessor.IsPositive(scalar2) &&
                   scalarProcessor.IsPositive(scalar3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllPositive<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
        {
            return scalarList.All(scalarProcessor.IsPositive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return scalarProcessor.IsNegative(scalar1) &&
                   scalarProcessor.IsNegative(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
        {
            return scalarProcessor.IsNegative(scalar1) &&
                   scalarProcessor.IsNegative(scalar2) &&
                   scalarProcessor.IsNegative(scalar3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNegative<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
        {
            return scalarList.All(scalarProcessor.IsNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllZeroOrPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return scalarProcessor.IsZeroOrPositive(scalar1) &&
                   scalarProcessor.IsZeroOrPositive(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllZeroOrPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
        {
            return scalarProcessor.IsZeroOrPositive(scalar1) &&
                   scalarProcessor.IsZeroOrPositive(scalar2) &&
                   scalarProcessor.IsZeroOrPositive(scalar3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllZeroOrPositive<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
        {
            return scalarList.All(scalarProcessor.IsZeroOrPositive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllZeroOrNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return scalarProcessor.IsZeroOrNegative(scalar1) &&
                   scalarProcessor.IsZeroOrNegative(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllZeroOrNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
        {
            return scalarProcessor.IsZeroOrNegative(scalar1) &&
                   scalarProcessor.IsZeroOrNegative(scalar2) &&
                   scalarProcessor.IsZeroOrNegative(scalar3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllZeroOrNegative<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
        {
            return scalarList.All(scalarProcessor.IsZeroOrNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroOrPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.IsZero(scalar) ||
                   scalarProcessor.IsPositive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroOrNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.IsZero(scalar) ||
                   scalarProcessor.IsNegative(scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotZero<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return !scalarProcessor.IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.IsZero(
                scalarProcessor.Subtract(scalar, scalarProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMinusOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.IsZero(
                scalarProcessor.Subtract(scalar, scalarProcessor.ScalarMinusOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, int angleInDegrees)
        {
            angleInDegrees = angleInDegrees switch
            {
                < -360 => angleInDegrees % 720 + 360,
                > 360 => angleInDegrees % 360,
                _ => angleInDegrees
            };

            return scalarProcessor.Times(
                scalarProcessor.ScalarDegreeToRadian,
                scalarProcessor.GetScalarFromNumber(angleInDegrees)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, long angleInDegrees)
        {
            angleInDegrees = angleInDegrees switch
            {
                < -360L => angleInDegrees % 720L + 360L,
                > 360L => angleInDegrees % 360L,
                _ => angleInDegrees
            };

            return scalarProcessor.Times(
                scalarProcessor.ScalarDegreeToRadian,
                scalarProcessor.GetScalarFromNumber(angleInDegrees)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, float angleInDegrees)
        {
            angleInDegrees = angleInDegrees switch
            {
                < -360f => angleInDegrees % 720f + 360f,
                > 360f => angleInDegrees % 360f,
                _ => angleInDegrees
            };

            return scalarProcessor.Times(
                scalarProcessor.ScalarDegreeToRadian,
                scalarProcessor.GetScalarFromNumber(angleInDegrees)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, double angleInDegrees)
        {
            angleInDegrees = angleInDegrees switch
            {
                < -360d => angleInDegrees % 720d + 360d,
                > 360d => angleInDegrees % 360d,
                _ => angleInDegrees
            };

            return scalarProcessor.Times(
                scalarProcessor.ScalarDegreeToRadian,
                scalarProcessor.GetScalarFromNumber(angleInDegrees)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, T angleInDegrees)
        {
            return scalarProcessor.Times(
                scalarProcessor.ScalarDegreeToRadian,
                angleInDegrees
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RadiansToDegrees<T>(this IScalarProcessor<T> scalarProcessor, float angleInRadians)
        {
            return scalarProcessor.Times(
                scalarProcessor.ScalarRadianToDegree,
                scalarProcessor.GetScalarFromNumber(angleInRadians)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RadiansToDegrees<T>(this IScalarProcessor<T> scalarProcessor, double angleInRadians)
        {
            return scalarProcessor.Times(
                scalarProcessor.ScalarRadianToDegree,
                scalarProcessor.GetScalarFromNumber(angleInRadians)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RadiansToDegrees<T>(this IScalarProcessor<T> scalarProcessor, T angleInRadians)
        {
            return scalarProcessor.Times(
                scalarProcessor.ScalarRadianToDegree,
                angleInRadians
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, int denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    scalarProcessor.GetScalarFromNumber(numerator),
                    scalarProcessor.ScalarPi
                ),
                scalarProcessor.GetScalarFromNumber(denominator)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PiRatio<T>(this IScalarProcessor<T> scalarProcessor, T numerator, int denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    numerator,
                    scalarProcessor.ScalarPi
                ),
                scalarProcessor.GetScalarFromNumber(denominator)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    scalarProcessor.GetScalarFromNumber(numerator),
                    scalarProcessor.ScalarPi
                ),
                denominator
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PiRatio<T>(this IScalarProcessor<T> scalarProcessor, T numerator, T denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    numerator,
                    scalarProcessor.ScalarPi
                ),
                denominator
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CosPiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, int denominator)
        {
            return scalarProcessor.Cos(
                    scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.GetScalarFromNumber(numerator),
                        scalarProcessor.ScalarPi
                    ),
                    scalarProcessor.GetScalarFromNumber(denominator)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CosPiRatio<T>(this IScalarProcessor<T> scalarProcessor, T numerator, int denominator)
        {
            return scalarProcessor.Cos(
                    scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.ScalarPi
                    ),
                    scalarProcessor.GetScalarFromNumber(denominator)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CosPiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Cos(
                    scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.GetScalarFromNumber(numerator),
                        scalarProcessor.ScalarPi
                    ),
                    denominator
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CosPiRatio<T>(this IScalarProcessor<T> scalarProcessor, T numerator, T denominator)
        {
            return scalarProcessor.Cos(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.ScalarPi
                    ),
                    denominator
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SinPiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, int denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.GetScalarFromNumber(numerator),
                        scalarProcessor.ScalarPi
                    ),
                    scalarProcessor.GetScalarFromNumber(denominator)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SinPiRatio<T>(this IScalarProcessor<T> scalarProcessor, T numerator, int denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.ScalarPi
                    ),
                    scalarProcessor.GetScalarFromNumber(denominator)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SinPiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.GetScalarFromNumber(numerator),
                        scalarProcessor.ScalarPi
                    ),
                    denominator
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SinPiRatio<T>(this IScalarProcessor<T> scalarProcessor, T numerator, T denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.ScalarPi
                    ),
                    denominator
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sqrt<T>(this IScalarProcessor<T> scalarProcessor, int scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.GetScalarFromNumber(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sqrt<T>(this IScalarProcessor<T> scalarProcessor, double scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.GetScalarFromNumber(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, int scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Negative(scalarProcessor.GetScalarFromNumber(scalar1))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, double scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Negative(scalarProcessor.GetScalarFromNumber(scalar1))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Negative(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar1)
        {
            return scalarProcessor.Add(
                scalar1,
                scalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddToOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.ScalarOne,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Add(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SubtractOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar1)
        {
            return scalarProcessor.Subtract(
                scalar1,
                scalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SubtractFromOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.ScalarOne,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Subtract(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
        {
            return scalarProcessor.Subtract(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Times(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Times(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Times(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign sign, T scalar1, T scalar2)
        {
            if (sign.IsZero)
                return scalarProcessor.ScalarZero;

            return sign.IsPositive
                ? scalarProcessor.Times(scalar1, scalar2)
                : scalarProcessor.NegativeTimes(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarProcessor<T> scalarProcessor, int intScalar, T scalar1, T scalar2)
        {
            return intScalar switch
            {
                0 => scalarProcessor.ScalarZero,
                1 => scalarProcessor.Times(scalar1, scalar2),
                -1 => scalarProcessor.NegativeTimes(scalar1, scalar2),
                _ => scalarProcessor.Times(
                        scalarProcessor.GetScalarFromNumber(intScalar),
                        scalarProcessor.Times(scalar1, scalar2)
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtRational<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Divide(
                    scalarProcessor.GetScalarFromNumber(scalar1),
                    scalarProcessor.GetScalarFromNumber(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Rational<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Square<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.Times(scalar, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Cube<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.Times(scalar, scalarProcessor.Times(scalar, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, float scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T BinomialCoefficient<T>(this IScalarProcessor<T> scalarProcessor, uint setSize, uint subsetSize)
        {
            return scalarProcessor.GetScalarFromNumber(setSize.GetBinomialCoefficient(subsetSize));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T BinomialCoefficient<T>(this IScalarProcessor<T> scalarProcessor, int setSize, int subsetSize)
        {
            return scalarProcessor.GetScalarFromNumber(setSize.GetBinomialCoefficient(subsetSize));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sign<T>(this IScalarProcessor<T> scalarProcessor, T scalar, T offsetScalar)
        {
            return scalarProcessor.Sign(
                scalarProcessor.Subtract(scalar, offsetScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T BoxCar<T>(this IScalarProcessor<T> scalarProcessor, T scalar, T scalarA, T scalarB)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.UnitStep(scalarProcessor.Subtract(scalar, scalarA)),
                scalarProcessor.UnitStep(scalarProcessor.Subtract(scalar, scalarB))
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<TKey, TValue>> Negative<TKey, TValue>(this IScalarProcessor<TValue> scalarProcessor, IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            return keyValuePairs.Select(
                pair => new KeyValuePair<TKey, TValue>(pair.Key, scalarProcessor.Negative(pair.Value))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<TKey, TValue>> Negative<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs, IScalarProcessor<TValue> scalarProcessor)
        {
            return keyValuePairs.Select(
                pair => new KeyValuePair<TKey, TValue>(pair.Key, scalarProcessor.Negative(pair.Value))
            );
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static SparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarAlgebraProcessor<T> itemScalarsDomain)
        //{
        //    return new SparseTupleComposer<T>(itemScalarsDomain);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static SparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarAlgebraProcessor<T> itemScalarsDomain, Dictionary<int, T> indexScalarDictionary)
        //{
        //    return new SparseTupleComposer<T>(itemScalarsDomain, indexScalarDictionary);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static SparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarAlgebraProcessor<T> itemScalarsDomain, IEnumerable<KeyValuePair<int, T>> indexScalarPairs)
        //{
        //    return new SparseTupleComposer<T>(itemScalarsDomain, indexScalarPairs);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static SparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarAlgebraProcessor<T> itemScalarsDomain, IEnumerable<Tuple<int, T>> indexScalarTuples)
        //{
        //    return new SparseTupleComposer<T>(itemScalarsDomain, indexScalarTuples);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToScalarValue<T>(this IntegerSign sign, IScalarProcessor<T> scalarProcessor)
        {
            if (sign.IsZero)
                return scalarProcessor.ScalarZero;

            return sign.IsPositive
                ? scalarProcessor.ScalarOne
                : scalarProcessor.ScalarMinusOne;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ToScalar<T>(this IntegerSign sign, IScalarProcessor<T> scalarProcessor)
        {
            if (sign.IsZero)
                return scalarProcessor.CreateScalarZero();

            return sign.IsPositive
                ? scalarProcessor.CreateScalarOne()
                : scalarProcessor.CreateScalarMinusOne();
        }
    }
}