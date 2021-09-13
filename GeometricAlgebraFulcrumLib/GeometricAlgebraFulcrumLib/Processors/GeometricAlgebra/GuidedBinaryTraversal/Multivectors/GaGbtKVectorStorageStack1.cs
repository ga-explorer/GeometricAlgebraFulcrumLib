using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Multivectors
{
    /// <summary>
    /// TODO: Simplify this to handle a single grade k-vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GeoGbtKVectorStorageStack1<T>
        : GeoGbtStack1, IGeoGbtMultivectorStorageStack1<T>
    {
        public static GeoGbtKVectorStorageStack1<T> Create(int capacity, int treeDepth, IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> multivectorStorage)
        {
            return new GeoGbtKVectorStorageStack1<T>(capacity, treeDepth, scalarProcessor, multivectorStorage);
        }


        private ulong[] ActiveGradesBitMask0Array { get; }

        private ulong[] ActiveGradesBitMask1Array { get; }


        public KVectorStorage<T> KVectorStorage { get; }

        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public IMultivectorStorage<T> Storage 
            => KVectorStorage;

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


        private GeoGbtKVectorStorageStack1(int capacity, int treeDepth, [NotNull] IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] KVectorStorage<T> multivectorStorage)
            : base(capacity, treeDepth, 0ul)
        {
            ScalarProcessor = scalarProcessor;
            KVectorStorage = multivectorStorage;
            ActiveGradesBitPattern = 1UL << (int) multivectorStorage.Grade;

            ActiveGradesBitMask0Array = new ulong[capacity];
            ActiveGradesBitMask1Array = new ulong[capacity];

            RootActiveGradesBitMask0 = 
                RootActiveGradesBitMask1 = 
                    (1ul << (int) (multivectorStorage.MinVSpaceDimension + 2)) - 1;
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
                TosScalar = ScalarProcessor.GetTermScalar(Storage, TosId);
            }

            TosIndex--;
        }

        public override bool TosHasChild(int childIndex)
        {
            if ((childIndex & 1) == 0)
                return TosChildActiveGradesBitPattern0 != 0 && (
                    TosTreeDepth > 1 || Storage.ContainsTerm(TosChildId0)
                );

            return TosChildActiveGradesBitPattern1 != 0 && (
                TosTreeDepth > 1 || Storage.ContainsTerm(TosChildId1)
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
}