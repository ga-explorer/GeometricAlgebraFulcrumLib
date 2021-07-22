using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;


namespace GeometricAlgebraFulcrumLib.Storage
{
    public static class GasStorageUtils
    {
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


        public static T[] VectorToArray<T>(this IGasVector<T> vectorStorage, uint vSpaceDimension)
        {
            var scalarProcessor = vectorStorage.ScalarProcessor;

            var array = new T[vSpaceDimension];

            for (var index = 0; index < vSpaceDimension; index++)
                array[index] = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar) in vectorStorage.GetIndexScalarPairs()) 
                array[index] = scalar;

            return array;
        }

        public static T[] BivectorToArray<T>(this IGasBivector<T> bivectorStorage, uint vSpaceDimension)
        {
            var scalarProcessor = bivectorStorage.ScalarProcessor;

            var arrayLength = (int) GaBasisUtils.KvSpaceDimension(
                vSpaceDimension, 
                2
            );

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar) in bivectorStorage.GetIndexScalarPairs()) 
                array[index] = scalar;

            return array;
        }

        public static T[,] BivectorToArray2D<T>(this IGasBivector<T> bivectorStorage, uint vSpaceDimension)
        {
            var scalarProcessor = bivectorStorage.ScalarProcessor;

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

        public static T[,] ScalarPlusBivectorToArray2D<T>(this IGasMultivector<T> storage, uint vSpaceDimension)
        {
            var scalarProcessor = storage.ScalarProcessor;

            var array = new T[vSpaceDimension, vSpaceDimension];

            var scalar = storage.GetTermScalar(0);

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

        public static T[] KVectorToArray<T>(this IGasKVector<T> kVectorStorage, uint vSpaceDimension)
        {
            var scalarProcessor = kVectorStorage.ScalarProcessor;

            var arrayLength = (int) GaBasisUtils.KvSpaceDimension(
                vSpaceDimension, 
                kVectorStorage.Grade
            );

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar) in kVectorStorage.GetIndexScalarPairs()) 
                array[index] = scalar;

            return array;
        }

        public static T[] MultivectorToArray<T>(this IGasMultivector<T> multivectorStorage, uint vSpaceDimension)
        {
            var scalarProcessor = multivectorStorage.ScalarProcessor;

            var arrayLength = (int) vSpaceDimension.ToGaSpaceDimension();

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ZeroScalar;

            foreach (var (index, scalar) in multivectorStorage.GetIdScalarPairs()) 
                array[index] = scalar;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> GetNegativeScalarPart<T>(this IGasMultivector<T> mv)
        {
            return mv.GetScalarPart(
                mv.ScalarProcessor.Negative
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVector<T> GetNegativeVectorPart<T>(this IGasMultivector<T> mv)
        {
            return mv.GetVectorPart(
                mv.ScalarProcessor.Negative
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivector<T> GetNegativeBivectorPart<T>(this IGasMultivector<T> mv)
        {
            return mv.GetBivectorPart(
                mv.ScalarProcessor.Negative
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> GetNegativeKVectorPart<T>(this IGasMultivector<T> mv, uint grade)
        {
            return mv.GetKVectorPart(
                grade,
                mv.ScalarProcessor.Negative
            );
        }
    }
}