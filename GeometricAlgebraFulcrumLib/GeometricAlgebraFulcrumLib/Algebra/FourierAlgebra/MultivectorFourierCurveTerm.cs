using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.FourierAlgebra
{
    public class MultivectorFourierCurveTerm<T>
    {
        public RGaProcessor<T> Processor 
            => CosMultivector.Processor;

        public RGaMultivector<T> CosMultivector { get; private set; }

        public RGaMultivector<T> SinMultivector { get; private set; }

        public Scalar<T> Frequency { get; }


        internal MultivectorFourierCurveTerm(RGaMultivector<T> cosMultivector, RGaMultivector<T> sinMultivector, Scalar<T> frequency)
        {
            CosMultivector = cosMultivector;
            SinMultivector = sinMultivector;
            Frequency = frequency;
        }


        public MultivectorFourierCurveTerm<T> AddMultivectors(RGaMultivector<T> cosMultivector, RGaMultivector<T> sinMultivector)
        {
            CosMultivector += cosMultivector;
            SinMultivector += sinMultivector;

            return this;
        }

        public RGaMultivector<T> GetValue(Scalar<T> parameterValue)
        {
            var angle = Frequency * parameterValue;

            return CosMultivector * angle.Cos() + SinMultivector * angle.Sin();
        }
        
        public RGaMultivector<T> GetValue(T parameterValue)
        {
            var angle = Frequency * parameterValue;

            return CosMultivector * angle.Cos() + SinMultivector * angle.Sin();
        }

        public MultivectorFourierCurveTerm<T> GetDerivative(int degree = 1)
        {
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
                0 => new MultivectorFourierCurveTerm<T>(
                    f * SinMultivector, 
                    f * CosMultivector, 
                    Frequency
                ),

                1 => new MultivectorFourierCurveTerm<T>(
                    f * SinMultivector, 
                    -f * CosMultivector, 
                    Frequency
                ),
                
                2 => new MultivectorFourierCurveTerm<T>(
                    -f * CosMultivector, 
                    -f * SinMultivector, 
                    Frequency
                ),
                
                _ => new MultivectorFourierCurveTerm<T>(
                    -f * SinMultivector, 
                    f * CosMultivector, 
                    Frequency
                )
            };
        }
    }
}