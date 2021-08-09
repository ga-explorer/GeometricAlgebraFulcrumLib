using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Terms;

namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Products
{
    public sealed class GaGbtProductsStack3<T> 
        : GaGbtStack3
    {
        public static GaGbtProductsStack3<T> Create(GaMultivector<T> mv1, GaMultivector<T> mv2, GaMultivector<T> mv3)
        {
            Debug.Assert(
                mv1.MultivectorStorage.VSpaceDimension == mv2.MultivectorStorage.VSpaceDimension &&
                mv2.MultivectorStorage.VSpaceDimension == mv3.MultivectorStorage.VSpaceDimension
            );

            var processor = mv1.Processor;

            var treeDepth = 
                (int) Math.Max(
                    1, 
                    Math.Max(
                        mv1.MultivectorStorage.VSpaceDimension,
                        Math.Max(
                            mv2.MultivectorStorage.VSpaceDimension, 
                            mv3.MultivectorStorage.VSpaceDimension
                        )
                    )
                );

            var capacity = (treeDepth + 1) * (treeDepth + 1) * (treeDepth + 1);
            
            var stack1 = mv1.MultivectorStorage.CreateGbtStack(treeDepth, capacity, processor);
            var stack2 = mv2.MultivectorStorage.CreateGbtStack(treeDepth, capacity, processor);
            var stack3 = mv3.MultivectorStorage.CreateGbtStack(treeDepth, capacity, processor);

            return new GaGbtProductsStack3<T>(stack1, stack2, stack3);
        }


        private IGaGbtMultivectorStorageStack1<T> MultivectorStack1 { get; }

        private IGaGbtMultivectorStorageStack1<T> MultivectorStack2 { get; }

        private IGaGbtMultivectorStorageStack1<T> MultivectorStack3 { get; }


        public IGaScalarProcessor<T> ScalarProcessor 
            => MultivectorStack1.ScalarProcessor;

        public IGaStorageMultivector<T> Storage1 
            => MultivectorStack1.Storage;

        public IGaStorageMultivector<T> Storage2 
            => MultivectorStack2.Storage;

        public IGaStorageMultivector<T> Storage3 
            => MultivectorStack3.Storage;

        public T TosValue1 
            => MultivectorStack1.TosScalar;

        public T TosValue2 
            => MultivectorStack2.TosScalar;

        public T TosValue3 
            => MultivectorStack3.TosScalar;

        public bool TosIsNonZeroOp
            => GaBasisUtils.IsNonZeroOp(TosId1, TosId2);

        public bool TosIsNonZeroESp
            => GaBasisUtils.IsNonZeroESp(TosId1, TosId2);

        public bool TosIsNonZeroELcp
            => GaBasisUtils.IsNonZeroELcp(TosId1, TosId2);

        public bool TosIsNonZeroERcp
            => GaBasisUtils.IsNonZeroERcp(TosId1, TosId2);

        public bool TosIsNonZeroEFdp
            => GaBasisUtils.IsNonZeroEFdp(TosId1, TosId2);

        public bool TosIsNonZeroEHip
            => GaBasisUtils.IsNonZeroEHip(TosId1, TosId2);

        public bool TosIsNonZeroEAcp
            => GaBasisUtils.IsNonZeroEAcp(TosId1, TosId2);

        public bool TosIsNonZeroECp
            => GaBasisUtils.IsNonZeroECp(TosId1, TosId2);

        public ulong TosChildIdXor000
            => Stack1.TosChildId0 ^ Stack2.TosChildId0 ^ Stack3.TosChildId0;

        public ulong TosChildIdXor100
            => Stack1.TosChildId1 ^ Stack2.TosChildId0 ^ Stack3.TosChildId0;

        public ulong TosChildIdXor010
            => Stack1.TosChildId0 ^ Stack2.TosChildId1 ^ Stack3.TosChildId0;

        public ulong TosChildIdXor110
            => Stack1.TosChildId1 ^ Stack2.TosChildId1 ^ Stack3.TosChildId0;

        public ulong TosChildIdXor001
            => Stack1.TosChildId0 ^ Stack2.TosChildId0 ^ Stack3.TosChildId1;

        public ulong TosChildIdXor101
            => Stack1.TosChildId1 ^ Stack2.TosChildId0 ^ Stack3.TosChildId1;

        public ulong TosChildIdXor011
            => Stack1.TosChildId0 ^ Stack2.TosChildId1 ^ Stack3.TosChildId1;

        public ulong TosChildIdXor111
            => Stack1.TosChildId1 ^ Stack2.TosChildId1 ^ Stack3.TosChildId1;

        public int TosChildIdXorGrade000
            => TosChildIdXor000.CountOnes();

        public int TosChildIdXorGrade100
            => TosChildIdXor100.CountOnes();

        public int TosChildIdXorGrade010
            => TosChildIdXor010.CountOnes();

        public int TosChildIdXorGrade110
            => TosChildIdXor110.CountOnes();

        public int TosChildIdXorGrade001
            => TosChildIdXor001.CountOnes();

        public int TosChildIdXorGrade101
            => TosChildIdXor101.CountOnes();

        public int TosChildIdXorGrade011
            => TosChildIdXor011.CountOnes();

        public int TosChildIdXorGrade111
            => TosChildIdXor111.CountOnes();


        private GaGbtProductsStack3(IGaGbtMultivectorStorageStack1<T> stack1, IGaGbtMultivectorStorageStack1<T> stack2, IGaGbtMultivectorStorageStack1<T> stack3) 
            : base(stack1, stack2, stack3)
        {
            MultivectorStack1 = stack1;
            MultivectorStack2 = stack2;
            MultivectorStack3 = stack3;
        }

        
        public bool TosChildMayContainGrade1(int childGrade)
        {
            return
                (TosTreeDepth > 1 && childGrade <= 1) ||
                (TosTreeDepth == 1 && childGrade == 1);
        }

        public bool TosChildMayContainGrade(int childGrade, uint grade)
        {
            return
                (TosTreeDepth > 1 && childGrade <= grade) ||
                (TosTreeDepth == 1 && childGrade == grade);
        }


        public GaTerm<T> TosGetTermsEGp()
        {
            var id1 = TosId1;
            var id2 = TosId2;
            var id3 = TosId3;

            var value = GaBasisUtils.IsNegativeEGp(id1, id2)
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2, TosValue3)
                : ScalarProcessor.Times(TosValue1, TosValue2, TosValue3);

            //Console.Out.WriteLine($"id: ({id1}, {id2}, {id3}), value: {value}");

            return GaTerm<T>.CreateUniform(id1 ^ id2 ^ id3, value);
        }

        //public GaTerm<T> TosGetTermsGp(IGaNumMetricOrthogonal metric)
        //{
        //    var id1 = (int)TosId1;
        //    var id2 = (int)TosId2;

        //    var term = metric.Gp(id1, id2);
        //    term.ScalarValue *= TosValue1 * TosValue2;

        //    return term;
        //}

        public GaTerm<T> TosGetTermsGp(T basisBladeSignature)
        {
            var id1 = TosId1;
            var id2 = TosId2;
            var id3 = TosId3;

            var value = GaBasisUtils.IsNegativeEGp(id1, id2)
                ? ScalarProcessor.NegativeTimes(basisBladeSignature, TosValue1, TosValue2, TosValue3)
                : ScalarProcessor.Times(basisBladeSignature, TosValue1, TosValue2, TosValue3);

            return GaTerm<T>.CreateUniform(id1 ^ id2 ^ id3, value);
        }


        public IEnumerable<GaTerm<T>> TraverseForEGpGpTerms()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetTermsEGp();

                    continue;
                }

                var hasChild10 = Stack1.TosHasChild(0);
                var hasChild11 = Stack1.TosHasChild(1);

                var hasChild20 = Stack2.TosHasChild(0);
                var hasChild21 = Stack2.TosHasChild(1);

                var hasChild30 = Stack3.TosHasChild(0);
                var hasChild31 = Stack3.TosHasChild(1);

                if (hasChild31)
                {
                    if (hasChild21)
                    {
                        if (hasChild11) PushDataOfChild(7); //111
                        if (hasChild10) PushDataOfChild(6); //110
                    }

                    if (hasChild20)
                    {
                        if (hasChild11) PushDataOfChild(5); //101
                        if (hasChild10) PushDataOfChild(4); //100
                    }
                }

                if (hasChild30)
                {
                    if (hasChild21)
                    {
                        if (hasChild11) PushDataOfChild(3); //011
                        if (hasChild10) PushDataOfChild(2); //010
                    }

                    if (hasChild20)
                    {
                        if (hasChild11) PushDataOfChild(1); //001
                        if (hasChild10) PushDataOfChild(0); //000
                    }
                }
            }
        }
        
        /// <summary>
        /// (X op Y) lcp Z
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GaTerm<T>> TraverseForEOpLcpLaTerms()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetTermsEGp();

                    continue;
                }

                var hasChild10 = Stack1.TosHasChild(0);
                var hasChild11 = Stack1.TosHasChild(1);

                var hasChild20 = Stack2.TosHasChild(0);
                var hasChild21 = Stack2.TosHasChild(1);

                var hasChild30 = Stack3.TosHasChild(0);
                var hasChild31 = Stack3.TosHasChild(1);

                if (hasChild31)
                {
                    if (hasChild21)
                    {
                        //if (hasChild11) PushDataOfChild(7); //111
                        if (hasChild10) PushDataOfChild(6); //110
                    }

                    if (hasChild20)
                    {
                        if (hasChild11) PushDataOfChild(5); //101
                        if (hasChild10) PushDataOfChild(4); //100
                    }
                }

                if (hasChild30)
                {
                    //if (hasChild21)
                    //{
                    //    if (hasChild11) PushDataOfChild(3); //011
                    //    if (hasChild10) PushDataOfChild(2); //010
                    //}

                    if (hasChild20)
                    {
                        //if (hasChild11) PushDataOfChild(1); //001
                        if (hasChild10) PushDataOfChild(0); //000
                    }
                }
            }
        }
    }
}