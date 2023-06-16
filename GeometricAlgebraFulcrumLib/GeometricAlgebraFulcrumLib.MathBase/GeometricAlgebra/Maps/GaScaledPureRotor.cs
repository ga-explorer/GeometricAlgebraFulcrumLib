using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Maps
{
    public class GaScaledPureRotor :
        IGeometricElement
    {
        public RGaFloat64Processor Processor 
            => Multivector.Processor;

        public double ScalingFactor { get; }

        public RGaFloat64Multivector Multivector { get; }

        public RGaFloat64Multivector MultivectorReverse { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaScaledPureRotor(RGaFloat64Multivector multivector)
        {
            Multivector = multivector;
            MultivectorReverse = multivector.Reverse();
            ScalingFactor = Multivector.Sp(MultivectorReverse).ScalarValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaScaledPureRotor(RGaFloat64Multivector multivector, RGaFloat64Multivector multivectorReverse)
        {
            Multivector = multivector;
            MultivectorReverse = multivectorReverse;
            ScalingFactor = multivector.Sp(multivectorReverse).ScalarValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaScaledPureRotor(RGaFloat64Multivector multivector, RGaFloat64Multivector multivectorReverse, double scalingFactor)
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
            return Multivector.Sp(MultivectorReverse).ScalarValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScaledPureRotor GetPureRotor()
        {
            var normSquared = Multivector.Sp(MultivectorReverse).ScalarValue;

            var mv = Processor.IsEuclidean
                ? Multivector.Divide(normSquared.Sqrt())
                : Multivector.Divide(normSquared.SqrtOfAbs());

            return new GaScaledPureRotor(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScaledPureRotor GetPureScaledRotorInverse()
        {
            var scalingFactor = GetScalingFactor();
            
            return new GaScaledPureRotor(
                MultivectorReverse.Divide(scalingFactor),
                Multivector.Divide(scalingFactor)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D OmMap(IFloat64Tuple2D multivector)
        {
            return Multivector.Gp(multivector.ToRGaFloat64Vector(Processor)).Gp(MultivectorReverse).GetVectorPartAsTuple2D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D OmMap(IFloat64Tuple3D multivector)
        {
            return Multivector.Gp(multivector.ToRGaFloat64Vector(Processor)).Gp(MultivectorReverse).GetVectorPartAsTuple3D();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector)
        {
            return Multivector.Gp(multivector).Gp(MultivectorReverse);
        }
    }
}
