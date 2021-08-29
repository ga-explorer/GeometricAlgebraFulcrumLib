using System;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public sealed class GaSubspace<T> 
        : IGaSubspace<T>
    {
        public static GaSubspace<T> Create(IGaProcessor<T> processor, IGaKVectorStorage<T> bladeStorage)
        {
            return new GaSubspace<T>(processor, bladeStorage);
        }

        public static GaSubspace<T> CreateFromPseudoScalar(IGaProcessor<T> processor, uint vSpaceDimension)
        {
            return new GaSubspace<T>(
                processor,
                processor.CreatePseudoScalarStorage(vSpaceDimension
                )
            );
        }


        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public uint SubspaceDimension 
            => Blade.Grade;

        public IGaKVectorStorage<T> Blade { get; }

        public IGaKVectorStorage<T> BladeInverse { get; }

        public T BladeScalarProductSquared { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GaSubspace([NotNull] IGaProcessor<T> processor, [NotNull] IGaKVectorStorage<T> bladeStorage)
        {
            Processor = processor;
            Blade = bladeStorage;
            BladeScalarProductSquared = processor.ESp(bladeStorage);
            BladeInverse = 
                Processor
                    .Divide(bladeStorage, BladeScalarProductSquared)
                    .GetKVectorPart(bladeStorage.Grade);
        }


        public IGaMultivectorStorage<T> Project(IGaMultivectorStorage<T> storage)
        {
            return Processor.ELcp(Processor.ELcp(storage, Blade), BladeInverse);
        }

        public IGaMultivectorStorage<T> Reflect(IGaMultivectorStorage<T> mv)
        {
            //TODO: Implement all cases in table 7.1 page 201 in "Geometric Algebra for Computer Science"
            return Processor.EGp(Processor.EGp(Blade, Processor.GradeInvolution(mv)), Processor.EBladeInverse(Blade));
        }

        public IGaMultivectorStorage<T> Rotate(IGaMultivectorStorage<T> mv)
        {
            if (Blade.Grade.IsOdd())
                throw new InvalidOperationException();

            //Debug.Assert(ScalarProcessor.IsOne(BladeScalarProductSquared));

            var rotatedMv =
                Processor.Gp(
                    Blade,
                    Processor.Gp(mv, Blade)
                );

            return Blade.Grade.GradeHasNegativeReverse()
                ? Processor.Negative(rotatedMv)
                : rotatedMv;
        }

        public IGaMultivectorStorage<T> VersorProduct(IGaMultivectorStorage<T> mv)
        {
            return Processor.Gp(
                Blade,
                Processor.Gp(mv, BladeInverse)
            );
        }
        
        public IGaMultivectorStorage<T> Complement(IGaMultivectorStorage<T> storage)
        {
            return Processor.ELcp(storage, Processor.EBladeInverse(Blade));
        }
    }
}