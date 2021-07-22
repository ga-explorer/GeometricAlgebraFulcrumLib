using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Geometry.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;

namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Outermorphisms
{
    public sealed class GaGbtMultivectorOutermorphismStack<T>
        : GaGbtStack1
    {
        public static GaGbtMultivectorOutermorphismStack<T> Create(IReadOnlyList<IGasVector<T>> basisVectorsMappingsList, GaMultivector<T> mv)
        {
            var treeDepth = (int) Math.Max(1, mv.Storage.VSpaceDimension);
            var capacity = treeDepth + 1;

            return new GaGbtMultivectorOutermorphismStack<T>(
                basisVectorsMappingsList,
                mv.Storage.CreateGbtStack(treeDepth, capacity)
            );
        }

        public static GaGbtMultivectorOutermorphismStack<T> Create(IReadOnlyList<IGasVector<T>> basisVectorsMappingsList, IGasMultivector<T> mv)
        {
            var treeDepth = (int) Math.Max(1, mv.VSpaceDimension);
            var capacity = treeDepth + 1;

            return new GaGbtMultivectorOutermorphismStack<T>(
                basisVectorsMappingsList,
                mv.CreateGbtStack(treeDepth, capacity)
            );
        }


        private IGasKVector<T>[] KVectorArray { get; }

        private IGaGbtMultivectorStorageStack1<T> MultivectorStack { get; }

        public IGaScalarProcessor<T> ScalarProcessor 
            => MultivectorStack.Storage.ScalarProcessor;

        public IReadOnlyList<IGasVector<T>> BasisVectorsMappingsList { get; }

        public IGasMultivector<T> Storage 
            => MultivectorStack.Storage;

        public IGasKVector<T> TosKVector { get; private set; }

        public T TosValue 
            => MultivectorStack.TosScalar;

        public IGasKVector<T> RootKVector { get; }


        private GaGbtMultivectorOutermorphismStack(IReadOnlyList<IGasVector<T>> basisVectorsMappingsList, IGaGbtMultivectorStorageStack1<T> multivectorStack)
            : base(multivectorStack.Capacity, multivectorStack.RootTreeDepth, multivectorStack.RootId)
        {
            KVectorArray = new IGasKVector<T>[Capacity];

            BasisVectorsMappingsList = basisVectorsMappingsList;
            MultivectorStack = multivectorStack;

            RootKVector = ScalarProcessor.CreateBasisScalar();
        }


        public IGasKVector<T> GetTosChildKVector0()
        {
            return TosKVector;
        }

        public IGasKVector<T> GetTosChildKVector1()
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
                if (storage.IsEmpty())
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

        public IEnumerable<Tuple<T, IGasKVector<T>>> TraverseForScaledKVectors()
        {
            //GaNumVectorKVectorOpUtils.SetActiveVSpaceDimension(Multivector.VSpaceDimension);

            PushRootData();

            //var maxStackSizeCounter = 0;

            while (!IsEmpty)
            {
                //maxStackSizeCounter = Math.Max(maxStackSizeCounter, nodesStack.Count);

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (!ScalarProcessor.IsZero(TosValue))
                        yield return new Tuple<T, IGasKVector<T>>(TosValue, TosKVector);

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

        public IEnumerable<Tuple<ulong, IGasKVector<T>>> TraverseForIdKVectors()
        {
            //GaNumVectorKVectorOpUtils.SetActiveVSpaceDimension(Multivector.VSpaceDimension);

            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new Tuple<ulong, IGasKVector<T>>(TosId, TosKVector);

                    continue;
                }

                if (TosHasChild(1))
                    PushDataOfChild(1);

                if (TosHasChild(0))
                    PushDataOfChild(0);
            }
        }
    }
}