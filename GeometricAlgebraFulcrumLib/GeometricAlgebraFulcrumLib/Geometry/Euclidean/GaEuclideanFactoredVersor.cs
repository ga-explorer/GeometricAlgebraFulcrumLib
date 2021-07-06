using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean
{
    public sealed class GaEuclideanFactoredVersor<T> 
        : IGaVersor<T>, IGaEuclideanGeometry<T>
    {
        public static GaEuclideanFactoredVersor<T> Create(IEnumerable<IGaVectorStorage<T>> unitVectorStorages)
        {
            var unitVectorStoragesArray = unitVectorStorages.ToArray();
            var scalarProcessor = unitVectorStoragesArray[0].ScalarProcessor;

            return new GaEuclideanFactoredVersor<T>(
                scalarProcessor,
                unitVectorStoragesArray
            );
        }

        public static GaEuclideanFactoredVersor<T> CreateIdentity(IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaEuclideanFactoredVersor<T>(
                scalarProcessor,
                new IGaVectorStorage<T>[0]
            );
        }


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IReadOnlyList<IGaVectorStorage<T>> UnitVectorStorages { get; }

        public bool IsValid
        {
            get
            {
                return UnitVectorStorages
                    .Select(vector =>
                        ScalarProcessor.Subtract(
                            vector.ENormSquared(),
                            ScalarProcessor.OneScalar
                        )
                    )
                    .All(unitDiff => ScalarProcessor.IsNearZero(unitDiff));
            }
        }

        public bool IsInvalid 
            => !IsValid;


        private GaEuclideanFactoredVersor([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] IReadOnlyList<IGaVectorStorage<T>> unitVectorStorages)
        {
            ScalarProcessor = scalarProcessor;
            UnitVectorStorages = unitVectorStorages;
        }


        public IGaVectorsLinearMap<T> GetAdjoint()
        {
            return UnitVectorStorages.Count == 0
                ? CreateIdentity(ScalarProcessor)
                : new GaEuclideanFactoredVersor<T>(
                    ScalarProcessor, 
                    UnitVectorStorages.Reverse().ToArray()
                );
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            return MapVector(
                GaVectorTermStorage<T>.CreateBasisVector(ScalarProcessor, index)
            );
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            return MapVector(
                GaVectorTermStorage<T>.CreateBasisVector(ScalarProcessor, index)
            );
        }

        public IGaBivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            return MapBivector(
                GaBivectorTermStorage<T>.CreateBasisBivector(ScalarProcessor, index1, index2)
            );
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return MapBivector(
                GaBivectorTermStorage<T>.CreateBasisBivector(ScalarProcessor, index1, index2)
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            return MapKVector(
                GaKVectorTermStorage<T>.CreateBasisBlade(ScalarProcessor, id)
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index)
        {
            return MapKVector(
                GaKVectorTermStorage<T>.CreateBasisBlade(ScalarProcessor, grade, index)
            );
        }

        public IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> storage)
        {
            return storage;
        }

        public IGaKVectorStorage<T> MapTerm(IGaKVectorTermStorage<T> storage)
        {
            return MapKVector(storage);
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> storage)
        {
            if (UnitVectorStorages.Count == 0)
                return storage;

            var negativeFlag =
                UnitVectorStorages.Count.IsOdd();

            var mappedStorage = 
                UnitVectorStorages.Aggregate(
                    storage, 
                    (current, vector) => 
                        vector.EGp(current).EGp(vector).GetVectorPart()
                );

            return negativeFlag
                ? mappedStorage.GetVectorPart(ScalarProcessor.Negative)
                : mappedStorage.GetVectorPart();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> storage)
        {
            if (UnitVectorStorages.Count == 0)
                return storage;

            var mappedStorage = 
                UnitVectorStorages.Aggregate(
                    storage, 
                    (current, vector) => 
                        vector.EGp(current).EGp(vector).GetBivectorPart()
                );

            return mappedStorage.GetBivectorPart();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> storage)
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
                        vector.EGp(current).EGp(vector).GetKVectorPart(grade)
                );

            return negativeFlag
                ? mappedStorage.GetKVectorPart(grade, ScalarProcessor.Negative)
                : mappedStorage.GetKVectorPart(grade);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> storage)
        {
            return MapMultivector((IGaMultivectorStorage<T>) storage);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorTermsStorage<T> storage)
        {
            return MapMultivector((IGaMultivectorStorage<T>) storage);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> storage)
        {
            if (UnitVectorStorages.Count == 0)
                return storage;

            var mappedStorage = 
                UnitVectorStorages.Aggregate(
                    storage, 
                    (current, vector) => 
                        vector.EGp(current.GetGradeInvolution()).EGp(vector)
                );

            return mappedStorage;
        }

        public GaEuclideanSimpleRotorsSequence<T> CreateSimpleRotorsSequence()
        {
            if (UnitVectorStorages.Count % 2 != 0)
                throw new InvalidOperationException();

            var rotorsCount = UnitVectorStorages.Count / 2;

            var simpleRotorsArray = new GaEuclideanSimpleRotor<T>[rotorsCount];

            for (var i = 0; i < rotorsCount; i++)
            {
                var rotor = 
                    UnitVectorStorages[2 * i + 1].EGp(UnitVectorStorages[2 * i]);

                simpleRotorsArray[i] = GaEuclideanSimpleRotor<T>.Create(rotor);
            }

            return GaEuclideanSimpleRotorsSequence<T>.Create(
                ScalarProcessor,
                simpleRotorsArray
            );
        }
    }
}