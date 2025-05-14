using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.ProductIterators;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Stacks;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal.Products;



public sealed class XGaGbtProductsStack2<T> : 
    XGaGbtStack2, 
    IXGaMultivectorTermsIterator<T>
{
    public static XGaGbtProductsStack2<T> Create(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
    {
        var treeDepth = 
            Math.Max(1, Math.Max(mv1.VSpaceDimensions, mv2.VSpaceDimensions));

        var capacity = (treeDepth + 1) * (treeDepth + 1);
            
        var stack1 = mv1.CreateGbtStack(treeDepth, capacity);
        var stack2 = mv2.CreateGbtStack(treeDepth, capacity);

        return new XGaGbtProductsStack2<T>(stack1, stack2);
    }
        

    private IXGaGbtMultivectorStorageStack1<T> MultivectorStack1 { get; }

    private IXGaGbtMultivectorStorageStack1<T> MultivectorStack2 { get; }


    public XGaProcessor<T> Processor 
        => MultivectorStack1.Multivector.Processor;

    public IScalarProcessor<T> ScalarProcessor 
        => MultivectorStack1.ScalarProcessor;

    public XGaMultivector<T> Multivector1 
        => MultivectorStack1.Multivector;

    public XGaMultivector<T> Multivector2 
        => MultivectorStack2.Multivector;

    public T TosValue1 
        => MultivectorStack1.TosScalar;

    public T TosValue2 
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

    public IndexSet TosChildIdXor00
        => Stack1.TosChildId0 ^ Stack2.TosChildId0;

    public IndexSet TosChildIdXor10
        => Stack1.TosChildId1 ^ Stack2.TosChildId0;

    public IndexSet TosChildIdXor01
        => Stack1.TosChildId0 ^ Stack2.TosChildId1;

    public IndexSet TosChildIdXor11
        => Stack1.TosChildId1 ^ Stack2.TosChildId1;

    public int TosChildIdXorGrade00
        => TosChildIdXor00.Grade();

    public int TosChildIdXorGrade10
        => TosChildIdXor10.Grade();

    public int TosChildIdXorGrade01
        => TosChildIdXor01.Grade();

    public int TosChildIdXorGrade11
        => TosChildIdXor11.Grade();


    private XGaGbtProductsStack2(IXGaGbtMultivectorStorageStack1<T> stack1, IXGaGbtMultivectorStorageStack1<T> stack2) 
        : base(stack1, stack2)
    {
        MultivectorStack1 = stack1;
        MultivectorStack2 = stack2;
    }

        
    public bool TosChildMayContainGrade1(int childGrade)
    {
        return
            TosTreeDepth > 1 && childGrade <= 1 ||
            TosTreeDepth == 1 && childGrade == 1;
    }

    public bool TosChildMayContainGrade(int childGrade, int grade)
    {
        return
            TosTreeDepth > 1 && childGrade <= grade ||
            TosTreeDepth == 1 && childGrade == grade;
    }


    private KeyValuePair<IndexSet, T> TosGetEGpIdScalarPair()
    {
        var id1 = TosId1;
        var id2 = TosId2;

        var id = id1 ^ id2;
        var scalar = id1.EGpIsNegative(id2)
            ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
            : ScalarProcessor.Times(TosValue1, TosValue2);

        //Console.Out.WriteLine($"id1: {id1}, id2: {id2}, value: {value}");

        return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
    }

    private KeyValuePair<IndexSet, T> TosGetGpIdScalarPair(int gpSquaredSign)
    {
        Debug.Assert(gpSquaredSign is 1 or -1);

        var id1 = TosId1;
        var id2 = TosId2;

        var id = id1 ^ id2;
        var scalar = id1.EGpSign(id2) * gpSquaredSign == -1
            ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
            : ScalarProcessor.Times(TosValue1, TosValue2);

        return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
    }

    private T TosGetEGpScalar()
    {
        var scalar = TosId1.EGpIsNegative(TosId2)
            ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
            : ScalarProcessor.Times(TosValue1, TosValue2);

        return scalar.ScalarValue;
    }

    private T TosGetGpScalar(int gpSquaredSign)
    {
        Debug.Assert(gpSquaredSign is 1 or -1);

        var id1 = TosId1;
        var id2 = TosId2;

        var scalar = id1.EGpSign(id2) * gpSquaredSign == -1
            ? ScalarProcessor.NegativeTimes(TosValue1, TosValue2)
            : ScalarProcessor.Times(TosValue1, TosValue2);

        return scalar.ScalarValue;
    }


    public IEnumerable<KeyValuePair<IndexSet, T>> GetOpIdScalarRecords()
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

    public IEnumerable<KeyValuePair<IndexSet, T>> GetEGpIdScalarRecords()
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

    public IEnumerable<KeyValuePair<IndexSet, T>> GetESpIdScalarRecords()
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

    public IEnumerable<KeyValuePair<IndexSet, T>> GetELcpIdScalarRecords()
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

    public IEnumerable<KeyValuePair<IndexSet, T>> GetERcpIdScalarRecords()
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

    public IEnumerable<KeyValuePair<IndexSet, T>> GetEFdpIdScalarRecords()
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

    public IEnumerable<KeyValuePair<IndexSet, T>> GetEHipIdScalarRecords()
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

    public IEnumerable<KeyValuePair<IndexSet, T>> GetEAcpIdScalarRecords()
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

    public IEnumerable<KeyValuePair<IndexSet, T>> GetECpIdScalarRecords()
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


    public IEnumerable<KeyValuePair<IndexSet, T>> GetGpIdScalarRecords()
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
                        Processor.Signature(TosTreeDepth - 1);

                    if (basisVectorSignature.IsNotZero)
                    {
                        PushDataOfChild(3);
                        metricStack.Push(gpSquaredSign * basisVectorSignature);
                    }
                }
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetSpIdScalarRecords()
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
                        Processor.Signature(TosTreeDepth - 1);

                    if (basisVectorSignature.IsNotZero)
                    {
                        PushDataOfChild(3);
                        metricStack.Push(gpSquaredSign * basisVectorSignature);
                    }
                }
            }
        }
    }

    public IEnumerable<T> GetSpScalars()
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
                        Processor.Signature(TosTreeDepth - 1);

                    if (basisVectorSignature.IsNotZero)
                    {
                        PushDataOfChild(3);
                        metricStack.Push(gpSquaredSign * basisVectorSignature);
                    }
                }
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetLcpIdScalarRecords()
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
                        Processor.Signature(TosTreeDepth - 1);

                    if (basisVectorSignature.IsNotZero)
                    {
                        PushDataOfChild(3);
                        metricStack.Push(gpSquaredSign * basisVectorSignature);
                    }
                }
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetRcpIdScalarRecords()
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
                        Processor.Signature(TosTreeDepth - 1);

                    if (basisVectorSignature.IsNotZero)
                    {
                        PushDataOfChild(3);
                        metricStack.Push(gpSquaredSign * basisVectorSignature);
                    }
                }
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetFdpIdScalarRecords()
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
                        Processor.Signature(TosTreeDepth - 1);

                    if (basisVectorSignature.IsNotZero)
                    {
                        PushDataOfChild(3);
                        metricStack.Push(gpSquaredSign * basisVectorSignature);
                    }
                }
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetHipIdScalarRecords()
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
                        Processor.Signature(TosTreeDepth - 1);

                    if (basisVectorSignature.IsNotZero)
                    {
                        PushDataOfChild(3);
                        metricStack.Push(gpSquaredSign * basisVectorSignature);
                    }
                }
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetAcpIdScalarRecords()
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
                        Processor.Signature(TosTreeDepth - 1);

                    if (basisVectorSignature.IsNotZero)
                    {
                        PushDataOfChild(3);
                        metricStack.Push(gpSquaredSign * basisVectorSignature);
                    }
                }
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetCpIdScalarRecords()
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
                        Processor.Signature(TosTreeDepth - 1);

                    if (basisVectorSignature.IsNotZero)
                    {
                        PushDataOfChild(3);
                        metricStack.Push(gpSquaredSign * basisVectorSignature);
                    }
                }
            }
        }
    }
}