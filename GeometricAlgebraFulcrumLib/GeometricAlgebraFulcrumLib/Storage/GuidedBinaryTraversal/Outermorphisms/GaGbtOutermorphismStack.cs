using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Outermorphisms
{
    public sealed class GaGbtOutermorphismStack<T>
        : GaGbtStack1
    {
        public static GaGbtOutermorphismStack<T> Create(IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<IGaStorageVector<T>> basisVectorsMappingsList)
        {
            var domainVSpaceDim = (uint) basisVectorsMappingsList.Count;
            var targetVSpaceDim = basisVectorsMappingsList[0].VSpaceDimension;
            var capacity = (int) domainVSpaceDim + 1;

            return new GaGbtOutermorphismStack<T>(
                capacity,
                domainVSpaceDim,
                targetVSpaceDim,
                scalarProcessor,
                basisVectorsMappingsList
            );
        }


        private IGaStorageKVector<T>[] KVectorArray { get; }


        public uint DomainVSpaceDimension { get; }

        public uint TargetVSpaceDimension { get; }

        public ulong DomainGaSpaceDimension 
            => 1UL << (int) DomainVSpaceDimension;

        public ulong TargetGaSpaceDimension 
            => 1UL << (int) TargetVSpaceDimension;

        public IReadOnlyList<IGaStorageVector<T>> BasisVectorsMappingsList { get; }

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGaStorageKVector<T> TosKVector { get; private set; }

        public IGaStorageKVector<T> RootKVector { get; }


        private GaGbtOutermorphismStack(int capacity, uint domainVSpaceDim, uint targetVSpaceDim, [NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] IReadOnlyList<IGaStorageVector<T>> basisVectorsMappingsList)
            : base(capacity, (int) Math.Max(1U, domainVSpaceDim), 0ul)
        {
            ScalarProcessor = scalarProcessor;
            KVectorArray = new IGaStorageKVector<T>[Capacity];

            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
            BasisVectorsMappingsList = basisVectorsMappingsList;

            RootKVector = ScalarProcessor.CreateStorageBasisScalar();
        }


        public IGaStorageKVector<T> GetTosChildKVector0()
        {
            return TosKVector;
        }

        public IGaStorageKVector<T> GetTosChildKVector1()
        {
            var basisVector = BasisVectorsMappingsList[TosTreeDepth - 1];

            return TosKVector.Grade == 0
                ? basisVector
                : ScalarProcessor.Op(basisVector, TosKVector);
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

        public IEnumerable<Tuple<ulong, IGaStorageKVector<T>>> Traverse()
        {
            //GaNumVectorKVectorOpUtils.SetActiveVSpaceDimension(TargetVSpaceDimension);

            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new Tuple<ulong, IGaStorageKVector<T>>(TosId, TosKVector);

                    continue;
                }

                PushDataOfChild(1);

                PushDataOfChild(0);
            }
        }
    }
}