using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class KVectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> storage)
        {
            return new KVector<T>(processor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this KVectorStorage<T> storage, IGeometricAlgebraProcessor<T> processor)
        {
            return new KVector<T>(processor, storage);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CopyToKVector<T>(this IGeometricAlgebraProcessor<T> processor, IReadOnlyDictionary<ulong, T> indexScalarDictionary, uint grade)
        {
            return indexScalarDictionary.CopyToKVectorStorage(grade).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CopyToKVector<T>(this IReadOnlyDictionary<ulong, T> indexScalarDictionary, IGeometricAlgebraProcessor<T> processor, uint grade)
        {
            return indexScalarDictionary.CopyToKVectorStorage(grade).CreateKVector(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorPseudoScalar<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            return processor.CreateKVectorStoragePseudoScalar(vSpaceDimension).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorPseudoScalar<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return processor.CreateKVectorStoragePseudoScalar().CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorPseudoScalarReverse<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            return processor.CreateKVectorStoragePseudoScalarReverse(vSpaceDimension).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorEuclideanPseudoScalarInverse<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            return processor.CreateKVectorStorageEuclideanPseudoScalarInverse(vSpaceDimension).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorPseudoScalarInverse<T>(this IGeometricAlgebraProcessor<T> processor, BasisBladeSet basisSet)
        {
            return processor.CreateKVectorStoragePseudoScalarInverse(basisSet).CreateKVector(processor);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorZero<T>(this IGeometricAlgebraProcessor<T> processor, uint grade)
        {
            return processor.CreateKVectorStorageZero(grade).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorBasisScalar<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return processor.CreateKVectorStorageBasisScalar().CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorBasis<T>(this IGeometricAlgebraProcessor<T> processor, params int[] basisVectorIndices)
        {
            return processor.CreateKVectorStorageBasis(basisVectorIndices).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorBasis<T>(this IGeometricAlgebraProcessor<T> processor, params ulong[] basisVectorIndices)
        {
            return processor.CreateKVectorStorageBasis(basisVectorIndices).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorBasis<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<int> basisVectorIndices)
        {
            return processor.CreateKVectorStorageBasis(basisVectorIndices).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorBasis<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<ulong> basisVectorIndices)
        {
            return processor.CreateKVectorStorageBasis(basisVectorIndices).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorBasis<T>(this IGeometricAlgebraProcessor<T> processor, ulong id)
        {
            return processor.CreateKVectorStorageBasis(id).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorBasis<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, ulong index)
        {
            return processor.CreateKVectorStorageBasis(grade, index).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorScalar<T>(this IGeometricAlgebraProcessor<T> processor, int scalar)
        {
            return processor.CreateKVectorStorageScalar(scalar).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorScalar<T>(this IGeometricAlgebraProcessor<T> processor, uint scalar)
        {
            return processor.CreateKVectorStorageScalar(scalar).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorScalar<T>(this IGeometricAlgebraProcessor<T> processor, long scalar)
        {
            return processor.CreateKVectorStorageScalar(scalar).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorScalar<T>(this IGeometricAlgebraProcessor<T> processor, ulong scalar)
        {
            return processor.CreateKVectorStorageScalar(scalar).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorScalar<T>(this IGeometricAlgebraProcessor<T> processor, float scalar)
        {
            return processor.CreateKVectorStorageScalar(scalar).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorScalar<T>(this IGeometricAlgebraProcessor<T> processor, double scalar)
        {
            return processor.CreateKVectorStorageScalar(scalar).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorScalar<T>(this IGeometricAlgebraProcessor<T> processor, T scalar)
        {
            return processor.CreateKVectorStorageScalar(scalar).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorScalar<T>(this IGeometricAlgebraProcessor<T> processor, string scalar)
        {
            return processor.CreateKVectorStorageScalar(scalar).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorScalar<T>(this IGeometricAlgebraProcessor<T> processor, object scalar)
        {
            return processor.CreateKVectorStorageScalar(scalar).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorTerm<T>(this IGeometricAlgebraProcessor<T> processor, BasisBlade basisBlade, T scalar)
        {
            return processor.CreateKVectorStorageTerm(basisBlade, scalar).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorTerm<T>(this IGeometricAlgebraProcessor<T> processor, ulong id, T scalar)
        {
            return processor.CreateKVectorStorageTerm(id, scalar).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorTerm<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, ulong index, T scalar)
        {
            return processor.CreateKVectorStorageTerm(grade, index, scalar).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorTerm<T>(this IGeometricAlgebraProcessor<T> processor, IndexScalarRecord<T> idScalarPair)
        {
            return processor.CreateKVectorStorageTerm(idScalarPair).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorTerm<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, IndexScalarRecord<T> indexScalarPair)
        {
            return processor.CreateKVectorStorageTerm(grade, indexScalarPair).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, params T[] scalarsList)
        {
            return processor.CreateKVectorStorage(grade, scalarsList).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, params object[] scalarsList)
        {
            return processor.CreateKVectorStorage(grade, scalarsList).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, IEnumerable<T> scalarsList)
        {
            return processor.CreateKVectorStorage(grade, scalarsList).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, IEnumerable<IndexScalarRecord<T>> termsList, bool sumFlag)
        {
            return sumFlag
                ? processor.SumToKVectorStorage(grade, termsList).CreateKVector(processor)
                : processor.CreateKVectorStorage(grade, termsList).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, IEnumerable<BasisTerm<T>> termsList)
        {
            return processor.CreateKVectorStorage(grade, termsList).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, Dictionary<ulong, T> indexScalarDictionary)
        {
            return processor.CreateKVectorStorage(grade, indexScalarDictionary).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return processor.CreateKVectorStorage(grade, termsList).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, ILinVectorStorage<T> termsList)
        {
            return termsList.CreateKVectorStorage(grade).CreateKVector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IGeometricAlgebraProcessor<T> processor, GradeLinVectorStorageRecord<T> gradeListRecord)
        {
            return gradeListRecord.CreateKVectorStorage().CreateKVector(processor);
        }


    }
}