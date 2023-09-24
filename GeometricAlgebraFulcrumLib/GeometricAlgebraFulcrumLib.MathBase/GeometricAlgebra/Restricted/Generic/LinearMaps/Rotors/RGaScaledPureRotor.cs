using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors
{
    /// <summary>
    /// A pure rotor is the exponential of a 2-blade. The geometric product of
    /// the rotor with its reverse is one. The squared norm of the 2-blade could either
    /// be positive, zero, or negative. Each case has its own formulation for the exponential
    /// See Section 7.4 of "Geometric Algebra for Computer Science"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RGaScaledPureRotor<T>
        : RGaScaledRotorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaScaledPureRotor<T> CreateIdentity(RGaProcessor<T> processor)
        {
            return new RGaScaledPureRotor<T>(processor, processor.ScalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaScaledPureRotor<T> Create(RGaProcessor<T> processor, T scalarPart)
        {
            return new RGaScaledPureRotor<T>(processor, scalarPart);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaScaledPureRotor<T> Create(T scalarPart, RGaBivector<T> bivectorPart)
        {
            return new RGaScaledPureRotor<T>(
                scalarPart + bivectorPart,
                scalarPart - bivectorPart
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaScaledPureRotor<T> Create(RGaMultivector<T> multivector)
        {
            return new RGaScaledPureRotor<T>(
                multivector,
                multivector.Reverse()
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RGaMultivector<T>(RGaScaledPureRotor<T> rotor)
        {
            return rotor.Multivector;
        }


        public RGaMultivector<T> Multivector { get; }

        public RGaMultivector<T> MultivectorReverse { get; }


        private RGaScaledPureRotor(RGaProcessor<T> processor, [NotNull] T scalarPart)
            : base(processor)
        {
            Multivector = Processor.CreateScalar(scalarPart);
            MultivectorReverse = Multivector;
        }

        private RGaScaledPureRotor([NotNull] T scalarPart, RGaBivector<T> bivectorPart)
            : base(bivectorPart.Processor)
        {
            Multivector = scalarPart + bivectorPart;
            MultivectorReverse = scalarPart - bivectorPart;
        }

        private RGaScaledPureRotor(RGaMultivector<T> multivector, RGaMultivector<T> multivectorReverse)
            : base(multivector.Processor)
        {
            Multivector = multivector;
            MultivectorReverse = multivectorReverse;
        }


        public override bool IsValid()
        {
            // Make sure the storage and its reverse are correct
            if (!(Multivector.Reverse() - MultivectorReverse).IsNearZero())
                return false;

            // Make sure storage contains only terms of grades 0,2
            if (Multivector.GetMaxGrade() <= 2 && !Multivector.ContainsKVectorPart(1))
                return false;

            // Make sure storage gp reverse(storage) == scalar
            var gp = 
                Multivector.Gp(MultivectorReverse);

            return gp.IsScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScaledPureRotor<T> GetPureScaledRotorInverse()
        {
            var scalingFactor = GetScalingFactor();
            
            return new RGaScaledPureRotor<T>(
                MultivectorReverse / scalingFactor,
                Multivector / scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalingFactor()
        {
            return Multivector.Sp(MultivectorReverse).Scalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaPureRotor<T> GetPureRotor()
        {
            var mv = Processor.IsEuclidean
                ? Multivector / Multivector.Sp(MultivectorReverse).Sqrt()
                : Multivector / Multivector.Sp(MultivectorReverse).SqrtOfAbs();

            return RGaPureRotor<T>.Create(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaScaledRotor<T> GetScaledRotorInverse()
        {
            return GetPureScaledRotorInverse();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaVector<T> OmMap(RGaVector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaBivector<T> OmMap(RGaBivector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaHigherKVector<T> OmMap(RGaHigherKVector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetHigherKVectorPart(mv.Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> OmMap(RGaMultivector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse);
        }

        public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> GetMultivector()
        {
            return Multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> GetMultivectorReverse()
        {
            return MultivectorReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> GetMultivectorInverse()
        {
            return MultivectorReverse;
        }
        
    }
}