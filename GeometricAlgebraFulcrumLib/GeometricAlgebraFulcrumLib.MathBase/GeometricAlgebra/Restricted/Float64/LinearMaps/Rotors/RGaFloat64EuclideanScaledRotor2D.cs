using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors
{
    /// <summary>
    /// This class 
    /// </summary>
    public sealed class RGaFloat64EuclideanScaledRotor2D
        : RGaFloat64ScaledRotorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64EuclideanScaledRotor2D Create(RGaFloat64Processor metric, double scalar0)
        {
            return new RGaFloat64EuclideanScaledRotor2D(metric, scalar0, 0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64EuclideanScaledRotor2D Create(RGaFloat64Processor metric, double scalar0, double scalar12)
        {
            return new RGaFloat64EuclideanScaledRotor2D(metric, scalar0, scalar12);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RGaFloat64Multivector(RGaFloat64EuclideanScaledRotor2D rotor)
        {
            return rotor.GetMultivector();
        }


        public double Scalar0 { get; }

        public double Scalar12 { get; }


        private RGaFloat64EuclideanScaledRotor2D(RGaFloat64Processor metric, double scalar0, double scalar12)
            : base(metric)
        {
            Scalar0 = scalar0;
            Scalar12 = scalar12;
        }


        public override bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64EuclideanScaledRotor2D GetPureScaledRotor2DInverse()
        {
            var scalingFactor = GetScalingFactor();

            return new RGaFloat64EuclideanScaledRotor2D(
                Processor,
                Scalar0 / scalingFactor,
                -Scalar12 / scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalingFactor()
        {
            return Scalar0 * Scalar0 + Scalar12 * Scalar12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64EuclideanScaledRotor2D GetPureRotor()
        {
            var scalingFactor = 
                GetScalingFactor().Sqrt();

            return Create(
                Processor,
                Scalar0 / scalingFactor,
                Scalar12 / scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaFloat64ScaledRotor GetScaledRotorInverse()
        {
            return GetPureScaledRotor2DInverse();
        }

        public RGaFloat64Vector OmMap(double u1, double u2)
        {
            var s0 = Scalar0 * Scalar0 - Scalar12 * Scalar12;
            var s12 = 2 * Scalar0 * Scalar12;
            var v1 = s0 * u1 + s12 * u2;
            var v2 = s0 * u2 - s12 * u1;

            return Processor.CreateVector(v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector OmMap(RGaFloat64Vector mv)
        {
            return OmMap(mv[0], mv[1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector OmMap(RGaFloat64Bivector mv)
        {
            return GetScalingFactor() * mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector mv)
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector OmMap(RGaFloat64KVector mv)
        {
            return mv.Grade switch
            {
                0 or 2 => GetScalingFactor() * mv,
                1 => OmMap(mv[0], mv[1]),
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

            var s0 = Scalar0 * Scalar0 - Scalar12 * Scalar12;
            var s12 = 2 * Scalar0 * Scalar12;

            var scalingFactor = GetScalingFactor();

            var v0 = scalingFactor * mv0;
            var v1 = s0 * mv1 + s12 * mv2;
            var v2 = s0 * mv2 - s12 * mv1;
            var v12 = scalingFactor * mv12;

            return Processor
                .CreateComposer()
                .SetTerm(0, v0)
                .SetTerm(1, v1)
                .SetTerm(2, v2)
                .SetTerm(3, v12)
                .GetSimpleMultivector();
        }

        public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public EuclideanScaledRotor2D GpSquared()
        //{
        //    var s0 = ScalarProcessor.Subtract(
        //        ScalarProcessor.Times(Scalar0, Scalar0),
        //        ScalarProcessor.Times(Scalar12, Scalar12)
        //    );

        //    var s12 = ScalarProcessor.Times(
        //        ScalarProcessor.ScalarTwo,
        //        ScalarProcessor.Times(Scalar0, Scalar12)
        //    );

        //    return new EuclideanScaledRotor2D(GeometricProcessor, s0, s12);
        //}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64EuclideanScaledRotor2D Gp(RGaFloat64EuclideanScaledRotor2D rotor2)
        {
            var s0 = Scalar0 * rotor2.Scalar0 - Scalar12 * rotor2.Scalar12;
            var s12 = Scalar0 * rotor2.Scalar12 + Scalar12 * rotor2.Scalar0;

            return new RGaFloat64EuclideanScaledRotor2D(Processor, s0, s12);
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

        //    return GeometricProcessor.CreateVectorStorage(v1, v2);
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivector()
        {
            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0)
                .SetTerm(3, Scalar12)
                .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorReverse()
        {
            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0)
                .SetTerm(3, -(Scalar12))
                .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorInverse()
        {
            var scalingFactor = GetScalingFactor();

            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0 / scalingFactor)
                .SetTerm(3, -Scalar12 / scalingFactor)
                .GetSimpleMultivector();
        }
    }
}