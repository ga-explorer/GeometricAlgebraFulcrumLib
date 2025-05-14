using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal.Outermorphisms;



public sealed class XGaGbtOutermorphismStack<T>
    : XGaGbtStack1
{
    public static XGaGbtOutermorphismStack<T> Create(IScalarProcessor<T> scalarProcessor, IReadOnlyList<XGaVector<T>> basisVectorsMappingsList)
    {
        var domainVSpaceDim = basisVectorsMappingsList.Count;
        var targetVSpaceDim = basisVectorsMappingsList[0].VSpaceDimensions;
        var capacity = domainVSpaceDim + 1;

        return new XGaGbtOutermorphismStack<T>(
            capacity,
            domainVSpaceDim,
            targetVSpaceDim,
            scalarProcessor,
            basisVectorsMappingsList
        );
    }


    private XGaKVector<T>[] KVectorArray { get; }


    public int DomainVSpaceDimension { get; }

    public int TargetVSpaceDimension { get; }

    public ulong DomainGaSpaceDimension 
        => 1UL << DomainVSpaceDimension;

    public ulong TargetGaSpaceDimension 
        => 1UL << TargetVSpaceDimension;

    public IReadOnlyList<XGaVector<T>> BasisVectorsMappingsList { get; }

    public IScalarProcessor<T> ScalarProcessor { get; }

    public XGaKVector<T> TosKVector { get; private set; }

    public XGaKVector<T> RootKVector { get; }


    private XGaGbtOutermorphismStack(int capacity, int domainVSpaceDim, int targetVSpaceDim, IScalarProcessor<T> scalarProcessor, IReadOnlyList<XGaVector<T>> basisVectorsMappingsList)
        : base(capacity, (int) Math.Max(1U, domainVSpaceDim), IndexSet.EmptySet)
    {
        ScalarProcessor = scalarProcessor;
        KVectorArray = new XGaKVector<T>[Capacity];

        DomainVSpaceDimension = domainVSpaceDim;
        TargetVSpaceDimension = targetVSpaceDim;
        BasisVectorsMappingsList = basisVectorsMappingsList;

        RootKVector = basisVectorsMappingsList[0].Processor.ScalarOne;
    }


    public XGaKVector<T> GetTosChildKVector0()
    {
        return TosKVector;
    }

    public XGaKVector<T> GetTosChildKVector1()
    {
        var basisVector = BasisVectorsMappingsList[TosTreeDepth - 1];

        return TosKVector.Grade == 0
            ? basisVector
            : basisVector.Op(TosKVector);
    }


    //public override bool TosHasChild0()
    //{
    //    return true;
    //}

    //public override bool TosHasChild1()
    //{
    //    return true;
    //}


    public override void PushRootData()
    {
        TosIndex = 0;

        TreeDepthArray[TosIndex] = RootTreeDepth;
        IdArray[TosIndex] = RootId;
        KVectorArray[TosIndex] = RootKVector;
    }

    public override void PopNodeData()
    {
        TosTreeDepth = TreeDepthArray[TosIndex];
        TosId = IdArray[TosIndex];
        TosKVector = KVectorArray[TosIndex];

        TosIndex--;
    }

    public override bool TosHasChild(int childIndex)
    {
        return true;
    }

    public override void PushDataOfChild(int childIndex)
    {
        TosIndex++;
        TreeDepthArray[TosIndex] = TosTreeDepth - 1;

        if ((childIndex & 1) == 0)
        {
            IdArray[TosIndex] = TosChildId0;
            KVectorArray[TosIndex] = GetTosChildKVector0();
        }
        else
        {
            IdArray[TosIndex] = TosChildId1;
            KVectorArray[TosIndex] = GetTosChildKVector1();
        }
    }

    //public override void PushDataOfChild0()
    //{
    //    TosIndex++;

    //    TreeDepthArray[TosIndex] = TosTreeDepth - 1;
    //    IdArray[TosIndex] = TosChildId0;
    //    KVectorArray[TosIndex] = GetTosChildKVector0();
    //}

    //public override void PushDataOfChild1()
    //{
    //    TosIndex++;

    //    TreeDepthArray[TosIndex] = TosTreeDepth - 1;
    //    IdArray[TosIndex] = TosChildId1;
    //    KVectorArray[TosIndex] = GetTosChildKVector1();
    //}

    public IEnumerable<Tuple<ulong, XGaKVector<T>>> Traverse()
    {
        //GeoNumVectorKVectorOpUtils.SetActiveVSpaceDimension(TargetVSpaceDimension);

        PushRootData();

        while (!IsEmpty)
        {
            PopNodeData();

            if (TosIsLeaf)
            {
                yield return new Tuple<ulong, XGaKVector<T>>((ulong)TosId, TosKVector);

                continue;
            }

            PushDataOfChild(1);

            PushDataOfChild(0);
        }
    }
}