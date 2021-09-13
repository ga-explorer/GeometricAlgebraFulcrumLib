using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class BasisTermFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisScalarTerm<T>(this T scalar)
        {
            return BasisScalar.DefaultBasisScalar.CreateTerm(scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisVectorTerm<T>(this T scalar, int index)
        {
            return index.CreateBasisVector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisVectorTerm<T>(this T scalar, ulong index)
        {
            return index.CreateBasisVector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisVectorTerm<T>(this int index, T scalar)
        {
            return index.CreateBasisVector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisVectorTerm<T>(this ulong index, T scalar)
        {
            return index.CreateBasisVector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisVectorTerm<T>(this IndexScalarRecord<T> indexScalarRecord)
        {
            var (index, scalar) = indexScalarRecord;

            return index.CreateBasisVectorTerm(scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisBivectorTerm<T>(this T scalar, int basisVectorIndex1, int basisVectorIndex2)
        {
            return BasisBladeFactory
                .CreateBasisBivector(basisVectorIndex1, basisVectorIndex2)
                .CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisBivectorTerm<T>(this T scalar, ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            return BasisBladeFactory
                .CreateBasisBivector(basisVectorIndex1, basisVectorIndex2)
                .CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisBivectorTerm<T>(this T scalar, int index)
        {
            return index.CreateBasisBivector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisBivectorTerm<T>(this T scalar, ulong index)
        {
            return index.CreateBasisBivector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisBivectorTerm<T>(this int index, T scalar)
        {
            return index.CreateBasisBivector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisBivectorTerm<T>(this ulong index, T scalar)
        {
            return index.CreateBasisBivector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisBivectorTerm<T>(this IndexScalarRecord<T> indexScalarRecord)
        {
            var (index, scalar) = indexScalarRecord;

            return index.CreateBasisBivectorTerm(scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisTerm<T>(this T scalar, ulong id)
        {
            return id.CreateBasisBlade().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisTerm<T>(this T scalar, uint grade, int index)
        {
            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisTerm<T>(this T scalar, uint grade, ulong index)
        {
            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisTerm<T>(this T scalar, GradeIndexRecord gradeIndexRecord)
        {
            var (grade, index) = gradeIndexRecord;

            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisTerm<T>(this ulong id, T scalar)
        {
            return id.CreateBasisBlade().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisTerm<T>(this uint grade, int index, T scalar)
        {
            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisTerm<T>(this uint grade, ulong index, T scalar)
        {
            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisTerm<T>(this uint grade, IndexScalarRecord<T> indexScalarRecord)
        {
            var (index, scalar) = indexScalarRecord;

            return grade.CreateBasisTerm(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisTerm<T>(this IndexScalarRecord<T> idScalarRecord)
        {
            var (id, scalar) = idScalarRecord;

            return id.CreateBasisBlade().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisTerm<T>(this IndexScalarRecord<T> indexScalarRecord, uint grade)
        {
            var (index, scalar) = indexScalarRecord;

            return grade.CreateBasisTerm(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> CreateBasisTerm<T>(this GradeIndexScalarRecord<T> gradeIndexValueRecord)
        {
            var (grade, index, scalar) = gradeIndexValueRecord;

            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetBasisVectorTerms<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return indexScalarRecords.Select(record => CreateBasisVectorTerm(record));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetBasisBivectorTerms<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return indexScalarRecords.Select(record => CreateBasisBivectorTerm(record));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetBasisTerms<T>(this uint grade, IEnumerable<IndexScalarRecord<T>> indexScalarList)
        {
            return indexScalarList.Select(record => CreateBasisTerm(grade, record));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetBasisTerms<T>(this IEnumerable<IndexScalarRecord<T>> indexScalarRecords, uint grade)
        {
            return indexScalarRecords.Select(record => CreateBasisTerm(record, grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetBasisTerms<T>(this IEnumerable<IndexScalarRecord<T>> idScalarList)
        {
            return idScalarList.Select(CreateBasisTerm);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetBasisTerms<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarList)
        {
            return gradeIndexScalarList.Select(CreateBasisTerm);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> GetGrades<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexRecord> GetGradeIndexRecords<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.GetGradeIndexRecord());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.GetIdScalarRecord());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.GetIndexScalarRecord());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.GetGradeIndexScalarRecord());
        }
    }
}