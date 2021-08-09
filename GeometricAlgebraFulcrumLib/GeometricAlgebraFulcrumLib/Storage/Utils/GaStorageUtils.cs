using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Terms;

namespace GeometricAlgebraFulcrumLib.Storage.Utils
{
    public static class GaStorageUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs<T>(this IGaStorageMultivector<T> mv, Func<T, bool> filterFunc)
        {
            return mv
                .GetIdScalarPairs()
                .Where(pair => filterFunc(pair.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs<T>(this IGaStorageMultivector<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv
                .GetIdScalarPairs()
                .Where(pair => filterFunc(pair.Key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs<T>(this IGaStorageMultivector<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv
                .GetIdScalarPairs()
                .Where(pair => filterFunc(pair.Key, pair.Value));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetIndexScalarPairs<T>(this IGaStorageKVector<T> mv, Func<T, bool> filterFunc)
        {
            return mv
                .IndexScalarDictionary
                .Where(pair => filterFunc(pair.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetIndexScalarPairs<T>(this IGaStorageKVector<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv
                .IndexScalarDictionary
                .Where(pair => filterFunc(pair.Key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetIndexScalarPairs<T>(this IGaStorageKVector<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv
                .IndexScalarDictionary
                .Where(pair => filterFunc(pair.Key, pair.Value));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetNotZeroIdScalarPairs<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetIdScalarPairs(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetNotZeroIdScalarPairs<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetIdScalarPairs(scalar => !scalarProcessor.IsNearZero(scalar))
                : mv.GetIdScalarPairs(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetNotNearZeroIdScalarPairs<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetIdScalarPairs(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetNearZeroIdScalarPairs<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetIdScalarPairs(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetNotZeroIndexScalarPairs<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv)
        {
            return mv.GetIndexScalarPairs(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetNotZeroIndexScalarPairs<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetIndexScalarPairs(scalar => !scalarProcessor.IsNearZero(scalar))
                : mv.GetIndexScalarPairs(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetNotNearZeroIndexScalarPairs<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv)
        {
            return mv.GetIndexScalarPairs(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetNearZeroIndexScalarPairs<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv)
        {
            return mv.GetIndexScalarPairs(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaTerm<T>> GetTerms<T>(this IGaStorageMultivector<T> mv, Func<T, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaTerm<T>> GetTerms<T>(this IGaStorageMultivector<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaTerm<T>> GetTerms<T>(this IGaStorageMultivector<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaTerm<T>> GetNotZeroTerms<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetTerms(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaTerm<T>> GetNotZeroTerms<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetTerms(scalar => !scalarProcessor.IsNearZero(scalar)) 
                : mv.GetTerms(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaTerm<T>> GetNotNearZeroTerms<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetTerms(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaTerm<T>> GetNearZeroTerms<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetTerms(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.TryGetTermScalar(0, out var scalar)
                ? scalar
                : scalarProcessor.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, ulong id)
        {
            return mv.TryGetTermScalar(id, out var scalar)
                ? scalar
                : scalarProcessor.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, uint grade, ulong index)
        {
            return mv.TryGetTermScalar(grade, index, out var scalar)
                ? scalar
                : scalarProcessor.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalarByIndex<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, int index)
        {
            return mv.TryGetTermScalarByIndex((ulong) index, out var scalar)
                ? scalar
                : scalarProcessor.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalarByIndex<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, ulong index)
        {
            return mv.TryGetTermScalarByIndex(index, out var scalar)
                ? scalar
                : scalarProcessor.ZeroScalar;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm<T> GetTerm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, ulong id)
        {
            return GaTerm<T>.CreateUniform(
                id,
                scalarProcessor.GetTermScalar(mv, id)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm<T> GetTerm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, uint grade, ulong index)
        {
            return GaTerm<T>.CreateGraded(
                grade, 
                index,
                scalarProcessor.GetTermScalar(mv, grade, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm<T> GetTermByIndex<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, int index)
        {
            return GaTerm<T>.CreateGraded(
                mv.Grade, 
                (ulong) index,
                scalarProcessor.GetTermScalarByIndex(mv, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm<T> GetTermByIndex<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, ulong index)
        {
            return GaTerm<T>.CreateGraded(
                mv.Grade, 
                index,
                scalarProcessor.GetTermScalarByIndex(mv, index)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds(this IEnumerable<IGaBasisBlade> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> GetGrades(this IEnumerable<IGaBasisBlade> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Grade).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this IEnumerable<IGaBasisBlade> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple<uint, ulong>> GetGradeIndexTuples(this IEnumerable<IGaBasisBlade> basisBladesList)
        {
            return basisBladesList.Select(basisBlade =>
            {
                basisBlade.GetGradeIndex(out var grade, out var index);
                return new Tuple<uint, ulong>(grade, index);
            });
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> GetGrades<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Grade).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple<uint, ulong>> GetGradeIndexTuples<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term =>
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);
                return new Tuple<uint, ulong>(grade, index);
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGaBasisBlade> GetBasisBlades<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalars<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => term.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new KeyValuePair<ulong, T>(term.BasisBlade.Id, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple<ulong, T>> GetIdScalarTuples<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new Tuple<ulong, T>(term.BasisBlade.Id, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple<uint, ulong, T>> GetGradeIndexScalarTuples<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term =>
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);
                return new Tuple<uint, ulong, T>(grade, index, term.Scalar);
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetIndexScalarPairs<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new KeyValuePair<ulong, T>(term.BasisBlade.Index, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple<ulong, T>> GetIndexScalarTuples<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new Tuple<ulong, T>(term.BasisBlade.Index, term.Scalar)
            );
        }

        
        public static IEnumerable<KeyValuePair<ulong, T>> OrderByGradeIndex<T>(this IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            var termsArray = idScalarPairs.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => t.Key)
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Key.BasisBladeGrade())
                .ThenByDescending(t => t.Key.ReverseBits(bitsCount));
        }
        
        public static IEnumerable<KeyValuePair<ulong, T>> OrderById<T>(this IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            return idScalarPairs
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Key);
        }

        public static IEnumerable<Tuple<ulong, T>> OrderByGradeIndex<T>(this IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            var termsArray = idScalarTuples.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => t.Item1)
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Item1.BasisBladeGrade())
                .ThenByDescending(t => t.Item1.ReverseBits(bitsCount));
        }
        
        public static IEnumerable<Tuple<ulong, T>> OrderById<T>(this IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            return idScalarTuples
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Item1);
        }

        public static IEnumerable<Tuple<uint, ulong, T>> OrderByGradeIndex<T>(this IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples)
        {
            var termsArray = gradeIndexScalarTuples.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => 
                    GaBasisUtils.BasisBladeId(t.Item1, t.Item2)
                )
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Item1)
                .ThenByDescending(t => 
                    GaBasisUtils
                        .BasisBladeId(t.Item1, t.Item2)
                        .ReverseBits(bitsCount)
                );
        }
        
        public static IEnumerable<Tuple<uint, ulong, T>> OrderById<T>(this IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples)
        {
            return gradeIndexScalarTuples
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => 
                    GaBasisUtils.BasisBladeId(t.Item1, t.Item2)
                );
        }

        public static IEnumerable<GaTerm<T>> OrderByGradeIndex<T>(this IEnumerable<GaTerm<T>> termsList)
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
        
        public static IEnumerable<GaTerm<T>> OrderById<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.BasisBlade.Id);
        }


        public static T[] VectorToArray<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> vectorStorage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension];

            for (var index = 0; index < vSpaceDimension; index++)
                array[index] = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar) in vectorStorage.IndexScalarDictionary) 
                array[index] = scalar;

            return array;
        }

        public static T[] BivectorToArray<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> bivectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) GaBasisUtils.KvSpaceDimension(
                vSpaceDimension, 
                2
            );

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar) in bivectorStorage.IndexScalarDictionary) 
                array[index] = scalar;

            return array;
        }

        public static T[,] BivectorToArray2D<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> bivectorStorage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension, vSpaceDimension];

            for (var i = 0; i < vSpaceDimension; i++)
            for (var j = 0; j < vSpaceDimension; j++)
                array[i, j] = scalarProcessor.ZeroScalar;

            foreach (var (index1, index2, scalar) in bivectorStorage.GetBasisVectorsIndexScalarTuples())
            {
                array[index1, index2] = scalar;
                array[index2, index1] = scalarProcessor.Negative(scalar);
            }

            return array;
        }

        public static T[,] ScalarPlusBivectorToArray2D<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> storage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension, vSpaceDimension];

            var scalar = scalarProcessor.GetTermScalar(storage, 0);

            for (var i = 0; i < vSpaceDimension; i++)
            {
                array[i, i] = scalar;

                for (var j = i + 1; j < vSpaceDimension; j++)
                {
                    array[i, j] = scalarProcessor.ZeroScalar;
                    array[j, i] = scalarProcessor.ZeroScalar;
                }
            }

            var bivectorTerms = storage
                .GetTerms()
                .Where(term => term.BasisBlade.Grade == 2);

            foreach (var term in bivectorTerms)
            {
                var bivectorScalar = term.Scalar;
                var basisVectorIndices = 
                    term.BasisBlade.GetBasisVectorIndices();

                var index1 = basisVectorIndices[0];
                var index2 = basisVectorIndices[1];

                array[index1, index2] = bivectorScalar;
                array[index2, index1] = scalarProcessor.Negative(bivectorScalar);
            }

            return array;
        }

        public static T[] KVectorToArray<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> kVectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) GaBasisUtils.KvSpaceDimension(
                vSpaceDimension, 
                kVectorStorage.Grade
            );

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar) in kVectorStorage.IndexScalarDictionary) 
                array[index] = scalar;

            return array;
        }

        public static T[] MultivectorToArray<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> multivectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) vSpaceDimension.ToGaSpaceDimension();

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar) in multivectorStorage.GetIdScalarPairs()) 
                array[index] = scalar;

            return array;
        }
    }
}