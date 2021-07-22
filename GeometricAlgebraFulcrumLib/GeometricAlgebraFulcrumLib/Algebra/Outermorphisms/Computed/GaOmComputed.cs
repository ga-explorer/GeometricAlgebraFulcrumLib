using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Outermorphisms;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Computed
{
    public sealed class GaOmComputed<T> 
        : IGaOutermorphism<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGasKVector<T> MappedPseudoScalar { get; }
        
        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public IReadOnlyList<IGasVector<T>> MappedBasisVectors { get; }
        

        internal GaOmComputed([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] IReadOnlyList<IGasVector<T>> mappedBasisVectors)
        {
            VSpaceDimension = (uint) mappedBasisVectors.Count;
            ScalarProcessor = scalarProcessor;
            MappedBasisVectors = mappedBasisVectors;
            MappedPseudoScalar = ScalarProcessor.Op(mappedBasisVectors);
        }

        
        public IReadOnlyList<IGasVector<T>> GetMappedBasisVectors()
        {
            return MappedBasisVectors;
        }

        public IGasVector<T> MapBasisVector(ulong index)
        {
            return MappedBasisVectors[(int)index];
        }

        public IGasBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return ScalarProcessor.CreateZeroBivector();

            return index1 < index2 
                ? MappedBasisVectors[(int) index1].Op(MappedBasisVectors[(int) index2]) 
                : MappedBasisVectors[(int) index2].Op(MappedBasisVectors[(int) index1]);
        }

        public IGasKVector<T> MapBasisBlade(ulong id)
        {
            if (id == 0)
                return ScalarProcessor.CreateBasisScalar();

            if (id.IsBasicPattern())
                return MappedBasisVectors[(int)id.BasisBladeIndex()];

            var kVectorStorageList = 
                MappedBasisVectors.PickItemsUsingPattern(id);

            return ScalarProcessor.Op(kVectorStorageList);
        }

        public IGasKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            if (grade == 0)
                return ScalarProcessor.CreateBasisScalar();

            if (grade == 1)
                return MappedBasisVectors[(int)index];

            var id = GaBasisUtils.BasisBladeId(grade, index);

            var kVectorStorageList = 
                MappedBasisVectors.PickItemsUsingPattern(id);

            return ScalarProcessor.Op(kVectorStorageList);
        }
        
        public IGasVector<T> MapVector(IGasVector<T> vector)
        {
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, 1);

            foreach (var (index, scalar) in vector.GetIndexScalarPairs())
                storage.AddLeftScaledTerms(
                    scalar, 
                    MappedBasisVectors[(int)index].GetIndexScalarPairs()
                );

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public IGasBivector<T> MapBivector(IGasBivector<T> bivector)
        {
            var scaledKVectorsList = GaGbtKVectorOutermorphismStack<T>
                .Create(MappedBasisVectors, bivector)
                .TraverseForScaledKVectors();

            var storage = new GaBivectorStorageComposer<T>(ScalarProcessor);

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddLeftScaledTerms(
                    scalingFactor, 
                    kVectorStorage.GetIndexScalarPairs()
                );

            storage.RemoveZeroTerms();

            return storage.GetBivectorStorage();
        }

        public IGasKVector<T> MapKVector(IGasKVector<T> kVector)
        {
            var scaledKVectorsList = GaGbtKVectorOutermorphismStack<T>
                .Create(MappedBasisVectors, kVector)
                .TraverseForScaledKVectors();

            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, kVector.Grade);

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddLeftScaledTerms(
                    scalingFactor, 
                    kVectorStorage.GetIndexScalarPairs()
                );

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public IGasMultivector<T> MapMultivector(IGasMultivector<T> mv)
        {
            var scaledKVectorsList = GaGbtMultivectorOutermorphismStack<T>
                .Create(MappedBasisVectors, mv)
                .TraverseForScaledKVectors();

            var storage = new GaMultivectorGradedStorageComposer<T>(ScalarProcessor);

            storage.AddLeftScaledKVectors(scaledKVectorsList);

            storage.RemoveZeroTerms();

            return storage.GetCompactMultivector();
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