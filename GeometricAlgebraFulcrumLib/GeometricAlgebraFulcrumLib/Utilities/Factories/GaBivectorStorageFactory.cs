using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaBivectorStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> CopyToBivectorStorage<T>(this IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            var evenDictionary = indexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return GaBivectorStorage<T>.Create(evenDictionary);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this ILaVectorEvenStorage<T> termsList)
        {
            return GaBivectorStorage<T>.Create(termsList);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateBivectorStorage<T>(int index1, int index2, T scalar)
        {
            if (index1 >= index2) 
                throw new InvalidOperationException();

            var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

            return GaBivectorStorage<T>.Create(index, scalar);

        }

        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, int index1, int index2, T scalar)
        {
            if (index1 < index2)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

                return GaBivectorStorage<T>.Create(index, scalar);
            }
            else if (index2 < index1)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index2, index1);

                return GaBivectorStorage<T>.Create(index, scalarProcessor.Negative(scalar));
            }
            else
                throw new InvalidOperationException();
        }

        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, ulong index1, ulong index2, T scalar)
        {
            if (index1 < index2)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

                return GaBivectorStorage<T>.Create(index, scalar);
            }
            else if (index2 < index1)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index2, index1);

                return GaBivectorStorage<T>.Create(index, scalarProcessor.Negative(scalar));
            }
            else
                throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this KeyValuePair<ulong, T> indexScalarPair)
        {
            return GaBivectorStorage<T>.Create(
                indexScalarPair.Key,
                indexScalarPair.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, KeyValuePair<ulong, T> indexScalarPair)
        {
            return GaBivectorStorage<T>.Create(
                indexScalarPair.Key,
                indexScalarPair.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateBivectorStorage<T>(int index, T scalar)
        {
            return GaBivectorStorage<T>.Create((ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateBivectorStorage<T>(ulong index, T scalar)
        {
            return GaBivectorStorage<T>.Create(
                index,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, ulong index, T scalar)
        {
            return GaBivectorStorage<T>.Create(
                index,
                scalar
            );
        }

        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, int index1, int index2)
        {
            if (index1 < index2)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

                return GaBivectorStorage<T>.Create(index, scalarProcessor.ScalarOne);
            }
            else if (index2 < index1)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index2, index1);

                return GaBivectorStorage<T>.Create(index, scalarProcessor.ScalarMinusOne);
            }
            else
                throw new InvalidOperationException();
        }

        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, ulong index1, ulong index2)
        {
            if (index1 < index2)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

                return GaBivectorStorage<T>.Create(index, scalarProcessor.ScalarOne);
            }
            else if (index2 < index1)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index2, index1);

                return GaBivectorStorage<T>.Create(index, scalarProcessor.ScalarMinusOne);
            }
            else
                throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, int index)
        {
            return GaBivectorStorage<T>.Create(
                ((ulong) index),
                scalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, ulong index)
        {
            return GaBivectorStorage<T>.Create(
                index,
                scalarProcessor.ScalarOne
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateZeroBivectorStorage<T>()
        {
            return GaBivectorStorage<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateZeroBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return GaBivectorStorage<T>.ZeroBivector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this Dictionary<ulong, T> indexScalarDictionary)
        {
            return GaBivectorStorage<T>.Create(indexScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> CreateBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return GaBivectorStorage<T>.Create(indexScalarDictionary);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> SumToBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return scalarProcessor.CreateKVectorStorageComposer()
                .SetTerms(termsList)
                .RemoveZeroTerms()
                .CreateGaBivectorStorage();
        }
    }
}