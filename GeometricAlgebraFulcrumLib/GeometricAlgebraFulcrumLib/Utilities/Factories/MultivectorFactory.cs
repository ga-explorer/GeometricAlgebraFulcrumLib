using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class MultivectorFactory
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivector<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> storage)
        {
            return new Multivector<T>(processor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivector<T>(this IMultivectorStorage<T> storage, IGeometricAlgebraProcessor<T> processor)
        {
            return new Multivector<T>(processor, storage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this Dictionary<ulong, T> idScalarDictionary, IGeometricAlgebraProcessor<T> processor)
        {
            return processor
                .CreateMultivectorStorageSparse(idScalarDictionary)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorGraded<T>(this IGeometricAlgebraProcessor<T> processor, Dictionary<uint, Dictionary<ulong, T>> gradeIndexScalarDictionary)
        {
            return new Multivector<T>(
                processor, 
                processor.CreateMultivectorStorageGraded(gradeIndexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorGraded<T>(this Dictionary<uint, Dictionary<ulong, T>> gradeIndexScalarDictionary, IGeometricAlgebraProcessor<T> processor)
        {
            return new Multivector<T>(
                processor, 
                processor.CreateMultivectorStorageGraded(gradeIndexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<IndexScalarRecord<T>> idScalarRecords, bool sumFlag)
        {
            return sumFlag
                ? processor.SumToMultivectorStorageSparse(idScalarRecords).CreateMultivector(processor)
                : processor.CreateMultivectorStorageSparse(idScalarRecords).CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IEnumerable<IndexScalarRecord<T>> idScalarRecords, IGeometricAlgebraProcessor<T> processor, bool sumFlag = false)
        {
            return sumFlag
                ? processor.SumToMultivectorStorageSparse(idScalarRecords).CreateMultivector(processor)
                : processor.CreateMultivectorStorageSparse(idScalarRecords).CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorGraded<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<IndexScalarRecord<T>> idScalarRecords, bool sumFlag = false)
        {
            return sumFlag
                ? processor.SumToMultivectorStorageSparse(idScalarRecords).CreateMultivector(processor)
                : processor.CreateMultivectorStorageSparse(idScalarRecords).CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorGraded<T>(this IEnumerable<IndexScalarRecord<T>> idScalarRecords, IGeometricAlgebraProcessor<T> processor, bool sumFlag = false)
        {
            return sumFlag
                ? processor.SumToMultivectorStorageSparse(idScalarRecords).CreateMultivector(processor)
                : processor.CreateMultivectorStorageSparse(idScalarRecords).CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorGraded<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarRecords, bool sumFlag = false)
        {
            return sumFlag
                ? processor.SumToMultivectorStorageGraded(gradeIndexScalarRecords).CreateMultivector(processor)
                : processor.CreateMultivectorStorageGraded(gradeIndexScalarRecords).CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorGraded<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarRecords, IGeometricAlgebraProcessor<T> processor, bool sumFlag = false)
        {
            var storage = sumFlag
                ? processor.SumToMultivectorStorageGraded(gradeIndexScalarRecords)
                : processor.CreateMultivectorStorageGraded(gradeIndexScalarRecords);

            return storage.CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivector<T>(this IGeometricAlgebraProcessor<T> processor, ILinVectorStorage<T> idScalarList)
        {
            return idScalarList
                .CreateMultivectorStorageSparse()
                .CreateMultivector(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CopyToMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IReadOnlyDictionary<ulong, T> idScalarDictionary)
        {
            return idScalarDictionary
                .CopyToMultivectorStorageSparse()
                .CreateMultivector(processor);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateToMultivectorSparseZero<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return processor
                .CreateToMultivectorStorageSparseZero()
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparseTerm<T>(this IGeometricAlgebraProcessor<T> processor, int index, T scalar)
        {
            return processor
                .CreateMultivectorStorageSparseTerm(index, scalar)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparseTerm<T>(this IGeometricAlgebraProcessor<T> processor, IndexScalarRecord<T> idScalarPair)
        {
            return processor
                .CreateMultivectorStorageSparseTerm(idScalarPair)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparseBasis<T>(this IGeometricAlgebraProcessor<T> processor, int index)
        {
            return processor
                .CreateMultivectorStorageSparseBasis(index)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparseBasis<T>(this IGeometricAlgebraProcessor<T> processor, ulong index)
        {
            return processor
                .CreateMultivectorStorageSparseBasis(index)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IReadOnlyList<T> scalarList)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarList)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<BasisTerm<T>> termsList)
        {
            return processor
                .CreateMultivectorStorageSparse(termsList)
                .CreateMultivector(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, ILinVectorStorage<T> storage)
        {
            return processor
                .CreateMultivectorStorageSparse(storage)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, ILinVectorGradedStorage<T> storage)
        {
            return processor
                .CreateMultivectorStorageSparse(storage)
                .CreateMultivector(processor);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IReadOnlyDictionary<ulong, int> idScalarDictionary)
        {
            return processor
                .CreateMultivectorStorageSparse(idScalarDictionary)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, int termsCount, Func<ulong, int> indexToScalarFunc)
        {
            return processor
                .CreateMultivectorStorageSparse(indexToScalarFunc)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, params int[] scalarArray)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarArray)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<int> scalarList)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarList)
                .CreateMultivector(processor);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IReadOnlyDictionary<ulong, uint> idScalarDictionary)
        {
            return processor
                .CreateMultivectorStorageSparse(idScalarDictionary)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, int termsCount, Func<ulong, uint> indexToScalarFunc)
        {
            return processor
                .CreateMultivectorStorageSparse(indexToScalarFunc)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, params uint[] scalarArray)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarArray)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<uint> scalarList)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarList)
                .CreateMultivector(processor);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IReadOnlyDictionary<ulong, long> idScalarDictionary)
        {
            return processor
                .CreateMultivectorStorageSparse(idScalarDictionary)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, int termsCount, Func<ulong, long> indexToScalarFunc)
        {
            return processor
                .CreateMultivectorStorageSparse(termsCount, indexToScalarFunc)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, params long[] scalarArray)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarArray)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<long> scalarList)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarList)
                .CreateMultivector(processor);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IReadOnlyDictionary<ulong, ulong> idScalarDictionary)
        {
            return processor
                .CreateMultivectorStorageSparse(idScalarDictionary)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, int termsCount, Func<ulong, ulong> indexToScalarFunc)
        {
            return processor
                .CreateMultivectorStorageSparse(termsCount, indexToScalarFunc)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, params ulong[] scalarArray)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarArray)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<ulong> scalarList)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarList)
                .CreateMultivector(processor);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IReadOnlyDictionary<ulong, float> idScalarDictionary)
        {
            return processor
                .CreateMultivectorStorageSparse(idScalarDictionary)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, int termsCount, Func<ulong, float> indexToScalarFunc)
        {
            return processor
                .CreateMultivectorStorageSparse(indexToScalarFunc)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, params float[] scalarArray)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarArray)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<float> scalarList)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarList)
                .CreateMultivector(processor);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IReadOnlyDictionary<ulong, double> idScalarDictionary)
        {
            return processor
                .CreateMultivectorStorageSparse(idScalarDictionary)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, int termsCount, Func<ulong, double> indexToScalarFunc)
        {
            return processor
                .CreateMultivectorStorageSparse(termsCount, indexToScalarFunc)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, params double[] scalarArray)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarArray)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<double> scalarList)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarList)
                .CreateMultivector(processor);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IReadOnlyDictionary<ulong, string> idScalarDictionary)
        {
            return processor
                .CreateMultivectorStorageSparse(idScalarDictionary)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, int termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return processor
                .CreateMultivectorStorageSparse(termsCount, indexToScalarFunc)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, uint termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return processor
                .CreateMultivectorStorageSparse(termsCount, indexToScalarFunc)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, params string[] scalarArray)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarArray)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<string> scalarList)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarList)
                .CreateMultivector(processor);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IReadOnlyDictionary<ulong, object> idScalarDictionary)
        {
            return processor
                .CreateMultivectorStorageSparse(idScalarDictionary)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, int termsCount, Func<ulong, object> indexToScalarFunc)
        {
            return processor
                .CreateMultivectorStorageSparse(indexToScalarFunc)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, params object[] scalarArray)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarArray)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<object> scalarList)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarList)
                .CreateMultivector(processor);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, Dictionary<ulong, T> idScalarDictionary)
        {
            return processor
                .CreateMultivectorStorageSparse(idScalarDictionary)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, int termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return processor
                .CreateMultivectorStorageSparse(indexToScalarFunc)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, uint termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return processor
                .CreateMultivectorStorageSparse(termsCount, indexToScalarFunc)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, params T[] scalarArray)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarArray)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<T> scalarList)
        {
            return processor
                .CreateMultivectorStorageSparse(scalarList)
                .CreateMultivector(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparseZero<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return processor
                .CreateMultivectorStorageSparseZero()
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparseRepeatedScalar<T>(this IGeometricAlgebraProcessor<T> processor, int count, T value)
        {
            return processor
                .CreateMultivectorStorageSparseRepeatedScalar(count, value)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparseTerm<T>(this IGeometricAlgebraProcessor<T> processor, T value)
        {
            return processor
                .CreateMultivectorStorageSparseTerm(value)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparseTerm<T>(this IGeometricAlgebraProcessor<T> processor, ulong index, T value)
        {
            return processor
                .CreateMultivectorStorageSparseTerm(value)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<IndexScalarRecord<T>> idScalarRecords)
        {
            return processor
                .CreateMultivectorStorageSparse(idScalarRecords)
                .CreateMultivector(processor);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse2D<T>(this IGeometricAlgebraProcessor<T> processor, float scalar0, float scalar1, float scalar2, float scalar12)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromNumber(scalar0),
                [1] = processor.GetScalarFromNumber(scalar1),
                [2] = processor.GetScalarFromNumber(scalar2),
                [3] = processor.GetScalarFromNumber(scalar12)
            };

            return processor.CreateMultivectorSparse(
                processor.CreateLinVectorStorage(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse2D<T>(this IGeometricAlgebraProcessor<T> processor, double scalar0, double scalar1, double scalar2, double scalar12)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromNumber(scalar0),
                [1] = processor.GetScalarFromNumber(scalar1),
                [2] = processor.GetScalarFromNumber(scalar2),
                [3] = processor.GetScalarFromNumber(scalar12)
            };

            return processor.CreateMultivectorSparse(
                processor.CreateLinVectorStorage(idScalarDictionary)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse2D<T>(this IGeometricAlgebraProcessor<T> processor, object scalar0, object scalar1, object scalar2, object scalar12)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromObject(scalar0),
                [1] = processor.GetScalarFromObject(scalar1),
                [2] = processor.GetScalarFromObject(scalar2),
                [3] = processor.GetScalarFromObject(scalar12)
            };

            return processor.CreateMultivectorSparse(
                processor.CreateLinVectorStorage(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse2D<T>(this IGeometricAlgebraProcessor<T> processor, string scalar0, string scalar1, string scalar2, string scalar12)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromText(scalar0),
                [1] = processor.GetScalarFromText(scalar1),
                [2] = processor.GetScalarFromText(scalar2),
                [3] = processor.GetScalarFromText(scalar12)
            };

            return processor.CreateMultivectorSparse(
                processor.CreateLinVectorStorage(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse2D<T>(this IGeometricAlgebraProcessor<T> processor, T scalar0, T scalar1, T scalar2, T scalar12)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = scalar0,
                [1] = scalar1,
                [2] = scalar2,
                [3] = scalar12
            };

            return processor.CreateMultivectorSparse(
                processor.CreateLinVectorStorage(idScalarDictionary)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivectorSparse<T>(this IGeometricAlgebraProcessor<T> processor, IEnumerable<GradeIndexScalarRecord<T>> termsList)
        {
            return processor
                .CreateMultivectorStorageSparse(termsList)
                .CreateMultivector(processor);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivector2D<T>(this IGeometricAlgebraProcessor<T> processor, T scalar0, T scalar1, T scalar2, T scalar12)
        {
            return processor
                .CreateMultivectorStorageSparse2D(scalar0, scalar1, scalar2, scalar12)
                .CreateMultivector(processor);
        }
    }
}
