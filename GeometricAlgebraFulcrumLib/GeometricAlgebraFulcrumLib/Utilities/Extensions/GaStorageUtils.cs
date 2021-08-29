using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaStorageUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGaKVectorStorage<T>> GetKVectorStorages<T>(this IGaMultivectorStorage<T> multivector)
        {
            return multivector
                .GetGradeIndexScalarList()
                .GetGradeStorageRecords()
                .Select(
                    gradeListRecord => gradeListRecord.CreateKVectorStorage()
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeVectorStorageScalarRecord<T> GetScaledListRecord<T>(this IGaKVectorStorage<T> kVector, T scalingFactor)
        {
            return new GradeVectorStorageScalarRecord<T>(
                kVector.Grade,
                kVector.IndexScalarList,
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorEvenStorageScalarRecord<T> GetScaledListRecord<T>(this IGaMultivectorSparseStorage<T> multivector, T scalingFactor)
        {
            return new VectorEvenStorageScalarRecord<T>(
                multivector.IdScalarList,
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorGradedStorageScalarRecord<T> GetScaledListRecord<T>(this IGaMultivectorGradedStorage<T> multivector, T scalingFactor)
        {
            return new VectorGradedStorageScalarRecord<T>(
                multivector.GetGradeIndexScalarList(),
                scalingFactor
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMinId<T>(this GaMultivectorSparseStorage<T> mv)
        {
            return mv.IdScalarList.GetMinIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxId<T>(this GaMultivectorSparseStorage<T> mv)
        {
            return mv.IdScalarList.GetMaxIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMinIndex<T>(this IGaKVectorStorage<T> mv)
        {
            return mv.IndexScalarList.GetMinIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxIndex<T>(this IGaKVectorStorage<T> mv)
        {
            return mv.IndexScalarList.GetMaxIndex();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords<T>(this IGaMultivectorStorage<T> mv, Func<T, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords<T>(this IGaMultivectorStorage<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords<T>(this IGaMultivectorStorage<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Index, record.Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords<T>(this IGaKVectorStorage<T> mv, Func<T, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords<T>(this IGaKVectorStorage<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv
                .IndexScalarList
                .GetIndexScalarRecords()
                .Where(record => filterFunc(record.Index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords<T>(this IGaKVectorStorage<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv
                .IndexScalarList
                .GetIndexScalarRecords()
                .Where(record => filterFunc(record.Index, record.Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotZeroIdScalarRecords<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.GetIdScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotZeroIdScalarRecords<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetIdScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar))
                : mv.GetIdScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotNearZeroIdScalarRecords<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.GetIdScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNearZeroIdScalarRecords<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.GetIdScalarRecords(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotZeroIndexScalarRecords<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv)
        {
            return mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotZeroIndexScalarRecords<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar))
                : mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotNearZeroIndexScalarRecords<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv)
        {
            return mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNearZeroIndexScalarRecords<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv)
        {
            return mv.GetIndexScalarRecords(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetTerms<T>(this IGaMultivectorStorage<T> mv, Func<T, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetTerms<T>(this IGaMultivectorStorage<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetTerms<T>(this IGaMultivectorStorage<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetNotZeroTerms<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.GetTerms(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetNotZeroTerms<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetTerms(scalar => !scalarProcessor.IsNearZero(scalar)) 
                : mv.GetTerms(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetNotNearZeroTerms<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.GetTerms(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetNearZeroTerms<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.GetTerms(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.TryGetTermScalar(0, out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, ulong id)
        {
            return mv.TryGetTermScalar(id, out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, uint grade, ulong index)
        {
            return mv.TryGetTermScalar(grade, index, out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalarByIndex<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, int index)
        {
            return mv.TryGetTermScalarByIndex((ulong) index, out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalarByIndex<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, ulong index)
        {
            return mv.TryGetTermScalarByIndex(index, out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> GetTerm<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, ulong id)
        {
            return id.CreateBasisTerm(
                scalarProcessor.GetTermScalar(mv, id)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> GetTerm<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, uint grade, ulong index)
        {
            return grade.CreateBasisTerm(
                index,
                scalarProcessor.GetTermScalar(mv, grade, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> GetTermByIndex<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, int index)
        {
            return mv.Grade.CreateBasisTerm(
                index,
                scalarProcessor.GetTermScalarByIndex(mv, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> GetTermByIndex<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, ulong index)
        {
            return mv.Grade.CreateBasisTerm(
                index,
                scalarProcessor.GetTermScalarByIndex(mv, index)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds(this IEnumerable<GaBasisBlade> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> GetGrades(this IEnumerable<GaBasisBlade> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Grade).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this IEnumerable<GaBasisBlade> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Index);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> GetGrades<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Grade).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisBlade> GetBasisBlades<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalars<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new IndexScalarRecord<T>(term.BasisBlade.Id, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIdScalarTuples<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new IndexScalarRecord<T>(term.BasisBlade.Id, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new IndexScalarRecord<T>(term.BasisBlade.Index, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarTuples<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new IndexScalarRecord<T>(term.BasisBlade.Index, term.Scalar)
            );
        }

        
        public static IEnumerable<IndexScalarRecord<T>> OrderByGradeIndex<T>(this IEnumerable<IndexScalarRecord<T>> idScalarRecords)
        {
            var termsArray = 
                idScalarRecords.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => t.Index)
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Index.BasisBladeIdToGrade())
                .ThenByDescending(t => t.Index.ReverseBits(bitsCount));
        }
        
        public static IEnumerable<IndexScalarRecord<T>> OrderById<T>(this IEnumerable<IndexScalarRecord<T>> idScalarRecords)
        {
            return idScalarRecords
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Index);
        }
        
        public static IEnumerable<GradeIndexScalarRecord<T>> OrderByGradeIndex<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            var termsArray = gradeIndexScalarTuples.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => 
                    t.Index.BasisBladeIndexToId(t.Grade)
                )
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Grade)
                .ThenByDescending(t => 
                    t.Index.BasisBladeIndexToId(t.Grade).ReverseBits(bitsCount)
                );
        }
        
        public static IEnumerable<GradeIndexScalarRecord<T>> OrderById<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            return gradeIndexScalarTuples
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => 
                    t.Index.BasisBladeIndexToId(t.Grade)
                );
        }

        public static IEnumerable<GaBasisTerm<T>> OrderByGradeIndex<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            var termsArray = termsList.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => t.BasisBlade.Id)
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.BasisBlade.Grade)
                .ThenByDescending(t => t.BasisBlade.Id.ReverseBits(bitsCount));
        }
        
        public static IEnumerable<GaBasisTerm<T>> OrderById<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.BasisBlade.Id);
        }


        public static ILaVectorDenseEvenStorage<T> VectorToArrayVector<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> vectorStorage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension];

            for (var index = 0; index < vSpaceDimension; index++)
                array[index] = scalarProcessor.ScalarZero;

            foreach (var (index, scalar) in vectorStorage.IndexScalarList.GetIndexScalarRecords()) 
                array[index] = scalar;

            return array.CreateLaVectorDenseStorage();
        }

        public static ILaVectorDenseEvenStorage<T> BivectorToArrayVector<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> bivectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) vSpaceDimension.KVectorSpaceDimension(2
            );

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ScalarZero;

            foreach (var (index, scalar) in bivectorStorage.IndexScalarList.GetIndexScalarRecords()) 
                array[index] = scalar;

            return array.CreateLaVectorDenseStorage();
        }

        public static ILaMatrixDenseEvenStorage<T> BivectorToArray<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> bivectorStorage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension, vSpaceDimension];

            for (var i = 0; i < vSpaceDimension; i++)
            for (var j = 0; j < vSpaceDimension; j++)
                array[i, j] = scalarProcessor.ScalarZero;

            foreach (var (index1, index2, scalar) in bivectorStorage.GetBasisVectorsIndexScalarRecords())
            {
                array[index1, index2] = scalar;
                array[index2, index1] = scalarProcessor.Negative(scalar);
            }

            return array.CreateEvenGridDense();
        }

        public static ILaMatrixDenseEvenStorage<T> ScalarPlusBivectorToArray<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> storage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension, vSpaceDimension];

            var scalar = scalarProcessor.GetTermScalar(storage, 0);

            for (var i = 0; i < vSpaceDimension; i++)
            {
                array[i, i] = scalar;

                for (var j = i + 1; j < vSpaceDimension; j++)
                {
                    array[i, j] = scalarProcessor.ScalarZero;
                    array[j, i] = scalarProcessor.ScalarZero;
                }
            }

            var bivectorTerms = storage
                .GetTerms()
                .Where(term => term.BasisBlade.Grade == 2);

            foreach (var term in bivectorTerms)
            {
                var bivectorScalar = term.Scalar;
                var basisVectorIndices = 
                    term.BasisBlade.GetBasisVectorIndices().ToArray();

                var index1 = basisVectorIndices[0];
                var index2 = basisVectorIndices[1];

                array[index1, index2] = bivectorScalar;
                array[index2, index1] = scalarProcessor.Negative(bivectorScalar);
            }

            return array.CreateEvenGridDense();
        }

        public static ILaVectorDenseEvenStorage<T> KVectorToArrayVector<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> kVectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) vSpaceDimension.KVectorSpaceDimension(kVectorStorage.Grade
            );

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ScalarZero;

            foreach (var (index, scalar) in kVectorStorage.IndexScalarList.GetIndexScalarRecords()) 
                array[index] = scalar;

            return array.CreateLaVectorDenseStorage();
        }

        public static ILaVectorDenseEvenStorage<T> MultivectorToArrayVector<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> multivectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) vSpaceDimension.ToGaSpaceDimension();

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ScalarZero;

            foreach (var (index, scalar) in multivectorStorage.GetIdScalarRecords()) 
                array[index] = scalar;

            return array.CreateLaVectorDenseStorage();
        }
    }
}