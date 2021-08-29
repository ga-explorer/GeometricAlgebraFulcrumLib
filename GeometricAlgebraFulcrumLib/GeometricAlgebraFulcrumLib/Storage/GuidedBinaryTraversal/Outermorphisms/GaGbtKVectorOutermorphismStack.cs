using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Outermorphisms
{
    public sealed class GaGbtKVectorOutermorphismStack<T>
        : GaGbtStack1
    {
        public static GaGbtKVectorOutermorphismStack<T> Create(IReadOnlyList<IGaVectorStorage<T>> basisVectorsMappingsList, GaMultivector<T> mv)
        {
            var treeDepth = (int) Math.Max(1, mv.MultivectorStorage.MinVSpaceDimension);
            var capacity = treeDepth + 1;

            return new GaGbtKVectorOutermorphismStack<T>(
                basisVectorsMappingsList,
                mv.MultivectorStorage.CreateGbtStack(treeDepth, capacity, mv.Processor)
            );
        }

        public static GaGbtKVectorOutermorphismStack<T> Create(IReadOnlyList<IGaVectorStorage<T>> basisVectorsMappingsList, IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            var treeDepth = (int) Math.Max(1, mv.MinVSpaceDimension);
            var capacity = treeDepth + 1;

            return new GaGbtKVectorOutermorphismStack<T>(
                basisVectorsMappingsList,
                mv.CreateGbtStack(treeDepth, capacity, scalarProcessor)
            );
        }


        private IGaKVectorStorage<T>[] KVectorArray { get; }

        private IGaGbtMultivectorStorageStack1<T> MultivectorStack { get; }

        public IScalarProcessor<T> ScalarProcessor 
            => MultivectorStack.ScalarProcessor;

        public IReadOnlyList<IGaVectorStorage<T>> BasisVectorsMappingsList { get; }

        public IGaMultivectorStorage<T> Storage 
            => MultivectorStack.Storage;

        public IGaKVectorStorage<T> TosKVector { get; private set; }

        public T TosValue 
            => MultivectorStack.TosScalar;

        public IGaKVectorStorage<T> RootKVector { get; }


        private GaGbtKVectorOutermorphismStack(IReadOnlyList<IGaVectorStorage<T>> basisVectorsMappingsList, IGaGbtMultivectorStorageStack1<T> multivectorStack)
            : base(multivectorStack.Capacity, multivectorStack.RootTreeDepth, multivectorStack.RootId)
        {
            KVectorArray = new IGaKVectorStorage<T>[Capacity];

            BasisVectorsMappingsList = basisVectorsMappingsList;
            MultivectorStack = multivectorStack;

            RootKVector = ScalarProcessor.CreateStorageBasisScalar();
        }


        public IGaKVectorStorage<T> GetTosChildKVector0()
        {
            return TosKVector;
        }

        public IGaKVectorStorage<T> GetTosChildKVector1()
        {
            var basisVector = BasisVectorsMappingsList[TosTreeDepth - 1];

            var storage = TosKVector.Grade == 0
                ? basisVector
                : ScalarProcessor.Op(basisVector, TosKVector);

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

        public IEnumerable<Tuple<T, IGaKVectorStorage<T>>> TraverseForScaledKVectors()
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
                        yield return new Tuple<T, IGaKVectorStorage<T>>(TosValue, TosKVector);

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

        public IEnumerable<IndexScalarRecord<IGaKVectorStorage<T>>> TraverseForIdKVectors()
        {
            //GaNumVectorKVectorOpUtils.SetActiveVSpaceDimension(Multivector.VSpaceDimension);

            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new IndexScalarRecord<IGaKVectorStorage<T>>(TosId, TosKVector);

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