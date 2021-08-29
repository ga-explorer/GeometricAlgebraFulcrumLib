using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

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

        public ILaProcessor<T> ScalarsGridProcessor 
            => Processor;

        public IGaKVectorStorage<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IGaMultivectorStorage<T> Multivector { get; }

        public IGaMultivectorStorage<T> MultivectorReverse { get; }

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
                        Processor.ScalarOne
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

        internal GaPureRotor([NotNull] IGaProcessor<T> processor, [NotNull] T scalarPart, [NotNull] IGaBivectorStorage<T> bivectorPart)
        {
            Processor = processor;
            Multivector = processor.Add(scalarPart, bivectorPart);
            MultivectorReverse = Processor.Subtract(scalarPart, bivectorPart);
            MappedPseudoScalar = Processor.CreatePseudoScalarStorage(VSpaceDimension);
        }

        internal GaPureRotor([NotNull] IGaProcessor<T> processor, [NotNull] IGaMultivectorStorage<T> multivector, [NotNull] IGaMultivectorStorage<T> multivectorReverse)
        {
            Processor = processor;
            Multivector = multivector;
            MultivectorReverse = multivectorReverse;
            MappedPseudoScalar = Processor.CreatePseudoScalarStorage(VSpaceDimension);
        }


        public GaVector<T> Map(GaVector<T> vector)
        {
            return Processor.CreateGaVector(
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

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            return MapVector(
                Processor.CreateGaVectorStorage(index, 
                    Processor.ScalarOne
                )
            );
        }

        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            return ((ulong) VSpaceDimension)
                .GetRange()
                .Select(MapBasisVector)
                .ToArray();
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
            return MapTerm(
                Processor.CreateKVectorStorage(id)
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            return MapTerm(
                Processor.CreateKVectorStorage(grade, index)
            );
        }

        public IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> storage)
        {
            return storage;
        }

        public IGaKVectorStorage<T> MapTerm(IGaKVectorStorage<T> storage)
        {
            return Processor
                .Gp(Multivector, storage, MultivectorReverse)
                .GetKVectorPart(storage.Grade);
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> storage)
        {
            return Processor
                .Gp(Multivector, storage, MultivectorReverse)
                .GetVectorPart();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> storage)
        {
            return Processor
                .Gp(Multivector, storage, MultivectorReverse)
                .GetBivectorPart();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> storage)
        {
            return Processor
                .Gp(Multivector, storage, MultivectorReverse)
                .GetKVectorPart(storage.Grade);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> storage)
        {
            return Processor.Gp(Multivector, storage, MultivectorReverse);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorSparseStorage<T> storage)
        {
            return Processor.Gp(Multivector, storage, MultivectorReverse);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> storage)
        {
            return Processor.Gp(Multivector, storage, MultivectorReverse);
        }
    }
}