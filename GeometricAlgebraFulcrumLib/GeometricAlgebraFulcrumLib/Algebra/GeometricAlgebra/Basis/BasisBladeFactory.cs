using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    public static class BasisBladeFactory
    {
        public static BasisScalar BasisScalar
            => BasisScalar.DefaultBasisScalar;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBladeSubSet CreateBasisSubSet(this int vSpaceDimension)
        {
            if (vSpaceDimension < 1)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            return BasisBladeSubSet.Create((uint)vSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBladeSubSet CreateBasisSubSet(this uint vSpaceDimension)
        {
            if (vSpaceDimension < 1)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            return BasisBladeSubSet.Create(vSpaceDimension);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisVector CreateBasisVector(this int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return BasisVector.Create((ulong)index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisVector CreateBasisVector(this ulong index)
        {
            return BasisVector.Create(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisVector CreateBasisVector<T>(this IndexScalarRecord<T> indexScalarRecord)
        {
            return BasisVector.Create(indexScalarRecord.Index);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBivector CreateBasisBivector(int basisVectorIndex1, int basisVectorIndex2)
        {
            if (basisVectorIndex1 < 0 || basisVectorIndex2 <= basisVectorIndex1)
                throw new ArgumentOutOfRangeException();

            return BasisBivector.Create(
                (ulong)basisVectorIndex1,
                (ulong)basisVectorIndex2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBivector CreateBasisBivector(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            if (basisVectorIndex2 <= basisVectorIndex1)
                throw new ArgumentOutOfRangeException();

            return BasisBivector.Create(
                basisVectorIndex1,
                basisVectorIndex2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBivector CreateBasisBivector(this int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return BasisBivector.Create((ulong)index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBivector CreateBasisBivector(this ulong index)
        {
            return BasisBivector.Create(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBivector CreateBasisBivector<T>(this IndexScalarRecord<T> indexScalarRecord)
        {
            return BasisBivector.Create(indexScalarRecord.Index);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBlade CreateBasisBlade(this ulong id)
        {
            if (id == 0)
                return BasisScalar.DefaultBasisScalar;

            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return grade switch
            {
                1 => BasisVector.Create(index),
                2 => BasisBivector.Create(index),
                _ => BasisKVector.Create(id, grade, index)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBlade CreateBasisBlade(this uint grade, int index)
        {
            if (index < 0 || grade == 0 && index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            var indexULong = (ulong)index;

            return grade switch
            {
                0 => BasisScalar.DefaultBasisScalar,
                1 => BasisVector.Create(indexULong),
                2 => BasisBivector.Create(indexULong),
                _ => BasisKVector.Create(
                    indexULong.BasisBladeIndexToId(grade),
                    grade,
                    indexULong
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBlade CreateBasisBlade(this uint grade, ulong index)
        {
            if (grade == 0 && index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return grade switch
            {
                0 => BasisScalar.DefaultBasisScalar,
                1 => BasisVector.Create(index),
                2 => BasisBivector.Create(index),
                _ => BasisKVector.Create(
                    index.BasisBladeIndexToId(grade),
                    grade,
                    index
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBlade CreateBasisBlade<T>(this uint grade, IndexScalarRecord<T> indexScalarRecord)
        {
            return grade.CreateBasisBlade(indexScalarRecord.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBlade CreateBasisBlade<T>(this IndexScalarRecord<T> indexScalarRecord, uint grade)
        {
            return grade.CreateBasisBlade(indexScalarRecord.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBlade CreateBasisBlade(this IEnumerable<int> basisVectorIndices)
        {
            return basisVectorIndices
                .BasisVectorIndicesToBasisBladeId()
                .CreateBasisBlade();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBlade CreateBasisBlade(this IEnumerable<ulong> basisVectorIndices)
        {
            return basisVectorIndices
                .BasisVectorIndicesToBasisBladeId()
                .CreateBasisBlade();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBlade CreateBasisBlade(this GradeIndexRecord gradeIndexRecord)
        {
            var (grade, index) = gradeIndexRecord;

            return grade.CreateBasisBlade(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBlade CreateBasisBlade<T>(this IndexScalarRecord<T> idScalarRecord)
        {
            var (id, _) = idScalarRecord;

            return id.CreateBasisBlade();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBlade CreateBasisBlade<T>(this GradeIndexScalarRecord<T> gradeIndexValueRecord)
        {
            var (grade, index, _) = gradeIndexValueRecord;

            return grade.CreateBasisBlade(index);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisVector> GetBasisVectors<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return indexScalarRecords.Select(CreateBasisVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisBivector> GetBasisBivector<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return indexScalarRecords.Select(CreateBasisBivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisBlade> GetBasisBlades<T>(this uint grade, IEnumerable<IndexScalarRecord<T>> indexScalarList)
        {
            return indexScalarList.Select(record => grade.CreateBasisBlade(record));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisBlade> GetBasisBlade<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords, uint grade)
        {
            return indexScalarRecords.Select(record => record.CreateBasisBlade(grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisBlade> GetBasisBlade<T>(this IEnumerable<IndexScalarRecord<T>> idScalarList)
        {
            return idScalarList.Select(CreateBasisBlade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisBlade> GetBasisBlade<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarList)
        {
            return gradeIndexScalarList.Select(CreateBasisBlade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds(this IEnumerable<BasisBlade> termsList)
        {
            return termsList.Select(term => term.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this IEnumerable<BasisBlade> termsList)
        {
            return termsList.Select(term => term.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> GetGrades(this IEnumerable<BasisBlade> termsList)
        {
            return termsList.Select(term => term.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexRecord> GetGradeIndexRecords(this IEnumerable<BasisBlade> termsList)
        {
            return termsList.Select(term => term.GetGradeIndexRecord());
        }
    }
}