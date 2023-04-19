using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Stacks;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.GuidedBinaryTraversal
{
    public sealed class GaGbtMultivectorProductsBinaryTrieStack
    {
        //public static GaGbtProductsStack2 Create(XGaMultivector mv1, XGaMultivector mv2)
        //{
        //    var basisSet = mv1.BasisSet;

        //    var treeDepth = (int) basisSet.VSpaceDimensions;

        //    var capacity = (treeDepth + 1) * (treeDepth + 1);
            
        //    var stack1 = mv1.CreateGbtStack(capacity);
        //    var stack2 = mv2.CreateGbtStack(capacity);

        //    return new GaGbtProductsStack2(stack1, stack2);
        //}

        /// <summary>
        /// Array containing node tree depth information in this stack
        /// </summary>
        private int[] TreeDepthArray { get; }

        /// <summary>
        /// Maximum number of nodes that can be stored in this stack
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// Number of nodes currently stored in this stack
        /// </summary>
        public int Count
            => TosIndex + 1;

        /// <summary>
        /// True if this stack is empty
        /// </summary>
        public bool IsEmpty
            => TosIndex < 0;

        /// <summary>
        /// Top-of-stack node is a leaf node
        /// </summary>
        public bool TosIsLeaf
            => TosTreeDepth == 0;

        /// <summary>
        /// Top-of-stack node is a leaf parent internal node
        /// </summary>
        public bool TosIsLeafParent
            => TosTreeDepth == 1;

        /// <summary>
        /// Top-of-stack node is an internal node
        /// </summary>
        public bool TosIsInternal
            => TosTreeDepth > 0;

        /// <summary>
        /// Top-of-stack node index
        /// </summary>
        public int TosIndex { get; private set; }

        /// <summary>
        /// Top-of-stack node tree depth
        /// </summary>
        public int TosTreeDepth { get; private set; }

        public int RootTreeDepth { get; }

        public ulong TosId1
            => MultivectorStack1.TosId;

        public ulong TosId2
            => MultivectorStack2.TosId;

        public ulong TosChildId10
            => MultivectorStack1.TosChildId0;

        public ulong TosChildId11
            => MultivectorStack1.TosChildId1;

        public ulong TosChildId20
            => MultivectorStack2.TosChildId0;

        public ulong TosChildId21
            => MultivectorStack2.TosChildId1;

        public ulong RootId1 
            => MultivectorStack1.RootId;

        public ulong RootId2 
            => MultivectorStack2.RootId;

        public GaGbtMultivectorBinaryTrieStack MultivectorStack1 { get; }

        public GaGbtMultivectorBinaryTrieStack MultivectorStack2 { get; }

        public RGaFloat64Processor BasisSet 
            => MultivectorStack1.BasisSet;
        
        public double TosValue1 
            => MultivectorStack1.TosScalar;

        public double TosValue2 
            => MultivectorStack2.TosScalar;

        public bool TosIsNonZeroOp
            => TosId1.OpIsNonZero(TosId2);

        public bool TosIsNonZeroESp
            => TosId1.ESpIsNonZero(TosId2);

        public bool TosIsNonZeroELcp
            => TosId1.ELcpIsNonZero(TosId2);

        public bool TosIsNonZeroERcp
            => TosId1.ERcpIsNonZero(TosId2);

        public bool TosIsNonZeroEFdp
            => TosId1.EFdpIsNonZero(TosId2);

        public bool TosIsNonZeroEHip
            => TosId1.EHipIsNonZero(TosId2);

        public bool TosIsNonZeroEAcp
            => TosId1.EAcpIsNonZero(TosId2);

        public bool TosIsNonZeroECp
            => TosId1.ECpIsNonZero(TosId2);

        public ulong TosChildIdXor00
            => MultivectorStack1.TosChildId0 ^ MultivectorStack2.TosChildId0;

        public ulong TosChildIdXor10
            => MultivectorStack1.TosChildId1 ^ MultivectorStack2.TosChildId0;

        public ulong TosChildIdXor01
            => MultivectorStack1.TosChildId0 ^ MultivectorStack2.TosChildId1;

        public ulong TosChildIdXor11
            => MultivectorStack1.TosChildId1 ^ MultivectorStack2.TosChildId1;

        public int TosChildIdXorGrade00
            => TosChildIdXor00.Grade();

        public int TosChildIdXorGrade10
            => TosChildIdXor10.Grade();

        public int TosChildIdXorGrade01
            => TosChildIdXor01.Grade();

        public int TosChildIdXorGrade11
            => TosChildIdXor11.Grade();

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaGbtMultivectorProductsBinaryTrieStack(GaGbtMultivectorBinaryTrieStack stack1, GaGbtMultivectorBinaryTrieStack stack2) 
        {
            Debug.Assert(stack1.RootTreeDepth > 0);

            Debug.Assert(
                stack1.Capacity == stack2.Capacity &&
                stack1.RootTreeDepth == stack2.RootTreeDepth
            );

            Capacity = stack1.Capacity;

            TreeDepthArray = new int[Capacity];

            TosIndex = -1;
            RootTreeDepth = stack1.RootTreeDepth;
            
            MultivectorStack1 = stack1;
            MultivectorStack2 = stack2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushRootData()
        {
            TosIndex = 0;

            TreeDepthArray[TosIndex] = RootTreeDepth;

            MultivectorStack1.PushRootData();
            MultivectorStack2.PushRootData();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PopNodeData()
        {
            MultivectorStack1.PopNodeData();
            MultivectorStack2.PopNodeData();

            TosTreeDepth = TreeDepthArray[TosIndex];

            //Console.Out.WriteLine($"depth:{TosTreeDepth}, id1: {TosId1}, id2: {TosId2}");

            TosIndex--;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TosHasChild10()
        {
            return MultivectorStack1.TosHasChild(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TosHasChild11()
        {
            return MultivectorStack1.TosHasChild(1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TosHasChild20()
        {
            return MultivectorStack2.TosHasChild(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TosHasChild21()
        {
            return MultivectorStack2.TosHasChild(1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int TosHasChildPattern()
        {
            var hasChild10 = MultivectorStack1.TosHasChild(0);
            var hasChild11 = MultivectorStack1.TosHasChild(1);

            var hasChild20 = MultivectorStack2.TosHasChild(0);
            var hasChild21 = MultivectorStack2.TosHasChild(1);

            var pattern = 0;
            if (hasChild10)
            {
                if (hasChild20) pattern |= 1;
                if (hasChild21) pattern |= 2;
            }

            if (hasChild11)
            {
                if (hasChild20) pattern |= 4;
                if (hasChild21) pattern |= 8;
            }

            return pattern;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int TosHasChildPattern(int selectionMask)
        {
            var hasChild10 = MultivectorStack1.TosHasChild(0);
            var hasChild11 = MultivectorStack1.TosHasChild(1);

            var hasChild20 = MultivectorStack2.TosHasChild(0);
            var hasChild21 = MultivectorStack2.TosHasChild(1);

            var pattern = 0;
            if (hasChild10)
            {
                if (hasChild20) pattern |= 1;
                if (hasChild21) pattern |= 2;
            }

            if (hasChild11)
            {
                if (hasChild20) pattern |= 4;
                if (hasChild21) pattern |= 8;
            }

            return pattern & selectionMask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushDataOfChildren()
        {
            var selectionPattern = 
                TosHasChildPattern();

            if ((selectionPattern & 1) != 0) 
                PushDataOfChild(0);

            if ((selectionPattern & 2) != 0) 
                PushDataOfChild(1);

            if ((selectionPattern & 4) != 0) 
                PushDataOfChild(2);

            if ((selectionPattern & 8) != 0) 
                PushDataOfChild(3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushDataOfChildren(int selectionMask)
        {
            var selectionPattern = 
                TosHasChildPattern(selectionMask);

            if ((selectionPattern & 1) != 0) 
                PushDataOfChild(0);

            if ((selectionPattern & 2) != 0) 
                PushDataOfChild(1);

            if ((selectionPattern & 4) != 0) 
                PushDataOfChild(2);

            if ((selectionPattern & 8) != 0) 
                PushDataOfChild(3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushDataOfChild(int childIndex)
        {
            TosIndex++;
            TreeDepthArray[TosIndex] = TosTreeDepth - 1;

            MultivectorStack1.PushDataOfChild(childIndex & 1);
            MultivectorStack2.PushDataOfChild((childIndex >> 1) & 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TosChildMayContainGrade1(int childGrade)
        {
            return
                (TosTreeDepth > 1 && childGrade <= 1) ||
                (TosTreeDepth == 1 && childGrade == 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TosChildMayContainGrade(int childGrade, uint grade)
        {
            return
                (TosTreeDepth > 1 && childGrade <= grade) ||
                (TosTreeDepth == 1 && childGrade == grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGaIdScalarRecord TosGetEGpIdScalarPair()
        {
            var id1 = TosId1;
            var id2 = TosId2;

            var basisBladeSignature = 
                BasisBladeDataLookup.EGpSign(id1, id2);

            var id = id1 ^ id2;
            var scalar = basisBladeSignature * TosValue1 * TosValue2;

            //Console.Out.WriteLine($"id1: {id1}, id2: {id2}, value: {value}");

            return new RGaIdScalarRecord(id, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGaIdScalarRecord TosGetGpIdScalarPair(int basisBladeSignature)
        {
            Debug.Assert(basisBladeSignature is 1 or -1);

            var id1 = TosId1;
            var id2 = TosId2;

            basisBladeSignature *= 
                BasisBladeDataLookup.EGpSign(id1, id2);

            var id = id1 ^ id2;
            var scalar = basisBladeSignature * TosValue1 * TosValue2;

            return new RGaIdScalarRecord(id, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TosGetEGpScalar()
        {
            var basisBladeSignature = 
                BasisBladeDataLookup.EGpSign(TosId1, TosId2);

            return basisBladeSignature * TosValue1 * TosValue2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TosGetGpScalar(int basisBladeSignature)
        {
            Debug.Assert(basisBladeSignature is 1 or -1);

            basisBladeSignature *= 
                BasisBladeDataLookup.EGpSign(TosId1, TosId2);

            return basisBladeSignature * TosValue1 * TosValue2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetOpIdScalarRecords()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetEGpIdScalarRecords()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetESpIdScalarRecords()
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> GetESpScalars()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetELcpIdScalarRecords()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetERcpIdScalarRecords()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetEFdpIdScalarRecords()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetEHipIdScalarRecords()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetEAcpIdScalarRecords()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetECpIdScalarRecords()
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetGpIdScalarRecords()
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
                            BasisSet.Signature(TosTreeDepth - 1);

                        if (basisVectorSignature.IsNotZero)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetSpIdScalarRecords()
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
                            BasisSet.Signature(TosTreeDepth - 1);

                        if (basisVectorSignature.IsNotZero)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> GetSpScalars()
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
                            BasisSet.Signature(TosTreeDepth - 1);

                        if (basisVectorSignature.IsNotZero)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetLcpIdScalarRecords()
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
                            BasisSet.Signature(TosTreeDepth - 1);

                        if (basisVectorSignature.IsNotZero)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetRcpIdScalarRecords()
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
                            BasisSet.Signature(TosTreeDepth - 1);

                        if (basisVectorSignature.IsNotZero)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetFdpIdScalarRecords()
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
                            BasisSet.Signature(TosTreeDepth - 1);

                        if (basisVectorSignature.IsNotZero)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetHipIdScalarRecords()
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
                            BasisSet.Signature(TosTreeDepth - 1);

                        if (basisVectorSignature.IsNotZero)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetAcpIdScalarRecords()
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
                            BasisSet.Signature(TosTreeDepth - 1);

                        if (basisVectorSignature.IsNotZero)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdScalarRecord> GetCpIdScalarRecords()
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
                            BasisSet.Signature(TosTreeDepth - 1);

                        if (basisVectorSignature.IsNotZero)
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