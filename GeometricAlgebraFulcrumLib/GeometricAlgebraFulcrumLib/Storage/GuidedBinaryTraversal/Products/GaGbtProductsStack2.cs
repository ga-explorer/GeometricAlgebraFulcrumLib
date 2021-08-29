using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Stacks;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Iterators;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Products
{
    public sealed class GaGbtProductsStack2<T>
        : GaGbtStack2, IGaProductTermsIterator<T>
    {
        public static GaGbtProductsStack2<T> Create(GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            var treeDepth = 
                (int) Math.Max(1, Math.Max(mv1.MultivectorStorage.MinVSpaceDimension, mv2.MultivectorStorage.MinVSpaceDimension));

            var capacity = (treeDepth + 1) * (treeDepth + 1);
            
            var stack1 = mv1.MultivectorStorage.CreateGbtStack(treeDepth, capacity, processor);
            var stack2 = mv2.MultivectorStorage.CreateGbtStack(treeDepth, capacity, processor);

            return new GaGbtProductsStack2<T>(stack1, stack2);
        }

        public static GaGbtProductsStack2<T> Create(IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var treeDepth = 
                (int) Math.Max(1, Math.Max(mv1.MinVSpaceDimension, mv2.MinVSpaceDimension));

            var capacity = (treeDepth + 1) * (treeDepth + 1);
            
            var stack1 = mv1.CreateGbtStack(treeDepth, capacity, scalarProcessor);
            var stack2 = mv2.CreateGbtStack(treeDepth, capacity, scalarProcessor);

            return new GaGbtProductsStack2<T>(stack1, stack2);
        }


        private IGaGbtMultivectorStorageStack1<T> MultivectorStack1 { get; }

        private IGaGbtMultivectorStorageStack1<T> MultivectorStack2 { get; }


        public IScalarProcessor<T> ScalarProcessor 
            => MultivectorStack1.ScalarProcessor;

        public IGaMultivectorStorage<T> Storage1 
            => MultivectorStack1.Storage;

        public IGaMultivectorStorage<T> Storage2 
            => MultivectorStack2.Storage;

        public T TosValue1 
            => MultivectorStack1.TosScalar;

        public T TosValue2 
            => MultivectorStack2.TosScalar;

        public bool TosIsNonZeroOp
            => GaBasisBladeProductUtils.IsNonZeroOp(TosId1, TosId2);

        public bool TosIsNonZeroESp
            => GaBasisBladeProductUtils.IsNonZeroESp(TosId1, TosId2);

        public bool TosIsNonZeroELcp
            => GaBasisBladeProductUtils.IsNonZeroELcp(TosId1, TosId2);

        public bool TosIsNonZeroERcp
            => GaBasisBladeProductUtils.IsNonZeroERcp(TosId1, TosId2);

        public bool TosIsNonZeroEFdp
            => GaBasisBladeProductUtils.IsNonZeroEFdp(TosId1, TosId2);

        public bool TosIsNonZeroEHip
            => GaBasisBladeProductUtils.IsNonZeroEHip(TosId1, TosId2);

        public bool TosIsNonZeroEAcp
            => GaBasisBladeProductUtils.IsNonZeroEAcp(TosId1, TosId2);

        public bool TosIsNonZeroECp
            => GaBasisBladeProductUtils.IsNonZeroECp(TosId1, TosId2);

        public ulong TosChildIdXor00
            => Stack1.TosChildId0 ^ Stack2.TosChildId0;

        public ulong TosChildIdXor10
            => Stack1.TosChildId1 ^ Stack2.TosChildId0;

        public ulong TosChildIdXor01
            => Stack1.TosChildId0 ^ Stack2.TosChildId1;

        public ulong TosChildIdXor11
            => Stack1.TosChildId1 ^ Stack2.TosChildId1;

        public int TosChildIdXorGrade00
            => TosChildIdXor00.CountOnes();

        public int TosChildIdXorGrade10
            => TosChildIdXor10.CountOnes();

        public int TosChildIdXorGrade01
            => TosChildIdXor01.CountOnes();

        public int TosChildIdXorGrade11
            => TosChildIdXor11.CountOnes();


        private GaGbtProductsStack2(IGaGbtMultivectorStorageStack1<T> stack1, IGaGbtMultivectorStorageStack1<T> stack2) 
            : base(stack1, stack2)
        {
            MultivectorStack1 = stack1;
            MultivectorStack2 = stack2;
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


        private IndexScalarRecord<T> TosGetEGpIdScalarPair()
        {
            var id1 = TosId1;
            var id2 = TosId2;

            var id = id1 ^ id2;
            var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
                : ScalarProcessor.Times(TosValue1, TosValue2);

            //Console.Out.WriteLine($"id1: {id1}, id2: {id2}, value: {value}");

            return new IndexScalarRecord<T>(id, scalar);
        }

        private IndexScalarRecord<T> TosGetGpIdScalarPair(int basisBladeSignature)
        {
            Debug.Assert(basisBladeSignature == 1 || basisBladeSignature == -1);

            var id1 = TosId1;
            var id2 = TosId2;

            var id = id1 ^ id2;
            var scalar = GaBasisBladeProductUtils.IsNegativeGp(basisBladeSignature, id1, id2)
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
                : ScalarProcessor.Times(TosValue1, TosValue2);

            return new IndexScalarRecord<T>(id, scalar);
        }

        private T TosGetEGpScalar()
        {
            var scalar = GaBasisBladeProductUtils.IsNegativeEGp(TosId1, TosId2)
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
                : ScalarProcessor.Times(TosValue1, TosValue2);

            return scalar;
        }

        private T TosGetGpScalar(int basisBladeSignature)
        {
            Debug.Assert(basisBladeSignature == 1 || basisBladeSignature == -1);

            var id1 = TosId1;
            var id2 = TosId2;

            var scalar = GaBasisBladeProductUtils.IsNegativeGp(basisBladeSignature, id1, id2)
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
                : ScalarProcessor.Times(TosValue1, TosValue2);

            return scalar;
        }


        public IEnumerable<IndexScalarRecord<T>> GetOpIdScalarRecords()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetEGpIdScalarPair();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetEGpIdScalarRecords()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetEGpIdScalarPair();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetESpIdScalarRecords()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetEGpIdScalarPair();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                }

                if (hasChild11)
                {
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }
        
        public IEnumerable<T> GetESpScalars()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetEGpScalar();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                }

                if (hasChild11)
                {
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetELcpIdScalarRecords()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetEGpIdScalarPair();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetERcpIdScalarRecords()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetEGpIdScalarPair();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetEFdpIdScalarRecords()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEFdp)
                        yield return TosGetEGpIdScalarPair();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetEHipIdScalarRecords()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEHip)
                        yield return TosGetEGpIdScalarPair();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetEAcpIdScalarRecords()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEAcp)
                        yield return TosGetEGpIdScalarPair();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetECpIdScalarRecords()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroECp)
                        yield return TosGetEGpIdScalarPair();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }


        public IEnumerable<IndexScalarRecord<T>> GetGpIdScalarRecords(IGaSignature metric)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetGpIdScalarPair(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature = 
                            metric.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetSpIdScalarRecords(IGaSignature metric)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetGpIdScalarPair(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metric.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<T> GetSpScalars(IGaSignature metric)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetGpScalar(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metric.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetLcpIdScalarRecords(IGaSignature metric)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetGpIdScalarPair(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metric.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetRcpIdScalarRecords(IGaSignature metric)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetGpIdScalarPair(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metric.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetFdpIdScalarRecords(IGaSignature metric)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEFdp)
                        yield return TosGetGpIdScalarPair(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metric.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetHipIdScalarRecords(IGaSignature metric)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEHip)
                        yield return TosGetGpIdScalarPair(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metric.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetAcpIdScalarRecords(IGaSignature metric)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEAcp)
                        yield return TosGetGpIdScalarPair(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metric.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetCpIdScalarRecords(IGaSignature metric)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroECp)
                        yield return TosGetGpIdScalarPair(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metric.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }
    }
}