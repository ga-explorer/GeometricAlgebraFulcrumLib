using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors
{
    public sealed class RGaEuclideanScaledRotorSquared2D
        : RGaFloat64ScaledRotorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaEuclideanScaledRotorSquared2D Create(double scalar0)
        {
            return new RGaEuclideanScaledRotorSquared2D(scalar0, 0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaEuclideanScaledRotorSquared2D Create(double scalar0, double scalar12)
        {
            return new RGaEuclideanScaledRotorSquared2D(scalar0, scalar12);
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator Multivector(ScaledPureRotor rotor)
        //{
        //    return rotor.Multivector;
        //}


        public double Scalar0 { get; }

        public double Scalar12 { get; }


        private RGaEuclideanScaledRotorSquared2D(double scalar0, double scalar12)
            : base(RGaFloat64Processor.Euclidean)
        {
            Scalar0 = scalar0;
            Scalar12 = scalar12;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaEuclideanScaledRotorSquared2D GetPureScaledRotorSquared2DInverse()
        {
            var scalingFactorSquared = GetScalingFactorSquared();

            return new RGaEuclideanScaledRotorSquared2D(
                Scalar0 / scalingFactorSquared,
                -Scalar12 / scalingFactorSquared
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalingFactor()
        {
            return GetScalingFactorSquared().Sqrt();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalingFactorSquared()
        {
            return Scalar0 * Scalar0 + Scalar12 * Scalar12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaEuclideanScaledRotorSquared2D GetPureRotorSquared2D()
        {
            var scalingFactor = 
                GetScalingFactor();

            return Create(
                Scalar0 / scalingFactor,
                Scalar12 / scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaFloat64ScaledRotor GetScaledRotorInverse()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector OmMap(double u1, double u2)
        {
            var v1 = Scalar0 * u1 + Scalar12 * u2;
            var v2 = Scalar0 * u2 - Scalar12 * u1;

            return Processor.CreateVector(v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector OmMap(RGaFloat64Vector mv)
        {
            var u1 = mv[0];
            var u2 = mv[1];

            return OmMap(u1, u2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector OmMap(RGaFloat64Bivector mv)
        {
            return GetScalingFactor() * mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector OmMap(RGaFloat64KVector mv)
        {
            return mv.Grade switch
            {
                0 or 2 => GetScalingFactor() * mv,
                1 => OmMap(mv[0], mv[1]).AsKVector(),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector OmMap(RGaFloat64Multivector mv)
        {
            var mv0 = mv[0];
            var mv1 = mv[1];
            var mv2 = mv[2];
            var mv12 = mv[3];
            
            var scalingFactor = GetScalingFactor();

            var v0 = scalingFactor * mv0;
            var v1 = Scalar0 * mv1 + Scalar12 * mv2;
            var v2 = Scalar0 * mv2 - Scalar12 * mv1;
            var v12 = scalingFactor * mv12;

            return Processor.CreateMultivector2D(v0, v1, v2, v12);
        }

        public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public EuclideanScaledRotorSquared2D GpSquared()
        //{
        //    var s0 = ScalarProcessor.Subtract(
        //        ScalarProcessor.Times(Scalar0, Scalar0),
        //        ScalarProcessor.Times(Scalar12, Scalar12)
        //    );

        //    var s12 = ScalarProcessor.Times(
        //        ScalarProcessor.ScalarTwo,
        //        ScalarProcessor.Times(Scalar0, Scalar12)
        //    );

        //    return new EuclideanScaledRotorSquared2D(s0, s12);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaEuclideanScaledRotorSquared2D Gp(RGaEuclideanScaledRotorSquared2D rotor2)
        {
            var s0 = Scalar0 * rotor2.Scalar0 - Scalar12 * rotor2.Scalar12;
            var s12 = Scalar0 * rotor2.Scalar12 + Scalar12 * rotor2.Scalar0;

            return new RGaEuclideanScaledRotorSquared2D(s0, s12);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public VectorStorage Gp(VectorStorage vector)
        //{
        //    var u1 = ScalarProcessor.GetTermScalarByIndex(vector, 0);
        //    var u2 = ScalarProcessor.GetTermScalarByIndex(vector, 1);

        //    var v1 = ScalarProcessor.Add(
        //        ScalarProcessor.Times(Scalar0, u1),
        //        ScalarProcessor.Times(Scalar12, u2)
        //    );

        //    var v2 = ScalarProcessor.Subtract(
        //        ScalarProcessor.Times(Scalar0, u2),
        //        ScalarProcessor.Times(Scalar12, u1)
        //    );

        //    return ScalarProcessor.CreateVectorStorage(v1, v2);
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivector()
        {
            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0)
                .SetTerm(3, Scalar12)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorReverse()
        {
            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0)
                .SetTerm(3, -Scalar12)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorInverse()
        {
            var scalingFactor = 1d / GetScalingFactor();

            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0 * scalingFactor)
                .SetTerm(3, -Scalar12 * scalingFactor)
                .GetMultivector();
        }
    }
}