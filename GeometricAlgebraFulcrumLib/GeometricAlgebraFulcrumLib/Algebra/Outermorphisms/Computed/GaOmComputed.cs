using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Computed
{
    public sealed class GaOmComputed<T> 
        : IGaOutermorphism<T>
    {
        public IGaScalarsGridProcessor<T> ScalarsGridProcessor { get; }

        public IGaStorageKVector<T> MappedPseudoScalar { get; }

        public IGaSpace Space { get; }

        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public ulong MaxBasisBladeId 
            => (VSpaceDimension.ToGaSpaceDimension()) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public IReadOnlyList<IGaStorageVector<T>> MappedBasisVectors { get; }
        

        internal GaOmComputed([NotNull] IGaScalarsGridProcessor<T> arrayProcessor, [NotNull] IReadOnlyList<IGaStorageVector<T>> mappedBasisVectors)
        {
            VSpaceDimension = (uint) mappedBasisVectors.Count;
            ScalarsGridProcessor = arrayProcessor;
            MappedBasisVectors = mappedBasisVectors;
            MappedPseudoScalar = ScalarsGridProcessor.Op(mappedBasisVectors);
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
                ? ScalarsGridProcessor.Op(MappedBasisVectors[(int) index1], MappedBasisVectors[(int) index2])
                : ScalarsGridProcessor.Op(MappedBasisVectors[(int) index2], MappedBasisVectors[(int) index1]);
        }

        public IGaStorageKVector<T> MapBasisBlade(ulong id)
        {
            if (id == 0)
                return ScalarsGridProcessor.CreateStorageBasisScalar();

            if (id.IsBasicPattern())
                return MappedBasisVectors[(int)id.BasisBladeIdToIndex()];

            var kVectorStorageList = 
                MappedBasisVectors.PickItemsUsingPattern(id);

            return ScalarsGridProcessor.Op(kVectorStorageList);
        }

        public IGaStorageKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            if (grade == 0)
                return ScalarsGridProcessor.CreateStorageBasisScalar();

            if (grade == 1)
                return MappedBasisVectors[(int)index];

            var id = index.BasisBladeIndexToId(grade);

            var kVectorStorageList = 
                MappedBasisVectors.PickItemsUsingPattern(id);

            return ScalarsGridProcessor.Op(kVectorStorageList);
        }
        
        public IGaStorageVector<T> MapVector(IGaStorageVector<T> vector)
        {
            var storage = 
                ScalarsGridProcessor.CreateStorageKVectorComposer();

            foreach (var (index, scalar) in vector.IndexScalarList.GetKeyValueRecords())
                storage.AddScaledTerms(
                    scalar, 
                    MappedBasisVectors[(int)index].IndexScalarList.GetKeyValueRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateStorageVector();
        }

        public IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> bivector)
        {
            var scaledKVectorsList = GaGbtKVectorOutermorphismStack<T>
                .Create(MappedBasisVectors, ScalarsGridProcessor, bivector)
                .TraverseForScaledKVectors();

            var storage = 
                ScalarsGridProcessor.CreateStorageKVectorComposer();

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddScaledTerms(
                    scalingFactor, 
                    kVectorStorage.IndexScalarList.GetKeyValueRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateStorageBivector();
        }

        public IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> kVector)
        {
            var scaledKVectorsList = GaGbtKVectorOutermorphismStack<T>
                .Create(MappedBasisVectors, ScalarsGridProcessor, kVector)
                .TraverseForScaledKVectors();

            var storage = 
                ScalarsGridProcessor.CreateStorageKVectorComposer();

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddScaledTerms(
                    scalingFactor, 
                    kVectorStorage.IndexScalarList.GetKeyValueRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateStorageKVector(kVector.Grade);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> mv)
        {
            var scaledKVectorsList = 
                GaGbtMultivectorOutermorphismStack<T>
                    .Create(MappedBasisVectors, ScalarsGridProcessor, mv)
                    .TraverseForScaledKVectors();

            return ScalarsGridProcessor
                .CreateStorageGradedMultivectorComposer()
                .AddScaledTerms(scaledKVectorsList)
                .RemoveZeroTerms()
                .CreateStorageGradedMultivector();
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