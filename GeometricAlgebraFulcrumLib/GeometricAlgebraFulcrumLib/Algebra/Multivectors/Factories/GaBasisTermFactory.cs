using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Structures;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories
{
    public static class GaBasisTermFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisScalarTerm<T>(this T scalar)
        {
            return GaBasisScalar.BasisScalar.CreateTerm(scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisVectorTerm<T>(this T scalar, int index)
        {
            return index.CreateBasisVector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisVectorTerm<T>(this T scalar, ulong index)
        {
            return index.CreateBasisVector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisVectorTerm<T>(this int index, T scalar)
        {
            return index.CreateBasisVector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisVectorTerm<T>(this ulong index, T scalar)
        {
            return index.CreateBasisVector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisVectorTerm<T>(this GaRecordKeyValue<T> indexScalarRecord)
        {
            var (index, scalar) = indexScalarRecord;

            return index.CreateBasisVectorTerm(scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisBivectorTerm<T>(this T scalar, int basisVectorIndex1, int basisVectorIndex2)
        {
            return GaBasisBladeFactory
                .CreateBasisBivector(basisVectorIndex1, basisVectorIndex2)
                .CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisBivectorTerm<T>(this T scalar, ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            return GaBasisBladeFactory
                .CreateBasisBivector(basisVectorIndex1, basisVectorIndex2)
                .CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisBivectorTerm<T>(this T scalar, int index)
        {
            return index.CreateBasisBivector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisBivectorTerm<T>(this T scalar, ulong index)
        {
            return index.CreateBasisBivector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisBivectorTerm<T>(this int index, T scalar)
        {
            return index.CreateBasisBivector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisBivectorTerm<T>(this ulong index, T scalar)
        {
            return index.CreateBasisBivector().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisBivectorTerm<T>(this GaRecordKeyValue<T> indexScalarRecord)
        {
            var (index, scalar) = indexScalarRecord;

            return index.CreateBasisBivectorTerm(scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisTerm<T>(this T scalar, ulong id)
        {
            return id.CreateBasisBlade().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisTerm<T>(this T scalar, uint grade, int index)
        {
            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisTerm<T>(this T scalar, uint grade, ulong index)
        {
            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisTerm<T>(this T scalar, GaRecordGradeKey gradeIndexRecord)
        {
            var (grade, index) = gradeIndexRecord;

            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisTerm<T>(this ulong id, T scalar)
        {
            return id.CreateBasisBlade().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisTerm<T>(this uint grade, int index, T scalar)
        {
            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisTerm<T>(this uint grade, ulong index, T scalar)
        {
            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisTerm<T>(this uint grade, GaRecordKeyValue<T> indexScalarRecord)
        {
            var (index, scalar) = indexScalarRecord;

            return grade.CreateBasisTerm(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisTerm<T>(this GaRecordKeyValue<T> idScalarRecord)
        {
            var (id, scalar) = idScalarRecord;

            return id.CreateBasisBlade().CreateTerm(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisTerm<T>(this GaRecordKeyValue<T> indexScalarRecord, uint grade)
        {
            var (index, scalar) = indexScalarRecord;

            return grade.CreateBasisTerm(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> CreateBasisTerm<T>(this GaRecordGradeKeyValue<T> gradeIndexValueRecord)
        {
            var (grade, index, scalar) = gradeIndexValueRecord;

            return grade.CreateBasisBlade(index).CreateTerm(scalar);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetBasisVectorTerms<T>(this IEnumerable<GaRecordKeyValue<T>> indexScalarRecords)
        {
            return indexScalarRecords.Select(record => CreateBasisVectorTerm(record));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetBasisBivectorTerms<T>(this IEnumerable<GaRecordKeyValue<T>> indexScalarRecords)
        {
            return indexScalarRecords.Select(record => CreateBasisBivectorTerm(record));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetBasisTerms<T>(this uint grade, IEnumerable<GaRecordKeyValue<T>> indexScalarList)
        {
            return indexScalarList.Select(record => CreateBasisTerm(grade, record));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetBasisTerms<T>(this IEnumerable<GaRecordKeyValue<T>> indexScalarRecords, uint grade)
        {
            return indexScalarRecords.Select(record => CreateBasisTerm(record, grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetBasisTerms<T>(this IEnumerable<GaRecordKeyValue<T>> idScalarList)
        {
            return idScalarList.Select(CreateBasisTerm);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaBasisTerm<T>> GetBasisTerms<T>(this IEnumerable<GaRecordGradeKeyValue<T>> gradeIndexScalarList)
        {
            return gradeIndexScalarList.Select(CreateBasisTerm);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> GetGrades<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordGradeKey> GetGradeIndexRecords<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.GetGradeIndexRecord());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetIdScalarRecords<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.GetIdScalarRecord());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetIndexScalarRecords<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.GetIndexScalarRecord());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordGradeKeyValue<T>> GetGradeIndexScalarRecords<T>(this IEnumerable<GaBasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.GetGradeIndexScalarRecord());
        }
    }
}