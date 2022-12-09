using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Stacks;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.ProductIterators;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Products
{
    public sealed class GeoGbtProductsStack2<T> : 
        GeoGbtStack2, 
        IMultivectorStorageTermsIterator<T>
    {
        public static GeoGbtProductsStack2<T> Create(GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            var treeDepth = 
                (int) Math.Max(1, Math.Max(mv1.MultivectorStorage.MinVSpaceDimension, mv2.MultivectorStorage.MinVSpaceDimension));

            var capacity = (treeDepth + 1) * (treeDepth + 1);
            
            var stack1 = mv1.MultivectorStorage.CreateGbtStack(treeDepth, capacity, processor);
            var stack2 = mv2.MultivectorStorage.CreateGbtStack(treeDepth, capacity, processor);

            return new GeoGbtProductsStack2<T>(stack1, stack2);
        }

        public static GeoGbtProductsStack2<T> Create(IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var treeDepth = 
                (int) Math.Max(1, Math.Max(mv1.MinVSpaceDimension, mv2.MinVSpaceDimension));

            var capacity = (treeDepth + 1) * (treeDepth + 1);
            
            var stack1 = mv1.CreateGbtStack(treeDepth, capacity, scalarProcessor);
            var stack2 = mv2.CreateGbtStack(treeDepth, capacity, scalarProcessor);

            return new GeoGbtProductsStack2<T>(stack1, stack2);
        }


        private IGeoGbtMultivectorStorageStack1<T> MultivectorStack1 { get; }

        private IGeoGbtMultivectorStorageStack1<T> MultivectorStack2 { get; }


        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => MultivectorStack1.ScalarProcessor;

        public IMultivectorStorage<T> Storage1 
            => MultivectorStack1.Storage;

        public IMultivectorStorage<T> Storage2 
            => MultivectorStack2.Storage;

        public T TosValue1 
            => MultivectorStack1.TosScalar;

        public T TosValue2 
            => MultivectorStack2.TosScalar;

        public bool TosIsNonZeroOp
            => BasisBladeProductUtils.IsNonZeroOp(TosId1, TosId2);

        public bool TosIsNonZeroESp
            => BasisBladeProductUtils.IsNonZeroESp(TosId1, TosId2);

        public bool TosIsNonZeroELcp
            => BasisBladeProductUtils.IsNonZeroELcp(TosId1, TosId2);

        public bool TosIsNonZeroERcp
            => BasisBladeProductUtils.IsNonZeroERcp(TosId1, TosId2);

        public bool TosIsNonZeroEFdp
            => BasisBladeProductUtils.IsNonZeroEFdp(TosId1, TosId2);

        public bool TosIsNonZeroEHip
            => BasisBladeProductUtils.IsNonZeroEHip(TosId1, TosId2);

        public bool TosIsNonZeroEAcp
            => BasisBladeProductUtils.IsNonZeroEAcp(TosId1, TosId2);

        public bool TosIsNonZeroECp
            => BasisBladeProductUtils.IsNonZeroECp(TosId1, TosId2);

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


        private GeoGbtProductsStack2(IGeoGbtMultivectorStorageStack1<T> stack1, IGeoGbtMultivectorStorageStack1<T> stack2) 
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
            var scalar = BasisBladeProductUtils.EGpIsNegative(id1, id2)
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
                : ScalarProcessor.Times(TosValue1, TosValue2);

            //Console.Out.WriteLine($"id1: {id1}, id2: {id2}, value: {value}");

            return new IndexScalarRecord<T>(id, scalar);
        }

        private IndexScalarRecord<T> TosGetGpIdScalarPair(int gpSquaredSign)
        {
            Debug.Assert(gpSquaredSign is 1 or -1);

            var id1 = TosId1;
            var id2 = TosId2;

            var id = id1 ^ id2;
            var scalar = BasisBladeProductUtils.EGpSign(id1, id2) * gpSquaredSign == -1
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
                : ScalarProcessor.Times(TosValue1, TosValue2);

            return new IndexScalarRecord<T>(id, scalar);
        }

        private T TosGetEGpScalar()
        {
            var scalar = BasisBladeProductUtils.EGpIsNegative(TosId1, TosId2)
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
                : ScalarProcessor.Times(TosValue1, TosValue2);

            return scalar;
        }

        private T TosGetGpScalar(int gpSquaredSign)
        {
            Debug.Assert(gpSquaredSign is 1 or -1);

            var id1 = TosId1;
            var id2 = TosId2;

            var scalar = BasisBladeProductUtils.EGpSign(id1, id2) * gpSquaredSign == -1
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


        public IEnumerable<IndexScalarRecord<T>> GetGpIdScalarRecords(BasisBladeSet basisSet)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var gpSquaredSign = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetGpIdScalarPair(gpSquaredSign);

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
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(gpSquaredSign);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature = 
                            basisSet.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(gpSquaredSign * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetSpIdScalarRecords(BasisBladeSet basisSet)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var gpSquaredSign = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetGpIdScalarPair(gpSquaredSign);

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
                        metricStack.Push(gpSquaredSign);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            basisSet.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(gpSquaredSign * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<T> GetSpScalars(BasisBladeSet basisSet)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var gpSquaredSign = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetGpScalar(gpSquaredSign);

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
                        metricStack.Push(gpSquaredSign);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            basisSet.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(gpSquaredSign * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetLcpIdScalarRecords(BasisBladeSet basisSet)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var gpSquaredSign = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetGpIdScalarPair(gpSquaredSign);

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
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(gpSquaredSign);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            basisSet.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(gpSquaredSign * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetRcpIdScalarRecords(BasisBladeSet basisSet)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var gpSquaredSign = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetGpIdScalarPair(gpSquaredSign);

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
                        metricStack.Push(gpSquaredSign);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            basisSet.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(gpSquaredSign * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetFdpIdScalarRecords(BasisBladeSet basisSet)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var gpSquaredSign = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEFdp)
                        yield return TosGetGpIdScalarPair(gpSquaredSign);

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
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(gpSquaredSign);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            basisSet.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(gpSquaredSign * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetHipIdScalarRecords(BasisBladeSet basisSet)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var gpSquaredSign = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEHip)
                        yield return TosGetGpIdScalarPair(gpSquaredSign);

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
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(gpSquaredSign);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            basisSet.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(gpSquaredSign * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetAcpIdScalarRecords(BasisBladeSet basisSet)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var gpSquaredSign = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEAcp)
                        yield return TosGetGpIdScalarPair(gpSquaredSign);

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
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(gpSquaredSign);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            basisSet.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(gpSquaredSign * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetCpIdScalarRecords(BasisBladeSet basisSet)
        {
            PushRootData();

            var metricStack = new FixedStack<int>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var gpSquaredSign = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroECp)
                        yield return TosGetGpIdScalarPair(gpSquaredSign);

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
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(gpSquaredSign);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(gpSquaredSign);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            basisSet.GetBasisVectorSignature(TosTreeDepth - 1);

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(gpSquaredSign * basisVectorSignature);
                        }
                    }
                }
            }
        }
    }
}