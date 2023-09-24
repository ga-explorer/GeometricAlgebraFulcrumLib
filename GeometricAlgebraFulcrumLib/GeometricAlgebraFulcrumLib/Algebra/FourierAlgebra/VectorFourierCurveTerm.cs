using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.FourierAlgebra
{
    public sealed class VectorFourierCurveTerm<T>
    {
        public RGaProcessor<T> Processor 
            => CosVector.Processor;

        public RGaVector<T> CosVector { get; private set; }

        public RGaVector<T> SinVector { get; private set; }

        public Scalar<T> Frequency { get; }


        internal VectorFourierCurveTerm(RGaVector<T> cosVector, RGaVector<T> sinVector, Scalar<T> frequency)
        {
            CosVector = cosVector;
            SinVector = sinVector;
            Frequency = frequency;
        }


        public VectorFourierCurveTerm<T> AddVectors(RGaVector<T> cosVector, RGaVector<T> sinVector)
        {
            CosVector += cosVector;
            SinVector += sinVector;

            return this;
        }

        public RGaVector<T> GetValue(Scalar<T> parameterValue)
        {
            var angle = Frequency * parameterValue;

            return CosVector * angle.Cos() + SinVector * angle.Sin();
        }
        
        public RGaVector<T> GetValue(T parameterValue)
        {
            var angle = Frequency * parameterValue;

            return CosVector * angle.Cos() + SinVector * angle.Sin();
        }

        public VectorFourierCurveTerm<T> GetTermDerivative(int degree = 1)
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
                0 => new VectorFourierCurveTerm<T>(
                    f * SinVector, 
                    f * CosVector, 
                    Frequency
                ),

                1 => new VectorFourierCurveTerm<T>(
                    f * SinVector, 
                    -f * CosVector, 
                    Frequency
                ),
                
                2 => new VectorFourierCurveTerm<T>(
                    -f * CosVector, 
                    -f * SinVector, 
                    Frequency
                ),
                
                _ => new VectorFourierCurveTerm<T>(
                    -f * SinVector, 
                    f * CosVector, 
                    Frequency
                )
            };
        }
    }
}