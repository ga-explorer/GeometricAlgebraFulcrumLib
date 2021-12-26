using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageUtils
    {
        internal static IMultivectorStorage<T> BilinearProduct<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, Func<ulong, ulong, int> basisSignatureFunction)
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

                    if (signature == 0) 
                        continue;

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorSparseStorage();
        }

        internal static IMultivectorStorage<T> BilinearProduct<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, Func<ulong, ulong, int> basisSignatureFunction)
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

                    if (signature == 0) 
                        continue;

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorSparseStorage();
        }

        internal static IMultivectorGradedStorage<T> BilinearProduct<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, Func<ulong, ulong, int> basisSignatureFunction)
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

                    if (signature == 0) 
                        continue;

                    var (grade, index) = (id1 ^ id2).BasisBladeIdToGradeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(grade, index, scalar);
                    else
                        composer.SubtractTerm(grade, index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorGradedStorage();
        }

        internal static IMultivectorGradedStorage<T> BilinearProduct<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2, Func<ulong, ulong, int> basisSignatureFunction)
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

                    if (signature == 0) 
                        continue;

                    var (grade, index) = (id1 ^ id2).BasisBladeIdToGradeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(grade, index, scalar);
                    else
                        composer.SubtractTerm(grade, index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateMultivectorGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<double> Dual(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1)
        {
            var scalarProcessor = ScalarAlgebraFloat64Processor.DefaultProcessor;

            var pseudoScalarInverse =
                scalarProcessor.CreatePseudoScalarInverseStorage(basisSet);
            
            return scalarProcessor.Lcp(basisSet, mv1, pseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Dual<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1)
        {
            var pseudoScalarInverse =
                scalarProcessor.CreatePseudoScalarInverseStorage(basisSet);

            return scalarProcessor.Lcp(basisSet, mv1, pseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Dual<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, BasisBladeSet basisSet)
        {
            var pseudoScalarInverse =
                scalarProcessor.CreatePseudoScalarInverseStorage(basisSet);

            return scalarProcessor.Lcp(basisSet, mv1, pseudoScalarInverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<double> UnDual(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1)
        {
            var pseudoScalarReverse =
                ScalarAlgebraFloat64Processor.DefaultProcessor.CreatePseudoScalarReverseStorage(basisSet.VSpaceDimension);

            return ScalarAlgebraFloat64Processor.DefaultProcessor.Lcp(basisSet, mv1, pseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> UnDual<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1)
        {
            var pseudoScalarReverse =
                scalarProcessor.CreatePseudoScalarReverseStorage(basisSet.VSpaceDimension);

            return scalarProcessor.Lcp(basisSet, mv1, pseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> UnDual<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, BasisBladeSet basisSet)
        {
            var pseudoScalarReverse =
                scalarProcessor.CreatePseudoScalarReverseStorage(basisSet.VSpaceDimension);

            return scalarProcessor.Lcp(basisSet, mv1, pseudoScalarReverse);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<double> BladeInverse(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1)
        {
            var bladeSpSquared = basisSet.Sp(mv1);

            return ScalarAlgebraFloat64Processor.DefaultProcessor.Divide(mv1, bladeSpSquared);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> BladeInverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, KVectorStorage<T> kVector)
        {
            var bladeSpSquared = scalarProcessor.Sp(basisSet, kVector);

            return scalarProcessor.Divide(kVector, bladeSpSquared);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> BladeInverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, BasisBladeSet basisSet)
        {
            var bladeSpSquared = scalarProcessor.Sp(basisSet, mv1);

            return scalarProcessor.Divide(mv1, bladeSpSquared);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<double> VersorInverse(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1)
        {
            var versorSpReverse = basisSet.NormSquared(mv1);

            return ScalarAlgebraFloat64Processor.DefaultProcessor.Divide(ScalarAlgebraFloat64Processor.DefaultProcessor.Reverse(mv1), versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> VersorInverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1)
        {
            var versorSpReverse = scalarProcessor.NormSquared(basisSet, mv1);

            return scalarProcessor.Divide(scalarProcessor.Reverse(mv1), versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> VersorInverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, BasisBladeSet basisSet)
        {
            var versorSpReverse = scalarProcessor.NormSquared(basisSet, mv1);

            return scalarProcessor.Divide(scalarProcessor.Reverse(mv1), versorSpReverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> EDual<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, uint vSpaceDimension)
        {
            var pseudoScalarInverse =
                scalarProcessor.CreateEuclideanPseudoScalarInverseStorage(vSpaceDimension);

            return scalarProcessor.ELcp(mv1, pseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> EDual<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, uint vSpaceDimension)
        {
            var pseudoScalarInverse =
                scalarProcessor.CreateEuclideanPseudoScalarInverseStorage(vSpaceDimension);

            return scalarProcessor.ELcp(mv1, pseudoScalarInverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> EDual<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> mv1)
        {
            return processor.EDual(mv1, processor.VSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> EDual<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            return processor.EDual(mv1, processor.VSpaceDimension);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> EUnDual<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, uint vSpaceDimension)
        {
            var pseudoScalarReverse =
                scalarProcessor.CreatePseudoScalarReverseStorage(vSpaceDimension);

            return scalarProcessor.ELcp(mv1, pseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> EUnDual<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, uint vSpaceDimension)
        {
            var pseudoScalarReverse =
                scalarProcessor.CreatePseudoScalarReverseStorage(vSpaceDimension);

            return scalarProcessor.ELcp(mv1, pseudoScalarReverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> EUnDual<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> mv1)
        {
            return processor.EUnDual(mv1, processor.VSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> EUnDual<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            return processor.EUnDual(mv1, processor.VSpaceDimension);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> EBladeInverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> kVector)
        {
            var bladeSpSquared = scalarProcessor.ESp(kVector);

            return scalarProcessor.Divide(kVector, bladeSpSquared);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> EBladeInverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            var bladeSpSquared = scalarProcessor.ESp(mv1);

            return scalarProcessor.Divide(mv1, bladeSpSquared);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> EVersorInverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1)
        {
            var versorSpReverse = scalarProcessor.ENormSquared(mv1);

            return scalarProcessor.Divide(scalarProcessor.Reverse(mv1), versorSpReverse);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static KVectorStorage<T> Dual<T>(this IGeometricAlgebraProcessor<T> processor, BivectorStorage<T> mv1)
        //{
        //    return processor.Lcp(mv1, processor.PseudoScalarInverse);
        //}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Dual<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> mv1)
        {
            return processor.Lcp(mv1, processor.PseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Dual<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            return processor.Lcp(mv1, processor.PseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Dual<T>(this IMultivectorStorage<T> mv1, IGeometricAlgebraProcessor<T> processor)
        {
            return processor.Lcp(mv1, processor.PseudoScalarInverse);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> UnDual<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> mv1)
        {
            return processor.Lcp(mv1, processor.PseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> UnDual<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            return processor.Lcp(mv1, processor.PseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> UnDual<T>(this IMultivectorStorage<T> mv1, IGeometricAlgebraProcessor<T> processor)
        {
            return processor.Lcp(mv1, processor.PseudoScalarReverse);
        }
        
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IMultivectorStorage<T> BladeInverse<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        //{
        //    var bladeSpSquared = processor.Sp(mv1);

        //    return processor.Divide(mv1, bladeSpSquared);
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> VersorInverse<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            var versorSpReverse = processor.NormSquared(mv1);

            return processor.Divide(processor.Reverse(mv1), versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> VersorInverse<T>(this IMultivectorStorage<T> mv1, IGeometricAlgebraProcessor<T> processor)
        {
            var versorSpReverse = processor.NormSquared(mv1);

            return processor.Divide(processor.Reverse(mv1), versorSpReverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KVectorStorage<T>> GetKVectorStorages<T>(this IMultivectorStorage<T> multivector)
        {
            return multivector
                .GetLinVectorGradedStorage()
                .GetGradeStorageRecords()
                .Select(
                    gradeListRecord => gradeListRecord.CreateKVectorStorage()
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeLinVectorStorageScalarRecord<T> GetScaledListRecord<T>(this KVectorStorage<T> kVector, T scalingFactor)
        {
            return new GradeLinVectorStorageScalarRecord<T>(
                kVector.Grade,
                kVector.GetLinVectorIndexScalarStorage(),
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorStorageScalarRecord<T> GetScaledListRecord<T>(this MultivectorStorage<T> multivector, T scalingFactor)
        {
            return new LinVectorStorageScalarRecord<T>(
                multivector.GetLinVectorIdScalarStorage(),
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorGradedStorageScalarRecord<T> GetScaledListRecord<T>(this IMultivectorGradedStorage<T> multivector, T scalingFactor)
        {
            return new LinVectorGradedStorageScalarRecord<T>(
                multivector.GetLinVectorGradedStorage(),
                scalingFactor
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMinId<T>(this MultivectorStorage<T> mv)
        {
            return mv.GetLinVectorIdScalarStorage().GetMinIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxId<T>(this MultivectorStorage<T> mv)
        {
            return mv.GetLinVectorIdScalarStorage().GetMaxIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMinIndex<T>(this KVectorStorage<T> mv)
        {
            return mv.GetLinVectorIndexScalarStorage().GetMinIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxIndex<T>(this KVectorStorage<T> mv)
        {
            return mv.GetLinVectorIndexScalarStorage().GetMaxIndex();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords<T>(this IMultivectorStorage<T> mv, Func<T, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords<T>(this IMultivectorStorage<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords<T>(this IMultivectorStorage<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Index, record.Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords<T>(this KVectorStorage<T> mv, Func<T, bool> filterFunc)
        {
            return mv
                .GetIdScalarRecords()
                .Where(record => filterFunc(record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords<T>(this KVectorStorage<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv.GetLinVectorIndexScalarStorage()
                .GetIndexScalarRecords()
                .Where(record => filterFunc(record.Index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords<T>(this KVectorStorage<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv.GetLinVectorIndexScalarStorage()
                .GetIndexScalarRecords()
                .Where(record => filterFunc(record.Index, record.Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotZeroIdScalarRecords<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.GetIdScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotZeroIdScalarRecords<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetIdScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar))
                : mv.GetIdScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotNearZeroIdScalarRecords<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.GetIdScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNearZeroIdScalarRecords<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.GetIdScalarRecords(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotZeroIndexScalarRecords<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            return mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotZeroIndexScalarRecords<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar))
                : mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNotNearZeroIndexScalarRecords<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            return mv.GetIndexScalarRecords(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetNearZeroIndexScalarRecords<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            return mv.GetIndexScalarRecords(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetTerms<T>(this IMultivectorStorage<T> mv, Func<T, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetTerms<T>(this IMultivectorStorage<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetTerms<T>(this IMultivectorStorage<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv.GetTerms(filterFunc);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetNotZeroTerms<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.GetTerms(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetNotZeroTerms<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? mv.GetTerms(scalar => !scalarProcessor.IsNearZero(scalar)) 
                : mv.GetTerms(scalar => !scalarProcessor.IsZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetNotNearZeroTerms<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.GetTerms(scalar => !scalarProcessor.IsNearZero(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisTerm<T>> GetNearZeroTerms<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.GetTerms(scalarProcessor.IsNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.TryGetScalar(out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, ulong id)
        {
            return mv.TryGetTermScalar(id, out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, uint grade, ulong index)
        {
            return mv.TryGetTermScalar(grade, index, out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalarByIndex<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, int index)
        {
            return mv.TryGetTermScalarByIndex((ulong) index, out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetTermScalarByIndex<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, ulong index)
        {
            return mv.TryGetTermScalarByIndex(index, out var scalar)
                ? scalar
                : scalarProcessor.ScalarZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> GetTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, ulong id)
        {
            return id.CreateBasisTerm(
                scalarProcessor.GetTermScalar(mv, id)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> GetTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, uint grade, ulong index)
        {
            return grade.CreateBasisTerm(
                index,
                scalarProcessor.GetTermScalar(mv, grade, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> GetTermByIndex<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, int index)
        {
            return mv.Grade.CreateBasisTerm(
                index,
                scalarProcessor.GetTermScalarByIndex(mv, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> GetTermByIndex<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, ulong index)
        {
            return mv.Grade.CreateBasisTerm(
                index,
                scalarProcessor.GetTermScalarByIndex(mv, index)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds(this IEnumerable<BasisBlade> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> GetGrades(this IEnumerable<BasisBlade> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Grade).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this IEnumerable<BasisBlade> basisBladesList)
        {
            return basisBladesList.Select(basisBlade => basisBlade.Index);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIds<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> GetGrades<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Grade).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BasisBlade> GetBasisBlades<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.BasisBlade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalars<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => term.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new IndexScalarRecord<T>(term.BasisBlade.Id, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIdScalarTuples<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new IndexScalarRecord<T>(term.BasisBlade.Id, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new IndexScalarRecord<T>(term.BasisBlade.Index, term.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarTuples<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList.Select(term => 
                new IndexScalarRecord<T>(term.BasisBlade.Index, term.Scalar)
            );
        }

        
        public static IEnumerable<IndexScalarRecord<T>> OrderByGradeIndex<T>(this IEnumerable<IndexScalarRecord<T>> idScalarRecords)
        {
            var termsArray = 
                idScalarRecords.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => t.Index)
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Index.BasisBladeIdToGrade())
                .ThenByDescending(t => t.Index.ReverseBits(bitsCount));
        }
        
        public static IEnumerable<IndexScalarRecord<T>> OrderById<T>(this IEnumerable<IndexScalarRecord<T>> idScalarRecords)
        {
            return idScalarRecords
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Index);
        }
        
        public static IEnumerable<GradeIndexScalarRecord<T>> OrderByGradeIndex<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            var termsArray = gradeIndexScalarTuples.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => 
                    t.Index.BasisBladeIndexToId(t.Grade)
                )
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.Grade)
                .ThenByDescending(t => 
                    t.Index.BasisBladeIndexToId(t.Grade).ReverseBits(bitsCount)
                );
        }
        
        public static IEnumerable<GradeIndexScalarRecord<T>> OrderById<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            return gradeIndexScalarTuples
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => 
                    t.Index.BasisBladeIndexToId(t.Grade)
                );
        }

        public static IEnumerable<BasisTerm<T>> OrderByGradeIndex<T>(this IEnumerable<BasisTerm<T>> termsList)
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
        
        public static IEnumerable<BasisTerm<T>> OrderById<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return termsList
                //.Where(t => !t.Scalar.IsZero())
                .OrderBy(t => t.BasisBlade.Id);
        }


        public static ILinVectorDenseStorage<T> VectorToArrayVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> vectorStorage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension];

            for (var index = 0; index < vSpaceDimension; index++)
                array[index] = scalarProcessor.ScalarZero;

            foreach (var (index, scalar) in vectorStorage.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()) 
                array[index] = scalar;

            return array.CreateLinVectorDenseStorage();
        }

        public static ILinVectorDenseStorage<T> BivectorToArrayVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> bivectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) vSpaceDimension.KVectorSpaceDimension(2
            );

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ScalarZero;

            foreach (var (index, scalar) in bivectorStorage.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()) 
                array[index] = scalar;

            return array.CreateLinVectorDenseStorage();
        }

        public static ILinMatrixDenseStorage<T> BivectorToArray<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> bivectorStorage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension, vSpaceDimension];

            for (var i = 0; i < vSpaceDimension; i++)
            for (var j = 0; j < vSpaceDimension; j++)
                array[i, j] = scalarProcessor.ScalarZero;

            foreach (var (index1, index2, scalar) in bivectorStorage.GetBasisVectorsIndexScalarRecords())
            {
                array[index1, index2] = scalar;
                array[index2, index1] = scalarProcessor.Negative(scalar);
            }

            return array.CreateLinMatrixDenseStorage();
        }

        public static ILinMatrixDenseStorage<T> ScalarPlusBivectorToArray<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> storage, uint vSpaceDimension)
        {
            var array = new T[vSpaceDimension, vSpaceDimension];

            var scalar = scalarProcessor.GetTermScalar(storage, 0);

            for (var i = 0; i < vSpaceDimension; i++)
            {
                array[i, i] = scalar;

                for (var j = i + 1; j < vSpaceDimension; j++)
                {
                    array[i, j] = scalarProcessor.ScalarZero;
                    array[j, i] = scalarProcessor.ScalarZero;
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

            return array.CreateLinMatrixDenseStorage();
        }

        public static ILinVectorDenseStorage<T> KVectorToArrayVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> kVectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) vSpaceDimension.KVectorSpaceDimension(kVectorStorage.Grade
            );

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ScalarZero;

            foreach (var (index, scalar) in kVectorStorage.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()) 
                array[index] = scalar;

            return array.CreateLinVectorDenseStorage();
        }

        public static ILinVectorDenseStorage<T> MultivectorToArrayVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> multivectorStorage, uint vSpaceDimension)
        {
            var arrayLength = (int) vSpaceDimension.ToGaSpaceDimension();

            var array = new T[arrayLength];

            for (var index = 0; index < arrayLength; index++)
                array[index] = scalarProcessor.ScalarZero;

            foreach (var (index, scalar) in multivectorStorage.GetIdScalarRecords()) 
                array[index] = scalar;

            return array.CreateLinVectorDenseStorage();
        }
    }
}