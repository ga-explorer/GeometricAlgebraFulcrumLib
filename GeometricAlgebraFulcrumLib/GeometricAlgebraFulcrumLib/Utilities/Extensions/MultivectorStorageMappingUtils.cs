using System;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageMappingUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> OmMapPseudoScalar<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IOutermorphism<T> outermorphism)
        {
            return outermorphism.OmMapBasisBlade(geometricProcessor.MaxBasisBladeId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> MapMultivector<T>(this IUnilinearMap<T> map, IMultivectorStorage<T> mv)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => map.MapVector(mv1),
                BivectorStorage<T> mv1 => map.MapBivector(mv1),
                KVectorStorage<T> mv1 => map.MapKVector(mv1),
                MultivectorStorage<T> mv1 => map.MapMultivector(mv1),
                MultivectorGradedStorage<T> mv1 => map.MapMultivector(mv1),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> OmMapMultivector<T>(this IOutermorphism<T> map, IMultivectorStorage<T> mv)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => map.OmMapVector(mv1),
                BivectorStorage<T> mv1 => map.OmMapBivector(mv1),
                KVectorStorage<T> mv1 => map.OmMapKVector(mv1),
                MultivectorStorage<T> mv1 => map.OmMapMultivector(mv1),
                MultivectorGradedStorage<T> mv1 => map.OmMapMultivector(mv1),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MapToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, Func<ILinVectorStorage<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? scalarProcessor.ScalarZero
                : mappingFunc(mv.GetLinVectorIndexScalarStorage());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MapToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, Func<ILinVectorStorage<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? scalarProcessor.ScalarZero
                : mappingFunc(mv.GetLinVectorIdScalarStorage());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MapToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, Func<ILinVectorGradedStorage<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? scalarProcessor.ScalarZero
                : mappingFunc(mv.GetLinVectorGradedStorage());
        }


        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv, Func<T, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateVectorStorageComposer()
                    .SetTerms(
                        mv.GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Select(
                            indexScalar => 
                                new IndexScalarRecord<T>(
                                    indexScalar.Index, 
                                    mappingFunc(indexScalar.Scalar)
                                )
                        )
                    )
                    .CreateVectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv, Func<ulong, T, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateVectorStorageComposer()
                    .SetTerms(
                        mv.GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Select(
                            indexScalar => 
                                new IndexScalarRecord<T>(
                                    indexScalar.Index, 
                                    mappingFunc(indexScalar.Index, indexScalar.Scalar)
                                )
                        )
                    )
                    .CreateVectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv, Func<IndexScalarRecord<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateVectorStorageComposer()
                    .SetTerms(
                        mv.GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Select(
                            indexScalar => 
                                new IndexScalarRecord<T>(
                                    indexScalar.Index, 
                                    mappingFunc(indexScalar)
                                )
                        )
                    )
                    .CreateVectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, Func<ulong, T, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateVectorStorageComposer()
                    .AddTerms(
                        mv.GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Select(
                            indexScalar => mappingFunc(indexScalar.Index, indexScalar.Scalar)
                        )
                    )
                    .CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, Func<IndexScalarRecord<T>, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateVectorStorageComposer()
                    .AddTerms(mv.GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Select(mappingFunc))
                    .CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, Func<ILinVectorStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : mappingFunc(mv.GetLinVectorIndexScalarStorage()).CreateVectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, Func<uint, ulong, T, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateVectorStorageComposer()
                    .AddTerms(
                        mv.GetLinVectorGradedStorage()
                            .GetGradeIndexScalarRecords()
                            .Select(gradeIndexScalar => 
                                mappingFunc(gradeIndexScalar.Grade, gradeIndexScalar.Index, gradeIndexScalar.Scalar)
                            )
                        )
                    .CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, Func<GradeIndexScalarRecord<T>, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateVectorStorageComposer()
                    .AddTerms(
                        mv.GetLinVectorGradedStorage()
                            .GetGradeIndexScalarRecords()
                            .Select(mappingFunc)
                        )
                    .CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, Func<ILinVectorGradedStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : mappingFunc(mv.GetLinVectorGradedStorage()).CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, Func<ulong, T, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateVectorStorageComposer()
                    .AddTerms(
                        mv.GetLinVectorIdScalarStorage()
                            .GetIndexScalarRecords()
                            .Select(idScalar => 
                                mappingFunc(idScalar.Index, idScalar.Scalar)
                            )
                    )
                    .CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, Func<IndexScalarRecord<T>, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateVectorStorageComposer()
                    .AddTerms(
                        mv.GetLinVectorIdScalarStorage()
                            .GetIndexScalarRecords()
                            .Select(mappingFunc)
                    )
                    .CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, Func<ILinVectorStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? VectorStorage<T>.ZeroVector
                : mappingFunc(mv.GetLinVectorIdScalarStorage()).CreateVectorStorage();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> MapToBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, Func<ILinVectorStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? BivectorStorage<T>.ZeroBivector
                : mappingFunc(mv.GetLinVectorIndexScalarStorage()).CreateBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> MapToBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, Func<ILinVectorGradedStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? BivectorStorage<T>.ZeroBivector
                : mappingFunc(mv.GetLinVectorGradedStorage()).CreateBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> MapToBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, Func<ILinVectorStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? BivectorStorage<T>.ZeroBivector
                : mappingFunc(mv.GetLinVectorIdScalarStorage()).CreateBivectorStorage();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> MapToKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, uint grade, Func<ILinVectorStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? KVectorStorage<T>.CreateKVectorZero(grade)
                : mappingFunc(mv.GetLinVectorIndexScalarStorage()).CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> MapToKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, uint grade, Func<ILinVectorGradedStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? KVectorStorage<T>.CreateKVectorZero(grade)
                : mappingFunc(mv.GetLinVectorGradedStorage()).CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> MapToKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, uint grade, Func<ILinVectorStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? KVectorStorage<T>.CreateKVectorZero(grade)
                : mappingFunc(mv.GetLinVectorIdScalarStorage()).CreateKVectorStorage(grade);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> MapToGradedMultivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, Func<ILinVectorStorage<T>, ILinVectorGradedStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? MultivectorGradedStorage<T>.ZeroMultivector
                : mappingFunc(mv.GetLinVectorIndexScalarStorage()).CreateMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> MapToGradedMultivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, Func<ILinVectorGradedStorage<T>, ILinVectorGradedStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? MultivectorGradedStorage<T>.ZeroMultivector
                : mappingFunc(mv.GetLinVectorGradedStorage()).CreateMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> MapToGradedMultivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, Func<ILinVectorStorage<T>, ILinVectorGradedStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? MultivectorGradedStorage<T>.ZeroMultivector
                : mappingFunc(mv.GetLinVectorIdScalarStorage()).CreateMultivectorGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> MapToSparseMultivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, Func<ILinVectorStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? MultivectorStorage<T>.ZeroMultivector
                : mappingFunc(mv.GetLinVectorIndexScalarStorage()).CreateMultivectorSparseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> MapToSparseMultivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, Func<ILinVectorGradedStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? MultivectorStorage<T>.ZeroMultivector
                : mappingFunc(mv.GetLinVectorGradedStorage()).CreateMultivectorSparseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> MapToSparseMultivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, Func<ILinVectorStorage<T>, ILinVectorStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? MultivectorStorage<T>.ZeroMultivector
                : mappingFunc(mv.GetLinVectorIdScalarStorage()).CreateMultivectorSparseStorage();
        }
        
        public static IMultivectorGradedStorage<T2> MapScalars<T, T2>(this IMultivectorGradedStorage<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => 
                    mv1.MapVectorScalars(scalarMapping),

                BivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalars(scalarMapping),

                KVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalars(scalarMapping),

                MultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IMultivectorStorage<T2> MapScalars<T, T2>(this IMultivectorStorage<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => 
                    mv1.MapVectorScalars(scalarMapping),

                BivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalars(scalarMapping),

                KVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalars(scalarMapping),

                MultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalars(scalarMapping),

                MultivectorStorage<T> mv1 => 
                    mv1.MapMultivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        
        public static IMultivectorGradedStorage<T2> MapScalarsById<T, T2>(this IMultivectorGradedStorage<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsById(idScalarMapping),

                BivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsById(idScalarMapping),

                KVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsById(idScalarMapping),

                MultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IMultivectorStorage<T2> MapScalarsById<T, T2>(this IMultivectorStorage<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsById(idScalarMapping),

                BivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsById(idScalarMapping),

                KVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsById(idScalarMapping),

                MultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsById(idScalarMapping),

                MultivectorStorage<T> mv1 => 
                    mv1.MapMultivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        
        public static IMultivectorGradedStorage<T2> MapScalarsByIndex<T, T2>(this IMultivectorGradedStorage<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByIndex(indexScalarMapping),

                BivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByIndex(indexScalarMapping),

                KVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsByIndex(indexScalarMapping),

                MultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IMultivectorStorage<T2> MapScalarsByIndex<T, T2>(this IMultivectorStorage<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByIndex(indexScalarMapping),

                BivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByIndex(indexScalarMapping),

                KVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsByIndex(indexScalarMapping),

                MultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByIndex(indexScalarMapping),

                MultivectorStorage<T> mv1 => 
                    mv1.MapMultivectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }


        public static IMultivectorGradedStorage<T2> MapScalarsByGradeIndex<T, T2>(this IMultivectorGradedStorage<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                BivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                KVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                MultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IMultivectorStorage<T2> MapScalarsByGradeIndex<T, T2>(this IMultivectorStorage<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                BivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                KVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                MultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                MultivectorStorage<T> mv1 => 
                    mv1.MapMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
    }
}