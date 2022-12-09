using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Outermorphisms
{
    public sealed class GeoGbtKVectorOutermorphismStack<T>
        : GeoGbtStack1
    {
        public static GeoGbtKVectorOutermorphismStack<T> Create(IReadOnlyList<VectorStorage<T>> basisVectorsMappingsList, GaMultivector<T> mv)
        {
            var treeDepth = (int) Math.Max(1, mv.MultivectorStorage.MinVSpaceDimension);
            var capacity = treeDepth + 1;

            return new GeoGbtKVectorOutermorphismStack<T>(
                basisVectorsMappingsList,
                mv.MultivectorStorage.CreateGbtStack(treeDepth, capacity, mv.GeometricProcessor)
            );
        }

        public static GeoGbtKVectorOutermorphismStack<T> Create(IReadOnlyList<VectorStorage<T>> basisVectorsMappingsList, IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            var treeDepth = (int) Math.Max(1, mv.MinVSpaceDimension);
            var capacity = treeDepth + 1;

            return new GeoGbtKVectorOutermorphismStack<T>(
                basisVectorsMappingsList,
                mv.CreateGbtStack(treeDepth, capacity, scalarProcessor)
            );
        }


        private KVectorStorage<T>[] KVectorArray { get; }

        private IGeoGbtMultivectorStorageStack1<T> MultivectorStack { get; }

        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => MultivectorStack.ScalarProcessor;

        public IReadOnlyList<VectorStorage<T>> BasisVectorsMappingsList { get; }

        public IMultivectorStorage<T> Storage 
            => MultivectorStack.Storage;

        public KVectorStorage<T> TosKVector { get; private set; }

        public T TosValue 
            => MultivectorStack.TosScalar;

        public KVectorStorage<T> RootKVector { get; }


        private GeoGbtKVectorOutermorphismStack(IReadOnlyList<VectorStorage<T>> basisVectorsMappingsList, IGeoGbtMultivectorStorageStack1<T> multivectorStack)
            : base(multivectorStack.Capacity, multivectorStack.RootTreeDepth, multivectorStack.RootId)
        {
            KVectorArray = new KVectorStorage<T>[Capacity];

            BasisVectorsMappingsList = basisVectorsMappingsList;
            MultivectorStack = multivectorStack;

            RootKVector = ScalarProcessor.CreateKVectorStorageBasisScalar();
        }


        public KVectorStorage<T> GetTosChildKVector0()
        {
            return TosKVector;
        }

        public KVectorStorage<T> GetTosChildKVector1()
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

        public IEnumerable<Tuple<T, KVectorStorage<T>>> TraverseForScaledKVectors()
        {
            //GeoNumVectorKVectorOpUtils.SetActiveVSpaceDimension(Multivector.VSpaceDimension);

            PushRootData();

            //var maxStackSizeCounter = 0;

            while (!IsEmpty)
            {
                //maxStackSizeCounter = Math.Max(maxStackSizeCounter, nodesStack.Count);

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (!ScalarProcessor.IsZero(TosValue))
                        yield return new Tuple<T, KVectorStorage<T>>(TosValue, TosKVector);

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

        public IEnumerable<IndexScalarRecord<KVectorStorage<T>>> TraverseForIdKVectors()
        {
            //GeoNumVectorKVectorOpUtils.SetActiveVSpaceDimension(Multivector.VSpaceDimension);

            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new IndexScalarRecord<KVectorStorage<T>>(TosId, TosKVector);

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