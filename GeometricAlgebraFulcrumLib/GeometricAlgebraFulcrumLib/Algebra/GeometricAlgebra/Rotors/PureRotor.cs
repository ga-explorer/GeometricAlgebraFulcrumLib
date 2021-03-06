using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    /// <summary>
    /// A pure rotor is the exponential of a 2-blade. The geometric product of
    /// the rotor with its reverse is one. The squared norm of the 2-blade could either
    /// be positive, zero, or negative. Each case has its own formulation for the exponential
    /// See Section 7.4 of "Geometric Algebra for Computer Science"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PureRotor<T>
        : RotorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> Create(T scalarPart, Bivector<T> bivectorPart)
        {
            return new PureRotor<T>(
                scalarPart + bivectorPart,
                scalarPart - bivectorPart
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> Create(Multivector<T> multivector)
        {
            return new PureRotor<T>(
                multivector,
                multivector.Reverse()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Multivector<T>(PureRotor<T> rotor)
        {
            return rotor.Multivector;
        }


        public Multivector<T> Multivector { get; }

        public Multivector<T> MultivectorReverse { get; }


        private PureRotor([NotNull] T scalarPart, [NotNull] Bivector<T> bivectorPart)
            : base(bivectorPart.GeometricProcessor)
        {
            Multivector = scalarPart + bivectorPart;
            MultivectorReverse = scalarPart - bivectorPart;
        }
        
        private PureRotor([NotNull] Multivector<T> multivector, [NotNull] Multivector<T> multivectorReverse)
            : base(multivector.GeometricProcessor)
        {
            Multivector = multivector;
            MultivectorReverse = multivectorReverse;
        }

        private PureRotor([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] IMultivectorStorage<T> multivector, [NotNull] IMultivectorStorage<T> multivectorReverse)
            : base(processor)
        {
            Multivector = multivector.CreateMultivector(processor);
            MultivectorReverse = multivectorReverse.CreateMultivector(processor);
        }


        public override bool IsValid()
        {
            // Make sure the storage and its reverse are correct
            if (!GeometricProcessor.IsNearZero(GeometricProcessor.Subtract(GeometricProcessor.Reverse(Multivector.MultivectorStorage), MultivectorReverse.MultivectorStorage)))
                return false;

            // Make sure storage contains only terms of grades 0,2
            if ((Multivector.MultivectorStorage.GetStoredGradesBitPattern() | 5UL) != 5UL)
                return false;

            // Make sure storage gp reverse(storage) == 1
            var gp = 
                Multivector.Gp(MultivectorReverse);

            if (!gp.IsScalar())
                return false;

            var diff = gp[0] - 1;

            return diff.IsNearZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureRotor<T> GetPureRotorInverse()
        {
            return new PureRotor<T>(
                GeometricProcessor, 
                MultivectorReverse.MultivectorStorage, 
                Multivector.MultivectorStorage
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRotor<T> GetRotorInverse()
        {
            return GetPureRotorInverse();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector<T> OmMap(Vector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Bivector<T> OmMap(Bivector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVector<T> OmMap(KVector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> OmMap(Multivector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivector()
        {
            return Multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivectorReverse()
        {
            return MultivectorReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivectorInverse()
        {
            return MultivectorReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorage()
        {
            return Multivector.MultivectorStorage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageReverse()
        {
            return MultivectorReverse.MultivectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageInverse()
        {
            return MultivectorReverse.MultivectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalingFactor()
        {
            return ScalarProcessor.CreateScalarOne();
        }
    }
}