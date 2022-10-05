using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public sealed class Subspace<T> 
        : ISubspace<T>
    {
        private readonly GaKVector<T> _blade;
        private readonly GaKVector<T> _bladeInverse;
        private readonly GaKVector<T> _bladePseudoInverse;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> Create(IGeometricAlgebraProcessor<T> processor, GaKVector<T> blade)
        {
            return new Subspace<T>(processor, blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateFromDual(IGeometricAlgebraProcessor<T> processor, GaKVector<T> blade)
        {
            return new Subspace<T>(
                processor, 
                blade.Dual()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateFromBasisVector(IGeometricAlgebraProcessor<T> processor, ulong index)
        {
            return new Subspace<T>(
                processor, 
                processor.CreateKVectorBasis(1, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateFromBasisBivector(IGeometricAlgebraProcessor<T> processor, ulong index)
        {
            return new Subspace<T>(
                processor, 
                processor.CreateKVectorBasis(2, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateFromBasisBivector(IGeometricAlgebraProcessor<T> processor, ulong index1, ulong index2)
        {
            return new Subspace<T>(
                processor, 
                processor.CreateBivectorBasis(index1, index2).AsKVector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateFromBasisBlade(IGeometricAlgebraProcessor<T> processor, ulong id)
        {
            return new Subspace<T>(
                processor, 
                processor.CreateKVectorBasis(id)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateFromBasisBlade(IGeometricAlgebraProcessor<T> processor, uint grade, ulong index)
        {
            return new Subspace<T>(
                processor, 
                processor.CreateKVectorBasis(grade, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateFromPseudoScalar(IGeometricAlgebraProcessor<T> processor)
        {
            return new Subspace<T>(
                processor,
                processor.CreateKVectorPseudoScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateFromPseudoScalar(IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            return new Subspace<T>(
                processor,
                processor.CreateKVectorPseudoScalar(vSpaceDimension)
            );
        }


        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public uint SubspaceDimension 
            => GetBlade().Grade;

        //public bool IsDirect { get; }

        //public bool IsDual 
        //    => !IsDirect;

        public GaKVector<T> GetBlade()
        {
            return _blade;
        }

        public GaKVector<T> GetBladeInverse()
        {
            return _bladeInverse;
        }

        public GaKVector<T> GetBladePseudoInverse()
        {
            return _bladePseudoInverse;
        }

        public Scalar<T> BladeSignature { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private Subspace([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] GaKVector<T> blade)
        {
            GeometricProcessor = processor;

            _blade = blade;
            BladeSignature = blade.SpSquared();
            _bladeInverse = blade / BladeSignature;
            _bladePseudoInverse = blade.PseudoInverse();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISubspace<T> Project(ISubspace<T> subspace)
        {
            var a = GetBlade();
            var aInv = GetBladePseudoInverse();
            var x = subspace.GetBlade();

            var blade =
                x.Lcp(aInv).Lcp(a);
            
            return new Subspace<T>(GeometricProcessor, blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISubspace<T> Reflect(ISubspace<T> subspace)
        {
            var a = GetBlade();
            var aInv = GetBladeInverse();
            var x = subspace.GetBlade();

            // This assumes this subspace and the reflected subspace are represented using
            // direct blades
            //TODO: Implement all cases in table 7.1 page 201 in "Geometric Algebra for Computer Science"
            var isNegative = (x.Grade * (a.Grade + 1)).IsOdd();

            var axa = a.Gp(x).Gp(aInv).GetKVectorPart(subspace.SubspaceDimension);

            var blade = 
               isNegative ? -axa : axa;

            return new Subspace<T>(GeometricProcessor, blade);
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
        public ISubspace<T> VersorProduct(ISubspace<T> subspace)
        {
            var a = GetBlade();
            var aInv = GetBladeInverse();
            var x = subspace.GetBlade();

            var subspaceBlade = subspace.GetBlade();

            var blade = 
                a.Gp(x).Gp(aInv).GetKVectorPart(subspaceBlade.Grade);

            return new Subspace<T>(GeometricProcessor, blade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISubspace<T> Complement(ISubspace<T> subspace)
        {
            if (subspace.SubspaceDimension > SubspaceDimension)
                throw new InvalidOperationException();

            var blade = subspace.GetBlade().Lcp(GetBladeInverse());

            return new Subspace<T>(GeometricProcessor, blade);
        }
    }
}