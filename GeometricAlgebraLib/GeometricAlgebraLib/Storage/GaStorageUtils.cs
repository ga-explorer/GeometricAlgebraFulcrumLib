using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib;
using GeometricAlgebraLib.Frames;
using GeometricAlgebraLib.Multivectors.Bases;
using GeometricAlgebraLib.Multivectors.Terms;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage.Composers;

namespace GeometricAlgebraLib.Storage
{
    public static class GaStorageUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds(this IEnumerable<IGaBasis> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetGrades(this IEnumerable<IGaBasis> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Grade).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this IEnumerable<IGaBasis> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple<int, ulong>> GetGradeIndexTuples(this IEnumerable<IGaBasis> basisBladesList)
        {
            return basisBladesList.Select(basisBlade =>
            {
                basisBlade.GetGradeIndex(out var grade, out var index);
                return new Tuple<int, ulong>(grade, index);
            });
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetGrades<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Grade).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple<int, ulong>> GetGradeIndexTuples<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term =>
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);
                return new Tuple<int, ulong>(grade, index);
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGaBasis> GetBasisBlades<T>(this IEnumerable<GaTerm<T>> termsList)
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
        public static IEnumerable<Tuple<int, ulong, T>> GetGradeIndexScalarTuples<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            return termsList.Select(term =>
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);
                return new Tuple<int, ulong, T>(grade, index, term.Scalar);
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


        public static IGaScalarStorage<T> CreateScalarStorage<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar)
        {
            return GaScalarTermStorage<T>.Create(
                scalarProcessor,
                scalar
            );
        }


        public static IGaScalarStorage<T> SumToScalarStorage<T>(this IGaScalarProcessor<T> scalarProcessor, params T[] scalars)
        {
            return GaScalarTermStorage<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars)
            );
        }

        public static IGaScalarStorage<T> SumToScalarStorage<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return GaScalarTermStorage<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalar1, scalar2)
            );
        }

        public static IGaScalarStorage<T> SumToScalarStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalars)
        {
            return GaScalarTermStorage<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars)
            );
        }


        public static IGaVectorStorage<T> CreateVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(scalarsList);

            return storage.GetVectorStorage();
        }

        public static IGaVectorStorage<T> CreateVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            return storage.GetVectorStorage();
        }

        public static IGaVectorStorage<T> CreateVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            return storage.GetVectorStorage();
        }

        internal static IGaVectorStorage<T> CreateVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaTerm<T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            return storage.GetVectorStorage();
        }


        public static IGaVectorStorage<T> SumToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            return storage.GetVectorStorage();
        }

        public static IGaVectorStorage<T> SumToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            return storage.GetVectorStorage();
        }

        internal static IGaVectorStorage<T> SumToVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaTerm<T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            return storage.GetVectorStorage();
        }


        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, int grade, IEnumerable<T> scalarsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(scalarsList);

            return storage.GetKVectorStorage();
        }

        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, int grade, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            return storage.GetKVectorStorage();
        }

        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, int grade, IEnumerable<Tuple<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            return storage.GetKVectorStorage();
        }

        internal static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, int grade, IEnumerable<GaTerm<T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            return storage.GetKVectorStorage();
        }


        public static IGaKVectorStorage<T> SumToKVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, int grade, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            return storage.GetKVectorStorage();
        }

        public static IGaKVectorStorage<T> SumToKVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, int grade, IEnumerable<Tuple<ulong, T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            return storage.GetKVectorStorage();
        }

        internal static IGaKVectorStorage<T> SumToKVectorStorage<T>(this IGaScalarProcessor<T> scalarProcessor, int grade, IEnumerable<GaTerm<T>> termsList)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            return storage.GetKVectorStorage();
        }


        public static IGaVectorStorage<T> CreateVectorStorage<T>(this IEnumerable<T> scalarsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(scalarsList);

            return storage.GetVectorStorage();
        }

        public static IGaVectorStorage<T> CreateVectorStorage<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            return storage.GetVectorStorage();
        }

        public static IGaVectorStorage<T> CreateVectorStorage<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            return storage.GetVectorStorage();
        }

        internal static IGaVectorStorage<T> CreateVectorStorage<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.SetTerms(termsList);

            return storage.GetVectorStorage();
        }


        public static IGaVectorStorage<T> SumToVectorStorage<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            return storage.GetVectorStorage();
        }

        public static IGaVectorStorage<T> SumToVectorStorage<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            return storage.GetVectorStorage();
        }

        internal static IGaVectorStorage<T> SumToVectorStorage<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            storage.AddTerms(termsList);

            return storage.GetVectorStorage();
        }


        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IEnumerable<T> scalarsList, int grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(scalarsList);

            return storage.GetKVectorStorage();
        }

        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, int grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            return storage.GetKVectorStorage();
        }

        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IEnumerable<Tuple<ulong, T>> termsList, int grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            return storage.GetKVectorStorage();
        }

        internal static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IEnumerable<GaTerm<T>> termsList, int grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.SetTerms(termsList);

            return storage.GetKVectorStorage();
        }


        public static IGaKVectorStorage<T> SumToKVectorStorage<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, int grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            return storage.GetKVectorStorage();
        }

        public static IGaKVectorStorage<T> SumToKVectorStorage<T>(this IEnumerable<Tuple<ulong, T>> termsList, int grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            return storage.GetKVectorStorage();
        }

        internal static IGaKVectorStorage<T> SumToKVectorStorage<T>(this IEnumerable<GaTerm<T>> termsList, int grade, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            storage.AddTerms(termsList);

            return storage.GetKVectorStorage();
        }


        public static IGaMultivectorTermsStorage<T> SumToMultivectorTermsStorage<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.CreateMultivectorTermsStorage();
        }

        public static IGaMultivectorTermsStorage<T> SumToMultivectorTermsStorage<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.CreateMultivectorTermsStorage();
        }

        public static IGaMultivectorTermsStorage<T> SumToMultivectorTermsStorage<T>(this IEnumerable<Tuple<int, ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.CreateMultivectorTermsStorage();
        }

        public static IGaMultivectorTermsStorage<T> SumToMultivectorTermsStorage<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.CreateMultivectorTermsStorage();
        }


        public static IGaMultivectorGradedStorage<T> SumToMultivectorStorageGraded<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.GetMultivectorGradedStorageCopy();
        }

        public static IGaMultivectorGradedStorage<T> SumToMultivectorStorageGraded<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.GetMultivectorGradedStorageCopy();
        }

        public static IGaMultivectorGradedStorage<T> SumToMultivectorStorageGraded<T>(this IEnumerable<Tuple<int, ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.CreateMultivectorGradedStorage();
        }

        public static IGaMultivectorGradedStorage<T> SumToMultivectorStorageGraded<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.CreateMultivectorGradedStorage();
        }


        public static GaMultivectorTreeStorage<T> SumToMultivectorStorageTree<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.GetMultivectorTreeStorageCopy();
        }

        public static GaMultivectorTreeStorage<T> SumToMultivectorStorageTree<T>(this IEnumerable<Tuple<ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.GetMultivectorTreeStorageCopy();
        }

        public static GaMultivectorTreeStorage<T> SumToMultivectorStorageTree<T>(this IEnumerable<Tuple<int, ulong, T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.GetMultivectorTreeStorageCopy();
        }

        public static GaMultivectorTreeStorage<T> SumToMultivectorStorageTree<T>(this IEnumerable<GaTerm<T>> termsList, IGaScalarProcessor<T> scalarProcessor)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            storage.AddTerms(termsList);

            return storage.GetMultivectorTreeStorageCopy();
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

        public static IEnumerable<Tuple<int, ulong, T>> OrderByGradeIndex<T>(this IEnumerable<Tuple<int, ulong, T>> gradeIndexScalarTuples)
        {
            var termsArray = gradeIndexScalarTuples.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => 
                    GaFrameUtils.BasisBladeId(t.Item1, t.Item2)
                )
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Item1)
                .ThenByDescending(t => 
                    GaFrameUtils
                        .BasisBladeId(t.Item1, t.Item2)
                        .ReverseBits(bitsCount)
                );
        }
        
        public static IEnumerable<Tuple<int, ulong, T>> OrderById<T>(this IEnumerable<Tuple<int, ulong, T>> gradeIndexScalarTuples)
        {
            return gradeIndexScalarTuples
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => 
                    GaFrameUtils.BasisBladeId(t.Item1, t.Item2)
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
    }
}