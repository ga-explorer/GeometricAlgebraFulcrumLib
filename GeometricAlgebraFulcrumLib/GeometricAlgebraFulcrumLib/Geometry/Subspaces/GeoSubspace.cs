using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public sealed class GeoSubspace<T> 
        : IGeoSubspace<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreateDirect(IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> bladeStorage)
        {
            return new GeoSubspace<T>(processor, bladeStorage, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreateDual(IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> bladeStorage)
        {
            return new GeoSubspace<T>(processor, bladeStorage, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreateFromPseudoScalar(IGeometricAlgebraProcessor<T> processor)
        {
            return new GeoSubspace<T>(
                processor,
                processor.CreatePseudoScalarStorage(),
                true
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreateFromPseudoScalar(IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            return new GeoSubspace<T>(
                processor,
                processor.CreatePseudoScalarStorage(vSpaceDimension),
                true
            );
        }


        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public uint SubspaceDimension 
            => Blade.Grade;

        public bool IsDirect { get; }

        public bool IsDual 
            => !IsDirect;

        public KVectorStorage<T> Blade { get; }

        public KVectorStorage<T> BladeInverse { get; }

        public T BladeSignature { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GeoSubspace([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] KVectorStorage<T> bladeStorage, bool isDirect)
        {
            GeometricProcessor = processor;

            Blade = bladeStorage;
            BladeSignature = processor.Sp(bladeStorage);
            BladeInverse = GeometricProcessor.Divide(bladeStorage, BladeSignature);

            IsDirect = isDirect;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoSubspace<T> Project(IGeoSubspace<T> subspace)
        {
            var blade = 
                GeometricProcessor.Lcp(
                    GeometricProcessor.Lcp(subspace.Blade, Blade), 
                    BladeInverse
                );

            return new GeoSubspace<T>(GeometricProcessor, blade, subspace.IsDirect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoSubspace<T> Reflect(IGeoSubspace<T> subspace)
        {
            //TODO: Implement all cases in table 7.1 page 201 in "Geometric Algebra for Computer Science"
            var blade = 
                GeometricProcessor.Gp(
                    Blade, 
                    GeometricProcessor.GradeInvolution(subspace.Blade), 
                    BladeInverse
                ).GetKVectorPart(subspace.Blade.Grade);

            return new GeoSubspace<T>(GeometricProcessor, blade, subspace.IsDirect);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public IGeoSubspace<T> Rotate(IGeoSubspace<T> subspace)
        //{
        //    if (Blade.Grade.IsOdd())
        //        throw new InvalidOperationException();

        //    //Debug.Assert(ScalarProcessor.IsOne(BladeSignature));

        //    var rotatedMv =
        //        GeometricProcessor.Gp(
        //            Blade,
        //            subspace.Blade, 
        //            BladeInverse
        //        ).GetKVectorPart(subspace.Blade.Grade);

        //    var blade = Blade.Grade.GradeHasNegativeReverse()
        //        ? GeometricProcessor.Negative(rotatedMv)
        //        : rotatedMv;

        //    return new GeoSubspace<T>(GeometricProcessor, blade, subspace.IsDirect);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoSubspace<T> VersorProduct(IGeoSubspace<T> subspace)
        {
            var blade = GeometricProcessor.Gp(
                Blade,
                subspace.Blade, 
                BladeInverse
            ).GetKVectorPart(subspace.Blade.Grade);

            return new GeoSubspace<T>(GeometricProcessor, blade, subspace.IsDirect);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoSubspace<T> Complement(IGeoSubspace<T> subspace)
        {
            if (subspace.SubspaceDimension > SubspaceDimension)
                throw new InvalidOperationException();

            var blade = GeometricProcessor.Lcp(
                subspace.Blade, 
                Blade
            );

            return new GeoSubspace<T>(GeometricProcessor, blade, subspace.IsDirect);
        }
    }
}