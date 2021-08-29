using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Computed
{
    public sealed class GaOmComputed<T> 
        : IGaOutermorphism<T>
    {
        public ILaProcessor<T> ScalarsGridProcessor { get; }

        public IGaKVectorStorage<T> MappedPseudoScalar { get; }

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

        public IReadOnlyList<IGaVectorStorage<T>> MappedBasisVectors { get; }
        

        internal GaOmComputed([NotNull] ILaProcessor<T> arrayProcessor, [NotNull] IReadOnlyList<IGaVectorStorage<T>> mappedBasisVectors)
        {
            VSpaceDimension = (uint) mappedBasisVectors.Count;
            ScalarsGridProcessor = arrayProcessor;
            MappedBasisVectors = mappedBasisVectors;
            MappedPseudoScalar = ScalarsGridProcessor.Op(mappedBasisVectors);
        }

        
        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            return MappedBasisVectors;
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            return MappedBasisVectors[(int)index];
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return GaBivectorStorage<T>.ZeroBivector;

            return index1 < index2 
                ? ScalarsGridProcessor.Op(MappedBasisVectors[(int) index1], MappedBasisVectors[(int) index2])
                : ScalarsGridProcessor.Op(MappedBasisVectors[(int) index2], MappedBasisVectors[(int) index1]);
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            if (id == 0)
                return ScalarsGridProcessor.CreateStorageBasisScalar();

            if (id.IsBasicPattern())
                return MappedBasisVectors[(int)id.BasisBladeIdToIndex()];

            var kVectorStorageList = 
                MappedBasisVectors.PickItemsUsingPattern(id);

            return ScalarsGridProcessor.Op(kVectorStorageList);
        }

        public IGaKVectorStorage<T> MapBasisBlade(uint grade, ulong index)
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
        
        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> vector)
        {
            var storage = 
                ScalarsGridProcessor.CreateKVectorStorageComposer();

            foreach (var (index, scalar) in vector.IndexScalarList.GetIndexScalarRecords())
                storage.AddScaledTerms(
                    scalar, 
                    MappedBasisVectors[(int)index].IndexScalarList.GetIndexScalarRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateGaVectorStorage();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> bivector)
        {
            var scaledKVectorsList = GaGbtKVectorOutermorphismStack<T>
                .Create(MappedBasisVectors, ScalarsGridProcessor, bivector)
                .TraverseForScaledKVectors();

            var storage = 
                ScalarsGridProcessor.CreateKVectorStorageComposer();

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddScaledTerms(
                    scalingFactor, 
                    kVectorStorage.IndexScalarList.GetIndexScalarRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateGaBivectorStorage();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> kVector)
        {
            var scaledKVectorsList = GaGbtKVectorOutermorphismStack<T>
                .Create(MappedBasisVectors, ScalarsGridProcessor, kVector)
                .TraverseForScaledKVectors();

            var storage = 
                ScalarsGridProcessor.CreateKVectorStorageComposer();

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddScaledTerms(
                    scalingFactor, 
                    kVectorStorage.IndexScalarList.GetIndexScalarRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateGaKVectorStorage(kVector.Grade);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> mv)
        {
            var scaledKVectorsList = 
                GaGbtMultivectorOutermorphismStack<T>
                    .Create(MappedBasisVectors, ScalarsGridProcessor, mv)
                    .TraverseForScaledKVectors();

            return ScalarsGridProcessor
                .CreateStorageGradedMultivectorComposer()
                .AddScaledTerms(scaledKVectorsList)
                .RemoveZeroTerms()
                .CreateGaMultivectorGradedStorage();
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