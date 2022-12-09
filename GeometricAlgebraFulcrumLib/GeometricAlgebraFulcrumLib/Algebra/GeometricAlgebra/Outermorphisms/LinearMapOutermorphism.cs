using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Collections;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public sealed class LinearMapOutermorphism<T> 
        : GaOutermorphismBase<T>
    {
        public override IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public ILinUnilinearMap<T> LinearMap { get; }


        internal LinearMapOutermorphism([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, [NotNull] ILinUnilinearMap<T> linearMap)
        {
            GeometricProcessor = geometricProcessor;
            LinearMap = linearMap;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaOutermorphism<T> GetOmAdjoint()
        {
            return new LinearMapOutermorphism<T>(GeometricProcessor, LinearMap.GetLinAdjoint());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaVector<T> OmMapBasisVector(ulong index)
        {
            return LinearMap.LinMapBasisVector(index).CreateVector(GeometricProcessor);
        }

        public override GaBivector<T> OmMapBasisBivector(ulong index)
        {
            var (index1, index2) = 
                index.BasisBivectorIndexToVectorIndices();

            return OmMapBasisVector(index1).Op(OmMapBasisVector(index2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return GeometricProcessor.CreateBivectorZero();

            return index1 < index2 
                ? OmMapBasisVector(index1).Op(OmMapBasisVector(index2))
                : OmMapBasisVector(index2).Op(OmMapBasisVector(index1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMapBasisBlade(ulong id)
        {
            if (id == 0)
                return GeometricProcessor.CreateKVectorBasisScalar();

            return id.IsBasicPattern() 
                ? OmMapBasisVector(id.BasisBladeIdToIndex()).AsKVector() 
                : id.BasisBladeIdToBasisVectorIndices().Select(OmMapBasisVector).Op();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return grade switch
            {
                0 => GeometricProcessor.CreateKVectorBasisScalar(),
                1 => OmMapBasisVector(index).AsKVector(),
                _ => BasisBladeUtils
                    .BasisBladeGradeIndexToBasisVectorIds(grade, index)
                    .Select(OmMapBasisVector)
                    .Op()
            };
        }
        

        public override GaVector<T> OmMap(GaVector<T> vector)
        {
            var storage = 
                GeometricProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in vector.GetIndexScalarRecords())
                storage.AddScaledTerms(
                    scalar, 
                    OmMapBasisVector(index).GetIndexScalarRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateVector();
        }

        public override GaBivector<T> OmMap(GaBivector<T> bivector)
        {
            var dict = 
                GetOmMappedBasisVectors().ToDictionary(
                    r => (int) r.Index,
                    r => r.Vector.VectorStorage
                );

            var basisVectorsMappingsList = 
                new SparseReadOnlyList<VectorStorage<T>>(
                    dict.Keys.Max(), 
                    GeometricProcessor.CreateVectorStorageZero(), 
                    dict
                );

            var scaledKVectorsList = 
                GeoGbtKVectorOutermorphismStack<T>
                    .Create(basisVectorsMappingsList, GeometricProcessor, bivector.BivectorStorage)
                    .TraverseForScaledKVectors();

            var storage = 
                GeometricProcessor.CreateVectorStorageComposer();

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddScaledTerms(
                    scalingFactor, 
                    kVectorStorage.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateBivector();
        }

        public override GaKVector<T> OmMap(GaKVector<T> kVector)
        {
            var dict = 
                GetOmMappedBasisVectors().ToDictionary(
                    r => (int) r.Index,
                    r => r.Vector.VectorStorage
                );

            var basisVectorsMappingsList = 
                new SparseReadOnlyList<VectorStorage<T>>(
                    dict.Keys.Max(), 
                    GeometricProcessor.CreateVectorStorageZero(), 
                    dict
                );

            var scaledKVectorsList = 
                GeoGbtKVectorOutermorphismStack<T>
                    .Create(basisVectorsMappingsList, GeometricProcessor, kVector.KVectorStorage)
                    .TraverseForScaledKVectors();

            var storage = 
                GeometricProcessor.CreateVectorStorageComposer();

            foreach (var (scalingFactor, kVectorStorage) in scaledKVectorsList)
                storage.AddScaledTerms(
                    scalingFactor, 
                    kVectorStorage.GetLinVectorIndexScalarStorage().GetIndexScalarRecords()
                );

            storage.RemoveZeroTerms();

            return storage.CreateKVector(kVector.Grade);
        }

        public override GaMultivector<T> OmMap(GaMultivector<T> multivector)
        {
            var dict = 
                GetOmMappedBasisVectors().ToDictionary(
                    r => (int) r.Index,
                    r => r.Vector.VectorStorage
                );

            var basisVectorsMappingsList = 
                new SparseReadOnlyList<VectorStorage<T>>(
                    dict.Keys.Max(), 
                    GeometricProcessor.CreateVectorStorageZero(), 
                    dict
                );

            var scaledKVectorsList = 
                GeoGbtMultivectorOutermorphismStack<T>
                    .Create(basisVectorsMappingsList, GeometricProcessor, multivector.MultivectorStorage)
                    .TraverseForScaledKVectors();

            return GeometricProcessor
                .CreateMultivectorGradedStorageComposer()
                .AddScaledTerms(scaledKVectorsList)
                .RemoveZeroTerms()
                .CreateMultivector();
        }


        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        public override ILinMatrixStorage<T> GetMultivectorMappingMatrixStorage()
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetVectorOmMappingMatrixStorage()
        {
            return LinearMap.GetLinMappingMatrix();
        }

        public override ILinMatrixStorage<T> GetBivectorOmMappingMatrixStorage()
        {
            throw new NotImplementedException();
        }

        public override ILinMatrixStorage<T> GetKVectorOmMappingMatrixStorage(uint grade)
        {
            throw new NotImplementedException();
        }

        public override ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrixStorage()
        {
            throw new NotImplementedException();
        }


        public override IEnumerable<IdMultivectorRecord<T>> GetMappedBasisBlades()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IndexVectorRecord<T>> GetOmMappedBasisVectors()
        {
            return LinearMap
                .GetLinMappedBasisVectors()
                .Select(r => 
                    new IndexVectorRecord<T>(
                        r.Index, 
                        r.Storage.CreateVector(GeometricProcessor)
                    )
                );
        }
    }
}