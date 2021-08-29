using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.Tuples;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaScalarProcessorUtils
    {
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
        public static T PiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, int denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    scalarProcessor.GetScalarFromInteger(numerator),
                    scalarProcessor.ScalarPi
                ),
                scalarProcessor.GetScalarFromInteger(denominator)
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
                scalarProcessor.GetScalarFromInteger(denominator)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    scalarProcessor.GetScalarFromInteger(numerator),
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
                        scalarProcessor.GetScalarFromInteger(numerator),
                        scalarProcessor.ScalarPi
                    ),
                    scalarProcessor.GetScalarFromInteger(denominator)
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
                    scalarProcessor.GetScalarFromInteger(denominator)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CosPiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Cos(
                    scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.GetScalarFromInteger(numerator),
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
                        scalarProcessor.GetScalarFromInteger(numerator),
                        scalarProcessor.ScalarPi
                    ),
                    scalarProcessor.GetScalarFromInteger(denominator)
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
                    scalarProcessor.GetScalarFromInteger(denominator)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SinPiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.GetScalarFromInteger(numerator),
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
                scalarProcessor.GetScalarFromInteger(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sqrt<T>(this IScalarProcessor<T> scalarProcessor, double scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.GetScalarFromFloat64(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, int scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Negative(scalarProcessor.GetScalarFromInteger(scalar1))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, double scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Negative(scalarProcessor.GetScalarFromFloat64(scalar1))
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
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Add(
                scalar1,
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.GetScalarFromInteger(scalar1),
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
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Subtract(
                scalar1,
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Times(
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Times(
                scalar1,
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Times(
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalar2
            );
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
                        scalarProcessor.GetScalarFromInteger(intScalar),
                        scalarProcessor.Times(scalar1, scalar2)
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalar1,
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtRational<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Divide(
                    scalarProcessor.GetScalarFromInteger(scalar1),
                    scalarProcessor.GetScalarFromInteger(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Rational<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalar1,
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalar2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalar1,
                scalarProcessor.GetScalarFromInteger(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalarProcessor.GetScalarFromInteger(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Square<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.Times(scalar, scalar);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarProcessor<T> itemScalarsDomain)
        {
            return new GaSparseTupleComposer<T>(itemScalarsDomain);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarProcessor<T> itemScalarsDomain, Dictionary<int, T> indexScalarDictionary)
        {
            return new GaSparseTupleComposer<T>(itemScalarsDomain, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarProcessor<T> itemScalarsDomain, IEnumerable<KeyValuePair<int, T>> indexScalarPairs)
        {
            return new GaSparseTupleComposer<T>(itemScalarsDomain, indexScalarPairs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarProcessor<T> itemScalarsDomain, IEnumerable<Tuple<int, T>> indexScalarTuples)
        {
            return new GaSparseTupleComposer<T>(itemScalarsDomain, indexScalarTuples);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> RemoveZeroValues<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> evenDictionary)
        {
            return evenDictionary.FilterByScalar(scalarProcessor.IsNotZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> RemoveZeroValues<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> evenDictionary, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? evenDictionary.FilterByScalar(scalarProcessor.IsNotNearZero)
                : evenDictionary.FilterByScalar(scalarProcessor.IsNotZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> RemoveNearZeroValues<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> evenDictionary)
        {
            return evenDictionary.FilterByScalar(scalarProcessor.IsNotNearZero);
        }
    }
}