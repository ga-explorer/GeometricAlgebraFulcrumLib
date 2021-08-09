using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Factories
{
    public static class GaStorageVectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerVector<T> CreateStorageComposerVector<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaStorageComposerVector<T>(
                scalarProcessor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerVector<T> CreateStorageComposerVector<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, T> indexScalarsDictionary) 
        {
            return new GaStorageComposerVector<T>(
                scalarProcessor,
                indexScalarsDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerVector<T> CreateStorageComposerVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs) 
        {
            return new GaStorageComposerVector<T>(
                scalarProcessor,
                indexScalarPairs
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerVector<T> CreateStorageComposerVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            return new GaStorageComposerVector<T>(
                scalarProcessor,
                indexScalarTuples
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> CopyToStorageVector<T>(this IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            var evenDictionary = indexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return GaStorageVector<T>.Create(evenDictionary);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateZero<T>()
        {
            return GaStorageVector<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageZeroVector<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return GaStorageVector<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create<T>(int index, T scalar)
        {
            return GaStorageVector<T>.Create((ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create<T>(ulong index, T scalar)
        {
            return GaStorageVector<T>.Create(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, int index, T scalar)
        {
            return GaStorageVector<T>.Create((ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index, T scalar)
        {
            return GaStorageVector<T>.Create(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this KeyValuePair<ulong, T> indexScalarPair)
        {
            return GaStorageVector<T>.Create(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, KeyValuePair<ulong, T> indexScalarPair)
        {
            return GaStorageVector<T>.Create(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageBasisVector<T>(this IGaScalarProcessor<T> scalarProcessor, int index)
        {
            return GaStorageVector<T>.Create((ulong) index, scalarProcessor.OneScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageBasisVector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index)
        {
            return GaStorageVector<T>.Create(index, scalarProcessor.OneScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageBasisVectorNegative<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index)
        {
            return GaStorageVector<T>.Create(index, scalarProcessor.MinusOneScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageOnesVector<T>(this IGaScalarProcessor<T> scalarProcessor, int termsCount)
        {
            return GaStorageVector<T>.Create(
                scalarProcessor
                    .OneScalar
                    .CreateEvenDictionaryRepeatedValue(termsCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageUnitOnesVector<T>(this IGaScalarProcessor<T> scalarProcessor, int termsCount)
        {
            var length = scalarProcessor.Sqrt(termsCount);

            return GaStorageVector<T>.Create(
                scalarProcessor
                    .Divide(scalarProcessor.OneScalar, length)
                    .CreateEvenDictionaryRepeatedValue(termsCount)
            );
        }
        
        public static GaStorageVector<T> CreateStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint termsCount, Func<ulong, T> indexScalarFunc)
        {
            if (termsCount == 0)
                return GaStorageVector<T>.ZeroVector;

            if (termsCount == 1) 
                return GaStorageVector<T>.Create(0, indexScalarFunc(0));

            var indexScalarDictionary =
                ((ulong) termsCount).RangeToDictionary(indexScalarFunc);

            return GaStorageVector<T>.Create(indexScalarDictionary);

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this Dictionary<ulong, T> indexScalarDictionary)
        {
            return GaStorageVector<T>.Create(indexScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create<T>(params T[] scalarArray)
        {
            return GaStorageVector<T>.Create(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IReadOnlyList<T> scalarList)
        {
            return GaStorageVector<T>.Create(scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IEnumerable<T> scalarList)
        {
            return GaStorageVector<T>.Create(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            return GaStorageVector<T>.Create(termsList.CopyToDictionary());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IEnumerable<Tuple<ulong, T>> termsList)
        {
            return GaStorageVector<T>.Create(
                termsList.ToDictionary(
                    t => t.Item1, 
                    t => t.Item2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return GaStorageVector<T>.Create(
                termsList.ToDictionary(
                    t => t.BasisBlade.Index, 
                    t => t.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, params T[] scalarArray)
        {
            return GaStorageVector<T>.Create(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> scalarList)
        {
            return GaStorageVector<T>.Create(scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
        {
            return GaStorageVector<T>.Create(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            return GaStorageVector<T>.Create(termsList.CopyToDictionary());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            return GaStorageVector<T>.Create(
                termsList.ToDictionary(
                    t => t.Item1, 
                    t => t.Item2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> CreateStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaTerm<T>> termsList)
        {
            return GaStorageVector<T>.Create(
                termsList.ToDictionary(
                    t => t.BasisBlade.Index, 
                    t => t.Scalar
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> SumToStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            return scalarProcessor
                .CreateStorageComposerVector(termsList)
                .RemoveZeroTerms()
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> SumToStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            return scalarProcessor
                .CreateStorageComposerVector(termsList)
                .RemoveZeroTerms()
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> SumToStorageVector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaTerm<T>> termsList)
        {
            return scalarProcessor
                .CreateStorageComposerVector()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .GetVector();
        }
    }
}