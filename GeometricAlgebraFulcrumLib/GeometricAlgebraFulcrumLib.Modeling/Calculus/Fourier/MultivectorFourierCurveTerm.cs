using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Fourier;

public class MultivectorFourierCurveTerm<T>
{
    public XGaProcessor<T> Processor 
        => CosMultivector.Processor;

    public XGaMultivector<T> CosMultivector { get; private set; }

    public XGaMultivector<T> SinMultivector { get; private set; }

    public Scalar<T> Frequency { get; }


    internal MultivectorFourierCurveTerm(XGaMultivector<T> cosMultivector, XGaMultivector<T> sinMultivector, Scalar<T> frequency)
    {
        CosMultivector = cosMultivector;
        SinMultivector = sinMultivector;
        Frequency = frequency;
    }


    public MultivectorFourierCurveTerm<T> AddMultivectors(XGaMultivector<T> cosMultivector, XGaMultivector<T> sinMultivector)
    {
        CosMultivector += cosMultivector;
        SinMultivector += sinMultivector;

        return this;
    }

    public XGaMultivector<T> GetValue(Scalar<T> parameterValue)
    {
        var angle = Frequency * parameterValue;

        return CosMultivector * angle.Cos() + SinMultivector * angle.Sin();
    }
        
    public XGaMultivector<T> GetValue(T parameterValue)
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