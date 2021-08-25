using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Utils
{
    public static class GaStorageUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGaStorageKVector<T>> GetKVectorStorages<T>(this IGaStorageMultivector<T> multivector)
        {
            return multivector
                .GetGradeIndexScalarList()
                .GetGradeListRecords()
                .Select(
                    gradeListRecord => gradeListRecord.CreateStorageKVector()
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeEvenListValue<T> GetScaledListRecord<T>(this IGaStorageKVector<T> kVector, T scalingFactor)
        {
            return new GaRecordGradeEvenListValue<T>(
                kVector.Grade,
                kVector.IndexScalarList,
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordEvenListValue<T> GetScaledListRecord<T>(this IGaStorageMultivectorSparse<T> multivector, T scalingFactor)
        {
            return new GaRecordEvenListValue<T>(
                multivector.IdScalarList,
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradedListValue<T> GetScaledListRecord<T>(this IGaStorageMultivectorGraded<T> multivector, T scalingFactor)
        {
            return new GaRecordGradedListValue<T>(
                multivector.GetGradeIndexScalarList(),
                scalingFactor
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMinId<T>(this GaStorageMultivectorSparse<T> mv)
        {
            return mv.IdScalarList.GetMinKey();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxId<T>(this GaStorageMultivectorSparse<T> mv)
        {
            return mv.IdScalarList.GetMaxKey();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMinIndex<T>(this IGaStorageKVector<T> mv)
        {
            return mv.IndexScalarList.GetMinKey();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxIndex<T>(this IGaStorageKVector<T> mv)
        {
            return mv.IndexScalarList.GetMaxKey();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetIdScalarRecords<T>(this IGaStorageMultivector<T> mv, Func<T, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetIdScalarRecords<T>(this IGaStorageMultivector<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetIdScalarRecords<T>(this IGaStorageMultivector<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Key, record.Value));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetIndexScalarRecords<T>(this IGaStorageKVector<T> mv, Func<T, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetIndexScalarRecords<T>(this IGaStorageKVector<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv
                .IndexScalarList
                .GetKeyValueRecords()
                .Where(record => filterFunc(record.Key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetIndexScalarRecords<T>(this IGaStorageKVector<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv
                .IndexScalarList
                .GetKeyValueRecords()
                .Where(record => filterFunc(record.Key, record.Value));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetNotZeroIdScalarRecords<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetIdScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetNotZeroIdScalarRecords<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetIdScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar))
                : mv.GetIdScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetNotNearZeroIdScalarRecords<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetIdScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetNearZeroIdScalarRecords<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetIdScalarRecords(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetNotZeroIndexScalarRecords<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv)
        {
            return mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetNotZeroIndexScalarRecords<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar))
                : mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetNotNearZeroIndexScalarRecords<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv)
        {
            return mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetNearZeroIndexScalarRecords<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv)
        {
            return mv.GetIndexScalarRecords(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetTerms<T>(this IGaStorageMultivector<T> mv, Func<T, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetTerms<T>(this IGaStorageMultivector<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetTerms<T>(this IGaStorageMultivector<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetNotZeroTerms<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetTerms(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetNotZeroTerms<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetTerms(scalar => !scalarProcessor.IsNearZero(scalar)) 
                : mv.GetTerms(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetNotNearZeroTerms<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetTerms(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetNearZeroTerms<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetTerms(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.TryGetTermScalar(0, out var scalar)
                ? scalar
                : scalarProcessor.GetZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, ulong id)
        {
            return mv.TryGetTermScalar(id, out var scalar)
                ? scalar
                : scalarProcessor.GetZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, uint grade, ulong index)
        {
            return mv.TryGetTermScalar(grade, index, out var scalar)
                ? scalar
                : scalarProcessor.GetZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalarByIndex<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, int index)
        {
            return mv.TryGetTermScalarByIndex((ulong) index, out var scalar)
                ? scalar
                : scalarProcessor.GetZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalarByIndex<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, ulong index)
        {
            return mv.TryGetTermScalarByIndex(index, out var scalar)
                ? scalar
                : scalarProcessor.GetZeroScalar();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> GetTerm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, ulong id)
        {
            return id.CreateBasisTerm(
                scalarProcessor.GetTermScalar(mv, id)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> GetTerm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, uint grade, ulong index)
        {
            return grade.CreateBasisTerm(
                index,
                scalarProcessor.GetTermScalar(mv, grade, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> GetTermByIndex<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, int index)
        {
            return mv.Grade.CreateBasisTerm(
                index,
                scalarProcessor.GetTermScalarByIndex(mv, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> GetTermByIndex<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, ulong index)
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
        public static IEnumerable<GaRecordKeyValue<T>> GetIdScalarRecords<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new GaRecordKeyValue<T>(term.BasisBlade.Id, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetIdScalarTuples<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new GaRecordKeyValue<T>(term.BasisBlade.Id, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetIndexScalarRecords<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new GaRecordKeyValue<T>(term.BasisBlade.Index, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetIndexScalarTuples<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new GaRecordKeyValue<T>(term.BasisBlade.Index, term.Scalar)
            );
        }

        
        public static IEnumerable<GaRecordKeyValue<T>> OrderByGradeIndex<T>(this IEnumerable<GaRecordKeyValue<T>> idScalarRecords)
        {
            var termsArray = 
                idScalarRecords.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => t.Key)
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Key.BasisBladeIdToGrade())
                .ThenByDescending(t => t.Key.ReverseBits(bitsCount));
        }
        
        public static IEnumerable<GaRecordKeyValue<T>> OrderById<T>(this IEnumerable<GaRecordKeyValue<T>> idScalarRecords)
        {
            return idScalarRecords
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Key);
        }
        
        public static IEnumerable<GaRecordGradeKeyValue<T>> OrderByGradeIndex<T>(this IEnumerable<GaRecordGradeKeyValue<T>> gradeIndexScalarTuples)
        {
            var termsArray = gradeIndexScalarTuples.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => 
                    t.Key.BasisBladeIndexToId(t.Grade)
                )
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Grade)
                .ThenByDescending(t => 
                    t.Key.BasisBladeIndexToId(t.Grade).ReverseBits(bitsCount)
                );
        }
        
        public static IEnumerable<GaRecordGradeKeyValue<T>> OrderById<T>(this IEnumerable<GaRecordGradeKeyValue<T>> gradeIndexScalarTuples)
        {
            return gradeIndexScalarTuples
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => 
                    t.Key.BasisBladeIndexToId(t.Grade)
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


        public static IGaListEvenDense<T> VectorToArrayVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> vectorStorage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension];

            for (var index = 0; index < vSpaceDimension; index++)
                array[index] = scalarProcessor.GetZeroScalar();

            foreach (var (index, scalar) in vectorStorage.IndexScalarList.GetKeyValueRecords()) 
                array[index] = scalar;

            return array.CreateEvenListDense();
        }

        public static IGaListEvenDense<T> BivectorToArrayVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> bivectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) vSpaceDimension.KVectorSpaceDimension(2
            );

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.GetZeroScalar();

            foreach (var (index, scalar) in bivectorStorage.IndexScalarList.GetKeyValueRecords()) 
                array[index] = scalar;

            return array.CreateEvenListDense();
        }

        public static IGaGridEvenDense<T> BivectorToArray<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> bivectorStorage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension, vSpaceDimension];

            for (var i = 0; i < vSpaceDimension; i++)
            for (var j = 0; j < vSpaceDimension; j++)
                array[i, j] = scalarProcessor.GetZeroScalar();

            foreach (var (index1, index2, scalar) in bivectorStorage.GetBasisVectorsIndexScalarRecords())
            {
                array[index1, index2] = scalar;
                array[index2, index1] = scalarProcessor.Negative(scalar);
            }

            return array.CreateEvenGridDense();
        }

        public static IGaGridEvenDense<T> ScalarPlusBivectorToArray<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> storage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension, vSpaceDimension];

            var scalar = scalarProcessor.GetTermScalar(storage, 0);

            for (var i = 0; i < vSpaceDimension; i++)
            {
                array[i, i] = scalar;

                for (var j = i + 1; j < vSpaceDimension; j++)
                {
                    array[i, j] = scalarProcessor.GetZeroScalar();
                    array[j, i] = scalarProcessor.GetZeroScalar();
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

        public static IGaListEvenDense<T> KVectorToArrayVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> kVectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) vSpaceDimension.KVectorSpaceDimension(kVectorStorage.Grade
            );

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.GetZeroScalar();

            foreach (var (index, scalar) in kVectorStorage.IndexScalarList.GetKeyValueRecords()) 
                array[index] = scalar;

            return array.CreateEvenListDense();
        }

        public static IGaListEvenDense<T> MultivectorToArrayVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> multivectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) vSpaceDimension.ToGaSpaceDimension();

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.GetZeroScalar();

            foreach (var (index, scalar) in multivectorStorage.GetIdScalarRecords()) 
                array[index] = scalar;

            return array.CreateEvenListDense();
        }
    }
}