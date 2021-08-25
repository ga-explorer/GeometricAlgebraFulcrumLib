using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Utils;

namespace GeometricAlgebraFulcrumLib.Geometry.Versors
{
    public sealed class GaFactoredVersor<T> 
        : IGaVersor<T>
    {
        public static GaFactoredVersor<T> Create(IGaProcessor<T> processor, IEnumerable<IGaStorageVector<T>> unitVectorStorages)
        {
            return new GaFactoredVersor<T>(
                processor,
                unitVectorStorages.ToArray()
            );
        }

        public static GaFactoredVersor<T> CreateIdentity(IGaProcessor<T> processor)
        {
            return new GaFactoredVersor<T>(
                processor,
                new IGaStorageVector<T>[0]
            );
        }


        public IGaSpace Space => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaScalarsGridProcessor<T> ScalarsGridProcessor 
            => Processor;

        public IGaStorageKVector<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IReadOnlyList<IGaStorageVector<T>> UnitVectorStorages { get; }

        public bool IsValid
        {
            get
            {
                return UnitVectorStorages
                    .Select(vector =>
                        Processor.Subtract(
                            Processor.ENormSquared(vector),
                            Processor.GetOneScalar()
                        )
                    )
                    .All(unitDiff => Processor.IsNearZero(unitDiff));
            }
        }

        public bool IsInvalid 
            => !IsValid;


        private GaFactoredVersor([NotNull] IGaProcessor<T> processor, [NotNull] IReadOnlyList<IGaStorageVector<T>> unitVectorStorages)
        {
            Processor = processor;
            UnitVectorStorages = unitVectorStorages;
        }


        public IGaOutermorphism<T> GetAdjoint()
        {
            return UnitVectorStorages.Count == 0
                ? CreateIdentity(Processor)
                : new GaFactoredVersor<T>(
                    Processor, 
                    UnitVectorStorages.Reverse().ToArray()
                );
        }

        public IGaStorageVector<T> MapBasisVector(int index)
        {
            return MapVector(
                Processor.CreateStorageBasisVector(index)
            );
        }

        public IReadOnlyList<IGaStorageVector<T>> GetMappedBasisVectors()
        {
            throw new NotImplementedException();
        }

        public IGaStorageVector<T> MapBasisVector(ulong index)
        {
            return MapVector(
                Processor.CreateStorageBasisVector(index)
            );
        }

        public IGaStorageBivector<T> MapBasisBivector(int index1, int index2)
        {
            return MapBivector(
                Processor.CreateStorageBasisBivector(index1, index2)
            );
        }

        public IGaStorageBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return MapBivector(
                Processor.CreateStorageBasisBivector(index1, index2)
            );
        }

        public IGaStorageKVector<T> MapBasisBlade(ulong id)
        {
            return MapKVector(
                Processor.CreateStorageBasisBlade(id)
            );
        }

        public IGaStorageKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            return MapKVector(
                Processor.CreateStorageBasisBlade(grade, index)
            );
        }

        public IGaStorageScalar<T> MapScalar(IGaStorageScalar<T> storage)
        {
            return storage;
        }

        public IGaStorageKVector<T> MapTerm(IGaStorageKVector<T> storage)
        {
            return MapKVector(storage);
        }

        public IGaStorageVector<T> MapVector(IGaStorageVector<T> storage)
        {
            if (UnitVectorStorages.Count == 0)
                return storage;

            var negativeFlag =
                UnitVectorStorages.Count.IsOdd();

            var mappedStorage = 
                UnitVectorStorages.Aggregate(
                    storage, 
                    (current, vector) => 
                        Processor.EGp(Processor.EGp(vector, current), vector).GetVectorPart()
                );

            return negativeFlag
                ? mappedStorage.GetVectorPart(Processor.Negative)
                : mappedStorage.GetVectorPart();
        }

        public IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> storage)
        {
            if (UnitVectorStorages.Count == 0)
                return storage;

            var mappedStorage = 
                UnitVectorStorages.Aggregate(
                    storage, 
                    (current, vector) => 
                        Processor.EGp(Processor.EGp(vector, current), vector).GetBivectorPart()
                );

            return mappedStorage.GetBivectorPart();
        }

        public IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> storage)
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
                        Processor.EGp(Processor.EGp(vector, current), vector).GetKVectorPart(grade)
                );

            return negativeFlag
                ? mappedStorage.GetKVectorPart(grade, Processor.Negative)
                : mappedStorage.GetKVectorPart(grade);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivectorGraded<T> storage)
        {
            return MapMultivector((IGaStorageMultivector<T>) storage);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivectorSparse<T> storage)
        {
            return MapMultivector((IGaStorageMultivector<T>) storage);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> storage)
        {
            if (UnitVectorStorages.Count == 0)
                return storage;

            var mappedStorage = 
                UnitVectorStorages.Aggregate(
                    storage, 
                    (current, vector) => 
                        Processor.EGp(Processor.EGp(vector, Processor.GradeInvolution(current)), vector)
                );

            return mappedStorage;
        }

        public GaPureRotorsSequence<T> CreatePureRotorsSequence()
        {
            if (UnitVectorStorages.Count % 2 != 0)
                throw new InvalidOperationException();

            var rotorsCount = UnitVectorStorages.Count / 2;

            var simpleRotorsArray = new GaPureRotor<T>[rotorsCount];

            for (var i = 0; i < rotorsCount; i++)
            {
                var rotor = 
                    Processor.EGp(UnitVectorStorages[2 * i + 1], UnitVectorStorages[2 * i]);

                simpleRotorsArray[i] = 
                    new GaPureRotor<T>(
                        Processor, 
                        rotor, 
                        Processor.Reverse(rotor)
                    );
            }

            return GaPureRotorsSequence<T>.Create(
                Processor,
                simpleRotorsArray
            );
        }
    }
}