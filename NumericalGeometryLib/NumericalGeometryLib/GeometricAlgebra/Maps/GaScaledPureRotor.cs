using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.Multivectors;

namespace NumericalGeometryLib.GeometricAlgebra.Maps
{
    public class GaScaledPureRotor :
        IGeometricElement
    {
        public BasisBladeSet BasisSet 
            => Multivector.BasisSet;

        public double ScalingFactor { get; }

        public GaMultivector Multivector { get; }

        public GaMultivector MultivectorReverse { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaScaledPureRotor([NotNull] GaMultivector multivector)
        {
            Multivector = multivector;
            MultivectorReverse = multivector.Reverse();
            ScalingFactor = Multivector.Sp(MultivectorReverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaScaledPureRotor([NotNull] GaMultivector multivector, [NotNull] GaMultivector multivectorReverse)
        {
            Multivector = multivector;
            MultivectorReverse = multivectorReverse;
            ScalingFactor = multivector.Sp(multivectorReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaScaledPureRotor([NotNull] GaMultivector multivector, [NotNull] GaMultivector multivectorReverse, double scalingFactor)
        {
            Multivector = multivector;
            MultivectorReverse = multivectorReverse;
            ScalingFactor = scalingFactor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Multivector.IsValid() &&
                   MultivectorReverse.IsValid() &&
                   Multivector.Gp(MultivectorReverse).IsScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalingFactor()
        {
            return Multivector.Sp(MultivectorReverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScaledPureRotor GetPureRotor()
        {
            var mv = BasisSet.IsEuclidean
                ? Multivector / Multivector.Sp(MultivectorReverse).Sqrt()
                : Multivector / Multivector.Sp(MultivectorReverse).SqrtOfAbs();

            return new GaScaledPureRotor(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScaledPureRotor GetPureScaledRotorInverse()
        {
            var scalingFactor = GetScalingFactor();
            
            return new GaScaledPureRotor(
                MultivectorReverse / scalingFactor,
                Multivector / scalingFactor
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple2D OmMap(IFloat64Tuple2D multivector)
        {
            return Multivector.Gp(multivector).Gp(MultivectorReverse).GetVectorPartAsTuple2D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D OmMap(IFloat64Tuple3D multivector)
        {
            return Multivector.Gp(multivector).Gp(MultivectorReverse).GetVectorPartAsTuple3D();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector OmMap(GaTerm multivector)
        {
            return Multivector.Gp(multivector).Gp(MultivectorReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector OmMap(GaMultivector multivector)
        {
            return Multivector.Gp(multivector).Gp(MultivectorReverse);
        }
    }
}
