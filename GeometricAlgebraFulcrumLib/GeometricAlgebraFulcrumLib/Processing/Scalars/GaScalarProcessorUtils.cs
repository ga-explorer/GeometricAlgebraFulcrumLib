using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Tuples;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars
{
    public static class GaScalarProcessorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotZero<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar)
        {
            return !scalarProcessor.IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.IsZero(
                scalarProcessor.Subtract(scalar, scalarProcessor.GetOneScalar())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMinusOne<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.IsZero(
                scalarProcessor.Subtract(scalar, scalarProcessor.GetMinusOneScalar())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, int denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    scalarProcessor.IntegerToScalar(numerator),
                    scalarProcessor.GetPiScalar()
                ),
                scalarProcessor.IntegerToScalar(denominator)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, int denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    numerator,
                    scalarProcessor.GetPiScalar()
                ),
                scalarProcessor.IntegerToScalar(denominator)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    scalarProcessor.IntegerToScalar(numerator),
                    scalarProcessor.GetPiScalar()
                ),
                denominator
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, T denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    numerator,
                    scalarProcessor.GetPiScalar()
                ),
                denominator
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CosPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, int denominator)
        {
            return scalarProcessor.Cos(
                    scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.IntegerToScalar(numerator),
                        scalarProcessor.GetPiScalar()
                    ),
                    scalarProcessor.IntegerToScalar(denominator)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CosPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, int denominator)
        {
            return scalarProcessor.Cos(
                    scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.GetPiScalar()
                    ),
                    scalarProcessor.IntegerToScalar(denominator)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CosPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Cos(
                    scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.IntegerToScalar(numerator),
                        scalarProcessor.GetPiScalar()
                    ),
                    denominator
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CosPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, T denominator)
        {
            return scalarProcessor.Cos(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.GetPiScalar()
                    ),
                    denominator
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SinPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, int denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.IntegerToScalar(numerator),
                        scalarProcessor.GetPiScalar()
                    ),
                    scalarProcessor.IntegerToScalar(denominator)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SinPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, int denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.GetPiScalar()
                    ),
                    scalarProcessor.IntegerToScalar(denominator)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SinPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.IntegerToScalar(numerator),
                        scalarProcessor.GetPiScalar()
                    ),
                    denominator
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SinPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, T denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.GetPiScalar()
                    ),
                    denominator
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sqrt<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.IntegerToScalar(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sqrt<T>(this IGaScalarProcessor<T> scalarProcessor, double scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Float64ToScalar(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtOfNegative<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Negative(scalarProcessor.IntegerToScalar(scalar1))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtOfNegative<T>(this IGaScalarProcessor<T> scalarProcessor, double scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Negative(scalarProcessor.Float64ToScalar(scalar1))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtOfNegative<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Negative(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddOne<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1)
        {
            return scalarProcessor.Add(
                scalar1,
                scalarProcessor.GetOneScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddToOne<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.GetOneScalar(),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Add(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SubtractOne<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1)
        {
            return scalarProcessor.Subtract(
                scalar1,
                scalarProcessor.GetOneScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SubtractFromOne<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.GetOneScalar(),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Subtract(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Times(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Times(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Times(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IGaScalarProcessor<T> scalarProcessor, int intScalar, T scalar1, T scalar2)
        {
            return intScalar switch
            {
                0 => scalarProcessor.GetZeroScalar(),
                1 => scalarProcessor.Times(scalar1, scalar2),
                -1 => scalarProcessor.NegativeTimes(scalar1, scalar2),
                _ => scalarProcessor.Times(
                        scalarProcessor.IntegerToScalar(intScalar),
                        scalarProcessor.Times(scalar1, scalar2)
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtRational<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Divide(
                    scalarProcessor.IntegerToScalar(scalar1),
                    scalarProcessor.IntegerToScalar(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Rational<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Square<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.Times(scalar, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<TKey, TValue>> Negative<TKey, TValue>(this IGaScalarProcessor<TValue> scalarProcessor, IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            return keyValuePairs.Select(
                pair => new KeyValuePair<TKey, TValue>(pair.Key, scalarProcessor.Negative(pair.Value))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<TKey, TValue>> Negative<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs, IGaScalarProcessor<TValue> scalarProcessor)
        {
            return keyValuePairs.Select(
                pair => new KeyValuePair<TKey, TValue>(pair.Key, scalarProcessor.Negative(pair.Value))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IGaScalarProcessor<T> itemScalarsDomain)
        {
            return new GaSparseTupleComposer<T>(itemScalarsDomain);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IGaScalarProcessor<T> itemScalarsDomain, Dictionary<int, T> indexScalarDictionary)
        {
            return new GaSparseTupleComposer<T>(itemScalarsDomain, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IGaScalarProcessor<T> itemScalarsDomain, IEnumerable<KeyValuePair<int, T>> indexScalarPairs)
        {
            return new GaSparseTupleComposer<T>(itemScalarsDomain, indexScalarPairs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IGaScalarProcessor<T> itemScalarsDomain, IEnumerable<Tuple<int, T>> indexScalarTuples)
        {
            return new GaSparseTupleComposer<T>(itemScalarsDomain, indexScalarTuples);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> RemoveZeroValues<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> evenDictionary)
        {
            return evenDictionary.FilterByValue(scalarProcessor.IsNotZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> RemoveZeroValues<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> evenDictionary, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? evenDictionary.FilterByValue(scalarProcessor.IsNotNearZero)
                : evenDictionary.FilterByValue(scalarProcessor.IsNotZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> RemoveNearZeroValues<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> evenDictionary)
        {
            return evenDictionary.FilterByValue(scalarProcessor.IsNotNearZero);
        }
    }
}