using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public sealed class LinearMapOutermorphism<T> 
        : OutermorphismBase<T>
    {
        public override ILinearAlgebraProcessor<T> LinearProcessor 
            => LinearMap.LinearProcessor;

        public ILinUnilinearMap<T> LinearMap { get; }


        internal LinearMapOutermorphism([NotNull] ILinUnilinearMap<T> linearMap)
        {
            LinearMap = linearMap;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IOutermorphism<T> GetOmAdjoint()
        {
            return new LinearMapOutermorphism<T>(LinearMap.GetLinAdjoint());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> OmMapBasisVector(ulong index)
        {
            return LinearMap.LinMapBasisVector(index).CreateVectorStorage();
        }

        public override BivectorStorage<T> OmMapBasisBivector(ulong index)
        {
            var (index1, index2) = 
                index.BasisBivectorIndexToVectorIndices();

            return LinearProcessor.Op(
                OmMapBasisVector(index1), 
                OmMapBasisVector(index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return BivectorStorage<T>.ZeroBivector;

            return index1 < index2 
                ? LinearProcessor.Op(
                    OmMapBasisVector(index1), 
                    OmMapBasisVector(index2)
                )
                : LinearProcessor.Op(
                    OmMapBasisVector(index2), 
                    OmMapBasisVector(index1)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapBasisBlade(ulong id)
        {
            if (id == 0)
                return LinearProcessor.CreateKVectorBasisScalarStorage();

            if (id.IsBasicPattern())
                return OmMapBasisVector(id.BasisBladeIdToIndex());
            
            return LinearProcessor.Op(
                id.BasisBladeIdToBasisVectorIndices().Select(OmMapBasisVector)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return grade switch
            {
                0 => LinearProcessor.CreateKVectorBasisScalarStorage(),
                1 => OmMapBasisVector(index),
                _ => LinearProcessor.Op(
                    BasisBladeUtils
                        .BasisBladeGradeIndexToBasisVectorIds(grade, index)
                        .Select(OmMapBasisVector)
                    )
            };
        }
        

        public override VectorStorage<T> OmMapVector(VectorStorage<T> vector)
        {
            var storage = 
                LinearProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in vector.GetIndexScalarRecords())
                storage.AddScaledTerms(
                    scalar, 
                    OmMapBasisVector(index).GetIndexScalarRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateVectorStorage();
        }

        public override BivectorStorage<T> OmMapBivector(BivectorStorage<T> bivector)
        {
            var dict = 
                GetOmMappedBasisVectors().ToDictionary(
                    r => (int) r.Index,
                    r => r.Storage
                );

            var basisVectorsMappingsList = 
                new SparseReadOnlyList<VectorStorage<T>>(
                    dict.Keys.Max(), 
                    KVectorStorage<T>.ZeroVector, 
                    dict
                );

            var scaledKVectorsList = 
                GeoGbtKVectorOutermorphismStack<T>
                    .Create(basisVectorsMappingsList, LinearProcessor, bivector)
                    .TraverseForScaledKVectors();

            var storage = 
                LinearProcessor.CreateVectorStorageComposer();

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddScaledTerms(
                    scalingFactor, 
                    kVectorStorage.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateBivectorStorage();
        }

        public override KVectorStorage<T> OmMapKVector(KVectorStorage<T> kVector)
        {
            var dict = 
                GetOmMappedBasisVectors().ToDictionary(
                    r => (int) r.Index,
                    r => r.Storage
                );

            var basisVectorsMappingsList = 
                new SparseReadOnlyList<VectorStorage<T>>(
                    dict.Keys.Max(), 
                    KVectorStorage<T>.ZeroVector, 
                    dict
                );

            var scaledKVectorsList = 
                GeoGbtKVectorOutermorphismStack<T>
                    .Create(basisVectorsMappingsList, LinearProcessor, kVector)
                    .TraverseForScaledKVectors();

            var storage = 
                LinearProcessor.CreateVectorStorageComposer();

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddScaledTerms(
                    scalingFactor, 
                    kVectorStorage.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateKVectorStorage(kVector.Grade);
        }

        public override MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> multivector)
        {
            var dict = 
                GetOmMappedBasisVectors().ToDictionary(
                    r => (int) r.Index,
                    r => r.Storage
                );

            var basisVectorsMappingsList = 
                new SparseReadOnlyList<VectorStorage<T>>(
                    dict.Keys.Max(), 
                    KVectorStorage<T>.ZeroVector, 
                    dict
                );

            var scaledKVectorsList = 
                GeoGbtMultivectorOutermorphismStack<T>
                    .Create(basisVectorsMappingsList, LinearProcessor, multivector)
                    .TraverseForScaledKVectors();

            return LinearProcessor
                .CreateMultivectorGradedStorageComposer()
                .AddScaledTerms(scaledKVectorsList)
                .RemoveZeroTerms()
                .CreateMultivectorSparseStorage();
        }

        public override MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> multivector)
        {
            var dict = 
                GetOmMappedBasisVectors().ToDictionary(
                    r => (int) r.Index,
                    r => r.Storage
                );

            var basisVectorsMappingsList = 
                new SparseReadOnlyList<VectorStorage<T>>(
                    dict.Keys.Max(), 
                    KVectorStorage<T>.ZeroVector, 
                    dict
                );

            var scaledKVectorsList = 
                GeoGbtMultivectorOutermorphismStack<T>
                    .Create(basisVectorsMappingsList, LinearProcessor, multivector)
                    .TraverseForScaledKVectors();

            return LinearProcessor
                .CreateMultivectorGradedStorageComposer()
                .AddScaledTerms(scaledKVectorsList)
                .RemoveZeroTerms()
                .CreateMultivectorGradedStorage();
        }


        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        public override ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetVectorOmMappingMatrix()
        {
            return LinearMap.GetLinMappingMatrix();
        }

        public override ILinMatrixStorage<T> GetBivectorOmMappingMatrix()
        {
            throw new NotImplementedException();
        }

        public override ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade)
        {
            throw new NotImplementedException();
        }

        public override ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix()
        {
            throw new NotImplementedException();
        }


        public override IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IndexVectorStorageRecord<T>> GetOmMappedBasisVectors()
        {
            return LinearMap
                .GetLinMappedBasisVectors()
                .Select(r => 
                    new IndexVectorStorageRecord<T>(
                        r.Index, 
                        r.Storage.CreateVectorStorage()
                    )
                );
        }
    }
}