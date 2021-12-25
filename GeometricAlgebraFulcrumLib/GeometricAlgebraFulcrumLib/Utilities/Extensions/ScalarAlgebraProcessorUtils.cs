using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.TupleAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class ScalarAlgebraProcessorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar)
        {
            return !scalarProcessor.IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.IsZero(
                scalarProcessor.Subtract(scalar, scalarProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMinusOne<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.IsZero(
                scalarProcessor.Subtract(scalar, scalarProcessor.ScalarMinusOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int numerator, int denominator)
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
        public static T PiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T numerator, int denominator)
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
        public static T PiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int numerator, T denominator)
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
        public static T PiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T numerator, T denominator)
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
        public static T CosPiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int numerator, int denominator)
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
        public static T CosPiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T numerator, int denominator)
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
        public static T CosPiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int numerator, T denominator)
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
        public static T CosPiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T numerator, T denominator)
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
        public static T SinPiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int numerator, int denominator)
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
        public static T SinPiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T numerator, int denominator)
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
        public static T SinPiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int numerator, T denominator)
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
        public static T SinPiRatio<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T numerator, T denominator)
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
        public static T Sqrt<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.GetScalarFromNumber(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sqrt<T>(this IScalarAlgebraProcessor<T> scalarProcessor, double scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.GetScalarFromNumber(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtOfNegative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Negative(scalarProcessor.GetScalarFromNumber(scalar1))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtOfNegative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, double scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Negative(scalarProcessor.GetScalarFromNumber(scalar1))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtOfNegative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Negative(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddOne<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1)
        {
            return scalarProcessor.Add(
                scalar1,
                scalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddToOne<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.ScalarOne,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Add(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SubtractOne<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1)
        {
            return scalarProcessor.Subtract(
                scalar1,
                scalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SubtractFromOne<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.ScalarOne,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Subtract(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Times(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Times(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Times(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int intScalar, T scalar1, T scalar2)
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
        public static T NegativeTimes<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SqrtRational<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Divide(
                    scalarProcessor.GetScalarFromNumber(scalar1),
                    scalarProcessor.GetScalarFromNumber(scalar2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Rational<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeDivide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Square<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar)
        {
            return scalarProcessor.Times(scalar, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, long scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, uint scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, float scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, double scalar2)
        {
            return scalarProcessor.Power(
                scalar1,
                scalarProcessor.GetScalarFromNumber(scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T BinomialCoefficient<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint setSize, uint subsetSize)
        {
            return scalarProcessor.GetScalarFromNumber(setSize.GetBinomialCoefficient(subsetSize));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T BinomialCoefficient<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int setSize, int subsetSize)
        {
            return scalarProcessor.GetScalarFromNumber(setSize.GetBinomialCoefficient(subsetSize));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<TKey, TValue>> Negative<TKey, TValue>(this IScalarAlgebraProcessor<TValue> scalarProcessor, IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            return keyValuePairs.Select(
                pair => new KeyValuePair<TKey, TValue>(pair.Key, scalarProcessor.Negative(pair.Value))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<TKey, TValue>> Negative<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs, IScalarAlgebraProcessor<TValue> scalarProcessor)
        {
            return keyValuePairs.Select(
                pair => new KeyValuePair<TKey, TValue>(pair.Key, scalarProcessor.Negative(pair.Value))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarAlgebraProcessor<T> itemScalarsDomain)
        {
            return new SparseTupleComposer<T>(itemScalarsDomain);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarAlgebraProcessor<T> itemScalarsDomain, Dictionary<int, T> indexScalarDictionary)
        {
            return new SparseTupleComposer<T>(itemScalarsDomain, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarAlgebraProcessor<T> itemScalarsDomain, IEnumerable<KeyValuePair<int, T>> indexScalarPairs)
        {
            return new SparseTupleComposer<T>(itemScalarsDomain, indexScalarPairs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IScalarAlgebraProcessor<T> itemScalarsDomain, IEnumerable<Tuple<int, T>> indexScalarTuples)
        {
            return new SparseTupleComposer<T>(itemScalarsDomain, indexScalarTuples);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> RemoveZeroValues<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> evenDictionary)
        {
            return evenDictionary.FilterByScalar(scalarProcessor.IsNotZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> RemoveZeroValues<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> evenDictionary, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? evenDictionary.FilterByScalar(scalarProcessor.IsNotNearZero)
                : evenDictionary.FilterByScalar(scalarProcessor.IsNotZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> RemoveNearZeroValues<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> evenDictionary)
        {
            return evenDictionary.FilterByScalar(scalarProcessor.IsNotNearZero);
        }
    }
}