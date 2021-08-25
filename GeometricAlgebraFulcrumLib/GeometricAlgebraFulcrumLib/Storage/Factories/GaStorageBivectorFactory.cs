using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Factories
{
    public static class GaStorageBivectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> CopyToStorageBivector<T>(this IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            var evenDictionary = indexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return GaStorageBivector<T>.Create(evenDictionary);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBivector<T>(this IGaListEven<T> termsList)
        {
            return GaStorageBivector<T>.Create(termsList);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create<T>(int index1, int index2, T scalar)
        {
            if (index1 >= index2) 
                throw new InvalidOperationException();

            var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

            return GaStorageBivector<T>.Create(index, scalar);

        }

        public static GaStorageBivector<T> CreateStorageBivector<T>(this IGaScalarProcessor<T> scalarProcessor, int index1, int index2, T scalar)
        {
            if (index1 < index2)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

                return GaStorageBivector<T>.Create(index, scalar);
            }
            else if (index2 < index1)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index2, index1);

                return GaStorageBivector<T>.Create(index, scalarProcessor.Negative(scalar));
            }
            else
                throw new InvalidOperationException();
        }

        public static GaStorageBivector<T> CreateStorageBivector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index1, ulong index2, T scalar)
        {
            if (index1 < index2)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

                return GaStorageBivector<T>.Create(index, scalar);
            }
            else if (index2 < index1)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index2, index1);

                return GaStorageBivector<T>.Create(index, scalarProcessor.Negative(scalar));
            }
            else
                throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBivector<T>(this KeyValuePair<ulong, T> indexScalarPair)
        {
            return GaStorageBivector<T>.Create(
                indexScalarPair.Key,
                indexScalarPair.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBivector<T>(this IGaScalarProcessor<T> scalarProcessor, KeyValuePair<ulong, T> indexScalarPair)
        {
            return GaStorageBivector<T>.Create(
                indexScalarPair.Key,
                indexScalarPair.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBivector<T>(int index, T scalar)
        {
            return GaStorageBivector<T>.Create((ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBivector<T>(ulong index, T scalar)
        {
            return GaStorageBivector<T>.Create(
                index,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBivector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index, T scalar)
        {
            return GaStorageBivector<T>.Create(
                index,
                scalar
            );
        }

        public static GaStorageBivector<T> CreateStorageBasisBivector<T>(this IGaScalarProcessor<T> scalarProcessor, int index1, int index2)
        {
            if (index1 < index2)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

                return GaStorageBivector<T>.Create(index, scalarProcessor.GetOneScalar());
            }
            else if (index2 < index1)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index2, index1);

                return GaStorageBivector<T>.Create(index, scalarProcessor.GetMinusOneScalar());
            }
            else
                throw new InvalidOperationException();
        }

        public static GaStorageBivector<T> CreateStorageBasisBivector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index1, ulong index2)
        {
            if (index1 < index2)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

                return GaStorageBivector<T>.Create(index, scalarProcessor.GetOneScalar());
            }
            else if (index2 < index1)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index2, index1);

                return GaStorageBivector<T>.Create(index, scalarProcessor.GetMinusOneScalar());
            }
            else
                throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBasisBivector<T>(this IGaScalarProcessor<T> scalarProcessor, int index)
        {
            return GaStorageBivector<T>.Create(
                ((ulong) index),
                scalarProcessor.GetOneScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBasisBivector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index)
        {
            return GaStorageBivector<T>.Create(
                index,
                scalarProcessor.GetOneScalar()
            );
        }

        public static GaStorageBivector<T> CreateStorageBasisBivectorNegative<T>(this IGaScalarProcessor<T> scalarProcessor, int index1, int index2)
        {
            if (index1 < index2)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

                return GaStorageBivector<T>.Create(index, scalarProcessor.GetMinusOneScalar());
            }
            else if (index2 < index1)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index2, index1);

                return GaStorageBivector<T>.Create(index, scalarProcessor.GetOneScalar());
            }
            else
                throw new InvalidOperationException();
        }

        public static GaStorageBivector<T> CreateStorageBasisBivectorNegative<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index1, ulong index2)
        {
            if (index1 < index2)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index1, index2);

                return GaStorageBivector<T>.Create(index, scalarProcessor.GetMinusOneScalar());
            }
            else if (index2 < index1)
            {
                var index = GaBasisBivectorUtils.BasisBivectorIndex(index2, index1);

                return GaStorageBivector<T>.Create(index, scalarProcessor.GetOneScalar());
            }
            else
                throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBasisBivectorNegative<T>(this IGaScalarProcessor<T> scalarProcessor, int index)
        {
            return GaStorageBivector<T>.Create(
                ((ulong) index),
                scalarProcessor.GetMinusOneScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBasisBivectorNegative<T>(this IGaScalarProcessor<T> scalarProcessor, ulong index)
        {
            return GaStorageBivector<T>.Create(
                index,
                scalarProcessor.GetMinusOneScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateZero<T>()
        {
            return GaStorageBivector<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageZeroBivector<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return GaStorageBivector<T>.ZeroBivector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBivector<T>(this Dictionary<ulong, T> indexScalarDictionary)
        {
            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> CreateStorageBivector<T>(this IGaScalarProcessor<T> scalarProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> SumToStorageBivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaRecordKeyValue<T>> termsList)
        {
            return scalarProcessor.CreateStorageKVectorComposer()
                .SetTerms(termsList)
                .RemoveZeroTerms()
                .CreateStorageBivector();
        }
    }
}