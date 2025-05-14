using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;



//TODO: This class is not working. WHY?
public sealed class XGaGbtMultivectorStorageGradedStack1<T>
    : XGaGbtStack1, IXGaGbtMultivectorStorageStack1<T>
{
    public static XGaGbtMultivectorStorageGradedStack1<T> Create(int capacity, int treeDepth, IScalarProcessor<T> scalarProcessor, XGaMultivector<T> multivectorStorage)
    {
        return new(capacity, treeDepth, scalarProcessor, multivectorStorage);
    }


    private IndexSet[] ActiveGradesBitMask0Array { get; }

    private IndexSet[] ActiveGradesBitMask1Array { get; }

    public IScalarProcessor<T> ScalarProcessor { get; }

    public XGaMultivector<T> Multivector { get; }

    public T TosScalar { get; private set; }

    public IndexSet ActiveGradesBitPattern { get; }

    public IndexSet TosActiveGradesBitMask0 { get; private set; }

    public IndexSet TosActiveGradesBitMask1 { get; private set; }

    public IndexSet TosChildActiveGradesBitPattern0
        => ActiveGradesBitPattern &
           TosActiveGradesBitMask0 >> 1 &
           TosActiveGradesBitMask1;

    public IndexSet TosChildActiveGradesBitPattern1
        => ActiveGradesBitPattern &
           TosActiveGradesBitMask0 &
           TosActiveGradesBitMask1 << 1;

    public IndexSet RootActiveGradesBitMask0 { get; }

    public IndexSet RootActiveGradesBitMask1 { get; }


    private XGaGbtMultivectorStorageGradedStack1(int capacity, int treeDepth, IScalarProcessor<T> scalarProcessor, XGaMultivector<T> multivectorStorage)
        : base(capacity, treeDepth, IndexSet.EmptySet)
    {
        ScalarProcessor = scalarProcessor;
        Multivector = multivectorStorage;
        ActiveGradesBitPattern = multivectorStorage.GetStoredGradesBitPattern();

        ActiveGradesBitMask0Array = new IndexSet[capacity];
        ActiveGradesBitMask1Array = new IndexSet[capacity];

        RootActiveGradesBitMask0 = 
            RootActiveGradesBitMask1 = 
                IndexSet.CreateDense(multivectorStorage.VSpaceDimensions + 2);
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
            return !TosChildActiveGradesBitPattern0.IsEmptySet && (
                TosTreeDepth > 1 || Multivector.ContainsKey(TosChildId0)
            );

        return !TosChildActiveGradesBitPattern1.IsEmptySet && (
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