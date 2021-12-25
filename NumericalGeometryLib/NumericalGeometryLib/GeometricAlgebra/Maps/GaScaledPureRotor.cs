using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.Multivectors;

namespace NumericalGeometryLib.GeometricAlgebra.Maps
{
    public class GaScaledPureRotor :
        IGeometricElement
    {
        public GaBasisSet BasisSet 
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
        public GaMultivector Rotate(GaMultivector multivector)
        {
            return Multivector.Gp(multivector).Gp(MultivectorReverse);
        }
    }
}
