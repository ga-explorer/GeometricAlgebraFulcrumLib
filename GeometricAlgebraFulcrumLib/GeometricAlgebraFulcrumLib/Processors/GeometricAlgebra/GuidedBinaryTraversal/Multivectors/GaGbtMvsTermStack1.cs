namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Multivectors
{
    //public sealed class GeoGbtMvsTermStack1<T>
    //    : GeoGbtStack1, IGeoGbtMultivectorStorageStack1<T>
    //{
    //    public static GeoGbtMvsTermStack1<T> Create(int capacity, GeoTerm<T> termStorage)
    //    {
    //        return new GeoGbtMvsTermStack1<T>(capacity, termStorage);
    //    }


    //    public GeoTerm<T> TermStorage { get; }

    //    public IMultivectorStorage<T> Storage 
    //        => TermStorage;

    //    public T TosValue { get; private set; }


    //    private GeoGbtMvsTermStack1(int capacity, GeoTerm<T> termStorage)
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