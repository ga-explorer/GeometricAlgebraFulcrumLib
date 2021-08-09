using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Outermorphisms;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Computed
{
    public sealed class GaOmComputed<T> 
        : IGaOutermorphism<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGaStorageKVector<T> MappedPseudoScalar { get; }
        
        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public IReadOnlyList<IGaStorageVector<T>> MappedBasisVectors { get; }
        

        internal GaOmComputed([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] IReadOnlyList<IGaStorageVector<T>> mappedBasisVectors)
        {
            VSpaceDimension = (uint) mappedBasisVectors.Count;
            ScalarProcessor = scalarProcessor;
            MappedBasisVectors = mappedBasisVectors;
            MappedPseudoScalar = ScalarProcessor.Op(mappedBasisVectors);
        }

        
        public IReadOnlyList<IGaStorageVector<T>> GetMappedBasisVectors()
        {
            return MappedBasisVectors;
        }

        public IGaStorageVector<T> MapBasisVector(ulong index)
        {
            return MappedBasisVectors[(int)index];
        }

        public IGaStorageBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return GaStorageBivector<T>.ZeroBivector;

            return index1 < index2 
                ? ScalarProcessor.Op(MappedBasisVectors[(int) index1], MappedBasisVectors[(int) index2])
                : ScalarProcessor.Op(MappedBasisVectors[(int) index2], MappedBasisVectors[(int) index1]);
        }

        public IGaStorageKVector<T> MapBasisBlade(ulong id)
        {
            if (id == 0)
                return ScalarProcessor.CreateStorageBasisScalar();

            if (id.IsBasicPattern())
                return MappedBasisVectors[(int)id.BasisBladeIndex()];

            var kVectorStorageList = 
                MappedBasisVectors.PickItemsUsingPattern(id);

            return ScalarProcessor.Op(kVectorStorageList);
        }

        public IGaStorageKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            if (grade == 0)
                return ScalarProcessor.CreateStorageBasisScalar();

            if (grade == 1)
                return MappedBasisVectors[(int)index];

            var id = GaBasisUtils.BasisBladeId(grade, index);

            var kVectorStorageList = 
                MappedBasisVectors.PickItemsUsingPattern(id);

            return ScalarProcessor.Op(kVectorStorageList);
        }
        
        public IGaStorageVector<T> MapVector(IGaStorageVector<T> vector)
        {
            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, 1);

            foreach (var (index, scalar) in vector.IndexScalarDictionary)
                storage.AddLeftScaledTerms(
                    scalar, 
                    MappedBasisVectors[(int)index].IndexScalarDictionary
                );

            storage.RemoveZeroTerms();

            return storage.GetVector();
        }

        public IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> bivector)
        {
            var scaledKVectorsList = GaGbtKVectorOutermorphismStack<T>
                .Create(MappedBasisVectors, ScalarProcessor, bivector)
                .TraverseForScaledKVectors();

            var storage = new GaStorageComposerBivector<T>(ScalarProcessor);

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddLeftScaledTerms(
                    scalingFactor, 
                    kVectorStorage.IndexScalarDictionary
                );

            storage.RemoveZeroTerms();

            return storage.GetBivector();
        }

        public IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> kVector)
        {
            var scaledKVectorsList = GaGbtKVectorOutermorphismStack<T>
                .Create(MappedBasisVectors, ScalarProcessor, kVector)
                .TraverseForScaledKVectors();

            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, kVector.Grade);

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddLeftScaledTerms(
                    scalingFactor, 
                    kVectorStorage.IndexScalarDictionary
                );

            storage.RemoveZeroTerms();

            return storage.GetKVector();
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> mv)
        {
            var scaledKVectorsList = GaGbtMultivectorOutermorphismStack<T>
                .Create(MappedBasisVectors, ScalarProcessor, mv)
                .TraverseForScaledKVectors();

            var storage = new GaStorageComposerMultivectorGraded<T>(ScalarProcessor);

            storage.AddLeftScaledKVectors(scaledKVectorsList);

            storage.RemoveZeroTerms();

            return storage.GetMultivector();
        }


        //public override GaNumMapUnilinear<T, TArray> Adjoint()
        //{
        //    return new GaNumOutermorphism<T, TArray>(
        //        ArraysDomain.Adjoint(VectorsMappingMatrix)
        //    );
        //}

        //public override GaNumMapUnilinear<T, TArray> Inverse()
        //{
        //    return new GaNumOutermorphism<T, TArray>(
        //        ArraysDomain.Inverse(VectorsMappingMatrix)
        //    );
        //}

        //public override GaNumMapUnilinear<T, TArray> InverseAdjoint()
        //{
        //    return new GaNumOutermorphism<T, TArray>(
        //        ArraysDomain.InverseAdjoint(VectorsMappingMatrix)
        //    );
        //}
        

        //public override IEnumerable<Tuple<ulong, GaMultivector<T>>> BasisBladeMaps()
        //{
        //    var mvStack = new Stack<Tuple<int, int>>();
        //    mvStack.Push(
        //        Tuple.Create(0, DomainVSpaceDimension)
        //    );

        //    var opStack = new Stack<GaMultivector<T>>();
        //    opStack.Push(
        //        GaMultivector<T>.CreateScalar(TargetGaSpaceDimension, 1)
        //    );

        //    while (mvStack.Count > 0)
        //    {
        //        var mvNode = mvStack.Pop();
        //        var opNode = opStack.Pop();

        //        if (mvNode.Item2 == 0)
        //        {
        //            yield return Tuple.Create(
        //                mvNode.Item1, 
        //                (GaMultivector<T>) opNode
        //            );

        //            continue;
        //        }

        //        var childNodeTreeDepth = mvNode.Item2 - 1;
        //        var basisVectorMv = 
        //            MappedBasisVectors[mvNode.Item2 - 1];

        //        mvStack.Push(Tuple.Create(
        //            mvNode.Item1 | (1 << childNodeTreeDepth), 
        //            childNodeTreeDepth
        //        ));
        //        opStack.Push(basisVectorMv.Op(opNode));

        //        mvStack.Push(Tuple.Create(
        //            mvNode.Item1, 
        //            childNodeTreeDepth
        //        ));
        //        opStack.Push(opNode);
        //    }
        //}

        //public override IEnumerable<Tuple<ulong, GaMultivector<T>>> BasisVectorMaps()
        //{
        //    return MappedBasisVectors.Select(
        //        (mv, i) => Tuple.Create(i, (GaMultivector<T>) mv)
        //    );
        //}
    }
}