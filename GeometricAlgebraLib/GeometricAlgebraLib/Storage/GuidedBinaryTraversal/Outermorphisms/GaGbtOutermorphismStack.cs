using System;
using System.Collections.Generic;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Outermorphisms
{
    public sealed class GaGbtOutermorphismStack<T>
        : GaGbtStack1
    {
        public static GaGbtOutermorphismStack<T> Create(IReadOnlyList<IGaVectorStorage<T>> basisVectorsMappingsList)
        {
            var domainVSpaceDim = basisVectorsMappingsList.Count;
            var targetVSpaceDim = basisVectorsMappingsList[0].VSpaceDimension;
            var capacity = domainVSpaceDim + 1;

            return new GaGbtOutermorphismStack<T>(
                capacity,
                domainVSpaceDim,
                targetVSpaceDim,
                basisVectorsMappingsList
            );
        }


        private IGaKVectorStorage<T>[] KVectorArray { get; }


        public int DomainVSpaceDimension { get; }

        public int TargetVSpaceDimension { get; }

        public ulong DomainGaSpaceDimension 
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public ulong TargetGaSpaceDimension 
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public IReadOnlyList<IGaVectorStorage<T>> BasisVectorsMappingsList { get; }

        public IGaScalarProcessor<T> ScalarProcessor 
            => BasisVectorsMappingsList[0].ScalarProcessor;

        public IGaKVectorStorage<T> TosKVector { get; private set; }

        public IGaKVectorStorage<T> RootKVector { get; }


        private GaGbtOutermorphismStack(int capacity, int domainVSpaceDim, int targetVSpaceDim, IReadOnlyList<IGaVectorStorage<T>> basisVectorsMappingsList)
            : base(capacity, Math.Max(1, domainVSpaceDim), 0ul)
        {
            KVectorArray = new IGaKVectorStorage<T>[Capacity];

            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
            BasisVectorsMappingsList = basisVectorsMappingsList;

            RootKVector = GaScalarTermStorage<T>.CreateBasisScalar(ScalarProcessor);
        }


        public IGaKVectorStorage<T> GetTosChildKVector0()
        {
            return TosKVector;
        }

        public IGaKVectorStorage<T> GetTosChildKVector1()
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

        public IEnumerable<Tuple<ulong, IGaKVectorStorage<T>>> Traverse()
        {
            //GaNumVectorKVectorOpUtils.SetActiveVSpaceDimension(TargetVSpaceDimension);

            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new Tuple<ulong, IGaKVectorStorage<T>>(TosId, TosKVector);

                    continue;
                }

                PushDataOfChild(1);

                PushDataOfChild(0);
            }
        }
    }
}