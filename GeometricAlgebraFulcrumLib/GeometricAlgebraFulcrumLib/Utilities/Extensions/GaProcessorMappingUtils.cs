using System;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaProcessorMappingUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MapToScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, Func<ILaVectorEvenStorage<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? scalarProcessor.ScalarZero
                : mappingFunc(mv.IndexScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MapToScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, Func<ILaVectorEvenStorage<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? scalarProcessor.ScalarZero
                : mappingFunc(mv.IdScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MapToScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, Func<ILaVectorGradedStorage<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? scalarProcessor.ScalarZero
                : mappingFunc(mv.GradeIndexScalarList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> MapToScalarStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, Func<ILaVectorEvenStorage<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaScalarStorage<T>.ZeroScalar
                : mappingFunc(mv.IndexScalarList).CreateStorageScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> MapToScalarStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, Func<ILaVectorGradedStorage<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaScalarStorage<T>.ZeroScalar
                : mappingFunc(mv.GradeIndexScalarList).CreateStorageScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> MapToScalarStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, Func<ILaVectorEvenStorage<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaScalarStorage<T>.ZeroScalar
                : mappingFunc(mv.IdScalarList).CreateStorageScalar();
        }

        
        //TODO: Duplicate these functions for ToScalar, ToBivector, ToKVector, ToSparsMultivector, ToGradedMultivector
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv, Func<T, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateKVectorStorageComposer()
                    .SetTerms(
                        mv.IndexScalarList.GetIndexScalarRecords().Select(
                            indexScalar => 
                                new IndexScalarRecord<T>(
                                    indexScalar.Index, 
                                    mappingFunc(indexScalar.Scalar)
                                )
                        )
                    )
                    .CreateGaVectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv, Func<ulong, T, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateKVectorStorageComposer()
                    .SetTerms(
                        mv.IndexScalarList.GetIndexScalarRecords().Select(
                            indexScalar => 
                                new IndexScalarRecord<T>(
                                    indexScalar.Index, 
                                    mappingFunc(indexScalar.Index, indexScalar.Scalar)
                                )
                        )
                    )
                    .CreateGaVectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv, Func<IndexScalarRecord<T>, T> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateKVectorStorageComposer()
                    .SetTerms(
                        mv.IndexScalarList.GetIndexScalarRecords().Select(
                            indexScalar => 
                                new IndexScalarRecord<T>(
                                    indexScalar.Index, 
                                    mappingFunc(indexScalar)
                                )
                        )
                    )
                    .CreateGaVectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, Func<ulong, T, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateKVectorStorageComposer()
                    .AddTerms(
                        mv.IndexScalarList.GetIndexScalarRecords().Select(
                            indexScalar => mappingFunc(indexScalar.Index, indexScalar.Scalar)
                        )
                    )
                    .CreateGaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, Func<IndexScalarRecord<T>, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateKVectorStorageComposer()
                    .AddTerms(mv.IndexScalarList.GetIndexScalarRecords().Select(mappingFunc))
                    .CreateGaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, Func<ILaVectorEvenStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : mappingFunc(mv.IndexScalarList).CreateGaVectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, Func<uint, ulong, T, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateKVectorStorageComposer()
                    .AddTerms(
                        mv
                            .GradeIndexScalarList
                            .GetGradeIndexScalarRecords()
                            .Select(gradeIndexScalar => 
                                mappingFunc(gradeIndexScalar.Grade, gradeIndexScalar.Index, gradeIndexScalar.Scalar)
                            )
                        )
                    .CreateGaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, Func<GradeIndexScalarRecord<T>, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateKVectorStorageComposer()
                    .AddTerms(
                        mv
                            .GradeIndexScalarList
                            .GetGradeIndexScalarRecords()
                            .Select(mappingFunc)
                        )
                    .CreateGaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, Func<ILaVectorGradedStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : mappingFunc(mv.GradeIndexScalarList).CreateGaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, Func<ulong, T, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateKVectorStorageComposer()
                    .AddTerms(
                        mv
                            .IdScalarList
                            .GetIndexScalarRecords()
                            .Select(idScalar => 
                                mappingFunc(idScalar.Index, idScalar.Scalar)
                            )
                    )
                    .CreateGaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, Func<IndexScalarRecord<T>, IndexScalarRecord<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : scalarProcessor
                    .CreateKVectorStorageComposer()
                    .AddTerms(
                        mv
                            .IdScalarList
                            .GetIndexScalarRecords()
                            .Select(mappingFunc)
                    )
                    .CreateGaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> MapToVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, Func<ILaVectorEvenStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaVectorStorage<T>.ZeroVector
                : mappingFunc(mv.IdScalarList).CreateGaVectorStorage();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> MapToBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, Func<ILaVectorEvenStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaBivectorStorage<T>.ZeroBivector
                : mappingFunc(mv.IndexScalarList).CreateBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> MapToBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, Func<ILaVectorGradedStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaBivectorStorage<T>.ZeroBivector
                : mappingFunc(mv.GradeIndexScalarList).CreateBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> MapToBivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, Func<ILaVectorEvenStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaBivectorStorage<T>.ZeroBivector
                : mappingFunc(mv.IdScalarList).CreateBivectorStorage();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> MapToKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, uint grade, Func<ILaVectorEvenStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaKVectorStorage<T>.ZeroKVector(grade)
                : mappingFunc(mv.IndexScalarList).CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> MapToKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, uint grade, Func<ILaVectorGradedStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaKVectorStorage<T>.ZeroKVector(grade)
                : mappingFunc(mv.GradeIndexScalarList).CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> MapToKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, uint grade, Func<ILaVectorEvenStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaKVectorStorage<T>.ZeroKVector(grade)
                : mappingFunc(mv.IdScalarList).CreateKVectorStorage(grade);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> MapToGradedMultivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, Func<ILaVectorEvenStorage<T>, ILaVectorGradedStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaMultivectorGradedStorage<T>.ZeroMultivector
                : mappingFunc(mv.IndexScalarList).CreateStorageGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> MapToGradedMultivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, Func<ILaVectorGradedStorage<T>, ILaVectorGradedStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaMultivectorGradedStorage<T>.ZeroMultivector
                : mappingFunc(mv.GradeIndexScalarList).CreateStorageGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> MapToGradedMultivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, Func<ILaVectorEvenStorage<T>, ILaVectorGradedStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaMultivectorGradedStorage<T>.ZeroMultivector
                : mappingFunc(mv.IdScalarList).CreateStorageGradedMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> MapToSparseMultivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, Func<ILaVectorEvenStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaMultivectorSparseStorage<T>.ZeroMultivector
                : mappingFunc(mv.IndexScalarList).CreateStorageSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> MapToSparseMultivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, Func<ILaVectorGradedStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaMultivectorSparseStorage<T>.ZeroMultivector
                : mappingFunc(mv.GradeIndexScalarList).CreateStorageSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> MapToSparseMultivectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, Func<ILaVectorEvenStorage<T>, ILaVectorEvenStorage<T>> mappingFunc)
        {
            return mv.IsEmpty()
                ? GaMultivectorSparseStorage<T>.ZeroMultivector
                : mappingFunc(mv.IdScalarList).CreateStorageSparseMultivector();
        }






        public static IGaScalarStorage<T2> MapScalars<T, T2>(this IGaScalarStorage<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalar(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaVectorStorage<T2> MapScalars<T, T2>(this IGaVectorStorage<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaBivectorStorage<T2> MapScalars<T, T2>(this IGaBivectorStorage<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaKVectorStorage<T2> MapScalars<T, T2>(this IGaKVectorStorage<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalar(scalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalars(scalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalars(scalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaMultivectorGradedStorage<T2> MapScalars<T, T2>(this IGaMultivectorGradedStorage<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalar(scalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalars(scalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalars(scalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalars(scalarMapping),

                GaMultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaMultivectorSparseStorage<T2> MapScalars<T, T2>(this IGaMultivectorSparseStorage<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaMultivectorSparseStorage<T> mv1 => 
                    mv1.MapSparseMultivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaMultivectorStorage<T2> MapScalars<T, T2>(this IGaMultivectorStorage<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalar(scalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalars(scalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalars(scalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalars(scalarMapping),

                GaMultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalars(scalarMapping),

                GaMultivectorSparseStorage<T> mv1 => 
                    mv1.MapSparseMultivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }


        public static IGaKVectorStorage<T2> MapScalarsById<T, T2>(this IGaKVectorStorage<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalarById(idScalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsById(idScalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsById(idScalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaVectorStorage<T2> MapScalarsById<T, T2>(this IGaVectorStorage<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaBivectorStorage<T2> MapScalarsById<T, T2>(this IGaBivectorStorage<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaMultivectorGradedStorage<T2> MapScalarsById<T, T2>(this IGaMultivectorGradedStorage<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalarById(idScalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsById(idScalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsById(idScalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsById(idScalarMapping),

                GaMultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaMultivectorSparseStorage<T2> MapScalarsById<T, T2>(this IGaMultivectorSparseStorage<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaMultivectorSparseStorage<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaMultivectorStorage<T2> MapScalarsById<T, T2>(this IGaMultivectorStorage<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalarById(idScalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsById(idScalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsById(idScalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsById(idScalarMapping),

                GaMultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsById(idScalarMapping),

                GaMultivectorSparseStorage<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }


        public static IGaKVectorStorage<T2> MapScalarsByIndex<T, T2>(this IGaKVectorStorage<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalarByIndex(indexScalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByIndex(indexScalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByIndex(indexScalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaVectorStorage<T2> MapScalarsByIndex<T, T2>(this IGaVectorStorage<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaBivectorStorage<T2> MapScalarsByIndex<T, T2>(this IGaBivectorStorage<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaMultivectorGradedStorage<T2> MapScalarsByIndex<T, T2>(this IGaMultivectorGradedStorage<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalarByIndex(indexScalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByIndex(indexScalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByIndex(indexScalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsByIndex(indexScalarMapping),

                GaMultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaMultivectorSparseStorage<T2> MapScalarsByIndex<T, T2>(this IGaMultivectorSparseStorage<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaMultivectorSparseStorage<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaMultivectorStorage<T2> MapScalarsByIndex<T, T2>(this IGaMultivectorStorage<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalarByIndex(indexScalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByIndex(indexScalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByIndex(indexScalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsByIndex(indexScalarMapping),

                GaMultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByIndex(indexScalarMapping),

                GaMultivectorSparseStorage<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }


        public static IGaKVectorStorage<T2> MapScalarsByGradeIndex<T, T2>(this IGaKVectorStorage<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalarByGradeIndex(gradeIndexScalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaVectorStorage<T2> MapScalarsByGradeIndex<T, T2>(this IGaVectorStorage<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaBivectorStorage<T2> MapScalarsByGradeIndex<T, T2>(this IGaBivectorStorage<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaMultivectorGradedStorage<T2> MapScalarsByGradeIndex<T, T2>(this IGaMultivectorGradedStorage<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalarByGradeIndex(gradeIndexScalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaMultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaMultivectorSparseStorage<T2> MapScalarsByGradeIndex<T, T2>(this IGaMultivectorSparseStorage<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaMultivectorSparseStorage<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaMultivectorStorage<T2> MapScalarsByGradeIndex<T, T2>(this IGaMultivectorStorage<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.MapScalarByGradeIndex(gradeIndexScalarMapping),

                GaVectorStorage<T> mv1 => 
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaBivectorStorage<T> mv1 => 
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaKVectorStorage<T> mv1 => 
                    mv1.MapKVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaMultivectorGradedStorage<T> mv1 => 
                    mv1.MapGradedMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                GaMultivectorSparseStorage<T> mv1 => 
                    mv1.MapSparseMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
    }
}