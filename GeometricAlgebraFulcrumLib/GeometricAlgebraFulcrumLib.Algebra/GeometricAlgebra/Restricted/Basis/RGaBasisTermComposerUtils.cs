using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;

public static class BasisTermFactory
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisScalarTerm<T>(this T scalar)
    {
        return new KeyValuePair<ulong, T>(
            0, 
            scalar
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisVectorTerm<T>(this T scalar, int index)
    {
        return new KeyValuePair<ulong, T>(
            index.BasisVectorIndexToId(), 
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisVectorTerm<T>(this T scalar, ulong index)
    {
        return new KeyValuePair<ulong, T>(
            index.BasisVectorIndexToId(), 
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisVectorTerm<T>(this int index, T scalar)
    {
        return new KeyValuePair<ulong, T>(
            index.BasisVectorIndexToId(), 
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisVectorTerm<T>(this ulong index, T scalar)
    {
        return new KeyValuePair<ulong, T>(
            index.BasisVectorIndexToId(), 
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisVectorTerm<T>(this RGaKvIndexScalarRecord<T> indexScalarRecord)
    {
        var (index, scalar) = indexScalarRecord;

        return index.CreateBasisVectorTerm(scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisBivectorTerm<T>(this T scalar, int basisVectorIndex1, int basisVectorIndex2)
    {
        return new KeyValuePair<ulong, T>(
            BasisBivectorUtils.IndexPairToBivectorId(
                basisVectorIndex1, 
                basisVectorIndex2
            ), 
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisBivectorTerm<T>(this T scalar, ulong basisVectorIndex1, ulong basisVectorIndex2)
    {
        return new KeyValuePair<ulong, T>(
            BasisBivectorUtils.IndexPairToBivectorId(
                basisVectorIndex1, 
                basisVectorIndex2
            ), 
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisBivectorTerm<T>(this T scalar, int index)
    {
        return new KeyValuePair<ulong, T>(
            index.BasisBivectorIndexToId(),
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisBivectorTerm<T>(this T scalar, ulong index)
    {
        return new KeyValuePair<ulong, T>(
            index.BasisBivectorIndexToId(),
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisBivectorTerm<T>(this int index, T scalar)
    {
        return new KeyValuePair<ulong, T>(
            index.BasisBivectorIndexToId(),
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisBivectorTerm<T>(this ulong index, T scalar)
    {
        return new KeyValuePair<ulong, T>(
            index.BasisBivectorIndexToId(),
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisBivectorTerm<T>(this RGaKvIndexScalarRecord<T> indexScalarRecord)
    {
        var (index, scalar) = indexScalarRecord;

        return index.CreateBasisBivectorTerm(scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisTerm<T>(this T scalar, ulong id)
    {
        return new KeyValuePair<ulong, T>(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisTerm<T>(this T scalar, uint grade, int index)
    {
        var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, (ulong) index);

        return new KeyValuePair<ulong, T>(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisTerm<T>(this T scalar, uint grade, ulong index)
    {
        var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

        return new KeyValuePair<ulong, T>(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisTerm<T>(this T scalar, RGaGradeKvIndexRecord gradeIndexRecord)
    {
        var (grade, index) = gradeIndexRecord;

        var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

        return new KeyValuePair<ulong, T>(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisTerm<T>(this ulong id, T scalar)
    {
        return new KeyValuePair<ulong, T>(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisTerm<T>(this uint grade, int index, T scalar)
    {
        var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, (ulong) index);

        return new KeyValuePair<ulong, T>(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisTerm<T>(this uint grade, ulong index, T scalar)
    {
        var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

        return new KeyValuePair<ulong, T>(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisTerm<T>(this uint grade, RGaKvIndexScalarRecord<T> indexScalarRecord)
    {
        var (index, scalar) = indexScalarRecord;

        return grade.CreateBasisTerm(index, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisTerm<T>(this RGaKvIndexScalarRecord<T> idScalarRecord)
    {
        var (id, scalar) = idScalarRecord;

        return new KeyValuePair<ulong, T>(id, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisTerm<T>(this RGaKvIndexScalarRecord<T> indexScalarRecord, uint grade)
    {
        var (index, scalar) = indexScalarRecord;

        return grade.CreateBasisTerm(index, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<ulong, T> CreateBasisTerm<T>(this RGaGradeKvIndexScalarRecord<T> gradeIndexValueRecord)
    {
        var (grade, index, scalar) = gradeIndexValueRecord;

        var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

        return new KeyValuePair<ulong, T>(id, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<ulong, T>> GetBasisVectorTerms<T>(this IEnumerable<RGaKvIndexScalarRecord<T>> indexScalarRecords)
    {
        return indexScalarRecords.Select(record => record.CreateBasisVectorTerm());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<ulong, T>> GetBasisBivectorTerms<T>(this IEnumerable<RGaKvIndexScalarRecord<T>> indexScalarRecords)
    {
        return indexScalarRecords.Select(record => record.CreateBasisBivectorTerm());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<ulong, T>> GetBasisTerms<T>(this uint grade, IEnumerable<RGaKvIndexScalarRecord<T>> indexScalarList)
    {
        return indexScalarList.Select(record => grade.CreateBasisTerm(record));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<ulong, T>> GetBasisTerms<T>(this IEnumerable<RGaKvIndexScalarRecord<T>> indexScalarRecords, uint grade)
    {
        return indexScalarRecords.Select(record => record.CreateBasisTerm(grade));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<ulong, T>> GetBasisTerms<T>(this IEnumerable<RGaKvIndexScalarRecord<T>> idScalarList)
    {
        return idScalarList.Select(CreateBasisTerm);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<ulong, T>> GetBasisTerms<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> gradeIndexScalarList)
    {
        return gradeIndexScalarList.Select(CreateBasisTerm);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ulong> GetIds<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList)
    {
        return termsList.Select(term => term.Key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ulong> GetIndices<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList)
    {
        return termsList.Select(term => term.Key.BasisBladeIdToIndex());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> GetGrades<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList)
    {
        return termsList.Select(term => term.Key.Grade());
    }
        
}