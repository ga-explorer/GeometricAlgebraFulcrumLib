using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Geometry.Versors
{
    public sealed class GeoFactoredVersor<T> 
        : IGeoVersor<T>
    {
        private readonly KVectorStorage<T> _mappedPseudoScalar;

        public static GeoFactoredVersor<T> Create(IGeometricAlgebraProcessor<T> processor, IEnumerable<VectorStorage<T>> unitVectorStorages)
        {
            return new GeoFactoredVersor<T>(
                processor,
                unitVectorStorages.ToArray()
            );
        }

        public static GeoFactoredVersor<T> CreateIdentity(IGeometricAlgebraProcessor<T> processor)
        {
            return new GeoFactoredVersor<T>(
                processor,
                Array.Empty<VectorStorage<T>>()
            );
        }


        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public IReadOnlyList<VectorStorage<T>> UnitVectorStorages { get; }

        public bool IsValid
        {
            get
            {
                return UnitVectorStorages
                    .Select(vector =>
                        GeometricProcessor.Subtract(
                            GeometricProcessor.ENormSquared(vector),
                            GeometricProcessor.ScalarOne
                        )
                    )
                    .All(unitDiff => GeometricProcessor.IsNearZero(unitDiff));
            }
        }

        public bool IsInvalid 
            => !IsValid;


        private GeoFactoredVersor([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] IReadOnlyList<VectorStorage<T>> unitVectorStorages)
        {
            GeometricProcessor = processor;
            UnitVectorStorages = unitVectorStorages;
        }


        public IOutermorphism<T> GetAdjoint()
        {
            return UnitVectorStorages.Count == 0
                ? CreateIdentity(GeometricProcessor)
                : new GeoFactoredVersor<T>(
                    GeometricProcessor, 
                    UnitVectorStorages.Reverse().ToArray()
                );
        }

        public VectorStorage<T> MapBasisVector(int index)
        {
            return OmMapVector(
                GeometricProcessor.CreateVectorBasisStorage(index)
            );
        }

        public ILinMatrixGradedStorage<T> GetOmMappingMatrix()
        {
            throw new NotImplementedException();
        }

        public ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexVectorStorageRecord<T>> GetOmMappedBasisVectors()
        {
            throw new NotImplementedException();
        }

        public IOutermorphism<T> GetOmAdjoint()
        {
            throw new NotImplementedException();
        }

        public VectorStorage<T> OmMapBasisVector(ulong index)
        {
            return OmMapVector(
                GeometricProcessor.CreateVectorBasisStorage(index)
            );
        }

        public BivectorStorage<T> OmMapBasisBivector(ulong index)
        {
            throw new NotImplementedException();
        }

        public BivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            return OmMapBivector(
                GeometricProcessor.CreateBivectorBasisStorage(index1, index2)
            );
        }

        public BivectorStorage<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            return OmMapBivector(
                GeometricProcessor.CreateBivectorBasisStorage(index1, index2)
            );
        }

        public KVectorStorage<T> OmMapBasisBlade(ulong id)
        {
            return OmMapKVector(
                GeometricProcessor.CreateKVectorBasisStorage(id)
            );
        }

        public KVectorStorage<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return OmMapKVector(
                GeometricProcessor.CreateKVectorBasisStorage(grade, index)
            );
        }

        public KVectorStorage<T> MapTerm(KVectorStorage<T> storage)
        {
            return OmMapKVector(storage);
        }

        public VectorStorage<T> OmMapVector(VectorStorage<T> storage)
        {
            if (UnitVectorStorages.Count == 0)
                return storage;

            var negativeFlag =
                UnitVectorStorages.Count.IsOdd();

            var mappedStorage = 
                UnitVectorStorages.Aggregate(
                    storage, 
                    (current, vector) => 
                        GeometricProcessor.EGp(GeometricProcessor.EGp(vector, current), vector).GetVectorPart()
                );

            return negativeFlag
                ? mappedStorage.GetVectorPart(GeometricProcessor.Negative)
                : mappedStorage.GetVectorPart();
        }

        public BivectorStorage<T> OmMapBivector(BivectorStorage<T> storage)
        {
            if (UnitVectorStorages.Count == 0)
                return storage;

            var mappedStorage = 
                UnitVectorStorages.Aggregate(
                    storage, 
                    (current, vector) => 
                        GeometricProcessor.EGp(GeometricProcessor.EGp(vector, current), vector).GetBivectorPart()
                );

            return mappedStorage.GetBivectorPart();
        }

        public KVectorStorage<T> OmMapKVector(KVectorStorage<T> storage)
        {
            if (UnitVectorStorages.Count == 0)
                return storage;

            var grade = storage.Grade;

            var negativeFlag =
                grade.IsOdd() && UnitVectorStorages.Count.IsOdd();

            var mappedStorage = 
                UnitVectorStorages.Aggregate(
                    storage, 
                    (current, vector) => 
                        GeometricProcessor.EGp(GeometricProcessor.EGp(vector, current), vector).GetKVectorPart(grade)
                );

            return negativeFlag
                ? mappedStorage.GetKVectorPart(grade, GeometricProcessor.Negative)
                : mappedStorage.GetKVectorPart(grade);
        }

        public MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> multivector)
        {
            throw new NotImplementedException();
        }

        public MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> multivector)
        {
            throw new NotImplementedException();
        }

        public ILinMatrixStorage<T> GetVectorOmMappingMatrix()
        {
            throw new NotImplementedException();
        }

        public ILinMatrixStorage<T> GetBivectorOmMappingMatrix()
        {
            throw new NotImplementedException();
        }

        public ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapKVector(KVectorStorage<T> mv)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapMultivector(MultivectorGradedStorage<T> mv)
        {
            throw new NotImplementedException();
        }

        public ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades()
        {
            throw new NotImplementedException();
        }

        IUnilinearMap<T> IUnilinearMap<T>.GetAdjoint()
        {
            return GetAdjoint();
        }

        public IMultivectorStorage<T> MapBasisScalar()
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisVector(ulong index)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisBivector(ulong index)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisBlade(ulong id)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapScalar(T mv)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapVector(VectorStorage<T> mv)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBivector(BivectorStorage<T> mv)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapMultivector(MultivectorStorage<T> storage)
        {
            if (UnitVectorStorages.Count == 0)
                return storage;

            var mappedStorage = 
                UnitVectorStorages.Aggregate(
                    (IMultivectorStorage<T>) storage, 
                    (current, vector) => 
                        GeometricProcessor.EGp(GeometricProcessor.EGp(vector, GeometricProcessor.GradeInvolution(current)), vector)
                );

            return mappedStorage;
        }

        public PureRotorsSequence<T> CreatePureRotorsSequence()
        {
            if (UnitVectorStorages.Count % 2 != 0)
                throw new InvalidOperationException();

            var rotorsCount = UnitVectorStorages.Count / 2;

            var simpleRotorsArray = new PureRotor<T>[rotorsCount];

            for (var i = 0; i < rotorsCount; i++)
            {
                var rotor = 
                    GeometricProcessor.EGp(UnitVectorStorages[2 * i + 1], UnitVectorStorages[2 * i]);

                simpleRotorsArray[i] = 
                    new PureRotor<T>(
                        GeometricProcessor, 
                        GeometricProcessor.GetScalar(rotor), 
                        rotor.GetBivectorPart()
                    );
            }

            return PureRotorsSequence<T>.Create(
                GeometricProcessor,
                simpleRotorsArray
            );
        }

        public ILinVectorStorage<T> LinMapBasisVector(ulong index)
        {
            throw new NotImplementedException();
        }

        public ILinVectorStorage<T> LinMapVector(ILinVectorStorage<T> vectorStorage)
        {
            throw new NotImplementedException();
        }

        public ILinMatrixStorage<T> LinMapMatrix(ILinMatrixStorage<T> matrixStorage)
        {
            throw new NotImplementedException();
        }

        public ILinMatrixStorage<T> GetLinMappingMatrix()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexLinVectorStorageRecord<T>> GetLinMappedBasisVectors()
        {
            throw new NotImplementedException();
        }
    }
}