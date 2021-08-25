using System;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary
{
    public static class GaProcessorMappingUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MapToScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, Func<IGaListEven<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? scalarProcessor.GetZeroScalar()
                : mappingFunc(mv.IndexScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MapToScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, Func<IGaListEven<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? scalarProcessor.GetZeroScalar()
                : mappingFunc(mv.IdScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MapToScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, Func<IGaListGraded<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? scalarProcessor.GetZeroScalar()
                : mappingFunc(mv.GradeIndexScalarList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> MapToScalarStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, Func<IGaListEven<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageScalar<T>.ZeroScalar
                : mappingFunc(mv.IndexScalarList).CreateStorageScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> MapToScalarStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, Func<IGaListGraded<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageScalar<T>.ZeroScalar
                : mappingFunc(mv.GradeIndexScalarList).CreateStorageScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> MapToScalarStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, Func<IGaListEven<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageScalar<T>.ZeroScalar
                : mappingFunc(mv.IdScalarList).CreateStorageScalar();
        }

        
        //TODO: Duplicate these functions for ToScalar, ToBivector, ToKVector, ToSparsMultivector, ToGradedMultivector
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv, Func<T, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : scalarProcessor
                    .CreateStorageKVectorComposer()
                    .SetTerms(
                        mv.IndexScalarList.GetKeyValueRecords().Select(
                            indexScalar => 
                                new GaRecordKeyValue<T>(
                                    indexScalar.Key, 
                                    mappingFunc(indexScalar.Value)
                                )
                        )
                    )
                    .CreateStorageVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv, Func<ulong, T, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : scalarProcessor
                    .CreateStorageKVectorComposer()
                    .SetTerms(
                        mv.IndexScalarList.GetKeyValueRecords().Select(
                            indexScalar => 
                                new GaRecordKeyValue<T>(
                                    indexScalar.Key, 
                                    mappingFunc(indexScalar.Key, indexScalar.Value)
                                )
                        )
                    )
                    .CreateStorageVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv, Func<GaRecordKeyValue<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : scalarProcessor
                    .CreateStorageKVectorComposer()
                    .SetTerms(
                        mv.IndexScalarList.GetKeyValueRecords().Select(
                            indexScalar => 
                                new GaRecordKeyValue<T>(
                                    indexScalar.Key, 
                                    mappingFunc(indexScalar)
                                )
                        )
                    )
                    .CreateStorageVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, Func<ulong, T, GaRecordKeyValue<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : scalarProcessor
                    .CreateStorageKVectorComposer()
                    .AddTerms(
                        mv.IndexScalarList.GetKeyValueRecords().Select(
                            indexScalar => mappingFunc(indexScalar.Key, indexScalar.Value)
                        )
                    )
                    .CreateStorageVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, Func<GaRecordKeyValue<T>, GaRecordKeyValue<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : scalarProcessor
                    .CreateStorageKVectorComposer()
                    .AddTerms(mv.IndexScalarList.GetKeyValueRecords().Select(mappingFunc))
                    .CreateStorageVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, Func<IGaListEven<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : mappingFunc(mv.IndexScalarList).CreateStorageVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, Func<uint, ulong, T, GaRecordKeyValue<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : scalarProcessor
                    .CreateStorageKVectorComposer()
                    .AddTerms(
                        mv
                            .GradeIndexScalarList
                            .GetGradeKeyValueRecords()
                            .Select(gradeIndexScalar => 
                                mappingFunc(gradeIndexScalar.Grade, gradeIndexScalar.Key, gradeIndexScalar.Value)
                            )
                        )
                    .CreateStorageVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, Func<GaRecordGradeKeyValue<T>, GaRecordKeyValue<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : scalarProcessor
                    .CreateStorageKVectorComposer()
                    .AddTerms(
                        mv
                            .GradeIndexScalarList
                            .GetGradeKeyValueRecords()
                            .Select(mappingFunc)
                        )
                    .CreateStorageVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, Func<IGaListGraded<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : mappingFunc(mv.GradeIndexScalarList).CreateStorageVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, Func<ulong, T, GaRecordKeyValue<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : scalarProcessor
                    .CreateStorageKVectorComposer()
                    .AddTerms(
                        mv
                            .IdScalarList
                            .GetKeyValueRecords()
                            .Select(idScalar => 
                                mappingFunc(idScalar.Key, idScalar.Value)
                            )
                    )
                    .CreateStorageVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, Func<GaRecordKeyValue<T>, GaRecordKeyValue<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : scalarProcessor
                    .CreateStorageKVectorComposer()
                    .AddTerms(
                        mv
                            .IdScalarList
                            .GetKeyValueRecords()
                            .Select(mappingFunc)
                    )
                    .CreateStorageVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> MapToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, Func<IGaListEven<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageVector<T>.ZeroVector
                : mappingFunc(mv.IdScalarList).CreateStorageVector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> MapToBivectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, Func<IGaListEven<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageBivector<T>.ZeroBivector
                : mappingFunc(mv.IndexScalarList).CreateStorageBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> MapToBivectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, Func<IGaListGraded<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageBivector<T>.ZeroBivector
                : mappingFunc(mv.GradeIndexScalarList).CreateStorageBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> MapToBivectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, Func<IGaListEven<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageBivector<T>.ZeroBivector
                : mappingFunc(mv.IdScalarList).CreateStorageBivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> MapToKVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, uint grade, Func<IGaListEven<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageKVector<T>.ZeroKVector(grade)
                : mappingFunc(mv.IndexScalarList).CreateStorageKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> MapToKVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, uint grade, Func<IGaListGraded<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageKVector<T>.ZeroKVector(grade)
                : mappingFunc(mv.GradeIndexScalarList).CreateStorageKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> MapToKVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, uint grade, Func<IGaListEven<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageKVector<T>.ZeroKVector(grade)
                : mappingFunc(mv.IdScalarList).CreateStorageKVector(grade);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> MapToGradedMultivectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, Func<IGaListEven<T>, IGaListGraded<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageMultivectorGraded<T>.ZeroMultivector
                : mappingFunc(mv.IndexScalarList).CreateStorageGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> MapToGradedMultivectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, Func<IGaListGraded<T>, IGaListGraded<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageMultivectorGraded<T>.ZeroMultivector
                : mappingFunc(mv.GradeIndexScalarList).CreateStorageGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> MapToGradedMultivectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, Func<IGaListEven<T>, IGaListGraded<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageMultivectorGraded<T>.ZeroMultivector
                : mappingFunc(mv.IdScalarList).CreateStorageGradedMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> MapToSparseMultivectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, Func<IGaListEven<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageMultivectorSparse<T>.ZeroMultivector
                : mappingFunc(mv.IndexScalarList).CreateStorageSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> MapToSparseMultivectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, Func<IGaListGraded<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageMultivectorSparse<T>.ZeroMultivector
                : mappingFunc(mv.GradeIndexScalarList).CreateStorageSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> MapToSparseMultivectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, Func<IGaListEven<T>, IGaListEven<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaStorageMultivectorSparse<T>.ZeroMultivector
                : mappingFunc(mv.IdScalarList).CreateStorageSparseMultivector();
        }






        public static IGaStorageScalar<T2> MapScalars<T, T2>(this IGaStorageScalar<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalar(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageVector<T2> MapScalars<T, T2>(this IGaStorageVector<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageBivector<T2> MapScalars<T, T2>(this IGaStorageBivector<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaStorageKVector<T2> MapScalars<T, T2>(this IGaStorageKVector<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalar(scalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalars(scalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalars(scalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaStorageMultivectorGraded<T2> MapScalars<T, T2>(this IGaStorageMultivectorGraded<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalar(scalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalars(scalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalars(scalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalars(scalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapGradedMultivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorSparse<T2> MapScalars<T, T2>(this IGaStorageMultivectorSparse<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapSparseMultivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivector<T2> MapScalars<T, T2>(this IGaStorageMultivector<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalar(scalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalars(scalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalars(scalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalars(scalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapGradedMultivectorScalars(scalarMapping),

                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapSparseMultivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }


        public static IGaStorageKVector<T2> MapScalarsById<T, T2>(this IGaStorageKVector<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarById(idScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsById(idScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsById(idScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageVector<T2> MapScalarsById<T, T2>(this IGaStorageVector<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageBivector<T2> MapScalarsById<T, T2>(this IGaStorageBivector<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaStorageMultivectorGraded<T2> MapScalarsById<T, T2>(this IGaStorageMultivectorGraded<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarById(idScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsById(idScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsById(idScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalarsById(idScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorSparse<T2> MapScalarsById<T, T2>(this IGaStorageMultivectorSparse<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivector<T2> MapScalarsById<T, T2>(this IGaStorageMultivector<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarById(idScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsById(idScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsById(idScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalarsById(idScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsById(idScalarMapping),

                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }


        public static IGaStorageKVector<T2> MapScalarsByIndex<T, T2>(this IGaStorageKVector<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarByIndex(indexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsByIndex(indexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsByIndex(indexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageVector<T2> MapScalarsByIndex<T, T2>(this IGaStorageVector<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageBivector<T2> MapScalarsByIndex<T, T2>(this IGaStorageBivector<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaStorageMultivectorGraded<T2> MapScalarsByIndex<T, T2>(this IGaStorageMultivectorGraded<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarByIndex(indexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsByIndex(indexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsByIndex(indexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalarsByIndex(indexScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorSparse<T2> MapScalarsByIndex<T, T2>(this IGaStorageMultivectorSparse<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivector<T2> MapScalarsByIndex<T, T2>(this IGaStorageMultivector<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarByIndex(indexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsByIndex(indexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsByIndex(indexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalarsByIndex(indexScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByIndex(indexScalarMapping),

                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }


        public static IGaStorageKVector<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageKVector<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarByGradeIndex(gradeIndexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageVector<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageVector<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageBivector<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageBivector<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaStorageMultivectorGraded<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageMultivectorGraded<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarByGradeIndex(gradeIndexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorSparse<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageMultivectorSparse<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivector<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageMultivector<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarByGradeIndex(gradeIndexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapKVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
    }
}