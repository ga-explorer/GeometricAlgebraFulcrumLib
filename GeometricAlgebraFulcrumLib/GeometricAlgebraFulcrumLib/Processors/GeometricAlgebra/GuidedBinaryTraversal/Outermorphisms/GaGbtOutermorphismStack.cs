using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Outermorphisms
{
    public sealed class GeoGbtOutermorphismStack<T>
        : GeoGbtStack1
    {
        public static GeoGbtOutermorphismStack<T> Create(IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyList<VectorStorage<T>> basisVectorsMappingsList)
        {
            var domainVSpaceDim = (uint) basisVectorsMappingsList.Count;
            var targetVSpaceDim = basisVectorsMappingsList[0].MinVSpaceDimension;
            var capacity = (int) domainVSpaceDim + 1;

            return new GeoGbtOutermorphismStack<T>(
                capacity,
                domainVSpaceDim,
                targetVSpaceDim,
                scalarProcessor,
                basisVectorsMappingsList
            );
        }


        private KVectorStorage<T>[] KVectorArray { get; }


        public uint DomainVSpaceDimension { get; }

        public uint TargetVSpaceDimension { get; }

        public ulong DomainGaSpaceDimension 
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public ulong TargetGaSpaceDimension 
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public IReadOnlyList<VectorStorage<T>> BasisVectorsMappingsList { get; }

        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public KVectorStorage<T> TosKVector { get; private set; }

        public KVectorStorage<T> RootKVector { get; }


        private GeoGbtOutermorphismStack(int capacity, uint domainVSpaceDim, uint targetVSpaceDim, [NotNull] IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] IReadOnlyList<VectorStorage<T>> basisVectorsMappingsList)
            : base(capacity, (int) Math.Max(1U, domainVSpaceDim), 0ul)
        {
            ScalarProcessor = scalarProcessor;
            KVectorArray = new KVectorStorage<T>[Capacity];

            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
            BasisVectorsMappingsList = basisVectorsMappingsList;

            RootKVector = ScalarProcessor.CreateKVectorStorageBasisScalar();
        }


        public KVectorStorage<T> GetTosChildKVector0()
        {
            return TosKVector;
        }

        public KVectorStorage<T> GetTosChildKVector1()
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

        public IEnumerable<IndexScalarRecord<KVectorStorage<T>>> Traverse()
        {
            //GeoNumVectorKVectorOpUtils.SetActiveVSpaceDimension(TargetVSpaceDimension);

            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new IndexScalarRecord<KVectorStorage<T>>(TosId, TosKVector);

                    continue;
                }

                PushDataOfChild(1);

                PushDataOfChild(0);
            }
        }
    }
}