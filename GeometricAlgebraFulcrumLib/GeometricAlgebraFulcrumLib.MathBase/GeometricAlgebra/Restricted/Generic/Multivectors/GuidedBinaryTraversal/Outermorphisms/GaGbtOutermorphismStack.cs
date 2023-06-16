using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal.Outermorphisms
{
    public sealed class RGaGbtOutermorphismStack<T>
        : RGaGbtStack1
    {
        public static RGaGbtOutermorphismStack<T> Create(IScalarProcessor<T> scalarProcessor, IReadOnlyList<RGaVector<T>> basisVectorsMappingsList)
        {
            var domainVSpaceDim = basisVectorsMappingsList.Count;
            var targetVSpaceDim = basisVectorsMappingsList[0].VSpaceDimensions;
            var capacity = domainVSpaceDim + 1;

            return new RGaGbtOutermorphismStack<T>(
                capacity,
                domainVSpaceDim,
                targetVSpaceDim,
                scalarProcessor,
                basisVectorsMappingsList
            );
        }


        private RGaKVector<T>[] KVectorArray { get; }


        public int DomainVSpaceDimension { get; }

        public int TargetVSpaceDimension { get; }

        public ulong DomainGaSpaceDimension 
            => 1UL << DomainVSpaceDimension;

        public ulong TargetGaSpaceDimension 
            => 1UL << TargetVSpaceDimension;

        public IReadOnlyList<RGaVector<T>> BasisVectorsMappingsList { get; }

        public IScalarProcessor<T> ScalarProcessor { get; }

        public RGaKVector<T> TosKVector { get; private set; }

        public RGaKVector<T> RootKVector { get; }


        private RGaGbtOutermorphismStack(int capacity, int domainVSpaceDim, int targetVSpaceDim, IScalarProcessor<T> scalarProcessor, IReadOnlyList<RGaVector<T>> basisVectorsMappingsList)
            : base(capacity, (int) Math.Max(1U, domainVSpaceDim), 0ul)
        {
            ScalarProcessor = scalarProcessor;
            KVectorArray = new RGaKVector<T>[Capacity];

            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
            BasisVectorsMappingsList = basisVectorsMappingsList;

            RootKVector = basisVectorsMappingsList[0].Processor.CreateOneScalar();
        }


        public RGaKVector<T> GetTosChildKVector0()
        {
            return TosKVector;
        }

        public RGaKVector<T> GetTosChildKVector1()
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

        public IEnumerable<RGaKvIndexScalarRecord<RGaKVector<T>>> Traverse()
        {
            //GeoNumVectorKVectorOpUtils.SetActiveVSpaceDimension(TargetVSpaceDimension);

            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new RGaKvIndexScalarRecord<RGaKVector<T>>(TosId, TosKVector);

                    continue;
                }

                PushDataOfChild(1);

                PushDataOfChild(0);
            }
        }
    }
}