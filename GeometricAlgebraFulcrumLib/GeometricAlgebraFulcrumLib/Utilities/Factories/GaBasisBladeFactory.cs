using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaBasisBladeFactory
    {
        public static GaBasisScalar BasisScalar 
            => GaBasisScalar.BasisScalar;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisSet CreateBasisSet(this int vSpaceDimension)
        {
            if (vSpaceDimension < 1)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            return GaBasisSet.Create((uint) vSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisSet CreateBasisSet(this uint vSpaceDimension)
        {
            if (vSpaceDimension < 1)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            return GaBasisSet.Create(vSpaceDimension);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisVector CreateBasisVector(this int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return GaBasisVector.Create((ulong) index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisVector CreateBasisVector(this ulong index)
        {
            return GaBasisVector.Create(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisVector CreateBasisVector<T>(this IndexScalarRecord<T> indexScalarRecord)
        {
            return GaBasisVector.Create(indexScalarRecord.Index);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBivector CreateBasisBivector(int basisVectorIndex1, int basisVectorIndex2)
        {
            if (basisVectorIndex1 < 0 || basisVectorIndex2 <= basisVectorIndex1)
                throw new ArgumentOutOfRangeException();

            return GaBasisBivector.Create(
                (ulong) basisVectorIndex1, 
                (ulong) basisVectorIndex2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBivector CreateBasisBivector(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            if (basisVectorIndex2 <= basisVectorIndex1)
                throw new ArgumentOutOfRangeException();

            return GaBasisBivector.Create(
                basisVectorIndex1, 
                basisVectorIndex2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBivector CreateBasisBivector(this int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return GaBasisBivector.Create((ulong) index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBivector CreateBasisBivector(this ulong index)
        {
            return GaBasisBivector.Create(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBivector CreateBasisBivector<T>(this IndexScalarRecord<T> indexScalarRecord)
        {
            return GaBasisBivector.Create(indexScalarRecord.Index);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBlade CreateBasisBlade(this ulong id)
        {
            if (id == 0)
                return GaBasisScalar.BasisScalar;

            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return grade switch
            {
                1 => GaBasisVector.Create(index),
                2 => GaBasisBivector.Create(index),
                _ => GaBasisKVector.Create(id, grade, index)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBlade CreateBasisBlade(this uint grade, int index)
        {
            if (index < 0 || (grade == 0 && index != 0))
                throw new ArgumentOutOfRangeException(nameof(index));

            var indexULong = (ulong) index;

            return grade switch
            {
                0 => GaBasisScalar.BasisScalar,
                1 => GaBasisVector.Create(indexULong),
                2 => GaBasisBivector.Create(indexULong),
                _ => GaBasisKVector.Create(
                    indexULong.BasisBladeIndexToId(grade), 
                    grade, 
                    indexULong
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBlade CreateBasisBlade(this uint grade, ulong index)
        {
            if (grade == 0 && index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return grade switch
            {
                0 => GaBasisScalar.BasisScalar,
                1 => GaBasisVector.Create(index),
                2 => GaBasisBivector.Create(index),
                _ => GaBasisKVector.Create(
                    index.BasisBladeIndexToId(grade), 
                    grade, 
                    index
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBlade CreateBasisBlade<T>(this uint grade, IndexScalarRecord<T> indexScalarRecord)
        {
            return CreateBasisBlade(grade, indexScalarRecord.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBlade CreateBasisBlade<T>(this IndexScalarRecord<T> indexScalarRecord, uint grade)
        {
            return CreateBasisBlade(grade, indexScalarRecord.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBlade CreateBasisBlade(this IEnumerable<int> basisVectorIndices)
        {
            return basisVectorIndices
                .BasisVectorIndicesToBasisBladeId()
                .CreateBasisBlade();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBlade CreateBasisBlade(this IEnumerable<ulong> basisVectorIndices)
        {
            return basisVectorIndices
                .BasisVectorIndicesToBasisBladeId()
                .CreateBasisBlade();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBlade CreateBasisBlade(this GradeIndexRecord gradeIndexRecord)
        {
            var (grade, index) = gradeIndexRecord;

            return CreateBasisBlade(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBlade CreateBasisBlade<T>(this IndexScalarRecord<T> idScalarRecord)
        {
            var (id, _) = idScalarRecord;

            return id.CreateBasisBlade();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBlade CreateBasisBlade<T>(this GradeIndexScalarRecord<T> gradeIndexValueRecord)
        {
            var (grade, index, _) = gradeIndexValueRecord;

            return CreateBasisBlade(grade, index);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisVector> GetBasisVectors<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return indexScalarRecords.Select(CreateBasisVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisBivector> GetBasisBivector<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return indexScalarRecords.Select(CreateBasisBivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisBlade> GetBasisBlades<T>(this uint grade, IEnumerable<IndexScalarRecord<T>> indexScalarList)
        {
            return indexScalarList.Select(record => CreateBasisBlade(grade, record));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisBlade> GetBasisBlade<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords, uint grade)
        {
            return indexScalarRecords.Select(record => CreateBasisBlade(record, grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisBlade> GetBasisBlade<T>(this IEnumerable<IndexScalarRecord<T>> idScalarList)
        {
            return idScalarList.Select(CreateBasisBlade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisBlade> GetBasisBlade<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarList)
        {
            return gradeIndexScalarList.Select(CreateBasisBlade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds(this IEnumerable<GaBasisBlade> termsList)
        {
            return termsList.Select(term => term.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this IEnumerable<GaBasisBlade> termsList)
        {
            return termsList.Select(term => term.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> GetGrades(this IEnumerable<GaBasisBlade> termsList)
        {
            return termsList.Select(term => term.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexRecord> GetGradeIndexRecords(this IEnumerable<GaBasisBlade> termsList)
        {
            return termsList.Select(term => term.GetGradeIndexRecord());
        }
    }
}