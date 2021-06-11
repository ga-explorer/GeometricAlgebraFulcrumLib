namespace GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Multivectors
{
    //public sealed class GaGbtMvsTermStack1<T>
    //    : GaGbtStack1, IGaGbtMultivectorStorageStack1<T>
    //{
    //    public static GaGbtMvsTermStack1<T> Create(int capacity, GaTerm<T> termStorage)
    //    {
    //        return new GaGbtMvsTermStack1<T>(capacity, termStorage);
    //    }


    //    public GaTerm<T> TermStorage { get; }

    //    public IGaMultivectorStorage<T> Storage 
    //        => TermStorage;

    //    public T TosValue { get; private set; }


    //    private GaGbtMvsTermStack1(int capacity, GaTerm<T> termStorage)
    //        : base(capacity, termStorage.VSpaceDimension, 0ul)
    //    {
    //        TermStorage = termStorage;
    //    }


    //    public override void PushRootData()
    //    {
    //        TosIndex = 0;

    //        TreeDepthArray[TosIndex] = RootTreeDepth;
    //        IdArray[TosIndex] = RootId;
    //    }

    //    public override void PopNodeData()
    //    {
    //        TosTreeDepth = TreeDepthArray[TosIndex];
    //        TosId = IdArray[TosIndex];

    //        if (TosTreeDepth == 0)
    //        {
    //            TosValue = TermStorage.GetTermScalar(TosId);
    //        }

    //        TosIndex--;
    //    }

    //    public override bool TosHasChild(int childIndex)
    //    {
    //        var v = 
    //            (1ul << (TosTreeDepth - 1)) & TermStorage.BasisBlade.Id;

    //        return (childIndex & 1) == 0
    //            ? v == 0
    //            : v != 0;
    //    }

    //    public override void PushDataOfChild(int childIndex)
    //    {
    //        TosIndex++;
    //        TreeDepthArray[TosIndex] = TosTreeDepth - 1;

    //        if ((childIndex & 1) == 0)
    //            IdArray[TosIndex] = TosChildId0;
    //        else
    //            IdArray[TosIndex] = TosChildId1;
    //    }
    //}
}