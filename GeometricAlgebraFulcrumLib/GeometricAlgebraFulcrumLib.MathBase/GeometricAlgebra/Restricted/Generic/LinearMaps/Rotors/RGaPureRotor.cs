using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
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
    public sealed class RGaPureRotor<T>
        : RGaRotorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaPureRotor<T> Create(T scalarPart, RGaBivector<T> bivectorPart)
        {
            return new RGaPureRotor<T>(
                scalarPart + bivectorPart,
                scalarPart - bivectorPart
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaPureRotor<T> Create(RGaMultivector<T> multivector)
        {
            return new RGaPureRotor<T>(
                multivector,
                multivector.Reverse()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RGaMultivector<T>(RGaPureRotor<T> rotor)
        {
            return rotor.Multivector;
        }


        public RGaMultivector<T> Multivector { get; }

        public RGaMultivector<T> MultivectorReverse { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGaPureRotor([NotNull] T scalarPart, RGaBivector<T> bivectorPart)
            : base(bivectorPart.Processor)
        {
            Multivector = scalarPart + bivectorPart;
            MultivectorReverse = scalarPart - bivectorPart;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGaPureRotor(RGaMultivector<T> multivector, RGaMultivector<T> multivectorReverse)
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
            if (Multivector.IsEven(2))
                return false;

            // Make sure storage gp reverse(storage) == 1
            var gp = 
                Multivector.Gp(MultivectorReverse);

            if (!gp.IsScalar())
                return false;

            var diff = gp.Scalar() - 1;

            return diff.IsNearZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaPureRotor<T> GetPureRotorInverse()
        {
            return new RGaPureRotor<T>(
                MultivectorReverse, 
                Multivector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaRotor<T> GetRotorInverse()
        {
            return GetPureRotorInverse();
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalingFactor()
        {
            return ScalarProcessor.CreateScalarOne();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple<Scalar<T>, RGaBivector<T>> GetEuclideanAngleBivector()
        {
            var halfAngle = 
                Multivector.GetScalarPart().ArcCos();

            var angle = 
                2 * halfAngle;

            var bivector = 
                Multivector.GetBivectorPart() / halfAngle.Sin();

            return new Tuple<Scalar<T>, RGaBivector<T>>(
                angle.Scalar(),
                bivector
            );
        }
    }
}