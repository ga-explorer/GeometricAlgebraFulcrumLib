using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors
{
    public sealed class XGaFloat64EuclideanScaledRotorSquared2D
        : XGaFloat64ScaledRotorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static XGaFloat64EuclideanScaledRotorSquared2D Create(XGaFloat64Processor processor, double scalar0)
        {
            return new XGaFloat64EuclideanScaledRotorSquared2D(processor, scalar0, 0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static XGaFloat64EuclideanScaledRotorSquared2D Create(XGaFloat64Processor processor, double scalar0, double scalar12)
        {
            return new XGaFloat64EuclideanScaledRotorSquared2D(processor, scalar0, scalar12);
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator Multivector(ScaledPureRotor rotor)
        //{
        //    return rotor.Multivector;
        //}


        public double Scalar0 { get; }

        public double Scalar12 { get; }


        private XGaFloat64EuclideanScaledRotorSquared2D(XGaFloat64Processor processor, double scalar0, double scalar12)
            : base(processor)
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
        public XGaFloat64EuclideanScaledRotorSquared2D GetPureScaledRotorSquared2DInverse()
        {
            var scalingFactorSquared = GetScalingFactorSquared();

            return new XGaFloat64EuclideanScaledRotorSquared2D(
                Processor,
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
        public XGaFloat64EuclideanScaledRotorSquared2D GetPureRotorSquared2D()
        {
            var scalingFactor = 
                GetScalingFactor();

            return Create(
                Processor,
                Scalar0 / scalingFactor,
                Scalar12 / scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IXGaFloat64ScaledRotor GetScaledRotorInverse()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector OmMap(double u1, double u2)
        {
            var v1 = Scalar0 * u1 + Scalar12 * u2;
            var v2 = Scalar0 * u2 - Scalar12 * u1;

            return Processor.CreateVector(v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
        {
            var u1 = mv[0];
            var u2 = mv[1];

            return OmMap(u1, u2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
        {
            return GetScalingFactor() * mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
        {
            throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
        {
            var mv0 = mv.Scalar();
            var mv1 = mv[0];
            var mv2 = mv[1];
            var mv12 = mv[0, 1];
            
            var scalingFactor = GetScalingFactor();

            var v0 = scalingFactor * mv0;
            var v1 = Scalar0 * mv1 + Scalar12 * mv2;
            var v2 = Scalar0 * mv2 - Scalar12 * mv1;
            var v12 = scalingFactor * mv12;

            return Processor.CreateMultivector2D(v0, v1, v2, v12);
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
        public XGaFloat64EuclideanScaledRotorSquared2D Gp(XGaFloat64EuclideanScaledRotorSquared2D rotor2)
        {
            var s0 = Scalar0 * rotor2.Scalar0 - Scalar12 * rotor2.Scalar12;
            var s12 = Scalar0 * rotor2.Scalar12 + Scalar12 * rotor2.Scalar0;

            return new XGaFloat64EuclideanScaledRotorSquared2D(Processor, s0, s12);
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
        public override XGaFloat64Multivector GetMultivector()
        {
            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0)
                .SetTerm(3, Scalar12)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetMultivectorReverse()
        {
            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0)
                .SetTerm(3, -Scalar12)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetMultivectorInverse()
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