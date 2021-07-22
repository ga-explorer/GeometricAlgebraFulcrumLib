using System;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public sealed class GaSubspace<T> 
        : IGaSubspace<T>
    {
        public static GaSubspace<T> Create(IGaProcessor<T> processor, IGasKVector<T> bladeStorage)
        {
            return new GaSubspace<T>(processor, bladeStorage);
        }

        public static GaSubspace<T> CreateFromPseudoScalar(IGaProcessor<T> processor, uint vSpaceDimension)
        {
            return new GaSubspace<T>(
                processor,
                processor.CreatePseudoScalar(vSpaceDimension
                )
            );
        }


        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGaScalarProcessor<T> ScalarProcessor
            => Blade.ScalarProcessor;

        public uint SubspaceDimension 
            => Blade.Grade;

        public IGasKVector<T> Blade { get; }

        public IGasKVector<T> BladeInverse { get; }

        public T BladeScalarProductSquared { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GaSubspace([NotNull] IGaProcessor<T> processor, [NotNull] IGasKVector<T> bladeStorage)
        {
            Processor = processor;
            Blade = bladeStorage;
            BladeScalarProductSquared = bladeStorage.ESp();
            BladeInverse = 
                bladeStorage
                    .Divide(BladeScalarProductSquared)
                    .GetKVectorPart(bladeStorage.Grade);
        }


        public IGasMultivector<T> Project(IGasMultivector<T> storage)
        {
            return storage.ELcp(Blade).ELcp(BladeInverse);
        }

        public IGasMultivector<T> Reflect(IGasMultivector<T> mv)
        {
            //TODO: Implement all cases in table 7.1 page 201 in "Geometric Algebra for Computer Science"
            return Blade.EGp(mv.GetGradeInvolution()).EGp(Blade.EBladeInverse());
        }

        public IGasMultivector<T> Rotate([NotNull] IGasMultivector<T> mv)
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
                ? rotatedMv.GetNegative()
                : rotatedMv;
        }

        public IGasMultivector<T> VersorProduct(IGasMultivector<T> mv)
        {
            return Processor.Gp(
                Blade,
                Processor.Gp(mv, BladeInverse)
            );
        }
        
        public IGasMultivector<T> Complement(IGasMultivector<T> storage)
        {
            return storage.ELcp(Blade.EBladeInverse());
        }
    }
}