using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public sealed class GaFactoredVersor<T> 
        : IGaVersor<T>
    {
        public static GaFactoredVersor<T> Create(IGaProcessor<T> processor, IEnumerable<IGasVector<T>> unitVectorStorages)
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
                new IGasVector<T>[0]
            );
        }


        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public ulong MaxBasisBladeId { get; }
        
        public uint GradesCount { get; }

        public IEnumerable<uint> Grades { get; }

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGasKVector<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IReadOnlyList<IGasVector<T>> UnitVectorStorages { get; }

        public bool IsValid
        {
            get
            {
                return UnitVectorStorages
                    .Select(vector =>
                        Processor.Subtract(
                            vector.ENormSquared(),
                            Processor.OneScalar
                        )
                    )
                    .All(unitDiff => Processor.IsNearZero(unitDiff));
            }
        }

        public bool IsInvalid 
            => !IsValid;


        private GaFactoredVersor([NotNull] IGaProcessor<T> processor, [NotNull] IReadOnlyList<IGasVector<T>> unitVectorStorages)
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

        public IGasVector<T> MapBasisVector(int index)
        {
            return MapVector(
                Processor.CreateBasisVector(index)
            );
        }

        public IReadOnlyList<IGasVector<T>> GetMappedBasisVectors()
        {
            throw new NotImplementedException();
        }

        public IGasVector<T> MapBasisVector(ulong index)
        {
            return MapVector(
                Processor.CreateBasisVector(index)
            );
        }

        public IGasBivector<T> MapBasisBivector(int index1, int index2)
        {
            return MapBivector(
                Processor.CreateBasisBivector(index1, index2)
            );
        }

        public IGasBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return MapBivector(
                Processor.CreateBasisBivector(index1, index2)
            );
        }

        public IGasKVector<T> MapBasisBlade(ulong id)
        {
            return MapKVector(
                Processor.CreateBasisBlade(id)
            );
        }

        public IGasKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            return MapKVector(
                GaStorageFactory.CreateBasisBlade(Processor, grade, index)
            );
        }

        public IGasScalar<T> MapScalar(IGasScalar<T> storage)
        {
            return storage;
        }

        public IGasKVector<T> MapTerm(IGasKVectorTerm<T> storage)
        {
            return MapKVector(storage);
        }

        public IGasVector<T> MapVector(IGasVector<T> storage)
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
                ? mappedStorage.GetVectorPart(Processor.Negative)
                : mappedStorage.GetVectorPart();
        }

        public IGasBivector<T> MapBivector(IGasBivector<T> storage)
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

        public IGasKVector<T> MapKVector(IGasKVector<T> storage)
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
                ? mappedStorage.GetKVectorPart(grade, Processor.Negative)
                : mappedStorage.GetKVectorPart(grade);
        }

        public IGasMultivector<T> MapMultivector(IGasGradedMultivector<T> storage)
        {
            return MapMultivector((IGasMultivector<T>) storage);
        }

        public IGasMultivector<T> MapMultivector(IGasTermsMultivector<T> storage)
        {
            return MapMultivector((IGasMultivector<T>) storage);
        }

        public IGasMultivector<T> MapMultivector(IGasMultivector<T> storage)
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

                simpleRotorsArray[i] = 
                    GaEuclideanSimpleRotor<T>.Create(Processor, rotor);
            }

            return GaEuclideanSimpleRotorsSequence<T>.Create(
                Processor,
                simpleRotorsArray
            );
        }
    }
}