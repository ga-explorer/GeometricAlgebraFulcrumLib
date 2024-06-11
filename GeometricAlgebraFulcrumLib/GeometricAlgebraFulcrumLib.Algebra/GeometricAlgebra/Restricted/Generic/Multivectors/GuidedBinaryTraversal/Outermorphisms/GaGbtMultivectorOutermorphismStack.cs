using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal.Outermorphisms;



public record RGaScalarKVectorRecord<T>(T Scalar, RGaKVector<T> KVector);

public sealed class RGaGbtMultivectorOutermorphismStack<T>
    : RGaGbtStack1
{
    public static RGaGbtMultivectorOutermorphismStack<T> Create(IReadOnlyList<RGaVector<T>> basisVectorsMappingsList, RGaMultivector<T> mv)
    {
        var treeDepth = Math.Max(1, mv.VSpaceDimensions);
        var capacity = treeDepth + 1;

        return new RGaGbtMultivectorOutermorphismStack<T>(
            basisVectorsMappingsList,
            mv.CreateGbtStack(treeDepth, capacity)
        );
    }

    public static RGaGbtMultivectorOutermorphismStack<T> Create(IReadOnlyList<RGaVector<T>> basisVectorsMappingsList, IScalarProcessor<T> scalarProcessor, RGaMultivector<T> mv)
    {
        var treeDepth = Math.Max(1, mv.VSpaceDimensions);
        var capacity = treeDepth + 1;

        return new RGaGbtMultivectorOutermorphismStack<T>(
            basisVectorsMappingsList,
            mv.CreateGbtStack(treeDepth, capacity)
        );
    }


    private RGaKVector<T>[] KVectorArray { get; }

    private IRGaGbtMultivectorStorageStack1<T> MultivectorStack { get; }

    public IScalarProcessor<T> ScalarProcessor 
        => MultivectorStack.ScalarProcessor;

    public IReadOnlyList<RGaVector<T>> BasisVectorsMappingsList { get; }

    public RGaMultivector<T> Storage 
        => MultivectorStack.Multivector;

    public RGaKVector<T> TosKVector { get; private set; }

    public T TosValue 
        => MultivectorStack.TosScalar;

    public RGaKVector<T> RootKVector { get; }


    private RGaGbtMultivectorOutermorphismStack(IReadOnlyList<RGaVector<T>> basisVectorsMappingsList, IRGaGbtMultivectorStorageStack1<T> multivectorStack)
        : base(multivectorStack.Capacity, multivectorStack.RootTreeDepth, multivectorStack.RootId)
    {
        KVectorArray = new RGaKVector<T>[Capacity];

        BasisVectorsMappingsList = basisVectorsMappingsList;
        MultivectorStack = multivectorStack;

        RootKVector = basisVectorsMappingsList[0].Processor.ScalarOne;
    }


    public RGaKVector<T> GetTosChildKVector0()
    {
        return TosKVector;
    }

    public RGaKVector<T> GetTosChildKVector1()
    {
        var basisVector = BasisVectorsMappingsList[TosTreeDepth - 1];

        var storage = TosKVector.Grade == 0
            ? basisVector
            : basisVector.Op(TosKVector);

        return storage;
    }



    //public override bool TosHasChild0()
    //{
    //    return MultivectorStack.TosHasChild0();
    //}

    //public override bool TosHasChild1()
    //{
    //    return MultivectorStack.TosHasChild1();
    //}


    public override void PushRootData()
    {
        TosIndex = 0;

        TreeDepthArray[TosIndex] = RootTreeDepth;
        IdArray[TosIndex] = RootId;
        KVectorArray[TosIndex] = RootKVector;
            
        MultivectorStack.PushRootData();
    }

    public override void PopNodeData()
    {
        MultivectorStack.PopNodeData();

        TosTreeDepth = TreeDepthArray[TosIndex];
        TosId = IdArray[TosIndex];
        TosKVector = KVectorArray[TosIndex];

        TosIndex--;
    }

    public override bool TosHasChild(int childIndex)
    {
        return MultivectorStack.TosHasChild(childIndex);
    }

    public override void PushDataOfChild(int childIndex)
    {
        if ((childIndex & 1) == 0)
        {
            MultivectorStack.PushDataOfChild(childIndex);
            TosIndex++;
            TreeDepthArray[TosIndex] = TosTreeDepth - 1;

            IdArray[TosIndex] = TosChildId0;
            KVectorArray[TosIndex] = GetTosChildKVector0();
        }
        else
        {
            var storage = GetTosChildKVector1();

            //Avoid pushing a child when the mapped basis blade is zero
            if (storage.IsZero)
                return;

            MultivectorStack.PushDataOfChild(childIndex);
            TosIndex++;
            TreeDepthArray[TosIndex] = TosTreeDepth - 1;

            IdArray[TosIndex] = TosChildId1;
            KVectorArray[TosIndex] = storage;
        }
    }

    //public override void PushDataOfChild0()
    //{
    //    MultivectorStack.PushDataOfChild0();

    //    TosIndex++;

    //    TreeDepthArray[TosIndex] = TosTreeDepth - 1;
    //    IdArray[TosIndex] = TosChildId0;
    //    KVectorArray[TosIndex] = GetTosChildKVector0();
    //}

    //public override void PushDataOfChild1()
    //{
    //    MultivectorStack.PushDataOfChild1();

    //    TosIndex++;

    //    TreeDepthArray[TosIndex] = TosTreeDepth - 1;
    //    IdArray[TosIndex] = TosChildId1;
    //    KVectorArray[TosIndex] = GetTosChildKVector1();
    //}

    public IEnumerable<RGaScalarKVectorRecord<T>> TraverseForScaledKVectors()
    {
        //GeoNumVectorKVectorOpUtils.SetActiveVSpaceDimension(Multivector.VSpaceDimensions);

        PushRootData();

        //var maxStackSizeCounter = 0;

        while (!IsEmpty)
        {
            //maxStackSizeCounter = Math.Max(maxStackSizeCounter, nodesStack.Count);

            PopNodeData();

            if (TosIsLeaf)
            {
                if (!ScalarProcessor.IsZero(TosValue))
                    yield return new RGaScalarKVectorRecord<T>(
                        TosValue,
                        TosKVector
                    );

                continue;
            }

            if (TosHasChild(1))
                PushDataOfChild(1);

            if (TosHasChild(0))
                PushDataOfChild(0);

            //var stackSize = opStack.SizeInBytes();
            //if (sizeCounter < stackSize) sizeCounter = stackSize;
        }

        //Console.WriteLine("Max Stack Size: " + sizeCounter.ToString("###,###,###,###,###,##0"));
        //Console.WriteLine(@"Max Stack Size: " + maxStackSizeCounter.ToString("###,###,###,###,###,##0"));        }
    }

    public IEnumerable<RGaKvIndexScalarRecord<RGaKVector<T>>> TraverseForIdKVectors()
    {
        //GeoNumVectorKVectorOpUtils.SetActiveVSpaceDimension(Multivector.VSpaceDimensions);

        PushRootData();

        while (!IsEmpty)
        {
            PopNodeData();

            if (TosIsLeaf)
            {
                yield return new RGaKvIndexScalarRecord<RGaKVector<T>>(TosId, TosKVector);

                continue;
            }

            if (TosHasChild(1))
                PushDataOfChild(1);

            if (TosHasChild(0))
                PushDataOfChild(0);
        }
    }
}