﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Stacks;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Geometry.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Products.Iterators;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;

namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Products
{
    public sealed class GaGbtProductsStack2<T>
        : GaGbtStack2, IGaProductTermsIterator<T>
    {
        public static GaGbtProductsStack2<T> Create(GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            //TODO: Generalize to case where one is larger than the other
            //Debug.Assert(mv1.Storage.VSpaceDimension == mv2.Storage.VSpaceDimension);

            var treeDepth = 
                (int) Math.Max(1, Math.Max(mv1.Storage.VSpaceDimension, mv2.Storage.VSpaceDimension));

            var capacity = (treeDepth + 1) * (treeDepth + 1);
            
            var stack1 = mv1.Storage.CreateGbtStack(treeDepth, capacity);
            var stack2 = mv2.Storage.CreateGbtStack(treeDepth, capacity);

            return new GaGbtProductsStack2<T>(stack1, stack2);
        }

        public static GaGbtProductsStack2<T> Create(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            //TODO: Generalize to case where one is larger than the other
            //Debug.Assert(mv1.VSpaceDimension == mv2.VSpaceDimension);

            var treeDepth = 
                (int) Math.Max(1, Math.Max(mv1.VSpaceDimension, mv2.VSpaceDimension));

            var capacity = (treeDepth + 1) * (treeDepth + 1);
            
            var stack1 = mv1.CreateGbtStack(treeDepth, capacity);
            var stack2 = mv2.CreateGbtStack(treeDepth, capacity);

            return new GaGbtProductsStack2<T>(stack1, stack2);
        }


        private IGaGbtMultivectorStorageStack1<T> MultivectorStack1 { get; }

        private IGaGbtMultivectorStorageStack1<T> MultivectorStack2 { get; }


        public IGaScalarProcessor<T> ScalarProcessor 
            => Storage1.ScalarProcessor;

        public IGasMultivector<T> Storage1 
            => MultivectorStack1.Storage;

        public IGasMultivector<T> Storage2 
            => MultivectorStack2.Storage;

        public T TosValue1 
            => MultivectorStack1.TosScalar;

        public T TosValue2 
            => MultivectorStack2.TosScalar;

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


        private KeyValuePair<ulong, T> TosGetEGpIdScalarPair()
        {
            var id1 = TosId1;
            var id2 = TosId2;

            var id = id1 ^ id2;
            var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
                : ScalarProcessor.Times(TosValue1, TosValue2);

            //Console.Out.WriteLine($"id1: {id1}, id2: {id2}, value: {value}");

            return new KeyValuePair<ulong, T>(id, scalar);
        }

        private KeyValuePair<ulong, T> TosGetGpIdScalarPair(int basisBladeSignature)
        {
            Debug.Assert(basisBladeSignature == 1 || basisBladeSignature == -1);

            var id1 = TosId1;
            var id2 = TosId2;

            var id = id1 ^ id2;
            var scalar = GaBasisUtils.IsNegativeGp(basisBladeSignature, id1, id2)
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
                : ScalarProcessor.Times(TosValue1, TosValue2);

            return new KeyValuePair<ulong, T>(id, scalar);
        }

        private T TosGetEGpScalar()
        {
            var scalar = GaBasisUtils.IsNegativeEGp(TosId1, TosId2)
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
                : ScalarProcessor.Times(TosValue1, TosValue2);

            return scalar;
        }

        private T TosGetGpScalar(int basisBladeSignature)
        {
            Debug.Assert(basisBladeSignature == 1 || basisBladeSignature == -1);

            var id1 = TosId1;
            var id2 = TosId2;

            var scalar = GaBasisUtils.IsNegativeGp(basisBladeSignature, id1, id2)
                ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
                : ScalarProcessor.Times(TosValue1, TosValue2);

            return scalar;
        }


        public IEnumerable<KeyValuePair<ulong, T>> GetOpIdScalarPairs()
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

        public IEnumerable<KeyValuePair<ulong, T>> GetEGpIdScalarPairs()
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

        public IEnumerable<KeyValuePair<ulong, T>> GetESpIdScalarPairs()
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

        public IEnumerable<KeyValuePair<ulong, T>> GetELcpIdScalarPairs()
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

        public IEnumerable<KeyValuePair<ulong, T>> GetERcpIdScalarPairs()
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

        public IEnumerable<KeyValuePair<ulong, T>> GetEFdpIdScalarPairs()
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

        public IEnumerable<KeyValuePair<ulong, T>> GetEHipIdScalarPairs()
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

        public IEnumerable<KeyValuePair<ulong, T>> GetEAcpIdScalarPairs()
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

        public IEnumerable<KeyValuePair<ulong, T>> GetECpIdScalarPairs()
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


        public IEnumerable<KeyValuePair<ulong, T>> GetGpIdScalarPairs(GaSignature metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorSignatures;
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
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetSpIdScalarPairs(GaSignature metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorSignatures;
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
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<T> GetSpScalars(GaSignature metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorSignatures;
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
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetLcpIdScalarPairs(GaSignature metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorSignatures;
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
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetRcpIdScalarPairs(GaSignature metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorSignatures;
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
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetFdpIdScalarPairs(GaSignature metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorSignatures;
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
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetHipIdScalarPairs(GaSignature metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorSignatures;
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
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetAcpIdScalarPairs(GaSignature metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorSignatures;
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
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetCpIdScalarPairs(GaSignature metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorSignatures;
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
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

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