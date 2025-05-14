using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Fourier;

public sealed class VectorFourierCurveTerm<T>
{
    public XGaProcessor<T> Processor 
        => CosVector.Processor;

    public XGaVector<T> CosVector { get; private set; }

    public XGaVector<T> SinVector { get; private set; }

    public Scalar<T> Frequency { get; }


    internal VectorFourierCurveTerm(XGaVector<T> cosVector, XGaVector<T> sinVector, Scalar<T> frequency)
    {
        CosVector = cosVector;
        SinVector = sinVector;
        Frequency = frequency;
    }


    public VectorFourierCurveTerm<T> AddVectors(XGaVector<T> cosVector, XGaVector<T> sinVector)
    {
        CosVector += cosVector;
        SinVector += sinVector;

        return this;
    }

    public XGaVector<T> GetValue(Scalar<T> parameterValue)
    {
        var angle = Frequency * parameterValue;

        return CosVector * angle.Cos() + SinVector * angle.Sin();
    }
        
    public XGaVector<T> GetValue(T parameterValue)
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