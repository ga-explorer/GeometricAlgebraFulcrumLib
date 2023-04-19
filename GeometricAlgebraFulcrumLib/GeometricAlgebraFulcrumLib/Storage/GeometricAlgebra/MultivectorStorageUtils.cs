using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    public static class MultivectorStorageUtils
    {
        internal static IMultivectorStorage<T> BilinearProduct<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, Func<ulong, ulong, IntegerSign> basisSignatureFunction)
        {
            var composer =
                scalarProcessor.CreateVectorStorageComposer();

            var idScalarPairs =
                mv1.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
            {
                foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
                {
                    var signature =
                        basisSignatureFunction(id1, id2);

                    if (signature.IsZero)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature.IsPositive)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorStorageSparse();
        }

        internal static IMultivectorStorage<T> BilinearProduct<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, Func<ulong, ulong, IntegerSign> basisSignatureFunction)
        {
            var composer =
                scalarProcessor.CreateVectorStorageComposer();

            var idScalarPairs1 =
                mv1.GetIdScalarRecords();

            var idScalarPairs2 =
                mv2.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
                {
                    var signature =
                        basisSignatureFunction(id1, id2);

                    if (signature.IsZero)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature.IsPositive)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorStorageSparse();
        }

        internal static IMultivectorGradedStorage<T> BilinearProduct<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv1, Func<ulong, ulong, IntegerSign> basisSignatureFunction)
        {
            var grade1 = mv1.Grade;

            var composer =
                scalarProcessor.CreateMultivectorGradedStorageComposer();

            var indexScalarPairs1 =
                mv1.GetLinVectorIndexScalarStorage();

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs1.GetIndexScalarRecords())
                {
                    var id2 = index2.BasisBladeIndexToId(grade1);

                    var signature =
                        basisSignatureFunction(id1, id2);

                    if (signature.IsZero)
                        continue;

                    var (grade, index) = (id1 ^ id2).BasisBladeIdToGradeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature.IsPositive)
                        composer.AddTerm(grade, index, scalar);
                    else
                        composer.SubtractTerm(grade, index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorGradedStorage();
        }

        internal static IMultivectorGradedStorage<T> BilinearProduct<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2, Func<ulong, ulong, IntegerSign> basisSignatureFunction)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;

            var composer =
                scalarProcessor.CreateMultivectorGradedStorageComposer();

            var indexScalarPairs1 =
                mv1.GetLinVectorIndexScalarStorage();

            var indexScalarPairs2 =
                mv2.GetLinVectorIndexScalarStorage();

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
                {
                    var id2 = index2.BasisBladeIndexToId(grade2);

                    var signature =
                        basisSignatureFunction(id1, id2);

                    if (signature.IsZero)
                        continue;

                    var (grade, index) = (id1 ^ id2).BasisBladeIdToGradeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature.IsPositive)
                        composer.AddTerm(grade, index, scalar);
                    else
                        composer.SubtractTerm(grade, index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorGradedStorage();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> GetNotZeroTerms<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.GetTerms(scalar => !scalarProcessor.IsZero(scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalar<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, ulong id)
        {
            return mv.TryGetTermScalar(id, out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalarByIndex<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv, ulong index)
        {
            return mv.TryGetTermScalarByIndex(index, out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }

        
        public static IEnumerable<KeyValuePair<ulong, T>> OrderByGradeIndex<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var termsArray = termsList.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => t.Key)
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Key.Grade())
                .ThenByDescending(t => t.Key.ReverseBits(bitsCount));
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexScalarRecord<T>> GetIdScalarRecords<T>(this IMultivectorStorageContainer<T> storageContainer)
        {
            return storageContainer.GetMultivectorStorage().GetIdScalarRecords();
        }
        
    }
}