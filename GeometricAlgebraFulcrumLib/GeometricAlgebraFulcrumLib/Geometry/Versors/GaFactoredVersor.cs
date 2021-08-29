using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Versors
{
    public sealed class GaFactoredVersor<T> 
        : IGaVersor<T>
    {
        public static GaFactoredVersor<T> Create(IGaProcessor<T> processor, IEnumerable<IGaVectorStorage<T>> unitVectorStorages)
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
                new IGaVectorStorage<T>[0]
            );
        }


        public IGaSpace Space => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public ILaProcessor<T> ScalarsGridProcessor 
            => Processor;

        public IGaKVectorStorage<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IReadOnlyList<IGaVectorStorage<T>> UnitVectorStorages { get; }

        public bool IsValid
        {
            get
            {
                return UnitVectorStorages
                    .Select(vector =>
                        Processor.Subtract(
                            Processor.ENormSquared(vector),
                            Processor.ScalarOne
                        )
                    )
                    .All(unitDiff => Processor.IsNearZero(unitDiff));
            }
        }

        public bool IsInvalid 
            => !IsValid;


        private GaFactoredVersor([NotNull] IGaProcessor<T> processor, [NotNull] IReadOnlyList<IGaVectorStorage<T>> unitVectorStorages)
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

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            return MapVector(
                Processor.CreateGaVectorStorage(index)
            );
        }

        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            throw new NotImplementedException();
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            return MapVector(
                Processor.CreateGaVectorStorage(index)
            );
        }

        public IGaBivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            return MapBivector(
                Processor.CreateBivectorStorage(index1, index2)
            );
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return MapBivector(
                Processor.CreateBivectorStorage(index1, index2)
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            return MapKVector(
                Processor.CreateKVectorStorage(id)
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            return MapKVector(
                Processor.CreateKVectorStorage(grade, index)
            );
        }

        public IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> storage)
        {
            return storage;
        }

        public IGaKVectorStorage<T> MapTerm(IGaKVectorStorage<T> storage)
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
                        Processor.EGp(Processor.EGp(vector, current), vector).GetVectorPart()
                );

            return negativeFlag
                ? mappedStorage.GetVectorPart(Processor.Negative)
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
                        Processor.EGp(Processor.EGp(vector, current), vector).GetBivectorPart()
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
                        Processor.EGp(Processor.EGp(vector, current), vector).GetKVectorPart(grade)
                );

            return negativeFlag
                ? mappedStorage.GetKVectorPart(grade, Processor.Negative)
                : mappedStorage.GetKVectorPart(grade);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> storage)
        {
            return MapMultivector((IGaMultivectorStorage<T>) storage);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorSparseStorage<T> storage)
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