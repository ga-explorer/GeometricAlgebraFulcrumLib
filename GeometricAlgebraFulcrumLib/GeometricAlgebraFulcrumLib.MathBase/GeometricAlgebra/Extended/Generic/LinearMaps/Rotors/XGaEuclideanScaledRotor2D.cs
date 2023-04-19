using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors
{
    /// <summary>
    /// This class 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class XGaEuclideanScaledRotor2D<T>
        : XGaScaledRotorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static XGaEuclideanScaledRotor2D<T> Create(XGaProcessor<T> processor, Scalar<T> scalar0)
        {
            return new XGaEuclideanScaledRotor2D<T>(processor, scalar0, processor.ScalarProcessor.CreateScalar(0));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static XGaEuclideanScaledRotor2D<T> Create(XGaProcessor<T> processor, Scalar<T> scalar0, Scalar<T> scalar12)
        {
            return new XGaEuclideanScaledRotor2D<T>(processor, scalar0, scalar12);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator XGaMultivector<T>(XGaEuclideanScaledRotor2D<T> rotor)
        {
            return rotor.GetMultivector();
        }


        public Scalar<T> Scalar0 { get; }

        public Scalar<T> Scalar12 { get; }


        private XGaEuclideanScaledRotor2D(XGaProcessor<T> processor, Scalar<T> scalar0, Scalar<T> scalar12)
            : base(processor)
        {
            Scalar0 = scalar0;
            Scalar12 = scalar12;
        }


        public override bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaEuclideanScaledRotor2D<T> GetPureScaledRotor2DInverse()
        {
            var scalingFactor = GetScalingFactor();

            return new XGaEuclideanScaledRotor2D<T>(
                Processor,
                Scalar0 / scalingFactor,
                -Scalar12 / scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalingFactor()
        {
            return Scalar0 * Scalar0 + Scalar12 * Scalar12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaEuclideanScaledRotor2D<T> GetPureRotor()
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
        public override IXGaScaledRotor<T> GetScaledRotorInverse()
        {
            return GetPureScaledRotor2DInverse();
        }

        public XGaVector<T> OmMap(T u1, T u2)
        {
            var s0 = Scalar0 * Scalar0 - Scalar12 * Scalar12;
            var s12 = 2 * Scalar0 * Scalar12;
            var v1 = s0 * u1 + s12 * u2;
            var v2 = s0 * u2 - s12 * u1;

            return Processor.CreateVector(v1.ScalarValue, v2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> OmMap(XGaVector<T> mv)
        {
            return OmMap(mv[0], mv[1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> OmMap(XGaBivector<T> mv)
        {
            return GetScalingFactor() * mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> mv)
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> OmMap(XGaKVector<T> mv)
        {
            return mv.Grade switch
            {
                0 or 2 => GetScalingFactor() * mv,
                1 => OmMap(mv[0].ScalarValue, mv[1].ScalarValue),
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

            var s0 = Scalar0 * Scalar0 - Scalar12 * Scalar12;
            var s12 = 2 * Scalar0 * Scalar12;

            var scalingFactor = GetScalingFactor();

            var v0 = scalingFactor * mv0;
            var v1 = s0 * mv1 + s12 * mv2;
            var v2 = s0 * mv2 - s12 * mv1;
            var v12 = scalingFactor * mv12;

            return Processor
                .CreateComposer()
                .SetTerm(0, v0.ScalarValue)
                .SetTerm(1, v1.ScalarValue)
                .SetTerm(2, v2.ScalarValue)
                .SetTerm(3, v12.ScalarValue)
                .GetSimpleMultivector();
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public EuclideanScaledRotor2D<T> GpSquared()
        //{
        //    var s0 = ScalarProcessor.Subtract(
        //        ScalarProcessor.Times(Scalar0, Scalar0),
        //        ScalarProcessor.Times(Scalar12, Scalar12)
        //    );

        //    var s12 = ScalarProcessor.Times(
        //        ScalarProcessor.ScalarTwo,
        //        ScalarProcessor.Times(Scalar0, Scalar12)
        //    );

        //    return new EuclideanScaledRotor2D<T>(GeometricProcessor, s0, s12);
        //}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaEuclideanScaledRotor2D<T> Gp(XGaEuclideanScaledRotor2D<T> rotor2)
        {
            var s0 = Scalar0 * rotor2.Scalar0 - Scalar12 * rotor2.Scalar12;
            var s12 = Scalar0 * rotor2.Scalar12 + Scalar12 * rotor2.Scalar0;

            return new XGaEuclideanScaledRotor2D<T>(
                Processor, 
                s0, 
                s12
            );
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

        //    return GeometricProcessor.CreateVectorStorage(v1, v2);
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivector()
        {
            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0.ScalarValue)
                .SetTerm(3, Scalar12.ScalarValue)
                .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivectorReverse()
        {
            return Processor
                .CreateComposer()
                .SetTerm(0, Scalar0.ScalarValue)
                .SetTerm(3, ScalarProcessor.Negative(Scalar12.ScalarValue))
                .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivectorInverse()
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