using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;



//TODO: This class is not working. WHY?
public sealed class RGaGbtMultivectorStorageGradedStack1<T>
    : RGaGbtStack1, IRGaGbtMultivectorStorageStack1<T>
{
    public static RGaGbtMultivectorStorageGradedStack1<T> Create(int capacity, int treeDepth, IScalarProcessor<T> scalarProcessor, RGaMultivector<T> multivectorStorage)
    {
        return new(capacity, treeDepth, scalarProcessor, multivectorStorage);
    }


    private ulong[] ActiveGradesBitMask0Array { get; }

    private ulong[] ActiveGradesBitMask1Array { get; }

    public IScalarProcessor<T> ScalarProcessor { get; }

    public RGaMultivector<T> Multivector { get; }

    public T TosScalar { get; private set; }

    public ulong ActiveGradesBitPattern { get; }

    public ulong TosActiveGradesBitMask0 { get; private set; }

    public ulong TosActiveGradesBitMask1 { get; private set; }

    public ulong TosChildActiveGradesBitPattern0
        => ActiveGradesBitPattern &
           (TosActiveGradesBitMask0 >> 1) &
           TosActiveGradesBitMask1;

    public ulong TosChildActiveGradesBitPattern1
        => ActiveGradesBitPattern &
           TosActiveGradesBitMask0 &
           (TosActiveGradesBitMask1 << 1);

    public ulong RootActiveGradesBitMask0 { get; }

    public ulong RootActiveGradesBitMask1 { get; }


    private RGaGbtMultivectorStorageGradedStack1(int capacity, int treeDepth, IScalarProcessor<T> scalarProcessor, RGaMultivector<T> multivectorStorage)
        : base(capacity, treeDepth, 0ul)
    {
        ScalarProcessor = scalarProcessor;
        Multivector = multivectorStorage;
        ActiveGradesBitPattern = multivectorStorage.GetStoredGradesBitPattern();

        ActiveGradesBitMask0Array = new ulong[capacity];
        ActiveGradesBitMask1Array = new ulong[capacity];

        RootActiveGradesBitMask0 = 
            RootActiveGradesBitMask1 = 
                (1ul << multivectorStorage.VSpaceDimensions + 2) - 1;
    }
        

    public override void PushRootData()
    {
        TosIndex = 0;

        TreeDepthArray[TosIndex] = RootTreeDepth;
        IdArray[TosIndex] = RootId;
        ActiveGradesBitMask0Array[TosIndex] = RootActiveGradesBitMask0;
        ActiveGradesBitMask1Array[TosIndex] = RootActiveGradesBitMask1;
    }

    public override void PopNodeData()
    {
        TosTreeDepth = TreeDepthArray[TosIndex];
        TosId = IdArray[TosIndex];

        if (TosTreeDepth > 0)
        {
            TosActiveGradesBitMask0 = ActiveGradesBitMask0Array[TosIndex];
            TosActiveGradesBitMask1 = ActiveGradesBitMask1Array[TosIndex];
        }
        else
        {
            TosScalar = Multivector.GetBasisBladeScalar(TosId).ScalarValue;
        }

        TosIndex--;
    }

    public override bool TosHasChild(int childIndex)
    {
        if ((childIndex & 1) == 0)
            return TosChildActiveGradesBitPattern0 != 0 && (
                TosTreeDepth > 1 || Multivector.ContainsKey(TosChildId0)
            );

        return TosChildActiveGradesBitPattern1 != 0 && (
            TosTreeDepth > 1 || Multivector.ContainsKey(TosChildId1)
        );
    }

    public override void PushDataOfChild(int childIndex)
    {
        TosIndex++;
        TreeDepthArray[TosIndex] = TosTreeDepth - 1;

        if ((childIndex & 1) == 0)
        {
            IdArray[TosIndex] = TosChildId0;
            ActiveGradesBitMask0Array[TosIndex] = TosActiveGradesBitMask0 >> 1;
            ActiveGradesBitMask1Array[TosIndex] = TosActiveGradesBitMask1;
        }
        else
        {
            IdArray[TosIndex] = TosChildId1;
            ActiveGradesBitMask0Array[TosIndex] = TosActiveGradesBitMask0;
            ActiveGradesBitMask1Array[TosIndex] = TosActiveGradesBitMask1 << 1;
        }
    }
}