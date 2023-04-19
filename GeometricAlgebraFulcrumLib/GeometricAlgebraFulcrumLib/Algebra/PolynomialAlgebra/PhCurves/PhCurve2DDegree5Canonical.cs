using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.PhCurves
{
    /// <summary>
    /// This class represents a quintic Pythagorean hodograph curve in 3D Euclidean space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PhCurve2DDegree5Canonical<T>
    {
        public static PhCurve2DDegree5Canonical<T> Create(RGaProcessor<T> processor, RGaVector<T> p, RGaVector<T> d)
        {
            return new PhCurve2DDegree5Canonical<T>(processor, p, d);
        }

        public static PhCurve2DDegree5Canonical<T> Create(RGaProcessor<T> processor, T p1, T p2, T d1, T d2)
        {
            return new PhCurve2DDegree5Canonical<T>(
                processor,
                processor.CreateVector(p1, p2),
                processor.CreateVector(d1, d2)
            );
        }


        public BernsteinBasisSet<T> BasisSet { get; }

        private readonly BernsteinBasisPairProductSet<T> _basisPairProductSet;
        private readonly BernsteinBasisPairProductIntegralSet<T> _basisPairProductIntegralSet;

        public Scalar<T> Scalar00 { get; }

        public Scalar<T> Scalar01 { get; }

        public Scalar<T> Scalar02 { get; }

        public Scalar<T> Scalar11 { get; }

        public Scalar<T> Scalar12 { get; }

        public Scalar<T> Scalar22 { get; }

        public RGaVector<T> VectorU { get; }

        public RGaVector<T> Vector00 { get; }

        public RGaVector<T> Vector01 { get; }

        public RGaVector<T> Vector02 { get; }

        public RGaVector<T> Vector11 { get; }

        public RGaVector<T> Vector12 { get; }

        public RGaVector<T> Vector22 { get; }

        public RGaScaledPureRotor<T> ScaledRotor0 { get; }

        public RGaScaledPureRotor<T> ScaledRotor1 { get; }

        public RGaScaledPureRotor<T> ScaledRotor2 { get; }

        public RGaScaledPureRotor<T> ScaledRotorV { get; }

        public RGaProcessor<T> GeometricProcessor { get; }


        private PhCurve2DDegree5Canonical(RGaProcessor<T> processor, RGaVector<T> p, RGaVector<T> d)
        {
            GeometricProcessor = processor;

            BasisSet = BernsteinBasisSet<T>.Create(processor.ScalarProcessor, 2);
            _basisPairProductSet = BernsteinBasisPairProductSet<T>.Create(BasisSet);
            _basisPairProductIntegralSet = BernsteinBasisPairProductIntegralSet<T>.Create(_basisPairProductSet);

            var e1 = processor.CreateVector(0);

            ScaledRotor0 = processor.CreateScaledIdentityRotor();

            var f01 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
            var f11 = _basisPairProductIntegralSet.GetValueAt1(1, 1);
            var f12 = _basisPairProductIntegralSet.GetValueAt1(1, 2);

            var dNorm = d.ENorm();
            var dUnit = d / dNorm;
            
            ScaledRotor2 = e1.CreateScaledPureRotor(d);

            Vector00 = e1;
            Vector22 = d;
            Vector02 = (e1.Gp(ScaledRotor2.MultivectorReverse) + ScaledRotor2.Multivector.Gp(e1)).GetVectorPart();

            VectorU = p - (e1 + d) / 8 + Vector02 / 24;
            var uNorm = VectorU.ENorm();
            var uUnit = VectorU / uNorm;

            var v1 = f11.Sqrt();
            var v0 = f01 / v1;
            var v2 = f12 / v1;
            
            var v = e1.CreateScaledPureRotor(VectorU).Multivector;

            ScaledRotorV = processor.CreateScaledPureRotor2D(v[0], v[3]);

            var a1 = (v - v0 - v2 * ScaledRotor2.Multivector) / v1;

            ScaledRotor1 = processor.CreateScaledPureRotor2D(a1[0], a1[3]);

            Vector01 = (e1.Gp(ScaledRotor1.MultivectorReverse) + ScaledRotor1.Multivector.Gp(e1)).GetVectorPart();
            Vector12 = (ScaledRotor1.Multivector.Gp(e1).Gp(ScaledRotor2.MultivectorReverse) + ScaledRotor2.Multivector.Gp(e1).Gp(ScaledRotor1.MultivectorReverse)).GetVectorPart();
            Vector11 = ScaledRotor1.Multivector.Gp(e1).Gp(ScaledRotor1.MultivectorReverse).GetVectorPart();

            Scalar00 = ScaledRotor0.Multivector.ESp(ScaledRotor0.MultivectorReverse);
            Scalar11 = ScaledRotor1.Multivector.ESp(ScaledRotor1.MultivectorReverse);
            Scalar22 = ScaledRotor2.Multivector.ESp(ScaledRotor2.MultivectorReverse);

            Scalar01 = ScaledRotor0.Multivector.ESp(ScaledRotor1.MultivectorReverse) +
                       ScaledRotor1.Multivector.ESp(ScaledRotor0.MultivectorReverse);

            Scalar02 = ScaledRotor0.Multivector.ESp(ScaledRotor2.MultivectorReverse) +
                       ScaledRotor2.Multivector.ESp(ScaledRotor0.MultivectorReverse);

            Scalar12 = ScaledRotor1.Multivector.ESp(ScaledRotor2.MultivectorReverse) +
                       ScaledRotor2.Multivector.ESp(ScaledRotor1.MultivectorReverse);
        }


        public RGaVector<T> GetHodographPoint(T parameterValue)
        {
            var f00 = _basisPairProductSet.GetValue(0, 0, parameterValue);
            var f01 = _basisPairProductSet.GetValue(0, 1, parameterValue);
            var f02 = _basisPairProductSet.GetValue(0, 2, parameterValue);
            var f11 = _basisPairProductSet.GetValue(1, 1, parameterValue);
            var f12 = _basisPairProductSet.GetValue(1, 2, parameterValue);
            var f22 = _basisPairProductSet.GetValue(2, 2, parameterValue);

            return 
                f00 * Vector00 +
                f01 * Vector01 +
                f02 * Vector02 +
                f11 * Vector11 +
                f12 * Vector12 +
                f22 * Vector22;
        }

        public RGaVector<T> GetCurvePoint(T parameterValue)
        {
            var f00 = _basisPairProductIntegralSet.GetValue(0, 0, parameterValue);
            var f01 = _basisPairProductIntegralSet.GetValue(0, 1, parameterValue);
            var f02 = _basisPairProductIntegralSet.GetValue(0, 2, parameterValue);
            var f11 = _basisPairProductIntegralSet.GetValue(1, 1, parameterValue);
            var f12 = _basisPairProductIntegralSet.GetValue(1, 2, parameterValue);
            var f22 = _basisPairProductIntegralSet.GetValue(2, 2, parameterValue);

            return 
                f00 * Vector00 +
                f01 * Vector01 +
                f02 * Vector02 +
                f11 * Vector11 +
                f12 * Vector12 +
                f22 * Vector22;
        }

        public Scalar<T> GetSigmaValue(T parameterValue)
        {
            var f00 = _basisPairProductSet.GetValue(0, 0, parameterValue);
            var f01 = _basisPairProductSet.GetValue(0, 1, parameterValue);
            var f02 = _basisPairProductSet.GetValue(0, 2, parameterValue);
            var f11 = _basisPairProductSet.GetValue(1, 1, parameterValue);
            var f12 = _basisPairProductSet.GetValue(1, 2, parameterValue);
            var f22 = _basisPairProductSet.GetValue(2, 2, parameterValue);

            return 
                f00 * Scalar00 +
                f01 * Scalar01 +
                f02 * Scalar02 +
                f11 * Scalar11 +
                f12 * Scalar12 +
                f22 * Scalar22;
        }

        public Scalar<T> GetLength(T parameterValue)
        {
            var f00 = _basisPairProductIntegralSet.GetValue(0, 0, parameterValue);
            var f01 = _basisPairProductIntegralSet.GetValue(0, 1, parameterValue);
            var f02 = _basisPairProductIntegralSet.GetValue(0, 2, parameterValue);
            var f11 = _basisPairProductIntegralSet.GetValue(1, 1, parameterValue);
            var f12 = _basisPairProductIntegralSet.GetValue(1, 2, parameterValue);
            var f22 = _basisPairProductIntegralSet.GetValue(2, 2, parameterValue);

            return 
                f00 * Scalar00 +
                f01 * Scalar01 +
                f02 * Scalar02 +
                f11 * Scalar11 +
                f12 * Scalar12 +
                f22 * Scalar22;
        }

        public Scalar<T> GetLength(T parameterValue1, T parameterValue2)
        {
            return GetLength(parameterValue2) - GetLength(parameterValue1);
        }

        public Scalar<T> GetLength()
        {
            var f00 = _basisPairProductIntegralSet.GetValueAt1(0, 0);
            var f01 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
            var f02 = _basisPairProductIntegralSet.GetValueAt1(0, 2);
            var f11 = _basisPairProductIntegralSet.GetValueAt1(1, 1);
            var f12 = _basisPairProductIntegralSet.GetValueAt1(1, 2);
            var f22 = _basisPairProductIntegralSet.GetValueAt1(2, 2);

            return 
                f00 * Scalar00 +
                f01 * Scalar01 +
                f02 * Scalar02 +
                f11 * Scalar11 +
                f12 * Scalar12 +
                f22 * Scalar22;
        }
    }
}