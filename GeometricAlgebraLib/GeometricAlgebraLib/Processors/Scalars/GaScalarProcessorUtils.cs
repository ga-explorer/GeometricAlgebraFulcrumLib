using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.Processors.Tuples;

namespace GeometricAlgebraLib.Processors.Scalars
{
    public static class GaScalarProcessorUtils
    {
        public static T PiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, int denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    scalarProcessor.IntegerToScalar(numerator),
                    scalarProcessor.PiScalar
                ),
                scalarProcessor.IntegerToScalar(denominator)
            );
        }

        public static T PiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, int denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    numerator,
                    scalarProcessor.PiScalar
                ),
                scalarProcessor.IntegerToScalar(denominator)
            );
        }

        public static T PiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    scalarProcessor.IntegerToScalar(numerator),
                    scalarProcessor.PiScalar
                ),
                denominator
            );
        }

        public static T PiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, T denominator)
        {
            return scalarProcessor.Divide(
                scalarProcessor.Times(
                    numerator,
                    scalarProcessor.PiScalar
                ),
                denominator
            );
        }

        public static T CosPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, int denominator)
        {
            return scalarProcessor.Cos(
                    scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.IntegerToScalar(numerator),
                        scalarProcessor.PiScalar
                    ),
                    scalarProcessor.IntegerToScalar(denominator)
                )
            );
        }

        public static T CosPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, int denominator)
        {
            return scalarProcessor.Cos(
                    scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.PiScalar
                    ),
                    scalarProcessor.IntegerToScalar(denominator)
                )
            );
        }

        public static T CosPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Cos(
                    scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.IntegerToScalar(numerator),
                        scalarProcessor.PiScalar
                    ),
                    denominator
                )
            );
        }

        public static T CosPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, T denominator)
        {
            return scalarProcessor.Cos(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.PiScalar
                    ),
                    denominator
                )
            );
        }

        public static T SinPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, int denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.IntegerToScalar(numerator),
                        scalarProcessor.PiScalar
                    ),
                    scalarProcessor.IntegerToScalar(denominator)
                )
            );
        }

        public static T SinPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, int denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.PiScalar
                    ),
                    scalarProcessor.IntegerToScalar(denominator)
                )
            );
        }

        public static T SinPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, int numerator, T denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        scalarProcessor.IntegerToScalar(numerator),
                        scalarProcessor.PiScalar
                    ),
                    denominator
                )
            );
        }

        public static T SinPiRatio<T>(this IGaScalarProcessor<T> scalarProcessor, T numerator, T denominator)
        {
            return scalarProcessor.Sin(
                scalarProcessor.Divide(
                    scalarProcessor.Times(
                        numerator,
                        scalarProcessor.PiScalar
                    ),
                    denominator
                )
            );
        }

        public static T Sqrt<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.IntegerToScalar(scalar1)
            );
        }

        public static T Add<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T Add<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Add(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T Add<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Add(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }

        public static T Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Subtract(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Subtract(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }

        public static T Times<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Times(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Times(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T Times<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Times(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }

        public static T NegativeTimes<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T NegativeTimes<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T NegativeTimes<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.NegativeTimes(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }

        public static T SqrtRational<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Sqrt(
                scalarProcessor.Divide(
                    scalarProcessor.IntegerToScalar(scalar1),
                    scalarProcessor.IntegerToScalar(scalar2)
                )
            );
        }

        public static T Rational<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T Divide<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.Divide(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T Divide<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.Divide(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }
        
        public static T NegativeDivide<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalarProcessor.IntegerToScalar(scalar1),
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T NegativeDivide<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalar1,
                scalarProcessor.IntegerToScalar(scalar2)
            );
        }

        public static T NegativeDivide<T>(this IGaScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
        {
            return scalarProcessor.NegativeDivide(
                scalarProcessor.IntegerToScalar(scalar1),
                scalar2
            );
        }


        public static IEnumerable<KeyValuePair<TKey, TValue>> Negative<TKey, TValue>(this IGaScalarProcessor<TValue> scalarProcessor, IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            return keyValuePairs.Select(
                pair => new KeyValuePair<TKey, TValue>(pair.Key, scalarProcessor.Negative(pair.Value))
            );
        }


        public static IEnumerable<KeyValuePair<TKey, TValue>> Negative<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs, IGaScalarProcessor<TValue> scalarProcessor)
        {
            return keyValuePairs.Select(
                pair => new KeyValuePair<TKey, TValue>(pair.Key, scalarProcessor.Negative(pair.Value))
            );
        }


        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IGaScalarProcessor<T> itemScalarsDomain)
        {
            return new(itemScalarsDomain);
        }

        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IGaScalarProcessor<T> itemScalarsDomain, Dictionary<int, T> indexScalarDictionary)
        {
            return new(itemScalarsDomain, indexScalarDictionary);
        }

        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IGaScalarProcessor<T> itemScalarsDomain, IEnumerable<KeyValuePair<int, T>> indexScalarPairs)
        {
            return new(itemScalarsDomain, indexScalarPairs);
        }

        public static GaSparseTupleComposer<T> CreateSparseScalarsTupleComposer<T>(this IGaScalarProcessor<T> itemScalarsDomain, IEnumerable<Tuple<int, T>> indexScalarTuples)
        {
            return new(itemScalarsDomain, indexScalarTuples);
        }
    }
}