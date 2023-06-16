using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors
{
    public sealed class XGaEuclideanScaledRotorSquared2D<T>
        : XGaScaledRotorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static XGaEuclideanScaledRotorSquared2D<T> Create(XGaProcessor<T> processor, Scalar<T> scalar0)
        {
            return new XGaEuclideanScaledRotorSquared2D<T>(processor, scalar0, processor.ScalarProcessor.CreateScalarZero());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static XGaEuclideanScaledRotorSquared2D<T> Create(XGaProcessor<T> processor, Scalar<T> scalar0, Scalar<T> scalar12)
        {
            return new XGaEuclideanScaledRotorSquared2D<T>(processor, scalar0, scalar12);
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator Multivector<T>(ScaledPureRotor<T> rotor)
        //{
        //    return rotor.Multivector;
        //}


        public Scalar<T> Scalar0 { get; }

        public Scalar<T> Scalar12 { get; }


        private XGaEuclideanScaledRotorSquared2D(XGaProcessor<T> processor, T scalar0, T scalar12)
            : base(processor)
        {
            Scalar0 = scalar0.CreateScalar(processor.ScalarProcessor);
            Scalar12 = scalar12.CreateScalar(processor.ScalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaEuclideanScaledRotorSquared2D<T> GetPureScaledRotorSquared2DInverse()
        {
            var scalingFactorSquared = GetScalingFactorSquared();

            return new XGaEuclideanScaledRotorSquared2D<T>(
                Processor,
                Scalar0 / scalingFactorSquared,
                -Scalar12 / scalingFactorSquared
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalingFactor()
        {
            return GetScalingFactorSquared().Sqrt();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetScalingFactorSquared()
        {
            return Scalar0 * Scalar0 + Scalar12 * Scalar12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaEuclideanScaledRotorSquared2D<T> GetPureRotorSquared2D()
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
        public override IXGaScaledRotor<T> GetScaledRotorInverse()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> OmMap(T u1, T u2)
        {
            var v1 = Scalar0 * u1 + Scalar12 * u2;
            var v2 = Scalar0 * u2 - Scalar12 * u1;

            return Processor.CreateVector(v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> OmMap(XGaVector<T> mv)
        {
            var u1 = mv[0];
            var u2 = mv[1];

            return OmMap(u1, u2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> OmMap(XGaBivector<T> mv)
        {
            return GetScalingFactor() * mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> OmMap(XGaKVector<T> mv)
        {
            return mv.Grade switch
            {
                0 or 2 => GetScalingFactor() * mv,
                1 => OmMap(mv[0], mv[1]).AsKVector(),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> OmMap(XGaMultivector<T> mv)
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

            return Processor.CreateMultivector2D(
                v0, 
                v1, 
                v2, 
                v12
            );
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public EuclideanScaledRotorSquared2D<T> GpSquared()
        //{
        //    var s0 = ScalarProcessor.Subtract(
        //        ScalarProcessor.Times(Scalar0, Scalar0),
        //        ScalarProcessor.Times(Scalar12, Scalar12)
        //    );

        //    var s12 = ScalarProcessor.Times(
        //        ScalarProcessor.ScalarTwo,
        //        ScalarProcessor.Times(Scalar0, Scalar12)
        //    );

        //    return new EuclideanScaledRotorSquared2D<T>(ScalarProcessor, s0, s12);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaEuclideanScaledRotorSquared2D<T> Gp(XGaEuclideanScaledRotorSquared2D<T> rotor2)
        {
            var s0 = Scalar0 * rotor2.Scalar0 - Scalar12 * rotor2.Scalar12;
            var s12 = Scalar0 * rotor2.Scalar12 + Scalar12 * rotor2.Scalar0;

            return new XGaEuclideanScaledRotorSquared2D<T>(Processor, s0, s12);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public VectorStorage<T> Gp(VectorStorage<T> vector)
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
        public override XGaMultivector<T> GetMultivector()
        {
            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0)
                .SetTerm(3, Scalar12)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivectorReverse()
        {
            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0)
                .SetTerm(3, -Scalar12)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivectorInverse()
        {
            var scalingFactor = GetScalingFactor().Inverse();

            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0 * scalingFactor)
                .SetTerm(3, -Scalar12 * scalingFactor)
                .GetMultivector();
        }
    }
}