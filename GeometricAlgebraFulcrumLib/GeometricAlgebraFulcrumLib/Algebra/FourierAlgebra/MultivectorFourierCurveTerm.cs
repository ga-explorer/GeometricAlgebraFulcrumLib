using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.FourierAlgebra
{
    public class MultivectorFourierCurveTerm<T>
    {
        public IGeometricAlgebraProcessor<T> GeometricProcessor 
            => CosMultivector.GeometricProcessor;

        public GaMultivector<T> CosMultivector { get; private set; }

        public GaMultivector<T> SinMultivector { get; private set; }

        public Scalar<T> Frequency { get; }


        internal MultivectorFourierCurveTerm([NotNull] GaMultivector<T> cosMultivector, [NotNull] GaMultivector<T> sinMultivector, [NotNull] Scalar<T> frequency)
        {
            CosMultivector = cosMultivector;
            SinMultivector = sinMultivector;
            Frequency = frequency;
        }


        public MultivectorFourierCurveTerm<T> AddMultivectors([NotNull] GaMultivector<T> cosMultivector, [NotNull] GaMultivector<T> sinMultivector)
        {
            CosMultivector += cosMultivector;
            SinMultivector += sinMultivector;

            return this;
        }

        public GaMultivector<T> GetValue(Scalar<T> parameterValue)
        {
            var angle = Frequency * parameterValue;

            return CosMultivector * angle.Cos() + SinMultivector * angle.Sin();
        }
        
        public GaMultivector<T> GetValue(T parameterValue)
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