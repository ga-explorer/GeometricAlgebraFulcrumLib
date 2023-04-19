using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra
{
    public sealed class VectorFourierInterpolatorTerm
    {
        public XGaFloat64Vector CosVector { get; private set; }

        public XGaFloat64Vector SinVector { get; private set; }

        public double Frequency { get; }


        internal VectorFourierInterpolatorTerm(XGaFloat64Vector cosVector, XGaFloat64Vector sinVector, double frequency)
        {
            CosVector = cosVector;
            SinVector = frequency >= 0 ? sinVector : -sinVector;
            Frequency = frequency.Abs();
        }


        internal VectorFourierInterpolatorTerm AddVectors(XGaFloat64Vector cosVector, XGaFloat64Vector sinVector)
        {
            CosVector += cosVector;
            SinVector += sinVector;

            return this;
        }
        
        public XGaFloat64Vector GetVector(double parameterValue)
        {
            var angle = Frequency * parameterValue;

            return CosVector * angle.Cos() + SinVector * angle.Sin();
        }
        
        public XGaFloat64Vector GetVectorDt(double parameterValue, int degree = 1)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            if (degree == 0)
                return GetVector(parameterValue);

            var f = degree switch
            {
                1 => Frequency,
                2 => Frequency.Square(),
                3 => Frequency.Cube(),
                _ => Frequency.Power(degree)
            };

            var (cosVector, sinVector) = (degree % 4) switch
            {
                0 => new Pair<XGaFloat64Vector>(f * CosVector, f * SinVector),
                1 => new Pair<XGaFloat64Vector>(f * SinVector, -f * CosVector),
                2 => new Pair<XGaFloat64Vector>(-f * CosVector, -f * SinVector),
                _ => new Pair<XGaFloat64Vector>(-f * SinVector, f * CosVector)
            };

            var angle = Frequency * parameterValue;

            return cosVector * angle.Cos() + sinVector * angle.Sin();
        }

        public VectorFourierInterpolatorTerm GetTermDerivative(int degree = 1)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            if (degree == 0)
                return this;

            var n = degree % 4;

            var f = degree switch
            {
                1 => Frequency,
                2 => Frequency.Square(),
                3 => Frequency.Cube(),
                _ => Frequency.Power(degree)
            };

            return n switch
            {
                0 => new VectorFourierInterpolatorTerm(
                    f * CosVector, 
                    f * SinVector, 
                    Frequency
                ),

                1 => new VectorFourierInterpolatorTerm(
                    f * SinVector, 
                    -f * CosVector, 
                    Frequency
                ),
                
                2 => new VectorFourierInterpolatorTerm(
                    -f * CosVector, 
                    -f * SinVector, 
                    Frequency
                ),
                
                _ => new VectorFourierInterpolatorTerm(
                    -f * SinVector, 
                    f * CosVector, 
                    Frequency
                )
            };
        }
    }
}