using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors
{
    /// <summary>
    /// A pure rotor is the exponential of a 2-blade. The geometric product of
    /// the rotor with its reverse is one. The squared norm of the 2-blade could either
    /// be positive, zero, or negative. Each case has its own formulation for the exponential
    /// See Section 7.4 of "Geometric Algebra for Computer Science"
    /// </summary>
    public sealed class RGaFloat64ScaledPureRotor
        : RGaFloat64ScaledRotorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64ScaledPureRotor CreateIdentity(RGaFloat64Processor metric)
        {
            return new RGaFloat64ScaledPureRotor(metric, 1d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64ScaledPureRotor Create(RGaFloat64Processor metric, double scalarPart)
        {
            return new RGaFloat64ScaledPureRotor(metric, scalarPart);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64ScaledPureRotor Create(double scalarPart, RGaFloat64Bivector bivectorPart)
        {
            return new RGaFloat64ScaledPureRotor(
                scalarPart + bivectorPart,
                scalarPart - bivectorPart
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64ScaledPureRotor Create(RGaFloat64Multivector multivector)
        {
            return new RGaFloat64ScaledPureRotor(
                multivector,
                multivector.Reverse()
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RGaFloat64Multivector(RGaFloat64ScaledPureRotor rotor)
        {
            return rotor.Multivector;
        }


        public RGaFloat64Multivector Multivector { get; }

        public RGaFloat64Multivector MultivectorReverse { get; }


        private RGaFloat64ScaledPureRotor(RGaFloat64Processor metric, double scalarPart)
            : base(metric)
        {
            Multivector = Processor.CreateScalar(scalarPart);
            MultivectorReverse = Multivector;
        }

        private RGaFloat64ScaledPureRotor(double scalarPart, RGaFloat64Bivector bivectorPart)
            : base(bivectorPart.Processor)
        {
            Multivector = scalarPart + bivectorPart;
            MultivectorReverse = scalarPart - bivectorPart;
        }

        private RGaFloat64ScaledPureRotor(RGaFloat64Multivector multivector, RGaFloat64Multivector multivectorReverse)
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
        public RGaFloat64ScaledPureRotor GetPureScaledRotorInverse()
        {
            var scalingFactor = GetScalingFactor();
            
            return new RGaFloat64ScaledPureRotor(
                MultivectorReverse / scalingFactor,
                Multivector / scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalingFactor()
        {
            return Multivector.Sp(MultivectorReverse).Scalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64PureRotor GetPureRotor()
        {
            var mv = Metric.IsEuclidean
                ? Multivector / Multivector.Sp(MultivectorReverse).Sqrt()
                : Multivector / Multivector.Sp(MultivectorReverse).SqrtOfAbs();

            return RGaFloat64PureRotor.Create(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaFloat64ScaledRotor GetScaledRotorInverse()
        {
            return GetPureScaledRotorInverse();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector OmMap(RGaFloat64Vector mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector OmMap(RGaFloat64Bivector mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetHigherKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector OmMap(RGaFloat64KVector mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector OmMap(RGaFloat64Multivector mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse);
        }

        public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivector()
        {
            return Multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorReverse()
        {
            return MultivectorReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorInverse()
        {
            return MultivectorReverse;
        }
        
    }
}