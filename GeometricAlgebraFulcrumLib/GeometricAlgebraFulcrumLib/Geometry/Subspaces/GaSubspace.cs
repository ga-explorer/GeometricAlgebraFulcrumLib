using System;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public sealed class GeoSubspace<T> 
        : IGeoSubspace<T>
    {
        public static GeoSubspace<T> Create(IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> bladeStorage)
        {
            return new GeoSubspace<T>(processor, bladeStorage);
        }

        public static GeoSubspace<T> CreateFromPseudoScalar(IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            return new GeoSubspace<T>(
                processor,
                processor.CreatePseudoScalarStorage(vSpaceDimension
                )
            );
        }


        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public uint SubspaceDimension 
            => Blade.Grade;

        public KVectorStorage<T> Blade { get; }

        public KVectorStorage<T> BladeInverse { get; }

        public T BladeScalarProductSquared { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GeoSubspace([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] KVectorStorage<T> bladeStorage)
        {
            GeometricProcessor = processor;
            Blade = bladeStorage;
            BladeScalarProductSquared = processor.ESp(bladeStorage);
            BladeInverse = 
                GeometricProcessor
                    .Divide(bladeStorage, BladeScalarProductSquared)
                    .GetKVectorPart(bladeStorage.Grade);
        }


        public IMultivectorStorage<T> Project(IMultivectorStorage<T> storage)
        {
            return GeometricProcessor.ELcp(GeometricProcessor.ELcp(storage, Blade), BladeInverse);
        }

        public IMultivectorStorage<T> Reflect(IMultivectorStorage<T> mv)
        {
            //TODO: Implement all cases in table 7.1 page 201 in "Geometric Algebra for Computer Science"
            return GeometricProcessor.EGp(GeometricProcessor.EGp(Blade, GeometricProcessor.GradeInvolution(mv)), GeometricProcessor.EBladeInverse(Blade));
        }

        public IMultivectorStorage<T> Rotate(IMultivectorStorage<T> mv)
        {
            if (Blade.Grade.IsOdd())
                throw new InvalidOperationException();

            //Debug.Assert(ScalarProcessor.IsOne(BladeScalarProductSquared));

            var rotatedMv =
                GeometricProcessor.Gp(
                    Blade,
                    GeometricProcessor.Gp(mv, Blade)
                );

            return Blade.Grade.GradeHasNegativeReverse()
                ? GeometricProcessor.Negative(rotatedMv)
                : rotatedMv;
        }

        public IMultivectorStorage<T> VersorProduct(IMultivectorStorage<T> mv)
        {
            return GeometricProcessor.Gp(
                Blade,
                GeometricProcessor.Gp(mv, BladeInverse)
            );
        }
        
        public IMultivectorStorage<T> Complement(IMultivectorStorage<T> storage)
        {
            return GeometricProcessor.ELcp(storage, GeometricProcessor.EBladeInverse(Blade));
        }
    }
}