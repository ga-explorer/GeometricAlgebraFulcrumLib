using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Utils;

namespace GeometricAlgebraFulcrumLib.Geometry.Rotors
{
    /// <summary>
    /// A pure rotor is the exponential of a 2-blade. The geometric product of
    /// the rotor with its reverse is one. The squared norm of the 2-blade could either
    /// be positive, zero, or negative. Each case has its own formulation for the exponential
    /// See Section 7.4 of "Geometric Algebra for Computer Science"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaPureRotor<T>
        : IGaGeometry<T>, IGaRotor<T>
    {
        public IGaSpace Space => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public ulong MaxBasisBladeId 
            => Processor.MaxBasisBladeId;

        public uint GradesCount 
            => Processor.GradesCount;

        public IEnumerable<uint> Grades 
            => Processor.Grades;

        public IGaScalarsGridProcessor<T> ScalarsGridProcessor 
            => Processor;

        public IGaStorageKVector<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IGaStorageMultivector<T> Multivector { get; }

        public IGaStorageMultivector<T> MultivectorReverse { get; }

        public bool IsValid
        {
            get
            {
                // Make sure the storage and its reverse are correct
                if (!Processor.IsNearZero(Processor.Subtract(Processor.Reverse(Multivector), MultivectorReverse)))
                    return false;

                // Make sure storage contains only terms of grades 0,2
                if ((Multivector.GetStoredGradesBitPattern() | 5UL) != 5UL)
                    return false;

                // Make sure storage gp reverse(storage) == 1
                var gp = 
                    Processor.EGp(Multivector, MultivectorReverse);

                if (!gp.IsScalar())
                    return false;

                var diff =
                    Processor.Subtract(
                        Processor.GetTermScalar(gp, 0),
                        Processor.GetOneScalar()
                    );

                if (!Processor.IsNearZero(diff))
                    return false;

                return true;
            }
        }

        public bool IsInvalid 
            => !IsValid;


        //internal GaPureRotor([NotNull] IGaProcessor<T> processor, [NotNull] IGasMultivector<T> storage)
        //{
        //    Processor = processor;
        //    Rotor = storage;
        //    RotorReverse = Rotor.GetReverse();
        //    MappedPseudoScalar = Processor.CreatePseudoScalar(VSpaceDimension);
        //}

        internal GaPureRotor([NotNull] IGaProcessor<T> processor, [NotNull] T scalarPart, [NotNull] IGaStorageBivector<T> bivectorPart)
        {
            Processor = processor;
            Multivector = processor.Add(scalarPart, bivectorPart);
            MultivectorReverse = Processor.Subtract(scalarPart, bivectorPart);
            MappedPseudoScalar = Processor.CreateStoragePseudoScalar(VSpaceDimension);
        }

        internal GaPureRotor([NotNull] IGaProcessor<T> processor, [NotNull] IGaStorageMultivector<T> multivector, [NotNull] IGaStorageMultivector<T> multivectorReverse)
        {
            Processor = processor;
            Multivector = multivector;
            MultivectorReverse = multivectorReverse;
            MappedPseudoScalar = Processor.CreateStoragePseudoScalar(VSpaceDimension);
        }


        public GaVector<T> Map(GaVector<T> vector)
        {
            return Processor.CreateGenericVector(
                MapVector(vector.VectorStorage)
            );
        }

        public GaPureRotor<T> GetReverseRotor()
        {
            return new GaPureRotor<T>(
                Processor, 
                MultivectorReverse, 
                Multivector
            );
        }
        

        public IGaOutermorphism<T> GetAdjoint()
        {
            return new GaPureRotor<T>(
                Processor,
                MultivectorReverse, 
                Multivector
            );
        }

        public IGaStorageVector<T> MapBasisVector(int index)
        {
            return MapVector(
                Processor.CreateStorageVector(index, 
                    Processor.GetOneScalar()
                )
            );
        }

        public IReadOnlyList<IGaStorageVector<T>> GetMappedBasisVectors()
        {
            return ((ulong) VSpaceDimension)
                .GetRange()
                .Select(MapBasisVector)
                .ToArray();
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
            return MapTerm(
                Processor.CreateStorageBasisBlade(id)
            );
        }

        public IGaStorageKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            return MapTerm(
                Processor.CreateStorageBasisBlade(grade, index)
            );
        }

        public IGaStorageScalar<T> MapScalar(IGaStorageScalar<T> storage)
        {
            return storage;
        }

        public IGaStorageKVector<T> MapTerm(IGaStorageKVector<T> storage)
        {
            return Processor
                .Gp(Multivector, storage, MultivectorReverse)
                .GetKVectorPart(storage.Grade);
        }

        public IGaStorageVector<T> MapVector(IGaStorageVector<T> storage)
        {
            return Processor
                .Gp(Multivector, storage, MultivectorReverse)
                .GetVectorPart();
        }

        public IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> storage)
        {
            return Processor
                .Gp(Multivector, storage, MultivectorReverse)
                .GetBivectorPart();
        }

        public IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> storage)
        {
            return Processor
                .Gp(Multivector, storage, MultivectorReverse)
                .GetKVectorPart(storage.Grade);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivectorGraded<T> storage)
        {
            return Processor.Gp(Multivector, storage, MultivectorReverse);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivectorSparse<T> storage)
        {
            return Processor.Gp(Multivector, storage, MultivectorReverse);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> storage)
        {
            return Processor.Gp(Multivector, storage, MultivectorReverse);
        }
    }
}